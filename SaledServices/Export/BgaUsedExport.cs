using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Export
{
    public partial class BgaUsedExport : Form
    {
        public BgaUsedExport()
        {
            InitializeComponent();

            bgaRepair_resultcomboBox.SelectedIndex = 0;
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<BgaUsedStruct> receiveOrderList = new List<BgaUsedStruct>();
            List<bagUsedSumStruct> bagWaitSumList = new List<bagUsedSumStruct>();
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,repairer,vendor,product,source,orderno,receivedate,bga_repairer,mb_brief,custom_serial_no," +
                "vendor_serail_no,mpn,mb_make_date,customFault,bgatype,BGAPN,bga_brief,mbfa1, short_cut,bga_repair_date,oldSn, newSn from bga_repair_record_table where bga_repair_date between '"
                    + startTime + "' and '" + endTime + "' and bga_repair_result='" + this.bgaRepair_resultcomboBox.Text.Trim() + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    BgaUsedStruct temp = new BgaUsedStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.repairer = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.source = querySdr[4].ToString();
                    temp.orderno = querySdr[5].ToString();
                    temp.receivedate = querySdr[6].ToString();
                    temp.bga_repairer = querySdr[7].ToString();
                    temp.mb_brief = querySdr[8].ToString();
                    temp.custom_serial_no = querySdr[9].ToString();
                    temp.vendor_serail_no = querySdr[10].ToString();
                    temp.mpn = querySdr[11].ToString();
                    temp.mb_make_date = querySdr[12].ToString();
                    temp.customFault = querySdr[13].ToString();
                    temp.bgatype = querySdr[14].ToString();
                    temp.BGAPN = querySdr[15].ToString();

                    temp.bga_brief = querySdr[16].ToString();
                    temp.change_reason = querySdr[17].ToString();
                    temp.shortcut = querySdr[18].ToString();
                    temp.change_date = querySdr[19].ToString();

                    temp.oldSn = querySdr[20].ToString();
                    temp.newSn = querySdr[21].ToString();

                    receiveOrderList.Add(temp);                    
                }
                querySdr.Close();
                //public string customMaterialNo;客户料号
                //public string mb_describe;物料对照表
                //public string ECO;物料对照表
                //public string BGA_describe;、、bom表

                foreach (BgaUsedStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select custommaterialNo,mb_describe from DeliveredTable where track_serial_no='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.customMaterialNo = querySdr[0].ToString();
                        temp.mb_describe = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select eol from MBMaterialCompare where custommaterialNo='" + temp.customMaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.ECO = querySdr[0].ToString();
                    }
                    querySdr.Close();


                    string queryData = "", condition = "";
                    if (temp.bgatype == "CPU")
                    {
                        queryData = "cpu_describe";
                        condition = "cpu_brief";
                        if (temp.oldSn != null && temp.oldSn != "")
                        {
                            //CPU的采购类别
                            cmd.CommandText = "select buy_type from bga_in_stock where bgasn='" + temp.oldSn + "'";
                            querySdr = cmd.ExecuteReader();
                            string isbgaExist="";
                            while (querySdr.Read())
                            {
                                isbgaExist = querySdr[0].ToString();
                            }
                            querySdr.Close();

                            if(isbgaExist != "")
                            {
                                temp.cpu_buy_type = isbgaExist;
                            }else{
                                temp.cpu_buy_type = "首次更换";
                            }

                            //
                            cmd.CommandText = "select Id from old_cpu_sn_record where oldsn='" + temp.oldSn + "'";
                            querySdr = cmd.ExecuteReader();
                            if (querySdr.HasRows)
                            {
                                temp.cpu_change = "已拆件";
                            }
                            else
                            {
                                temp.cpu_change = "未拆件";
                            }
                            querySdr.Close();
                        }
                        else
                        {
                            temp.cpu_change = "老SN无记录";
                        }
                    }
                    else if (temp.bgatype == "PCH")
                    {
                        queryData = "pcb_describe";
                        condition = "pcb_brief_describe";
                    }
                    else if (temp.bgatype == "VGA")
                    {
                        queryData = "vga_describe";
                        condition = "vga_brief_describe";
                    }

                    cmd.CommandText = "select "+queryData+" from MBMaterialCompare where "+condition+"='" + temp.bga_brief + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.BGA_describe = querySdr[0].ToString();
                    }
                    querySdr.Close();


                    //下面是统计信息
                    if (bagWaitSumList.Count == 0)
                    {
                        if (temp.bga_brief != null && temp.bga_brief.Trim() != "")
                        {
                            bagUsedSumStruct newTemp = new bagUsedSumStruct();
                            newTemp.bga_brief = temp.bga_brief;
                            newTemp.bga_type = temp.bgatype;
                            newTemp.eco = temp.ECO;
                            bagWaitSumList.Add(newTemp);
                        }
                    }
                    else
                    {
                        bool exist = false;
                        foreach (bagUsedSumStruct oldrecord in bagWaitSumList)
                        {
                            if (oldrecord.equals(temp.bga_brief))
                            {
                                oldrecord.incrementNum();
                                exist = true;
                                break;
                            }
                        }

                        if (exist == false)
                        {
                            if (temp.bga_brief != null && temp.bga_brief.Trim() != "")
                            {
                                bagUsedSumStruct newTemp = new bagUsedSumStruct();
                                newTemp.bga_brief = temp.bga_brief;
                                newTemp.bga_type = temp.bgatype;
                                newTemp.eco = temp.ECO;
                                bagWaitSumList.Add(newTemp);
                            }
                        }
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime, bagWaitSumList);
        }

        public void generateExcelToCheck(List<BgaUsedStruct> StockCheckList, string startTime, string endTime, List<bagUsedSumStruct> bagWaitSumList)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent firstsheet = new allContent();
            firstsheet.sheetName = "BGA待料信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            firstsheet.titleList = new List<string>();
            firstsheet.contentList = new List<object>();

            firstsheet.titleList.Add("跟踪条码");
            firstsheet.titleList.Add("客户料号");
            firstsheet.titleList.Add("厂商");
            firstsheet.titleList.Add("客户别");
            firstsheet.titleList.Add("来源");
            firstsheet.titleList.Add("订单编号");
            firstsheet.titleList.Add("收货日期");
            firstsheet.titleList.Add("MB描述");
            firstsheet.titleList.Add("MB简称");
            firstsheet.titleList.Add("客户序号");
            firstsheet.titleList.Add("厂商序号");
            firstsheet.titleList.Add("MPN");
            firstsheet.titleList.Add("MB生产日期");
            firstsheet.titleList.Add("客户故障");
            firstsheet.titleList.Add("ECO");
            firstsheet.titleList.Add("bga类型");
            firstsheet.titleList.Add("BGAPN");
            firstsheet.titleList.Add("BGA描述");
            firstsheet.titleList.Add("BGA简述");
            firstsheet.titleList.Add("老sn");
            firstsheet.titleList.Add("新sn");
            				
            firstsheet.titleList.Add("更换原因");
            firstsheet.titleList.Add("短路电压");
            firstsheet.titleList.Add("维修人");
            firstsheet.titleList.Add("更换人");
            firstsheet.titleList.Add("更换日期");
            firstsheet.titleList.Add("CPU采购类别");
            firstsheet.titleList.Add("CPU是否更换");

            foreach (BgaUsedStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.customMaterialNo);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.source);
                ct1.Add(stockcheck.orderno);
                ct1.Add(Utils.modifyDataFormat(stockcheck.receivedate));
                ct1.Add(stockcheck.mb_describe);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.vendor_serail_no);
                ct1.Add(stockcheck.mpn);
                ct1.Add(Utils.modifyDataFormat(stockcheck.mb_make_date));
                ct1.Add(stockcheck.customFault);
                ct1.Add(stockcheck.ECO);
                ct1.Add(stockcheck.bgatype);
                ct1.Add(stockcheck.BGAPN);
                ct1.Add(stockcheck.BGA_describe);

                ct1.Add(stockcheck.bga_brief);
                ct1.Add(stockcheck.oldSn);
                ct1.Add(stockcheck.newSn);

                ct1.Add(stockcheck.change_reason);
                ct1.Add(stockcheck.shortcut);
                ct1.Add(stockcheck.repairer);
                ct1.Add(stockcheck.bga_repairer);
                ct1.Add(Utils.modifyDataFormat(stockcheck.change_date));
                ct1.Add(stockcheck.cpu_buy_type);
                ct1.Add(stockcheck.cpu_change);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(firstsheet);

            allContent secondsheet = new allContent();
            secondsheet.sheetName = "统计信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            secondsheet.titleList = new List<string>();
            secondsheet.contentList = new List<object>();

            secondsheet.titleList.Add("ECO");
            secondsheet.titleList.Add("BGA类型");
            secondsheet.titleList.Add("BGA简述");
            secondsheet.titleList.Add("待货数量");

            foreach (bagUsedSumStruct stockcheck in bagWaitSumList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.eco);
                ct1.Add(stockcheck.bga_type);
                ct1.Add(stockcheck.bga_brief);
                ct1.Add(stockcheck.returnNum+"");

                ctest1.contentArray = ct1;
                secondsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(secondsheet);

            Utils.createMulitSheetsUsingNPOI("BGA维修信息" + bgaRepair_resultcomboBox .Text.Trim()+"-"+ startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);
        }
    }

    public class bagUsedSumStruct
    {
        public string bga_brief;
        public string eco;
        public string bga_type;
        public int returnNum = 1;

        public bool equals(string bgabrief)
        {
            return (this.bga_brief == bgabrief);
        }

        public void incrementNum()
        {
            this.returnNum++;
        }
    }
    
    //跟踪条码	客户料号 厂商 客户别	来源	订单编号 收货日期 MB描述	MB简称	客户序号 厂商序号 MPN	MB生产日期	客户故障	ECO	bga类型	BGAPN	BGA描述 BGA简述	
    //更换原因	短路电压	维修人	更换人	更换日期
   public class BgaUsedStruct
    {
        public string track_serial_no;
        public string customMaterialNo;
        public string vendor;
        public string product;
        public string source;
        public string orderno;
        public string receivedate;
        public string mb_describe;
        public string mb_brief;
        public string custom_serial_no;
        public string vendor_serail_no;
        public string mpn;
        public string mb_make_date;
        public string customFault;
        public string ECO;
        public string bgatype;
        public string BGAPN;
        public string BGA_describe;
        public string bga_brief;

        public string oldSn;
        public string newSn;

        public string change_reason;
        public string shortcut;
        public string repairer;
        public string bga_repairer;
        public string change_date;

        public string cpu_buy_type;
        public string cpu_change;
    }
}

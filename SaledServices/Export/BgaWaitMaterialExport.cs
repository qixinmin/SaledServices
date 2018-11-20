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
    public partial class BgaWaitMaterialExport : Form
    {
        public BgaWaitMaterialExport()
        {
            InitializeComponent();
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

            List<BgaWaitMaterialStruct> receiveOrderList = new List<BgaWaitMaterialStruct>();
            List<bagWaitSumStruct> bagWaitSumList = new List<bagWaitSumStruct>();
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,customMaterialNo,vendor,product,source,orderno,receivedate,mb_describe,mb_brief,custom_serial_no,"+
                "vendor_serail_no,mpn,mb_make_date,customFault,ECO,bgatype,BGAPN,BGA_describe,bga_brief,inputer,input_date from bga_wait_material_record_table where status!='' input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    BgaWaitMaterialStruct temp = new BgaWaitMaterialStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.customMaterialNo = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.source = querySdr[4].ToString();
                    temp.orderno = querySdr[5].ToString();
                    temp.receivedate = querySdr[6].ToString();
                    temp.mb_describe = querySdr[7].ToString();
                    temp.mb_brief = querySdr[8].ToString();
                    temp.custom_serial_no = querySdr[9].ToString();
                    temp.vendor_serail_no = querySdr[10].ToString();
                    temp.mpn = querySdr[11].ToString();
                    temp.mb_make_date = querySdr[12].ToString();
                    temp.customFault = querySdr[13].ToString();
                    temp.ECO = querySdr[14].ToString();
                    temp.bgatype = querySdr[15].ToString();
                    temp.BGAPN = querySdr[16].ToString();
                    temp.BGA_describe = querySdr[17].ToString();

                    temp.bga_brief = querySdr[18].ToString();
                    temp.inputer = querySdr[19].ToString();
                    temp.input_date = querySdr[20].ToString();

                    receiveOrderList.Add(temp);

                    //下面是统计信息
                    if (bagWaitSumList.Count == 0)
                    {
                        if (temp.bga_brief != null && temp.bga_brief.Trim() != "")
                        {
                            bagWaitSumStruct newTemp = new bagWaitSumStruct();
                            newTemp.bga_brief = temp.bga_brief;
                            newTemp.bga_type = temp.bgatype;
                            newTemp.eco = temp.ECO;
                            bagWaitSumList.Add(newTemp);
                        }
                    }
                    else
                    {
                        bool exist = false;
                        foreach (bagWaitSumStruct oldrecord in bagWaitSumList)
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
                                bagWaitSumStruct newTemp = new bagWaitSumStruct();
                                newTemp.bga_brief = temp.bga_brief;
                                newTemp.bga_type = temp.bgatype;
                                newTemp.eco = temp.ECO;
                                bagWaitSumList.Add(newTemp);
                            }
                        }
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime, bagWaitSumList);
        }

        public void generateExcelToCheck(List<BgaWaitMaterialStruct> StockCheckList, string startTime, string endTime, List<bagWaitSumStruct> bagWaitSumList)
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
            firstsheet.titleList.Add("bga维修人");
            firstsheet.titleList.Add("bga维修日期");

            foreach (BgaWaitMaterialStruct stockcheck in StockCheckList)
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
                ct1.Add(stockcheck.inputer);
                ct1.Add(Utils.modifyDataFormat(stockcheck.input_date));

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

            foreach (bagWaitSumStruct stockcheck in bagWaitSumList)
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

            Utils.createMulitSheetsUsingNPOI("D:\\BGA待料信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);
        }
    }

    public class bagWaitSumStruct
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

   public class BgaWaitMaterialStruct
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
        public string inputer;
        public string input_date;
    }
}

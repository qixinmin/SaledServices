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
    public partial class FaultMBRecordExport : Form
    {
        public FaultMBRecordExport()
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

            List<FaultMbStruct> receiveOrderList = new List<FaultMbStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,vendor_sn,vendor,product,mb_brief,mpn,mb_describe,_status,pch_brief,vga_brief,cpu_brief,"+
                             "repairer,repair_date  from fault_mb_enter_record_table where repair_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    FaultMbStruct temp = new FaultMbStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.vendor_sn = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.mb_brief = querySdr[4].ToString();
                    temp.mpn = querySdr[5].ToString();
                    temp.mb_describe = querySdr[6].ToString();
                    temp._status = querySdr[7].ToString();
                    temp.pch_brief = querySdr[8].ToString();

                    temp.vga_brief = querySdr[9].ToString();
                    temp.cpu_brief = querySdr[10].ToString();
                    temp.repairer = querySdr[11].ToString();
                    temp.repair_date = querySdr[12].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                //遍历查找库位
                foreach (FaultMbStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select house, place from store_house_ng where mpn='" + temp.mpn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.house = querySdr[0].ToString();
                        temp.place = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();
                }

                foreach (FaultMbStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select fault_describe,fault_place,fault_reason,confirmer,confirm_date,pcbzhouqi,pcbtype from fault_mb_confirm_table where track_serial_no='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.fault_describe = querySdr[0].ToString();
                        temp.fault_place = querySdr[1].ToString();

                        temp.fault_reason = querySdr[0].ToString();
                        temp.confirmer = querySdr[1].ToString();
                        temp.confirm_date = querySdr[0].ToString();
                        temp.pcbzhouqi = querySdr[1].ToString();
                        temp.pcbtype = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<FaultMbStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("厂商sn");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MB简称");
            titleList.Add("MPN");
            titleList.Add("MB描述");

            titleList.Add("状态");
            titleList.Add("PCH描述");
            titleList.Add("VGA描述");
            titleList.Add("CPU描述");

            titleList.Add("入库人");
            titleList.Add("入库日期");
            titleList.Add("库房");
            titleList.Add("库位");
            titleList.Add("类型");


            titleList.Add("缺陷描述");
            titleList.Add("缺陷位置");
            titleList.Add("缺陷原因");
            titleList.Add("确认人");
            titleList.Add("确认日期");
            titleList.Add("PCB Zhouqi");
            titleList.Add("PCB类型");

            foreach (FaultMbStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.vendor_sn);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_describe);
                ct1.Add(stockcheck._status);
                ct1.Add(stockcheck.pch_brief);

                ct1.Add(stockcheck.vga_brief);
                ct1.Add(stockcheck.cpu_brief);
                ct1.Add(stockcheck.repairer);
                ct1.Add(stockcheck.repair_date);
                ct1.Add(stockcheck.house);
                ct1.Add(stockcheck.place);
                ct1.Add("报废");

                ct1.Add(stockcheck.fault_describe);
                ct1.Add(stockcheck.fault_place);
                ct1.Add(stockcheck.fault_reason);
                ct1.Add(stockcheck.confirmer);
                ct1.Add(stockcheck.confirm_date);
                ct1.Add(stockcheck.pcbzhouqi);
                ct1.Add(stockcheck.pcbtype);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("MB报废信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class FaultMbStruct
    {
        public string track_serial_no;
        public string vendor_sn;
        public string vendor;
        public string product;
        public string mb_brief;
        public string mpn;
        public string mb_describe;

        public string _status;
        public string pch_brief;
        public string vga_brief;
        public string cpu_brief;

        public string repairer;
        public string repair_date;

        public string house;
        public string place;

        public string fault_describe;
        public string fault_place;
        public string fault_reason;
        public string confirmer;
        public string confirm_date;
        public string pcbzhouqi;
        public string pcbtype;
    }
}

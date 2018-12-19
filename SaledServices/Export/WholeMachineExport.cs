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
    public partial class WholeMachineExport : Form
    {
        public WholeMachineExport()
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

            List<whole_import_sheet> receiveOrderList = new List<whole_import_sheet>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string sql = "select Id,vendor,product,mb_brief,custom_machine_type,machine_pn,machine_describe,mpn,mb_describe,out_number,out_date,"+
                    "gurrante_period,upload_ddate,buy_type,note from whole_import_sheet where out_date between '" + startTime + "' and '" + endTime + "'";

                if (this.mb_briefTextBox.Text.Trim() != "")
                {
                    sql += " and mb_brief like '%" + this.mb_briefTextBox.Text.Trim() + "%'";
                }
                cmd.CommandText = sql;
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    whole_import_sheet temp = new whole_import_sheet();
                    temp.Id = querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.mb_brief = querySdr[3].ToString();
                    temp.custom_machine_type = querySdr[4].ToString();
                    temp.machine_pn = querySdr[5].ToString();
                    temp.machine_describe = querySdr[6].ToString();
                    temp.mpn = querySdr[7].ToString();
                    temp.mb_describe = querySdr[8].ToString();
                    temp.out_number = querySdr[9].ToString();
                    temp.out_date = querySdr[10].ToString();
                    temp.gurrante_period = querySdr[11].ToString();
                    temp.upload_ddate = querySdr[12].ToString();
                    temp.buy_type = querySdr[13].ToString();
                    temp.note = querySdr[14].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<whole_import_sheet> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("ID");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MB简称");
            titleList.Add("客户机型");
            titleList.Add("整机PN");
            titleList.Add("整机描述");
            titleList.Add("MPN");
            titleList.Add("MB描述");
            titleList.Add("出货量");
            titleList.Add("出货日期");
            titleList.Add("保修期");
            titleList.Add("上传日期");
            titleList.Add("材料采购类别");
            titleList.Add("备注");

            foreach (whole_import_sheet stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.custom_machine_type);
                ct1.Add(stockcheck.machine_pn);
                ct1.Add(stockcheck.machine_describe);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_describe);
                ct1.Add(stockcheck.out_number);
                ct1.Add(Utils.modifyDataFormat(stockcheck.out_date));
                ct1.Add(stockcheck.gurrante_period);
                ct1.Add(Utils.modifyDataFormat(stockcheck.upload_ddate));
                ct1.Add(stockcheck.buy_type);
                ct1.Add(stockcheck.note);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("整机出货量信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class whole_import_sheet
    {
        public string Id;
        public string vendor;
        public string product;
        public string mb_brief;
        public string custom_machine_type;
        public string machine_pn;
        public string machine_describe;
        public string mpn;
        public string mb_describe;
        public string out_number;
        public string out_date;
        public string gurrante_period;
        public string upload_ddate;
        public string buy_type;
        public string note;
    }
}

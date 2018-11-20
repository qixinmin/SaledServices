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
    public partial class DPKExport : Form
    {
        public DPKExport()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager())
            {
                this.exportxmlbutton.Enabled = true;
           }
            else
            {
                this.exportxmlbutton.Enabled = false;
            }
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

            List<DpkStruct> receiveOrderList = new List<DpkStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string queryColumn = "";
                if (this.inputdateradioButton.Checked)
                {
                    queryColumn = "upload_date";
                }
                else if (this.usedateradioButton.Checked)
                {
                    queryColumn = "burn_date";
                }

                cmd.CommandText = "select dpk_type,KEYPN,KEYID,KEYSERIAL,_status,burn_date,custom_serial_no,Id,dpk_order_no,upload_date from DPK_table where " + queryColumn + " between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    DpkStruct temp = new DpkStruct();
                    temp.dpk_type =  querySdr[0].ToString();
                    temp.KEYPN =  querySdr[1].ToString();
                    temp.KEYID = querySdr[2].ToString();
                    temp.KEYSERIAL= querySdr[3].ToString();
                    temp._status = querySdr[4].ToString();
                    temp.burn_date = querySdr[5].ToString();
                    temp.custom_serial_no = querySdr[6].ToString();

                    temp.id = querySdr[7].ToString();
                    temp.dpk_order_no = querySdr[8].ToString();
                    temp.upload_date = querySdr[9].ToString();

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

        public void generateExcelToCheck(List<DpkStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("Id");
            titleList.Add("订单号");

            titleList.Add("DPK类别");
            titleList.Add("KEYPN");
            titleList.Add("KEYID");
            titleList.Add("KEYSERIAL");
            titleList.Add("上传日期");
            titleList.Add("状态");
            titleList.Add("烧录日期");
            titleList.Add("客户序号");

            //    dpk_type NVARCHAR(128) NOT NULL, /*DPK类别*/
            //KEYPN NVARCHAR(128) NOT NULL, /*KEYPN*/
            //KEYID  NVARCHAR(128) NOT NULL, /*KEYID*/
            //KEYSERIAL NVARCHAR(128) NOT NULL, /*KEYSERIAL*/
            //_status NVARCHAR(128) NOT NULL, /*状态*/
            //burn_date date,/*烧录日期*/
            //custom_serial_no NVARCHAR(128) /*客户序号*/

            foreach (DpkStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.id);
                ct1.Add(stockcheck.dpk_order_no);
                ct1.Add(stockcheck.dpk_type);
                ct1.Add(stockcheck.KEYPN);
                ct1.Add(stockcheck.KEYID);
                ct1.Add(stockcheck.KEYSERIAL);
                ct1.Add((stockcheck.upload_date!=null && stockcheck.upload_date!="" )? stockcheck.upload_date.Substring(0, stockcheck.upload_date.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck._status);
                ct1.Add((stockcheck.burn_date != null && stockcheck.burn_date != "") ? stockcheck.burn_date.Substring(0, stockcheck.burn_date.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck.custom_serial_no);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            string addtion = "使用日期";
            if (this.inputdateradioButton.Checked)
            {
                addtion = "导入日期";
            }

            Utils.createExcel("D:\\DPK信息" + addtion + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class DpkStruct
    {
       public string id;
          public string dpk_order_no;

        public string dpk_type;
        public string KEYPN;
        public string KEYID;
        public string KEYSERIAL;
          
          public string upload_date;

        public string _status;
        public string burn_date;
        public string taker;
        public string custom_serial_no;


    }
}

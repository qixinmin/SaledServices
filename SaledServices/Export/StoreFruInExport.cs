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
    public partial class StoreFruInExport : Form
    {
        public StoreFruInExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("ID");
            titleList.Add("采购订单编号");
            titleList.Add("厂商");
            titleList.Add("采购类别");
            titleList.Add("客户别");
            titleList.Add("材料大类");
            titleList.Add("MPN");
            titleList.Add("厂商料号");
            titleList.Add("描述");
            titleList.Add("订单数量");
            titleList.Add("单价");
            titleList.Add("MB简称");
            titleList.Add("材料名称");
            titleList.Add("入库数量");
            titleList.Add("金额合计");
            titleList.Add("库位");
            titleList.Add("备注");
            titleList.Add("输入人");
            titleList.Add("日期");

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select *  from fru_smt_in_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();
                    ct1.Add(querySdr[0].ToString());
                    ct1.Add(querySdr[1].ToString());
                    ct1.Add(querySdr[2].ToString());
                    ct1.Add(querySdr[3].ToString());
                    ct1.Add(querySdr[4].ToString());
                    ct1.Add(querySdr[5].ToString());
                    ct1.Add(querySdr[6].ToString());
                    ct1.Add(querySdr[7].ToString());
                    ct1.Add(querySdr[8].ToString());
                    ct1.Add(querySdr[9].ToString());
                    ct1.Add(querySdr[10].ToString());
                    ct1.Add(querySdr[11].ToString());
                    ct1.Add(querySdr[12].ToString());
                    ct1.Add(querySdr[13].ToString());
                    ct1.Add(querySdr[14].ToString());
                    ct1.Add(querySdr[15].ToString());
                    ct1.Add(querySdr[16].ToString());
                    ct1.Add(querySdr[17].ToString());
                    ct1.Add(querySdr[18].ToString());

                    ctest1.contentArray = ct1;
                    contentList.Add(ctest1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Utils.createExcel("FRU收货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }
}

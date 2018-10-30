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
    public partial class StoreHouseStatisticsExport : Form
    {
        private String tableName = "store_house";

        public StoreHouseStatisticsExport()
        {
            InitializeComponent();

            loadAdditionInfomation();
        }

        private void loadAdditionInfomation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct house from " + tableName;
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.houseComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                mConn.Close();
                this.houseComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            if (this.houseComboBox.Text.Trim() == "")
            {
                MessageBox.Show("请选择一个库房");
                return;
            }

            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<StoreHouseStatisticsStruct> receiveOrderList = new List<StoreHouseStatisticsStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,house,place,mpn,number from store_house where house ='"+this.houseComboBox.Text.Trim()+"'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreHouseStatisticsStruct temp = new StoreHouseStatisticsStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.house = querySdr[1].ToString();
                    temp.place = querySdr[2].ToString();                   
                    temp.leftnumber = querySdr[4].ToString();

                    string[] mpn_vendor = querySdr[3].ToString().Split('_');
                    if (mpn_vendor.Length > 1)
                    {
                        temp.mpn = mpn_vendor[0];
                        temp.vendor = mpn_vendor[1];
                    }
                    else
                    {
                        temp.mpn = mpn_vendor[0];
                    }
                    if (temp.leftnumber.Trim() != "")
                    {
                        receiveOrderList.Add(temp);        
                    }
                }
                querySdr.Close();

                foreach (StoreHouseStatisticsStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = "select describe,material_type from stock_in_sheet where mpn ='" + stockcheck.mpn.Split('_')[0] + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stockcheck.describe = querySdr[0].ToString();
                        stockcheck.type = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();
                }

                foreach (StoreHouseStatisticsStruct stockcheck in receiveOrderList)
                {
                    if (stockcheck.type == null)
                    {
                        continue;
                    }
                    string type = stockcheck.type.ToUpper();
                    switch (type)
                    {
                        case "MB":
                            {
                                cmd.CommandText = "select input_number from mb_in_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int mbinnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    mbinnumber += Int32.Parse(querySdr[0].ToString());
                                    //break;
                                }
                                querySdr.Close();
                                stockcheck.buynumber = mbinnumber + "";

                                cmd.CommandText = "select Id from mb_out_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int mboutnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    mboutnumber++;
                                    //break;
                                }
                                querySdr.Close();
                                stockcheck.outnumber = mboutnumber + "";

                                break;
                            }
                        case "BGA":
                            {
                                cmd.CommandText = "select input_number from bga_in_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int bgainnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    bgainnumber += Int32.Parse(querySdr[0].ToString());
                                   // break;
                                }
                                querySdr.Close();
                                stockcheck.buynumber = bgainnumber + "";

                                cmd.CommandText = "select out_number from bga_out_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int bgaoutnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    bgaoutnumber += Int32.Parse(querySdr[0].ToString());
                                   // break;
                                }
                                querySdr.Close();
                                stockcheck.outnumber = bgaoutnumber + "";
                                break;
                            }
                        default://smt/fru
                            {
                                cmd.CommandText = "select stock_in_num from fru_smt_in_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int bgainnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    bgainnumber += Int32.Parse(querySdr[0].ToString());
                                    //break;
                                }
                                querySdr.Close();
                                stockcheck.buynumber = bgainnumber + "";

                                cmd.CommandText = "select stock_out_num from fru_smt_out_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int bgaoutnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    bgaoutnumber += Int32.Parse(querySdr[0].ToString());
                                    //break;
                                }
                                querySdr.Close();
                                stockcheck.outnumber = bgaoutnumber + "";

                                break;
                            }
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<StoreHouseStatisticsStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
                 
            titleList.Add("ID");
            titleList.Add("库房");
            titleList.Add("储位");
            titleList.Add("存储料号");
            titleList.Add("类型");
            titleList.Add("厂商");
            titleList.Add("剩余数量");
            titleList.Add("购买数量");
            titleList.Add("出库数量");
            titleList.Add("描述");

            foreach (StoreHouseStatisticsStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.house);
                ct1.Add(stockcheck.place);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.type);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.leftnumber);
                ct1.Add(stockcheck.buynumber);
                ct1.Add(stockcheck.outnumber);
                ct1.Add(stockcheck.describe);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\"+this.houseComboBox.Text+"平衡表信息导出"+ startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-')  + ".xlsx", titleList, contentList);
        }
    }

   public class StoreHouseStatisticsStruct
    {
        public string Id;
        public string house;
        public string place;
        public string mpn;
        public string type;//mb,bga,smt/fru
        public string leftnumber;

        public string buynumber;
        public string outnumber;

        public string vendor;
        public string describe;
    }
}

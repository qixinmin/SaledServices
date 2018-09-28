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
    public partial class StoreHouseStatusExport : Form
    {
        private String tableName = "store_house";

        public StoreHouseStatusExport()
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
            List<StoreHouseStruct> receiveOrderList = new List<StoreHouseStruct>();

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
                    StoreHouseStruct temp = new StoreHouseStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.house = querySdr[1].ToString();
                    temp.place = querySdr[2].ToString();                   
                    temp.number = querySdr[4].ToString();

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
                    if (temp.number.Trim() != "")
                    {
                        receiveOrderList.Add(temp);
                    }
                }
                querySdr.Close();

                foreach (StoreHouseStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = "select describe from stock_in_sheet where mpn ='" + stockcheck.mpn.Split('_')[0] + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stockcheck.describe = querySdr[0].ToString();
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

            generateExcelToCheck(receiveOrderList);
        }

        public void generateExcelToCheck(List<StoreHouseStruct> StockCheckList)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

                 
            titleList.Add("ID");
            titleList.Add("库房");
            titleList.Add("储位");
            titleList.Add("存储料号");
            titleList.Add("厂商");
            titleList.Add("已存数量");
            titleList.Add("描述");

            foreach (StoreHouseStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.house);
                ct1.Add(stockcheck.place);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.number);
                ct1.Add(stockcheck.describe);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\"+this.houseComboBox.Text+"信息导出"+DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx", titleList, contentList);
        }
    }

   public class StoreHouseStruct
    {
        public string Id;
        public string house;
        public string place;
        public string mpn;
        public string number;

        public string vendor;
        public string describe;
    }
}

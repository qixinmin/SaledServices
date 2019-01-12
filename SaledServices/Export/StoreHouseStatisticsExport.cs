using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<StoreHouseStatisticsStruct> receiveOrderList = new List<StoreHouseStatisticsStruct>();
            Dictionary<string, bgaTypeStatistics> bgabriefdict = new Dictionary<string, bgaTypeStatistics>();
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
                    temp.othernumber = "0";//初始值
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
                                //只是获取简称信息，不需要加入时间限制
                                cmd.CommandText = "select bga_describe from bga_in_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "'";                              
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {                                  
                                    stockcheck.bga_brief = querySdr[0].ToString();
                                    break;
                                }
                                querySdr.Close();

                                cmd.CommandText = "select input_number,bga_describe from bga_in_stock where mpn ='" + stockcheck.mpn.Split('_')[0] + "' and input_date between '" + startTime + "' and '" + endTime + "'";
                                int bgainnumber = 0;
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    bgainnumber += Int32.Parse(querySdr[0].ToString());
                                    stockcheck.bga_brief = querySdr[1].ToString();
                                   // break;
                                }
                                querySdr.Close();
                                stockcheck.buynumber = bgainnumber + "";

                                cmd.CommandText = "select out_number from bga_out_stock where mpn like '%" + stockcheck.mpn + "%' and input_date between '" + startTime + "' and '" + endTime + "'";
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

                //考虑加入报废转卖信息
                foreach (StoreHouseStatisticsStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = " select * from (select  mpn, sum(cast(number as float)) as out_number from TransferOrSold_sheet group by mpn  ) as A where mpn='" + stockcheck.mpn.Split('_')[0] + "'";// and input_date between '" + startTime + "' and '" + endTime + "'";
                    int sumnumber = 0;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        sumnumber += Int32.Parse(querySdr[1].ToString());
                        //break;
                    }
                    querySdr.Close();

                    stockcheck.othernumber = sumnumber + "";
                }

                //按bga简称来统计信息
                foreach (StoreHouseStatisticsStruct stockcheck in receiveOrderList)
                {
                    if (stockcheck.bga_brief != null)
                    {
                        if (bgabriefdict.Keys == null || bgabriefdict.Keys.Contains(stockcheck.bga_brief) == false)
                        {
                            bgaTypeStatistics temp = new bgaTypeStatistics();
                            temp.bga_brief = stockcheck.bga_brief;
                            temp.buynumber = stockcheck.buynumber;
                            temp.leftnumber = stockcheck.leftnumber;
                            temp.outnumber = stockcheck.outnumber;
                            temp.othernumber = stockcheck.othernumber;
                            temp.describe = stockcheck.describe;

                            temp.releatedMpn = stockcheck.mpn;
                            bgabriefdict.Add(temp.bga_brief, temp);
                        }
                        else
                        {
                            try
                            {
                                bgaTypeStatistics temp = bgabriefdict[stockcheck.bga_brief];
                                temp.releatedMpn += "," + stockcheck.mpn;
                                temp.buynumber = Int16.Parse(temp.buynumber) + Int16.Parse(stockcheck.buynumber) + "";
                                temp.leftnumber = Int16.Parse(temp.leftnumber) + Int16.Parse(stockcheck.leftnumber) + "";
                                temp.outnumber = Int16.Parse(temp.outnumber) + Int16.Parse(stockcheck.outnumber) + "";

                                temp.othernumber = Int16.Parse(temp.othernumber) + Int16.Parse(stockcheck.othernumber) + "";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
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

            generateExcelToCheck(receiveOrderList,bgabriefdict, startTime, endTime);
        }

        public void generateExcelToCheck(List<StoreHouseStatisticsStruct> StockCheckList, Dictionary<string, bgaTypeStatistics> bgabriefdict,
            string startTime, string endTime)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent firstsheet = new allContent();
            firstsheet.sheetName = this.houseComboBox.Text + "平衡表信息导出" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            firstsheet.titleList = new List<string>();
            firstsheet.contentList = new List<object>();

            firstsheet.titleList.Add("ID");
            firstsheet.titleList.Add("库房");
            firstsheet.titleList.Add("储位");
            firstsheet.titleList.Add("存储料号");
            firstsheet.titleList.Add("类型");
            firstsheet.titleList.Add("厂商");
            firstsheet.titleList.Add("剩余数量");
            firstsheet.titleList.Add("购买数量");
            firstsheet.titleList.Add("出库数量");
            firstsheet.titleList.Add("报废转卖");
            firstsheet.titleList.Add("描述");
            firstsheet.titleList.Add("BGA简称");

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
                ct1.Add(stockcheck.othernumber);
                ct1.Add(stockcheck.describe);
                ct1.Add(stockcheck.bga_brief);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }
            allcontentList.Add(firstsheet);

            allContent secondsheet = new allContent();
            secondsheet.sheetName = "BGA简称" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            secondsheet.titleList = new List<string>();
            secondsheet.contentList = new List<object>();
            
            secondsheet.titleList.Add("BGA类型");
            secondsheet.titleList.Add("剩余数量");
            secondsheet.titleList.Add("购买数量");
            secondsheet.titleList.Add("出货数量");
            secondsheet.titleList.Add("报废转卖");
            secondsheet.titleList.Add("描述");
            secondsheet.titleList.Add("相关mpn");

            foreach (string key in bgabriefdict.Keys)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();

                List<string> ct1 = new List<string>();
                bgaTypeStatistics stockcheck = bgabriefdict[key];
                ct1.Add(stockcheck.bga_brief);
                ct1.Add(stockcheck.leftnumber);
                ct1.Add(stockcheck.buynumber);
                ct1.Add(stockcheck.outnumber);
                ct1.Add(stockcheck.othernumber);
                ct1.Add(stockcheck.describe);
                ct1.Add(stockcheck.releatedMpn);

                ctest1.contentArray = ct1;
                secondsheet.contentList.Add(ctest1);
            } 
            allcontentList.Add(secondsheet);

            Utils.createMulitSheetsUsingNPOI(string.Format("{0}平衡表信息导出{1}-{2}", this.houseComboBox.Text, startTime.Replace('/', '-'), endTime.Replace('/', '-')) + ".xls", allcontentList);
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

        public string othernumber;//报废转卖

        public string vendor;
        public string describe;

        public string bga_brief;
    }

   public class bgaTypeStatistics
   {
       public string bga_brief;
       public string leftnumber;

       public string buynumber;
       public string outnumber;

       public string othernumber;//报废转卖

       public string describe;

       public string releatedMpn;//所有类型的汇总
    }
}

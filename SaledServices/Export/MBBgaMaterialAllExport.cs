using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Export
{
    public partial class MBBgaMaterialAllExport : Form
    {
        public MBBgaMaterialAllExport()
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

            List<MBBgaMaterialStruct> receiveOrderList = new List<MBBgaMaterialStruct>();           

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //是否应该加入当前的维修记录？？ TODO 根据选择的开始日期与结束日期，分月份使用group by，然后更新总表
                //并考虑次数据是否存在历史记录中，如果不存在，则插入新的数据

                List<queryCondition> querys = new List<queryCondition>();

                //本月
                queryCondition conditions = new queryCondition();
                DateTime current = DateTime.Now;
                conditions.year = current.Year + "年";
                conditions.currentMonth = current.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(current);
                conditions.monthend = Utils.getEndDayOfMonth(current);
                conditions.dbcolumn = Utils.getColumnName(current.Month);
                querys.Add(conditions);

                //上个月
                conditions = new queryCondition();
                current = DateTime.Now.AddMonths(-1);
                conditions.year = current.Year + "年";
                conditions.currentMonth = current.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(current);
                conditions.monthend = Utils.getEndDayOfMonth(current);
                conditions.dbcolumn = Utils.getColumnName(current.Month);
                querys.Add(conditions);

                //上上月
                conditions = new queryCondition();
                current = DateTime.Now.AddMonths(-2);
                conditions.year = current.Year + "年";
                conditions.currentMonth = current.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(current);
                conditions.monthend = Utils.getEndDayOfMonth(current);
                conditions.dbcolumn = Utils.getColumnName(current.Month);
                querys.Add(conditions);
                SqlDataReader querySdr = null;

                cmd.CommandText = "select vendor,product,mb_brief,mpn," +
                    "mb_descripe,vendor_pch_mpn,pcb_brief_describe,pcb_describe,vendor_vga_mpn,vga_brief_describe,vga_describe,vendor_cpu_mpn,cpu_brief," +
                    "cpu_describe,dpk_type,warranty_period,custom_machine_type,eol" +
                    " from MBMaterialCompare";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MBBgaMaterialStruct temp = new MBBgaMaterialStruct();
                    temp.vendor = querySdr[0].ToString();
                    temp.product = querySdr[1].ToString();

                    temp.mb_brief = querySdr[2].ToString();
                    temp.mpn = querySdr[3].ToString();
                    temp.mb_descripe = querySdr[4].ToString();
                    temp.vendor_pch_mpn = querySdr[5].ToString();
                    temp.pcb_brief_describe = querySdr[6].ToString();
                    temp.pcb_describe = querySdr[7].ToString();
                    temp.vendor_vga_mpn = querySdr[8].ToString();
                    temp.vga_brief_describe = querySdr[9].ToString();
                    temp.vga_describe = querySdr[10].ToString();
                    temp.vendor_cpu_mpn = querySdr[11].ToString();
                    temp.cpu_brief = querySdr[12].ToString();
                    temp.cpu_describe = querySdr[13].ToString();
                    temp.dpk_type = querySdr[14].ToString();
                    temp.warranty_period = querySdr[15].ToString();
                    temp.custom_machine_type = querySdr[16].ToString();
                    temp.eol = querySdr[17].ToString();
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (queryCondition querystemp in querys)
                {
                    List<materialNo_num> materialNos = new List<materialNo_num>();
                    cmd.CommandText = "SELECT custommaterialNo,count(*) FROM DeliveredTable where order_receive_date between '" +
                        querystemp.monthstart + "' and '" + querystemp.monthend + "' group by custommaterialNo";

                    querySdr = cmd.ExecuteReader();
                    while(querySdr.Read())
                    {
                        materialNo_num materialNo_numTemp = new materialNo_num();
                        materialNo_numTemp.materialNo = querySdr[0].ToString();
                        materialNo_numTemp.num = Int16.Parse(querySdr[1].ToString());
                        materialNos.Add(materialNo_numTemp);
                    }
                    querySdr.Close();

                    //去查找之前的历史的信息了
                    foreach (materialNo_num materialNo_numtemp in materialNos)
                    {
                        cmd.CommandText = "SELECT sum_1 ,sum_2,sum_3,sum_4,sum_5,sum_6,sum_7,sum_8,sum_9,sum_10,sum_11,sum_12  FROM repaire_history_data_sheet where year_ ='"
                            + querystemp.year + "' and custom_material_no='" + materialNo_numtemp.materialNo + "'";

                        querySdr = cmd.ExecuteReader();
                        if (querySdr.Read())
                        {
                            //存在的处理方式,处理累积的字段sum_year
                            //找到当前是几月，然后把对应字段替换掉，然后才算总数
                            querySdr.Close();

                            //update 表
                            cmd.CommandText = "update repaire_history_data_sheet set " + querystemp.dbcolumn + "=" + materialNo_numtemp.num + " where year_ ='"
                                + querystemp.year + "' and custom_material_no='" + materialNo_numtemp.materialNo + "'";
                            cmd.ExecuteNonQuery();

                            //然后重新查询一遍
                            cmd.CommandText = "SELECT sum_1 ,sum_2,sum_3,sum_4,sum_5,sum_6,sum_7,sum_8,sum_9,sum_10,sum_11,sum_12  FROM repaire_history_data_sheet where year_ ='"
                            + querystemp.year + "' and custom_material_no='" + materialNo_numtemp.materialNo + "'";

                            int sumall = 0;
                            querySdr = cmd.ExecuteReader();
                            if (querySdr.Read())
                            {
                                sumall +=
                                    Int16.Parse(querySdr[0].ToString()) +
                                    Int16.Parse(querySdr[1].ToString()) +
                                    Int16.Parse(querySdr[2].ToString()) +
                                    Int16.Parse(querySdr[3].ToString()) +
                                    Int16.Parse(querySdr[4].ToString()) +
                                    Int16.Parse(querySdr[5].ToString()) +
                                    Int16.Parse(querySdr[6].ToString()) +
                                    Int16.Parse(querySdr[7].ToString()) +
                                    Int16.Parse(querySdr[8].ToString()) +
                                    Int16.Parse(querySdr[9].ToString()) +
                                    Int16.Parse(querySdr[10].ToString()) +
                                    Int16.Parse(querySdr[11].ToString());
                            }
                            querySdr.Close();

                            //把汇总的数据更新一遍
                            cmd.CommandText = "update repaire_history_data_sheet set sum_year=" + sumall + " where year_ ='"
                                + querystemp.year + "' and custom_material_no='" + querystemp.materialno + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            querySdr.Close();
                            //不存在的处理方式,处理累积的字段sum_year
                            //insert
                            int sumall = materialNo_numtemp.num;
                            //还要确定当前是几月份，位置不同 12个sql语句？？

                            string currentsqlpre = "insert into repaire_history_data_sheet values('" + querystemp.year + "','','','','" + materialNo_numtemp.materialNo + "','','','" + sumall + "',";
                            //下面根据月份组装
                            switch (querystemp.dbcolumn)
                            {
                                case "sum_1":
                                    currentsqlpre += "'" + materialNo_numtemp.num + "','0','0','0','0','0','0','0','0','0','0','0')";
                                    break;
                                case "sum_2":
                                    currentsqlpre += "'0','" + materialNo_numtemp.num + "','0','0','0','0','0','0','0','0','0','0')";
                                    break;
                                case "sum_3":
                                    currentsqlpre += "'0','0','" + materialNo_numtemp.num + "','0','0','0','0','0','0','0','0','0')";
                                    break;
                                case "sum_4":
                                    currentsqlpre += "'0','0','0','" + materialNo_numtemp.num + "','0','0','0','0','0','0','0','0')";
                                    break;
                                case "sum_5":
                                    currentsqlpre += "'0','0','0','0','" + materialNo_numtemp.num + "','0','0','0','0','0','0','0')";
                                    break;
                                case "sum_6":
                                    currentsqlpre += "'0','0','0','0','0','" + materialNo_numtemp.num + "','0','0','0','0','0','0')";
                                    break;
                                case "sum_7":
                                    currentsqlpre += "'0','0','0','0','0','0','" + materialNo_numtemp.num + "','0','0','0','0','0')";
                                    break;
                                case "sum_8":
                                    currentsqlpre += "'0','0','0','0','0','0','0','" + materialNo_numtemp.num + "','0','0','0','0')";
                                    break;
                                case "sum_9":
                                    currentsqlpre += "'0','0','0','0','0','0','0','0','" + materialNo_numtemp.num + "','0','0','0')";
                                    break;
                                case "sum_10":
                                    currentsqlpre += "'0','0','0','0','0','0','0','0','0','" + materialNo_numtemp.num + "','0','0')";
                                    break;
                                case "sum_11":
                                    currentsqlpre += "'0','0','0','0','0','0','0','0','0','0','" + materialNo_numtemp.num + "','0')";
                                    break;
                                case "sum_12":
                                    currentsqlpre += "'0','0','0','0','0','0','0','0','0','0','0','" + materialNo_numtemp.num + "')";
                                    break;
                            }

                            cmd.CommandText = currentsqlpre;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }                
                
                foreach (MBBgaMaterialStruct temp in receiveOrderList)
                {
                    //计算整机出货量
                    cmd.CommandText = "select * from (" +
                        "select  mpn, sum(cast(out_number as float)) as out_number from whole_import_sheet group by mpn" +
                        ") as A where mpn='" + temp.mpn + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.whole_out_num = querySdr[1].ToString();
                    }
                    querySdr.Close();
              
                    //查询历史维修量
                    cmd.CommandText = "select * from (" +
                        "select  mpn, sum(cast(sum_year as float)) as out_number from repaire_history_data_sheet group by mpn" +
                        ") as A where mpn='" + temp.mpn + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.repaire_num = querySdr[1].ToString();
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

        public void generateExcelToCheck(List<MBBgaMaterialStruct> StockCheckList, string startTime, string endTime)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent firstsheet = new allContent();
            firstsheet.sheetName = "MBBga材料一览表";
            firstsheet.titleList = new List<string>();
            firstsheet.contentList = new List<object>();

            firstsheet.titleList.Add("厂商");
            firstsheet.titleList.Add("客户别");
            firstsheet.titleList.Add("MB简称");
            firstsheet.titleList.Add("MPN");
            firstsheet.titleList.Add("MB描述"); ;
            firstsheet.titleList.Add("厂商PCH_MPN");
            firstsheet.titleList.Add("PCH简述");
            firstsheet.titleList.Add("PCH描述");
            firstsheet.titleList.Add("厂商VGA_MPN");
            firstsheet.titleList.Add("VGA简述");
            firstsheet.titleList.Add("VGA描述"); 
            firstsheet.titleList.Add("厂商CPU_MPN");
            firstsheet.titleList.Add("CPU简述");
            firstsheet.titleList.Add("CPU描述");
            firstsheet.titleList.Add("DPK类型");
            firstsheet.titleList.Add("保修期");
            firstsheet.titleList.Add("客户机型");

            firstsheet.titleList.Add("整机出货量");
            firstsheet.titleList.Add("客户退修量");

             firstsheet.titleList.Add("MB购买数量");
             firstsheet.titleList.Add("MB报废数量");
             firstsheet.titleList.Add("MB报废率");


            firstsheet.titleList.Add("材料类别");

            foreach (MBBgaMaterialStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);

                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_descripe);
                ct1.Add(stockcheck.vendor_pch_mpn);
                ct1.Add(stockcheck.pcb_brief_describe);
                ct1.Add(stockcheck.pcb_describe);
                ct1.Add(stockcheck.vendor_vga_mpn);
                ct1.Add(stockcheck.vga_brief_describe);
                ct1.Add(stockcheck.vga_describe);
                ct1.Add(stockcheck.vendor_cpu_mpn);
                ct1.Add(stockcheck.cpu_brief);
                ct1.Add(stockcheck.cpu_describe);
                ct1.Add(stockcheck.dpk_type);
                ct1.Add(stockcheck.warranty_period);
                ct1.Add(stockcheck.custom_machine_type);
                ct1.Add(stockcheck.whole_out_num);
                ct1.Add(stockcheck.repaire_num);

                ct1.Add(stockcheck.mb_buy_in_num);
                ct1.Add(stockcheck.mb_fault_num);
                ct1.Add(stockcheck.mb_fault_rate);

                ct1.Add(stockcheck.eol);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }
            allcontentList.Add(firstsheet);

            Utils.createMulitSheetsUsingNPOI("D:\\MBBga材料一览表" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);            
        }
    }

    public class materialNo_num
    {
        public string materialNo;
        public int num;
    }

    public class queryCondition
    {
        public string year;
        public string materialno;
        public string currentMonth;
        public string monthstart;
        public string monthend;
        public string dbcolumn;//月份对应的数据库列
    }

   public class MBBgaMaterialStruct
    {
        public string vendor;
        public string product;

        public string mb_brief;
        public string mpn;
        public string mb_descripe;
       
        public string vendor_pch_mpn;
        public string pcb_brief_describe;
        public string pcb_describe;
        public string vendor_vga_mpn;
        public string vga_brief_describe;
        public string vga_describe;
        public string vendor_cpu_mpn;
        public string cpu_brief;
        public string cpu_describe;

        public string dpk_type;
        public string warranty_period;
        public string custom_machine_type;
        public string whole_out_num;//整机出货量
        public string repaire_num;//退修数量

        public string mb_buy_in_num;//购买数量
        public string mb_fault_num;//报废数量
        public string mb_fault_rate;//MB报废率,报废数量除以退修数量

        public string eol;
    }
}

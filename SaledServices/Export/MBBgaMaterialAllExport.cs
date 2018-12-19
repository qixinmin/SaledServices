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
        string startTime, endTime;

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<MBBgaMaterialStruct> receiveOrderList = new List<MBBgaMaterialStruct>();
            List<bgaReport> bgas = new List<bgaReport>();
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
                DateTime currentMonth = DateTime.Now;
                conditions.year = currentMonth.Year + "年";
                conditions.currentMonth = currentMonth.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(currentMonth);
                conditions.monthend = Utils.getEndDayOfMonth(currentMonth);
                conditions.dbcolumn = Utils.getColumnName(currentMonth.Month);
                querys.Add(conditions);

                //上个月
                conditions = new queryCondition();
                DateTime lastMonth = DateTime.Now.AddMonths(-1);
                conditions.year = lastMonth.Year + "年";
                conditions.currentMonth = lastMonth.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(lastMonth);
                conditions.monthend = Utils.getEndDayOfMonth(lastMonth);
                conditions.dbcolumn = Utils.getColumnName(lastMonth.Month);
                querys.Add(conditions);

                //上上月
                conditions = new queryCondition();
                DateTime lastLastmonth = DateTime.Now.AddMonths(-2);
                conditions.year = lastLastmonth.Year + "年";
                conditions.currentMonth = lastLastmonth.Month + "";
                conditions.monthstart = Utils.getFirstDayOfMonth(lastLastmonth);
                conditions.monthend = Utils.getEndDayOfMonth(lastLastmonth);
                conditions.dbcolumn = Utils.getColumnName(lastLastmonth.Month);
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
                                + querystemp.year + "' and custom_material_no='" + materialNo_numtemp.materialNo + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            querySdr.Close();
                            //不存在的处理方式,处理累积的字段sum_year
                            //insert
                            int sumall = materialNo_numtemp.num;
                            //还要确定当前是几月份，位置不同 12个sql语句？？

                            cmd.CommandText = "SELECT vendor,product,storehouse,mpn,mb_brief from DeliveredTable where custommaterialNo='" + materialNo_numtemp.materialNo + "'";
                            querySdr = cmd.ExecuteReader();
                            if (querySdr.Read())
                            {
                                materialNo_numtemp.vendor = querySdr[0].ToString();
                                materialNo_numtemp.product = querySdr[1].ToString();
                                materialNo_numtemp.house = querySdr[2].ToString();
                                materialNo_numtemp.mpn = querySdr[3].ToString();
                                materialNo_numtemp.mb_brief = querySdr[4].ToString();
                            }
                            querySdr.Close();

                            string currentsqlpre = "insert into repaire_history_data_sheet values('" + querystemp.year + "','"+materialNo_numtemp.vendor+"','"+materialNo_numtemp.house+"','"+materialNo_numtemp.product+"','" 
                                + materialNo_numtemp.materialNo + "','"+materialNo_numtemp.mpn+"','"+materialNo_numtemp.mb_brief+"','" + sumall + "',";
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

                    //查询购买数量
                    cmd.CommandText = "select * from (" +
                        "select  mpn, sum(cast(input_number as float)) as out_number from mb_in_stock group by mpn" +
                        ") as A where mpn='" + temp.mpn + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.mb_buy_in_num = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    //查询报废数量
                    cmd.CommandText = "select * from (" +
                        "select  mpn, count(*) as out_number from fault_mb_enter_record_table group by mpn" +
                        ") as A where mpn='" + temp.mpn + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.mb_fault_num = querySdr[1].ToString();
                    }
                    querySdr.Close();
                }

                Dictionary<string, MBBgaMaterialStruct> mpn_all = new Dictionary<string, MBBgaMaterialStruct>();//用来第二张表查询信息
                //计算MB报废率，报废/退修量
                foreach (MBBgaMaterialStruct temp in receiveOrderList)
                {
                    int mb_fault_num = (temp.mb_fault_num == "" || temp.mb_fault_num == null) ? 0 : Int16.Parse(temp.mb_fault_num);
                    float repaire_num = (temp.repaire_num == "" || temp.repaire_num == null) ? 0 : float.Parse(temp.repaire_num);
                    if (repaire_num != 0)
                    {
                        temp.mb_fault_rate = mb_fault_num / repaire_num +"";
                    }

                    if (mpn_all.ContainsKey(temp.mpn) == false)
                    {
                        mpn_all.Add(temp.mpn, temp);
                    }
                }

                //下面是第二张表的内容
                cmd.CommandText = "select  mpn, bga_brief , count(*) as out_number from bga_repair_record_table where bga_repair_result= 'BGA待换' group by mpn, bga_brief";

                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    bgaReport temp = new bgaReport();
                    temp.mpn  = querySdr[0].ToString();
                    temp.bga_brief  = querySdr[1].ToString();
                    temp.bga_change_number = querySdr[2].ToString();

                    if (mpn_all.ContainsKey(temp.mpn))
                    {
                        MBBgaMaterialStruct temp2 = mpn_all[temp.mpn];
                        temp.vendor = temp2.vendor;
                        temp.product = temp2.product;
                        temp.mb_brief = temp2.mb_brief;
                        temp.whole_out_num = temp2.whole_out_num;
                        temp.repaire_num = temp2.repaire_num;
                        temp.material_type = temp2.eol;
                    }

                    int bga_change_number = (temp.bga_change_number == "" || temp.bga_change_number == null) ? 0 : Int16.Parse(temp.bga_change_number);
                    float whole_out_num = (temp.whole_out_num == "" || temp.whole_out_num == null) ? 0 : float.Parse(temp.whole_out_num);
                    if (whole_out_num != 0)
                    {
                        temp.bga_rate = (bga_change_number / whole_out_num) + "";
                    }
                  
                    bgas.Add(temp);
                }
                querySdr.Close();
                
                foreach (bgaReport temp in bgas)
                {
                    //查询历史维修量
                    cmd.CommandText = "select top 1 bgapn,bgatype from bga_repair_record_table where mpn='"+temp.mpn+"' and bga_brief='"+temp.bga_brief+"'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.bgapn = querySdr[0].ToString();
                        temp.bga_type = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    string queryCol = "",queryCol2="";
                    switch (temp.bga_type)
                    {
                        case "PCH":
                            queryCol = "pcb_describe";
                            queryCol2 = "pcb_brief_describe";
                            break;
                        case "CPU":
                            queryCol = "cpu_describe";
                            queryCol2 = "cpu_brief";
                            break;
                        case "VGA":
                            queryCol = "vga_describe";
                            queryCol2 = "vga_brief_describe";
                            break;
                    }

                    cmd.CommandText = "select top 1 "+queryCol +" from MBMaterialCompare where "+queryCol2+"='" + temp.bga_brief + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.bga_desc = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select " + Utils.getColumnName(lastMonth.Month) + "," + Utils.getColumnName(lastLastmonth.Month) + " from repaire_history_data_sheet where mpn='"+temp.mpn+"'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.Read())
                    {
                        temp.repair_num_1_month = querySdr[0].ToString();
                        temp.repair_num_2_month = Int16.Parse(querySdr[0].ToString()) + Int16.Parse(querySdr[1].ToString()) +"";
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, bgas, startTime, endTime);
        }

        public void generateExcelToCheck(List<MBBgaMaterialStruct> StockCheckList, List<bgaReport> bgas,string startTime, string endTime)
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

            allContent secondsheet = new allContent();
            secondsheet.sheetName = "BGAMB一览表";
            secondsheet.titleList = new List<string>();
            secondsheet.contentList = new List<object>();

            secondsheet.titleList.Add("BGA_厂商MPN");
            secondsheet.titleList.Add("BGA简述");
            secondsheet.titleList.Add("BGA描述");
            secondsheet.titleList.Add("BGA类别");

            secondsheet.titleList.Add("厂商");
            secondsheet.titleList.Add("客户别");
            secondsheet.titleList.Add("MB简称");
            secondsheet.titleList.Add("mpn");
            secondsheet.titleList.Add("对应整机出货量");
            secondsheet.titleList.Add("客户退修量");
            secondsheet.titleList.Add("近1个月MB退修量");
            secondsheet.titleList.Add("近2个月MB退修量");
            secondsheet.titleList.Add("BGA更换数量");
            secondsheet.titleList.Add("BGA不良率");
            secondsheet.titleList.Add("材料类别");


            foreach (bgaReport stockcheck in bgas)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.bgapn);
                ct1.Add(stockcheck.bga_brief);
                ct1.Add(stockcheck.bga_desc);
                ct1.Add(stockcheck.bga_type);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.whole_out_num);
                ct1.Add(stockcheck.repaire_num);
                ct1.Add(stockcheck.repair_num_1_month);
                ct1.Add(stockcheck.repair_num_2_month);
                ct1.Add(stockcheck.bga_change_number);
                ct1.Add(stockcheck.bga_rate);
                ct1.Add(stockcheck.material_type);

                ctest1.contentArray = ct1;
                secondsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(secondsheet);

            Utils.createMulitSheetsUsingNPOI("D:\\MBBga材料一览表" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<repaire_history_data> receiveOrderList = new List<repaire_history_data>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT year_,vendor,house,product,custom_material_no,mpn,mb_brief,sum_year, sum_1 ,sum_2,sum_3,sum_4,sum_5,sum_6,sum_7,sum_8,sum_9,sum_10,sum_11,sum_12  FROM repaire_history_data_sheet order by Id desc";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    repaire_history_data temp = new repaire_history_data();
                    temp.year_ = querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();

                    temp.house = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.custom_material_no = querySdr[4].ToString();
                    temp.mpn = querySdr[5].ToString();
                    temp.mb_brief = querySdr[6].ToString();
                    temp.sum_year = querySdr[7].ToString();
                    temp.sum_1 = querySdr[8].ToString();
                    temp.sum_2 = querySdr[9].ToString();
                    temp.sum_3 = querySdr[10].ToString();
                    temp.sum_4 = querySdr[11].ToString();
                    temp.sum_5 = querySdr[12].ToString();
                    temp.sum_6 = querySdr[13].ToString();
                    temp.sum_7 = querySdr[14].ToString();
                    temp.sum_8 = querySdr[15].ToString();
                    temp.sum_9 = querySdr[16].ToString();
                    temp.sum_10 = querySdr[17].ToString();
                    temp.sum_11= querySdr[18].ToString();
                    temp.sum_12 = querySdr[19].ToString();
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck2(receiveOrderList,startTime, endTime);
        }

        public void generateExcelToCheck2(List<repaire_history_data> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
           
            titleList.Add("年");
            titleList.Add("厂商");
            titleList.Add("house");
            titleList.Add("product");
            titleList.Add("custom_material_no");
            titleList.Add("MPN");
            titleList.Add("mb_brief");
            titleList.Add("sum_year");
            titleList.Add("sum_1");
            titleList.Add("sum_2");
            titleList.Add("sum_3");
            titleList.Add("sum_4");
            titleList.Add("sum_5");
            titleList.Add("sum_6");
            titleList.Add("sum_7");
            titleList.Add("sum_8");
            titleList.Add("sum_9");
            titleList.Add("sum_10");
            titleList.Add("sum_11");
            titleList.Add("sum_12");

            foreach (repaire_history_data stockcheck in StockCheckList)
            { //year_,vendor,house,product,custom_material_no,mpn,mb_brief,sum_year, sum_1 ,sum_2,sum_3,sum_4,sum_5,sum_6,sum_7,sum_8,sum_9,sum_10,sum_11,sum_12
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.year_);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.house);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.custom_material_no);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.sum_year);
                ct1.Add(stockcheck.sum_1);
                ct1.Add(stockcheck.sum_2);
                ct1.Add(stockcheck.sum_3);
                ct1.Add(stockcheck.sum_4);
                ct1.Add(stockcheck.sum_5);
                ct1.Add(stockcheck.sum_6);
                ct1.Add(stockcheck.sum_7);
                ct1.Add(stockcheck.sum_8);
                ct1.Add(stockcheck.sum_9);
                ct1.Add(stockcheck.sum_10);
                ct1.Add(stockcheck.sum_11);
                ct1.Add(stockcheck.sum_12);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\维修历史数据" + DateTime.Now.ToString
                ("yyyy-MM-dd").Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class repaire_history_data
    {
        public string  year_;
        public string vendor;
        public string house;
        public string product;
        public string custom_material_no;
        public string mpn;
        public string mb_brief;
        public string sum_year;
        public string  sum_1;
        public string sum_2;
        public string sum_3;
        public string sum_4;
        public string sum_5;
        public string sum_6;
        public string sum_7;
        public string sum_8;
        public string sum_9;
        public string sum_10;
        public string sum_11;
        public string sum_12; 
    }

    public class bgaReport
    {
        //BGA_厂商MPN	BGA简述	BGA描述	BGA类别	厂商	客户别	MB简称	mpn	对应整机出货量	客户退修量	近1个月MB退修量	近2个月MB退修量	BGA更换数量	BGA不良率	材料类别
        public string bgapn;
        public string bga_brief;
        public string bga_desc;
        public string bga_type;
        public string vendor;
        public string product;
        public string mb_brief;
        public string mpn;
        public string whole_out_num;//整机出货量
        public string repaire_num;//退修数量
        public string repair_num_1_month;
        public string repair_num_2_month;
        public string bga_change_number;
        public string bga_rate;
        public string material_type;
    }

    public class materialNo_num
    {
        public string materialNo;
        public int num;

        public string vendor;
        public string house;
        public string product;
        public string mpn;
        public string mb_brief;
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

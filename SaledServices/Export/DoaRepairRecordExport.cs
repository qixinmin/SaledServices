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
    public partial class DoaRepairRecordExport : Form
    {
        public DoaRepairRecordExport()
        {
            InitializeComponent();
        }      

        private void test_exportxmlbutton_Click(object sender, EventArgs e)
        {
            exportxmlbutton_Click(sender, e);
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }          

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd");

            List<DoaRecordStruct> doaListtarget = new List<DoaRecordStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string sql = "";
                SqlDataReader querySdr = null;

                sql = "SELECT custom_order,track_serial_no,order_receive_date,custom_machine_type,mb_brief,custommaterialNo,custom_serial_no,custom_fault"+
                    ",lenovo_repair_no,whole_machine_no " +
                " from DeliveredTable where source_brief='RMA_DOA' and order_receive_date between '" + startTime + "' and '" + endTime + "' ";
                    
                cmd.CommandText = sql;
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    DoaRecordStruct temp = new DoaRecordStruct();
                    temp.rma = querySdr[0].ToString().ToUpper();
                    temp.trackno = querySdr[1].ToString();
                    temp.receivedate = querySdr[2].ToString();
                    temp.lenovomodel = querySdr[3].ToString();
                    temp.lcfcmodel = querySdr[4].ToString();
                    temp.frupn = querySdr[5].ToString();
                    temp.sn = querySdr[6].ToString();
                    temp.customersymptom = querySdr[7].ToString();
                    temp.recheckproblem = querySdr[8].ToString();
                    temp.recheckstatus = querySdr[9].ToString();
                    doaListtarget.Add(temp);
                }
                querySdr.Close();



                foreach (DoaRecordStruct doaRecord in doaListtarget)
                {
                    DoaRecordStruct temp = doaRecord;

                    sql = "SELECT top 1 custom_fault" +
                    ",track_serial_no " +
                " from DeliveredTable where source_brief='RMA_DOA' and custom_serial_no='"+doaRecord.sn+"'and order_receive_date < '" + startTime + "' order by id desc ";

                    cmd.CommandText = sql;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        doaRecord.lastfault = querySdr[0].ToString();
                        doaRecord.lasttrackno = querySdr[1].ToString();

                    }
                    querySdr.Close();

                    sql = "SELECT top 1 response_check,analysis_step, improve_action " +
                         " from doa_analysis where  _8s='" + doaRecord.sn+ "' order by id desc ";

                    cmd.CommandText = sql;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        doaRecord.rrcategory = querySdr[0].ToString();
                        doaRecord.rootcause = querySdr[1].ToString();
                        doaRecord.recoveryaction = querySdr[2].ToString();

                    }
                    querySdr.Close();

                    sql = "SELECT top 1 fault_describe,not_good_place " +
                        " from repair_record_table where  track_serial_no='" + doaRecord.trackno + "' order by id desc ";

                    cmd.CommandText = sql;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        doaRecord.repairsymptom = querySdr[0].ToString();
                        doaRecord.repairlocation = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    sql = "SELECT top 1 fault_describe,not_good_place " +
                       " from repair_record_table where  track_serial_no='" + doaRecord.lasttrackno + "' order by id desc ";

                    cmd.CommandText = sql;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        doaRecord.lastrepairsymptom = querySdr[0].ToString();
                        doaRecord.lastrepairlocation = querySdr[1].ToString();
                        doaRecord.lastrepairplace = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    if (doaRecord.lasttrackno == "")
                    {
                        doaRecord.rmaorfru = "FRU";
                    }
                    else
                    {
                        doaRecord.rmaorfru = "RMA";
                    }

                    sql = "SELECT top 1 return_date " +
                     " from returnStore where  track_serial_no='" + doaRecord.lasttrackno + "' order by id desc ";

                    cmd.CommandText = sql;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        doaRecord.lastshipdate = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    doaRecord.week = "WK"+Utils.GetWeekOfYear(DateTime.Parse(doaRecord.receivedate));
                }                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(doaListtarget, startTime, endTime);
        }

        public void generateExcelToCheck(List<DoaRecordStruct> repairRecordList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("Repair Center");
            titleList.Add("Week");
            titleList.Add("SP");
            titleList.Add("RMA#");
            titleList.Add("跟踪条码");
            titleList.Add("Receive Date");
            titleList.Add("Lenovo Model");
            titleList.Add("LCFC model");
            titleList.Add("FRU PN#");
            titleList.Add("SN");
            titleList.Add("Customer Symptom");
            titleList.Add("上次故障问题");

            titleList.Add("Repair Symptom");
            titleList.Add("Customer Symptom & Repair Symptom Match or Not(Yes/No)");
            titleList.Add("驻厂复判问题");

            titleList.Add("驻厂复判状态");
            titleList.Add("Repair Location");
            titleList.Add("上次维修换件");
            titleList.Add("RMA or FRU");
            titleList.Add("RR Category(VFF/NTF/CID Repairable/Quality issue/CID Irreparable)");
            titleList.Add("Root Cause");
            titleList.Add("Recovery Action");

            titleList.Add("上次跟踪条码");
            titleList.Add("Last Shipped Date");
            titleList.Add("Last Customer Symptom");
            titleList.Add("Last Repair Symptom");
            titleList.Add("Last Repair Location ");
            titleList.Add("CID Image");
            titleList.Add("Last time OQC Photo 1");
            titleList.Add("Last time OQC Photo 2");
            titleList.Add("Last Time Test Pass Log");

            foreach (DoaRecordStruct repaircheck in repairRecordList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
        
                ct1.Add(repaircheck.repair_center);
                ct1.Add(repaircheck.week);
                ct1.Add(repaircheck.sp);
                ct1.Add(repaircheck.rma);
                ct1.Add(repaircheck.trackno);
                ct1.Add(Utils.modifyDataFormat(repaircheck.receivedate));
                ct1.Add(repaircheck.lenovomodel);
                ct1.Add(repaircheck.lcfcmodel);
                ct1.Add(repaircheck.frupn);
                ct1.Add(repaircheck.sn);
                ct1.Add(repaircheck.customersymptom);
                ct1.Add(repaircheck.lastfault);
                ct1.Add(repaircheck.repairsymptom);
                ct1.Add(repaircheck.csrsm);
                ct1.Add(repaircheck.recheckproblem);
                ct1.Add(repaircheck.recheckstatus);
                ct1.Add(repaircheck.repairlocation);
                ct1.Add(repaircheck.lastrepairplace);
                ct1.Add(repaircheck.rmaorfru);
                ct1.Add(repaircheck.rrcategory);
                ct1.Add(repaircheck.rootcause);
                ct1.Add(repaircheck.recoveryaction);
                ct1.Add(repaircheck.lasttrackno);
                ct1.Add(Utils.modifyDataFormat(repaircheck.lastshipdate));
                ct1.Add(repaircheck.lastcustomersymptom);
                ct1.Add(repaircheck.lastrepairsymptom);
                ct1.Add(repaircheck.lastrepairlocation);
                ct1.Add(repaircheck.cidimage);
                ct1.Add(repaircheck.lasttimeoqcphoto1);
                ct1.Add(repaircheck.lasttimeoqcphoto2);
                ct1.Add(repaircheck.lasttimetestpasslog);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("DOA报表" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class DoaRecordStruct
    {
        public string repair_center="上海一麦";//Repair Center	
        public string week;//Week
        public string sp="百福"; //SP
        public string rma; //RMA#	
        public string trackno; //跟踪条码	
        public string receivedate;  //Receive Date	
        public string lenovomodel;  //Lenovo Model	
        public string lcfcmodel;  //LCFC model	
        public string frupn;  //FRU PN#	
        public string sn;  //SN	
        public string customersymptom;  //Customer Symptom	
        public string lastfault;  //上次故障问题	
        public string repairsymptom;  //Repair Symptom	
        public string csrsm;  //Customer Symptom & Repair Symptom Match or Not(Yes/No)	
        public string recheckproblem;  //驻厂复判问题	
        public string recheckstatus;  //驻厂复判状态	
        public string repairlocation;  //Repair Location	
        public string lastrepairplace;   //上次维修换件	
        public string rmaorfru;  //RMA or FRU	
        public string rrcategory;  //"RR Category(VFF/NTF/CID Repairable/Quality issue/CID Irreparable)"	
        public string rootcause;  //Root Cause	
        public string recoveryaction;   //Recovery Action	
        public string lasttrackno;   //上次跟踪条码	
        public string lastshipdate;   //Last Shipped Date	
        public string lastcustomersymptom;   //Last Customer Symptom	
        public string lastrepairsymptom;   //Last Repair Symptom	
        public string lastrepairlocation;   //Last Repair Location	
        public string cidimage;   //CID Image	
        public string lasttimeoqcphoto1;   //Last time OQC Photo 1	
        public string lasttimeoqcphoto2;    //Last time OQC Photo 2	
        public string lasttimetestpasslog;   //Last Time Test Pass Log
    }
}

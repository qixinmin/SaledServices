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

                cmd.CommandText = "select vendor,product,mb_brief,mpn," +
                    "mb_descripe,vendor_pch_mpn,pcb_brief_describe,pcb_describe,vendor_vga_mpn,vga_brief_describe,vga_describe,vendor_cpu_mpn,cpu_brief," +
                    "cpu_describe,dpk_type,warranty_period,custom_machine_type,eol" +
                    " from MBMaterialCompare";
                SqlDataReader querySdr = cmd.ExecuteReader();
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
                ct1.Add(stockcheck.eol);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }
            allcontentList.Add(firstsheet);

            Utils.createMulitSheetsUsingNPOI("D:\\MBBga材料一览表" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);            
        }
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
        public string whole_out_num;
        public string repaire_num;

        public string eol;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class OpeningStockForm : Form
    {
        public OpeningStockForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            OpeningStockClass openingstock = new OpeningStockClass();
            List<StoreInit> storeInitList = new List<StoreInit>();

            string seq_no = DateTime.Now.ToString("yyyymmdd") + "2005" + "1";//日期+类型+序号
            string boxtype = "2005";//代码
            string flowstateg = "";
            string trade_code = "";
            string ems_no = "";

            string status = "A";
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select indentifier, book_number from company_fixed_table";
                SqlDataReader querySdr = cmd.ExecuteReader();               
               
                while (querySdr.Read())
                {
                    trade_code = querySdr[0].ToString();
                    ems_no = querySdr[1].ToString();
                }
                querySdr.Close();

                //选择主板信息
                cmd.CommandText = "select custom_order, track_serial_no,order_receive_date from DeliveredTable where order_receive_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd")+"'";
                querySdr = cmd.ExecuteReader();
               
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    init1.cop_g_no = querySdr[1].ToString();
                    init1.qty = "1";
                    init1.unit = "007";
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = querySdr[2].ToString();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();

                //选择库存信息，库存分几种，现在只看frusmt的，其他待做
                cmd.CommandText = "select mpn,stock_in_num,input_date,stock_place from fru_smt_in_stock where isdeclare='是' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd")+"'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    init1.cop_g_no = querySdr[0].ToString();
                    init1.qty = querySdr[1].ToString();
                    init1.unit = "007";//TODO
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = querySdr[2].ToString();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = querySdr[3].ToString();
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            openingstock.seq_no = seq_no;
            openingstock.boxtype = boxtype;
            openingstock.flowstateg = flowstateg;
            openingstock.trade_code = trade_code;
            openingstock.ems_no = ems_no;
            openingstock.status = status;

            openingstock.storeInitList = storeInitList;

            Untils.createOpeningStockXML(openingstock, "D:\\test_openingstock.xml");

            MessageBox.Show("finish");
        }
    }
}

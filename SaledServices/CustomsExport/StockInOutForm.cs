using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class StockInOutForm : Form
    {
        public StockInOutForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            StockInOutClass openingstock = new StockInOutClass();
            List<StoreTrans> storeInitList = new List<StoreTrans>();

            string seq_no = DateTime.Now.ToString("yyyymmdd") + "4001"+"1";//日期+类型,后面需要加入序号信息
            string boxtype = "4001";//代码
            string flowstateg = "";
            string trade_code = "";
            string ems_no = "";

            string status = "A";
            string today = DateTime.Now.ToString("yyyy/MM/dd");
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

                //板子入库信息,过滤条件是今天DateTime.Now.ToString("yyyy/MM/dd")
                cmd.CommandText = "select track_serial_no,order_receive_date ,declare_unit, declare_number, custom_request_number from DeliveredTable inner join receiveOrder on DeliveredTable.custom_order = receiveOrder.orderno where order_receive_date between '" + today + "' and '" + today + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    StoreTrans init1 = new StoreTrans();
                    init1.ems_no = ems_no;
                    init1.io_no = querySdr[0].ToString();
                    init1.goods_nature = "I";
                    init1.io_date = today;
                    init1.cop_g_no =querySdr[0].ToString();
                    init1.qty = "1";
                    init1.unit = querySdr[2].ToString();
                    init1.type = "I0002";//入
                    init1.chk_code = "";
                    init1.entry_id = querySdr[3].ToString();
                    init1.gatejob_no = querySdr[4].ToString();
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";

                    storeInitList.Add(init1);
                }
                querySdr.Close();

                //材料库存信息，库存分几种，现在只看frusmt的，其他待做
                cmd.CommandText = "select fru_smt_in_stock.Id, fru_smt_in_stock.mpn,fru_smt_in_stock.stock_in_num,fru_smt_in_stock.input_date,fru_smt_in_stock.stock_place,declare_unit,declare_number,custom_request_number from fru_smt_in_stock inner join stock_in_sheet on fru_smt_in_stock.buy_order_serial_no = stock_in_sheet.buy_order_serial_no where fru_smt_in_stock.isdeclare='是' and fru_smt_in_stock.input_date='" + today + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    StoreTrans init1 = new StoreTrans();
                    init1.ems_no = ems_no;
                    init1.io_no = querySdr[0].ToString();
                    init1.goods_nature = "I";
                    init1.io_date = today;
                    init1.cop_g_no = querySdr[1].ToString();
                    init1.qty = querySdr[2].ToString();
                    init1.unit = querySdr[5].ToString();
                    init1.type = "I0002";//入
                    init1.chk_code = "";
                    init1.entry_id = querySdr[6].ToString();
                    init1.gatejob_no = querySdr[7].ToString();
                    init1.whs_code = "";
                    init1.location_code = querySdr[4].ToString();
                    init1.note = "";

                    storeInitList.Add(init1);                    
                }
                querySdr.Close();

                //报关出库的板子
                cmd.CommandText = "select track_serial_no,return_date ,declare_unit, declare_number, custom_request_number from returnStore inner join receiveOrder on receiveOrder.orderno = returnStore.orderno where return_date='" + today + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    StoreTrans init1 = new StoreTrans();
                    init1.ems_no = ems_no;
                    init1.io_no = querySdr[0].ToString();
                    init1.goods_nature = "I";
                    init1.io_date = today;
                    init1.cop_g_no = querySdr[0].ToString();
                    init1.qty = "1";
                    init1.unit = querySdr[2].ToString();
                    init1.type = "E0002";//出
                    init1.chk_code = "";
                    init1.entry_id = querySdr[3].ToString();
                    init1.gatejob_no = querySdr[4].ToString();
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";

                    storeInitList.Add(init1);
                }
                querySdr.Close();

                //内部领料记录
                //良品到不良品库记录，如何从良品到不良品
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

            openingstock.storeTransList = storeInitList;
            Untils.createStockInOutXML(openingstock, "D:\\test_stockinout.xml");
            MessageBox.Show("finish");
        }
    }
}

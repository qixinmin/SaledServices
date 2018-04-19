using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;

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
            openingstock.seq_no = "seq_no";
            openingstock.boxtype = "boxtype";
            openingstock.flowstateg = "flowstateg";
            openingstock.trade_code = "trade_code";
            openingstock.ems_no = "emo_no";
            openingstock.status = "status";

            List<StoreTrans> storeInitList = new List<StoreTrans>();

            StoreTrans init1 = new StoreTrans();
            init1.ems_no = "emo_no1";
            init1.io_no = "io_no";
            init1.goods_nature = "good_nature";
            init1.io_date = "io_date";
            init1.cop_g_no = "cop_g_no1";
            init1.qty = "qty1";
            init1.unit = "unit1";
            init1.type = "type";
            init1.chk_code = "chk_code";
            init1.entry_id = "entry_id";
            init1.gatejob_no = "gatejob_no";
            init1.whs_code = "whs_code1";
            init1.location_code = "location_code1";
            init1.note = "note1";
            storeInitList.Add(init1);

            StoreTrans init2 = new StoreTrans();
            init2.ems_no = "emo_no1";
            init2.io_no = "io_no";
            init2.goods_nature = "good_nature";
            init2.io_date = "io_date";
            init2.cop_g_no = "cop_g_no1";
            init2.qty = "qty1";
            init2.unit = "unit1";
            init2.type = "type";
            init2.chk_code = "chk_code";
            init2.entry_id = "entry_id";
            init2.gatejob_no = "gatejob_no";
            init2.whs_code = "whs_code1";
            init2.location_code = "location_code1";
            init2.note = "note1";
            storeInitList.Add(init2);

            openingstock.storeTransList = storeInitList;
            Untils.createStockInOutXML(openingstock, "D:\\test_stockinout.xml");
            MessageBox.Show("finish");
        }
    }
}

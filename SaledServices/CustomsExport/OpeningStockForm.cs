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
            openingstock.seq_no = "seq_no";
            openingstock.boxtype = "boxtype";
            openingstock.flowstateg = "flowstateg";
            openingstock.trade_code = "trade_code";
            openingstock.ems_no = "emo_no";
            openingstock.status = "status";


            List<StoreInit> storeInitList = new List<StoreInit>();

            StoreInit init1 = new StoreInit();
            init1.ems_no = "emo_no1";
            init1.cop_g_no = "cop_g_no1";
            init1.qty = "qty1";
            init1.unit = "unit1";
            init1.goods_nature = "goods_nature1";
            init1.bom_version = "bom_version1";
            init1.check_date = "check_date1";
            init1.date_type = "date_type1";
            init1.whs_code = "whs_code1";
            init1.location_code = "location_code1";
            init1.note = "note1";
            storeInitList.Add(init1);

            StoreInit init2 = new StoreInit();
            init2.ems_no = "emo_no2";
            init2.cop_g_no = "cop_g_no2";
            init2.qty = "qty2";
            init2.unit = "unit2";
            init2.goods_nature = "goods_nature2";
            init2.bom_version = "bom_version2";
            init2.check_date = "check_date2";
            init2.date_type = "date_type2";
            init2.whs_code = "whs_code2";
            init2.location_code = "location_code2";
            init2.note = "note2";
            storeInitList.Add(init2);

            openingstock.storeInitList = storeInitList;

            Untils.createOpeningStockXML(openingstock, "D:\\test_openingstock.xml");

            MessageBox.Show("finish");
        }
    }
}

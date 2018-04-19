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
    public partial class WorkListHeadForm : Form
    {
        public WorkListHeadForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            WorkListHeadClass openingstock = new WorkListHeadClass();
            openingstock.seq_no = "seq_no";
            openingstock.boxtype = "boxtype";
            openingstock.flowstateg = "flowstateg";
            openingstock.trade_code = "trade_code";
            openingstock.ems_no = "emo_no";
            openingstock.status = "status";


            List<WorkOrderHead> storeInitList = new List<WorkOrderHead>();

            WorkOrderHead init1 = new WorkOrderHead();
            init1.wo_no = "wo_no";
            init1.wo_date = "wo_date";
            init1.goods_nature = "good_nature";
            init1.cop_g_no = "cop_g_no";
            init1.qty = "qty";
            init1.unit = "unit";
            init1.emo_no = "emo_no";
            storeInitList.Add(init1);

            WorkOrderHead init2 = new WorkOrderHead();
            init2.wo_no = "wo_no";
            init2.wo_date = "wo_date";
            init2.goods_nature = "good_nature";
            init2.cop_g_no = "cop_g_no";
            init2.qty = "qty";
            init2.unit = "unit";
            init2.emo_no = "emo_no";
            storeInitList.Add(init2);

            openingstock.workOrderHeadList = storeInitList;

            Untils.createWorkListHeadXML(openingstock, "D:\\test_worklisthead.xml");

            MessageBox.Show("finish");
        }
    }
}

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
    public partial class WorkListBodyForm : Form
    {
        public WorkListBodyForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            WorkListBodyClass openingstock = new WorkListBodyClass();
            openingstock.seq_no = "seq_no";
            openingstock.boxtype = "boxtype";
            openingstock.flowstateg = "flowstateg";
            openingstock.trade_code = "trade_code";
            openingstock.ems_no = "emo_no";
            openingstock.status = "status";


            List<WorkOrderList> storeInitList = new List<WorkOrderList>();

            WorkOrderList init1 = new WorkOrderList();
            init1.wo_no = "wo_no";
            init1.take_date = "take_date";
            init1.goods_nature = "good_nature";
            init1.cop_g_no = "cop_g_no";
            init1.qty = "qty";
            init1.unit = "unit";
            init1.emo_no = "emo_no";
            storeInitList.Add(init1);

            WorkOrderList init2 = new WorkOrderList();
            init2.wo_no = "wo_no";
            init2.take_date = "take_date";
            init2.goods_nature = "good_nature";
            init2.cop_g_no = "cop_g_no";
            init2.qty = "qty";
            init2.unit = "unit";
            init2.emo_no = "emo_no";
            storeInitList.Add(init2);

            openingstock.workOrderList = storeInitList;


            Untils.createWorkListBodyXML(openingstock, "D:\\test_worklistBody.xml");

            MessageBox.Show("finish");
        }
    }
}

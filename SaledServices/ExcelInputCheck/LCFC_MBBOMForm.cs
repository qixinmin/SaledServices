using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class LCFC_MBBOMForm : Form
    {
        private String tableName = "LCFC_MBBOM_table";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public LCFC_MBBOMForm()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.datetextBox.Text.Trim() + "','" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.mb_briefTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.material_mpnTextBox.Text.Trim() + "','" +
                        this.material_box_placeTextBox.Text.Trim() + "','" +
                        this.material_describeTextBox.Text.Trim() + "','" +
                        this.material_numTextBox.Text.Trim() + "','" +
                        this.L1TextBox.Text.Trim() + "','" +
                        this.L2TextBox.Text.Trim() + "','" +
                        this.L8TextBox.Text.Trim() + "','" +
                        this.L4TextBox.Text.Trim() + "','" +
                        this.L5TextBox.Text.Trim() + "','" +
                        this.L6TextBox.Text.Trim() + "','" +
                        this.L7TextBox.Text.Trim() + "','" +
                        this.L8TextBox.Text.Trim() + "')";

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 1000 * from " + tableName;

                if (material_mpnTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where material_mpn like '%" + material_mpnTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and material_mpn like '%" + material_mpnTextBox.Text.Trim() + "%' ";
                    }
                }

                if (productTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product= '" + productTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and product= '" + productTextBox.Text.Trim() + "' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            

            string[] hTxt = {"ID","日期","厂商","客户别","MB简称","	MPN","材料MPN","料盒位置","物料描述","用料数量","L1","L2","L3","L4","L5","L6","L7","L8"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();

            dr["material_describe"] = this.material_describeTextBox.Text.Trim();
            dr["MPN"] = this.mpnTextBox.Text.Trim();
            dr["material_mpn"] = this.material_mpnTextBox.Text.Trim();
            dr["material_box_place"] = this.material_box_placeTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_briefTextBox.Text.Trim();
            dr["material_num"] = this.material_numTextBox.Text.Trim();
            dr["L1"] = this.L1TextBox.Text.Trim();
            dr["L2"] = this.L2TextBox.Text.Trim();
            dr["L3"] = this.L3TextBox.Text.Trim();
            dr["L4"] = this.L4TextBox.Text.Trim();
            dr["L5"] = this.L5TextBox.Text.Trim();
            dr["L6"] = this.L6TextBox.Text.Trim();
            dr["L7"] = this.L7TextBox.Text.Trim();
            dr["L8"] = this.L8TextBox.Text.Trim();
            dr["_date"] = this.datetextBox.Text.Trim();

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.datetextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.material_mpnTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.material_box_placeTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.material_describeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.material_numTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.L1TextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.L2TextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.L3TextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.L4TextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.L5TextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.L6TextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.L7TextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.L8TextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();
            
        }

        private void ReceiveOrderForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
              GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
              SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel2, true, null);
            tableLayoutPanel3.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel3, true, null);            
            
        }

        private void query_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }

        private void material_mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            string querycolumnmb_brief = "", quereyContentmb_brief = "";

            if (this.mb_briefTextBox.Text.Trim() != "")
            {
                querycolumnmb_brief = "mb_brief";
                quereyContentmb_brief = this.mb_briefTextBox.Text.Trim();
            }

            string querycolumnmaterial_mpn = "", quereyContentmaterial_mpn = "";

            if (this.material_mpnTextBox.Text.Trim() != "")
            {
                querycolumnmaterial_mpn = "material_mpn";
                quereyContentmaterial_mpn = this.material_mpnTextBox.Text.Trim();
            }

            string querycolumnmaterial_describe = "", quereyContentmaterial_describe = "";

            if (this.material_describeTextBox.Text.Trim() != "")
            {
                querycolumnmaterial_describe = "material_describe";
                quereyContentmaterial_describe = this.material_describeTextBox.Text.Trim();
            }

            string sqlCmd = "select * from LCFC_MBBOM_table ";

            if (querycolumnmb_brief != "" )
            {
                if (sqlCmd.Contains("where"))
                {
                    sqlCmd += " and " + querycolumnmb_brief + " like '%" + quereyContentmb_brief + "%'";
                }
                else
                {
                    sqlCmd += " where " + querycolumnmb_brief + " like '%" + quereyContentmb_brief + "%'";
                }
            }

            if (querycolumnmaterial_mpn != "")
            {
                if (sqlCmd.Contains("where"))
                {
                    sqlCmd += " and " + querycolumnmaterial_mpn + " like '%" + quereyContentmaterial_mpn + "%'";
                }
                else
                {
                    sqlCmd += " where " + querycolumnmaterial_mpn + " like '%" + quereyContentmaterial_mpn + "%'";
                }
            }

            if (querycolumnmaterial_describe != "")
            {
                if (sqlCmd.Contains("where"))
                {
                    sqlCmd += " and " + querycolumnmaterial_describe + " like '%" + quereyContentmaterial_describe + "%'";
                }
                else
                {
                    sqlCmd += " where " + querycolumnmaterial_describe + " like '%" + quereyContentmaterial_describe + "%'";
                }
            }

            titleList.Add("ID");
            titleList.Add("日期");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MB简称");
            titleList.Add("MPN");
            titleList.Add("材料MPN");
            titleList.Add("料盒位置");
            titleList.Add("物料描述");
            titleList.Add("用料数量");
            titleList.Add("L1");
            titleList.Add("L2");
            titleList.Add("L3");
            titleList.Add("L4");
            titleList.Add("L5");
            titleList.Add("L6");
            titleList.Add("L7");
            titleList.Add("L8");

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlCmd;
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        ExportExcelContent ctest1 = new ExportExcelContent();
                        List<string> ct1 = new List<string>();
                        ct1.Add(querySdr[0].ToString());
                        ct1.Add(querySdr[1].ToString());
                        ct1.Add(querySdr[2].ToString());
                        ct1.Add(querySdr[3].ToString());
                        ct1.Add(querySdr[4].ToString());
                        ct1.Add(querySdr[5].ToString());
                        ct1.Add(querySdr[6].ToString());
                        ct1.Add(querySdr[7].ToString());
                        ct1.Add(querySdr[8].ToString());
                        ct1.Add(querySdr[9].ToString());
                        ct1.Add(querySdr[10].ToString());
                        ct1.Add(querySdr[11].ToString());
                        ct1.Add(querySdr[12].ToString());
                        ct1.Add(querySdr[13].ToString());
                        ct1.Add(querySdr[14].ToString());
                        ct1.Add(querySdr[15].ToString());
                        ct1.Add(querySdr[16].ToString());
                        ct1.Add(querySdr[17].ToString());                        

                        ctest1.contentArray = ct1;
                        contentList.Add(ctest1);
                    }
                    querySdr.Close();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                Utils.createExcel("D:\\LCFC_MBBOM" + DateTime.Now.ToString("yyyy-MM-dd").Replace('/', '-') + ".xlsx", titleList, contentList);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

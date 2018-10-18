using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class MBTransferToFaultForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "store_house_ng_in";

      //  string notgood_house = "", notgood_place = "";

        private ChooseStock chooseStock = new ChooseStock();

        public MBTransferToFaultForm()
        {
            InitializeComponent();
        }

        private void modify_Click(object sender, EventArgs e)
        {
            try
            {
                if (numberTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("数量有问题，请检查");
                    return;
                }
                // "MPN", "数量", "库房", "储位" };
                string mpn = dataGridView1.SelectedCells[0].Value.ToString().Trim();
                string number = dataGridView1.SelectedCells[1].Value.ToString().Trim();
                string house = dataGridView1.SelectedCells[2].Value.ToString().Trim();
                string place = dataGridView1.SelectedCells[3].Value.ToString().Trim();

                string houseN = house+"-N";
                string placeN = place + "-N";

                int wantedTotransferNumber = Int32.Parse(numberTextBox.Text.Trim());
                int oriNumber = Int32.Parse(number);
                if (wantedTotransferNumber < 0 || wantedTotransferNumber > oriNumber)
                {
                    MessageBox.Show("数量有问题，请检查");
                    return;
                }

                //继续操作
                try
                {                    
                    SqlConnection conn = new SqlConnection(Constlist.ConStr);
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        //减掉原来的
                        cmd.CommandText = "update store_house set number = '" + (oriNumber - wantedTotransferNumber) +
                             "'  where mpn='" + dataGridView1.SelectedCells[0].Value.ToString().Trim() + "'";
                        cmd.ExecuteNonQuery();

                        //不良品库加上
                        //记录信息，并更新库存，注意原来的库存
                        cmd.CommandText = "select number from store_house_ng where mpn ='" + mpn + "' and house ='"+houseN+"' and place ='"+placeN+"'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string oldbadstoreNum = "0";
                        bool exist = false;
                        while (querySdr.Read())
                        {
                            exist = true;
                            oldbadstoreNum = querySdr[0].ToString();                           
                        }
                        int oldNumber = Int16.Parse(oldbadstoreNum);
                        querySdr.Close();

                        if (exist)
                        {
                            //若库房不存在，则自动生成库
                            cmd.CommandText = "INSERT INTO store_house_ng_in VALUES('"
                                + houseN + "','"
                                + placeN + "','"
                                + mpn + "','"
                                    + number + "','"
                                    + LoginForm.currentUser + "','"
                                + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update store_house_ng set number = '" + (oldNumber + wantedTotransferNumber) + "' where house='" + houseN + "' and place='" + placeN + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //若库房不存在，则自动生成库
                            cmd.CommandText = "INSERT INTO store_house_ng_in VALUES('"
                                + houseN + "','"
                                + placeN + "','"
                                + mpn + "','"
                                    + wantedTotransferNumber + "','"
                                    + LoginForm.currentUser + "','"
                                + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO  store_house_ng VALUES('" + houseN + "','" + placeN + "','" + mpn + "','" + wantedTotransferNumber + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("SaledService is not opened");
                    }

                    conn.Close();
                    query_Click_1(null, null);
                    querybuttonng_Click(null, null);
                    this.numberTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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
        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;
        }

        class useClass
        {
            public string mpn {get;set;}
            public string storeNum { get; set; }

            public string house { get; set; }
            public string place { get; set; }
        }

        private void idTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e!=null && e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click_1(null, null);
            }
        }

        private void query_Click_1(object sender, EventArgs e)
        {
            if (this.mbbriefTextBox.Text.Trim() == "")
            {
                return;
            }
            try
            {
                List<useClass> list = new List<useClass>();
                List<string> mpnList = new List<string>();
                mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select distinct mpn from MBMaterialCompare where mb_brief like '%" + this.mbbriefTextBox.Text.Trim() + "%'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    mpnList.Add(querySdr[0].ToString());
                }
                querySdr.Close();

                foreach (string str in mpnList)
                {
                    cmd.CommandText = "select mpn,number,house,place from store_house where mpn like '%" + str + "%'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string storeNum = querySdr[1].ToString();
                        try
                        {
                            if (Int32.Parse(storeNum) > 0)
                            {
                                useClass temp = new useClass();
                                temp.mpn = querySdr[0].ToString();
                                temp.storeNum = querySdr[1].ToString();
                                temp.house = querySdr[2].ToString();
                                temp.place = querySdr[3].ToString();

                                list.Add(temp);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    querySdr.Close();
                }
                mConn.Close();

                dataGridView1.DataSource = list;
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "MPN", "数量", "库房", "储位" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void querybuttonng_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection  mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  store_house_ng order by Id desc";
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

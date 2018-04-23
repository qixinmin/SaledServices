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
    public partial class RepairOperationForm : Form
    {
        public RepairOperationForm()
        {
            InitializeComponent();

            loadAdditionInfomation();
        }

        private void loadAdditionInfomation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct type from repairFaultType";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.fault_typecomboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    this.track_serial_noTextBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    error = true;
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    string sourceBrief = "", customOrder = "", order_receive_date = "", custom_serial_no = "", vendor_serial_no = "", mb_make_date = "", custom_fault = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                        sourceBrief = querySdr[1].ToString();
                        customOrder = querySdr[2].ToString();
                        order_receive_date = querySdr[3].ToString();
                        custom_serial_no = querySdr[4].ToString();
                        vendor_serial_no = querySdr[5].ToString();
                        mb_make_date = querySdr[6].ToString();
                        custom_fault = querySdr[7].ToString();

                    }
                    querySdr.Close();

                    if (customMaterialNo != "")
                    {
                        string vendor = "", product = "", mb_describe = "", mb_brief = "", mpn = "", eco = "";
                        cmd.CommandText = "select vendor,product, mb_descripe, mb_brief,mpn,eco from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                        querySdr = cmd.ExecuteReader();

                        while (querySdr.Read())
                        {
                            vendor = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                            mb_describe = querySdr[2].ToString();
                            mb_brief = querySdr[3].ToString();
                            mpn = querySdr[4].ToString();
                            eco = querySdr[5].ToString();
                        }
                        querySdr.Close();

                        this.vendorTextBox.Text = vendor;
                        this.producttextBox.Text = product;
                        this.sourcetextBox.Text = sourceBrief;
                        this.ordernotextBox.Text = customOrder;
                        this.receivedatetextBox.Text = order_receive_date;
                        this.mb_describetextBox.Text = mb_describe;
                        this.mb_brieftextBox.Text = mb_brief;
                        this.custom_serial_notextBox.Text = custom_serial_no;
                        this.vendor_serail_notextBox.Text = vendor_serial_no;
                        this.mpntextBox.Text = mpn;
                        this.mb_make_dateTextBox.Text = mb_make_date;
                        this.customFaulttextBox.Text = custom_fault;
                        this.ECOtextBox.Text = eco;
                        this.repairertextBox.Text = "tester";
                        this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    }
                    else
                    {  
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        error = true;
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                if (!error)
                {
                    repair_resultcomboBox.Focus();
                    repair_resultcomboBox.SelectAll();
                }
            }
        }

        private void not_good_placetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入追踪条码的内容");
                    this.track_serial_noTextBox.Focus();
                    return;
                }
                if (this.not_good_placetextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入内容");
                    this.not_good_placetextBox.Focus();
                    return;
                }

                string tableName = "";
                if (this.vendorTextBox.Text.Trim() == "LCFC")
                {
                    tableName = Constlist.table_name_LCFC_MBBOM;
                }
                else if (this.vendorTextBox.Text.Trim() == "COMPAL")
                {
                    tableName = Constlist.table_name_COMPAL_MBBOM;
                }

                string not_good_place = this.not_good_placetextBox.Text.Trim();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    //先用mpn在bom表中找一遍，如果找不到，然后用mb简称再查一遍，如果都没有，要不输错了，要不bom表不全

                    cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8 from " + tableName + " where MPN ='" + this.mpntextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    this.material_mpntextBox.Text = "";
                    while (querySdr.Read())
                    {
                        string material_mpn = querySdr[0].ToString(); ;
                        string temp = querySdr[1].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[2].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[3].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[4].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[5].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[6].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[7].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        } temp = querySdr[8].ToString();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            this.material_mpntextBox.Text = material_mpn;
                            break;
                        }
                    }
                    querySdr.Close();

                    if (this.material_mpntextBox.Text == "")
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8 from " + tableName + " where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        this.material_mpntextBox.Text = "";
                        while (querySdr.Read())
                        {
                            string material_mpn = querySdr[0].ToString(); ;
                            string temp = querySdr[1].ToString();
                            if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[2].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[3].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[4].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[5].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[6].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[7].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            } temp = querySdr[8].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.material_mpntextBox.Text = material_mpn;
                                break;
                            }
                        }
                        querySdr.Close();
                    }

                    if (this.material_mpntextBox.Text.Trim() == "")
                    {
                        error = true;
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                    }
                    else
                    {
                        cmd.CommandText = "select material_vendor_pn from LCFC71BOM_table where material_mpn='" + this.material_mpntextBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();

                        string material_71pn_txt = "";
                        while (querySdr.Read())
                        {
                            material_71pn_txt = querySdr[0].ToString();
                            if (material_71pn_txt != "")
                            {
                                this.material_71pntextBox.Text = material_71pn_txt;
                            }
                            else
                            {
                                error = true;
                                MessageBox.Show("LCFC71BOM表中" + this.material_mpntextBox.Text.Trim() + "信息不全！");
                            }
                        }
                        querySdr.Close();
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (!error)
                {
                    material_mpntextBox.Focus();
                    material_mpntextBox.SelectAll();
                }
            }
        }

        private void uncheckShortCut()
        {
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;
            this.checkBox6.Checked = false;
            this.checkBox7.Checked = false;
            this.checkBox8.Checked = false;
            this.checkBox9.Checked = false;
            this.checkBox10.Checked = false;
            this.checkBox11.Checked = false;
            this.checkBox12.Checked = false;
            this.checkBox13.Checked = false;
            this.checkBox14.Checked = false;
            this.checkBox15.Checked = false;
            this.checkBox16.Checked = false;
            this.checkBox17.Checked = false;
            this.checkBox18.Checked = false;
            this.textBox1.Text = "";
        }

        private string getShortCutText()
        {
            string retStr = "";
            if (checkBox1.Checked)
            {
                retStr += checkBox1.Text.Trim() + ",";
            }
            if (checkBox2.Checked)
            {
                retStr += checkBox2.Text.Trim() + ",";
            }
            if (checkBox3.Checked)
            {
                retStr += checkBox3.Text.Trim() + ",";
            }

            if (checkBox4.Checked)
            {
                retStr += checkBox4.Text.Trim() + ",";
            }

            if (checkBox5.Checked)
            {
                retStr += checkBox5.Text.Trim() + ",";
            }
            if (checkBox6.Checked)
            {
                retStr += checkBox6.Text.Trim() + ",";
            }
            if (checkBox7.Checked)
            {
                retStr += checkBox7.Text.Trim() + ",";
            }
            if (checkBox8.Checked)
            {
                retStr += checkBox8.Text.Trim() + ",";
            }
            if (checkBox9.Checked)
            {
                retStr += checkBox9.Text.Trim() + ",";
            }
            if (checkBox10.Checked)
            {
                retStr += checkBox10.Text.Trim() + ",";
            }
            if (checkBox11.Checked)
            {
                retStr += checkBox11.Text.Trim() + ",";
            } if (checkBox12.Checked)
            {
                retStr += checkBox12.Text.Trim() + ",";
            } if (checkBox13.Checked)
            {
                retStr += checkBox13.Text.Trim() + ",";
            } if (checkBox14.Checked)
            {
                retStr += checkBox14.Text.Trim() + ",";
            } if (checkBox15.Checked)
            {
                retStr += checkBox15.Text.Trim() + ",";
            } if (checkBox16.Checked)
            {
                retStr += checkBox16.Text.Trim() + ",";
            } if (checkBox17.Checked)
            {
                retStr += checkBox17.Text.Trim() + ",";
            } if (checkBox18.Checked)
            {
                retStr += checkBox18.Text.Trim() + ",";
            } 
            if (this.textBox1.Text != "")
            {
                retStr += textBox1.Text.Trim() + ",";
            }

            return retStr;
        }


        private void RepairOperationForm_Load(object sender, EventArgs e)
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

            tableLayoutPanel4.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel4, true, null);
        }
       

        private void add_Click(object sender, EventArgs e)
        {
            bool error = false;
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();
            string source_txt = this.sourcetextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string mb_describe_txt = this.mb_describetextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string mb_make_date_txt = this.mb_make_dateTextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
            string fault_describe_txt = this.fault_describetextBox.Text.Trim();
            string mbfa1rich_txt = this.mbfa1richTextBox.Text.Trim();
            string short_cut_txt = getShortCutText();
            string software_update_txt = this.software_updatecomboBox.Text.Trim();
            string not_good_place_txt = this.not_good_placetextBox.Text.Trim();
            string material_mpn_txt = this.material_mpntextBox.Text.Trim();
            string material_71pn_txt = this.material_71pntextBox.Text.Trim();
            string material_type_txt = this.material_typetextBox.Text.Trim();
            string fault_type_txt = this.fault_typecomboBox.Text.Trim();
            string action_txt = this.actioncomboBox.Text.Trim();
          
            string ECO_txt = this.ECOtextBox.Text.Trim();
            string repair_result_txt = this.repair_resultcomboBox.Text.Trim();
            string repairer_txt = this.repairertextBox.Text.Trim();
            string repair_date_txt = this.repair_datetextBox.Text.Trim();

            bool isNTF = false;

            if (repair_resultcomboBox.Text.Contains("NTF"))
            {
                isNTF = true;

                mbfa1rich_txt = "NTF";
                software_update_txt = "NTF";
                not_good_place_txt = "NTF";
                material_mpn_txt = "NTF";
                material_71pn_txt = "NTF";
                material_type_txt = "NTF";
                fault_type_txt = "NTF";
                action_txt = "NTF";
            }
            else //“非NTF状态
            {
                isNTF = false;
                if (fault_describetextBox.Text.Trim() == ""
                    || not_good_placetextBox.Text.Trim() == ""
                    || fault_typecomboBox.Text.Trim() == ""
                    || actioncomboBox.Text.Trim() == "")
                {
                    MessageBox.Show("必须输入的框中有空值!");
                    return;
                }
            }


            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO repair_record_table VALUES('"
                        + track_serial_no_txt + "','"
                        + vendor_txt + "','"
                        + product_txt + "','"
                        + source_txt + "','"
                        + orderno_txt + "','"
                        + receivedate_txt + "','"
                        + mb_describe_txt + "','"
                        + mb_brief_txt + "','"
                        + custom_serial_no_txt + "','"
                        + vendor_serail_no_txt + "','"
                        + mpn_txt + "','"
                        + mb_make_date_txt + "','"
                        + customFault_txt + "','"
                        + fault_describe_txt + "','"
                        + mbfa1rich_txt + "','"
                        + short_cut_txt + "','"
                        + software_update_txt + "','"
                        + not_good_place_txt + "','"
                        + material_mpn_txt + "','"
                        + material_71pn_txt + "','"
                        + material_type_txt + "','"
                        + fault_type_txt + "','"
                        + action_txt + "','"
                      
                        + ECO_txt + "','"
                        + repair_result_txt + "','"
                        + repairer_txt + "','"
                        + repair_date_txt + "')";

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //更新维修站别
                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                               + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = true;
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(ex.ToString());
            }

            if (error == false)
            {
                MessageBox.Show("添加维修数据成功");

                this.track_serial_noTextBox.Text = "";
                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
                this.sourcetextBox.Text = "";
                this.ordernotextBox.Text = "";
                this.receivedatetextBox.Text = "";
                this.mb_describetextBox.Text = "";
                this.mb_brieftextBox.Text = "";
                this.custom_serial_notextBox.Text = "";
                this.vendor_serail_notextBox.Text = "";
                this.mpntextBox.Text = "";
                this.mb_make_dateTextBox.Text = "";
                this.customFaulttextBox.Text = "";
                this.fault_describetextBox.Text = "";
                this.mbfa1richTextBox.Text = "";
                uncheckShortCut();
                this.software_updatecomboBox.Text = "";
                this.not_good_placetextBox.Text = "";
                this.material_mpntextBox.Text = "";
                this.material_71pntextBox.Text = "";
                this.material_typetextBox.Text = "";
                this.fault_typecomboBox.Text = "";
                this.actioncomboBox.Text = "";
               
                this.ECOtextBox.Text = "";
                this.repair_resultcomboBox.Text = "";
                this.repairertextBox.Text = "";
                this.repair_datetextBox.Text = "";

                if (isNTF)//非NTF复位
                {
                    this.software_updatecomboBox.Enabled = true;
                    this.not_good_placetextBox.Enabled = true;
                    this.material_mpntextBox.Enabled = true;
                    this.fault_typecomboBox.Enabled = true;
                    this.actioncomboBox.Enabled = true;
                    this.mbfa1richTextBox.Enabled = true;
                    this.fault_describetextBox.Enabled = true;

                    this.checkBox1.Enabled = true;
                    this.checkBox2.Enabled = true;
                    this.checkBox3.Enabled = true;
                    this.checkBox4.Enabled = true;
                    this.checkBox5.Enabled = true;
                    this.checkBox6.Enabled = true;
                    this.checkBox7.Enabled = true;
                    this.checkBox8.Enabled = true;
                    this.checkBox9.Enabled = true;
                    this.checkBox10.Enabled = true;
                    this.checkBox11.Enabled = true;
                    this.checkBox12.Enabled = true;
                    this.checkBox13.Enabled = true;
                    this.checkBox14.Enabled = true;
                    this.checkBox15.Enabled = true;
                    this.checkBox16.Enabled = true;
                    this.checkBox17.Enabled = true;
                    this.checkBox18.Enabled = true;
                    this.textBox1.Enabled = true;
                }

                this.track_serial_noTextBox.Focus();
                query_Click(null, null);
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from repair_record_table";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "repair_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "厂商","客户别","来源","订单编号",
                             "收货日期","MB描述","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","故障原因","mbfa1","短路电压","软体更新",
                             "不良位置","材料MPN","材料71PN","材料类别","故障类别", "动作",
                             "ECO","修复结果","维修人", "修复日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void repair_resultcomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.repair_resultcomboBox.Text.Contains("NTF"))
            {
                this.software_updatecomboBox.Enabled = false;
                this.not_good_placetextBox.Enabled = false;
                this.material_mpntextBox.Enabled = false;
                this.fault_typecomboBox.Enabled = false;
                this.actioncomboBox.Enabled = false;
                this.mbfa1richTextBox.Enabled = false;
                this.fault_describetextBox.Enabled = false;             

                this.checkBox1.Enabled = false;
                this.checkBox2.Enabled = false;
                this.checkBox3.Enabled = false;
                this.checkBox4.Enabled = false;
                this.checkBox5.Enabled = false;
                this.checkBox6.Enabled = false;
                this.checkBox7.Enabled = false;
                this.checkBox8.Enabled = false;
                this.checkBox9.Enabled = false;
                this.checkBox10.Enabled = false;
                this.checkBox11.Enabled = false;
                this.checkBox12.Enabled = false;
                this.checkBox13.Enabled = false;
                this.checkBox14.Enabled = false;
                this.checkBox15.Enabled = false;
                this.checkBox16.Enabled = false;
                this.checkBox17.Enabled = false;
                this.checkBox18.Enabled = false;
                this.textBox1.Enabled = false;                
            }
            else
            {
                this.software_updatecomboBox.Enabled = true;
                this.not_good_placetextBox.Enabled = true;
                this.material_mpntextBox.Enabled = true;
                this.fault_typecomboBox.Enabled = true;
                this.actioncomboBox.Enabled = true;
                this.mbfa1richTextBox.Enabled = true;
                this.fault_describetextBox.Enabled = true;              

                this.checkBox1.Enabled = true;
                this.checkBox2.Enabled = true;
                this.checkBox3.Enabled = true;
                this.checkBox4.Enabled = true;
                this.checkBox5.Enabled = true;
                this.checkBox6.Enabled = true;
                this.checkBox7.Enabled = true;
                this.checkBox8.Enabled = true;
                this.checkBox9.Enabled = true;
                this.checkBox10.Enabled = true;
                this.checkBox11.Enabled = true;
                this.checkBox12.Enabled = true;
                this.checkBox13.Enabled = true;
                this.checkBox14.Enabled = true;
                this.checkBox15.Enabled = true;
                this.checkBox16.Enabled = true;
                this.checkBox17.Enabled = true;
                this.checkBox18.Enabled = true;
                this.textBox1.Enabled = true;

                this.software_updatecomboBox.Focus();
                this.software_updatecomboBox.SelectAll();
            }          
        }


        private void request_to_store_button_Click(object sender, EventArgs e)
        {
            Store.RequestToStoreForm rtsf = new Store.RequestToStoreForm();
            rtsf.setParameters(this.track_serial_noTextBox.Text, this.material_mpntextBox.Text, this.material_71pntextBox.Text);
            rtsf.Show();
        }
    }
}

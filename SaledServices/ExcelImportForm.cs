using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace SaledServices
{
    public partial class ExcelImportForm : Form
    {
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;


        public ExcelImportForm()
        {
            InitializeComponent();
        }

        private void findFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            if ((fileName.EndsWith("xls") || fileName.EndsWith("xlsx"))
                && importTargetComboBox.Text != "")
            {
                filePath.Text = fileName;
                importButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("请输入正确的xls文件 并选择正确的import目标!");
            }
        }

        private string appendString(string input)
        {
            input = input.TrimStart('0');

            //规则：最小10位，如果不满10位则前面补0
            int count = 10 - input.Length;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    input = "0" + input;
                }
            }
            Console.WriteLine(input);
            return input;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(filePath.Text);
            
            wb = wbs.Open(filePath.Text, 0, false, 5, string.Empty, string.Empty, 
                false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, 
                string.Empty, true, false, 0, true, 1, 0);

            string sheetName = "";
            string tableName = "";
            if (importTargetComboBox.Text == Constlist.table_MBMaterialCompare)
            {
                sheetName = Constlist.table_MBMaterialCompare;
                tableName = Constlist.table_name_MBMaterialCompare;
            }
            else if (importTargetComboBox.Text == Constlist.table_receiveOrder)
            {
                sheetName = Constlist.table_receiveOrder;
                tableName = Constlist.table_name_ReceiveOrder;
            }
            else
            {
                MessageBox.Show("信息不全");
            }

            if (importTargetComboBox.Text == Constlist.table_MBMaterialCompare)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;
                importMaterialCompare(ws, rowLength, columnLength, tableName);
            }
            else if (importTargetComboBox.Text == Constlist.table_receiveOrder)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;

                //订单号
                string order = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 2]).Value2.ToString();

                SqlConnection queryConn = new SqlConnection(Constlist.ConStr);
                queryConn.Open();

                SqlCommand queryCmd = new SqlCommand();
                queryCmd.Connection = queryConn;
                queryCmd.CommandType = CommandType.Text;

                SqlCommand insertCmd = new SqlCommand();
                insertCmd.Connection = queryConn;
                insertCmd.CommandType = CommandType.Text;

                if (queryConn.State == ConnectionState.Open)
                {
                    for (int i = 8; i < rowLength; i++)
                    {
                        //客户料号
                        string customOrderNo = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 2]).Value2.ToString();
                        //客户物料描述
                        string customMaterialDescribe = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();
                        //订单数量
                        string orderNum = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Value2.ToString();

                        customOrderNo = appendString(customOrderNo);
                        string querysql = "select * from " + Constlist.table_name_MBMaterialCompare + 
                            " where custommaterialNo ='" + customOrderNo + "'";

                        queryCmd.CommandText = querysql;
                        SqlDataReader querySdr = queryCmd.ExecuteReader();
                        string vendor = "";
                        string product = "";
                        string mb_brief = "";
                        string vendor_materialNo = "";
                        if (querySdr.FieldCount > 0 && querySdr.Read())
                        {
                            vendor = querySdr[1].ToString();
                            product = querySdr[2].ToString();
                            mb_brief = querySdr[3].ToString();
                            vendor_materialNo = querySdr[4].ToString();
                        }

                        querySdr.Close();


                        insertCmd.CommandText = "INSERT INTO " + "receiveOrder" + " VALUES('" +
                            vendor + "','" +
                            product + "','" +
                            order + "','" +
                            customOrderNo + "','" +
                            customMaterialDescribe + "','" +
                            orderNum + "','" +
                            mb_brief + "','" +
                            vendor_materialNo + "','" +
                            "testUser" + "','" +
                            DateTime.Now + "','" +
                            "0" + "','" +
                            "NULL" + "','" +
                            "open" + "')";

                        insertCmd.ExecuteNonQuery();
                    }

                    try
                    {                        
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                queryConn.Close();
            }
        }

        public void importMaterialCompare(Worksheet ws, int rowLength, int columnLength, string tableName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    for (int i = 1; i <= rowLength; i++)
                    {
                        string s = "INSERT INTO " + tableName + " VALUES('";
                        for (int j = 1; j <= columnLength; j++)
                        {
                            try
                            {
                                //有可能有空值
                                string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();

                                if (j == 7)
                                {
                                    temp = appendString(temp);
                                }
                                s += temp;
                            }
                            catch (Exception ex)
                            {
                                s += " ";
                            }

                            if (j != columnLength)
                            {
                                s += "','";
                            }
                            else
                            {
                                s += "')";
                            }

                            // Console.WriteLine(s);
                        }
                        cmd.CommandText = s;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("导入" + tableName + "完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                wbs.Close();
            }
        }
    }
}

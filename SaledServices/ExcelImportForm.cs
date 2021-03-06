﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            if (fileName.EndsWith("xls") || fileName.EndsWith("xlsx"))
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

        //判断excel表格的内容是否为空，如果为空，则弹出对话框提示
        private bool checkIsNullCell(Worksheet ws, int rowLength, int columnLength)
        {
            bool ret = false;
            for (int i = 1; i <= rowLength; i++)
            {  
                for (int j = 1; j <= columnLength; j++)
                {   
                    //有可能有空值
                    try
                    {
                        string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();

                        if (temp == null || temp == "")
                        {
                            ret = true;
                            return ret;
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = true;
                        return ret;
                    }
                }
            }

            return ret;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            this.importButton.Enabled = false;
            string sheetName = "";
            string tableName = "";
            if (this.mbmaterial.Checked)
            {
                sheetName = Constlist.table_MBMaterialCompare;
                tableName = Constlist.table_name_MBMaterialCompare;
            }
            else if (this.receiveOrder.Checked)
            {
                sheetName = Constlist.table_receiveOrder;
                tableName = Constlist.table_name_ReceiveOrder;
            }
            else if(this.frureceiveOrder.Checked)
            {
                sheetName = Constlist.table_frureceiveOrder;
                tableName = Constlist.table_name_fruReceiveOrder;
            }
            else if (this.LCFC_MBBOMradioButton.Checked)
            {
                sheetName = Constlist.table_LCFC_MBBOM;
                tableName = Constlist.table_name_LCFC_MBBOM;
            }
            //else if (this.COMPAL_MBBOMradioButton.Checked)
            //{
            //    sheetName = Constlist.table_COMPAL_MBBOM;
            //    tableName = Constlist.table_name_COMPAL_MBBOM;
            //}
            else if (this.LCFC71BOMRadioButton.Checked)
            {
                sheetName = Constlist.table_LCFC71BOM;
                tableName = Constlist.table_name_LCFC71BOM;
            }
            else if(this.DPKradioButton.Checked)
            {
                sheetName = Constlist.table_DPK;
                tableName = Constlist.table_name_DPK;
            }
            else if (this.faultTableRadioButton.Checked)
            {
                sheetName = Constlist.table_customFault;
                tableName = Constlist.table_name_customFault;
            }
            else if (this.stock_in_sheetradioButton.Checked)
            {
                sheetName = Constlist.table_stock_in_sheet;
                tableName = Constlist.table_name_stock_in_sheet;
            }
            else if (this.storeInfoImportradioButton.Checked)
            {
                sheetName = Constlist.table_stock_house;
                tableName = Constlist.table_name_store_house_sheet;
            }
            else if (this.ngstoreInfoImport.Checked)
            {
                sheetName = Constlist.table_stock_ng_house;
                tableName = Constlist.table_name_store_ng_house_sheet;
            }
            else if (this.userInputRadioButton.Checked)
            {
                sheetName = Constlist.table_users;
                tableName = Constlist.table_name_users_sheet;
            }
            else if (this.guranteCheckRadioButton.Checked)
            {
                sheetName = Constlist.table_limit_gurante;
                tableName = Constlist.table_limit_gurante_sheet;
            }
            else if (this.FruBom.Checked)
            {
                sheetName = Constlist.table_frubom;
                tableName = Constlist.table_name_frubom_sheet;
            }
            else if (this.ymrenbaocodecompare.Checked)
            {
                sheetName = Constlist.table_badcodes;
                tableName = Constlist.table_name_badcodes;
            }
            else if (this.machineImportRadioButton.Checked)
            {
                sheetName = "整机出货量";
                tableName = "whole_import_sheet";
            }
            else if (this.mbcheckimport.Checked)
            {
                sheetName = Constlist.table_mb_receive_check;
                tableName = Constlist.table_name_mb_receive_check;
            }

            if (this.LCFC_MBBOMradioButton.Checked)
            {
                importLCFC_MBBOMUsingADO(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            else if (this.LCFC71BOMRadioButton.Checked)
            {
                importLCFC_71BOMUsingADO(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            else if (this.userInputRadioButton.Checked)
            {
                importUserInfo(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            else if (this.updateDBRadio.Checked)
            {
                updateDB("updateDB", "store_house");
                this.importButton.Enabled = true;
                return;
            }
            else if (this.guranteCheckRadioButton.Checked)
            {
                importGuranteCheck(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            //else if (this.FruBom.Checked)
            //{
            //    importFrubomCheck(sheetName, tableName);
            //    this.importButton.Enabled = true;
            //    return;
            //}
            else if (this.ymrenbaocodecompare.Checked)
            {
                importbadcodesCheck(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            else if (this.mbcheckimport.Checked)
            {
                importMbCheck(sheetName, tableName);
                this.importButton.Enabled = true;
                return;
            }
            

            app = new Microsoft.Office.Interop.Excel.Application();
            app.DisplayAlerts = false;
            wbs = app.Workbooks;
            wb = wbs.Add(filePath.Text);            
            
            wb = wbs.Open(filePath.Text, 0, false, 5, string.Empty, string.Empty, 
                false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, 
                string.Empty, true, false, 0, true, 1, 0);

            if ( this.DPKradioButton.Checked
                || this.faultTableRadioButton.Checked
                
                || this.stock_in_sheetradioButton.Checked
                || this.storeInfoImportradioButton.Checked
                || this.ngstoreInfoImport.Checked)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;
                importLCFC_MBBOM(ws, rowLength, columnLength, tableName);                
            }
            else if (this.mbmaterial.Checked)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;
                importMaterialCompare(ws, rowLength, columnLength, tableName);
            }
            else if (this.FruBom.Checked)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;
                importFrubomCheck(ws, rowLength, columnLength, tableName);
            }
            else if (this.machineImportRadioButton.Checked)
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;
                importMachineImportCheck(ws, rowLength, columnLength, tableName);                
            }
            else if (this.receiveOrder.Checked)//表格类似
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;

                if (checkIsNullCell(ws, rowLength, columnLength))
                {
                    MessageBox.Show("RMA中有空值，请确认后在上传");
                    closeAndKillApp();
                    this.importButton.Enabled = true;
                    return;
                }

                try
                {
                    //订单号
                    string order = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[2, 2]).Value2.ToString();

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
                        bool customMaterialNoExist = true;
                        try
                        {
                            for (int i = 2; i <= rowLength; i++)
                            {
                                //逻辑1 判断客户料号是否在物料对照对照表中
                                //客户料号
                                string customMaterialNo = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();

                                customMaterialNo = appendString(customMaterialNo);
                                string querysql = "select vendor,product,mb_brief,vendormaterialNo,mb_descripe from " + Constlist.table_name_MBMaterialCompare +
                                    " where custommaterialNo ='" + customMaterialNo + "'";

                                queryCmd.CommandText = querysql;
                                SqlDataReader querySdr = queryCmd.ExecuteReader();
                                string vendor = "";
                                if (querySdr.FieldCount > 0 && querySdr.Read())
                                {
                                    vendor = querySdr[0].ToString();
                                }
                                querySdr.Close();
                                if (vendor == "")
                                {
                                    MessageBox.Show(customMaterialNo + "在物料对照表不存在，请添加！");
                                    customMaterialNoExist = false;
                                    break;
                                }

                                //逻辑2 判断此excel表格是否已经导入过数据库中
                                querysql = "select vendor from receiveOrder where custom_materialNo ='" + customMaterialNo + "' and orderno='" + order + "'";

                                queryCmd.CommandText = querysql;
                                querySdr = queryCmd.ExecuteReader();
                                vendor = "";
                                if (querySdr.FieldCount > 0 && querySdr.Read())
                                {
                                    vendor = querySdr[0].ToString();
                                }
                                querySdr.Close();
                                if (vendor != "")
                                {
                                    MessageBox.Show(filePath.Text + "已经导入过了，请检查！");
                                    customMaterialNoExist = false;
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            if (!customMaterialNoExist)
                            {
                                queryConn.Close();
                            }
                        }

                        if (!customMaterialNoExist)
                        {
                            queryConn.Close();
                            closeAndKillApp();
                            this.importButton.Enabled = true;
                            return;
                        }

                        for (int i = 2; i <= rowLength; i++)
                        {
                            string storeHouse = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 1]).Value2.ToString();
                            //客户料号
                            string customMaterialNo = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();

                            //订单数量
                            string orderNum = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Value2.ToString();

                            customMaterialNo = appendString(customMaterialNo);
                            string querysql = "select vendor,product,mb_brief,vendormaterialNo,mb_descripe from " + Constlist.table_name_MBMaterialCompare +
                                " where custommaterialNo ='" + customMaterialNo + "'";

                            queryCmd.CommandText = querysql;
                            SqlDataReader querySdr = queryCmd.ExecuteReader();
                            string vendor = "";
                            string product = "";
                            string mb_brief = "";
                            string vendor_materialNo = "";
                            string customMaterialDescribe = "";
                            if (querySdr.FieldCount > 0 && querySdr.Read())
                            {
                                vendor = querySdr[0].ToString();
                                product = querySdr[1].ToString();
                                mb_brief = querySdr[2].ToString();
                                vendor_materialNo = querySdr[3].ToString();
                                customMaterialDescribe = querySdr[4].ToString();
                            }

                            querySdr.Close();
                             
                            insertCmd.CommandText = "INSERT INTO receiveOrder VALUES('" +
                                vendor + "','" +
                                product + "','" +
                                order + "','" +
                                customMaterialNo + "','" +
                                customMaterialDescribe + "','" +
                                orderNum + "','" +
                                mb_brief + "','" +
                                vendor_materialNo + "','" +
                                LoginForm.currentUser + "','" +                                
                                 DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo)+ "','" +
                                "0" + "','" +
                                DateTime.Now.ToString("1900/01/01") + "','" +
                                "open" + "','" +
                                storeHouse + "','0','0')";

                            insertCmd.ExecuteNonQuery();

                            //插入obe抽查比例
                            //insertCmd.CommandText = "select Id from  obe_checkrate_table where orderno='" + order + "' and custom_materialNo='" + customMaterialNo + "'";

                            //querySdr = insertCmd.ExecuteReader();
                            //if (querySdr.HasRows)
                            //{
                            //    querySdr.Close();
                            //}
                            //else
                            //{
                            //    querySdr.Close();

                            //    insertCmd.CommandText = "INSERT INTO obe_checkrate_table VALUES('"
                            //        + order + "','"
                            //        + customMaterialNo + "','"
                            //        + "0.1" + "','"
                            //        + LoginForm.currentUser + "','"
                            //        + DateTime.Now.ToString("yyyy/MM/dd")
                            //        + "')";

                            //    insertCmd.ExecuteNonQuery();
                            //}
                            //end //插入obe抽查比例
                        }
                    }

                    queryConn.Close();
                    MessageBox.Show("导入完毕");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    closeAndKillApp();
                }
            }
            else if (this.frureceiveOrder.Checked)//表格类似
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;

                if (checkIsNullCell(ws, rowLength, columnLength))
                {
                    MessageBox.Show("RMA中有空值，请确认后在上传");
                    closeAndKillApp();
                    this.importButton.Enabled = true;
                    return;
                }

                try
                {
                    //订单号
                    string order = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[2, 2]).Value2.ToString();

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
                        bool customMaterialNoExist = true;
                        try
                        {
                            for (int i = 2; i <= rowLength; i++)
                            {
                                //逻辑1 判断客户料号是否在物料对照对照表中
                                //客户料号
                                string customMaterialNo = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();

                               // customMaterialNo = appendString(customMaterialNo);
                                string querysql = "select vendor,product,custommaterialdescribe,machine_type,mpn1 from " + Constlist.table_name_frubomtable +
                                    " where custom_material_no ='" + customMaterialNo + "'";

                                queryCmd.CommandText = querysql;
                                SqlDataReader querySdr = queryCmd.ExecuteReader();
                                string vendor = "";
                                if (querySdr.HasRows)
                                {
                                    querySdr.Read();
                                     vendor = querySdr[0].ToString();
                                }
                                querySdr.Close();
                                if (vendor == "")
                                {
                                    MessageBox.Show(customMaterialNo + "在物料对照表不存在，请添加！");
                                    customMaterialNoExist = false;
                                    break;
                                }

                                //逻辑2 判断此excel表格是否已经导入过数据库中
                                querysql = "select vendor from frureceiveOrder where custom_materialNo ='" + customMaterialNo + "' and orderno='" + order + "'";

                                queryCmd.CommandText = querysql;
                                querySdr = queryCmd.ExecuteReader();
                                vendor = "";
                                if (querySdr.HasRows)
                                {
                                    querySdr.Read();
                                    vendor = querySdr[0].ToString();
                                }
                                querySdr.Close();
                                if (vendor != "")
                                {
                                    MessageBox.Show(filePath.Text + "已经导入过了，请检查！");
                                    customMaterialNoExist = false;
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            if (!customMaterialNoExist)
                            {
                                queryConn.Close();
                            }
                        }

                        if (!customMaterialNoExist)
                        {
                            queryConn.Close();
                            closeAndKillApp();
                            this.importButton.Enabled = true;
                            return;
                        }

                        for (int i = 2; i <= rowLength; i++)
                        {
                            string storeHouse = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 1]).Value2.ToString();
                            //客户料号
                            string customMaterialNo = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();

                            //订单数量
                            string orderNum = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Value2.ToString();

                           // customMaterialNo = appendString(customMaterialNo);
                            string querysql = "select vendor,product,machine_type,mpn1,custommaterialdescribe from " + Constlist.table_name_frubomtable +
                                " where custom_material_no ='" + customMaterialNo + "'";

                            queryCmd.CommandText = querysql;
                            SqlDataReader querySdr = queryCmd.ExecuteReader();
                            string vendor = "";
                            string product = "";
                            string mb_brief = "";
                            string vendor_materialNo = "";
                            string customMaterialDescribe = "";
                            if (querySdr.HasRows)
                            {
                                querySdr.Read();
                                vendor = querySdr[0].ToString();
                                product = querySdr[1].ToString();
                                mb_brief = querySdr[2].ToString();
                                vendor_materialNo = querySdr[3].ToString();
                                customMaterialDescribe = querySdr[4].ToString();
                            }

                            querySdr.Close();
                             
                            insertCmd.CommandText = "INSERT INTO frureceiveOrder VALUES('" +
                                vendor + "','" +
                                product + "','" +
                                order + "','" +
                                customMaterialNo + "','" +
                                customMaterialDescribe + "','" +
                                orderNum + "','" +
                                mb_brief + "','" +
                                vendor_materialNo + "','" +
                                LoginForm.currentUser + "','" +                                
                                 DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo)+ "','" +
                                "0" + "','" +
                                DateTime.Now.ToString("1900/01/01") + "','" +
                                "open" + "','" +
                                storeHouse + "','0','0')";

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    queryConn.Close();
                    MessageBox.Show("导入完毕");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    closeAndKillApp();
                }
            }

            this.importButton.Enabled = true;
        }

        private void updateDB(string sheetName, string tableName)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            app.DisplayAlerts = false;
            wbs = app.Workbooks;
            wb = wbs.Add(filePath.Text);            
            
            wb = wbs.Open(filePath.Text, 0, false, 5, string.Empty, string.Empty, 
                false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, 
                string.Empty, true, false, 0, true, 1, 0);

            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
            int rowLength = ws.UsedRange.Rows.Count;
            int columnLength = ws.UsedRange.Columns.Count;            

            string s = "";
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();
                SqlTransaction transaction = null;
                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;

                    for (int i = 2; i <= rowLength; i++)
                    {
                        //string vendor = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();
                        string mpn = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Value2.ToString();
                        string combinMpn = mpn+"_COMPAL";
                        s = "update store_house set mpn ='"+combinMpn+"' where mpn='"+mpn+"'";
                        cmd.CommandText = s;
                        cmd.ExecuteNonQuery();

                    }
                    transaction.Commit();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                transaction.Dispose();
                conn.Dispose();

                MessageBox.Show("修改" + tableName + "完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(s + "#" + ex.ToString());
            }
            finally
            {
                wbs.Close();
            }
        }
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        private void closeAndKillApp()
        {
            try
            {
                wbs.Close();
                app.Quit();
                IntPtr intptr = new IntPtr(app.Hwnd);
                int id;
                GetWindowThreadProcessId(intptr, out id);
                var p = Process.GetProcessById(id);
                //if (p != null)
                p.Kill();
            }
            catch (Exception ex)
            { }
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

                    for (int i = 2; i <= rowLength; i++)
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
               
                closeAndKillApp();                
            }
        }

        public void importLCFC_MBBOMUsingADO(string sheetName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);
               
                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                   // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 10000;//每次传输的行数
                    bcp.NotifyAfter = 10000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表

                    bcp.ColumnMappings.Add("日期", "_date");
                    bcp.ColumnMappings.Add("厂商", "vendor");
                    bcp.ColumnMappings.Add("客户别","product");
                    bcp.ColumnMappings.Add("MB简称", "mb_brief");
                    bcp.ColumnMappings.Add("MPN", "MPN");
                    bcp.ColumnMappings.Add("材料MPN","material_mpn");
                    bcp.ColumnMappings.Add("料盒位置","material_box_place");
                    bcp.ColumnMappings.Add("物料描述","material_describe");
 
                    bcp.ColumnMappings.Add("用料数量","material_num");
                    bcp.ColumnMappings.Add("L1", "L1");
                    bcp.ColumnMappings.Add("L2", "L2");
                    bcp.ColumnMappings.Add("L3", "L3");
                    bcp.ColumnMappings.Add("L4", "L4");
                    bcp.ColumnMappings.Add("L5", "L5");
                    bcp.ColumnMappings.Add("L6", "L6");
                    bcp.ColumnMappings.Add("L7", "L7");
                    bcp.ColumnMappings.Add("L8", "L8");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }           
        }
        
        public void importLCFC_71BOMUsingADO(string sheetName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);
               
                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                   // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 1000;//每次传输的行数
                    bcp.NotifyAfter = 1000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表

                    bcp.ColumnMappings.Add("日期", "_date");
                    bcp.ColumnMappings.Add("MB简称", "mb_brief");
                    bcp.ColumnMappings.Add("材料厂商PN","material_vendor_pn");
                    bcp.ColumnMappings.Add("材料MPN", "material_mpn");
                    bcp.ColumnMappings.Add("Description", "_description");
                    bcp.ColumnMappings.Add("2017 Q4 final price","price");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }           
        }

        public void importUserInfo(string sheetName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);

                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                    // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 1000;//每次传输的行数
                    bcp.NotifyAfter = 1000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表

                    bcp.ColumnMappings.Add("用户名", "username");
                    bcp.ColumnMappings.Add("工号", "workId");
                    bcp.ColumnMappings.Add("默认密码", "_password");
                    bcp.ColumnMappings.Add("超级管理员", "super_manager");
                    bcp.ColumnMappings.Add("BGA", "bga");
                    bcp.ColumnMappings.Add("Repair", "repair");
                    bcp.ColumnMappings.Add("Test_all", "test_all");
                    bcp.ColumnMappings.Add("Test1", "test1");
                    bcp.ColumnMappings.Add("Test2", "test2");
                    bcp.ColumnMappings.Add("receive_return", "receive_return");
                    bcp.ColumnMappings.Add("store", "store");

                    bcp.ColumnMappings.Add("outlook", "outlook");
                    bcp.ColumnMappings.Add("running", "running");
                    bcp.ColumnMappings.Add("obe", "obe");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void importGuranteCheck(string sheetName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);

                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                    // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 1000;//每次传输的行数
                    bcp.NotifyAfter = 1000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表


                    bcp.ColumnMappings.Add("导入日期", "import_date");
                    bcp.ColumnMappings.Add("MB_CUST_SN", "MB_CUST_SN");
                    bcp.ColumnMappings.Add("MB_FRU_PN", "MB_FRU_PN");
                    bcp.ColumnMappings.Add("MB_COMPAL_PN", "MB_COMPAL_PN");
                    bcp.ColumnMappings.Add("MB_COMPAL_SN", "MB_COMPAL_SN");
                    bcp.ColumnMappings.Add("SERIAL_NUMBER", "SERIAL_NUMBER");
                    bcp.ColumnMappings.Add("MODEL_NAME", "MODEL_NAME");
                    bcp.ColumnMappings.Add("PRODUCT_DESC", "PRODUCT_DESC");
                    bcp.ColumnMappings.Add("DUE_DATE", "DUE_DATE");
                    bcp.ColumnMappings.Add("系列", "serial");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void importMachineImportCheck(Worksheet ws, int rowLength, int columnLength, string tableName)
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

                    for (int i = 2; i <= rowLength; i++)
                    {
                        string s = "INSERT INTO " + tableName + " VALUES('";
                        for (int j = 1; j <= columnLength; j++)
                        {
                            try
                            {
                                //有可能有空值
                                string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();

                                if (j == 10 || j == 12)//把对应的列转换为时间格式
                                {
                                    temp= DateTime.FromOADate(Convert.ToInt32(temp)).ToString("d");
                                    temp = DateTime.Parse(temp).ToString("yyyy-MM-dd");
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
                closeAndKillApp();
            }
        }

        public void importFrubomCheck(Worksheet ws, int rowLength, int columnLength, string tableName)
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

                    for (int i = 2; i <= rowLength; i++)
                    {
                        string s = "INSERT INTO " + tableName + " VALUES('";
                        for (int j = 1; j <= columnLength; j++)
                        {
                            try
                            {
                                //有可能有空值
                                string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();
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
                closeAndKillApp();
            }

            //TODO 下面的程序有问题，但是不知道什么原因，漏一部分数据

            //DataSet ds = new DataSet();
            //try
            //{
            //    //获取全部数据
            //    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
            //    OleDbConnection conn = new OleDbConnection(strConn);
            //    conn.Open();
            //    string strExcel = "";
            //    OleDbDataAdapter myCommand = null;
            //    strExcel = string.Format("select * from [{0}$]", "FRUBOM对照表");
            //    myCommand = new OleDbDataAdapter(strExcel, strConn);
            //    myCommand.Fill(ds,"FRUBOM对照表");

            //    //用bcp导入数据
            //    using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
            //    {
            //        // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
            //        bcp.BatchSize = 100;//每次传输的行数
            //        bcp.NotifyAfter = 100;//进度提示的行数
            //        bcp.DestinationTableName = tableName;//目标表

            //        bcp.ColumnMappings.Add("厂商", "vendor");
            //        bcp.ColumnMappings.Add("客户别", "product");
            //        bcp.ColumnMappings.Add("客户料号", "custom_material_no");
            //        bcp.ColumnMappings.Add("可替代料号", "replace_material");
            //        bcp.ColumnMappings.Add("机型", "machine_type");
            //        bcp.ColumnMappings.Add("名称", "name");
            //        bcp.ColumnMappings.Add("客户物料描述", "custommaterialdescribe");
            //        bcp.ColumnMappings.Add("厂商料号", "vendor_material_no");
            //        bcp.ColumnMappings.Add("MPN1", "mpn1");
            //        bcp.ColumnMappings.Add("MPN1描述", "mpn1_des");
            //        bcp.ColumnMappings.Add("MPN2", "mpn2");
            //        bcp.ColumnMappings.Add("MPN2描述", "mpn2_des");
            //        bcp.ColumnMappings.Add("MPN3", "mpn3");
            //        bcp.ColumnMappings.Add("MPN3描述", "mpn3_des");
            //        bcp.ColumnMappings.Add("MPN4", "mpn4");
            //        bcp.ColumnMappings.Add("MPN4描述", "mpn4_des");

            //        bcp.ColumnMappings.Add("保修期", "gurantee");
            //        bcp.ColumnMappings.Add("EOL", "eol");
            //        bcp.ColumnMappings.Add("添加日期", "input_date");

            //        bcp.WriteToServer(ds.Tables[0]);
            //        bcp.Close();

            //        conn.Close();
            //        MessageBox.Show("导入完成");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message);
            //}
        }

        public void importMbCheck(string sheetName, string tableName)
        {
            Microsoft.Office.Interop.Excel.Application app;
            Microsoft.Office.Interop.Excel.Workbooks wbs;
            Microsoft.Office.Interop.Excel.Workbook wb;

            app = new Microsoft.Office.Interop.Excel.Application();
            app.DisplayAlerts = false;
            wbs = app.Workbooks;
            wb = wbs.Add(filePath.Text);            
            
            wb = wbs.Open(filePath.Text, 0, false, 5, string.Empty, string.Empty, 
                false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, 
                string.Empty, true, false, 0, true, 1, 0);

                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
                int rowLength = ws.UsedRange.Rows.Count;
                int columnLength = ws.UsedRange.Columns.Count;

            if (checkIsNullCell(ws, rowLength, columnLength))
            {
                MessageBox.Show("主板拦截中有空值，请确认后在上传");
                //关闭excel应用
                try
                {
                    wbs.Close();
                    app.Quit();
                    IntPtr intptr = new IntPtr(app.Hwnd);
                    int id;
                    GetWindowThreadProcessId(intptr, out id);
                    var p = Process.GetProcessById(id);
                    //if (p != null)
                    p.Kill();
                }
                catch (Exception ex)
                { }
                this.importButton.Enabled = true;
                return;
            }
            //关闭excel应用
            try
            {
                wbs.Close();
                app.Quit();
                IntPtr intptr = new IntPtr(app.Hwnd);
                int id;
                GetWindowThreadProcessId(intptr, out id);
                var p = Process.GetProcessById(id);
                //if (p != null)
                p.Kill();
            }
            catch (Exception ex)
            { }

            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);

                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                    // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 1000;//每次传输的行数
                    bcp.NotifyAfter = 1000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表                    			

                    bcp.ColumnMappings.Add("厂商", "vendor");
                    bcp.ColumnMappings.Add("客户别", "product");
                    bcp.ColumnMappings.Add("客户机型", "custom_machine_type");
                    bcp.ColumnMappings.Add("MB简称", "mb_brief");

                    bcp.ColumnMappings.Add("客户料号", "custom_material_no");
                    bcp.ColumnMappings.Add("客户序号", "custom_serial_no");
                    bcp.ColumnMappings.Add("厂商序号", "vendor_serail_no");
                    bcp.ColumnMappings.Add("厂商料号", "vendor_material_no");

                    bcp.ColumnMappings.Add("MB描述", "mb_describe");
                    bcp.ColumnMappings.Add("拦截原因", "check_reason");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void importbadcodesCheck(string sheetName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取全部数据
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath.Text + ";Extended Properties=Excel 12.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = string.Format("select * from [{0}$]", sheetName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, sheetName);

                //用bcp导入数据
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(Constlist.ConStr))
                {
                    // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 1000;//每次传输的行数
                    bcp.NotifyAfter = 1000;//进度提示的行数
                    bcp.DestinationTableName = tableName;//目标表                    			

                    bcp.ColumnMappings.Add("一麦代码", "yimaicode");
                    bcp.ColumnMappings.Add("一麦描述", "yimaides");
                    bcp.ColumnMappings.Add("仁宝代码", "renbaocode");
                    bcp.ColumnMappings.Add("仁宝描述", "renbaodes");

                    bcp.WriteToServer(ds.Tables[0]);
                    bcp.Close();

                    conn.Close();
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void importLCFC_MBBOM(Worksheet ws, int rowLength, int columnLength, string tableName)
        {
            string s ="";
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();
                SqlTransaction transaction = null;


                int dateIndex = -1;
                if (this.mbmaterial.Checked)
                {
                    dateIndex = 32;
                }
                else if (this.receiveOrder.Checked)
                {
                    //自动添加
                }
                else if (this.stock_in_sheetradioButton.Checked)
                {
                    dateIndex = 16;
                }
                else if (this.LCFC_MBBOMradioButton.Checked)
                {
                    dateIndex = 1;
                }
                //else if (this.COMPAL_MBBOMradioButton.Checked)
                //{
                //    dateIndex = 1;
                //}
                else if (this.LCFC71BOMRadioButton.Checked)
                {
                    dateIndex = 1;
                }
                else if (this.DPKradioButton.Checked)
                {
                    dateIndex = 6;
                }

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;  

                    for (int i = 2; i <= rowLength; i++)
                    {
                        s = "INSERT INTO " + tableName + " VALUES('";
                        for (int j = 1; j <= columnLength; j++)
                        {
                            try
                            {                                
                                if (j == dateIndex)//时间所在的列，从1开始，没有是-1
                                {
                                    string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();
                                    DateTime strDate = DateTime.FromOADate(double.Parse(temp));
                                    s += strDate.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                }
                                else
                                {
                                    //有可能有空值
                                    string temp = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, j]).Value2.ToString();
                                    s += temp.Replace('\'', '"');
                                }                                
                               
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

                    transaction.Commit();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                transaction.Dispose();                
                conn.Dispose();

                MessageBox.Show("导入" + tableName + "完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(s + "#"+ex.ToString());
            }
            finally
            {
                wbs.Close();
                closeAndKillApp();
            }
        }

    }
}

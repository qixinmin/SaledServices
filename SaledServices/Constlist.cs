﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Net;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;

namespace SaledServices
{
    public class ChooseStock
    {
        public string Id;
        public string house;
        public string place;
        public string number;
    }

    public class PrepareUseDetail
    {
        public string Id;
        public string mb_brief;
        public string material_mpn;
        public string stock_place;
        public string thisUseNumber;
        public string totalUseNumber;
    }

    public class Constlist
    {
        //public static string ConStr = "server=.;database=SaledService;uid=admin;pwd=admin";
        public static string ConStr = "server=192.168.5.222;database=SaledService;uid=admin;pwd=admin";

        public static string table_MBMaterialCompare = "MB物料对照表";
        public static string table_name_MBMaterialCompare = "MBMaterialCompare";

        public static string table_receiveOrder = "RMA";//sheet 名字
        public static string table_name_ReceiveOrder = "ReceiveOrder";

        public static string table_frureceiveOrder = "FRU收货单";//sheet 名字
        public static string table_name_fruReceiveOrder = "frureceiveOrder";

        public static string table_LCFC_MBBOM = "LCFC_MBBOM";
        public static string table_name_LCFC_MBBOM = "LCFC_MBBOM_table";

        //public static string table_COMPAL_MBBOM = "COMPAL_MBBOM";
        //public static string table_name_COMPAL_MBBOM = "COMPAL_MBBOM_table";

        public static string table_LCFC71BOM = "LCFC71BOM表";
        public static string table_name_LCFC71BOM = "LCFC71BOM_table";

        public static string table_DPK = "DPK";
        public static string table_name_DPK = "DPK_table";

        public static string table_customFault = "故障代码表";
        public static string table_name_customFault = "customFault";

        public static string table_stock_in_sheet = "材料入库单";
        public static string table_name_stock_in_sheet = "stock_in_sheet";

        public static string table_stock_house = "库房";
        public static string table_name_store_house_sheet = "store_house";

        public static string table_stock_ng_house = "不良品库房";
        public static string table_name_store_ng_house_sheet = "store_house_ng";

        public static string table_users = "用户导入";
        public static string table_name_users_sheet = "users";

        public static string table_limit_gurante = "15个月";
        public static string table_limit_gurante_sheet = "limit_gurante";

        public static string table_frubom = "FRUBOM对照表";
        public static string table_name_frubom_sheet = "frubomtable";
    }

    public class PrintUtils
    {
        static LabelManager2.Application labApp = null;
        static LabelManager2.Document doc = null;
        public static void InitCodesoftForReturn()
        {
            try
            {
                labApp = new LabelManager2.Application();
                doc = labApp.ActiveDocument;
                string labFileName = @"D:\printLab\BAR11.Lab";
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：" + labFileName + ",請聯系系統管理員", "溫馨提示");
                    return;
                }             
                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                
                doc.Printer.SwitchTo("codesoft");//打印机名字，自定义，可以修改               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void printCustomMaterialNo(string customMaterial)
        {
            if (labApp == null)
            {
                InitCodesoftForReturn();
            }

            doc.Variables.FormVariables.Item("BAR").Value = customMaterial;
            doc.PrintDocument(); //打印一次
            doc.FormFeed(); //结束打印
        }

        public static void disposePrinter()
        {
            if (doc != null)
            {
                doc.Close();
                labApp.Quit();
            }
        }
    }

    public class ExportExcelContent
    {
        public List<string> contentArray;       
    }

    public class Utils
    {
        public static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    if (AddressIP.StartsWith("192.168.1"))
                    {
                        break;
                    }
                }

            }
            return AddressIP;
        }

        public static bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        } 

        public static bool isTimeError(string nowDate)
        {
            try
            {
                DateTime timeStart = Convert.ToDateTime(nowDate);

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select top 1 receivedate from receiveOrder order by receivedate desc ";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string oldTime = "";
                while (querySdr.Read())
                {
                    oldTime = querySdr[0].ToString();
                }

                querySdr.Close();

                mConn.Close();

                DateTime timeOld = Convert.ToDateTime(oldTime);

                if (DateTime.Compare(timeStart, timeOld) < 0) //判断日期大小
                {
                    MessageBox.Show("当前日期不对，请检查");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           

            return false;
        }

        public static void setValue(ref Microsoft.Office.Interop.Excel.Worksheet xSheet, int column, int row,Object content)
        {
            if(content is ExportExcelContent)
            {
                ExportExcelContent temp = (ExportExcelContent)content;
                xSheet.Cells[column][row] = temp.contentArray[column - 1];
            }
        }

        //title list的长度要保证与内容contentArray的长度一致, 一个文件包含多个sheet的尝试
        public static void createExcelList(List<string> titleList, List<Object> contentList)
        {
            HSSFWorkbook hssfworkbook =new HSSFWorkbook();

            HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet("newsheet");
            int row = contentList.Count +1;
            int column = ((ExportExcelContent)(contentList[0])).contentArray.Count;
         //   int contentListNum = contentList.Count;

            for (int ri = 0; ri < row; ri++)
            {
                sheet.CreateRow(ri);
            }
            for (int ri = 0; ri < row; ri++)
            {
                for (int ci = 0; ci < column; ci++)
                {
                    if (ri == 0)
                    {
                        
                        sheet.GetRow(ri).CreateCell(ci).SetCellValue(titleList[ci]);
                    }
                    else
                    {
                        string content =((ExportExcelContent)(contentList[ri-1])).contentArray[ci];
                        sheet.GetRow(ri).CreateCell(ci).SetCellValue(content);
                    }
                }
            }
             for (int ci = 0; ci < column; ci++)
             {
                 sheet.AutoSizeColumn(ci);
             }
            
            hssfworkbook.CreateSheet("Sheet1");
            //sheet.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");//npoi 的下标从0开始的，原始的从1开始

            //HSSFCell cell = (HSSFCell)sheet.CreateRow(0).CreateCell(0);
            //cell.SetCellValue(new DateTime(2008, 5, 5));

            //set dateformat

            //HSSFCellStyle cellStyle = (HSSFCellStyle)hssfworkbook.CreateCellStyle();

            //HSSFDataFormat format = (HSSFDataFormat)hssfworkbook.CreateDataFormat();

            //cellStyle.DataFormat = format.GetFormat("yyyy年m月d日");

            //cell.CellStyle = cellStyle;

            // Create arow and put some cells in it. Rows are 0 based.
            //HSSFRow row = (HSSFRow)sheet.CreateRow(1);
            //// Create acell and put a value in it.
            //cell = (HSSFCell)row.CreateCell(1);
            //// Style thecell with borders all around.
            //HSSFCellStyle style = (HSSFCellStyle)hssfworkbook.CreateCellStyle();

           

          //  cell.CellStyle = style;

            HSSFSheet sheet2 = (HSSFSheet)hssfworkbook.CreateSheet("Sheet2");

            for (int ri = 0; ri < row; ri++)
            {
                sheet2.CreateRow(ri);
            }
            for (int ri = 0; ri < row; ri++)
            {
                for (int ci = 0; ci < column; ci++)
                {
                    if (ri == 0)
                    {

                        sheet2.GetRow(ri).CreateCell(ci).SetCellValue(titleList[ci]);
                    }
                    else
                    {
                        string content = ((ExportExcelContent)(contentList[ri - 1])).contentArray[ci];
                        sheet2.GetRow(ri).CreateCell(ci).SetCellValue(content);
                    }
                }
            }
            for (int ci = 0; ci < column; ci++)
            {
                sheet2.AutoSizeColumn(ci);
            }
            
           // sheet2.CreateRow(0).CreateCell(0).SetCellValue("This2 is a Sample");//npoi 的下标从0开始的，原始的从1开始
            HSSFSheet sheet3 = (HSSFSheet)hssfworkbook.CreateSheet("Sheet3");
            sheet3.CreateRow(0).CreateCell(0).SetCellValue("This3 is a Sample");//npoi 的下标从0开始的，原始的从1开始
            FileStream file = new FileStream(@"D:\test.xls", FileMode.Create);

            hssfworkbook.Write(file);

            file.Close();
        }

        //title list的长度要保证与内容contentArray的长度一致
        public static void createExcel(string filepathname, List<string> titleList, List<Object> contentList)
        {
            //1.创建Applicaton对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook;

            xApp.Visible = false;
            xApp.DisplayAlerts = false;  

            if (File.Exists(filepathname))
            {
                try
                {
                    File.Delete(filepathname);
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("删除文件错误");
                }
                
               // workBook = xApp.Workbooks.Open(filepathname, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            
            workBook = xApp.Workbooks.Add(true);

            //3.指定要操作的Sheet
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];
            xSheet.Name = "test";//名称读取或修改
            Microsoft.Office.Interop.Excel.Range range;

            //4.向相应对位置写入相应的数据
            for (int column = 1; column <= titleList.Count; column++)
            {
                xSheet.Cells[column][1] = titleList[column - 1];
            }            
           
            object[,] saRet = new object[contentList.Count , titleList.Count]; //row,column

            for (int row = 0; row < contentList.Count ; row++)
            {
                for (int column = 0; column < titleList.Count; column++)
                {
                    saRet[row, column] = ((ExportExcelContent)contentList[row]).contentArray[column];
                }
            }

            //for (int row = 2; row <= contentList.Count + 1; row++)           
            //{
            //    for (int column = 1; column <= titleList.Count; column++)
            //    {
            //        setValue(ref xSheet,column, row, contentList[row - 2]);
            //    }
            //}

            //range = xSheet.get_Range(xSheet.Cells[2, 1], xSheet.Cells[contentList.Count+1, titleList.Count]);//此方案有问题，改成下面的方案range[,]方案

            range = xSheet.Range[xSheet.Cells[2, 1], xSheet.Cells[contentList.Count + 1, titleList.Count]];

           
           
            range.NumberFormatLocal = "@";
            range.Value2 = saRet;

            xSheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //5.保存保存WorkBook
            workBook.SaveAs(filepathname);
            //6.从内存中关闭Excel对象
            xSheet = null;
            //关闭EXCEL的提示框
            xApp.DisplayAlerts = false;
            //Excel从内存中退出        
            try
            {
                workBook.Close();
                xApp.Quit();
                IntPtr intptr = new IntPtr(xApp.Hwnd);
                int id;
                GetWindowThreadProcessId(intptr, out id);
                var p = Process.GetProcessById(id);
                if (p != null)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            { }
            MessageBox.Show("导出" + filepathname + "成功！");
        }

        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        public static string getCustomDate(string inputDate)
        {
            return DateTime.Parse(inputDate).ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string getCustomCurrentDate()
        {
            return DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }


        public static  bool in90Days(string burn_date)
        {
            DateTime dt1 = Convert.ToDateTime(burn_date);
            DateTime dt2 = DateTime.Now;

            TimeSpan ts = dt2.Subtract(dt1);
            int overdays = ts.Days;
            if (overdays > 90)
            {
                return false;
            }
            return true;
        }

        public static void createFile(string path, string filename, string content)
        {
            try
            {
                FileStream fs1 = new FileStream(path+filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(content);
                sw.Close();
                fs1.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
            }
        }

        public static void deleteFile(string path, string filename)
        {
            try
            {
                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //规则是0-9，a-z 累加， a =10
        public static string getTimeByChar(bool isYear, char ch)
        {
            ch = Char.ToLower(ch);
            string ret = ch.ToString() ;
            
            switch (ch)
            {
                case '0':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "0";
                    }
                    break;
                case '1':
                    if (isYear)
                    {
                        ret = "21";
                    }
                    else
                    {
                        ret = "1";
                    }
                    break;
                case '2':
                    if (isYear)
                    {
                        ret = "22";
                    }
                    else
                    {
                        ret = "2";
                    }
                    break;
                case '3':
                    if (isYear)
                    {
                        ret = "23";
                    }
                    else
                    {
                        ret = "3";
                    }
                    break;
                case '4':
                    if (isYear)
                    {
                        ret = "24";
                    }
                    else
                    {
                        ret = "4";
                    }
                    break;
                case '5':
                    if (isYear)
                    {
                        ret = "15";
                    }
                    else
                    {
                        ret = "5";
                    }
                    break;
                case '6':
                    if (isYear)
                    {
                        ret = "16";
                    }
                    else
                    {
                        ret = "6";
                    }
                    break;
                case '7':
                    if (isYear)
                    {
                        ret = "17";
                    }
                    else
                    {
                        ret = "7";
                    }
                    break;
                case '8':
                    if (isYear)
                    {
                        ret = "18";
                    }
                    else
                    {
                        ret = "8";
                    }
                    break;
                case '9':
                    if (isYear)
                    {
                        ret = "19";
                    }
                    else
                    {
                        ret = "9";
                    }
                    break;
                case 'a':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "10";
                    }
                    break;
                case 'b':
                    ret = "11";
                    break;
                case 'c':
                    ret = "12";
                    break;
                case 'd':
                    ret = "13";
                    break;
                case 'e':
                    ret = "14";
                    break;
                case 'f':
                    ret = "15";
                    break;
                case 'g':
                    ret = "16";
                    break;
                case 'h':
                    ret = "17";
                    break;
                //case 'i':
                //    ret = "18";
                //    break;
                case 'j':
                    ret = "18";
                    break;
                case 'k':
                    ret = "19";
                    break;
                case 'l':
                    ret = "20";
                    break;
                case 'm':
                    ret = "21";
                    break;
                case 'n':
                    ret = "22";
                    break;
                //case 'o':
                //    ret = "24";
                //    break;
                case 'p':
                    ret = "23";
                    break;
                //case 'q':
                //    ret = "24";
                //    break;
                case 'r':
                    ret = "24";
                    break;
                case 's':
                    ret = "25";
                    break;
                case 't':
                    ret = "26";
                    break;
                //case 'u':
                //    ret = "28";
                //    break;
                case 'v':
                    ret = "27";
                    break;
                case 'w':
                    ret = "28";
                    break;
                case 'x':
                    ret = "29";
                    break;
                case 'y':
                    ret = "30";
                    break;
                case 'z':
                    ret = "31";
                    break;
            }

            return ret;
        }

        //规则是0-9，a-z 累加， a =10
        public static string getTimeByCharCompal(bool isYear, char ch)
        {
            ch = Char.ToLower(ch);
            string ret = ch.ToString();

            switch (ch)
            {
                case '0':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "0";
                    }
                    break;
                case '1':
                    if (isYear)
                    {
                        ret = "21";
                    }
                    else
                    {
                        ret = "1";
                    }
                    break;
                case '2':
                    if (isYear)
                    {
                        ret = "22";
                    }
                    else
                    {
                        ret = "2";
                    }
                    break;
                case '3':
                    if (isYear)
                    {
                        ret = "23";
                    }
                    else
                    {
                        ret = "3";
                    }
                    break;
                case '4':
                    if (isYear)
                    {
                        ret = "24";
                    }
                    else
                    {
                        ret = "4";
                    }
                    break;
                case '5':
                    if (isYear)
                    {
                        ret = "15";
                    }
                    else
                    {
                        ret = "5";
                    }
                    break;
                case '6':
                    if (isYear)
                    {
                        ret = "16";
                    }
                    else
                    {
                        ret = "6";
                    }
                    break;
                case '7':
                    if (isYear)
                    {
                        ret = "17";
                    }
                    else
                    {
                        ret = "7";
                    }
                    break;
                case '8':
                    if (isYear)
                    {
                        ret = "18";
                    }
                    else
                    {
                        ret = "8";
                    }
                    break;
                case '9':
                    if (isYear)
                    {
                        ret = "19";
                    }
                    else
                    {
                        ret = "9";
                    }
                    break;
                case 'a':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "10";
                    }
                    break;
                case 'b':
                    ret = "11";
                    break;
                case 'c':
                    ret = "12";
                    break;
                case 'd':
                    ret = "13";
                    break;
                case 'e':
                    ret = "14";
                    break;
                case 'f':
                    ret = "15";
                    break;
                case 'g':
                    ret = "16";
                    break;
                case 'h':
                    ret = "17";
                    break;
                //case 'i':
                //    ret = "18";
                //    break;
                case 'j':
                    ret = "18";
                    break;
                case 'k':
                    ret = "19";
                    break;
                case 'l':
                    ret = "20";
                    break;
                case 'm':
                    ret = "21";
                    break;
                case 'n':
                    ret = "22";
                    break;
                //case 'o':
                //    ret = "24";
                //    break;
                case 'p':
                    ret = "23";
                    break;
                case 'q':
                    ret = "24";
                    break;
                case 'r':
                    ret = "25";
                    break;
                case 's':
                    ret = "26";
                    break;
                case 't':
                    ret = "27";
                    break;
                case 'u':
                    ret = "28";
                    break;
                case 'v':
                    ret = "29";
                    break;
                case 'w':
                    ret = "30";
                    break;
                case 'x':
                    ret = "31";
                    break;
                case 'y':
                    ret = "31";
                    break;
                case 'z':
                    ret = "31";
                    break;
            }

            return ret;
        }
    }
}

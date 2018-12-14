using System;
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
using System.Globalization;
using SaledServices.Export;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;

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

        public static string table_name_frubomtable = "frubomtable";

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

        public static string table_badcodes = "不良代码对照表";
        public static string table_name_badcodes = "badcodes";
    }

    public class PrintUtils
    {
        static LabelManager2.Application labApp = null;
        static LabelManager2.Document doc = null;

        public static void initCodeSoft()
        {
            try
            {
                labApp = new LabelManager2.Application();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void print8sCode(string _8sCode)
        {
            if (labApp == null)
            {
                initCodeSoft();
            }

            try
            {
                string labFileName = @"D:\printLab\BAR11.Lab";
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：" + labFileName + ",請聯系系統管理員", "溫馨提示");
                    return;
                }
                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }
                doc = labApp.ActiveDocument;
                doc.Printer.SwitchTo("codesoft");//打印机名字，自定义，可以修改

                doc.Variables.FormVariables.Item("BAR").Value = _8sCode;
                doc.PrintDocument(); //打印一次
                doc.FormFeed(); //结束打印
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void printMac(string mac)
        {
            if (labApp == null)
            {
                initCodeSoft();
            }

             try
            {
                string labFileName = @"D:\printLab\IBM_MAC.lab";
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：" + labFileName + ",請聯系系統管理員", "溫馨提示");
                    return;
                }
                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }
                doc = labApp.ActiveDocument;
                doc.Printer.SwitchTo("codesoft");//打印机名字，自定义，可以修改

                doc.Variables.FormVariables.Item("MAC").Value = mac;
                doc.PrintDocument(); //打印一次
                doc.FormFeed(); //结束打印
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void printCustomMaterial(string filename, string customMaterial)
        {
            if (labApp == null)
            {
                initCodeSoft();
            }

            try
            {
                string labFileName = @"D:\printLab\"+filename;
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：" + labFileName + ",請聯系系統管理員", "溫馨提示");
                    return;
                }
                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }
                doc = labApp.ActiveDocument;
                doc.Printer.SwitchTo("codesoft");//打印机名字，自定义，可以修改

                doc.Variables.FormVariables.Item("BAR").Value = customMaterial;
                doc.PrintDocument(); //打印一次
                doc.FormFeed(); //结束打印
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        public static void disposePrinter()
        {
            if (doc != null)
            {
                doc.Close();
                doc = null;
               // labApp.Quit();
            }
            if (labApp != null)
            {
                labApp.Quit();
                labApp = null;
            }
        }
    }

    public class ExportExcelContent
    {
        public List<string> contentArray;       
    }

    public class Utils
    {

        public static string getFirstDayOfMonth(DateTime current)
        {
            DateTime dt_First = current.AddDays(1 - (current.Day));

            return dt_First.ToString("yyyy-MM-dd");
        }

        public static string getEndDayOfMonth(DateTime current)
        {
            DateTime dt_First = current.AddDays(1 - (current.Day));
            int year = current.Date.Year;
            int month = current.Date.Month;
            int dayCount = DateTime.DaysInMonth(year, month);
            DateTime dt_Last = dt_First.AddDays(dayCount - 1);
            return dt_Last.ToString("yyyy-MM-dd");
        }

        public static string getColumnName(int month)
        {
            return "sum_" + month;
        }

        public static string modifyDataFormat(string date)
        {
            return (date != null && date != "") ? date.Substring(0, date.IndexOf(" ")).Trim() : "";
        }
        public static string GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            string retWeek = "";
            if (weekOfYear < 10)
            {
                retWeek = "0" + weekOfYear;
            }
            retWeek = "" + weekOfYear;
            return retWeek;
        }

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

        public static bool IsNum(string input)
        {
            string pattern = @"^[0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
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

        public static void createMulitSheetsUsingNPOI(string filepathname, List<allContent> allcontentList)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            //内容表格
            foreach (allContent temp in allcontentList)
            {
                if (temp.contentList.Count <= 0)
                {
                    continue;
                }
                HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet(temp.sheetName);
                int row = temp.contentList.Count + 1;
                int column = ((ExportExcelContent)(temp.contentList[0])).contentArray.Count;

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
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(temp.titleList[ci]);
                        }
                        else
                        {
                            string content = ((ExportExcelContent)(temp.contentList[ri - 1])).contentArray[ci];
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(content);
                        }
                    }
                }
                for (int ci = 0; ci < column; ci++)
                {
                    sheet.AutoSizeColumn(ci);
                }
            }
          
            FileStream file = new FileStream(filepathname, FileMode.Create);

            hssfworkbook.Write(file);
            file.Close();
            MessageBox.Show(filepathname + "导出成功");
        }


        //title list的长度要保证与内容contentArray的长度一致, 一个文件包含多个sheet的尝试
        public static void createExcelListUsingNPOI(string filepathname, debitnotsSheet3 debitnots, List<allContent> allcontentList)
        {
            HSSFWorkbook hssfworkbook =new HSSFWorkbook();
            
            //首页sheet
            HSSFSheet firstsheet = (HSSFSheet)hssfworkbook.CreateSheet("Debit NOTS");
            for (int i = 0; i <= 30; i++)
            {
                firstsheet.CreateRow(i);
            }

            firstsheet.GetRow(0).CreateCell(2).SetCellValue(debitnots.titleC1);

            HSSFCellStyle cellStyletitle = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            IFont titlefont = hssfworkbook.CreateFont();
            titlefont.FontHeightInPoints = 20;
            titlefont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            titlefont.FontName = "宋体";
            cellStyletitle.SetFont(titlefont);   
            firstsheet.GetRow(0).GetCell(2).CellStyle = cellStyletitle;

            firstsheet.GetRow(1).CreateCell(0).SetCellValue(debitnots.addressA2);
            firstsheet.GetRow(2).CreateCell(0).SetCellValue(debitnots.telA3);
            firstsheet.GetRow(4).CreateCell(0).SetCellValue(debitnots.billto1A5);
            firstsheet.GetRow(5).CreateCell(0).SetCellValue(debitnots.billto2A6);
            firstsheet.GetRow(6).CreateCell(0).SetCellValue(debitnots.billto3A7);
            firstsheet.GetRow(7).CreateCell(0).SetCellValue(debitnots.billto4A8);

            firstsheet.GetRow(8).CreateCell(0).SetCellValue(debitnots.telA9);
            firstsheet.GetRow(9).CreateCell(0).SetCellValue(debitnots.faxA10);

            firstsheet.GetRow(4).CreateCell(5).SetCellValue(debitnots.dateF5);
            firstsheet.GetRow(4).CreateCell(6).SetCellValue(debitnots.dateG5);
            firstsheet.GetRow(5).CreateCell(5).SetCellValue(debitnots.invF6);
            HSSFCellStyle cellStylesub = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            IFont subfont = hssfworkbook.CreateFont();
            subfont.FontHeightInPoints = 9;
            subfont.FontName = "宋体";
            cellStylesub.SetFont(subfont);          
            firstsheet.GetRow(1).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(2).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(4).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(5).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(6).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(7).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(8).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(9).GetCell(0).CellStyle = cellStylesub;
            firstsheet.GetRow(4).GetCell(5).CellStyle = cellStylesub;
            firstsheet.GetRow(4).GetCell(6).CellStyle = cellStylesub;
            firstsheet.GetRow(5).GetCell(5).CellStyle = cellStylesub;


            HSSFCellStyle cellStyleline = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            cellStyleline.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thick;//A11-G11,A26-G26
            firstsheet.GetRow(10).CreateCell(0);
            firstsheet.GetRow(10).CreateCell(1);
            firstsheet.GetRow(10).CreateCell(2);
            firstsheet.GetRow(10).CreateCell(3);
            firstsheet.GetRow(10).CreateCell(4);
            firstsheet.GetRow(10).CreateCell(5);
            firstsheet.GetRow(10).CreateCell(6);

            firstsheet.GetRow(10).GetCell(0).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(1).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(2).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(3).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(4).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(5).CellStyle = cellStyleline;
            firstsheet.GetRow(10).GetCell(6).CellStyle = cellStyleline;

            firstsheet.GetRow(25).CreateCell(0);
            firstsheet.GetRow(25).CreateCell(1);
            firstsheet.GetRow(25).CreateCell(2);
            firstsheet.GetRow(25).CreateCell(3);
            firstsheet.GetRow(25).CreateCell(4);
            firstsheet.GetRow(25).CreateCell(5);
            firstsheet.GetRow(25).CreateCell(6);

            firstsheet.GetRow(25).GetCell(0).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(1).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(2).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(3).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(4).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(5).CellStyle = cellStyleline;
            firstsheet.GetRow(25).GetCell(6).CellStyle = cellStyleline;

            firstsheet.GetRow(13).CreateCell(0).SetCellValue(debitnots.contentA14);
            firstsheet.GetRow(14).CreateCell(2).SetCellValue(debitnots.contentC14);
            firstsheet.GetRow(13).CreateCell(3).SetCellValue(debitnots.contentD14);
            firstsheet.GetRow(13).CreateCell(4).SetCellValue(debitnots.contentE14);
            firstsheet.GetRow(14).CreateCell(3).SetCellValue(debitnots.contentD15);
            firstsheet.GetRow(14).CreateCell(4).SetCellValue(debitnots.contentE15);

            firstsheet.GetRow(15).CreateCell(1).SetCellValue(debitnots.contentB16);
            firstsheet.GetRow(15).CreateCell(3).SetCellValue(debitnots.contentD16);
            firstsheet.GetRow(15).CreateCell(4).SetCellValue(debitnots.contentE16);
            firstsheet.GetRow(15).CreateCell(5).SetCellValue(debitnots.contentF16);

            firstsheet.GetRow(16).CreateCell(1).SetCellValue(debitnots.contentB17);
            firstsheet.GetRow(16).CreateCell(3).SetCellValue(debitnots.contentD17);
            firstsheet.GetRow(16).CreateCell(4).SetCellValue(debitnots.contentE17);
            firstsheet.GetRow(16).CreateCell(5).SetCellValue(debitnots.contentF17);

            firstsheet.GetRow(19).CreateCell(1).SetCellValue(debitnots.contentB20);
            firstsheet.GetRow(19).CreateCell(3).SetCellValue(debitnots.contentD20);
            firstsheet.GetRow(19).CreateCell(4).SetCellValue(debitnots.contentE20);
            firstsheet.GetRow(19).CreateCell(5).SetCellValue(debitnots.contentF20);

            firstsheet.GetRow(21).CreateCell(4).SetCellValue(debitnots.contentE22);
            firstsheet.GetRow(21).CreateCell(5).SetCellValue(debitnots.contentF22);

            firstsheet.GetRow(23).CreateCell(1).SetCellValue(debitnots.contentB24);
            firstsheet.GetRow(23).CreateCell(4).SetCellValue(debitnots.contentE24);
            firstsheet.GetRow(23).CreateCell(5).SetCellValue(debitnots.contentF24);

            firstsheet.GetRow(24).CreateCell(1).SetCellValue(debitnots.contentB25);
            firstsheet.GetRow(24).CreateCell(4).SetCellValue(debitnots.contentE25);
            firstsheet.GetRow(24).CreateCell(5).SetCellValue(debitnots.contentF25);

            firstsheet.GetRow(26).CreateCell(4).SetCellValue(debitnots.contentE27);
            firstsheet.GetRow(26).CreateCell(5).SetCellValue(debitnots.contentF27);

            firstsheet.GetRow(29).CreateCell(4).SetCellValue(debitnots.contentE30);
            firstsheet.GetRow(30).CreateCell(4).SetCellValue(debitnots.contentE31);

            HSSFCellStyle cellStylecontent = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            IFont contentfont = hssfworkbook.CreateFont();
            contentfont.FontHeightInPoints = 12;
            contentfont.FontName = "宋体";
            cellStylecontent.SetFont(contentfont);
            
            firstsheet.GetRow(13).GetCell(0).CellStyle = cellStylecontent;
            firstsheet.GetRow(14).GetCell(2).CellStyle = cellStylecontent;
            firstsheet.GetRow(13).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(13).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(14).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(14).GetCell(4).CellStyle = cellStylecontent;

            firstsheet.GetRow(15).GetCell(1).CellStyle = cellStylecontent;
            firstsheet.GetRow(15).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(15).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(15).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(16).GetCell(1).CellStyle = cellStylecontent;
            firstsheet.GetRow(16).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(16).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(16).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(19).GetCell(1).CellStyle = cellStylecontent;
            firstsheet.GetRow(19).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(19).GetCell(3).CellStyle = cellStylecontent;
            firstsheet.GetRow(19).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(21).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(21).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(23).GetCell(1).CellStyle = cellStylecontent;
            firstsheet.GetRow(23).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(23).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(24).GetCell(1).CellStyle = cellStylecontent;
            firstsheet.GetRow(24).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(24).GetCell(5).CellStyle = cellStylecontent;

            //firstsheet.GetRow(26).GetCell(4).CellStyle = cellStylecontent;
            //firstsheet.GetRow(26).GetCell(5).CellStyle = cellStylecontent;

            firstsheet.GetRow(29).GetCell(4).CellStyle = cellStylecontent;
            firstsheet.GetRow(30).GetCell(4).CellStyle = cellStylecontent;

            //HSSFCellStyle cellStyleback = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            //cellStyleback.FillBackgroundColor = HSSFColor.Yellow.Index;
            //cellStyleback.FillForegroundColor = HSSFColor.Yellow.Index;
            //cellStyleback.SetFont(contentfont);
            //firstsheet.GetRow(26).GetCell(5).CellStyle = cellStyleback;
            //firstsheet.GetRow(26).GetCell(4).CellStyle = cellStyleback;

            //其他内容表格
            foreach (allContent temp in allcontentList)
            {
                HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet(temp.sheetName);
                int row = temp.contentList.Count + 1;
                int column = ((ExportExcelContent)(temp.contentList[0])).contentArray.Count;

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
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(temp.titleList[ci]);
                        }
                        else
                        {
                            string content = ((ExportExcelContent)(temp.contentList[ri - 1])).contentArray[ci];
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(content);
                        }
                    }
                }
                for (int ci = 0; ci < column; ci++)
                {
                    sheet.AutoSizeColumn(ci);
                }
            }
            
            //hssfworkbook.CreateSheet("Sheet1");
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
            
            //sheet2.CreateRow(0).CreateCell(0).SetCellValue("This2 is a Sample");//npoi 的下标从0开始的，原始的从1开始
            //HSSFSheet sheet3 = (HSSFSheet)hssfworkbook.CreateSheet("Sheet3");
            //sheet3.CreateRow(0).CreateCell(0).SetCellValue("This3 is a Sample");//npoi 的下标从0开始的，原始的从1开始
            FileStream file = new FileStream(filepathname, FileMode.Create);

            hssfworkbook.Write(file);
            file.Close();
            MessageBox.Show(filepathname+"导出成功");
        }

        //title list的长度要保证与内容contentArray的长度一致
        public static void createExcel(string filepathname, List<string> titleList, List<Object> contentList)
        {
            if (contentList.Count == 0)
            {
                MessageBox.Show("查询到的内容为空，请检查！");
                return;
            }
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
                StreamWriter sw = new StreamWriter(fs1,Encoding.Default);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;

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
        public static string ConStr = "server=192.168.8.56;database=SaledService;uid=admin;pwd=admin";

        public static string table_MBMaterialCompare = "MB物料对照表";
        public static string table_name_MBMaterialCompare = "MBMaterialCompare";

        public static string table_receiveOrder = "RMA";//sheet 名字
        public static string table_name_ReceiveOrder = "ReceiveOrder";

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


        public static string table_users = "用户导入";
        public static string table_name_users_sheet = "users";
    }

    public class Untils
    {
        public static void InitCodesoftForReturn()
        {

            LabelManager2.Application labApp = new LabelManager2.Application();
            LabelManager2.Document doc = null;
            //string labFileName = System.Windows.Forms.Application.StartupPath + @"\barJapLab.Lab";
            string labFileName = @"D:\维修系统\测试专用\test.Lab";
            try
            {
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：barJapLab.Lab,請聯系系統管理員", "溫馨提示");
                    return;
                }
                labApp = new LabelManager2.Application();
                
                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                doc = labApp.ActiveDocument;
                doc.Printer.SwitchTo("a");//打印机名字，自定义，可以修改
                
                doc.Variables.FormVariables.Item("hbj3").Value = "ABCDEFG";
                doc.Variables.FormVariables.Item("hbj1").Value = "HIJKLMN";
                doc.Variables.FormVariables.Item("hbj2").Value = "OPQRSTU";

                doc.PrintDocument(); //打印一次
                doc.FormFeed(); //结束打印
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }
        public static void createOpeningStockXML(OpeningStockClass openingStockClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_INIT\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", openingStockClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", openingStockClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", openingStockClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", openingStockClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", openingStockClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", openingStockClass.status);
            xmlResult.Append("</TRANSMIT>");

            foreach (StoreInit storeInit in openingStockClass.storeInitList)
            {
                xmlResult.Append("<STORE_INIT>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeInit.ems_no);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeInit.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeInit.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeInit.unit);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeInit.goods_nature);
                xmlResult.AppendFormat("<BOM_VERSION>{0}</BOM_VERSION>\n", storeInit.bom_version);
                xmlResult.AppendFormat("<CHECK_DATE>{0}</CHECK_DATE>\n", storeInit.check_date);
                xmlResult.AppendFormat("<DATA_TYPE>{0}</DATA_TYPE>\n", storeInit.date_type);

                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeInit.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeInit.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeInit.note);
                xmlResult.Append("</STORE_INIT>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");            

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }        
        }

        public static void createRealStockXML(RealStockClass realStockClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_AMOUNT\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", realStockClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", realStockClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", realStockClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", realStockClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", realStockClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", realStockClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (StoreAmount storeInit in realStockClass.storeAmountList)
            {
                xmlResult.Append("<STORE_AMOUNT>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeInit.ems_no);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeInit.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeInit.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeInit.unit);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeInit.goods_nature);
                xmlResult.AppendFormat("<BOM_VERSION>{0}</BOM_VERSION>\n", storeInit.bom_version);
                xmlResult.AppendFormat("<CHECK_DATE>{0}</CHECK_DATE>\n", storeInit.check_date);
                xmlResult.AppendFormat("<DATA_TYPE>{0}</DATA_TYPE>\n", storeInit.date_type);

                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeInit.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeInit.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeInit.note);
                xmlResult.Append("</STORE_AMOUNT>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createStockInOutXML(StockInOutClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_TRANS\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (StoreTrans storeTrans in stockInOutClass.storeTransList)
            {
                xmlResult.Append("<STORE_TRANS>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.ems_no);
                xmlResult.AppendFormat("<IO_NO>{0}</IO_NO>\n", storeTrans.io_no);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<IO_DATE>{0}</IO_DATE>\n", storeTrans.io_date);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.unit);
                xmlResult.AppendFormat("<TYPE>{0}</TYPE>\n", storeTrans.type);
                xmlResult.AppendFormat("<CHK_CODE>{0}</CHK_CODE>\n", storeTrans.chk_code);
                xmlResult.AppendFormat("<ENTRY_ID>{0}</ENTRY_ID>\n", storeTrans.entry_id);
                xmlResult.AppendFormat("<GATEJOB_NO>{0}</GATEJOB_NO>\n", storeTrans.gatejob_no);
                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeTrans.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeTrans.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeTrans.note);
                xmlResult.Append("</STORE_TRANS>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createWorkListHeadXML(WorkListHeadClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.WORK_ORDER_HEAD\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (WorkOrderHead storeTrans in stockInOutClass.workOrderHeadList)
            {
                xmlResult.Append("<WORK_ORDER_HEAD>\n");

                xmlResult.AppendFormat("<WO_NO>{0}</WO_NO>\n", storeTrans.wo_no);
                xmlResult.AppendFormat("<WO_DATE>{0}</WO_DATE>\n", storeTrans.wo_date);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE> \n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.qty);
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.emo_no);

                xmlResult.Append("</WORK_ORDER_HEAD>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createWorkListBodyXML(WorkListBodyClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.WORK_ORDER_LIST\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (WorkOrderList storeTrans in stockInOutClass.workOrderList)
            {
                xmlResult.Append("<WORK_ORDER_LIST>\n");

                xmlResult.AppendFormat("<WO_NO>{0}</WO_NO>\n", storeTrans.wo_no);
                xmlResult.AppendFormat("<TAKE_DATE>{0}</TAKE_DATE>\n", storeTrans.take_date);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE> \n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.qty);
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.emo_no);

                xmlResult.Append("</WORK_ORDER_LIST>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
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
                    ret = "32";
                    break;
                case 'z':
                    ret = "33";
                    break;
            }

            return ret;
        }
    }
}

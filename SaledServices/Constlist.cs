using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices
{
    public class Constlist
    {
        public static string ConStr = "server=.;database=SaledService;uid=admin;pwd=admin";

        public static string table_MBMaterialCompare = "MB物料对照表";
        public static string table_name_MBMaterialCompare = "MBMaterialCompare";

        public static string table_receiveOrder = "RMA";//sheet 名字
        public static string table_name_ReceiveOrder = "ReceiveOrder";

        public static string table_LCFC_MBBOM = "LCFC_MBBOM";
        public static string table_name_LCFC_MBBOM = "LCFC_MBBOM_table";

        public static string table_COMPAL_MBBOM = "COMPAL_MBBOM";
        public static string table_name_COMPAL_MBBOM = "COMPAL_MBBOM_table";

        public static string table_LCFC71BOM = "LCFC71BOM表";
        public static string table_name_LCFC71BOM = "LCFC71BOM_table";

        public static string table_DPK = "DPK";
        public static string table_name_DPK = "DPK_table";

        public static string table_customFault = "故障代码表";
        public static string table_name_customFault = "customFault";
    }

    //订单状态
    public enum OrderStatus 
    {
        ORDER_OPEN,
        ORDER_CLOSE
    }

    public class Untils
    {
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
                case 'i':
                    ret = "18";
                    break;
                case 'j':
                    ret = "19";
                    break;
                case 'k':
                    ret = "20";
                    break;
                case 'l':
                    ret = "21";
                    break;
                case 'm':
                    ret = "22";
                    break;
                case 'n':
                    ret = "23";
                    break;
                case 'o':
                    ret = "24";
                    break;
                case 'p':
                    ret = "25";
                    break;
                case 'q':
                    ret = "26";
                    break;
                case 'r':
                    ret = "27";
                    break;
                case 's':
                    ret = "28";
                    break;
                case 't':
                    ret = "29";
                    break;
                case 'u':
                    ret = "30";
                    break;
                case 'v':
                    ret = "31";
                    break;
                case 'w':
                    ret = "32";
                    break;
                case 'x':
                    ret = "33";
                    break;
                case 'y':
                    ret = "34";
                    break;
                case 'z':
                    ret = "35";
                    break;
            }

            return ret;
        }
    }
}

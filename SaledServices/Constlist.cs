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

        public static string table_receiveOrder = "收货单";
        public static string table_name_ReceiveOrder = "ReceiveOrder";

    }



    //订单状态
    public enum OrderStatus 
    {
        ORDER_OPEN,
        ORDER_CLOSE
    }
}

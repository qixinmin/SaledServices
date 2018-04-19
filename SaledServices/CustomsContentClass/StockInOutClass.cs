using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices.CustomsContentClass
{
    public class StoreTrans
    {
        public string ems_no;
        public string io_no;
        public string goods_nature;
        
        public string io_date;

        public string cop_g_no;
        public string qty;
        public string unit;
        public string type;
        public string chk_code;
        public string entry_id;
        public string gatejob_no;
        public string whs_code;
        public string location_code;
        public string note;
    }

    public class StockInOutClass
    {
        public string seq_no;
        public string boxtype;
        public string flowstateg;
        public string trade_code;
        public string ems_no;
        public string status;

        public List<StoreTrans> storeTransList;
    }
}

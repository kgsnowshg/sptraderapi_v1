using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spapiApp
{
    class time
    {
        public static int hour()
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            return hour;
        }
        public static int min()
        {
            DateTime dt = DateTime.Now;
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            return min;
        }
        public static int sec()
        {
            DateTime dt = DateTime.Now;
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            return sec;
        }
    }
}

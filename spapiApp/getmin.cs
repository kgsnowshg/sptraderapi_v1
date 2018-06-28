using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;//IsoDateTimeConverter
using System.IO;
namespace spapiApp
{
    class getmin
    {
        public static int[] min1(int hour, int min, int sec)
        {
            if (min < 0)
            {
                hour = hour - 1;
                min = min+60;
                if (hour == 12)
                {
                    hour = hour - 1;
                }
                if (hour == 8)
                {
                    hour = 9;
                    min = 0;
                }
            }
            string jsonStrings = File.ReadAllText("json/hsi.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken low = abc[hour.ToString()][min]["low"];
            JToken high = abc[hour.ToString()][min]["high"];
            JToken qty = abc[hour.ToString()][min]["qty"];
            JToken now = abc[hour.ToString()][min]["now"];
            JToken areahigh = abc[hour.ToString()][min]["areahigh"];
            JToken arealow = abc[hour.ToString()][min]["arealow"];
            JToken areanow = abc[hour.ToString()][min]["areanow"];
            int ilow = Convert.ToInt32(low);
            int ihigh = Convert.ToInt32(high);
            int iqty = Convert.ToInt32(qty);
            int inow = Convert.ToInt32(now);
            int iareahigh = Convert.ToInt32(areahigh);
            int iarealow = Convert.ToInt32(arealow);
            int iareanow = Convert.ToInt32(areanow);
            int[] array = new int[7];
            array[0] = inow;
            array[1] = ihigh;
            array[2] = ilow;
            array[3] = iqty;
            array[4] = iareahigh;
            array[5] = iarealow;
            array[6] = iareanow;
            return array;
        }

        public static int[] min2(int hour, int min, int sec)
        {
            if (min < 0)
            {
                hour = hour - 1;
                min = min + 60;
                if (hour == 12)
                {
                    hour = hour - 1;
                }
                if (hour == 8)
                {
                    hour = 9;
                    min = 0;
                }
            }
            string jsonStrings = File.ReadAllText("json/hsia.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken low = abc[hour.ToString()][min]["low"];
            JToken high = abc[hour.ToString()][min]["high"];
            JToken qty = abc[hour.ToString()][min]["qty"];
            JToken now = abc[hour.ToString()][min]["now"];
            int ilow = Convert.ToInt32(low);
            int ihigh = Convert.ToInt32(high);
            int iqty = Convert.ToInt32(qty);
            int inow = Convert.ToInt32(now);
            int[] array = new int[4];
            array[0] = inow;
            array[1] = ihigh;
            array[2] = ilow;
            array[3] =iqty;
            return array;
        }
    }
}

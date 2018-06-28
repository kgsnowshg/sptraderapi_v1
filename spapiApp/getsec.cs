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
    class getsec
    {
        public static int[] hsisec(int totalsec)
        {
            string jsonStrings = File.ReadAllText("secjson/"+totalsec+ ".json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken qbid1 = abc[totalsec.ToString()][0]["qbid1"];
            JToken qbid2 = abc[totalsec.ToString()][0]["qbid2"];
            JToken qbid3 = abc[totalsec.ToString()][0]["qbid3"];
            JToken qbid4 = abc[totalsec.ToString()][0]["qbid4"];
            JToken qbid5 = abc[totalsec.ToString()][0]["qbid5"];
            JToken bid1 = abc[totalsec.ToString()][0]["bid1"];
            int iqbid1 = Convert.ToInt32(qbid1);
            int iqbid2 = Convert.ToInt32(qbid2);
            int iqbid3 = Convert.ToInt32(qbid3);
            int iqbid4 = Convert.ToInt32(qbid4);
            int iqbid5 = Convert.ToInt32(qbid5);
            int ibid1 = Convert.ToInt32(bid1);
            int sum = Convert.ToInt32(qbid1)+ Convert.ToInt32(qbid2)+ Convert.ToInt32(qbid3)+ Convert.ToInt32(qbid4)+ Convert.ToInt32(qbid5);
            int[] array = new int[7];
            array[0] = ibid1;
            array[1] = iqbid1;
            array[2] = iqbid2;
            array[3] = iqbid3;
            array[4] = iqbid4;
            array[5] = iqbid5;
            array[6] = sum;
            return array;
        }
        public static int[] hsisec2(int totalsec)
        {
            string jsonStrings = File.ReadAllText("secjson/" + totalsec + ".json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken qask1 = abc[totalsec.ToString()][0]["qask1"];
            JToken qask2 = abc[totalsec.ToString()][0]["qask2"];
            JToken qask3 = abc[totalsec.ToString()][0]["qask3"];
            JToken qask4 = abc[totalsec.ToString()][0]["qask4"];
            JToken qask5 = abc[totalsec.ToString()][0]["qask5"];
            JToken bid1 = abc[totalsec.ToString()][0]["bid1"]; 
            int iqask1 = Convert.ToInt32(qask1);
            int iqask2 = Convert.ToInt32(qask2);
            int iqask3 = Convert.ToInt32(qask3);
            int iqask4 = Convert.ToInt32(qask4);
            int iqask5 = Convert.ToInt32(qask5);
            int ibid1 = Convert.ToInt32(bid1);
            int sum = Convert.ToInt32(qask1) + Convert.ToInt32(qask2) + Convert.ToInt32(qask3) + Convert.ToInt32(qask4) + Convert.ToInt32(qask5);
            int[] array = new int[7];
            array[0] = ibid1;
            array[1] = iqask1;
            array[2] = iqask2;
            array[3] = iqask3;
            array[4] = iqask4;
            array[5] = iqask5;
            array[6] = sum;
            return array;
        }

        public static int[] hsisec3(int totalsec)
        {
            string jsonStrings = File.ReadAllText("secjson/" + totalsec + ".json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken sec = abc[totalsec.ToString()][0]["sec"];
            JToken low = abc[totalsec.ToString()][0]["low"];
            JToken high = abc[totalsec.ToString()][0]["high"];
            int isec = Convert.ToInt32(sec);
            int ihigh = Convert.ToInt32(high);
            int ilow = Convert.ToInt32(low);
            int[] array = new int[3];
            array[0] = isec;
            array[1] = ihigh;
            array[2] = ilow;
            return array;
        }

        public static int hsia(int totalsec)
        {
            string jsonStrings = File.ReadAllText("sechsiajson/" + totalsec + ".json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken sec = abc[totalsec.ToString()][0]["sec"];
            int secs = Convert.ToInt32(sec);
            return secs;
        }
    }
}

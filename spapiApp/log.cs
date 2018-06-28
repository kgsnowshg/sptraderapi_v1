using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace spapiApp
{
    class log
    {
        public static string buy(int hsi,string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            StreamReader sr = new StreamReader(@"json/log.txt");
            string result = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(@"json/log.txt");
            string time = dt.ToString();//2005-11-5 13:47:04
            sw.WriteLine(result);
            sw.WriteLine("price: " + hsi.ToString());
            sw.WriteLine("B/S: B");
            sw.WriteLine("CASE: "+cases);
            sw.WriteLine("time: " + time+","+ totalsec.ToString());
            sw.Close();
            return "0";
        }
        public static string sell(int hsi, string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            StreamReader sr = new StreamReader(@"json/log.txt");
            string result = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(@"json/log.txt");
            string time=dt.ToString();//2005-11-5 13:47:04
            sw.WriteLine(result);
            sw.WriteLine("price: " + hsi.ToString());
            sw.WriteLine("B/S: S");
            sw.WriteLine("CASE: " + cases);
            sw.WriteLine("time: " + time + "," + totalsec.ToString());
            sw.Close();
            return "0";
        }
        public static string error(string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            StreamReader sr = new StreamReader(@"json/log.txt");
            string result = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(@"json/log.txt");
            string time = dt.ToString();//2005-11-5 13:47:04
            sw.WriteLine(result);
            sw.WriteLine("CASE: " + cases);
            sw.WriteLine("time: " + time + "," + totalsec.ToString());
            sw.Close();
            return "0";
        }
    }
}

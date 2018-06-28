using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spapiApp
{
    class signals
    {
        public static string[] buy(int hour, int min, int sec, double hsi)
        {
           
             string minstr = string.Join(" ", getmin.min1(hour, min, sec));
             string[] minary = minstr.Split(' ');
             int minnow = Convert.ToInt32(minary[0]); //現在
             int minhigh = Convert.ToInt32(minary[1]); //最高
             int minlow = Convert.ToInt32(minary[2]); //最低
             int qty = Convert.ToInt32(minary[3]); //成交量
             int areahigh = Convert.ToInt32(minary[4]); //AREA最高
             int arealow = Convert.ToInt32(minary[5]); //AREA最低
             int areanow = Convert.ToInt32(minary[6]); //AREA現在

            int min2 = min - 1;
            string minstr2 = string.Join(" ", getmin.min1(hour, min2, sec));
            string[] minary2 = minstr.Split(' ');
            int minnow2 = Convert.ToInt32(minary2[0]); //現在
            int minhigh2 = Convert.ToInt32(minary2[1]); //最高
            int minlow2 = Convert.ToInt32(minary2[2]); //最低
            int qty2 = Convert.ToInt32(minary2[3]); //成交量
            int areahigh2 = Convert.ToInt32(minary2[4]); //AREA最高
            int arealow2 = Convert.ToInt32(minary2[5]); //AREA最低
            int areanow2 = Convert.ToInt32(minary2[6]); //AREA現在

            int min3 = min - 6;
            string minstr3 = string.Join(" ", getmin.min1(hour, min2, sec));
            string[] minary3 = minstr.Split(' ');
            int minnow3 = Convert.ToInt32(minstr3[0]); //現在
            int minhigh3 = Convert.ToInt32(minstr3[1]); //最高
            int minlow3 = Convert.ToInt32(minstr3[2]); //最低
            int qty3 = Convert.ToInt32(minstr3[3]); //成交量
            int areahigh3 = Convert.ToInt32(minstr3[4]); //AREA最高
            int arealow3 = Convert.ToInt32(minstr3[5]); //AREA最低
            int areanow3 = Convert.ToInt32(minstr3[6]); //AREA現在
            //第一個情況
            int action = 0;
            string[] arr = new string[4];
            arr[0] = "0";
            int[,] value = new int[12, 7];
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            for (int i = 0; i < 10; i++)
            {
                int path = totalsec - (i * 2);
                string str = string.Join(" ", getsec.hsisec(path));
                string[] ary = str.Split(' ');
                value[i,0] = Convert.ToInt32(ary[0]);
                value[i,1] = Convert.ToInt32(ary[1]);
                value[i,2] = Convert.ToInt32(ary[2]);
                value[i,3] = Convert.ToInt32(ary[3]);
                value[i,4] = Convert.ToInt32(ary[4]);
                value[i,5] = Convert.ToInt32(ary[5]);
                value[i,6] = Convert.ToInt32(ary[6]);
                if (value[i, 6]>50)
                {
                    action = 1;
                }
            }
            if (action==1)
            {
                int paths = totalsec - 100;
                string str = string.Join(" ", getsec.hsisec(paths));
                string[] ary = str.Split(' ');
                value[11, 0] = Convert.ToInt32(ary[0]);
                if ((hsi> (value[11, 0])+12) && (hsi+7)>minhigh && minhigh>minhigh2 && minhigh > minhigh3 && minlow > minlow2 && minlow > minlow3)
                {
                    arr[0] = "1";
                    arr[1] = "1";
                    arr[2] = hsi.ToString();
                    arr[3] = time+":"+ arr[2] +":nowbuy:50qtybuy";
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                    return arr;
                }
            }
            return arr;
        }

        public static string[] sell(int hour, int min, int sec, double hsi)
        {
            string minstr = string.Join(" ", getmin.min1(hour, min, sec));
            string[] minary = minstr.Split(' ');
            int minnow = Convert.ToInt32(minary[0]); //現在
            int minhigh = Convert.ToInt32(minary[1]); //最高
            int minlow = Convert.ToInt32(minary[2]); //最低
            int qty = Convert.ToInt32(minary[3]); //成交量
            int areahigh = Convert.ToInt32(minary[4]); //AREA最高
            int arealow = Convert.ToInt32(minary[5]); //AREA最低
            int min2 = min - 1;
            string minstr2 = string.Join(" ", getmin.min1(hour, min2, sec));
            string[] minary2 = minstr.Split(' ');
            int minnow2 = Convert.ToInt32(minary2[0]); //現在
            int minhigh2 = Convert.ToInt32(minary2[1]); //最高
            int minlow2 = Convert.ToInt32(minary2[2]); //最低
            int qty2 = Convert.ToInt32(minary2[3]); //成交量
            int areahigh2 = Convert.ToInt32(minary2[4]); //AREA最高
            int arealow2 = Convert.ToInt32(minary2[5]); //AREA最低
            int areanow2 = Convert.ToInt32(minary2[6]); //AREA現在

            int min3 = min - 6;
            string minstr3 = string.Join(" ", getmin.min1(hour, min2, sec));
            string[] minary3 = minstr.Split(' ');
            int minnow3 = Convert.ToInt32(minstr3[0]); //現在
            int minhigh3 = Convert.ToInt32(minstr3[1]); //最高
            int minlow3 = Convert.ToInt32(minstr3[2]); //最低
            int qty3 = Convert.ToInt32(minstr3[3]); //成交量
            int areahigh3 = Convert.ToInt32(minstr3[4]); //AREA最高
            int arealow3 = Convert.ToInt32(minstr3[5]); //AREA最低
            int areanow3 = Convert.ToInt32(minstr3[6]); //AREA現在
            int action = 0;
            string[] arr = new string[4];
            arr[0] = "0";
            int[,] value = new int[12, 7];
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            for (int i = 0; i < 10; i++)
            {
                int path = totalsec - (i * 2);
                string str = string.Join("|", getsec.hsisec2(path));
                string[] ary = str.Split('|');
                value[i, 0] = Convert.ToInt32(ary[0]);
                value[i, 1] = Convert.ToInt32(ary[1]);
                value[i, 2] = Convert.ToInt32(ary[2]);
                value[i, 3] = Convert.ToInt32(ary[3]);
                value[i, 4] = Convert.ToInt32(ary[4]);
                value[i, 5] = Convert.ToInt32(ary[5]);
                value[i, 6] = Convert.ToInt32(ary[6]);
                if (value[i, 6] > 50)
                {
                    action = 1;
                }
            }
            if (action == 1)
            {
                int paths = totalsec - 100;
                string str = string.Join(" ", getsec.hsisec2(paths)); 
                string[] ary = str.Split(' ');
                value[11, 0] = Convert.ToInt32(ary[0]);
                if ((hsi < (value[11, 0]) - 12) && minhigh < minhigh2 && minhigh < minhigh3 && minlow < minlow2 && minlow < minlow3)
                {
                    arr[0] = "1";
                    arr[1] = "2";
                    arr[2] = hsi.ToString();
                    arr[3] = time + ":" + arr[2] + ":nowsell:50qtysell";
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                    return arr;
                }
            }
            return arr;
        }

        public static string[] sec(int hour, int min, int sec, double hsi)
        {
            int action = 0;
            string[] arr = new string[4];
            arr[0] = "0";
            int[,] value = new int[5, 3];
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            for (int i = 0; i < 5; i++)
            {
                int path = totalsec - i;
                string str = string.Join(" ", getsec.hsisec3(path));
                string[] ary = str.Split(' ');
                value[i, 0] = Convert.ToInt32(ary[0]); //現在
                value[i, 1] = Convert.ToInt32(ary[1]);//最高
                value[i, 2] = Convert.ToInt32(ary[2]);//最低
            }
            int safe = 1;
            if ((((value[1, 2])-(value[0, 0]))>10000) && (((value[2, 2]) - (value[0, 0])) > 10000) && (((value[3, 2]) - (value[0, 0])) > 10000) && (((value[4, 2]) - (value[0, 0])) > 10000))
            {
                safe = 0;
            }
            if (totalsec > 33343)
            {
                if (((value[0, 1])> (value[1, 2]+8)) && ((value[0, 1]) > (value[2, 2] + 10)) && (value[0, 0]) > (value[3, 0]) && (value[0, 0]) > (value[4, 0]) && safe ==1)
                {
                    arr[0] = "2";
                    arr[1] = "1"; //買入
                    arr[2] = hsi.ToString();
                    arr[3] = time + ":" + arr[2] + ":nowbuy:fastbuy";
                    waitsignal.fastoperate(arr[0], arr[1], arr[2], arr[3]);
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                }
                else if (((value[0, 2]) < (value[1, 1] - 8)) && ((value[0, 2]) < (value[2, 1] - 10)) && (value[0, 0]) < (value[3, 0]) && (value[0, 0]) < (value[4, 0]) && safe==1)
                {
                    arr[0] = "2";
                    arr[1] = "2";//買出
                    arr[2] = hsi.ToString();
                    arr[3] = time + ":" + arr[2] + ":nowsell:fastsell";
                    waitsignal.fastoperate(arr[0], arr[1], arr[2], arr[3]);
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                }
            }
            arr[0] = "0";
            return arr;
        }
    }
}

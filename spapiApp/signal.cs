using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spapiApp
{
    class signal
    {
        public static int hsi(int hour, int min, int sec, double hsi)
        {
            int buys = 0;
            int sells = 0;
            int path;
            string cases = "";
            //獲得過去三十分鐘的值
            int action1 = 0;
            int[,] value = new int[30, 4];
            for (int i = 0; i < 30; i++)
            {
                path = min - i;
                string str = string.Join(" ", getmin.min1(hour, path, sec));
                string[] ary = str.Split(' ');
                value[i, 0] = Convert.ToInt32(ary[0]);
                value[i, 1] = Convert.ToInt32(ary[1]);
                value[i, 2] = Convert.ToInt32(ary[2]);
                value[i, 3] = Convert.ToInt32(ary[3]);
            }

            if ((value[1, 1]) > (value[2, 1]) && (value[1, 2]) > (value[2, 2]) && (value[2, 1]) > (value[3, 1]) && (value[2, 2]) > (value[3, 2]))
            {
                int buylow = (value[1, 2]) - (value[2, 2]);
                buy.case2(value[1, 2],buylow);
            }

            if ((value[1, 1]) < (value[2, 1]) && (value[1, 2]) < (value[2, 2]) && (value[2, 1]) < (value[3, 1]) && (value[2, 2]) < (value[3, 2]))
            {
                int sellhigh = (value[1, 1]) - (value[2, 1]);
                sell.case2(value[1, 1],sellhigh);
            }

            //獲得過去十分鐘最高/最低
            int high10 = value[0, 1];
            int low10 = value[0, 2];
            int high20 = value[0, 1];
            int low20 = value[0, 2];
            int high30 = value[0, 1];
            int low30 = value[0, 2];
            for (int i = 0; i < 9; i++)
            {
                if (value[i+1, 1]> value[i, 1])
                {
                    high10 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low10 = value[i + 1, 1];
                }
            }
            //獲得過去二十分鐘最高/最低
            for (int i = 0; i < 19; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high20 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low20 = value[i + 1, 1];
                }
            }
            //獲得過去三十分鐘最高/最低
            for (int i = 0; i < 29; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high30 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low30 = value[i + 1, 1];
                }
            }
            return 0;
        }


        public static int hsia(int hour, int min, int sec, int hsi)
        {
            int buys = 0;
            int sells = 0;
            string cases = "";
            //獲得過去三十分鐘的值
            int action1 = 0;
            int[,] value = new int[30, 4];
            for (int i = 0; i < 30; i++)
            {
                int path = min - i;
                string str = string.Join(" ", getmin.min2(hour, path, sec));
                string[] ary = str.Split(' ');
                value[i, 0] = Convert.ToInt32(ary[0]);
                value[i, 1] = Convert.ToInt32(ary[1]);
                value[i, 2] = Convert.ToInt32(ary[2]);
                value[i, 3] = Convert.ToInt32(ary[3]);
            }

            //獲得過去十分鐘最高/最低
            int high10 = value[0, 1];
            int low10 = value[0, 2];
            int high20 = value[0, 1];
            int low20 = value[0, 2];
            int high30 = value[0, 1];
            int low30 = value[0, 2];
            for (int i = 0; i < 9; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high10 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low10 = value[i + 1, 1];
                }
            }
            //獲得過去二十分鐘最高/最低
            for (int i = 0; i < 19; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high20 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low20 = value[i + 1, 1];
                }
            }
            //獲得過去三十分鐘最高/最低
            for (int i = 0; i < 29; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high30 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low30 = value[i + 1, 1];
                }
            }
            return 0;
        }
    }
}

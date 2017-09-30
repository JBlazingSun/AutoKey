using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeyCombin
{
    public class MyKey
    {
        //!M4  8倍  60
        public const int K_F3 = 103;
        public const int K_2 = 202;
        public const int K_3 = 203;
        public const int K_4 = 204;
        public const int K_5 = 205;
        public const int K_6 = 206;
        public const int K_7 = 207;
        public const int K_CLOCK = 400;
        public const int K_LShift = 500;
        public const int K_Alt = 602;
        public const int K_Q = 301;
        public const int K_W = 302;
        public const int K_E = 303;
        public const int K_C = 503;
        public const int K_D = 403;
        public const int K_R = 304;
        public const int K_Down = 1;
        public const int K_Up = 2;
        public const int K_SPACE = 603;
        public const int mouseLeftDown = 1;
        public const int mouseLeftUp = 2;
        public const int K_T = 305;

        //M16   70
        //UMP9  85
        //AK    65
       
        //功能： 模拟鼠标点击
        //    参数： 1 =左键按下 ，2 =左键放开
        //4 =右键按下 ，8 =右键放开
        //16 =中键按下 ，32 =中键放开
        //64 =4键按下 ，128 =4键放开
        //256 =5键按下 ，512 =5键放开

        public static void KeyDownUp(int keycode, int RepeateTime,  int interval)
        {
            for (int i = 0; i < RepeateTime; i++)
            {
                Form1.dd.key(keycode, K_Down);
                Thread.Sleep(interval);
                Form1.dd.key(keycode, K_Up);
            }
            
        }
        public static void LbtnAndMoveDown(int DownSpeed, int RepeateTime=5, int interval=10)
        {
            //for (int i = 0; i < RepeateTime; i++)
            //{

            //}
            Form1.dd.btn(1);                                    // 1=左键按下
            Thread.Sleep(interval);
            Form1.dd.btn(2);                                    // 2=左键放开 
            Form1.dd.movR(0, DownSpeed);                               //向下移动
        }

        public static void IsDownR(bool isdownR = true, int interval = 100)
        {
            if (isdownR)
            {
                Thread.Sleep(interval);
                Form1.dd.key(K_R, K_Down);
                Thread.Sleep(interval);
                Form1.dd.key(K_R, K_Up);
            }
        }

        public static void IsDownD(bool isdownD = false, int interval = 100)
        {
            if (isdownD)
            {
                Thread.Sleep(interval);
                Form1.dd.key(K_D, K_Down);
                Thread.Sleep(interval);
                Form1.dd.key(K_D, K_Up);
            }
        }
    }
}

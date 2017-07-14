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
        //F3 天火  +D
        //4   地狱火 +D
        //5  冲击波 +D 
        //6  急速冷却 +D
        //7  隐身  +D
        //大写切换  电磁脉冲  +D
        //T    飓风  +D
        //H 精灵 +D
        //G  冰墙  +D
        
        //迅捷  +D 不用
        //F11开关

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
        
        public static void KeyDownUp(int keycode, int RepeateTime,  int interval)
        {
            for (int i = 0; i < RepeateTime; i++)
            {
                Form1.dd.key(keycode, K_Down);
                Thread.Sleep(interval);
                Form1.dd.key(keycode, K_Up);
            }
            
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

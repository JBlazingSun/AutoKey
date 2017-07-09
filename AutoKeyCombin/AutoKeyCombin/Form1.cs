using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//F3 天火  +D
//4   地狱火 +D
//5  冲击波 +D 
//6   迅捷  +D
//7  隐身  +D
//大写切换  电磁脉冲  +D
//左Shift    飓风  +D
//ALT+3 精灵 +D
//Alt+2  冰墙  +D
//Alt+C  急速冷却 +D

    //F11开关

namespace AutoKeyCombin
{
    public partial class Form1 : Form
    {
        private bool open = true;
        KeyboardHook kh;
        public const int KEYDOWN = 0;
        public const int KEYUP = 2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public Form1()
        {
            InitializeComponent();

            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.F11))
            {
                open = !open;
            }
            if (open)
            {
                if (e.KeyData == (Keys.F3))//天火
                {
                    keybd_event((byte) Keys.E, 0, KEYDOWN, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.E, 0, KEYUP, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.E, 0, KEYDOWN, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.E, 0, KEYUP, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.E, 0, KEYDOWN, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.E, 0, KEYUP, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.D, 0, KEYDOWN, 0);
                    Thread.Sleep(100);
                    keybd_event((byte)Keys.D, 0, KEYUP, 0);
                    Thread.Sleep(100);
                }
                if (e.KeyData == (Keys.D4))//地狱火
                {
                    MessageBox.Show("地狱火");
                }
                if (e.KeyData == (Keys.D5))//冲击波
                {
                    MessageBox.Show("冲击波");
                }
                if (e.KeyData == (Keys.D6))//迅捷
                {
                    MessageBox.Show("迅捷");
                }
                if (e.KeyData == (Keys.D7))//隐身
                {
                    MessageBox.Show("隐身");
                }
                if (e.KeyData == (Keys.CapsLock))//电磁脉冲
                {
                    MessageBox.Show("电磁脉冲");
                }
                if (e.KeyData == (Keys.X| Keys.Alt))//飓风
                {
                    MessageBox.Show("飓风");
                }
                if (e.KeyData == (Keys.D3 | Keys.Alt))//精灵
                {
                    MessageBox.Show("精灵");
                }

                if (e.KeyData == (Keys.D2 | Keys.Alt))//冰墙
                {
                    MessageBox.Show("冰墙");
                }

                if (e.KeyData == (Keys.C | Keys.Alt))//急速冷却
                {
                    MessageBox.Show("急速冷却");
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
        }
    }
}

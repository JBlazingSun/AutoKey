using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AutoKeyCombin.MyKey;

namespace AutoKeyCombin
{
    public partial class Form1 : Form
    {
        private bool open = true;
        KeyboardHook kh;
        public static CDD dd;

        public Form1()
        {
            InitializeComponent();

            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            dd = new CDD();
            LoadDllFile(Directory.GetCurrentDirectory() + "\\dd74000x64.64.dll");
        }
        private void LoadDllFile(string dllfile)
        {
            
            System.IO.FileInfo fi = new System.IO.FileInfo(dllfile);
            if (!fi.Exists)
            {
                MessageBox.Show("文件不存在");
                return;
            }

            int ret = dd.Load(dllfile);
            if (ret == -2) { MessageBox.Show("装载库时发生错误"); return; }
            if (ret == -1) { MessageBox.Show("取函数地址时发生错误"); return; }
            if (ret == 0) { MessageBox.Show("非增强模块"); }

            return;
        }
        public int interval = 200;
        public int thirdinterval = 100;
        public bool isdownD = false;
        public bool isdownR = true;
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
                    KeyDownUp(K_E, 3, thirdinterval);

                    IsDownR(isdownR, interval);
                    //IsDownD(true,interval);
                }
                if (e.KeyData == (Keys.D4))//冲击波
                {
                    KeyDownUp(K_Q, 1, thirdinterval);
                    KeyDownUp(K_W, 1, thirdinterval);
                    KeyDownUp(K_E, 1, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
                if (e.KeyData == (Keys.D5))//地狱火
                {
                    KeyDownUp(K_W, 1,thirdinterval);
                    KeyDownUp(K_E, 2, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
                if (e.KeyData == (Keys.D6))//迅捷
                {
                    KeyDownUp(K_W, 2, thirdinterval);
                    KeyDownUp(K_E, 1, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
                if (e.KeyData == (Keys.D7))//隐身
                {
                    KeyDownUp(K_Q, 2, thirdinterval);
                    KeyDownUp(K_W, 1, thirdinterval);

                    IsDownR(isdownR, interval);
                    //IsDownD(true, interval);
                }
                if (e.KeyData == (Keys.CapsLock))//电磁脉冲
                {
                    KeyDownUp(K_W, 3, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
                if (e.KeyData == (Keys.X | Keys.Alt))//飓风
                {
                    KeyDownUp(K_Q, 1, thirdinterval);
                    KeyDownUp(K_W, 2, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
                if (e.KeyData == (Keys.D3 | Keys.Alt))//精灵
                {
                    KeyDownUp(K_E, 2, thirdinterval);
                    KeyDownUp(K_Q, 1, thirdinterval);

                    IsDownR(isdownR, interval);
                    //IsDownD(true, interval);
                }

                if (e.KeyData == (Keys.D2 | Keys.Alt))//冰墙
                {
                    KeyDownUp(K_E, 1, thirdinterval);
                    KeyDownUp(K_Q, 2, thirdinterval);

                    IsDownR(isdownR, interval);
                    //IsDownD(true, interval);
                }

                if (e.KeyData == (Keys.C | Keys.Alt))//急速冷却
                {
                    KeyDownUp(K_Q, 3, thirdinterval);

                    IsDownR(isdownR, interval);
                    IsDownD(isdownD, interval);
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
        }
    }
}

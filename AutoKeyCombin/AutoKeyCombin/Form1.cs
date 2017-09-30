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
                if (e.KeyData == Keys.N)//大跳
                {
                    dd.key(MyKey.K_SPACE, MyKey.K_Down);
                    Thread.Sleep(100);
                    dd.key(MyKey.K_SPACE, MyKey.K_Up);
                    Thread.Sleep(100);
                    dd.key(MyKey.K_C, MyKey.K_Down);
                    Thread.Sleep(100);
                    dd.key(MyKey.K_C, MyKey.K_Up);

                    //dd.key(MyKey.K_C, MyKey.K_Down);
                    //Thread.Sleep(100);
                    //dd.key(MyKey.K_SPACE, MyKey.K_Down);
                    //Thread.Sleep(100);
                    //dd.key(MyKey.K_C, MyKey.K_Up);
                    //Thread.Sleep(100);
                    //dd.key(MyKey.K_SPACE, MyKey.K_Up);
                }
                if (e.KeyData == Keys.T) //
                {
                    //Point ms = MousePosition;
                    //richTextBox1.Text += string.Format("{0}:{1}", ms.X, ms.Y)+Environment.NewLine;
                    //MouseButtons mb = Control.MouseButtons;
                    LbtnAndMoveDown(trackBar1.Value);
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }
    }
}

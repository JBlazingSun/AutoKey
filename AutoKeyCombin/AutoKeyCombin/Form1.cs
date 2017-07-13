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
        public const int KEYDOWN = 0x0104;
        public const int KEYUP = 0x0105;
        public const int VK_E = 0x45;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, uint wParam, uint lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        static extern bool PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);


        private CDD dd;

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
                    int ddcode = 303;                         //DD键码
                    dd.str("e");
                    dd.str("e");                        // 1=按下 2=放开   
                }
                //if (e.KeyData == (Keys.D4))//地狱火
                //{
                //    MessageBox.Show("地狱火");
                //}
                //if (e.KeyData == (Keys.D5))//冲击波
                //{
                //    MessageBox.Show("冲击波");
                //}
                //if (e.KeyData == (Keys.D6))//迅捷
                //{
                //    MessageBox.Show("迅捷");
                //}
                //if (e.KeyData == (Keys.D7))//隐身
                //{
                //    MessageBox.Show("隐身");
                //}
                //if (e.KeyData == (Keys.CapsLock))//电磁脉冲
                //{
                //    MessageBox.Show("电磁脉冲");
                //}
                //if (e.KeyData == (Keys.X| Keys.Alt))//飓风
                //{
                //    MessageBox.Show("飓风");
                //}
                //if (e.KeyData == (Keys.D3 | Keys.Alt))//精灵
                //{
                //    MessageBox.Show("精灵");
                //}

                //if (e.KeyData == (Keys.D2 | Keys.Alt))//冰墙
                //{
                //    MessageBox.Show("冰墙");
                //}

                //if (e.KeyData == (Keys.C | Keys.Alt))//急速冷却
                //{
                //    MessageBox.Show("急速冷却");
                //}
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//F3 天火  +D
//4   地狱火 +D
//5  冲击波 +D 
//6   迅捷  +D
//7  隐身  +D
//大写切换  电磁脉冲  +D
//Shift    飓风  +D
//ALT+3 精灵 +D
//Alt+2  冰墙  +D
//Alt+C  急速冷却 +D


    //F10开
    //F11关

namespace AutoKeyCombin
{
    public partial class Form1 : Form
    {
        private bool open = true;
        KeyboardHook kh;
        public Form1()
        {
            InitializeComponent();

            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.F10) && open)
            {

            }
            if (e.KeyData == (Keys.F11))
            {
                open = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
        }
    }
}

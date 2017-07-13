using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoKeyCombin
{
    public class WinIO
    {
        //winio开始
        public const int KBC_KEY_CMD = 0x64;
        public const int KBC_KEY_DATA = 0x60;
        [DllImport("WinIo64.dll")]
        public static extern bool InitializeWinIo();
        [DllImport("WinIo64.dll")]
        public static extern bool GetPortVal(IntPtr wPortAddr, out int pdwPortVal,
            byte bSize);
        [DllImport("WinIo64.dll")]
        public static extern bool SetPortVal(uint wPortAddr, IntPtr dwPortVal,
            byte bSize);
        [DllImport("WinIo64.dll")]
        public static extern byte MapPhysToLin(byte pbPhysAddr, uint dwPhysSize,
            IntPtr PhysicalMemoryHandle);
        [DllImport("WinIo64.dll")]
        public static extern bool UnmapPhysicalMemory(IntPtr PhysicalMemoryHandle,
            byte pbLinAddr);
        [DllImport("WinIo64.dll")]
        public static extern bool GetPhysLong(IntPtr pbPhysAddr, byte pdwPhysVal);
        [DllImport("WinIo64.dll")]
        public static extern bool SetPhysLong(IntPtr pbPhysAddr, byte dwPhysVal);
        [DllImport("WinIo64.dll")]
        public static extern void ShutdownWinIo();
        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(uint Ucode, uint uMapType);

        public void Initialize()
        {
            try
            {
                InitializeWinIo();
                KBCWait4IBE();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        public void Shutdown()
        {
            ShutdownWinIo();
            KBCWait4IBE();
        }
        ///等待键盘缓冲区为空
        public void KBCWait4IBE()
        {
            int dwVal = 0;
            do
            {
                bool flag = GetPortVal((IntPtr)0x64, out dwVal, 1);
            }
            while ((dwVal & 0x2) > 0);
        }
        /// 模拟键盘标按下
        public void KeyDown(Keys vKeyCoad)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((uint)vKeyCoad, 0);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);
        }
        /// 模拟键盘弹出
        public void KeyUp(Keys vKeyCoad)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((uint)vKeyCoad, 0);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);
        }
        /// 模拟一次按键
        public void KeyDownUp(Keys vKeyCoad)
        {
            Initialize();
            KeyUp(vKeyCoad);
            Thread.Sleep(100);
            KeyDown(vKeyCoad);
            Shutdown();
        }

    }
}

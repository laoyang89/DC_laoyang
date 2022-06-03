using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.UI
{
    public class SystemMenuManager
    {
        public SystemMenuManager()
        {

        }


        /// <summary>
        /// 移动
        /// </summary>
        public const uint SC_MOVE = 0xF010;

        /// <summary>
        /// 关闭
        /// </summary>
        public const uint SC_CLOSE = 0xF060;

        /// <summary>
        /// 按命令方式
        /// </summary>
        public const uint MF_BYCOMMAND = 0x00;

        /// <summary>
        /// 灰掉
        /// </summary>
        public const uint MF_GRAYED = 0x01;

        /// <summary>
        /// 不可用
        /// </summary>
        public const uint MF_DISABLED = 0x02;  

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]        
        static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        /// <summary>
        /// 删除系统菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        /// <summary>
        /// 启用or禁用系统菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uIDEnableItem"></param>
        /// <param name="uEnable"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);


        // 提示：C＃把函数声明为外部的，而且使用属性DllImport来指定DLL 
        //和任何其他可能需要的参数。 
        // 首先，我们需要GetSystemMenu() 函数 
        // 注意这个函数没有Unicode 版本 
        [DllImport("USER32", EntryPoint = "GetSystemMenu", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr apiGetSystemMenu(IntPtr WindowHandle, int bReset);


        /// <summary>
        /// 插入系统菜单
        /// </summary>
        /// <param name="MenuHandle"></param>
        /// <param name="Flags"></param>
        /// <param name="NewID"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        [DllImport("USER32", EntryPoint = "AppendMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int apiAppendMenu(IntPtr MenuHandle, int Flags, int NewID, String Item);

        //还可能需要InsertMenu() 
        [DllImport("USER32", EntryPoint = "InsertMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int apiInsertMenu(IntPtr hMenu, int Position, int Flags, int NewId, String Item);

        /// <summary>
        /// 删除系统菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        public static void DeleteSystemMenu(IntPtr hMenu, uint uPosition, uint uFlags)
        {
            DeleteMenu(hMenu, uPosition, uFlags);
        }

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        public static IntPtr GetSysMenu(IntPtr hwnd, bool bRevert)
        {
            return GetSystemMenu(hwnd, bRevert);
        }
    }
}

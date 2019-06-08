using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace win32api
{
	public class SetWindowTransparent
	{

		//下面这段代码主要用来调用Windows API实现窗体透明(鼠标可以穿透窗体)

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		public static extern long GetWindowLong(IntPtr hwnd, int nIndex);
		[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
		public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);
		[DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
		private static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags);
		const int GWL_EXSTYLE = -20;
		const int WS_EX_TRANSPARENT = 0x20;
		const int WS_EX_LAYERED = 0x80000;
		const int LWA_ALPHA = 2;

		public static void SetOpacity(IntPtr h1, byte opacity)
		{

			// 设置Windows属性
			SetWindowLong(h1, GWL_EXSTYLE, GetWindowLong(h1, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED);
			SetLayeredWindowAttributes(h1, 0, 100, LWA_ALPHA);


			SetLayeredWindowAttributes(h1, 0, opacity, LWA_ALPHA);
		}


	}







}

using NetCoreEx.Geometry;
using System;
using WinApi.Desktop;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Controls.Layouts;

namespace win32api
{
	public class ApiFuncs
	{




		//public static int Main(string[] args)
		//{
		//	using (var win = Window.Create(text: "Hello"))
		//	{
		//		win.Show();
		//		return new EventLoop().Run(win);
		//	}
		//}


		public static void ShowMsg(object ex) {


			ApplicationHelpers.ShowCriticalError(ex);
		}

		public static void SendSingleKey(Input[] m_inputs)
		{
			var x = User32Helpers.SendInput(m_inputs);

			Input.InitKeyboardInput(out m_inputs[0], VirtualKey.H, false);

			//Input.InitKeyboardInput(out m_inputs[0], , false);
			//Input.InitMouseInput(out m_inputs[0],0,0,0);
		}

		public static void SendMouseClick()
		{
			Input[] m_inputs = new Input[2];
			Input.InitMouseInput(out m_inputs[0], 0, 0, MouseInputFlags.MOUSEEVENTF_LEFTDOWN, 0, 0);
			Input.InitMouseInput(out m_inputs[1], 0, 0, MouseInputFlags.MOUSEEVENTF_LEFTUP, 0, 50);
			var x = User32Helpers.SendInput(m_inputs);
		}



		public static void SetMousePos(int x,int y)
		{
			User32Methods.SetPhysicalCursorPos(x, y);
		}

		public static IntPtr FindWindow(string lpClassName, string lpWindowName)
		{
			return  User32Methods.FindWindow(lpClassName,lpWindowName);
		}

		public static void SetForegroundWindow(IntPtr Hwnd)
		{
			User32Methods.SetForegroundWindow(Hwnd);
		}

		public static Rectangle GetWindowRect(IntPtr Hwnd)
		{
			var result = User32Methods.GetWindowRect(Hwnd, out Rectangle win_rectangle);

			return win_rectangle;
			
		}
		



		//protected override void OnCreate(ref CreateWindowPacket packet)
		//{
		//	this.m_textBox = StaticBox.Create(
		//		"Ahoy!",
		//		hParent: this.Handle);

		//	this.m_editBox = EditBox.Create(
		//		"Nothing here yet.",
		//		hParent: this.Handle,
		//		controlStyles:
		//		EditBox.EditStyles.ES_MULTILINE | EditBox.EditStyles.ES_WANTRETURN |
		//		(EditBox.EditStyles)WindowStyles.WS_VSCROLL);

		//	this.m_layout.ClientArea = this.GetClientRect();
		//	this.m_layout.Margin = new Rectangle(10, 10, 10, 10);
		//	this.m_layout.Children.Add(this.m_textBox);
		//	this.m_layout.Children.Add(this.m_editBox);
		//	this.m_layout.PerformLayout();
		//	this.m_timerProc = (wnd, uMsg, eventId, millis) =>
		//	{
		//		try
		//		{
		//			this.m_timesExecuted++;
		//			Input.InitKeyboardInput(out this.m_inputs[0], VirtualKey.H, false);
		//			Input.InitKeyboardInput(out this.m_inputs[1], VirtualKey.H, true);
		//			Input.InitKeyboardInput(out this.m_inputs[3], VirtualKey.E, false);
		//			Input.InitKeyboardInput(out this.m_inputs[4], VirtualKey.E, true);
		//			Input.InitKeyboardInput(out this.m_inputs[5], VirtualKey.L, false);
		//			Input.InitKeyboardInput(out this.m_inputs[6], VirtualKey.L, true);
		//			Input.InitKeyboardInput(out this.m_inputs[7], VirtualKey.L, false);
		//			Input.InitKeyboardInput(out this.m_inputs[8], VirtualKey.L, true);
		//			Input.InitKeyboardInput(out this.m_inputs[9], VirtualKey.O, false);
		//			Input.InitKeyboardInput(out this.m_inputs[10], VirtualKey.O, true);
		//			var x = User32Helpers.SendInput(this.m_inputs);
		//		}
		//		catch (Exception ex)
		//		{
		//			this.m_editBox.SetText($"ERROR: {ex.Message}\r\n{ex.StackTrace}");
		//		}
		//	};

		//	//this.m_timerId = User32Methods.SetTimer(this.Handle, IntPtr.Zero, 20, this.m_timerProc);
		//	//base.OnCreate(ref packet);
		//}

		//protected override void OnKey(ref KeyPacket packet)
		//{
		//	var key = packet.Key;
		//	var inputState = packet.InputState;
		//	var str = $"\r\n{DateTime.Now} :" +
		//			  $" {key} => {inputState.IsKeyUpTransition}; " +
		//			  $"{inputState.RepeatCount}; " +
		//			  $"{inputState.ScanCode}; " +
		//			  $"{inputState.IsContextual}; " +
		//			  $"{inputState.IsExtendedKey}" + "\r\n" +
		//			  $"No. of text display changes: {this.m_timesExecuted}" + "\0";
		//	this.m_textBox.SetText(str);
		//	base.OnKey(ref packet);
		//}

		//protected override void OnMouseButton(ref MouseButtonPacket packet)
		//{
		//	var button = packet.Button;
		//	var isButtonDown = packet.IsButtonDown;
		//	if ((button == MouseButton.Left) && isButtonDown) this.SetFocus();
		//	base.OnMouseButton(ref packet);
		//}

		//protected override void OnSize(ref SizePacket packet)
		//{
		//	var size = packet.Size;
		//	this.m_layout.SetSize(ref size);
		//	base.OnSize(ref packet);
		//}



	}











}

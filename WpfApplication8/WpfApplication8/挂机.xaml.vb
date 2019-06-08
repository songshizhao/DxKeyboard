Imports System.Threading
Imports System.Windows.Threading
Imports WinApi.User32
Imports SharpDX.DirectInput
Public Class 挂机
    Property timer As DispatcherTimer = New DispatcherTimer()
    Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        timer.Interval = New TimeSpan(0, 0, 0, 300)
        AddHandler timer.Tick, AddressOf TimerTick
    End Sub
    Private Function TimerTick() As Object
        Dim dHwnd = User32Methods.FindWindow("PopKart Client", "PopKart Client")
        Dim pRec As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        User32Methods.GetWindowRect(dHwnd, pRec)
        User32Methods.SetForegroundWindow(dHwnd)
        sendMouseClick(pRec.Left + 133, pRec.Top + 775)
        WinApi.Kernel32.Kernel32Methods.Sleep(1000) '延时1秒
        sendMouseClick(pRec.Left + 120, pRec.Top + 688)
        WinApi.Kernel32.Kernel32Methods.Sleep(1000) '延时1秒
        sendMouseClick(pRec.Left + 133, pRec.Top + 775)
        WinApi.Kernel32.Kernel32Methods.Sleep(1000) '延时1秒
        sendMouseClick(pRec.Left + 120, pRec.Top + 688)
        WinApi.Kernel32.Kernel32Methods.Sleep(1000) '延时1秒
        sendMouseClick(pRec.Left + 512, pRec.Top + 611) '点击确定领取奖品
        WinApi.Kernel32.Kernel32Methods.Sleep(1000) '延时1秒
        Dim m_inputs(0 To 1) As Input
        Input.InitKeyboardInput(m_inputs(0), Key.Escape, False, False, 0)
        Input.InitKeyboardInput(m_inputs(1), Key.Escape, True, False, 0)
        User32Helpers.SendInput(m_inputs)
        'WinApi.Kernel32.Kernel32Methods.Sleep(50) '延时
        'Input.InitKeyboardInput(m_inputs(0), Key.Enter, True, False, 0)
        'User32Helpers.SendInput(m_inputs)
        Input.InitKeyboardInput(m_inputs(0), Key.NumberPadEnter, False, False, 0)
        Input.InitKeyboardInput(m_inputs(1), Key.NumberPadEnter, True, False, 0)
        User32Helpers.SendInput(m_inputs)
        'WinApi.Kernel32.Kernel32Methods.Sleep(50) '延时
        'Input.InitKeyboardInput(m_inputs(0), Key.Escape, True, False, 0)
        'User32Helpers.SendInput(m_inputs)
        'Return Nothing
    End Function
    '开始白嫖
    Private Sub Command2_Click(sender As Object, e As RoutedEventArgs)
        timer.IsEnabled = Not timer.IsEnabled
        Command2.Content = IIf(timer.IsEnabled, "正在白嫖...", "开始白嫖")
        TimerTick()
    End Sub
    '测试
    Private Sub Command1_Click(sender As Object, e As RoutedEventArgs)
        Dim dHwnd = User32Methods.FindWindow("PopKart Client", "PopKart Client")
        Dim pRec As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        User32Methods.GetWindowRect(dHwnd, pRec)
        User32Methods.SetForegroundWindow(dHwnd)
        sendMouseClick(pRec.Left + 133, pRec.Top + 775)
    End Sub
    Sub sendMouseClick(x As Integer, y As Integer)
        WinApi.User32.User32Methods.SetCursorPos(x, y)
        Dim m_inputs(0 To 1) As Input
        Input.InitMouseInput(m_inputs(0), 0, 0, MouseInputFlags.MOUSEEVENTF_LEFTDOWN, 0, 0)
        Input.InitMouseInput(m_inputs(1), 0, 0, MouseInputFlags.MOUSEEVENTF_LEFTUP, 0, 0)
        User32Helpers.SendInput(m_inputs)
    End Sub
    Sub sendkey(key As Key)
        Dim m_inputs(0 To 1) As Input
        Input.InitKeyboardInput(m_inputs(0), Key.Escape, True, False, 0)
        User32Helpers.SendInput(m_inputs)
    End Sub
End Class

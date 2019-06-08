Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows.Controls.Primitives
Imports System.Windows.Interop
Imports System.Windows.Threading
Imports SharpDX.DirectInput
Imports WinApi.User32
Class MainWindow
    Implements INotifyPropertyChanged
    Public Shared thisUI As UIElement
    Public Shared thisParent As Form1
    Public can_drag As Boolean = True
    Public key_transparent As Boolean = False
    Public is_window As Boolean = False
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    Public directInput As DirectInput = New DirectInput()
    Public MyKeyboard As Keyboard
    Dim key_pressd_color As New SolidColorBrush(Colors.Red)
    'Dim key_pressd_color As New SolidColorBrush(Colors.Gold)
    Dim key_released_color As New SolidColorBrush(Color.FromRgb(0, 6, 7))
    Dim text_pressed_color As New SolidColorBrush(Colors.White)
    Dim text_released_color As New SolidColorBrush(Colors.White)
    Public Shared Mytimer As DispatcherTimer = New DispatcherTimer()
    'Public Shared SendInputTimer As DispatcherTimer = New DispatcherTimer()
    Const GWL_EXSTYLE = -20
    Const WS_EX_TRANSPARENT = &H20
    Const WS_EX_LAYERED = &H80000
    Const LWA_ALPHA = 2
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        thisUI = Me
        '窗口置顶
        Me.Topmost = True
        '隐藏任务栏图标
        'Me.ShowInTaskbar = False
        Dim Devices = directInput.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices)
        For Each device In Devices
            If (device.Type = SharpDX.DirectInput.DeviceType.Keyboard) Then
                MyKeyboard = New SharpDX.DirectInput.Keyboard(directInput)
                MyKeyboard.Acquire()
            End If
        Next
        '启动timer检测键盘按键扫描
        Mytimer.Interval = New TimeSpan(0, 0, 0, 0, 12)
        AddHandler Mytimer.Tick, AddressOf TimerTick
        Mytimer.Start()
        '修正窗口位置
        Dim dHwnd = User32Methods.FindWindow("PopKart Client", "PopKart Client")
        Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        If User32Methods.GetWindowRect(dHwnd, win_rectangle) Then
            Debug.WriteLine("找到窗口")
            Me.Left = win_rectangle.Left + 685
            Me.Top = win_rectangle.Top + 460
        End If
        ' 调用api实现窗口透明效果
        '
    End Sub
    Dim ut As TimeSpan = New TimeSpan(0, 0, 0)
    Private Sub TimerTick(sender As Object, e As EventArgs)
        '检测按键
        Check_key()
        '时间函数
        'ut = ut.Add(Mytimer.Interval)
        'If ut.TotalSeconds >= 1 Then
        '    ut = New TimeSpan(0, 0, 0)
        '    Dim m_inputs(0 To 1) As Input
        '    '发送回回车按键 28 
        '    Input.InitKeyboardInput(m_inputs(0), 203, False, False, 0)
        '    Input.InitKeyboardInput(m_inputs(0), 203, False, False, 0)
        '    User32Helpers.SendInput(m_inputs)
        'End If
    End Sub
    Sub sendkey()
        Try
            Dim m_inputs(0 To 1) As Input
            Dim curKeyboardState = MyKeyboard.GetCurrentState()
            If curKeyboardState.IsPressed(Key.Left) Then
                '左键抬起 右键按下
                Input.InitKeyboardInput(m_inputs(0), Key.Left, True, False, 0)
                Input.InitKeyboardInput(m_inputs(1), Key.Right, False, False, 0)
            Else
                '右键抬起 左键按下 
                Input.InitKeyboardInput(m_inputs(1), Key.Right, True, False, 0)
                Input.InitKeyboardInput(m_inputs(0), Key.Left, False, False, 0)
            End If
            User32Helpers.SendInput(m_inputs)
        Catch ex As Exception
        End Try
    End Sub
    Dim USETAB = False
    Sub Check_key()
        Try
            Dim curKeyboardState = MyKeyboard.GetCurrentState()
            '键盘检测
            If (curKeyboardState.IsPressed(Key.Capital)) Then
                USETAB = True
                sendkey()
            Else
                If USETAB Then
                    USETAB = False
                    Dim m_inputs(0 To 1) As Input
                    Input.InitKeyboardInput(m_inputs(0), Key.Left, True, False, 0)
                    Input.InitKeyboardInput(m_inputs(1), Key.Right, True, False, 0)
                    User32Helpers.SendInput(m_inputs)
                End If
            End If
            If curKeyboardState.IsPressed(Key.K) Then
                Me.Close()
            End If
            If curKeyboardState.IsPressed(Key.H) Then
                WinApi.Kernel32.Kernel32Methods.Sleep(500)
                If thisParent.Enabled = True Then
                    If thisParent.FormBorderStyle = Forms.FormBorderStyle.Sizable Then
                        thisParent.FormBorderStyle = Forms.FormBorderStyle.None
                    Else
                        thisParent.FormBorderStyle = Forms.FormBorderStyle.Sizable
                    End If
                End If
            End If
            '上方向键
            If curKeyboardState.IsPressed(Key.Up) Then
                Border_up.Background = key_pressd_color
                tup.Foreground = text_pressed_color
            Else
                Border_up.Background = key_released_color
                tup.Foreground = text_released_color
            End If
            '左方向键
            If curKeyboardState.IsPressed(Key.Left) Then
                Border_left.Background = key_pressd_color
                tleft.Foreground = text_pressed_color
            Else
                Border_left.Background = key_released_color
                tleft.Foreground = text_released_color
            End If
            '下方向键
            If curKeyboardState.IsPressed(Key.Down) Then
                Border_down.Background = key_pressd_color
                tdown.Foreground = text_pressed_color
            Else
                Border_down.Background = key_released_color
                tdown.Foreground = text_released_color
            End If
            '右方向键
            If curKeyboardState.IsPressed(Key.Right) Then
                Border_right.Background = key_pressd_color
                tright.Foreground = text_pressed_color
            Else
                Border_right.Background = key_released_color
                tright.Foreground = text_released_color
            End If
            'shieft键
            If curKeyboardState.IsPressed(Key.LeftShift) Or curKeyboardState.IsPressed(Key.RightShift) Then
                Border_shift.Background = key_pressd_color
                tshift.Foreground = text_pressed_color
            Else
                Border_shift.Background = key_released_color
                tshift.Foreground = text_released_color
            End If
            'ctrl键
            If curKeyboardState.IsPressed(Key.RightControl) Or curKeyboardState.IsPressed(Key.LeftControl) Then
                Border_ctrl.Background = key_pressd_color
                tctrl.Foreground = text_pressed_color
            Else
                Border_ctrl.Background = key_released_color
                tctrl.Foreground = text_released_color
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private keybackground As SolidColorBrush
    Public Property key_background() As SolidColorBrush
        Get
            Return keybackground
        End Get
        Set(ByVal value As SolidColorBrush)
            keybackground = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private keyforeground As SolidColorBrush
    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
    End Sub
    Public Property key_foreground() As SolidColorBrush
        Get
            Return keyforeground
        End Get
        Set(ByVal value As SolidColorBrush)
            keyforeground = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private Sub Window_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs) Handles MyBase.MouseLeftButtonDown
        If can_drag Then
            Me.DragMove()
        End If
    End Sub
    'Private Sub checkBox_Checked(sender As Object, e As RoutedEventArgs) Handles checkBox.Checked
    '    mainborder.Background = Brushes.Transparent
    '    checkBox.Content = "开"
    'End Sub
    'Private Sub checkBox_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkBox.Unchecked
    '    mainborder.Background = New SolidColorBrush（Colors.Goldenrod）
    '    checkBox.Content = "关"
    'End Sub
    Dim func1 As Func(Of Integer, Integer) = Function(ByVal i As Integer)
                                                 Return i + 1
                                             End Function
    Dim func2 As Func(Of Integer, Integer, Integer) = Function(ByVal i As Integer, ByVal t As Integer)
                                                          Return i * t
                                                      End Function
    Dim func3 As Func(Of Integer, Integer, String) = Function(ByVal i As Integer, ByVal t As Integer)
                                                         Return (i * t).ToString
                                                     End Function
    '无返回值的函数
    Dim act1 As Action(Of String) = Sub(ByVal name As String)
                                        Console.Write("hello," + name)
                                    End Sub
    Dim setup_wpf_window As Func(Of IntPtr) = Function()
                                                  Dim Container = New Container()
                                                  Container.Show()
                                                  Dim rr = TryCast(PresentationSource.FromVisual(Container.MyConainer), HwndSource)
                                                  Dim parent = rr.Handle 'New WindowInteropHelper(Container.MyConainer).Handle
                                                  Return parent
                                              End Function
    Dim setup_winform_window As Func(Of IntPtr) = Function()
                                                      Dim Container = New Form1()
                                                      Container.Show()
                                                      Dim parent = Container.Handle
                                                      Return parent
                                                  End Function
    'Async Function setupnewwindow() As Task(Of IntPtr)
    'End Function
    Private Sub Menu_Click(sender As Object, e As RoutedEventArgs)
        Select Case TryCast(sender, MenuItem).Tag.ToString
            Case "嵌入游戏"
                If is_window Then
                    MessageBox.Show("已经是窗口模式")
                End If
                'Dim parent = setup_wpf_window()
                Dim Container = New Form1()
                Container.MakeTransparent()
                Container.Show()
                thisParent = Container
                Dim newMainWindow = New MainWindow()
                newMainWindow.AllowsTransparency = False
                newMainWindow.can_drag = False
                newMainWindow.Background = New SolidColorBrush(Colors.WhiteSmoke)
                newMainWindow.Show()
                Dim parent = Container.Handle
                Dim child = New WindowInteropHelper(newMainWindow).Handle
                User32Methods.MoveWindow(child, 0, 0, Me.Width, Me.Height, False)
                Dim combine = User32Methods.SetParent(child, parent)
                is_window = True
                'WinApi.User32.User32Methods.MoveWindow(child, 0, 0, Me.Width, Me.Height, True)
                'Me.Left = 685
                'Me.Top = 460
                ''修正窗口位置
                Dim dHwnd = User32Methods.FindWindow("PopKart Client", "PopKart Client")
                'dHwnd = User32Methods.FindWindow("Notepad", Nothing)
                If dHwnd = 0 Then
                    MessageBox.Show("找不到跑跑卡丁车")
                End If
                Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
                If User32Methods.GetWindowRect(parent, win_rectangle) Then
                    Debug.WriteLine("找到窗口")
                    'Container.Left = win_rectangle.Left + 685
                    'Container.Top = win_rectangle.Top + 460
                End If
                User32Methods.SetParent(parent, dHwnd)
                '取消child的标题
                '移动child的位置
                WinApi.User32.User32Methods.MoveWindow(parent, 0, 0, 400, 200, False)
                Me.Close()
            Case "退出"
                Me.Close()
            Case "挂机"
                Dim gj = New 挂机()
                gj.Show()
            Case "背景透明"
                Try
                    Me.AllowsTransparency = True
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "按键透明"
                If key_transparent Then
                    key_released_color = New SolidColorBrush(Color.FromRgb(0, 6, 7))
                    key_transparent = False
                Else
                    key_released_color = New SolidColorBrush(Colors.Transparent)
                    key_transparent = True
                End If
            Case Else
        End Select
        'Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        'If User32Methods.GetWindowRect((New WindowInteropHelper(Container)).Handle, win_rectangle) Then
        '    Debug.WriteLine("找到窗口")
        '    Me.Left = win_rectangle.Left + 685
        '    Me.Top = win_rectangle.Top + 460
        'End If
        'User32Methods.SetParent(New WindowInteropHelper(Container)).Handle, New WindowInteropHelper(Container)).Handle)
        '
    End Sub
End Class

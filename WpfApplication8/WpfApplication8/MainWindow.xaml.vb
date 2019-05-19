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


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public directInput As DirectInput = New DirectInput()
    Public MyKeyboard As Keyboard

    Dim key_pressd_color As New SolidColorBrush(Colors.HotPink)
    Dim key_released_color As New SolidColorBrush(Colors.Black)
    Dim text_pressed_color As New SolidColorBrush(Colors.Black)
    Dim text_released_color As New SolidColorBrush(Colors.White)

    Public Shared Mytimer As DispatcherTimer = New DispatcherTimer()
    'Public Shared SendInputTimer As DispatcherTimer = New DispatcherTimer()

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


        'Me.WindowState = WindowState.Normal
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



    Sub Check_key()

        Dim curKeyboardState = MyKeyboard.GetCurrentState()
        '键盘检测
        If curKeyboardState.IsPressed(Key.H) Then

            User32Methods.SetActiveWindow((New WindowInteropHelper(Me)).Handle)

            'Dim m_inputs(0 To 1) As Input

            'Input.InitKeyboardInput(m_inputs(0), VirtualKey.ESCAPE, False)
            'Input.InitKeyboardInput(m_inputs(1), VirtualKey.ESCAPE, True)
            'User32Helpers.SendInput(m_inputs)
        End If
        If curKeyboardState.IsPressed(Key.K) Then
            Me.Close()
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
        Me.DragMove()
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


                Dim parent = setup_wpf_window()
                'Dim parent = setup_winform_window()



                'Dim child = New WindowInteropHelper(Me).Handle

                Me.Close()



                'User32Methods.SetParent(child, parent)

                'WinApi.User32.User32Methods.MoveWindow(child, 0, 0, Me.Width, Me.Height, True)

                'Me.Left = 685
                'Me.Top = 460

                ''修正窗口位置
                'Dim dHwnd = User32Methods.FindWindow("PopKart Client", "PopKart Client")
                'Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
                'If User32Methods.GetWindowRect(dHwnd, win_rectangle) Then
                '    Debug.WriteLine("找到窗口")
                '    Me.Left = win_rectangle.Left + 685
                '    Me.Top = win_rectangle.Top + 460
                'End If


            Case "退出"
                Me.Close()
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




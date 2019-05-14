Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Imports System.Windows.Threading
Imports System.Windows.Shapes
Imports SharpDX.DirectInput

Class MainWindow
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public directInput As DirectInput
    Public MyKeyboard As Keyboard

    Public Shared Mytimer As DispatcherTimer = New DispatcherTimer()


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded





        '窗口置顶
        Me.Topmost = True

        Try
            Dim dHwnd As Long
            dHwnd = FindWindow("PopKart Client", vbNullString)


            Dim pRec As RECT
            GetWindowRect(dHwnd, pRec)

            Me.Top = pRec.Left + 700
            Me.Left = pRec.Right + 1100

        Catch ex As Exception




            'MySendKey(&H4800)
        End Try









        directInput = New DirectInput()
        Dim Devices = directInput.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices)
        For Each device In Devices
            If (device.Type = SharpDX.DirectInput.DeviceType.Keyboard) Then
                MyKeyboard = New SharpDX.DirectInput.Keyboard(directInput)
                MyKeyboard.Acquire()
            End If
        Next




        '启动timer
        Mytimer.Interval = New TimeSpan(0, 0, 0, 0, 12)
        AddHandler Mytimer.Tick, AddressOf CheckKeyboard
        Mytimer.Start()



        'If (hKeyboardHook = 0) Then

        '    Me.KeyboardHookProcedure = New HookProc(AddressOf KeyboardHookProc)
        '    Dim curProcess As Process = Process.GetCurrentProcess
        '    Dim curModule As ProcessModule = curProcess.MainModule
        '    hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, Me.KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), Nothing)

        'End If
    End Sub

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Integer
    Private Sub CheckKeyboard(sender As Object, e As EventArgs)




        '-----------
        Dim curKeyboardState = MyKeyboard.GetCurrentState()


        '上方向键
        If curKeyboardState.IsPressed(Key.Up) Then
            Border_up.Background = key_pressd_color
            tup.Foreground = text_pressed_color
            'SendSingleKey(&H4800)


            'Dim dHwnd As Long
            'dHwnd = FindWindow("PopKart Client", vbNullString)


            'Dim pRec As RECT
            'GetWindowRect(dHwnd, pRec)
            'SetForegroundWindow(dHwnd)



            'MySendKey(&H4800)
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






        'Dim curPressedKeys = curKeyboardState.PressedKeys

        'Dim curPressed2Keys = curKeyboardState.


        'Dim Data = MyKeyboard.GetBufferedData()

        'For Each pressed In curPressedKeys





        'Next





    End Sub












    Dim key_pressd_color As New SolidColorBrush(Colors.HotPink)
    Dim key_released_color As New SolidColorBrush(Colors.Black)
    Dim text_pressed_color As New SolidColorBrush(Colors.Black)
    Dim text_released_color As New SolidColorBrush(Colors.White)


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
    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click
        Me.Close()
        End
    End Sub

    Private Sub checkBox_Checked(sender As Object, e As RoutedEventArgs) Handles checkBox.Checked


        mainborder.Background = Brushes.Transparent
        checkBox.Content = "开"
    End Sub

    Private Sub checkBox_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkBox.Unchecked
        'Dim BrushConverter As BrushConverter = New BrushConverter()
        'mainborder.Background = BrushConverter.ConvertFromString("#FFB9A9B9")

        mainborder.Background = New SolidColorBrush（Colors.Goldenrod）
        checkBox.Content = "关"
    End Sub





    Private Const HC_ACTION As Integer = 0
    Private Const WM_KEY_ENTER As Integer = &HD
    Private Const WH_KEYBOARD_LL = 13
    Private Const WM_KEYDOWN As Integer = &H100
    Private Const WM_KeyUP As Integer = &H101
    Private Const WM_SYSKEYDOWN As Integer = &H104
    Private Const WM_SYSKEYUP As Integer = &H105


    Public Delegate Function HookProc(ByVal nCode As Integer, ByVal wParam As Int32, ByVal lParam As IntPtr) As Integer
    Private KeyboardHookProcedure As HookProc
    Private Shared hKeyboardHook As Integer = 0


    Public Declare Function GetModuleHandle Lib "kernel32.dll" Alias "GetModuleHandleA" (ByVal ModuleName As String) As IntPtr

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Public Overloads Shared Function SetWindowsHookEx(ByVal idHook As Integer, ByVal HookProc As HookProc, ByVal hInstance As IntPtr, ByVal wParam As Integer) As Integer
    End Function

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Public Overloads Shared Function CallNextHookEx(ByVal idHook As Integer, ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Public Overloads Shared Function UnhookWindowsHookEx(ByVal idHook As Integer) As Boolean
    End Function


    <StructLayout(LayoutKind.Sequential)>
    Public Class KeyboardHookStruct
        Public vkCode As Integer
        Public scanCode As Integer
        Public flags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Class





    Private Function KeyboardHookProc(ByVal nCode As Integer, ByVal wParam As Int32, ByVal lParam As IntPtr) As Integer
        If nCode >= 0 Then
            Dim MyKeyboardHookStruct As KeyboardHookStruct = CType(Marshal.PtrToStructure(lParam, GetType(KeyboardHookStruct)), KeyboardHookStruct)
            Dim keyData As Key = CType(MyKeyboardHookStruct.vkCode, Key)
            If (wParam = WM_KEYDOWN) OrElse (wParam = WM_SYSKEYDOWN) Then
                Select Case (keyData)
                    '摁下一个键
                    Case 162, 163
                        Border_ctrl.Background = key_pressd_color
                        tctrl.Foreground = text_pressed_color
                    Case 160, 161
                        Border_shift.Background = key_pressd_color
                        tshift.Foreground = text_pressed_color
                    Case 37
                        Border_left.Background = key_pressd_color
                        tleft.Foreground = text_pressed_color
                    Case 38
                        Border_up.Background = key_pressd_color
                        tup.Foreground = text_pressed_color
                    Case 39
                        Border_right.Background = key_pressd_color
                        tright.Foreground = text_pressed_color
                    Case 40
                        Border_down.Background = key_pressd_color
                        tdown.Foreground = text_pressed_color
                End Select
            End If

            If wParam = WM_KeyUP OrElse wParam = WM_SYSKEYUP Then

                ' MsgBox("545454545")
                Select Case (keyData)
                    '摁下一个键  
                    Case 162, 163
                        Border_ctrl.Background = key_released_color
                        tctrl.Foreground = text_released_color
                    Case 160, 161
                        Border_shift.Background = key_released_color
                        tshift.Foreground = text_released_color
                    Case 37
                        Border_left.Background = key_released_color
                        tleft.Foreground = text_released_color
                    Case 38
                        Border_up.Background = key_released_color
                        tup.Foreground = text_released_color
                    Case 39
                        Border_right.Background = key_released_color
                        tright.Foreground = text_released_color
                    Case 40
                        Border_down.Background = key_released_color
                        tdown.Foreground = text_released_color
                End Select

            End If
            'Return 1
        End If

        Return 0
    End Function










    'Public Declare Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hWnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    'Public Declare Function UnregisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hWnd As IntPtr, ByVal id As Integer) As Boolean
    'Private Const WM_HOTKEY As Integer = 786
    Dim handle = New WindowInteropHelper(Me).Handle


    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        Dim retKeyboard As Boolean = True
        If (hKeyboardHook <> 0) Then
            retKeyboard = UnhookWindowsHookEx(hKeyboardHook)
            hKeyboardHook = 0
        End If
        If Not retKeyboard Then
            Throw New Exception("bbbbbbbbbb")
        End If
    End Sub

    Protected Overrides Sub OnSourceInitialized(ByVal e As EventArgs)
        MyBase.OnSourceInitialized(e)
        Dim hwndSource As HwndSource = CType(PresentationSource.FromVisual(Me), HwndSource)
        If (Not (hwndSource) Is Nothing) Then
            hwndSource.AddHook(New HwndSourceHook(AddressOf WndProc))
        End If
    End Sub

    Protected Overridable Function WndProc(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByRef handled As Boolean) As IntPtr

        'Select Case (msg)
        '    '摁下一个键
        '    Case 256
        '        If wParam = 37 Then
        '            Border_left.Background = key_pressd_color
        '            tleft.Foreground = text_pressed_color
        '        ElseIf wParam = 38 Then
        '            Border_up.Background = key_pressd_color
        '            tup.Foreground = text_pressed_color
        '        ElseIf wParam = 39 Then
        '            Border_right.Background = key_pressd_color
        '            tright.Foreground = text_pressed_color
        '        ElseIf wParam = 40 Then
        '            Border_down.Background = key_pressd_color
        '            tdown.Foreground = text_pressed_color
        '        ElseIf wParam = 16 Then
        '            Border_shift.Background = key_pressd_color
        '            tshift.Foreground = text_pressed_color
        '        ElseIf wParam = 17 Then
        '            Border_ctrl.Background = key_pressd_color
        '            tctrl.Foreground = text_pressed_color
        '        End If
        '    '释放一个键
        '    Case 257
        '        If wParam = 37 Then
        '            Border_left.Background = key_released_color
        '            tleft.Foreground = text_released_color
        '        ElseIf wParam = 38 Then
        '            Border_up.Background = key_released_color
        '            tup.Foreground = text_released_color
        '        ElseIf wParam = 39 Then
        '            Border_right.Background = key_released_color
        '            tright.Foreground = text_released_color
        '        ElseIf wParam = 40 Then
        '            Border_down.Background = key_released_color
        '            tdown.Foreground = text_released_color
        '        ElseIf wParam = 16 Then
        '            Border_shift.Background = key_released_color
        '            tshift.Foreground = text_released_color
        '        ElseIf wParam = 17 Then
        '            Border_ctrl.Background = key_released_color
        '            tctrl.Foreground = text_released_color
        '        End If
        'End Select
        Return IntPtr.Zero
    End Function




End Class




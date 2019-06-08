Imports System.Windows.Forms
Imports System.Windows.Interop
Imports WinApi.User32
Public Class Form1
    Const GWL_EXSTYLE = -20
    Const WS_EX_TRANSPARENT = &H20
    Const WS_EX_LAYERED = &H80000
    Const LWA_ALPHA = 2
    Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.TopMost = True
        Me.AllowTransparency = True
        'Me.TransparencyKey = System.Drawing.Color.White
        AddHandler Me.Load, AddressOf ContainerLoaded
        'AddHandler Me.KeyDown, AddressOf ContainerKeydown
        'win32api.SetWindowTransparent.SetOpacity(Me.Handle, 100)
        'User32Methods.SetWindowLongPtr((New WindowInteropHelper(Me)).Handle, GWL_EXSTYLE, User32Methods.GetWindowLongPtr(New WindowInteropHelper(Me).Handle, GWL_EXSTYLE) Or WS_EX_TRANSPARENT Or WS_EX_LAYERED)
        'SetLayeredWindowAttributes(this.Handle, 0, 100, LWA_ALPHA);
        '        this.BackColor = Color.Black;
        'Dim bf = New BlendFunction()
        'bf.BlendOp = 1 'AC_SRC_OVER
        'bf.SourceConstantAlpha = 255
        'bf.AlphaFormat = 1 'AC_SRC_ALPHA
        'bf.BlendFlags = 0
        'User32Methods.SetWindowLongPtr(,)
        'User32Methods.UpdateLayeredWindow((New WindowInteropHelper(Me)).Handle, Nothing, Nothing, Nothing, Nothing, Nothing, 80, bf, 8)
        'AddHandler Me.MouseDown, AddressOf MyMouseDown
    End Sub
    Private Sub ContainerKeydown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Key.H Then
            If Me.FormBorderStyle = FormBorderStyle.Sizable Then
                Dim child = Me.Handle
                Dim Style = User32Methods.GetWindowLongPtr(child, (-16))
                Style = Style - WindowStyles.WS_CAPTION - WindowStyles.WS_BORDER
                User32Methods.SetWindowLongPtr(child, (-16), Style)
                Me.FormBorderStyle = FormBorderStyle.None
            Else
                Me.FormBorderStyle = FormBorderStyle.Sizable
            End If
        End If
    End Sub
    Private Sub MouseEnterED(sender As Object, e As EventArgs)
    End Sub
    Public Sub MakeTransparent()
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Dim ret As Long = User32Methods.GetWindowLongPtr(Me.Handle, GWL_EXSTYLE)
        ret = ret Or WS_EX_LAYERED
        User32Methods.SetWindowLongPtr(Me.Handle, GWL_EXSTYLE, ret)
        User32Methods.SetLayeredWindowAttributes(Me.Handle, RGB(System.Drawing.Color.WhiteSmoke.R, System.Drawing.Color.WhiteSmoke.G, System.Drawing.Color.WhiteSmoke.B), 0, LayeredWindowAttributeFlag.LWA_COLORKEY)
    End Sub
    Public Sub showTitle(sender As Object, e As EventArgs)
        Me.FormBorderStyle = FormBorderStyle.Fixed3D
    End Sub
    Private Sub hideTitle(sender As Object, e As EventArgs)
        Me.FormBorderStyle = FormBorderStyle.Fixed3D
    End Sub
    'Private Sub MyMouseDown(sender As Object, e As MouseEventArgs)
    '    Me.DoDragDrop()
    'End Sub
    Private Sub ContainerLoaded(sender As Object, e As EventArgs)
        'Dim father = Me.Handle 'User32Methods.FindWindow("Form1", "PopKart Client")
        ''Dim child = User32Methods.FindWindow("Chrome_WidgetWin_1", "Chrome_WidgetWin_1")
        'Dim child = User32Methods.FindWindow("PopKart Client", "PopKart Client")
        'Dim Style = User32Methods.GetWindowLongPtr(child, (-16))
        'Style = Style - WindowStyles.WS_CAPTION - WindowStyles.WS_BORDER
        'User32Methods.SetWindowLongPtr(child, (-16), Style)
        ''重设容器尺寸
        'Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        'If User32Methods.GetWindowRect(child, win_rectangle) Then
        '    Me.Left = win_rectangle.Left
        '    Me.Top = win_rectangle.Top
        '    Me.Width = win_rectangle.Width + 10
        '    Me.Height = win_rectangle.Height + 40
        'End If
        'Dim combine = User32Methods.SetParent(child, father)
        'WinApi.User32.User32Methods.MoveWindow(child, 0, 0, win_rectangle.Width, win_rectangle.Height, False)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class
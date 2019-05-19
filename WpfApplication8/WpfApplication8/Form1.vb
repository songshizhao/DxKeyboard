Imports System.Windows.Forms
Imports System.Windows.Interop
Imports WinApi.User32

Public Class Form1

    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        AddHandler Me.Load, AddressOf ContainerLoaded


        'AddHandler Me.MouseDown, AddressOf MyMouseDown
    End Sub

    'Private Sub MyMouseDown(sender As Object, e As MouseEventArgs)
    '    Me.DoDragDrop()
    'End Sub

    Private Sub ContainerLoaded(sender As Object, e As EventArgs)
        Dim father = Me.Handle 'User32Methods.FindWindow("Form1", "PopKart Client")

        'Dim child = User32Methods.FindWindow("Chrome_WidgetWin_1", "Chrome_WidgetWin_1")

        Dim child = User32Methods.FindWindow("PopKart Client", "PopKart Client")

        Dim Style = User32Methods.GetWindowLongPtr(child, (-16))
        Style = Style - WindowStyles.WS_CAPTION - WindowStyles.WS_BORDER
        User32Methods.SetWindowLongPtr(child, (-16), Style)
        '重设容器尺寸
        Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        If User32Methods.GetWindowRect(child, win_rectangle) Then
            Me.Left = win_rectangle.Left
            Me.Top = win_rectangle.Top

            Me.Width = win_rectangle.Width + 10
            Me.Height = win_rectangle.Height + 40
        End If

        Dim combine = User32Methods.SetParent(child, father)

        WinApi.User32.User32Methods.MoveWindow(child, 0, 0, win_rectangle.Width, win_rectangle.Height, False)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
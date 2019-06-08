Imports System.Windows.Controls.Primitives
Imports System.Windows.Interop
Imports WinApi.User32
Public Class Container
    Public MyConainer As UIElement
    Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        AddHandler Me.Loaded, AddressOf ContainerLoaded
        MyConainer = innerContainer
    End Sub
    Private Sub ContainerLoaded(sender As Object, e As RoutedEventArgs)
        Dim rr = TryCast(PresentationSource.FromVisual(innerContainer), HwndSource)
        Dim father = rr.Handle '(New WindowInteropHelper(Me)).Handle
        Dim child = User32Methods.FindWindow("PopKart Client", "PopKart Client")
        'Dim child = User32Methods.FindWindow("Notepad", Nothing)
        '重设容器尺寸
        Dim win_rectangle As NetCoreEx.Geometry.Rectangle = New NetCoreEx.Geometry.Rectangle()
        If User32Methods.GetWindowRect(child, win_rectangle) Then
            'innerContainer.Width = win_rectangle.Width
            'innerContainer.Height = win_rectangle.Height + 140
        End If
        '取消child的标题
        Dim Style = User32Methods.GetWindowLongPtr(child, (-16))
        Style = Style - WindowStyles.WS_CAPTION - WindowStyles.WS_BORDER
        User32Methods.SetWindowLongPtr(child, (-16), Style)
        Dim combine = User32Methods.SetParent(child, father)
        '移动child的位置
        WinApi.User32.User32Methods.MoveWindow(child, 0, 0, win_rectangle.Width, win_rectangle.Height, False)
        'MessageBox.Show($"father:{father}child:{child}")
    End Sub
    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Try
            Me.DragMove()
        Catch ex As Exception
        End Try
    End Sub
End Class

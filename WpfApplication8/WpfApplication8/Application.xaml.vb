Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。


    Sub New()


#If Not DEBUG Then
        
        
#End If
        AddHandler Startup, CheckAdministrator()

    End Sub



    Public Function CheckAdministrator() As StartupEventHandler
        Dim wi = System.Security.Principal.WindowsIdentity.GetCurrent()
        Dim wp = New System.Security.Principal.WindowsPrincipal(wi)

        Dim runAsAdmin = wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)

        If (runAsAdmin = False) Then

            Dim ProcessInfo = New ProcessStartInfo(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
            ProcessInfo.UseShellExecute = True
            ProcessInfo.Verb = "runas"


            Try

                Process.Start(ProcessInfo)


            Catch ex As Exception

                MessageBox.Show("程序自动以管理员身份运行出错，请手动设置以管理员身份运行程序" + ex.ToString)
                Throw



            End Try

            Environment.Exit(0)


        End If

    End Function






End Class

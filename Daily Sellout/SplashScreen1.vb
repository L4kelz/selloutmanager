Public NotInheritable Class SplashScreen1

    Private iProgressBarValue As Integer
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Timer1.Interval = 5000
        'ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = 1000
        'ProgressBar1.Value = 0
        'Timer1.Interval = 5
        Timer1.Enabled = True
        'Timer1.Interval = 10
        'Timer1.Start()



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Panel2.Width += 3
        If Panel2.Width >= 700 Then

            Timer1.Stop()

        End If






    End Sub




End Class

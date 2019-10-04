Public Class CloseOutlookMessage

    Private countDownFrom As New TimeSpan(0, 15, 0) 'ten seconds
    Private stpw As New Stopwatch

    Private Sub CloseOutlookMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Timer_Countdown.Enabled = True
        Timer_Countdown.Start()
        stpw.Reset()
        stpw.Start()
        cmb_pospone.SelectedItem = cmb_pospone.Items(0)
    End Sub

    Private Sub Timer_Countdown_Tick(sender As Object, e As EventArgs) Handles Timer_Countdown.Tick

        If stpw.Elapsed <= countDownFrom Then
            Dim toGo As TimeSpan = countDownFrom - stpw.Elapsed
            lbl_Timercountdown.Text = String.Format("{0:00}:{1:00}:{2:00}", toGo.Hours, toGo.Minutes, toGo.Seconds)
        Else
            BackupScheduler.RunBackup()
            Me.Dispose()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectedvalue As String = cmb_pospone.SelectedItem
        If Not IsNothing(selectedvalue) Then
            Try
                If selectedvalue.Contains("min.") Then
                    Dim minselected As Integer = Integer.Parse(selectedvalue.Substring(0, 2))
                    BackupScheduler.SetCountdown(0, minselected)
                Else
                    Dim hrselected As Integer = Integer.Parse(selectedvalue.Substring(0, 1))
                    BackupScheduler.SetCountdown(hrselected, 0)
                End If

                BackupScheduler.StartCountDown()
                Me.Dispose()

            Catch ex As Exception
                InitRunner.LogEvents.Add("Error in module CloseOutlookMessage:Button1_click: Error > " & ex.Message.ToString)
            End Try
            
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Obtiene el parametro del la fecha de la configuracion
        Dim datebackup As Date = Date.Now
        Dim daystoaadd As Integer = Integer.Parse(InitRunner.ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue)
        Dim getHrsToRun As String() = InitRunner.ConfigurationDATFile.Item("BackupStarTime").ParameterValue.Split(":")

        'Agrega los dias configurados a la fecha actual
        Dim datenextbackup As Date = datebackup.AddDays(daystoaadd)

        'Crea fecha de siguiente backup.
        Dim hr As Integer = Integer.Parse(getHrsToRun(0))
        Dim min As Integer = Integer.Parse(getHrsToRun(1))
        Dim seg As Integer = Integer.Parse(getHrsToRun(2))
        Dim nextdatebacup As DateTime
        nextdatebacup = New DateTime(datenextbackup.Year, datenextbackup.Month, datenextbackup.Day, hr, min, seg)

        InitRunner.NextDateBackup = nextdatebacup
        InitRunner.SchedulerFrmopen = False
        BackupScheduler.Dispose()
        Me.Dispose()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BackupScheduler.RunBackup()
        Me.Dispose()
    End Sub
End Class
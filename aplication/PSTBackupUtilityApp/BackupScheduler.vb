Public Class BackupScheduler

    Private _countDown As TimeSpan
    Private stpw As New Stopwatch
    Private CloseOutlookMessage As CloseOutlookMessage
    Private _ConfigurationDATFile As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    Private _PSTFiles As New PSTBacktupUtilityCore.com.lib.objects.FileObjCollection
    Private _PSTindexSelect = 0
    Public Property CountDownPospone As TimeSpan
        Get
            Return _countDown
        End Get
        Set(value As TimeSpan)
            _countDown = value
        End Set
    End Property

    ''Funcion de inicio
    ''Oculta el formulario a la vista y inicia el reloj de apertura de la aplicacion de AMOS.
    Protected Overrides Sub SetVisibleCore(ByVal value As Boolean)
        ''Funcion de ocultar el formulario
        If Not Me.IsHandleCreated Then
            Me.CreateHandle()
            value = False
        End If
        CloseOutlookMessage = New CloseOutlookMessage
        CloseOutlookMessage.Show()
    End Sub

    Public Sub StartCountDown()
        Timer_Pospne_Countdown.Enabled = True
        Timer_Pospne_Countdown.Start()
        stpw.Reset()
        stpw.Start()
    End Sub

    Private Sub Timer_Pospne_Countdown_Tick(sender As Object, e As EventArgs) Handles Timer_Pospne_Countdown.Tick
        If stpw.Elapsed >= _countDown Then
            CloseOutlookMessage = New CloseOutlookMessage
            CloseOutlookMessage.Show()
            stpw.Stop()
            Timer_Pospne_Countdown.Stop()
        Else
            Dim toGo As TimeSpan = _countDown - stpw.Elapsed
            Console.WriteLine(String.Format("{0:00}:{1:00}:{2:00}", toGo.Hours, toGo.Minutes, toGo.Seconds))
        End If
    End Sub

    Public Sub SetCountdown(hr As Integer, min As Integer)
        _countDown = New TimeSpan(hr, min, 0)
    End Sub

    Public Sub RunBackup()

        'Empieza a revisar si esta outlook activo para cerrarlo.
        CloseOutlookApp()
        Timer_CheckOutlookOpen.Enabled = True
        Timer_CheckOutlookOpen.Start()

        _PSTFiles.Items.Clear()
        Dim pstfiles As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")

        For Each Val As String In pstfiles
            Dim fileinfo As New IO.FileInfo(Val)
            Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(fileinfo)
            _PSTFiles.Add(fileobj)
            _ConfigurationDATFile = InitRunner.ConfigurationDATFile
        Next

        If InitRunner.EnoughSpaceToBackup(_PSTFiles.Items(_PSTindexSelect)) Then
            RunbackupFile(_PSTFiles.Items(_PSTindexSelect))
            InitRunner.LogEvents.Add("Running backup of file >" & _PSTFiles.Items(_PSTindexSelect).FullPathFile)
            Timer_Backup.Enabled = True
            Timer_Backup.Start()
        Else
            InitRunner.LogEvents.Add("Error creating backup, Error> not enough space in disk ")
            InitRunner.NotifyIcon1.ShowBalloonTip(2000, "Backup Error", "There is not enough space on your destination drive, please make space on your drive or select a diferent destination to make your bacups.", ToolTipIcon.Error)
            'Actualiza la fecha ultima de respaldo realizado
            InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            InitRunner.UpdatedDTOConfigFile()
            InitRunner.CheckLasBackupDate()
            InitRunner.SchedulerFrmopen = False

            Me.Dispose()
        End If


    End Sub

    Public Sub RunbackupFile(fileobj As PSTBacktupUtilityCore.com.lib.objects.FileObj)
        Dim BackProcess As New BackupProcesFormS
        BackProcess.Configuration = _ConfigurationDATFile
        BackProcess.FileToCompress = fileobj
        BackProcess.Show()

    End Sub

    Private Sub Timer_Backup_Tick(sender As Object, e As EventArgs) Handles Timer_Backup.Tick
        If Not InitRunner.ManuallyCancelled Then
            Try
                If _PSTindexSelect > (_PSTFiles.Count - 1) Then
                    Timer_Backup.Stop()
                    Timer_CheckOutlookOpen.Stop()
                    _PSTindexSelect = 0
                    InitRunner.LogEvents.Add("Backup Process Finished.")
                    'Actualiza la fecha ultima de respaldo realizado
                    InitRunner.LogEvents.Add("Updates the LastBackupDate configuration from " & InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue & " to " & Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    InitRunner.UpdatedDTOConfigFile()
                    InitRunner.CheckLasBackupDate()
                    InitRunner.SchedulerFrmopen = False

                    Dim psi As ProcessStartInfo = New ProcessStartInfo("outlook.exe")
                    Process.Start(psi)
                    Me.Dispose()
                Else
                    If IsNothing(Application.OpenForms.Item("BackupProcesFormS")) Then
                        _PSTindexSelect = _PSTindexSelect + 1
                        RunbackupFile(_PSTFiles.Items(_PSTindexSelect))
                        InitRunner.LogEvents.Add("Creating a backup of " & _PSTFiles.Items(_PSTindexSelect).FullPathFile)
                    End If
                End If

            Catch ex As Exception
                'Console.WriteLine(ex.Message.ToString)
                InitRunner.LogEvents.Add("Error in module BackupScheduler: Timer_Backup_Tick: Error > " & ex.Message.ToString)
            End Try
        Else
            Timer_Backup.Stop()
            Timer_Backup.Enabled = False
            InitRunner.ManuallyCancelled = False
        End If
    End Sub

   
    Private Sub Timer_CheckOutlookOpen_Tick(sender As Object, e As EventArgs) Handles Timer_CheckOutlookOpen.Tick
        If OutlookIsOpen() Then
            CloseOutlookApp()
        End If
    End Sub

    Public Function OutlookIsOpen() As Boolean
        Dim isopen As Boolean = True
        Dim PS() As Process = Process.GetProcessesByName("OUTLOOK")
        If PS.Length > 0 Then
            isopen = True
        End If
        Return isopen
    End Function

    Public Sub CloseOutlookApp()
        Dim outlook_active As Boolean = False

        outlook_active = OutlookIsOpen()

        If outlook_active Then
            Dim proc = Process.GetProcessesByName("OUTLOOK")
            For i As Integer = 0 To proc.Count - 1
                proc(i).CloseMainWindow()
                'proc(i).WaitForExit()

            Next i
        End If

    End Sub
End Class
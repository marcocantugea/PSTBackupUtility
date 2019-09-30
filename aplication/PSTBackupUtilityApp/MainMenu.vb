Public Class MainMenu


    Private _BackupManuallyThreat As Threading.Thread
    Private _ConfigurationDATFile As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    'Private _ThreatBacklist As New Collection
    'Private _Threatfinis As Boolean = False
    Private _PSTFiles As New PSTBacktupUtilityCore.com.lib.objects.FileObjCollection
    Private _PSTindexSelect = 0
    Public nextbackup As Date

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.CenterToScreen()
        'Actualiza las Fechas en las cajas de texto
        DisplayBackupDates()
    End Sub

    Private Sub MainMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim configurator As New Configurator
        configurator.Show()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.ControlBox = False

        Timer_CheckOutlookOpen.Enabled = True
        Timer_CheckOutlookOpen.Start()

        If Not Timer_RunBackup.Enabled Then
            RunManuallyBackUp()
            InitRunner.LogEvents.Add("Start manually backup process.")
        End If

    End Sub

    Public Sub RunManuallyBackUp()
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
            InitRunner.LogEvents.Add("Creating a backup of " & _PSTFiles.Items(_PSTindexSelect).FullPathFile)
            Timer_RunBackup.Enabled = True
            Timer_RunBackup.Start()
        Else
            InitRunner.LogEvents.Add("Error creating a backup, Error > not enough space in disk >" & InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue)
            MsgBox("There is no Enough space to make the backup of you file, please make a space or select a diferent unit on the configuration option.", MsgBoxStyle.Critical, "Fail to make a backup.")
        End If
    End Sub

    Public Sub RunbackupFile(fileobj As PSTBacktupUtilityCore.com.lib.objects.FileObj)
        Dim BackProcess As New BackupProcesFormS
        BackProcess.Configuration = _ConfigurationDATFile
        BackProcess.FileToCompress = fileobj
        BackProcess.Show()

    End Sub
    Public Sub RunbackupFile()
        Dim BackProcess As New BackupProcesFormS
        BackProcess.Configuration = _ConfigurationDATFile
        BackProcess.Show()

    End Sub

    Private Sub Timer_RunBackup_Tick(sender As Object, e As EventArgs) Handles Timer_RunBackup.Tick
        If Not InitRunner.ManuallyCancelled Then
            If Not InitRunner.OutlookIsOpen Then
                Try
                    If _PSTindexSelect > (_PSTFiles.Count - 1) Then

                        'Termina el proceso de respaldo.
                        Timer_RunBackup.Stop()
                        _PSTindexSelect = 0

                        InitRunner.LogEvents.Add("Backup Process Finished.")

                        'Actualiza la fecha ultima de respaldo realizado
                        InitRunner.LogEvents.Add("Updates the LastBackupDate configuration from " & InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue & " to " & Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        InitRunner.UpdatedDTOConfigFile()
                        InitRunner.CheckLasBackupDate()
                        DisplayBackupDates()
                        Timer_CheckOutlookOpen.Stop()
                        Me.ControlBox = True
                    Else
                        If IsNothing(Application.OpenForms.Item("BackupProcesFormS")) Then
                            _PSTindexSelect = _PSTindexSelect + 1
                            RunbackupFile(_PSTFiles.Items(_PSTindexSelect))
                            InitRunner.LogEvents.Add("Creating a backup of " & _PSTFiles.Items(_PSTindexSelect).FullPathFile)
                        End If
                    End If

                Catch ex As Exception
                    InitRunner.LogEvents.Add("Error in module MainMenu:Timer_RunBackup_Tick:Error > " & ex.Message.ToString)
                End Try
            End If
        Else
            Timer_RunBackup.Stop()
            Timer_RunBackup.Enabled = False
            InitRunner.ManuallyCancelled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openfolder As String = "explorer " & InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue
        Shell(openfolder, AppWinStyle.NormalFocus)
    End Sub

    Private Sub MainMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Try
        '    'Revisa si ya existen respaldos
        '    Dim lastBackupDate As Date = Date.Parse(InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue)

        'Catch ex As Exception
        '    'Pregunta si desea realizar un respaldo 
        '    Dim valuebox As Integer = MsgBox("Do you want to create a Backup of your Outlook Email PST files?", MsgBoxStyle.OkCancel, "Creating Backups.")
        '    If valuebox = vbOK Then

        '    End If
        'End Try
    End Sub

    Public Sub DisplayBackupDates()
        'Obtiene la ultima fecha de respaldo generado
        Dim lastdatebackup As Date
        Try
            'Obtiene la ultima fecha del respaldo generado
            lastdatebackup = Date.Parse(InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue)
            lbl_lastbackupdate.Text = lastdatebackup.ToString("dd-MMM-yyyy hh:mm:ss")
        Catch ex As Exception
            'Si no existe agrega en la etiqueta que no hay backups realizados.
            lbl_lastbackupdate.Text = " Pending to make a backup."
        End Try
        'Obtiene la fecha del siguiente respaldo
        nextbackup = InitRunner.NextDateBackup
        lbl_nextbackup.Text = nextbackup.ToString("dd-MMM-yyyy hh:mm:ss")
    End Sub

    Private Sub Timer_CheckOutlookOpen_Tick(sender As Object, e As EventArgs) Handles Timer_CheckOutlookOpen.Tick
        If InitRunner.OutlookIsOpen Then
            InitRunner.CloseOutlookApp()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim RestoreBackupFrm As New RestoreBackupProcess
        'RestoreBackupFrm.Configuration = InitRunner.ConfigurationDATFile

        'Dim filetorestore As New IO.FileInfo("E:\bk\destination\LM4PCVIT01-tooher49-09282019043443.rar")
        'Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(filetorestore)
        'RestoreBackupFrm.FileToRestore = fileobj
        'RestoreBackupFrm.DestinationPath = "E:\"
        'RestoreBackupFrm.RestoreToDestinationPath = False

        'RestoreBackupFrm.Show()

        Dim RecoverOptions As New RecoverOptions
        RecoverOptions.Show()


    End Sub
End Class
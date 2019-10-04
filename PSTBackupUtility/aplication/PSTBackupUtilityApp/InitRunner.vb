Public Class InitRunner

    Private _ConfigurationDATFile As New Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    Private _PSTFound As PSTBacktupUtilityCore.com.lib.objects.FileObjCollection
    Private _CloseApp As Boolean = False
    Private _ManuallyCancelled As Boolean = False
    Private _NextDateBackup As Date
    Private _SchedulerFrmopen As Boolean = False
    Private _LogEvents As PSTBacktupUtilityCore.com.lib.process.LogControl

    Public ReadOnly Property LogEvents As PSTBacktupUtilityCore.com.lib.process.LogControl
        Get
            Return _LogEvents
        End Get
    End Property

    Public Property NextDateBackup As Date
        Get
            Return _NextDateBackup
        End Get
        Set(value As Date)
            _NextDateBackup = value
        End Set
    End Property

    Public Property SchedulerFrmopen As Boolean
        Get
            Return _SchedulerFrmopen
        End Get
        Set(value As Boolean)
            _SchedulerFrmopen = value
        End Set
    End Property

    Public Property ManuallyCancelled As Boolean
        Get
            Return _ManuallyCancelled
        End Get
        Set(value As Boolean)
            _ManuallyCancelled = value
        End Set
    End Property

    Public Property CloseApp As Boolean
        Get
            Return _CloseApp
        End Get
        Set(value As Boolean)
            _CloseApp = value
        End Set
    End Property

    Public Property PSTFoundList As PSTBacktupUtilityCore.com.lib.objects.FileObjCollection
        Get
            Return _PSTFound
        End Get
        Set(value As PSTBacktupUtilityCore.com.lib.objects.FileObjCollection)
            _PSTFound = value
        End Set
    End Property

    Public Sub AddItemToConfiguration(value As String, configobj As PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
        If value.Count > 0 And Not IsNothing(configobj) Then
            _ConfigurationDATFile.Add(value, configobj)
        End If
    End Sub

    Public Property ConfigurationDATFile As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
        Get
            Return _ConfigurationDATFile
        End Get
        Set(value As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj))
            _ConfigurationDATFile = value
        End Set
    End Property


    Private Sub InitRunner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    

    ''Funcion de inicio
    ''Oculta el formulario a la vista y inicia el reloj de apertura de la aplicacion de AMOS.
    Protected Overrides Sub SetVisibleCore(ByVal value As Boolean)

        ''Funcion de ocultar el formulario
        If Not Me.IsHandleCreated Then
            Me.CreateHandle()
            value = False
        End If


        'Instrucciones de carga de app.
        Dim LoadProcess As New LoadProcess
        LoadProcess.Show()

        'Dim MainMenu As New MainMenu
        'MainMenu.Show()
        LoadProcess.Focus()

        Timer_UpdateApp.Start()

    End Sub

    'Temporizador global de la aplicacion se acciona cada segundo.
    Private Sub _Global_Timer_Tick(sender As Object, e As EventArgs) Handles _Global_Timer.Tick
        ' Accion para cerrar la aplicacion en caso de que se le de la instruccion.
        If CloseApp Then
            Application.Exit()
        End If
        'revisa si la hr actual es igual o mayor a la del siguiente respaldo
        If Date.Now >= _NextDateBackup Then
            If Not _SchedulerFrmopen Then
                RunScheduleTask()
                _LogEvents.Add("Running Schedule backup.")
            End If
        End If

        'Actualiza el log de eventos
        If IsNothing(_LogEvents) Then
            If _ConfigurationDATFile.ContainsKey("LoggerPath") Then
                Dim filelog As New IO.FileInfo(_ConfigurationDATFile.Item("LoggerPath").ParameterValue & "\PSTBackupUtilityLog.txt")
                Dim filelog_obj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(filelog)
                If IO.File.Exists(filelog_obj.FullPathFile) Then
                    _LogEvents = New PSTBacktupUtilityCore.com.lib.process.LogControl(filelog_obj)
                    _LogEvents.Add("PST Backup utility open. " & vbNewLine)
                    _LogEvents.Add("Opening Log Object in the application. " & vbNewLine)
                Else
                    _LogEvents = New PSTBacktupUtilityCore.com.lib.process.LogControl(filelog_obj)
                    _LogEvents.Add("PST Backup utility open. " & vbNewLine)
                    _LogEvents.Add("Opening of the log file. " & vbNewLine)
                    _LogEvents.UpdateLog()
                End If
                
            End If
        Else
            _LogEvents.UpdateLog()
        End If

    End Sub

    Private Sub ToolStripMenuItem_Exit_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Exit.Click
        'Manda la instruccion para cerrar la aplicacion.
        _CloseApp = True
    End Sub

    Private Sub ToolStripMenuItem_Dashboard_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Dashboard.Click
        'Abre el formulario del menu principal
        If IsNothing(Application.OpenForms.Item("MainMenu")) Then
            Dim MainMenu As New MainMenu
            MainMenu.Show()
        Else
            Application.OpenForms.Item("MainMenu").WindowState = FormWindowState.Normal
            Application.OpenForms.Item("MainMenu").Focus()
        End If
    End Sub

    Private Sub ToolStripMenuItem_Config_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Config.Click
        'Abre el formulario de la configuracion.
        If IsNothing(Application.OpenForms.Item("Configurator")) Then
            Dim Configurator As New Configurator
            Configurator.Show()
        Else
            Application.OpenForms.Item("Configurator").WindowState = FormWindowState.Normal
            Application.OpenForms.Item("Configurator").Focus()
        End If
        

    End Sub

    Public Sub UpdatedDTOConfigFile()
        'Crea el nombre del archivo de la configuracion
        Dim DATConfigFile = Application.StartupPath.ToString & "\AppConfig.dto"
        'Crea un objeto Fileobj para crear el archivo de configuracion
        Dim DATConfigObj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(DATConfigFile))
        'Se Define el creador de archivo
        Dim TextFileWriter As New PSTBacktupUtilityCore.com.lib.file.TextFileWriter(DATConfigObj)

        'Se escriben los paramentros actualizados
        For Each item As KeyValuePair(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj) In _ConfigurationDATFile
            Dim configvalue As PSTBacktupUtilityCore.com.lib.objects.ConfigObj = CType(item.Value, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
            TextFileWriter.AddText(configvalue.Parameter & "=" & configvalue.ParameterValue & vbNewLine)
        Next

        'Crea y escribe archivo
        TextFileWriter.CreateFile()

        'Crear un archivo de respaldo en la carpeta destino.
        Dim BkConfigFile = ConfigurationDATFile.Item("BackupDestination").ParameterValue & "\AppConfig.dto"
        Dim BkConfigFileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(BkConfigFile))
        Dim TextFileWriterBk As New PSTBacktupUtilityCore.com.lib.file.TextFileWriter(BkConfigFileobj)
        For Each item As KeyValuePair(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj) In ConfigurationDATFile
            Dim configvalue As PSTBacktupUtilityCore.com.lib.objects.ConfigObj = CType(item.Value, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
            TextFileWriterBk.AddText(configvalue.Parameter & "=" & configvalue.ParameterValue & vbNewLine)
        Next

        TextFileWriterBk.CreateFile()
    End Sub

    Public Sub CloseOutlookApp()
        Dim outlook_active As Boolean = False
       
        outlook_active = OutlookIsOpen()

        If outlook_active Then
            Dim proc = Process.GetProcessesByName("OUTLOOK")
            For i As Integer = 0 To proc.Count - 1
                proc(i).CloseMainWindow()

                'If OutlookIsOpen() Then
                '    MsgBox("The Microsof Outlook is Open Please close the application to continue with the backup.", MsgBoxStyle.Exclamation, "PST Backup utility")
                'End If
                'proc(i).WaitForExit()

            Next i
        End If

    End Sub

    Public Function OutlookIsOpen() As Boolean
        Dim isopen As Boolean = False
        Dim PS() As Process = Process.GetProcessesByName("OUTLOOK")
        If PS.Length > 0 Then
            isopen = True
        End If
        Return isopen
    End Function

    Public Sub RunScheduleTask()
        'Abre el fromulario oculto de BackupScheduler
        Dim scheduler As New BackupScheduler
        scheduler.Show()
        _SchedulerFrmopen = True
    End Sub

    Public Sub CheckLasBackupDate()
        Dim lastDateBackup As Date
        'Revisa la ultima fecha de respaldo.
        Try
            'Obtiene la ultima fecha de respaldo
            lastDateBackup = Date.Parse(ConfigurationDATFile.Item("LastBackupDate").ParameterValue)

        Catch ex As Exception
            lastDateBackup = Date.Now
        End Try

        'si no existe la fecha de respaldo actualiza la fecha a la actual.
        'calcula la siguente fecha para generar backup
        Dim datenow As Date = lastDateBackup
        Dim daystoaadd As Integer = Integer.Parse(ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue)
        Dim getHrsToRun As String() = ConfigurationDATFile.Item("BackupStarTime").ParameterValue.Split(":")

        'Agrega los dias configurados a la recha actual
        Dim datenextbackup As Date = datenow.AddDays(daystoaadd)

        'Crea fecha de siguiente backup.
        Dim hr As Integer = Integer.Parse(getHrsToRun(0))
        Dim min As Integer = Integer.Parse(getHrsToRun(1))
        Dim seg As Integer = Integer.Parse(getHrsToRun(2))
        Dim nextdatebacup As DateTime
        nextdatebacup = New DateTime(datenextbackup.Year, datenextbackup.Month, datenextbackup.Day, hr, min, seg)

        _NextDateBackup = nextdatebacup
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        If IsNothing(Application.OpenForms.Item("MainMenu")) Then
            Dim MainMenu As New MainMenu
            MainMenu.Show()
        Else
            Application.OpenForms.Item("MainMenu").WindowState = FormWindowState.Normal
            Application.OpenForms.Item("MainMenu").Focus()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IsNothing(Application.OpenForms.Item("RecoverOptions")) Then
            Dim RecoverOptions As New RecoverOptions
            RecoverOptions.Show()
        Else
            Application.OpenForms.Item("RecoverOptions").WindowState = FormWindowState.Normal
            Application.OpenForms.Item("RecoverOptions").Focus()

        End If

    End Sub

    Public Function CheckDiskAvailableSpace(path As String) As Double
        Dim availablespace As Double

        Dim AllDrives As IO.DriveInfo() = IO.DriveInfo.GetDrives
        For Each drive As IO.DriveInfo In AllDrives
            If drive.Name.Equals(path) Then
                availablespace = drive.AvailableFreeSpace
            End If
        Next

        Return availablespace
    End Function

    Public Function EnoughSpaceToBackup(fileobj As PSTBacktupUtilityCore.com.lib.objects.FileObj) As Boolean
        Dim enoughspace As Boolean = True

        Dim sizeavailable As Double = CheckDiskAvailableSpace(ConfigurationDATFile.Item("BackupDestination").ParameterValue.Substring(0, 3))
        Dim sizeavailable_inkb As Double = sizeavailable / 1024
        Dim sizeavailable_inmb As Double = sizeavailable_inkb / 1024

        If sizeavailable_inmb < fileobj.SizeFileinMB Then
            enoughspace = False
        End If

        Return enoughspace
    End Function



    Private Sub Timer_UpdateApp_Tick(sender As Object, e As EventArgs) Handles Timer_UpdateApp.Tick
        Dim checkforupdates As Boolean
        Try
            checkforupdates = Boolean.Parse(_ConfigurationDATFile.Item("CheckForUpdates").ParameterValue)
            If checkforupdates Then
                Try
                    _LogEvents.Add("Run Process to update the application.")
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Try
                    Dim psi As ProcessStartInfo = New ProcessStartInfo(Application.StartupPath & "\PSTBackupUpdater.exe")
                    Process.Start(psi)
                Catch ex As Exception
                End Try
                
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
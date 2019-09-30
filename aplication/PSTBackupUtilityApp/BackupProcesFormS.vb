Public Class BackupProcesFormS

    Private psi As ProcessStartInfo
    Private cmd As Process
    Private _configuration As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    Private _filetocompress As PSTBacktupUtilityCore.com.lib.objects.FileObj
    Private _ProcessFinished As Boolean = False
    Private _FileSize As Long
    Private _FileSizeCalculated As Long
    Private _ZipFile As PSTBacktupUtilityCore.com.lib.objects.FileObj
    Private _DateStartBackup As Date
    Private _stpw As Stopwatch = Stopwatch.StartNew
    Public Property Configuration As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
        Get
            Return _configuration
        End Get
        Set(value As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj))
            _configuration = value
        End Set
    End Property

    Public Property FileToCompress As PSTBacktupUtilityCore.com.lib.objects.FileObj
        Get
            Return _filetocompress
        End Get
        Set(value As PSTBacktupUtilityCore.com.lib.objects.FileObj)
            _filetocompress = value
        End Set
    End Property

    Public Sub RunCMDBackup()
        'Configura el path de donde estal winrar.
        Dim _WINRARCLI As String
        _WINRARCLI = _configuration.Item("CompressorPath").ParameterValue

        'Configura los controles del formulario
        lbl_ProcessedFileLabel.Text = "Processing File > " & _filetocompress.FullPathFile
        _FileSize = _filetocompress.SizeFileinMB
        _FileSizeCalculated = _FileSize * 0.77
        ProgressBar1.Maximum = _FileSizeCalculated

        'muestra el tamaño del archivo
        Dim GB As Double = _filetocompress.SizeFileInGB
        If GB > 1 Then
            lbl_PstFileSize.Text = GB.ToString("0.00") & " GB"
        Else
            lbl_PstFileSize.Text = _filetocompress.SizeFileinMB.ToString("0.00") & " MB"
        End If

        If _filetocompress.SizeFileinMB < 1 Then
            lbl_PstFileSize.Text = _filetocompress.SizeFileinKB.ToString("0.00") & " KB"
        End If


        Dim parametes As String = GetCompressosParametes()
        'Console.WriteLine(_WINRARCLI & "\rar.exe " & parametes)
        InitRunner.LogEvents.Add("Running rar command > " & _WINRARCLI & "\rar.exe " & parametes)

        psi = New ProcessStartInfo()

        Dim systemencoding As System.Text.Encoding = _
            System.Text.Encoding.GetEncoding(Globalization.CultureInfo.CurrentUICulture.TextInfo.OEMCodePage)

        With psi
            .FileName = _WINRARCLI & "\rar.exe"
            .Arguments = parametes
            .UseShellExecute = False  ' Required for redirection
            .RedirectStandardError = True
            .RedirectStandardOutput = True
            .RedirectStandardInput = True
            .CreateNoWindow = True
            .StandardOutputEncoding = systemencoding  ' Use OEM encoding for console applications
            .StandardErrorEncoding = systemencoding

        End With

        ' EnableraisingEvents is required for Exited event
        cmd = New Process With {.StartInfo = psi, .EnableRaisingEvents = True}

        AddHandler cmd.Exited, AddressOf CMD_Exited

        cmd.Start()

    End Sub

    Private Sub CMD_Exited(ByVal sender As Object, ByVal e As EventArgs)
        _ProcessFinished = True
    End Sub

    Private Function GetCompressosParametes() As String
        Dim parameters As New System.Text.StringBuilder

        'Agrega la opcion de Password
        parameters.Append("-p" & _configuration.Item("TokenKey").ParameterValue & " ")
        parameters.Append("a ")
        'Agrega donde se va crear el backupcon el nombre
        Dim destinationpath As String = _configuration.Item("BackupDestination").ParameterValue

        'forma el nombre del archivo
        'Dim filename As String = Environment.UserName.ToString & "-" & _configuration.Item("TokenKey").ParameterValue & "-" & Date.Now.ToString("MMddyyyyhhmmss") & ""
        Dim name As String
        name = _filetocompress.FileName.Substring(0, 3) & _filetocompress.FileName.Substring(_filetocompress.FileName.Length - 7, 3) & _filetocompress.FullPathFile.Length
        Dim filename As String = Environment.UserName.ToString & "-" & name & "-" & Date.Now.ToString("MMddyyyyhhmmss") & ""

        'Configura el archivo zip para ser usado en la aplicacion
        _ZipFile = New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(destinationpath & "\" & filename & ".rar"))
        parameters.Append("""" & destinationpath & "\" & filename & """ ")

        'Agrega el archivo fuente a comprimir
        parameters.Append("""" & _filetocompress.FullPathFile & """")

        Return parameters.ToString

    End Function

    Private Sub Timer_ActiveProcess_Tick(sender As Object, e As EventArgs) Handles Timer_ActiveProcess.Tick
        Dim filename As String = _ZipFile.FullPathFile
        _ZipFile = New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(filename))

        If _ProcessFinished Then
            Timer_ActiveProcess.Stop()
            ProgressBar1.Maximum = _ZipFile.SizeFileinMB
            ProgressBar1.Value = ProgressBar1.Maximum
            Application.DoEvents()
            InitRunner.LogEvents.Add("Compress file finished >" & _ZipFile.FullPathFile)
            'Limpia backups viejos
            CleanOldBackups()

            Threading.Thread.Sleep("3000")
            Me.Dispose()

        End If

        lbl_enlapsedtime.Text = _stpw.Elapsed.ToString("hh\:mm\:ss")

        'Imprime el tamaño guardado al momento
        If _ZipFile.SizeFileInGB > 1 Then
            lbl_WrritenData.Text = _ZipFile.SizeFileInGB.ToString("0.00") & " GB"
        Else
            lbl_WrritenData.Text = _ZipFile.SizeFileinMB.ToString("0.00") & " MB"
        End If

        If _ZipFile.SizeFileinMB < 1 Then
            lbl_WrritenData.Text = _ZipFile.SizeFileinKB.ToString("0.00") & " KB"
        End If


        If IO.File.Exists(_ZipFile.FullPathFile) Then
            _ZipFile = Nothing
            _ZipFile = New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(filename))
            Dim actuatlsize As Long = _ZipFile.SizeFileinMB
            ProgressBar1.Value = actuatlsize
            Application.DoEvents()
        End If


    End Sub

    Private Sub BackupProcesFormS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BackupProcesFormS_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        RunCMDBackup()
        _DateStartBackup = Date.Now
    End Sub

    Private Sub btn_CancelBackup_Click(sender As Object, e As EventArgs) Handles btn_CancelBackup.Click

        'Pregunta si desea detener el proceso
        Dim SeleectionMessage As Integer = MsgBox("Do you want to cancel the Backup?", MsgBoxStyle.YesNo, "Backup Cancel")

        If SeleectionMessage = vbYes Then

            InitRunner.LogEvents.Add("Backup Process manually cancelled.")

            'Detiene el tiempo en el main menu
            InitRunner.ManuallyCancelled = True

            'Deteniene el tiempo por un momento
            Timer_ActiveProcess.Stop()
            'Muestra el progress bass in red and full
            ProgressBar1.ForeColor = Color.Red
            ProgressBar1.Value = ProgressBar1.Maximum
            Application.DoEvents()

            'Detiene el proceso de Win Rar
            Dim isopen As Boolean = True
            Dim PS() As Process = Process.GetProcessesByName("rar")
            If PS.Length > 0 Then
                isopen = True
            End If
            If isopen Then
                Dim proc = Process.GetProcessesByName("rar")
                For i As Integer = 0 To proc.Count - 1
                    proc(i).Kill()
                    proc(i).WaitForExit()
                Next i
            End If
            'Borra el archivo creado
            If IO.File.Exists(_ZipFile.FullPathFile) Then
                Try
                    IO.File.Delete(_ZipFile.FullPathFile)
                    InitRunner.LogEvents.Add("Delete not completed backup > " & _ZipFile.FullPathFile)
                Catch ex As Exception
                    InitRunner.LogEvents.Add("Error in module BackupProcessFromS:btn_CancelBackup_Click:Error > " & ex.Message.ToString)
                    MsgBox("Error trying to remove the created file.", MsgBoxStyle.Critical, "Removing incomplete Backup")
                End Try
            End If

            Threading.Thread.Sleep(2000)

            Me.Dispose()

        End If



    End Sub

    Public Sub CleanOldBackups()
        'Almacena los respaldos encontrados 
        Dim files_saved As New List(Of PSTBacktupUtilityCore.com.lib.objects.FileObj)
        'busca los respaldos encontrados
        Dim destinationpath As String = _configuration.Item("BackupDestination").ParameterValue
        Dim FilesEntries As String() = IO.Directory.GetFiles(destinationpath)
        For Each item As String In FilesEntries
            'obtiene el nombre clave del archivo en el folder destino
            Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(item))
            If fileobj.FileName.Contains("-") Then
                Dim filenamevalues As String() = fileobj.FileName.Split("-")
                If filenamevalues.Length > 2 Then
                    Dim keynamesaved As String = filenamevalues(1)

                    'obtiene el nombre clave del archivo a generar
                    Dim keynamefile As String
                    keynamefile = _filetocompress.FileName.Substring(0, 3) & _filetocompress.FileName.Substring(_filetocompress.FileName.Length - 7, 3) & _filetocompress.FullPathFile.Length

                    If keynamesaved.Equals(keynamefile) Then
                        files_saved.Add(fileobj)
                    End If
                End If
            End If
        Next
        'obtiene el parametro para ver cuantos archivos almacenara
        Dim limitfilestokeep As Integer = Integer.Parse(InitRunner.ConfigurationDATFile.Item("NumBackupSave").ParameterValue)
        'verifica si hay mas del limite definido.
        If files_saved.Count > limitfilestokeep Then
            'agrega el archivo actual a la lista de mantener
            files_saved.Add(_ZipFile)
            ' crea lista de los archivos que no se borran
            Dim filetokeep As New List(Of PSTBacktupUtilityCore.com.lib.objects.FileObj)
            'Agrega el archivo actual creado
            filetokeep.Add(_ZipFile)
            'agrega los archivos que se guardaran
            For i = 1 To limitfilestokeep
                filetokeep.Add(files_saved.Item(files_saved.Count - (2 + i)))
            Next

            'verifica cuales archivos borrar
            For Each item As PSTBacktupUtilityCore.com.lib.objects.FileObj In files_saved
                Dim deletefile As Boolean = True
                For Each filenotdelete As PSTBacktupUtilityCore.com.lib.objects.FileObj In filetokeep
                    If item.FileName.Equals(filenotdelete.FileName) Then
                        deletefile = False
                    End If
                Next
                If deletefile Then
                    InitRunner.LogEvents.Add("Removing old backups > " & item.FileName)
                    IO.File.Delete(item.FullPathFile)
                End If
            Next
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

        Dim sizeavailable As Double = CheckDiskAvailableSpace(InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue.Substring(0, 3))
        Dim sizeavailable_inkb As Double = sizeavailable / 1024
        Dim sizeavailable_inmb As Double = sizeavailable_inkb / 1024

        If sizeavailable_inmb < fileobj.SizeFileinMB Then
            enoughspace = False
        End If

        Return enoughspace
    End Function

End Class
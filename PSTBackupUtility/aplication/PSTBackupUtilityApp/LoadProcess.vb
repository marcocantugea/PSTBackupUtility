Imports PSTBacktupUtilityCore
Imports System.Management
Imports System.IO

Public Class LoadProcess

    Private _PSTFilesFound As New PSTBacktupUtilityCore.com.lib.objects.FileObjCollection
    Private _SearchPSTFilesThreat As New Threading.Thread(AddressOf SearchPSTFilesInDirectories)
    Private _SearchProgress As Integer = 0
    Private _TotalFoldersToSearch As Integer
    Private _Finishprocess As Boolean = False


    Public ReadOnly Property SearchPSTFilesThreat As Threading.Thread
        Get
            Return _SearchPSTFilesThreat
        End Get
    End Property

    Private Sub LoadProcess_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Me.CenterToScreen()
        'Carga el archivo de configuracion
        LoadConfigrationDatFile()
        'Revisa si hay archivos PST en la configuracion
        If CheckPSTSourceFiles() Then
            'Revisa si la variable Backupsorce esta en empty
            If InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = "empty" Then
                'inicia busqueda de archivos pst
                _SearchPSTFilesThreat.Start()
            Else
                'si existen revisa que existan los pst cargados
                Dim SourcesFiles As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")
                Dim AllValidPst As Boolean = True
                For Each SourcesFilesFound As String In SourcesFiles
                    If Not File.Exists(SourcesFilesFound) Then
                        AllValidPst = False
                    End If
                Next
                If Not AllValidPst Then
                    _SearchProgress.ToString()
                End If

                InitRunner.CheckLasBackupDate()

            End If

        Else
            ' si no existen archivos pst configurados
            _SearchPSTFilesThreat.Start()

            'Agrega al log la busqueda de PST
            InitRunner.CheckLasBackupDate()
        End If
        _Finishprocess = True
    End Sub

    'Carga la configuracion local de un archivo DAT
    Public Sub LoadConfigrationDatFile()
        Dim AppPath As String = Application.StartupPath
        Dim DatConfigFile As String = "AppConfig.dto"
        Dim FullPathConfigFile As String = AppPath & "\" & DatConfigFile

        If Not System.IO.File.Exists(FullPathConfigFile) Then
            CreateDTOConfigFile()
            LoadDTOConfigFile(FullPathConfigFile)
        Else
            LoadDTOConfigFile(FullPathConfigFile)
        End If

    End Sub

    'Crea el archivo de configuracion si la aplicacion no lo encuentra
    Public Sub CreateDTOConfigFile()
        'Crea el nombre del archivo de la configuracion
        Dim DATConfigFile = Application.StartupPath.ToString & "\AppConfig.dto"
        'Crea un objeto Fileobj para crear el archivo de configuracion
        Dim DATConfigObj As New com.lib.objects.FileObj(New IO.FileInfo(DATConfigFile))
        'Se Define el creador de archivo
        Dim TextFileWriter As New com.lib.file.TextFileWriter(DATConfigObj)

        'Se escriben los paramentros default
        TextFileWriter.AddText("AppVersion=1" & vbNewLine)
        TextFileWriter.AddText("TokenKey=" & GetTokenKey() & vbNewLine)
        TextFileWriter.AddText("BackuptimeInDays=2" & vbNewLine)
        TextFileWriter.AddText("BackupStarTime=01:00:00" & vbNewLine)
        TextFileWriter.AddText("BackupDestination=" & Application.StartupPath & vbNewLine)
        TextFileWriter.AddText("BackupSource=empty" & vbNewLine)
        TextFileWriter.AddText("LoggerPath=" & Application.StartupPath & vbNewLine)
        TextFileWriter.AddText("CompressorPath=" & Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\WINRAR\" & vbNewLine)
        TextFileWriter.AddText("NumBackupSave=2" & vbNewLine)
        TextFileWriter.AddText("LastBackupDate=empty" & vbNewLine)
        TextFileWriter.AddText("CheckForUpdates=true" & vbNewLine)


        'Crea y escribe archivo
        TextFileWriter.CreateFile()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MsgBox(CheckPSTSourceFiles)
    End Sub

    'Funcion para generar la Llave para abrir los archivos comprimidos de los PST
    Private Function GetTokenKey() As String
        Dim TokenKey As String = ""
        Dim userlogged As String = Environment.UserName

        Dim wrapper As New com.lib.cypher.CypherCoder(userlogged)
        TokenKey = wrapper.EncryptData(userlogged)

        Return TokenKey
    End Function

    'Carga la configuracion de la aplicacion toma el archivo AppConfig.dto que est en el directorio de la aplicacion
    Public Sub LoadDTOConfigFile(file As String)
        Dim objReader As New IO.StreamReader(file)
        Do While objReader.Peek <> -1
            Dim linevalue As String = objReader.ReadLine()
            Dim valuessplit As String() = linevalue.Split("=")
            Dim Configobj As New com.lib.objects.ConfigObj
            Configobj.Parameter = valuessplit(0)
            Configobj.ParameterValue = valuessplit(1)
            InitRunner.ConfigurationDATFile.Add(Configobj.Parameter, Configobj)
        Loop
        objReader.Close()
        objReader = Nothing
    End Sub

    'Funcion que revisa si en la variable de configuracion existen algun PST
    Public Function CheckPSTSourceFiles() As Boolean
        Dim value As Boolean = True
        Dim Backupsource As String = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue
        If Backupsource.Equals("empty") Then
            value = False
        Else
            'revisa si contiene la extencion .pst
            If Backupsource.Contains(".pst") Then
                'revisa si existen varios valores
                If Backupsource.Contains("|") Then
                    Try
                        'obtiene los valores separados por |
                        Dim values As String() = Backupsource.Split("|")
                        'revisa si los paths son validos.
                        For Each itm As String In values
                            If Not IO.File.Exists(itm) Then
                                value = False
                            End If
                        Next
                    Catch ex As Exception
                        value = False
                    End Try
                Else
                    'En caso de que exista un valor 
                    'revisa si el valor que contiene es una ruta valida
                    Try
                        If Not IO.File.Exists(Backupsource) Then
                            value = False
                        End If
                    Catch ex As Exception
                        value = False
                    End Try
                End If
            Else
                value = False
            End If

        End If
        Return value
    End Function

    'Funciones para poder usar los controles en el threath
    Delegate Sub UpdateUIHandler(ByVal newText As String)
    Delegate Sub UpdateUIHandler2(ByVal newInt As Integer)
    Private Sub UpdateLabelProcess(ByVal value As String)
        lbl_Progresslabel.Text = value
    End Sub
    Private Sub UpdateProgressBarMaxValue(ByVal value As Integer)
        pb_load.Maximum = value
    End Sub

    Private Sub UpdateProgressBarCurrentValue(ByVal value As Integer)
        pb_load.Value = value
    End Sub

    Private Sub UpdateBackupSource(value As String)
        InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = value
        UpdatedDTOConfigFile()
    End Sub
    'Funcion para buscar PST en la unidad
    Public Sub SearchPSTFilesInDirectories()
        ''Invoca al objeto para poder usado en el threat
        'If lbl_Progresslabel.InvokeRequired Then
        '    lbl_Progresslabel.Invoke(New Action(AddressOf SearchPSTFilesInDirectories))
        '    Return
        'End If

        ''Invoca al objeto para poder ser utilizado en el threat
        'If pb_load.InvokeRequired Then
        '    pb_load.Invoke(New Action(AddressOf SearchPSTFilesInDirectories))
        '    Return
        'End If

        'Realiza el proceso de busqueda y actualiza la barra de estado.

        'Primero busca en la carptea del usuario activo de windows
        'Buscar PST en la carpetas del usuario actual.
        Dim windowsuserfolder As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)

        'Avisa en la etiqueta que carpeta va a empezar a buscar
        If lbl_Progresslabel.InvokeRequired Then
            lbl_Progresslabel.Invoke(DirectCast(AddressOf UpdateLabelProcess, UpdateUIHandler), "Searching PST Files in folder > " & windowsuserfolder.ToString)
        Else
            lbl_Progresslabel.Text = "Searching PST Files in folder > " & windowsuserfolder.ToString
        End If


        'Cuenta cuantos carpetas por analizar hay
        CountFolders(windowsuserfolder)

        'Configura el progress bar el numero maximo
        If pb_load.InvokeRequired Then
            pb_load.Invoke(DirectCast(AddressOf UpdateProgressBarMaxValue, UpdateUIHandler2), _TotalFoldersToSearch)
        Else
            Me.pb_load.Maximum = _TotalFoldersToSearch
        End If

        'Realiza el proceso de busqueda
        ProcessDirectory(windowsuserfolder)

        'Ahora buscara en unidades externas
        'Busca las unidades externas activas.
        Dim drives As Collection = GetPhysicalDrives()
        'Por cada unidad encontrada busca pst
        For Each item As String In drives

            'Reinicia valores para el progress bar
            If pb_load.InvokeRequired Then
                pb_load.Invoke(DirectCast(AddressOf UpdateProgressBarCurrentValue, UpdateUIHandler2), 0)
            Else
                pb_load.Value = 0
            End If


            _TotalFoldersToSearch = 0
            _SearchProgress = 0

            'Actualiza la etiqueta para ver que unidad se va verificar
            If lbl_Progresslabel.InvokeRequired Then
                lbl_Progresslabel.Invoke(DirectCast(AddressOf UpdateLabelProcess, UpdateUIHandler), "Searching PST Files in folder > " & item)
            Else
                lbl_Progresslabel.Text = "Searching PST Files in folder > " & item
            End If

            'Determina la cantidad maxima de carpetas para el progres var
            CountFolders(item)

            'Actualiza el valor maximo del progress bar
            If pb_load.InvokeRequired Then
                pb_load.Invoke(DirectCast(AddressOf UpdateProgressBarMaxValue, UpdateUIHandler2), _TotalFoldersToSearch)
            Else
                Me.pb_load.Maximum = _TotalFoldersToSearch
            End If

            'Realiza el proceso de busqueda
            ProcessDirectory(item)
        Next

        'Guarda las rutas de los archivos encontrados en memoria del programa
        InitRunner.PSTFoundList = _PSTFilesFound
        'Guarda las turas de los archivos encontras en la configuracion cargada
        Dim destinationsource As String
        Dim count As Integer = 0
        For Each value As com.lib.objects.FileObj In _PSTFilesFound.Items
            count = count + 1
            If count = 1 Then
                destinationsource = value.FullPathFile
            Else
                destinationsource = destinationsource & "|" & value.FullPathFile
            End If

        Next

        If pb_load.InvokeRequired Then
            pb_load.Invoke(DirectCast(AddressOf UpdateBackupSource, UpdateUIHandler), destinationsource)
        Else
            InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = destinationsource
        End If


        'Console.WriteLine(InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue)

        'Console.WriteLine(InitRunner.PSTFoundList.Count)

        'Console.WriteLine("Process Directory  Done")
    End Sub

    'Procesa los directorios y archivos
    Private Sub ProcessDirectory(ByVal path As String)
        Try
            Dim FilesEntries As String() = Directory.GetFiles(path)
            _SearchProgress = _SearchProgress + 1
            If pb_load.InvokeRequired Then
                pb_load.Invoke(DirectCast(AddressOf UpdateProgressBarCurrentValue, UpdateUIHandler2), _SearchProgress)
            Else
                pb_load.Value = _SearchProgress
            End If

            Application.DoEvents()

            For Each item As String In FilesEntries
                Dim fileinfo As New FileInfo(item)
                If fileinfo.Extension.ToUpper = ".PST" Then
                    Dim fileobj As New com.lib.objects.FileObj(fileinfo)
                    _PSTFilesFound.Add(fileobj)
                    'Console.WriteLine("file found >" & fileinfo.FullName)
                End If
            Next

            Dim subdirectoryEntries As String() = Directory.GetDirectories(path)
            For Each item As String In subdirectoryEntries
                ProcessDirectory(item)
            Next

        Catch ex As Exception
            'InitRunner.LogEvents.Add("Error in module LoadProcess:ProcessDirectory:Error > " & ex.Message.ToString)
            Console.WriteLine("Error trying to read the path : " & path & " error >" & ex.Message.ToString)
        End Try
    End Sub


    Public Sub CountFolders(path As String)
        Try
            Dim subdirectoryEntries As String() = Directory.GetDirectories(path)
            _TotalFoldersToSearch = _TotalFoldersToSearch + 1
            For Each item As String In subdirectoryEntries

                CountFolders(item)
            Next
        Catch ex As Exception
            'InitRunner.LogEvents.Add("Error in module LoadProcess:CountFolders:Error > " & ex.Message.ToString)
            'Console.WriteLine("Error trying to read the path : " & path)
        End Try

    End Sub

    Public Function GetPhysicalDrives() As Collection
        Dim values As New Collection

        Dim DiskInfo As New com.lib.file.clsDiskInfoEx

        Dim Disks As New List(Of String)
        DiskInfo.GetPhysicalDisks(Disks)

        For Each item As String In Disks
            If item.Contains("Physical") Then
                If Not item.StartsWith("C") Then
                    Dim drive As String = item.Substring(0, 3)
                    values.Add(drive)
                End If
            End If
        Next

        Return values
    End Function

    Public Sub UpdatedDTOConfigFile()
        'Crea el nombre del archivo de la configuracion
        Dim DATConfigFile = Application.StartupPath.ToString & "\AppConfig.dto"
        'Crea un objeto Fileobj para crear el archivo de configuracion
        Dim DATConfigObj As New com.lib.objects.FileObj(New IO.FileInfo(DATConfigFile))
        'Se Define el creador de archivo
        Dim TextFileWriter As New com.lib.file.TextFileWriter(DATConfigObj)

        'Se escriben los paramentros actualizados
        For Each item As KeyValuePair(Of String, com.lib.objects.ConfigObj) In InitRunner.ConfigurationDATFile
            Dim configvalue As com.lib.objects.ConfigObj = CType(item.Value, com.lib.objects.ConfigObj)
            TextFileWriter.AddText(configvalue.Parameter & "=" & configvalue.ParameterValue & vbNewLine)
        Next

        'Crea y escribe archivo
        TextFileWriter.CreateFile()

        'Crear un archivo de respaldo en la carpeta destino.
        Dim BkConfigFile = InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue & "\AppConfig.dto"
        Dim BkConfigFileobj As New com.lib.objects.FileObj(New IO.FileInfo(BkConfigFile))
        Dim TextFileWriterBk As New com.lib.file.TextFileWriter(BkConfigFileobj)
        For Each item As KeyValuePair(Of String, com.lib.objects.ConfigObj) In InitRunner.ConfigurationDATFile
            Dim configvalue As com.lib.objects.ConfigObj = CType(item.Value, com.lib.objects.ConfigObj)
            TextFileWriterBk.AddText(configvalue.Parameter & "=" & configvalue.ParameterValue & vbNewLine)
        Next

        TextFileWriterBk.CreateFile()

    End Sub

    Private Sub TimerRunning_Tick(sender As Object, e As EventArgs) Handles TimerRunning.Tick
        If _Finishprocess Then
            If Not _SearchPSTFilesThreat.IsAlive Then
                _Finishprocess = False
                Me.Dispose()
            End If
        End If
    End Sub

    Public Sub CheckLasBackupDate()
        'Revisa la ultima fecha de respaldo.
        Try
            'Obtiene la ultima fecha de respaldo
            Dim lastDateBackup As Date = Date.Parse(InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue)

        Catch ex As Exception
            'si no existe la fecha de respaldo actualiza la fecha a la actual.
            'calcula la siguente fecha para generar backup
            Dim datenow As Date = Date.Now
            Dim daystoaadd As Integer = Integer.Parse(InitRunner.ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue)
            Dim getHrsToRun As String() = InitRunner.ConfigurationDATFile.Item("BackupStarTime").ParameterValue.Split(":")

            'Agrega los dias configurados a la recha actual
            Dim datenextbackup As Date = datenow.AddDays(daystoaadd)

            'Crea fecha de siguiente backup.
            Dim hr As Integer = Integer.Parse(getHrsToRun(0))
            Dim min As Integer = Integer.Parse(getHrsToRun(1))
            Dim seg As Integer = Integer.Parse(getHrsToRun(2))
            Dim nextdatebacup As DateTime
            nextdatebacup = New DateTime(datenextbackup.Year, datenextbackup.Month, datenextbackup.Day, hr, min, seg)

            InitRunner.NextDateBackup = nextdatebacup

            'Console.WriteLine(nextdatebacup.ToString("dd-MMM-yyyy hh:mm:ss"))
            'InitRunner.ConfigurationDATFile.Item("LastBackupDate").ParameterValue = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            'Calcula 
        End Try
    End Sub

End Class
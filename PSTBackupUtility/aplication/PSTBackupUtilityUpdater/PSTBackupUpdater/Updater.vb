Imports PSTBacktupUtilityCore
Public Class Updater

    Private local_ConfigurationDATFile As New Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    Private server_ConfigurationDATFile As New Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)

    ''Funcion de inicio
    ''Oculta el formulario a la vista y inicia el reloj de apertura de la aplicacion de AMOS.
    Protected Overrides Sub SetVisibleCore(ByVal value As Boolean)

        ''Funcion de ocultar el formulario
        If Not Me.IsHandleCreated Then
            Me.CreateHandle()
            value = False
        End If

        'Carga la configuracion del archivo DTO
        LoadConfigrationDatFile()

        'Verifica si es necesario realizar un actializacion a la aplicacion.
        CheckPSTBackupUtilityUpdate()
    End Sub

    Public Sub LoadConfigrationDatFile()
        Dim AppPath As String = Application.StartupPath
        Dim DatConfigFile As String = "AppConfig.dto"
        Dim local_FullPathConfigFile As String = AppPath & "\" & DatConfigFile

        Dim server_FullpathConfigFile As String = System.Configuration.ConfigurationSettings.AppSettings("ServerUpdate").ToString & "\" & DatConfigFile

        If System.IO.File.Exists(local_FullPathConfigFile) And System.IO.File.Exists(server_FullpathConfigFile) Then
            Load_local_DTOConfigFile(local_FullPathConfigFile)
            Load_server_DTOConfigFile(server_FullpathConfigFile)
        Else
            Me.Dispose()
        End If

    End Sub

    Public Sub Load_local_DTOConfigFile(file As String)
        Dim objReader As New IO.StreamReader(file)
        Do While objReader.Peek <> -1
            Dim linevalue As String = objReader.ReadLine()
            Dim valuessplit As String() = linevalue.Split("=")
            Dim Configobj As New com.lib.objects.ConfigObj
            Configobj.Parameter = valuessplit(0)
            Configobj.ParameterValue = valuessplit(1)
            local_ConfigurationDATFile.Add(Configobj.Parameter, Configobj)
        Loop
        objReader.Close()
        objReader = Nothing
    End Sub
    Public Sub Load_server_DTOConfigFile(file As String)
        Dim objReader As New IO.StreamReader(file)
        Do While objReader.Peek <> -1
            Dim linevalue As String = objReader.ReadLine()
            Dim valuessplit As String() = linevalue.Split("=")
            Dim Configobj As New com.lib.objects.ConfigObj
            Configobj.Parameter = valuessplit(0)
            Configobj.ParameterValue = valuessplit(1)
            server_ConfigurationDATFile.Add(Configobj.Parameter, Configobj)
        Loop
        objReader.Close()
        objReader = Nothing
    End Sub

   

    Public Sub CheckPSTBackupUtilityUpdate()
        Dim server_version As Integer
        Dim local_version As Integer
        Try
            local_version = Integer.Parse(local_ConfigurationDATFile.Item("AppVersion").ParameterValue)
            server_version = Integer.Parse(server_ConfigurationDATFile.Item("AppVersion").ParameterValue)

            If server_version > local_version Then
                UpdatePSTBackupUtility()
            Else
                Me.Dispose()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Public Sub UpdatePSTBackupUtility()

        If checkPSTBackupUtilityIsOpen() Then
            ClosePSTBackupUtilityTaskProcess()
        End If

        Threading.Thread.Sleep("3000")

        updateFilesAPP()

        local_ConfigurationDATFile.Item("AppVersion").ParameterValue = server_ConfigurationDATFile.Item("AppVersion").ParameterValue
        UpdatedDTOConfigFile()

        Threading.Thread.Sleep("5000")

        Dim psi As ProcessStartInfo = New ProcessStartInfo(Application.StartupPath & "\PSTBackupUtilityApp.exe")
        Process.Start(psi)

        Me.Dispose()

    End Sub

    Public Function checkPSTBackupUtilityIsOpen() As Boolean
        Dim isopen As Boolean = False
        Dim PS() As Process = Process.GetProcessesByName("PSTBackupUtilityAPP")
        If PS.Length > 0 Then
            isopen = True
        End If
        Return isopen
        Return isopen
    End Function

    Public Sub ClosePSTBackupUtilityTaskProcess()
        Dim app_active As Boolean = False

        app_active = checkPSTBackupUtilityIsOpen()

        If app_active Then
            Dim proc = Process.GetProcessesByName("PSTBackupUtilityAPP")
            For i As Integer = 0 To proc.Count - 1
                proc(i).Kill()
            Next i
        End If

    End Sub

    Private Sub updateFilesAPP()
        Dim backup_version_folder As String = Application.StartupPath.ToString & "\" & "version-" & local_ConfigurationDATFile.Item("AppVersion").ParameterValue
        Dim server_path As String = System.Configuration.ConfigurationSettings.AppSettings("ServerUpdate")

        If Not IO.Directory.Exists("version-" & local_ConfigurationDATFile.Item("AppVersion").ParameterValue) Then
            IO.Directory.CreateDirectory(backup_version_folder)
        Else
            Dim files As String() = IO.Directory.GetFiles(backup_version_folder)
            For Each f As String In files
                IO.File.Delete(f)
            Next
        End If

        Dim files_found As String() = IO.Directory.GetFiles(Application.StartupPath.ToString)
        For Each file As String In files_found
            Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(file))
            If file.Contains("PSTBackupUtilityApp") Then
                IO.File.Move(fileobj.FullPathFile, backup_version_folder & "\" & fileobj.FileName)
            End If
        Next

        Dim files_fromserver As String() = IO.Directory.GetFiles(server_path)
        For Each file As String In files_fromserver
            Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(file))
            If file.Contains("PSTBackupUtilityApp") Then
                IO.File.Copy(file, Application.StartupPath.ToString & "\" & fileobj.FileName)
            End If
        Next
    End Sub


    Public Sub UpdatedDTOConfigFile()
        'Crea el nombre del archivo de la configuracion
        Dim DATConfigFile = Application.StartupPath.ToString & "\AppConfig.dto"
        'Crea un objeto Fileobj para crear el archivo de configuracion
        Dim DATConfigObj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(DATConfigFile))
        'Se Define el creador de archivo
        Dim TextFileWriter As New PSTBacktupUtilityCore.com.lib.file.TextFileWriter(DATConfigObj)

        'Se escriben los paramentros actualizados
        For Each item As KeyValuePair(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj) In local_ConfigurationDATFile
            Dim configvalue As PSTBacktupUtilityCore.com.lib.objects.ConfigObj = CType(item.Value, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
            TextFileWriter.AddText(configvalue.Parameter & "=" & configvalue.ParameterValue & vbNewLine)
        Next

        'Crea y escribe archivo
        TextFileWriter.CreateFile()
    End Sub

End Class
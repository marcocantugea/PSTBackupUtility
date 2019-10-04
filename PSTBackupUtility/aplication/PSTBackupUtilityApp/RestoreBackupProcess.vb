Public Class RestoreBackupProcess

    Private psi As ProcessStartInfo
    Private cmd As Process
    Private _filetorestore As PSTBacktupUtilityCore.com.lib.objects.FileObj
    Private _configuration As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
    Private _ProcessFinished As Boolean = False
    Private _DestinationPath As String
    Private _RestoreToDestinationPath As Boolean = True

    Public Property RestoreToDestinationPath As Boolean
        Get
            Return _RestoreToDestinationPath
        End Get
        Set(value As Boolean)
            _RestoreToDestinationPath = value
        End Set
    End Property

    Public Property DestinationPath As String
        Get
            Return _DestinationPath
        End Get
        Set(value As String)
            _DestinationPath = value
        End Set
    End Property
    Public Property FileToRestore As PSTBacktupUtilityCore.com.lib.objects.FileObj
        Get
            Return _filetorestore
        End Get
        Set(value As PSTBacktupUtilityCore.com.lib.objects.FileObj)
            _filetorestore = value
        End Set
    End Property

    Public Property Configuration As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj)
        Get
            Return _configuration
        End Get
        Set(value As Dictionary(Of String, PSTBacktupUtilityCore.com.lib.objects.ConfigObj))
            _configuration = value
        End Set
    End Property

    Private Sub Timer_ActiveProcess_Tick(sender As Object, e As EventArgs) Handles Timer_ActiveProcess.Tick
        If _ProcessFinished Then
            Me.Dispose()
        End If
    End Sub

    Private Sub RestoreBackup()

        'Configura los controles del formulario
        lbl_ProcessedFile.Text = "Processing File > " & _filetorestore.FullPathFile

        'Configura el path de donde estal winrar.
        Dim _WINRARCLI As String
        _WINRARCLI = _configuration.Item("CompressorPath").ParameterValue

        'Carga parametros para descompression del archivo
        Dim parametes As String = GetParametes()
        Console.WriteLine(_WINRARCLI & "\rar.exe " & parametes)

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

    Private Function GetParametes() As String
        Dim parameters As New System.Text.StringBuilder

        'Agrega la opcion de Password
        parameters.Append("-p" & _configuration.Item("TokenKey").ParameterValue & " ")

        'Revisa si el respaldo se va a restaur en su carpeta destino o en otro lugar
        If _RestoreToDestinationPath Then
            'Parametro para restaurar el archivo con sus caprpetas
            parameters.Append("x ")
        Else
            'Parametro para restaurar el archivo en otro lugar ignorando sus subcarpetas
            parameters.Append("e ")
        End If

        'Parametro para sobrescribir archivo si existe.
        parameters.Append("-o+ ")

        'Archivo a descomprimir.
        parameters.Append(_filetorestore.FullPathFile & " ")

        'Destino a descomprimir.
        parameters.Append(_DestinationPath & " ")

        Return parameters.ToString
    End Function

    Private Sub RestoreBackupProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RestoreBackup()
    End Sub
End Class
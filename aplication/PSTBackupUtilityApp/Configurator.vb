Public Class Configurator

    Private Sub Configurator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadValuesOnControls()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        txt_DestinationFolderBackup.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn_AddPSTFile.Click
        OpenFileDialog1.ShowDialog()

        Dim fileselected As New IO.FileInfo(OpenFileDialog1.FileName)
        If fileselected.Extension.ToUpper = ".PST" Then
            Dim DuplicatedPath As Boolean = False
            'Dim pstfiles As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")
            For Each value As String In ListBox_PSTSourceFiles.Items
                If value = fileselected.FullName Then
                    DuplicatedPath = True
                    MsgBox("PST file is already defined on the list.", MsgBoxStyle.Information, "PST file path duplicated")
                End If
            Next
            If Not DuplicatedPath Then
                ListBox_PSTSourceFiles.Items.Add(fileselected.FullName)
            End If
        Else
            MsgBox("File type not supported. please select a PST file.", MsgBoxStyle.Exclamation, "File not supported")
        End If


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btn_RemoveItem.Click
        Dim selected_path As Integer = ListBox_PSTSourceFiles.SelectedIndex
        ListBox_PSTSourceFiles.Items.RemoveAt(selected_path)
    End Sub

    Private Sub btn_ScanForPST_Click(sender As Object, e As EventArgs) Handles btn_ScanForPST.Click
        'InitRunner.ConfigurationDATFile.Clear()
        InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = "empty"
        InitRunner.UpdatedDTOConfigFile()
        InitRunner.ConfigurationDATFile.Clear()

        Dim LoadConfig As New LoadProcess
        LoadConfig.Show()
        Timer_wait.Enabled = True
        Timer_wait.Start()
        'Do While LoadConfig.SearchPSTFilesThreat.IsAlive = True
        '    Threading.Thread.Sleep(1000)
        'Loop
        'MsgBox("finish")
    End Sub

    Public Sub LoadValuesOnControls()
        'Carta a la caja de texto la configuracion del token
        txt_Token.Text = InitRunner.ConfigurationDATFile.Item("TokenKey").ParameterValue

        'Carta al combo box la configuracion los dias para respaldo.
        For Each item As Object In cmb_DaysToBackup.Items
            If item = InitRunner.ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue Then
                cmb_DaysToBackup.SelectedIndex = cmb_DaysToBackup.Items.IndexOf(item)
            End If
        Next

        'Carga al combo box la hr en que se iniciara el processo de respaldo.
        For Each item As Object In cmb_StarTimeBackup.Items
            If item = InitRunner.ConfigurationDATFile.Item("BackupStarTime").ParameterValue Then
                cmb_StarTimeBackup.SelectedIndex = cmb_StarTimeBackup.Items.IndexOf(item)
            End If
        Next

        'Carga el valor de donde se va guargar los respaldos.
        txt_DestinationFolderBackup.Text = InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue

        'Carga los PST encontrados en la configuracion
        Dim pstfiles As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")
        For Each value As String In pstfiles
            ListBox_PSTSourceFiles.Items.Add(value)
        Next

        'Carga la ruta del los archivos log
        txt_LogPath.Text = InitRunner.ConfigurationDATFile.Item("LoggerPath").ParameterValue

        'Carga la ruta de donde esta el archivo Rar.Exe
        txt_WinRarPath.Text = InitRunner.ConfigurationDATFile.Item("CompressorPath").ParameterValue

        'Carga parametro de numero de respaldos guardados
        txt_NumStoredBackups.Text = InitRunner.ConfigurationDATFile("NumBackupSave").ParameterValue

    End Sub


    Private Sub Timer_wait_Tick(sender As Object, e As EventArgs) Handles Timer_wait.Tick

        Try
            If Application.OpenForms.Item("LoadProcess").IsDisposed Then
                LoadValuesOnControls()
                Timer_wait.Stop()
            End If
        Catch ex As Exception
            InitRunner.LogEvents.Add("Error in module Configurator:Timer_wait_tick > Error > " & ex.Message.ToString)
            ListBox_PSTSourceFiles.Items.Clear()
            LoadValuesOnControls()
            Timer_wait.Stop()
        End Try
        
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        FolderBrowserDialog1.ShowDialog()
        txt_LogPath.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        FolderBrowserDialog1.ShowDialog()

        Dim selectedpath As String = FolderBrowserDialog1.SelectedPath
        If selectedpath.ToUpper.Contains("WINRAR") Then
            txt_WinRarPath.Text = selectedpath
        Else
            MsgBox("The selected folder is not the correct one, please select a WINRAR folder", MsgBoxStyle.Critical, "Error in selecting a file.")
        End If

    End Sub

    Private Sub btn_SaveConfig_Click(sender As Object, e As EventArgs) Handles btn_SaveConfig.Click
        SaveConfiguration()
    End Sub

    Protected Sub SaveConfiguration()
        Dim message_error As New System.Text.StringBuilder
        Dim save As Boolean = True
        InitRunner.LogEvents.Add("Changing configuration.")

        'Valida los dias
        Try
            Dim BackuptimeInDays As Integer = Integer.Parse(cmb_DaysToBackup.Text)
            If BackuptimeInDays < 0 Then
                save = False
                message_error.Append("- Please select a valid option on Backup every option." & vbNewLine)
            Else
                InitRunner.LogEvents.Add("Changing configuration BackuptimeInDays from " & InitRunner.ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue & " to " & BackuptimeInDays.ToString)
                InitRunner.ConfigurationDATFile.Item("BackuptimeInDays").ParameterValue = BackuptimeInDays.ToString
            End If

        Catch ex As Exception
            save = False
            message_error.Append("- Please select a valid option on Backup every option." & vbNewLine)
            InitRunner.LogEvents.Add("Error in module Configurator:SaveConfiguration:Error > " & ex.Message.ToString)
        End Try

        'Valida el horario que inicia el respaldo
        Dim BackupStarTime As String = cmb_StarTimeBackup.Text
        If Not BackupStarTime.Length = 8 Then
            save = False
            message_error.Append("- Please select a valid option on Backup Star Time." & vbNewLine)
        Else
            Try
                Dim hrs As Date = Convert.ToDateTime(BackupStarTime)
                InitRunner.LogEvents.Add("Changing configuration BackupStarTime from " & InitRunner.ConfigurationDATFile.Item("BackupStarTime").ParameterValue & " to " & BackupStarTime.ToString)
                InitRunner.ConfigurationDATFile.Item("BackupStarTime").ParameterValue = BackupStarTime.ToString

            Catch ex As Exception
                save = False
                message_error.Append("- Please select a valid option on Backup Star Time." & vbNewLine)
                InitRunner.LogEvents.Add("Error in module Configurator:SaveConfiguration:Error > " & ex.Message.ToString)
            End Try

        End If

        'Valida el destination file
        Dim BackupDestination As String = txt_DestinationFolderBackup.Text
        If Not IO.Directory.Exists(BackupDestination) Then
            save = False
            message_error.Append("- Invalid Folder selected in Destination Folder Backup option" & vbNewLine)
        Else
            InitRunner.LogEvents.Add("Changing configuration BackupDestination from " & InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue & " to " & BackupDestination.ToString)
            InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue = BackupDestination
        End If

        'Valida la ruta del log.
        Dim LoggerPath As String = txt_LogPath.Text
        If Not IO.Directory.Exists(LoggerPath) Then
            save = False
            message_error.Append("- Invalid Folder Selected in Log File Path option." & vbNewLine)
        Else
            InitRunner.LogEvents.Add("Changing configuration LoggerPath from " & InitRunner.ConfigurationDATFile.Item("LoggerPath").ParameterValue & " to " & LoggerPath.ToString)
            InitRunner.ConfigurationDATFile.Item("LoggerPath").ParameterValue = LoggerPath
        End If

        'Valida la ruta del programa WINRAR
        Dim CompressorPath As String = txt_WinRarPath.Text
        If Not IO.Directory.Exists(CompressorPath) Then
            save = False
            message_error.Append("- Invalid folder selected in WINRAR application path option." & vbNewLine)
        Else
            InitRunner.LogEvents.Add("Changing configuration CompressorPath from " & InitRunner.ConfigurationDATFile.Item("CompressorPath").ParameterValue & " to " & CompressorPath.ToString)
            InitRunner.ConfigurationDATFile.Item("CompressorPath").ParameterValue = CompressorPath
        End If

        'Valida el numero de backups guardados
        Try
            Dim NumBackupSave As Integer = Integer.Parse(txt_NumStoredBackups.Text)
            If NumBackupSave < 0 Then
                save = False
                message_error.Append("- Invalid value on the option Number of backup to store." & vbNewLine)
            Else
                InitRunner.LogEvents.Add("Changing configuration NumBackupSave from " & InitRunner.ConfigurationDATFile.Item("NumBackupSave").ParameterValue & " to " & NumBackupSave.ToString)
                InitRunner.ConfigurationDATFile.Item("NumBackupSave").ParameterValue = NumBackupSave
            End If

        Catch ex As Exception
            save = False
            message_error.Append("- Invalid value on the option Number of backup to store." & vbNewLine)
            InitRunner.LogEvents.Add("Error in module Configurator:SaveConfiguration:Error > " & ex.Message.ToString)
        End Try

        'Valida la lista si hay items.
        If ListBox_PSTSourceFiles.Items.Count <= 0 Then
            InitRunner.LogEvents.Add("Changing configuration BackupSource from " & InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue & " to empty")
            InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = "empty"
        Else
            'Guarda las rutas de los pst
            Dim valuetosave As String
            Dim firstline As Integer = 0
            For Each line As String In ListBox_PSTSourceFiles.Items
                If firstline = 0 Then
                    valuetosave = line
                Else
                    valuetosave = valuetosave & "|" & line
                End If
                firstline = firstline + 1
            Next
            InitRunner.LogEvents.Add("Changing configuration BackupSource from " & InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue & " to " & valuetosave)
            InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue = valuetosave
        End If

        'Salva la configuracion si todo esta bien
        If save Then
            InitRunner.UpdatedDTOConfigFile()
            InitRunner.LogEvents.Add("DTO file configration saved")
            MsgBox("Configuration Saved.", MsgBoxStyle.Information, "Configuration Saved.")
        Else
            MsgBox("There was an error saving the configuration please check the following problems" & vbNewLine & message_error.ToString, MsgBoxStyle.Critical, "Error saving the configuration")
        End If

    End Sub

End Class
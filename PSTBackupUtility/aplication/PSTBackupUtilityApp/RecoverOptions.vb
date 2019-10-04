Imports PSTBacktupUtilityCore.com.lib
Public Class RecoverOptions

    Private _PSTFiles As Dictionary(Of PSTBacktupUtilityCore.com.lib.objects.FileObj, PSTBacktupUtilityCore.com.lib.objects.FileObjCollection)
    Private _SelectedBackup As objects.FileObj
    Private _SelectedPSTFile As objects.FileObj
    'Create a new ImageList with the size you want the icons to be
    Private ImgList As New ImageList With {.ImageSize = New Size(24, 24)}


    Private Sub RecoverOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfiguraListViews()
        SearchBackupsOfPST()
        FillPSTFiles()
    End Sub

    Public Sub ConfiguraListViews()
        'configura Listview 1
        ListView1.View = View.Details
        ListView1.FullRowSelect = True
        ListView1.SmallImageList = ImgList

        ListView1.Columns.Add("Icon", 40, HorizontalAlignment.Left)          'Add column 1
        ListView1.Columns.Add("Name", 140, HorizontalAlignment.Left) 'Add column 2
        ListView1.Columns.Add("Size", 140, HorizontalAlignment.Left) 'Add column 3
        ListView1.Columns.Add("Full Path Source", 350, HorizontalAlignment.Left) 'Add column 3

        ' Configura Listview2
        ListView2.View = View.Details
        ListView2.FullRowSelect = True
        ListView2.SmallImageList = ImgList

        ListView2.Columns.Add("Icon", 40, HorizontalAlignment.Left)          'Add column 1
        ListView2.Columns.Add("Backup Date", 140, HorizontalAlignment.Left) 'Add column 2
        ListView2.Columns.Add("Size", 140, HorizontalAlignment.Left) 'Add column 3
        ListView2.Columns.Add("Full Path Source", 350, HorizontalAlignment.Left) 'Add column 3
    End Sub

    Public Sub SearchBackupsOfPST()
        Dim files As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")
        Dim BackupFoler As String = InitRunner.ConfigurationDATFile.Item("BackupDestination").ParameterValue
        _PSTFiles = New Dictionary(Of PSTBacktupUtilityCore.com.lib.objects.FileObj, PSTBacktupUtilityCore.com.lib.objects.FileObjCollection)

        For Each Str As String In files
            Dim fileio As New IO.FileInfo(Str)
            Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(fileio)
            _PSTFiles.Add(fileobj, New PSTBacktupUtilityCore.com.lib.objects.FileObjCollection)
        Next

        'Obtiene todos los archivos de los respaldos.
        Dim BackupPSTFiles As String() = IO.Directory.GetFiles(BackupFoler)

        For Each PSTFile As KeyValuePair(Of PSTBacktupUtilityCore.com.lib.objects.FileObj, PSTBacktupUtilityCore.com.lib.objects.FileObjCollection) In _PSTFiles
            Dim key As PSTBacktupUtilityCore.com.lib.objects.FileObj = PSTFile.Key
            Dim keyname As String = GetKeyNameFile(key)
            For Each Str As String In BackupPSTFiles
                Dim backupfilekey As String = Nothing
                If Str.Contains("-") Then
                    If Str.Length > 1 Then
                        Dim filename_split As String() = Str.Split("-")
                        backupfilekey = filename_split(1)
                    End If
                End If
                If Not IsNothing(backupfilekey) Then
                    If backupfilekey.Equals(keyname) Then
                        PSTFile.Value.Add(New PSTBacktupUtilityCore.com.lib.objects.FileObj(New IO.FileInfo(Str)))
                    End If
                End If
            Next
        Next

    End Sub

    Public Sub FillPSTFiles()

        For Each item As KeyValuePair(Of PSTBacktupUtilityCore.com.lib.objects.FileObj, PSTBacktupUtilityCore.com.lib.objects.FileObjCollection) In _PSTFiles
            Dim filepst As PSTBacktupUtilityCore.com.lib.objects.FileObj = item.Key
            'Agrega el icono a la imagenlist
            Try
                ImgList.Images.Add(Icon.ExtractAssociatedIcon(filepst.FullPathFile).ToBitmap)
            Catch ex As Exception
                ImgList.Images.Add(New Icon(Application.StartupPath.ToString & "\notfound.ico"))
            End Try

            Dim lvi As New ListViewItem("", ImgList.Images.Count - 1)
            'lvi.SubItems.Add(IO.Path.GetFileNameWithoutExtension(fileobj.FullPathFile)) 'Add just the name of the exe file
            lvi.SubItems.Add(filepst.FileName) 'Add just the name of the exe file
            If filepst.SizeFileInGB >= 1 Then
                lvi.SubItems.Add(filepst.SizeFileInGB.ToString("0.00") & " GB")
            Else
                If filepst.SizeFileinMB < 1 Then
                    lvi.SubItems.Add(filepst.SizeFileinKB.ToString("0.00") & " KB")
                Else
                    lvi.SubItems.Add(filepst.SizeFileinMB.ToString("0.00") & " MB")
                End If

            End If

            lvi.SubItems.Add(filepst.FullPathFile)
            ListView1.Items.Add(lvi)
        Next
        'Dim files As String() = InitRunner.ConfigurationDATFile.Item("BackupSource").ParameterValue.Split("|")
        'For Each Str As String In files
        '    Dim fileio As New IO.FileInfo(Str)
        '    Dim fileobj As New PSTBacktupUtilityCore.com.lib.objects.FileObj(fileio)
        '    _PSTFiles.Add(fileobj, New PSTBacktupUtilityCore.com.lib.objects.FileObjCollection)

        '    'Agrega el icono a la imagenlist
        '    ImgList.Images.Add(Icon.ExtractAssociatedIcon(fileobj.FullPathFile).ToBitmap)
        '    Dim lvi As New ListViewItem("", ImgList.Images.Count - 1)
        '    'lvi.SubItems.Add(IO.Path.GetFileNameWithoutExtension(fileobj.FullPathFile)) 'Add just the name of the exe file
        '    lvi.SubItems.Add(fileobj.FileName) 'Add just the name of the exe file
        '    If fileobj.SizeFileInGB >= 1 Then
        '        lvi.SubItems.Add(fileobj.SizeFileInGB.ToString("0.00") & " GB")
        '    Else
        '        If fileobj.SizeFileinMB < 1 Then
        '            lvi.SubItems.Add(fileobj.SizeFileinKB.ToString("0.00") & " KB")
        '        Else
        '            lvi.SubItems.Add(fileobj.SizeFileinMB.ToString("0.00") & " MB")
        '        End If

        '    End If

        '    lvi.SubItems.Add(fileobj.FullPathFile)
        '    ListView1.Items.Add(lvi)
        'Next
    End Sub

    Public Sub DisplayBackupItems(BackupFiles As objects.FileObjCollection)

        For Each file As objects.FileObj In BackupFiles.Items
            'Agrega el icono a la imagenlist
            ImgList.Images.Add(Icon.ExtractAssociatedIcon(file.FullPathFile).ToBitmap)
            Dim lvi As New ListViewItem("", ImgList.Images.Count - 1)
            'lvi.SubItems.Add(IO.Path.GetFileNameWithoutExtension(fileobj.FullPathFile)) 'Add just the name of the exe file
            lvi.SubItems.Add(file.LastWriteTime.ToString("dd-MM-yyyy hh:mm")) 'Add just the name of the exe file
            If file.SizeFileInGB >= 1 Then
                lvi.SubItems.Add(file.SizeFileInGB.ToString("0.00") & " GB")
            Else
                If file.SizeFileinMB < 1 Then
                    lvi.SubItems.Add(file.SizeFileinKB.ToString("0.00") & " KB")
                Else
                    lvi.SubItems.Add(file.SizeFileinMB.ToString("0.00") & " MB")
                End If

            End If

            lvi.SubItems.Add(file.FullPathFile)
            ListView2.Items.Add(lvi)
        Next


    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click

        'Limpia el listview2
        ListView2.Items.Clear()

        'Crea el objeto para buscarlo en el objeto diccionario
        Dim Listview_selectedFile As New objects.FileObj(New IO.FileInfo(ListView1.SelectedItems.Item(0).SubItems.Item(3).Text))
        Dim ListView_SelectedFileKey As String = GetKeyNameFile(Listview_selectedFile)

        'Dim selectedfiles As New objects.FileObjCollection

        For Each item As KeyValuePair(Of objects.FileObj, objects.FileObjCollection) In _PSTFiles
            Dim selectedFileObj As objects.FileObj = item.Key
            Dim selectedFileObj_Key As String = GetKeyNameFile(selectedFileObj)
            If ListView_SelectedFileKey.Equals(selectedFileObj_Key) Then
                DisplayBackupItems(item.Value)
            End If
        Next

        'Dim getBackups As objects.FileObjCollection = _PSTFiles(filestr)
        'Console.WriteLine(getBackups.Items.Count)
        'Console.WriteLine(ListView1.SelectedItems.Item(0).SubItems.Item(1).Text)
    End Sub

    Private Function GetKeyNameFile(fileobj As PSTBacktupUtilityCore.com.lib.objects.FileObj) As String
        Dim keynamefile As String
        keynamefile = fileobj.FileName.Substring(0, 3) & fileobj.FileName.Substring(fileobj.FileName.Length - 7, 3) & fileobj.FullPathFile.Length
        Return keynamefile
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        txt_FolderToRestore.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub radiobtn_defaultdest_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtn_defaultdest.CheckedChanged
        Button1.Enabled = False
        txt_FolderToRestore.Text = ""
    End Sub

    Private Sub radiobtn_selectDestination_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtn_selectDestination.CheckedChanged
        Button1.Enabled = True
    End Sub

    Private Sub ListView2_Click(sender As Object, e As EventArgs) Handles ListView2.Click
        _SelectedBackup = New objects.FileObj(New IO.FileInfo(ListView2.SelectedItems.Item(0).SubItems.Item(3).Text))
        _SelectedPSTFile = New objects.FileObj(New IO.FileInfo(ListView1.SelectedItems.Item(0).SubItems.Item(3).Text))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.ControlBox = False

        Dim RestoreBackupFrm As New RestoreBackupProcess
        RestoreBackupFrm.Configuration = InitRunner.ConfigurationDATFile

        If Not IsNothing(_SelectedBackup) Then
            If radiobtn_defaultdest.Checked Then
                Dim response As Integer = MsgBox("This action will replace the original file located in its original path" & vbNewLine & vbNewLine & "Do you want to continue restoring the file ?", MsgBoxStyle.YesNo, "Recover PST File")
                If response = vbYes Then
                    RestoreBackupFrm.FileToRestore = _SelectedBackup
                    RestoreBackupFrm.DestinationPath = _SelectedBackup.FullPathFile.Substring(0, 3)
                    RestoreBackupFrm.RestoreToDestinationPath = True
                    RestoreBackupFrm.Show()
                    Timer_ProcessRestore.Enabled = True
                    Timer_ProcessRestore.Start()
                    Button2.Enabled = False

                End If
            End If
            If radiobtn_selectDestination.Checked Then
                Dim pathtorestore As String = txt_FolderToRestore.Text
                Dim response As Integer = MsgBox("Do you want to continue restoring the File?", MsgBoxStyle.YesNo, "Recover PST File")
                If pathtorestore.Length > 0 Then
                    If response = vbYes Then
                        RestoreBackupFrm.FileToRestore = _SelectedBackup
                        RestoreBackupFrm.DestinationPath = pathtorestore
                        RestoreBackupFrm.RestoreToDestinationPath = False
                        RestoreBackupFrm.Show()
                        Timer_ProcessRestore.Enabled = True
                        Timer_ProcessRestore.Start()
                        Button2.Enabled = False
                    End If
                Else
                    MsgBox("Please select a valid folder to restore the file", MsgBoxStyle.Critical, "Backup to restore")
                End If
            End If
        Else
            MsgBox("Please select a backup to restore", MsgBoxStyle.Exclamation, "Backup to restore")
        End If
    End Sub

    Private Sub Timer_ProcessRestore_Tick(sender As Object, e As EventArgs) Handles Timer_ProcessRestore.Tick

        Try
            If IsNothing(Application.OpenForms.Item("RestoreBackupProcess")) Then
                Timer_ProcessRestore.Stop()
                Button2.Enabled = True

                'Valida si el respaldo se realizo con exito
                If radiobtn_defaultdest.Checked Then
                    If IO.File.Exists(_SelectedPSTFile.FullPathFile) Then
                        MsgBox("Files Recovery Complete", MsgBoxStyle.Information, "PST Backup Utility")
                        Dim openfolder As String = "explorer " & _SelectedPSTFile.FilePath
                        Shell(openfolder, AppWinStyle.NormalFocus)
                    Else
                        MsgBox("Error recovering the PST File > " & vbNewLine & _SelectedPSTFile.FullPathFile, MsgBoxStyle.Critical, "Error recovering")
                    End If
                Else

                    If IO.File.Exists(txt_FolderToRestore.Text & "\" & _SelectedPSTFile.FileName) Then
                        MsgBox("Files Recovery Complete", MsgBoxStyle.Information, "PST Backup Utility")
                        Dim openfolder As String = "explorer " & txt_FolderToRestore.Text
                        Shell(openfolder, AppWinStyle.NormalFocus)
                    Else
                        MsgBox("Error recovering the PST File > " & vbNewLine & _SelectedPSTFile.FullPathFile, MsgBoxStyle.Critical, "Error recovering")
                    End If


                End If

                ListView1.Items.Clear()
                ListView2.Items.Clear()
                _SelectedBackup = Nothing
                _SelectedPSTFile = Nothing
                Me.ControlBox = True

                SearchBackupsOfPST()
                FillPSTFiles()

            End If
        Catch ex As Exception
            InitRunner.LogEvents.Add("Error in module RecoverOptions:Timer_ProcessRestore_Tick:Error > " & ex.Message.ToString)
        End Try


    End Sub
End Class
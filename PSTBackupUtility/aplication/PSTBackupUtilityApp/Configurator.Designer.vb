<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configurator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configurator))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btn_SaveConfig = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txt_WinRarPath = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_NumStoredBackups = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_LogPath = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListBox_PSTSourceFiles = New System.Windows.Forms.ListBox()
        Me.btn_ScanForPST = New System.Windows.Forms.Button()
        Me.btn_RemoveItem = New System.Windows.Forms.Button()
        Me.btn_AddPSTFile = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_DestinationFolderBackup = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_StarTimeBackup = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_DaysToBackup = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_tokenexplainlabel = New System.Windows.Forms.Label()
        Me.txt_Token = New System.Windows.Forms.TextBox()
        Me.lbl_Token = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Timer_wait = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(598, 24)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btn_SaveConfig)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 491)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(598, 38)
        Me.Panel2.TabIndex = 1
        '
        'btn_SaveConfig
        '
        Me.btn_SaveConfig.Location = New System.Drawing.Point(504, 6)
        Me.btn_SaveConfig.Name = "btn_SaveConfig"
        Me.btn_SaveConfig.Size = New System.Drawing.Size(75, 23)
        Me.btn_SaveConfig.TabIndex = 0
        Me.btn_SaveConfig.Text = "Save"
        Me.btn_SaveConfig.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.txt_WinRarPath)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.txt_NumStoredBackups)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.txt_LogPath)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.SplitContainer1)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.txt_DestinationFolderBackup)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.cmb_StarTimeBackup)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.cmb_DaysToBackup)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.lbl_tokenexplainlabel)
        Me.Panel3.Controls.Add(Me.txt_Token)
        Me.Panel3.Controls.Add(Me.lbl_Token)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 24)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(598, 467)
        Me.Panel3.TabIndex = 2
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(504, 399)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 20
        Me.Button3.Text = "Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txt_WinRarPath
        '
        Me.txt_WinRarPath.Location = New System.Drawing.Point(149, 401)
        Me.txt_WinRarPath.Name = "txt_WinRarPath"
        Me.txt_WinRarPath.Size = New System.Drawing.Size(349, 20)
        Me.txt_WinRarPath.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 404)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(127, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "WinRAR application path"
        '
        'txt_NumStoredBackups
        '
        Me.txt_NumStoredBackups.Location = New System.Drawing.Point(151, 435)
        Me.txt_NumStoredBackups.Name = "txt_NumStoredBackups"
        Me.txt_NumStoredBackups.Size = New System.Drawing.Size(62, 20)
        Me.txt_NumStoredBackups.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 438)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Number of Backps to store"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(504, 365)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Browse"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 371)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Log file Path:"
        '
        'txt_LogPath
        '
        Me.txt_LogPath.Location = New System.Drawing.Point(90, 368)
        Me.txt_LogPath.Name = "txt_LogPath"
        Me.txt_LogPath.Size = New System.Drawing.Size(408, 20)
        Me.txt_LogPath.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "PST Files Found"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(7, 180)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListBox_PSTSourceFiles)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_ScanForPST)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_RemoveItem)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_AddPSTFile)
        Me.SplitContainer1.Size = New System.Drawing.Size(579, 169)
        Me.SplitContainer1.SplitterDistance = 504
        Me.SplitContainer1.TabIndex = 11
        '
        'ListBox_PSTSourceFiles
        '
        Me.ListBox_PSTSourceFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox_PSTSourceFiles.FormattingEnabled = True
        Me.ListBox_PSTSourceFiles.Location = New System.Drawing.Point(0, 0)
        Me.ListBox_PSTSourceFiles.Name = "ListBox_PSTSourceFiles"
        Me.ListBox_PSTSourceFiles.Size = New System.Drawing.Size(504, 169)
        Me.ListBox_PSTSourceFiles.TabIndex = 0
        '
        'btn_ScanForPST
        '
        Me.btn_ScanForPST.Location = New System.Drawing.Point(4, 87)
        Me.btn_ScanForPST.Name = "btn_ScanForPST"
        Me.btn_ScanForPST.Size = New System.Drawing.Size(60, 43)
        Me.btn_ScanForPST.TabIndex = 2
        Me.btn_ScanForPST.Text = "Scan For PST files"
        Me.btn_ScanForPST.UseVisualStyleBackColor = True
        '
        'btn_RemoveItem
        '
        Me.btn_RemoveItem.Location = New System.Drawing.Point(4, 57)
        Me.btn_RemoveItem.Name = "btn_RemoveItem"
        Me.btn_RemoveItem.Size = New System.Drawing.Size(60, 23)
        Me.btn_RemoveItem.TabIndex = 1
        Me.btn_RemoveItem.Text = "Remove"
        Me.btn_RemoveItem.UseVisualStyleBackColor = True
        '
        'btn_AddPSTFile
        '
        Me.btn_AddPSTFile.Location = New System.Drawing.Point(3, 27)
        Me.btn_AddPSTFile.Name = "btn_AddPSTFile"
        Me.btn_AddPSTFile.Size = New System.Drawing.Size(61, 23)
        Me.btn_AddPSTFile.TabIndex = 0
        Me.btn_AddPSTFile.Text = "Add PST"
        Me.btn_AddPSTFile.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(504, 127)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txt_DestinationFolderBackup
        '
        Me.txt_DestinationFolderBackup.Location = New System.Drawing.Point(157, 129)
        Me.txt_DestinationFolderBackup.Name = "txt_DestinationFolderBackup"
        Me.txt_DestinationFolderBackup.Size = New System.Drawing.Size(341, 20)
        Me.txt_DestinationFolderBackup.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Destination Folder Backup :"
        '
        'cmb_StarTimeBackup
        '
        Me.cmb_StarTimeBackup.FormattingEnabled = True
        Me.cmb_StarTimeBackup.Items.AddRange(New Object() {"00:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00", "06:00:00", "07:00:00", "08:00:00", "09:00:00", "10:00:00", "11:00:00", "12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00", "18:00:00", "19:00:00", "20:00:00", "21:00:00", "22:00:00", "23:00:00"})
        Me.cmb_StarTimeBackup.Location = New System.Drawing.Point(331, 95)
        Me.cmb_StarTimeBackup.Name = "cmb_StarTimeBackup"
        Me.cmb_StarTimeBackup.Size = New System.Drawing.Size(121, 21)
        Me.cmb_StarTimeBackup.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(229, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Backup Start Time:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(183, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Days"
        '
        'cmb_DaysToBackup
        '
        Me.cmb_DaysToBackup.FormattingEnabled = True
        Me.cmb_DaysToBackup.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
        Me.cmb_DaysToBackup.Location = New System.Drawing.Point(99, 96)
        Me.cmb_DaysToBackup.Name = "cmb_DaysToBackup"
        Me.cmb_DaysToBackup.Size = New System.Drawing.Size(76, 21)
        Me.cmb_DaysToBackup.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Backup every:"
        '
        'lbl_tokenexplainlabel
        '
        Me.lbl_tokenexplainlabel.Location = New System.Drawing.Point(13, 39)
        Me.lbl_tokenexplainlabel.Name = "lbl_tokenexplainlabel"
        Me.lbl_tokenexplainlabel.Size = New System.Drawing.Size(486, 54)
        Me.lbl_tokenexplainlabel.TabIndex = 2
        Me.lbl_tokenexplainlabel.Text = resources.GetString("lbl_tokenexplainlabel.Text")
        '
        'txt_Token
        '
        Me.txt_Token.Location = New System.Drawing.Point(64, 7)
        Me.txt_Token.Name = "txt_Token"
        Me.txt_Token.ReadOnly = True
        Me.txt_Token.Size = New System.Drawing.Size(434, 20)
        Me.txt_Token.TabIndex = 1
        '
        'lbl_Token
        '
        Me.lbl_Token.AutoSize = True
        Me.lbl_Token.Location = New System.Drawing.Point(13, 9)
        Me.lbl_Token.Name = "lbl_Token"
        Me.lbl_Token.Size = New System.Drawing.Size(44, 13)
        Me.lbl_Token.TabIndex = 0
        Me.lbl_Token.Text = "Token :"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Timer_wait
        '
        Me.Timer_wait.Interval = 1000
        '
        'Configurator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 529)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(614, 567)
        Me.MinimumSize = New System.Drawing.Size(614, 567)
        Me.Name = "Configurator"
        Me.Text = "PST Backup Utility Configuration"
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lbl_tokenexplainlabel As System.Windows.Forms.Label
    Friend WithEvents txt_Token As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Token As System.Windows.Forms.Label
    Friend WithEvents cmb_DaysToBackup As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_StarTimeBackup As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txt_DestinationFolderBackup As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ListBox_PSTSourceFiles As System.Windows.Forms.ListBox
    Friend WithEvents btn_RemoveItem As System.Windows.Forms.Button
    Friend WithEvents btn_AddPSTFile As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btn_ScanForPST As System.Windows.Forms.Button
    Friend WithEvents Timer_wait As System.Windows.Forms.Timer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_LogPath As System.Windows.Forms.TextBox
    Friend WithEvents txt_NumStoredBackups As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txt_WinRarPath As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btn_SaveConfig As System.Windows.Forms.Button
End Class

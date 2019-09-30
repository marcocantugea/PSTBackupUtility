<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_lastbackupdate = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer_RunBackup = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_nextbackup = New System.Windows.Forms.Label()
        Me.Timer_CheckOutlookOpen = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Last Backup Time : "
        '
        'lbl_lastbackupdate
        '
        Me.lbl_lastbackupdate.AutoSize = True
        Me.lbl_lastbackupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_lastbackupdate.Location = New System.Drawing.Point(150, 8)
        Me.lbl_lastbackupdate.Name = "lbl_lastbackupdate"
        Me.lbl_lastbackupdate.Size = New System.Drawing.Size(130, 15)
        Me.lbl_lastbackupdate.TabIndex = 2
        Me.lbl_lastbackupdate.Text = "25-Sep-2019 01:00:00"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(309, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Open Backup Folder"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-2, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(439, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "________________________________________________________________________"
        '
        'Button4
        '
        Me.Button4.Image = Global.PSTBackupUtilityApp.My.Resources.Resources.Folder_URL_History_icon
        Me.Button4.Location = New System.Drawing.Point(286, 56)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(139, 179)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Restore PST"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.PSTBackupUtilityApp.My.Resources.Resources.pst_file_backup1
        Me.Button3.Location = New System.Drawing.Point(141, 56)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(139, 179)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Backup Now"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.PSTBackupUtilityApp.My.Resources.Resources.Settings_icon
        Me.Button1.Location = New System.Drawing.Point(9, 56)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 179)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Configuration"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer_RunBackup
        '
        Me.Timer_RunBackup.Interval = 1000
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Next Scheduled Backup :"
        '
        'lbl_nextbackup
        '
        Me.lbl_nextbackup.AutoSize = True
        Me.lbl_nextbackup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nextbackup.Location = New System.Drawing.Point(151, 28)
        Me.lbl_nextbackup.Name = "lbl_nextbackup"
        Me.lbl_nextbackup.Size = New System.Drawing.Size(127, 16)
        Me.lbl_nextbackup.TabIndex = 8
        Me.lbl_nextbackup.Text = "7-Ago-2019 14:00:00"
        '
        'Timer_CheckOutlookOpen
        '
        Me.Timer_CheckOutlookOpen.Interval = 350
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 247)
        Me.Controls.Add(Me.lbl_nextbackup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lbl_lastbackupdate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(450, 285)
        Me.MinimumSize = New System.Drawing.Size(450, 285)
        Me.Name = "MainMenu"
        Me.Text = "PST Backup Utility - Dashboard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_lastbackupdate As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Timer_RunBackup As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_nextbackup As System.Windows.Forms.Label
    Friend WithEvents Timer_CheckOutlookOpen As System.Windows.Forms.Timer
End Class

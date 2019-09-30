<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackupProcesFormS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BackupProcesFormS))
        Me.Timer_ActiveProcess = New System.Windows.Forms.Timer(Me.components)
        Me.lbl_ProcessedFileLabel = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lbl_enlapsedtime = New System.Windows.Forms.Label()
        Me.btn_CancelBackup = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_PstFileSize = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_WrritenData = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Timer_ActiveProcess
        '
        Me.Timer_ActiveProcess.Enabled = True
        Me.Timer_ActiveProcess.Interval = 1000
        '
        'lbl_ProcessedFileLabel
        '
        Me.lbl_ProcessedFileLabel.Location = New System.Drawing.Point(12, 9)
        Me.lbl_ProcessedFileLabel.Name = "lbl_ProcessedFileLabel"
        Me.lbl_ProcessedFileLabel.Size = New System.Drawing.Size(375, 37)
        Me.lbl_ProcessedFileLabel.TabIndex = 0
        Me.lbl_ProcessedFileLabel.Text = " "
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 92)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(375, 32)
        Me.ProgressBar1.TabIndex = 1
        '
        'lbl_enlapsedtime
        '
        Me.lbl_enlapsedtime.Location = New System.Drawing.Point(245, 63)
        Me.lbl_enlapsedtime.Name = "lbl_enlapsedtime"
        Me.lbl_enlapsedtime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_enlapsedtime.Size = New System.Drawing.Size(142, 23)
        Me.lbl_enlapsedtime.TabIndex = 2
        Me.lbl_enlapsedtime.Text = " "
        '
        'btn_CancelBackup
        '
        Me.btn_CancelBackup.Location = New System.Drawing.Point(12, 130)
        Me.btn_CancelBackup.Name = "btn_CancelBackup"
        Me.btn_CancelBackup.Size = New System.Drawing.Size(372, 36)
        Me.btn_CancelBackup.TabIndex = 3
        Me.btn_CancelBackup.Text = "Cancel Backup"
        Me.btn_CancelBackup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "PST Size:"
        '
        'lbl_PstFileSize
        '
        Me.lbl_PstFileSize.AutoSize = True
        Me.lbl_PstFileSize.Location = New System.Drawing.Point(76, 49)
        Me.lbl_PstFileSize.Name = "lbl_PstFileSize"
        Me.lbl_PstFileSize.Size = New System.Drawing.Size(13, 13)
        Me.lbl_PstFileSize.TabIndex = 5
        Me.lbl_PstFileSize.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Written Data Stored:"
        '
        'lbl_WrritenData
        '
        Me.lbl_WrritenData.AutoSize = True
        Me.lbl_WrritenData.Location = New System.Drawing.Point(129, 73)
        Me.lbl_WrritenData.Name = "lbl_WrritenData"
        Me.lbl_WrritenData.Size = New System.Drawing.Size(13, 13)
        Me.lbl_WrritenData.TabIndex = 7
        Me.lbl_WrritenData.Text = "0"
        '
        'BackupProcesFormS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 178)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbl_WrritenData)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_PstFileSize)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_CancelBackup)
        Me.Controls.Add(Me.lbl_enlapsedtime)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lbl_ProcessedFileLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(412, 216)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(412, 216)
        Me.Name = "BackupProcesFormS"
        Me.Text = "Creating PST Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer_ActiveProcess As System.Windows.Forms.Timer
    Friend WithEvents lbl_ProcessedFileLabel As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lbl_enlapsedtime As System.Windows.Forms.Label
    Friend WithEvents btn_CancelBackup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_PstFileSize As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl_WrritenData As System.Windows.Forms.Label
End Class

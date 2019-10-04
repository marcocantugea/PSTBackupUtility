<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackupScheduler
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
        Me.Timer_Pospne_Countdown = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Backup = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_CheckOutlookOpen = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Timer_Pospne_Countdown
        '
        Me.Timer_Pospne_Countdown.Interval = 250
        '
        'Timer_Backup
        '
        Me.Timer_Backup.Interval = 350
        '
        'Timer_CheckOutlookOpen
        '
        Me.Timer_CheckOutlookOpen.Interval = 400
        '
        'BackupScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Name = "BackupScheduler"
        Me.Text = "BackupScheduler"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer_Pospne_Countdown As System.Windows.Forms.Timer
    Friend WithEvents Timer_Backup As System.Windows.Forms.Timer
    Friend WithEvents Timer_CheckOutlookOpen As System.Windows.Forms.Timer
End Class

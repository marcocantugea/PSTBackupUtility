<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadProcess
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
        Me.pb_load = New System.Windows.Forms.ProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Progresslabel = New System.Windows.Forms.Label()
        Me.TimerRunning = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pb_load
        '
        Me.pb_load.Location = New System.Drawing.Point(3, 3)
        Me.pb_load.Name = "pb_load"
        Me.pb_load.Size = New System.Drawing.Size(434, 23)
        Me.pb_load.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pb_load)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(445, 31)
        Me.Panel1.TabIndex = 2
        '
        'lbl_Progresslabel
        '
        Me.lbl_Progresslabel.AutoSize = True
        Me.lbl_Progresslabel.Location = New System.Drawing.Point(0, 9)
        Me.lbl_Progresslabel.Name = "lbl_Progresslabel"
        Me.lbl_Progresslabel.Size = New System.Drawing.Size(119, 13)
        Me.lbl_Progresslabel.TabIndex = 3
        Me.lbl_Progresslabel.Text = "Loading Configuration..."
        '
        'TimerRunning
        '
        Me.TimerRunning.Enabled = True
        Me.TimerRunning.Interval = 500
        '
        'LoadProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 64)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbl_Progresslabel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "LoadProcess"
        Me.Text = "Loading Configurations"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pb_load As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Progresslabel As System.Windows.Forms.Label
    Friend WithEvents TimerRunning As System.Windows.Forms.Timer
End Class

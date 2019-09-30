<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CloseOutlookMessage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CloseOutlookMessage))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_pospone = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_Timercountdown = New System.Windows.Forms.Label()
        Me.Timer_Countdown = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(380, 56)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "The MS Outlook application needs to be closed in order to create the backup of th" & _
    "e PST files and during this process the application has to be close."
        '
        'cmb_pospone
        '
        Me.cmb_pospone.FormattingEnabled = True
        Me.cmb_pospone.Items.AddRange(New Object() {"15 min.", "30 min.", "1 hr.", "2 hrs.", "3 hrs."})
        Me.cmb_pospone.Location = New System.Drawing.Point(93, 122)
        Me.cmb_pospone.Name = "cmb_pospone"
        Me.cmb_pospone.Size = New System.Drawing.Size(89, 21)
        Me.cmb_pospone.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Remind me in:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(188, 120)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Postpone"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(13, 160)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 41)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cancel Backup"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(259, 160)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(135, 40)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Close Microsoft Outlook and Continue"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(13, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(186, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "The backup will star in : "
        '
        'lbl_Timercountdown
        '
        Me.lbl_Timercountdown.AutoSize = True
        Me.lbl_Timercountdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Timercountdown.ForeColor = System.Drawing.Color.Red
        Me.lbl_Timercountdown.Location = New System.Drawing.Point(209, 80)
        Me.lbl_Timercountdown.Name = "lbl_Timercountdown"
        Me.lbl_Timercountdown.Size = New System.Drawing.Size(54, 20)
        Me.lbl_Timercountdown.TabIndex = 7
        Me.lbl_Timercountdown.Text = "00:00"
        '
        'Timer_Countdown
        '
        Me.Timer_Countdown.Interval = 250
        '
        'CloseOutlookMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(405, 209)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbl_Timercountdown)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmb_pospone)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CloseOutlookMessage"
        Me.Text = "Closing Microsoft Outlook"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_pospone As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl_Timercountdown As System.Windows.Forms.Label
    Friend WithEvents Timer_Countdown As System.Windows.Forms.Timer
End Class

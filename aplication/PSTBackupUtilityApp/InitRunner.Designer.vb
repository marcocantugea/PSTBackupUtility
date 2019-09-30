<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitRunner
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InitRunner))
        Me._Global_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.GlobalContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Dashboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Config = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GlobalContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        '_Global_Timer
        '
        Me._Global_Timer.Enabled = True
        Me._Global_Timer.Interval = 1000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.GlobalContextMenu
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "PST Backup Utility"
        Me.NotifyIcon1.Visible = True
        '
        'GlobalContextMenu
        '
        Me.GlobalContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem_Dashboard, Me.ToolStripMenuItem_Config, Me.ToolStripSeparator1, Me.ToolStripMenuItem_Exit})
        Me.GlobalContextMenu.Name = "GlobalContextMenu"
        Me.GlobalContextMenu.Size = New System.Drawing.Size(153, 120)
        '
        'ToolStripMenuItem_Dashboard
        '
        Me.ToolStripMenuItem_Dashboard.Name = "ToolStripMenuItem_Dashboard"
        Me.ToolStripMenuItem_Dashboard.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem_Dashboard.Text = "Dashboard"
        '
        'ToolStripMenuItem_Config
        '
        Me.ToolStripMenuItem_Config.Name = "ToolStripMenuItem_Config"
        Me.ToolStripMenuItem_Config.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem_Config.Text = "Configuration"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripMenuItem_Exit
        '
        Me.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit"
        Me.ToolStripMenuItem_Exit.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem_Exit.Text = "Exit"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem1.Text = "Restore PST"
        '
        'InitRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Name = "InitRunner"
        Me.Text = "InitRunner"
        Me.GlobalContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents _Global_Timer As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents GlobalContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Config As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Dashboard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class

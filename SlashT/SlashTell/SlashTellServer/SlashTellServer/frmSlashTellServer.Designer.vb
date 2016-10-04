<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSlashTellServer
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
        Me.txtServerConsole = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLaunch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtServerConsole
        '
        Me.txtServerConsole.Location = New System.Drawing.Point(12, 41)
        Me.txtServerConsole.Multiline = True
        Me.txtServerConsole.Name = "txtServerConsole"
        Me.txtServerConsole.Size = New System.Drawing.Size(427, 404)
        Me.txtServerConsole.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(12, 451)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(427, 20)
        Me.TextBox2.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(445, 451)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 20)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnLaunch
        '
        Me.btnLaunch.Location = New System.Drawing.Point(445, 12)
        Me.btnLaunch.Name = "btnLaunch"
        Me.btnLaunch.Size = New System.Drawing.Size(75, 23)
        Me.btnLaunch.TabIndex = 3
        Me.btnLaunch.Text = "Launch"
        Me.btnLaunch.UseVisualStyleBackColor = True
        '
        'frmSlashTellServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 483)
        Me.Controls.Add(Me.btnLaunch)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.txtServerConsole)
        Me.Name = "frmSlashTellServer"
        Me.Text = "SlashTell -- Server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtServerConsole As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnLaunch As System.Windows.Forms.Button

End Class

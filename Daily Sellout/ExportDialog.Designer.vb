<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportDialog
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.myM = New System.Windows.Forms.Label()
        Me.myY = New System.Windows.Forms.Label()
        Me.mrM = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_NoOfFiles = New System.Windows.Forms.Label()
        Me.LstFiles = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.savedialog = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.LstFiles2 = New System.Windows.Forms.ListBox()
        Me.lbl_NoOfFiles2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(172, 163)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(310, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Export and merge existing records from a certain month"
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.ItemHeight = 13
        Me.ComboBox2.Location = New System.Drawing.Point(185, 46)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(112, 21)
        Me.ComboBox2.Sorted = True
        Me.ComboBox2.TabIndex = 4
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.ComboBox1.Location = New System.Drawing.Point(62, 46)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(112, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'myM
        '
        Me.myM.AutoSize = True
        Me.myM.Location = New System.Drawing.Point(474, 38)
        Me.myM.Name = "myM"
        Me.myM.Size = New System.Drawing.Size(63, 13)
        Me.myM.TabIndex = 50
        Me.myM.Text = "MYMONTH"
        Me.myM.Visible = False
        '
        'myY
        '
        Me.myY.AutoSize = True
        Me.myY.Location = New System.Drawing.Point(474, 25)
        Me.myY.Name = "myY"
        Me.myY.Size = New System.Drawing.Size(52, 13)
        Me.myY.TabIndex = 51
        Me.myY.Text = "MYYEAR"
        Me.myY.Visible = False
        '
        'mrM
        '
        Me.mrM.AutoSize = True
        Me.mrM.Location = New System.Drawing.Point(474, 51)
        Me.mrM.Name = "mrM"
        Me.mrM.Size = New System.Drawing.Size(91, 13)
        Me.mrM.TabIndex = 52
        Me.mrM.Text = "MYREALMONTH"
        Me.mrM.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(62, 90)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(205, 20)
        Me.TextBox1.TabIndex = 53
        Me.TextBox1.Text = "D:\Daily Sell-out Report"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Save to"
        '
        'lbl_NoOfFiles
        '
        Me.lbl_NoOfFiles.AutoSize = True
        Me.lbl_NoOfFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NoOfFiles.Location = New System.Drawing.Point(156, 134)
        Me.lbl_NoOfFiles.Name = "lbl_NoOfFiles"
        Me.lbl_NoOfFiles.Size = New System.Drawing.Size(14, 13)
        Me.lbl_NoOfFiles.TabIndex = 90
        Me.lbl_NoOfFiles.Text = "0"
        '
        'LstFiles
        '
        Me.LstFiles.FormattingEnabled = True
        Me.LstFiles.HorizontalScrollbar = True
        Me.LstFiles.Location = New System.Drawing.Point(386, 76)
        Me.LstFiles.Name = "LstFiles"
        Me.LstFiles.ScrollAlwaysVisible = True
        Me.LstFiles.Size = New System.Drawing.Size(246, 134)
        Me.LstFiles.TabIndex = 91
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 13)
        Me.Label3.TabIndex = 92
        Me.Label3.Text = "No. of Text Files Found:"
        '
        'savedialog
        '
        Me.savedialog.Location = New System.Drawing.Point(273, 90)
        Me.savedialog.Name = "savedialog"
        Me.savedialog.Size = New System.Drawing.Size(24, 23)
        Me.savedialog.TabIndex = 93
        Me.savedialog.Text = "..."
        Me.savedialog.UseVisualStyleBackColor = True
        '
        'LstFiles2
        '
        Me.LstFiles2.FormattingEnabled = True
        Me.LstFiles2.HorizontalScrollbar = True
        Me.LstFiles2.Location = New System.Drawing.Point(386, 216)
        Me.LstFiles2.Name = "LstFiles2"
        Me.LstFiles2.ScrollAlwaysVisible = True
        Me.LstFiles2.Size = New System.Drawing.Size(246, 134)
        Me.LstFiles2.TabIndex = 94
        '
        'lbl_NoOfFiles2
        '
        Me.lbl_NoOfFiles2.AutoSize = True
        Me.lbl_NoOfFiles2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NoOfFiles2.Location = New System.Drawing.Point(156, 160)
        Me.lbl_NoOfFiles2.Name = "lbl_NoOfFiles2"
        Me.lbl_NoOfFiles2.Size = New System.Drawing.Size(14, 13)
        Me.lbl_NoOfFiles2.TabIndex = 95
        Me.lbl_NoOfFiles2.Text = "0"
        '
        'ExportDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(330, 204)
        Me.Controls.Add(Me.lbl_NoOfFiles2)
        Me.Controls.Add(Me.LstFiles2)
        Me.Controls.Add(Me.savedialog)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_NoOfFiles)
        Me.Controls.Add(Me.LstFiles)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.mrM)
        Me.Controls.Add(Me.myM)
        Me.Controls.Add(Me.myY)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExportDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents myM As System.Windows.Forms.Label
    Friend WithEvents myY As System.Windows.Forms.Label
    Friend WithEvents mrM As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_NoOfFiles As System.Windows.Forms.Label
    Friend WithEvents LstFiles As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents savedialog As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LstFiles2 As System.Windows.Forms.ListBox
    Friend WithEvents lbl_NoOfFiles2 As System.Windows.Forms.Label

End Class

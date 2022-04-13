<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class targetset
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(targetset))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.tb1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.group1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.tb3 = New System.Windows.Forms.ComboBox()
        Me.tb2 = New System.Windows.Forms.ComboBox()
        Me.table1 = New System.Windows.Forms.DataGridView()
        Me.getid = New System.Windows.Forms.Label()
        Me.zero_value = New System.Windows.Forms.Label()
        Me.myyear = New System.Windows.Forms.Label()
        Me.group1.SuspendLayout()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btndelete
        '
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Image = CType(resources.GetObject("btndelete.Image"), System.Drawing.Image)
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(196, 365)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btndelete.Size = New System.Drawing.Size(94, 32)
        Me.btndelete.TabIndex = 24
        Me.btndelete.Text = "DELETE"
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(90, 365)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btnupdate.Size = New System.Drawing.Size(95, 32)
        Me.btnupdate.TabIndex = 23
        Me.btnupdate.Text = "UPDATE"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnadd.Location = New System.Drawing.Point(12, 365)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btnadd.Size = New System.Drawing.Size(68, 32)
        Me.btnadd.TabIndex = 22
        Me.btnadd.Text = "ADD"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'tb1
        '
        Me.tb1.Location = New System.Drawing.Point(58, 23)
        Me.tb1.Name = "tb1"
        Me.tb1.Size = New System.Drawing.Size(161, 20)
        Me.tb1.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Target"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Month"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Year"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'group1
        '
        Me.group1.Controls.Add(Me.CheckBox1)
        Me.group1.Controls.Add(Me.tb3)
        Me.group1.Controls.Add(Me.tb2)
        Me.group1.Controls.Add(Me.table1)
        Me.group1.Controls.Add(Me.tb1)
        Me.group1.Controls.Add(Me.Label4)
        Me.group1.Controls.Add(Me.Label3)
        Me.group1.Controls.Add(Me.btnadd)
        Me.group1.Controls.Add(Me.Label2)
        Me.group1.Controls.Add(Me.btnupdate)
        Me.group1.Controls.Add(Me.btndelete)
        Me.group1.Location = New System.Drawing.Point(12, 12)
        Me.group1.Name = "group1"
        Me.group1.Size = New System.Drawing.Size(303, 410)
        Me.group1.TabIndex = 32
        Me.group1.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(203, 62)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(87, 17)
        Me.CheckBox1.TabIndex = 37
        Me.CheckBox1.Text = "Mock Target"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'tb3
        '
        Me.tb3.FormattingEnabled = True
        Me.tb3.Location = New System.Drawing.Point(58, 92)
        Me.tb3.Name = "tb3"
        Me.tb3.Size = New System.Drawing.Size(121, 21)
        Me.tb3.TabIndex = 36
        Me.tb3.Text = "-Year-"
        '
        'tb2
        '
        Me.tb2.FormattingEnabled = True
        Me.tb2.Items.AddRange(New Object() {"-Month-", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.tb2.Location = New System.Drawing.Point(58, 58)
        Me.tb2.Name = "tb2"
        Me.tb2.Size = New System.Drawing.Size(121, 21)
        Me.tb2.TabIndex = 35
        Me.tb2.Text = "-Month-"
        '
        'table1
        '
        Me.table1.AllowUserToAddRows = False
        Me.table1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.table1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.table1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.table1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.table1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.table1.DefaultCellStyle = DataGridViewCellStyle2
        Me.table1.Location = New System.Drawing.Point(6, 128)
        Me.table1.Name = "table1"
        Me.table1.ReadOnly = True
        Me.table1.Size = New System.Drawing.Size(284, 221)
        Me.table1.TabIndex = 35
        Me.table1.TabStop = False
        '
        'getid
        '
        Me.getid.AutoSize = True
        Me.getid.Location = New System.Drawing.Point(552, 130)
        Me.getid.Name = "getid"
        Me.getid.Size = New System.Drawing.Size(39, 13)
        Me.getid.TabIndex = 33
        Me.getid.Text = "Label5"
        '
        'zero_value
        '
        Me.zero_value.AutoSize = True
        Me.zero_value.Location = New System.Drawing.Point(548, 73)
        Me.zero_value.Name = "zero_value"
        Me.zero_value.Size = New System.Drawing.Size(13, 13)
        Me.zero_value.TabIndex = 34
        Me.zero_value.Text = "0"
        '
        'myyear
        '
        Me.myyear.AutoSize = True
        Me.myyear.Location = New System.Drawing.Point(486, 260)
        Me.myyear.Name = "myyear"
        Me.myyear.Size = New System.Drawing.Size(40, 13)
        Me.myyear.TabIndex = 35
        Me.myyear.Text = "myyear"
        '
        'targetset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 434)
        Me.Controls.Add(Me.myyear)
        Me.Controls.Add(Me.zero_value)
        Me.Controls.Add(Me.getid)
        Me.Controls.Add(Me.group1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "targetset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Target Settings"
        Me.group1.ResumeLayout(False)
        Me.group1.PerformLayout()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btndelete As Button
    Friend WithEvents btnupdate As Button
    Friend WithEvents btnadd As Button
    Friend WithEvents tb1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents group1 As GroupBox
    Friend WithEvents getid As Label
    Friend WithEvents zero_value As Label
    Friend WithEvents table1 As DataGridView
    Friend WithEvents tb3 As ComboBox
    Friend WithEvents tb2 As ComboBox
    Friend WithEvents myyear As Label
    Friend WithEvents CheckBox1 As CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class qsldirectory
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(qsldirectory))
        Me.group1 = New System.Windows.Forms.GroupBox()
        Me.btn_browsepath = New System.Windows.Forms.Button()
        Me.getid = New System.Windows.Forms.Label()
        Me.directorypath = New System.Windows.Forms.TextBox()
        Me.table1 = New System.Windows.Forms.DataGridView()
        Me.directoryname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.group1.SuspendLayout()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'group1
        '
        Me.group1.Controls.Add(Me.btn_browsepath)
        Me.group1.Controls.Add(Me.getid)
        Me.group1.Controls.Add(Me.directorypath)
        Me.group1.Controls.Add(Me.table1)
        Me.group1.Controls.Add(Me.directoryname)
        Me.group1.Controls.Add(Me.Label3)
        Me.group1.Controls.Add(Me.btnadd)
        Me.group1.Controls.Add(Me.Label2)
        Me.group1.Controls.Add(Me.btnupdate)
        Me.group1.Controls.Add(Me.btndelete)
        Me.group1.Location = New System.Drawing.Point(12, 12)
        Me.group1.Name = "group1"
        Me.group1.Size = New System.Drawing.Size(817, 469)
        Me.group1.TabIndex = 33
        Me.group1.TabStop = False
        '
        'btn_browsepath
        '
        Me.btn_browsepath.Location = New System.Drawing.Point(732, 59)
        Me.btn_browsepath.Name = "btn_browsepath"
        Me.btn_browsepath.Size = New System.Drawing.Size(65, 23)
        Me.btn_browsepath.TabIndex = 38
        Me.btn_browsepath.Text = "Browse"
        Me.btn_browsepath.UseVisualStyleBackColor = True
        '
        'getid
        '
        Me.getid.AutoSize = True
        Me.getid.Location = New System.Drawing.Point(729, 16)
        Me.getid.Name = "getid"
        Me.getid.Size = New System.Drawing.Size(39, 13)
        Me.getid.TabIndex = 37
        Me.getid.Text = "Label1"
        '
        'directorypath
        '
        Me.directorypath.Location = New System.Drawing.Point(100, 61)
        Me.directorypath.Name = "directorypath"
        Me.directorypath.Size = New System.Drawing.Size(626, 20)
        Me.directorypath.TabIndex = 36
        '
        'table1
        '
        Me.table1.AllowUserToAddRows = False
        Me.table1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.table1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.table1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.table1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.table1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.table1.DefaultCellStyle = DataGridViewCellStyle5
        Me.table1.Location = New System.Drawing.Point(6, 92)
        Me.table1.Name = "table1"
        Me.table1.ReadOnly = True
        Me.table1.Size = New System.Drawing.Size(805, 323)
        Me.table1.TabIndex = 35
        Me.table1.TabStop = False
        '
        'directoryname
        '
        Me.directoryname.Location = New System.Drawing.Point(100, 23)
        Me.directoryname.Name = "directoryname"
        Me.directoryname.Size = New System.Drawing.Size(423, 20)
        Me.directoryname.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Directory Path"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnadd.Location = New System.Drawing.Point(6, 431)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btnadd.Size = New System.Drawing.Size(68, 32)
        Me.btnadd.TabIndex = 22
        Me.btnadd.Text = "ADD"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Directory Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(84, 431)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btnupdate.Size = New System.Drawing.Size(95, 32)
        Me.btnupdate.TabIndex = 23
        Me.btnupdate.Text = "UPDATE"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'btndelete
        '
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Image = CType(resources.GetObject("btndelete.Image"), System.Drawing.Image)
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(190, 431)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Padding = New System.Windows.Forms.Padding(5, 0, 3, 0)
        Me.btndelete.Size = New System.Drawing.Size(94, 32)
        Me.btndelete.TabIndex = 24
        Me.btndelete.Text = "DELETE"
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'qsldirectory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 493)
        Me.Controls.Add(Me.group1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "qsldirectory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QSL Directory"
        Me.group1.ResumeLayout(False)
        Me.group1.PerformLayout()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents group1 As GroupBox
    Friend WithEvents table1 As DataGridView
    Friend WithEvents directoryname As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnadd As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btnupdate As Button
    Friend WithEvents btndelete As Button
    Friend WithEvents directorypath As TextBox
    Friend WithEvents getid As Label
    Friend WithEvents btn_browsepath As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class

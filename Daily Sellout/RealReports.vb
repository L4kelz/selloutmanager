Imports System.Data.OleDb
Public Class RealReports
    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim Myconnection As OleDbConnection
    Dim dbda As OleDbDataAdapter
    Dim dbds As DataSet
    Dim tables As DataTableCollection
    Dim source As New BindingSource
    Public dbcmd As New OleDb.OleDbCommand
    Public result As Integer
    Public Sql As String

    Private Sub RealReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  
        ComboBox2.Items.Clear()
        Dim i As Integer
        For i = 2017 To Today.Year
            ComboBox2.Items.Add(i.ToString)
        Next
        Me.ComboBox1.SelectedItem = "--Month--"
        Me.ComboBox2.Items.Insert(0, "--Year--")
        Me.ComboBox2.SelectedItem = "--Year--"
        Me.ComboBox3.SelectedItem = "--Week--"
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))

        source.DataSource = view
        DataGridView1.DataSource = view

        'DataGridView1.Columns("ID").Visible = False
        'DataGridView1.Columns(1).DefaultCellStyle.Format = "dd.MMMM.yyyy"
    End Sub
    '===============================
    'COMBOBOX
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Alldate
            ifelsengmonth()
            Alldate()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month and Year Here
            ifelsengmonth()
            Monthandyear()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Month and Week Here
            ifelsengmonth()
            monthandweek()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Year and Week Here
            Label1.Text = ComboBox1.Text
            yearandweek()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month Only
            ifelsengmonth()
            dateko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Year Only
            Label1.Text = ComboBox1.Text
            yearko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Week Only
            Label1.Text = ComboBox1.Text
            weekko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Load the whole thing
            Label1.Text = ComboBox1.Text
            loadko()
        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'All Date
            Label2.Text = ComboBox2.Text
            Alldate()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month and Year
            Label2.Text = ComboBox2.Text
            Monthandyear()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Year and Week
            Label2.Text = ComboBox2.Text
            yearandweek()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Month and Week
            Label2.Text = ComboBox2.Text
            monthandweek()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month
            Label2.Text = ComboBox2.Text
            dateko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Year
            Label2.Text = ComboBox2.Text
            yearko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Week
            Label2.Text = ComboBox2.Text
            weekko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Load the whole thing
            Label2.Text = ComboBox2.Text
            loadko()
        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Alldate
            ifelsengweek()
            Alldate()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month and Year Here
            Label3.Text = ComboBox3.Text
            Monthandyear()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Month and Week Here
            ifelsengweek()
            monthandweek()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Year and Week Here
            ifelsengweek()
            yearandweek()
        ElseIf ComboBox1.Text <> "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Month Only
            Label3.Text = ComboBox3.Text
            dateko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text <> "--Year--" And ComboBox3.Text = "--Week--" Then
            'Year Only
            Label3.Text = ComboBox3.Text
            yearko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text <> "--Week--" Then
            'Week Only
            ifelsengweek()
            weekko()
        ElseIf ComboBox1.Text = "--Month--" And ComboBox2.Text = "--Year--" And ComboBox3.Text = "--Week--" Then
            'Load the whole thing
            Label3.Text = ComboBox3.Text
            loadko()
        End If
    End Sub

    '======================================================================================================================
    '======================================================================================================================
    '======================================================================================================================
    Private Sub dateko()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" & Label1.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub yearko()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where YEAR(DATE) = '" & Label2.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub weekko()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where WEEKDAY(DATE) = '" & Label3.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub Monthandyear()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" _
        & Label1.Text & "' and YEAR(DATE) = '" & Label2.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub monthandweek()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" _
        & Label1.Text & "' and WEEKDAY(DATE) = '" & Label3.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub yearandweek()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where YEAR(DATE) = '" _
        & Label2.Text & "' and WEEKDAY(DATE) = '" & Label3.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub
    Private Sub Alldate()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" & Label1.Text & "' and YEAR(DATE) = '" _
        & Label2.Text & "' and WEEKDAY(DATE) = '" & Label3.Text & "' order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub


    Private Sub loadko()
        Myconnection = New OleDbConnection
        Myconnection.ConnectionString = connString
        dbds = New DataSet
        tables = dbds.Tables
        dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", Myconnection)
        dbda.Fill(dbds, "tblitems")
        Dim view As New DataView(tables(0))
        source.DataSource = view
        DataGridView1.DataSource = view
    End Sub

    Private Sub ifelsengmonth()

        If ComboBox1.Text = "January" Then
            Label1.Text = "1"
        ElseIf ComboBox1.Text = "February" Then
            Label1.Text = "2"
        ElseIf ComboBox1.Text = "March" Then
            Label1.Text = "3"
        ElseIf ComboBox1.Text = "April" Then
            Label1.Text = "4"
        ElseIf ComboBox1.Text = "May" Then
            Label1.Text = "5"
        ElseIf ComboBox1.Text = "June" Then
            Label1.Text = "6"
        ElseIf ComboBox1.Text = "July" Then
            Label1.Text = "7"
        ElseIf ComboBox1.Text = "August" Then
            Label1.Text = "8"
        ElseIf ComboBox1.Text = "September" Then
            Label1.Text = "9"
        ElseIf ComboBox1.Text = "October" Then
            Label1.Text = "10"
        ElseIf ComboBox1.Text = "November" Then
            Label1.Text = "11"
        ElseIf ComboBox1.Text = "December" Then
            Label1.Text = "12"
        End If
    End Sub
    Private Sub ifelsengweek()
        If ComboBox3.Text = "Sunday" Then
            Label3.Text = "1"
        ElseIf ComboBox3.Text = "Monday" Then
            Label3.Text = "2"
        ElseIf ComboBox3.Text = "Tuesday" Then
            Label3.Text = "3"
        ElseIf ComboBox3.Text = "Wednesday" Then
            Label3.Text = "4"
        ElseIf ComboBox3.Text = "Thursday" Then
            Label3.Text = "5"
        ElseIf ComboBox3.Text = "Friday" Then
            Label3.Text = "6"
        ElseIf ComboBox3.Text = "Saturday" Then
            Label3.Text = "7"
        End If
    End Sub

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub
End Class
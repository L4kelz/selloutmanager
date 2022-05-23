Imports System.Data.OleDb
Public Class EditForm


    Dim edit_connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim edit_Myconnection As OleDbConnection
    Dim edit_dbda As OleDbDataAdapter
    Dim edit_dbds As DataSet
    Dim edit_tables As DataTableCollection
    Dim edit_source As New BindingSource
    Public edit_dbcmd As New OleDb.OleDbCommand
    Public edit_result As Integer
    Public edit_Sql As String

    Dim con As New OleDbConnection
    Private Sub EditForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dbbtnviewbymonth.Enabled = False
        dbbtnexportbymonth.Enabled = False
        dbmyM.Text = Format(dbDateTimePicker1.Value, "MMMM")
        dbMMMM.Text = Format(dbDateTimePicker1.Value, "MMMM")
        dbmyY.Text = Format(dbDateTimePicker1.Value, "yyyy")

        dbm.Text = Format(dbDateTimePicker1.Value, " M")
        dbmm.Text = Format(dbDateTimePicker1.Value, "MM")
        dbd.Text = Format(dbDateTimePicker1.Value, " d")
        dbdd.Text = Format(dbDateTimePicker1.Value, "dd")
        dby.Text = Format(dbDateTimePicker1.Value, "yyyy")
        dbComboBox2.Items.Clear()
        Dim i As Integer
        For i = 2017 To Today.Year
            dbComboBox2.Items.Add(i.ToString)
        Next
        Me.dbComboBox1.SelectedItem = dbmyM.Text
        Me.dbComboBox2.Items.Insert(0, "--Year--")
        Me.dbComboBox2.SelectedItem = dbmyY.Text
        Me.dbComboBox3.SelectedItem = "--Week--"

        Monthandyear()
        ' edit_Myconnection = New OleDbConnection
        ' edit_Myconnection.ConnectionString = edit_connString
        ' edit_dbds = New DataSet
        ' edit_tables = edit_dbds.Tables
        ' edit_dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", edit_Myconnection)
        ' edit_dbda.Fill(edit_dbds, "tblitems")
        ' Dim view As New DataView(edit_tables(0))

        'edit_source.DataSource = view
        'DataGridView2.DataSource = view


        DataGridView2.Columns("ID").Visible = False
        DataGridView2.Columns("PAR").Visible = False
        'DataGridView2.Columns(1).DefaultCellStyle.Format = "dd.MMMM.yyyy"
    End Sub

    Private Sub dbComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbComboBox1.SelectedIndexChanged
        If dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Alldate
            ifelsengmonth()
            Alldate()
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month and Year Here
            ifelsengmonth()
            Monthandyear()
            dbbtnviewbymonth.Enabled = True
            dbbtnexportbymonth.Enabled = True
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Month and Week Here
            ifelsengmonth()
            monthandweek()

        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Year and Week Here
            dbLabel1.Text = dbComboBox1.Text
            yearandweek()
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month Only
            ifelsengmonth()
            dateko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Year Only
            dbLabel1.Text = dbComboBox1.Text
            yearko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Week Only
            dbLabel1.Text = dbComboBox1.Text
            weekko()
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Load the whole thing
            dbLabel1.Text = dbComboBox1.Text
            loadko()
        End If

    End Sub

    Private Sub dbComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbComboBox2.SelectedIndexChanged
        If dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'All Date
            dbLabel2.Text = dbComboBox2.Text
            Alldate()
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month and Year
            dbLabel2.Text = dbComboBox2.Text
            Monthandyear()
            dbbtnviewbymonth.Enabled = True
            dbbtnexportbymonth.Enabled = True
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Year and Week
            dbLabel2.Text = dbComboBox2.Text
            yearandweek()
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Month and Week
            dbLabel2.Text = dbComboBox2.Text
            monthandweek()

        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month
            dbLabel2.Text = dbComboBox2.Text
            dateko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Year
            dbLabel2.Text = dbComboBox2.Text
            yearko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Week
            dbLabel2.Text = dbComboBox2.Text
            weekko()
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Load the whole thing
            dbLabel2.Text = dbComboBox2.Text
            loadko()
        End If

    End Sub

    Private Sub dbComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbComboBox3.SelectedIndexChanged
        If dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Alldate
            ifelsengweek()
            Alldate()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month and Year Here
            dbLabel3.Text = dbComboBox3.Text
            Monthandyear()
            dbbtnviewbymonth.Enabled = True
            dbbtnexportbymonth.Enabled = True
        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Month and Week Here
            ifelsengweek()
            monthandweek()

        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Year and Week Here
            ifelsengweek()
            yearandweek()

        ElseIf dbComboBox1.Text <> "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Month Only
            dbLabel3.Text = dbComboBox3.Text
            dateko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text <> "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Year Only
            dbLabel3.Text = dbComboBox3.Text
            yearko()
            dbbtnviewbymonth.Enabled = False
            dbbtnexportbymonth.Enabled = False
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text <> "--Week--" Then
            'Week Only
            ifelsengweek()
            weekko()
        ElseIf dbComboBox1.Text = "--Month--" And dbComboBox2.Text = "--Year--" And dbComboBox3.Text = "--Week--" Then
            'Load the whole thing
            dbLabel3.Text = dbComboBox3.Text
            loadko()
        End If
    End Sub

    Private Sub dbDateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbDateTimePicker1.ValueChanged
        dbMMMM.Text = Format(dbDateTimePicker1.Value, "MMMM")
        dbm.Text = Format(dbDateTimePicker1.Value, " M")
        dbmm.Text = Format(dbDateTimePicker1.Value, "MM")
        dbd.Text = Format(dbDateTimePicker1.Value, " d")
        dbdd.Text = Format(dbDateTimePicker1.Value, "dd")
        dby.Text = Format(dbDateTimePicker1.Value, "yyyy")
        dbLabel10.Text = dbDateTimePicker1.Value.Date.ToString("MM-dd-yyyy")
        loadthedate()
    End Sub
    Private Sub dbtb1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb1.TextChanged
        tb1ko()
    End Sub
    Private Sub dbtb2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb2.TextChanged
        tb2ko()
    End Sub
    Private Sub dbtb3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb3.TextChanged
        tb3ko()
    End Sub
    Private Sub dbtb4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb4.TextChanged
        tb4ko()
    End Sub
    Private Sub dbtb5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb5.TextChanged
        tb5ko()
    End Sub
    Private Sub dbtb6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb6.TextChanged
        tb6ko()
    End Sub
    Private Sub dbtb7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb7.TextChanged
        tb7ko()
    End Sub
    Private Sub dbtb8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb8.TextChanged
        tb8ko()
    End Sub
    Private Sub dbtb9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtb9.TextChanged
        tb9ko()
    End Sub

    Private Sub dbbtnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbbtnupdate.Click

        Dim ask As MsgBoxResult = MsgBox("Are you sure to update the record?", MsgBoxStyle.YesNo)
        If ask = MsgBoxResult.Yes Then
            Try
                edit_Sql = "UPDATE tblitems SET MTDSMART = '" & dbtb1.Text & "',MTDSUN = '" & dbtb2.Text & "', " &
            " MTDRECRUITMENTSMART = '" & dbtb3.Text & "', MTDRECRUITMENTSUN = '" & dbtb4.Text & "', SMARTLOAD = '" & dbtb5.Text & "', SUNLOAD = '" & dbtb6.Text & "', RECRUITMENTSMART = '" & dbtb7.Text & "', RECRUITMENTSUN = '" & dbtb8.Text & "', PAR = '" & dbtb9.Text & "' WHERE ID = " & dbLabel9.Text
                edit_Myconnection.Open()
                With edit_dbcmd
                    .CommandText = edit_Sql
                    .Connection = edit_Myconnection
                End With

                edit_result = edit_dbcmd.ExecuteNonQuery
                If edit_result > 0 Then
                    MsgBox("New Record has been Updated!", vbInformation)
                    edit_Myconnection.Close()
                    ' Call btnupdate_Click(sender, e)

                    DataGridView2.DataSource = Nothing
                    Monthandyear()
                    DataGridView2.Columns("ID").Visible = False
                    DataGridView2.Columns("PAR").Visible = False
                    Form1.DataGridView1.DataSource = Nothing
                    'Form1.iloadmoto2()
                    Form1.iloadmoto()
                    cleartextfields()
                Else
                    MsgBox("No Record has been Updated!")
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                'MsgBox("No record Found! Please add record before updating", vbExclamation)
            Finally
                edit_Myconnection.Close()
            End Try
        End If


    End Sub

    Private Sub dbbtnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbbtnadd.Click
        Try
            edit_Sql = "INSERT INTO  tblitems([DATE], MTDSMART, MTDSUN, MTDRECRUITMENTSMART, MTDRECRUITMENTSUN, SMARTLOAD, SUNLOAD, RECRUITMENTSMART, RECRUITMENTSUN, PAR) VALUES ('" & dbLabel10.Text & "','" & dbtb1.Text & "', '" & dbtb2.Text & "','" & dbtb3.Text & "','" & dbtb4.Text & "','" & dbtb5.Text & "', '" & dbtb6.Text & "', '" & dbtb7.Text & "', '" & dbtb8.Text & "', '" & dbtb9.Text & "')"
            edit_Myconnection.Open()
            With edit_dbcmd
                .CommandText = edit_Sql
                .Connection = edit_Myconnection
            End With
            edit_result = edit_dbcmd.ExecuteNonQuery
            If edit_result > 0 Then
                MsgBox("New item record has been added!", vbInformation)
                edit_Myconnection.Close()
                'Call Button1_Click(sender, e)

                DataGridView2.DataSource = Nothing
                Monthandyear()
                DataGridView2.Columns("ID").Visible = False
                DataGridView2.Columns("PAR").Visible = False
                Form1.DataGridView1.DataSource = Nothing
                Form1.iloadmoto()
                'Form1.iloadmoto2()
                cleartextfields()
            Else
                MsgBox("No item record has been saved!!")
            End If

        Catch ex As Exception
            ' MsgBox(ex.Message, vbExclamation)
            MsgBox("Record already exist!", vbExclamation)
        Finally
            edit_Myconnection.Close()


        End Try
    End Sub

    Private Sub dbbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbbtndelete.Click
        Dim ask As MsgBoxResult = MsgBox("Are you sure to delete record?", MsgBoxStyle.YesNo + vbInformation)
        If ask = MsgBoxResult.Yes Then
            Try
                edit_Sql = "DELETE From tblitems where ID = " & dbLabel9.Text
                edit_Myconnection.Open()
                With edit_dbcmd
                    .CommandText = edit_Sql
                    .Connection = edit_Myconnection
                End With

                edit_result = edit_dbcmd.ExecuteNonQuery
                If edit_result > 0 Then
                    MsgBox("Record has been deleted!")
                    edit_Myconnection.Close()
                    ' Call btnupdate_Click(sender, e)

                    DataGridView2.DataSource = Nothing
                    Monthandyear()
                    DataGridView2.Columns("ID").Visible = False
                    DataGridView2.Columns("PAR").Visible = False
                    Form1.DataGridView1.DataSource = Nothing
                    Form1.iloadmoto()
                    'Form1.iloadmoto2()
                    cleartextfields()
                    dbLabel9.Text = ""
                    Form1.uploaddatatoCloud()
                Else
                    MsgBox("No Record has been Deleted!")
                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
                MsgBox("No record Found!", vbExclamation)
            Finally
                edit_Myconnection.Close()
            End Try
        End If
    End Sub
    '======================================================================================================================
    '======================================================================================================================
    '======================================================================================================================
    Private Sub dateko()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" & dbLabel1.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub yearko()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where YEAR(DATE) = '" & dbLabel2.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub weekko()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where WEEKDAY(DATE) = '" & dbLabel3.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub Monthandyear()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" _
        & dbLabel1.Text & "' and YEAR(DATE) = '" & dbLabel2.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub monthandweek()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" _
        & dbLabel1.Text & "' and WEEKDAY(DATE) = '" & dbLabel3.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub yearandweek()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where YEAR(DATE) = '" _
        & dbLabel2.Text & "' and WEEKDAY(DATE) = '" & dbLabel3.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub
    Private Sub Alldate()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" & dbLabel1.Text & "' and YEAR(DATE) = '" _
        & dbLabel2.Text & "' and WEEKDAY(DATE) = '" & dbLabel3.Text & "' order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
    End Sub


    Private Sub loadko()
        edit_Myconnection = New OleDbConnection
        edit_Myconnection.ConnectionString = edit_connString
        edit_dbds = New DataSet
        edit_tables = edit_dbds.Tables
        edit_dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", edit_Myconnection)
        edit_dbda.Fill(edit_dbds, "tblitems")
        Dim view As New DataView(edit_tables(0))
        edit_source.DataSource = view
        DataGridView2.DataSource = view
        DataGridView2.Columns("ID").Visible = False
        DataGridView2.Columns("PAR").Visible = False
    End Sub

    Private Sub ifelsengmonth()

        If dbComboBox1.Text = "January" Then
            dbLabel1.Text = "1"
        ElseIf dbComboBox1.Text = "February" Then
            dbLabel1.Text = "2"
        ElseIf dbComboBox1.Text = "March" Then
            dbLabel1.Text = "3"
        ElseIf dbComboBox1.Text = "April" Then
            dbLabel1.Text = "4"
        ElseIf dbComboBox1.Text = "May" Then
            dbLabel1.Text = "5"
        ElseIf dbComboBox1.Text = "June" Then
            dbLabel1.Text = "6"
        ElseIf dbComboBox1.Text = "July" Then
            dbLabel1.Text = "7"
        ElseIf dbComboBox1.Text = "August" Then
            dbLabel1.Text = "8"
        ElseIf dbComboBox1.Text = "September" Then
            dbLabel1.Text = "9"
        ElseIf dbComboBox1.Text = "October" Then
            dbLabel1.Text = "10"
        ElseIf dbComboBox1.Text = "November" Then
            dbLabel1.Text = "11"
        ElseIf dbComboBox1.Text = "December" Then
            dbLabel1.Text = "12"
        End If
    End Sub
    Private Sub ifelsengweek()
        If dbComboBox3.Text = "Sunday" Then
            dbLabel3.Text = "1"
        ElseIf dbComboBox3.Text = "Monday" Then
            dbLabel3.Text = "2"
        ElseIf dbComboBox3.Text = "Tuesday" Then
            dbLabel3.Text = "3"
        ElseIf dbComboBox3.Text = "Wednesday" Then
            dbLabel3.Text = "4"
        ElseIf dbComboBox3.Text = "Thursday" Then
            dbLabel3.Text = "5"
        ElseIf dbComboBox3.Text = "Friday" Then
            dbLabel3.Text = "6"
        ElseIf dbComboBox3.Text = "Saturday" Then
            dbLabel3.Text = "7"
        End If
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick


        dbDateTimePicker1.Text = DataGridView2.CurrentRow.Cells(1).Value.ToString
        dbLabel9.Text = DataGridView2.CurrentRow.Cells(0).Value.ToString
        dbtb1.Text = DataGridView2.CurrentRow.Cells(2).Value.ToString
        dbtb2.Text = DataGridView2.CurrentRow.Cells(3).Value.ToString
        dbtb3.Text = DataGridView2.CurrentRow.Cells(4).Value.ToString
        dbtb4.Text = DataGridView2.CurrentRow.Cells(5).Value.ToString
        dbtb5.Text = DataGridView2.CurrentRow.Cells(6).Value.ToString
        dbtb6.Text = DataGridView2.CurrentRow.Cells(7).Value.ToString
        dbtb7.Text = DataGridView2.CurrentRow.Cells(8).Value.ToString
        dbtb8.Text = DataGridView2.CurrentRow.Cells(9).Value.ToString
        dbtb9.Text = DataGridView2.CurrentRow.Cells(10).Value.ToString
    End Sub

    Private Sub loadthedate()
        Try
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
            con.Open()
            Dim cmd As New OleDb.OleDbCommand("select count(*) from tblitems where MONTH(DATE) = " & dbm.Text & " AND YEAR(DATE) = " & dby.Text & " AND DAY(DATE) =  " & dbd.Text & " ", con)
            Dim count As Int32 = CInt(cmd.ExecuteScalar)
            If count > 0 Then
                Dim ds As New DataSet
                Dim dt As New DataTable
                ds.Tables.Add(dt)
                Dim da As New OleDbDataAdapter
                da = New OleDbDataAdapter("select * from tblitems where MONTH(DATE) = " & dbm.Text & " AND YEAR(DATE) = " & dby.Text & " AND DAY(DATE) =  " & dbd.Text & " ", con)
                da.Fill(dt)
                dbLabel9.Text = dt.Rows(0).Item(0)
                dbtb1.Text = dt.Rows(0).Item(2)
                dbtb2.Text = dt.Rows(0).Item(3)
                dbtb3.Text = dt.Rows(0).Item(4)
                dbtb4.Text = dt.Rows(0).Item(5)
                dbtb5.Text = dt.Rows(0).Item(6)
                dbtb6.Text = dt.Rows(0).Item(7)
                dbtb7.Text = dt.Rows(0).Item(8)
                dbtb8.Text = dt.Rows(0).Item(9)
                dbtb9.Text = dt.Rows(0).Item(10)


            Else
                dbLabel9.Text = ""
                dbtb1.Clear()
                dbtb2.Clear()
                dbtb3.Clear()
                dbtb4.Clear()
                dbtb5.Clear()
                dbtb6.Clear()
                dbtb7.Clear()
                dbtb8.Clear()
                dbtb9.Clear()
            End If

            con.Close()
        Catch ex As Exception
            '  MsgBox(ex.Message)
            con.Close()
        End Try



    End Sub
    Private Sub tb1ko()
        If Me.dbtb1.Text.Contains(",") Or Me.dbtb1.Text.Contains(".") Then
            dbtb1.Text = dbtb1.Text.Replace(",", "").Trim()
            dbtb1.Text = dbtb1.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb1.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl1.Text = eto1
        End If

        If dbtb1.Text = "" Then
            dblbl1.Text = "--"
        End If
    End Sub
    Private Sub tb2ko()
        If Me.dbtb2.Text.Contains(",") Or Me.dbtb2.Text.Contains(".") Then
            dbtb2.Text = dbtb2.Text.Replace(",", "").Trim()
            dbtb2.Text = dbtb2.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb2.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl2.Text = eto1
        End If

        If dbtb2.Text = "" Then
            dblbl2.Text = "--"
        End If
    End Sub
    Private Sub tb3ko()
        If Me.dbtb3.Text.Contains(",") Or Me.dbtb3.Text.Contains(".") Then
            dbtb3.Text = dbtb3.Text.Replace(",", "").Trim()
            dbtb3.Text = dbtb3.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb3.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl3.Text = eto1
        End If

        If dbtb3.Text = "" Then
            dblbl3.Text = "--"
        End If
    End Sub
    Private Sub tb4ko()
        If Me.dbtb4.Text.Contains(",") Or Me.dbtb4.Text.Contains(".") Then
            dbtb4.Text = dbtb4.Text.Replace(",", "").Trim()
            dbtb4.Text = dbtb4.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb4.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl4.Text = eto1
        End If

        If dbtb4.Text = "" Then
            dblbl4.Text = "--"
        End If
    End Sub
    Private Sub tb5ko()
        If Me.dbtb5.Text.Contains(",") Or Me.dbtb5.Text.Contains(".") Then
            dbtb5.Text = dbtb5.Text.Replace(",", "").Trim()
            dbtb5.Text = dbtb5.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb5.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl5.Text = eto1
        End If

        If dbtb5.Text = "" Then
            dblbl5.Text = "--"
        End If
    End Sub
    Private Sub tb6ko()
        If Me.dbtb6.Text.Contains(",") Or Me.dbtb6.Text.Contains(".") Then
            dbtb6.Text = dbtb6.Text.Replace(",", "").Trim()
            dbtb6.Text = dbtb6.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb6.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl6.Text = eto1
        End If

        If dbtb6.Text = "" Then
            dblbl6.Text = "--"
        End If
    End Sub
    Private Sub tb7ko()
        If Me.dbtb7.Text.Contains(",") Or Me.dbtb7.Text.Contains(".") Then
            dbtb7.Text = dbtb7.Text.Replace(",", "").Trim()
            dbtb7.Text = dbtb7.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb7.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl7.Text = eto1
        End If

        If dbtb7.Text = "" Then
            dblbl7.Text = "--"
        End If
    End Sub
    Private Sub tb8ko()
        If Me.dbtb8.Text.Contains(",") Or Me.dbtb8.Text.Contains(".") Then
            dbtb8.Text = dbtb8.Text.Replace(",", "").Trim()
            dbtb8.Text = dbtb8.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb8.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl8.Text = eto1
        End If

        If dbtb8.Text = "" Then
            dblbl8.Text = "--"
        End If
    End Sub
    Private Sub tb9ko()
        If Me.dbtb9.Text.Contains(",") Or Me.dbtb9.Text.Contains(".") Then
            dbtb9.Text = dbtb9.Text.Replace(",", "").Trim()

            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtb9.Text)
            Dim eto1 As String = a1.ToString("N0")
            dblbl9.Text = eto1
        End If

        If dbtb9.Text = "" Then
            dblbl9.Text = "--"
        End If
    End Sub
    Public Sub cleartextfields()
        For Each crt As Control In GroupBox1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next
        '
        '       For Each crt2 As Control In GroupBox2.Controls
        'If crt2.GetType Is GetType(Label) Then
        'crt2.Text = Nothing
        'End If
        'Next
    End Sub







    Private Sub DataGridView2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        getsum()
        getcount()
        getaverage()
    End Sub

    Private Sub getsum()
        Try
            Dim AverageValue As Decimal = 0
            ' Dim cellcountto As Decimal = 0
            dbSUM.Text = "--"
            For Each cell As DataGridViewCell In DataGridView2.SelectedCells
                AverageValue += (cell.Value)
                ' cellcountto += 1
            Next
            dbSUM.Text = FormatNumber(CDbl(AverageValue), 2)
            'count.Text = FormatNumber(CDbl(cellcountto), 0)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub getcount()
        Try
            Dim AverageValue As Decimal = 0
            Dim cellcountto As Decimal = 0
            dbcount.Text = "--"
            For Each cell As DataGridViewCell In DataGridView2.SelectedCells
                cellcountto += 1
            Next

            dbcount.Text = FormatNumber(CDbl(cellcountto), 0)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub getaverage()
        Try
            Dim average As Decimal = 0

            dblblaverage.Text = "--"
            For Each cell As DataGridViewCell In DataGridView2.SelectedCells
                average += (cell.Value) / DataGridView2.SelectedCells.Count()
            Next

            dblblaverage.Text = FormatNumber(CDbl(average), 2)
        Catch ex As Exception

        End Try
    End Sub












    Private Sub dbbtnexportbymonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbbtnexportbymonth.Click

    End Sub

    Private Sub dbbtnviewbymonth_Click(sender As Object, e As EventArgs) Handles dbbtnviewbymonth.Click
        Try
            Process.Start("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + dby.Text + "\" + dbMMMM.Text + "\sellout" + dbmm.Text + dbdd.Text + dby.Text + ".txt")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


End Class
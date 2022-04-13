Imports System.Data.OleDb
Public Class targetset

    Dim edit_connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim edit_Myconnection As OleDbConnection
    Dim edit_dbda As OleDbDataAdapter
    Dim edit_dbds As DataSet
    Dim edit_tables As DataTableCollection
    Dim edit_source As New BindingSource
    Public edit_dbcmd As New OleDb.OleDbCommand
    Public edit_result As Integer
    Public edit_Sql As String
    Private Sub targetset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tb1.Text = ""
        myyear.Text = Date.Now.ToString("yyyy")
        CheckBox1.CheckState = CheckState.Unchecked
        tb3.Items.Clear()
        Dim i As Integer
        For i = 2019 To Today.Year
            tb3.Items.Add(i.ToString)
        Next
        Me.tb3.Items.Insert(0, "-Year-")
        Me.tb3.SelectedItem = "-Year-"
        Me.tb2.SelectedItem = "-Month-"
        'table1.Columns(1).DefaultCellStyle.Format = "N2"
        targetdetails()
        tb1.Select()
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If CheckBox1.CheckState = CheckState.Checked Then
            Try
                edit_Sql = "INSERT INTO  MockTarget(SMART, SUN, M_month, M_year) VALUES ('" & tb1.Text & "','" & zero_value.Text & "', '" & tb2.Text & "', '" & tb3.Text & "')"
                edit_Myconnection.Open()
                With edit_dbcmd
                    .CommandText = edit_Sql
                    .Connection = edit_Myconnection
                End With
                edit_result = edit_dbcmd.ExecuteNonQuery
                If edit_result > 0 Then
                    MsgBox("New item record has been added!", vbInformation)
                    edit_Myconnection.Close()


                    table1.DataSource = Nothing

                    mock_targetdetails()


                    Form1.DataGridView1.DataSource = Nothing
                    Form1.iloadmoto()

                    cleartextfields()
                    Form1.uploaddatatoCloud()
                Else
                    MsgBox("No item record has been saved!!")
                End If

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                edit_Myconnection.Close()
            End Try
        Else
            Try
                edit_Sql = "INSERT INTO  target(SMART, SUN, Tmonth, Tyear) VALUES ('" & tb1.Text & "','" & zero_value.Text & "', '" & tb2.Text & "', '" & tb3.Text & "')"
                edit_Myconnection.Open()
                With edit_dbcmd
                    .CommandText = edit_Sql
                    .Connection = edit_Myconnection
                End With
                edit_result = edit_dbcmd.ExecuteNonQuery
                If edit_result > 0 Then
                    MsgBox("New item record has been added!", vbInformation)
                    edit_Myconnection.Close()


                    table1.DataSource = Nothing

                    targetdetails()


                    Form1.DataGridView1.DataSource = Nothing
                    Form1.iloadmoto()
                    cleartextfields()
                    Form1.uploaddatatoCloud()
                Else
                    MsgBox("No item record has been saved!!")
                End If

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                edit_Myconnection.Close()
            End Try
        End If



    End Sub


    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Dim ask As MsgBoxResult = MsgBox("Are you sure to update the record?", MsgBoxStyle.YesNo + vbInformation)

        If CheckBox1.CheckState = CheckState.Checked Then
            If ask = MsgBoxResult.Yes Then
                Try
                    edit_Sql = "UPDATE MockTarget SET SMART = '" & tb1.Text & "',SUN = '" & zero_value.Text & "', " &
                " M_Month = '" & tb2.Text & "', M_year = '" & tb3.Text & "' WHERE ID = " & getid.Text
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

                        table1.DataSource = Nothing
                        mock_targetdetails()
                        table1.Columns("ID").Visible = False
                        table1.Columns("SUN").Visible = False
                        Form1.DataGridView1.DataSource = Nothing
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
        Else

            If ask = MsgBoxResult.Yes Then
                Try
                    edit_Sql = "UPDATE target SET SMART = '" & tb1.Text & "',SUN = '" & zero_value.Text & "', " &
                " TMonth = '" & tb2.Text & "', Tyear = '" & tb3.Text & "' WHERE ID = " & getid.Text
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

                        table1.DataSource = Nothing
                        targetdetails()
                        table1.Columns("ID").Visible = False
                        table1.Columns("SUN").Visible = False
                        Form1.DataGridView1.DataSource = Nothing
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
        End If

    End Sub


    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Dim ask As MsgBoxResult = MsgBox("Are you sure to delete record?", MsgBoxStyle.YesNo + vbInformation)

        If CheckBox1.CheckState = CheckState.Checked Then
            If ask = MsgBoxResult.Yes Then
                Try
                    edit_Sql = "DELETE From MockTarget where ID = " & getid.Text
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

                        table1.DataSource = Nothing
                        mock_targetdetails()
                        table1.Columns("ID").Visible = False

                        Form1.DataGridView1.DataSource = Nothing
                        Form1.iloadmoto()
                        cleartextfields()
                        getid.Text = ""
                        Form1.uploaddatatoCloud()
                        Form1.smarttar.Clear()
                        Form1.suntar.Clear()
                        Form1.dividetarget.Text = "--"
                        Form1.resofper.Text = "--"
                        Form1.percentageko.Text = "--"
                        Form1.def.Text = ""

                        Form1.btn1.BackColor = Color.WhiteSmoke
                        Form1.btn1.ForeColor = Color.Black
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
        Else
            If ask = MsgBoxResult.Yes Then
                Try
                    edit_Sql = "DELETE From target where ID = " & getid.Text
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

                        table1.DataSource = Nothing
                        targetdetails()
                        table1.Columns("ID").Visible = False

                        Form1.DataGridView1.DataSource = Nothing
                        Form1.iloadmoto()
                        cleartextfields()
                        getid.Text = ""
                        Form1.uploaddatatoCloud()
                        'Form1.smarttar.Text = "0"
                        'Form1.suntar.Text = "0"
                        Form1.smarttar.Clear()
                        Form1.suntar.Clear()
                        Form1.dividetarget.Text = "--"
                        Form1.resofper.Text = "--"
                        Form1.percentageko.Text = "--"
                        Form1.def.Text = ""

                        Form1.btn1.BackColor = Color.WhiteSmoke
                        Form1.btn1.ForeColor = Color.Black
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
        End If

    End Sub


    Private Sub targetdetails()

        Try

            table1.RowHeadersVisible = False
            edit_Myconnection = New OleDbConnection
            edit_Myconnection.ConnectionString = edit_connString
            edit_dbds = New DataSet
            edit_tables = edit_dbds.Tables
            edit_dbda = New OleDbDataAdapter("Select * from target Order by ID Desc", edit_Myconnection)

            edit_dbda.Fill(edit_dbds, "tblitems")
            Dim view As New DataView(edit_tables(0))

            edit_source.DataSource = view
            table1.DataSource = view

            table1.Columns("ID").Visible = False
            table1.Columns("SUN").Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub mock_targetdetails()

        Try

            table1.RowHeadersVisible = False
            edit_Myconnection = New OleDbConnection
            edit_Myconnection.ConnectionString = edit_connString
            edit_dbds = New DataSet
            edit_tables = edit_dbds.Tables
            edit_dbda = New OleDbDataAdapter("Select * from MockTarget order by ID Desc", edit_Myconnection)

            edit_dbda.Fill(edit_dbds, "tblitems")
            Dim view As New DataView(edit_tables(0))

            edit_source.DataSource = view
            table1.DataSource = view

            table1.Columns("ID").Visible = False
            table1.Columns("SUN").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub table1_CellClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles table1.CellClick
        getid.Text = table1.CurrentRow.Cells(0).Value.ToString
        tb1.Text = table1.CurrentRow.Cells(1).Value.ToString
        tb2.Text = table1.CurrentRow.Cells(3).Value.ToString
        tb3.Text = table1.CurrentRow.Cells(4).Value.ToString
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            mock_targetdetails()
        Else
            targetdetails()
        End If
    End Sub
    Public Sub cleartextfields()
        For Each crt As Control In group1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next


        tb2.Text = "-Month-"
        tb3.Text = "-Year-"
        getid.Text = ""
        '       For Each crt2 As Control In GroupBox2.Controls
        'If crt2.GetType Is GetType(Label) Then
        'crt2.Text = Nothing
        'End If
        'Next
    End Sub
End Class
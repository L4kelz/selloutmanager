Imports System.Data.OleDb

Public Class qsldirectory
    Dim edit_connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim edit_Myconnection As OleDbConnection
    Dim edit_dbda As OleDbDataAdapter
    Dim edit_dbds As DataSet
    Dim edit_tables As DataTableCollection
    Dim edit_source As New BindingSource
    Public edit_dbcmd As New OleDb.OleDbCommand
    Public edit_result As Integer
    Public edit_Sql As String


    Private Sub qsldirectory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        directory()



    End Sub



    Private Sub directory()
        Try

            table1.RowHeadersVisible = False
            edit_Myconnection = New OleDbConnection
            edit_Myconnection.ConnectionString = edit_connString
            edit_dbds = New DataSet
            edit_tables = edit_dbds.Tables
            edit_dbda = New OleDbDataAdapter("Select * from qsldirectory order by ID Asc", edit_Myconnection)

            edit_dbda.Fill(edit_dbds, "qsldirectory")
            Dim view As New DataView(edit_tables(0))

            edit_source.DataSource = view
            table1.DataSource = view

            table1.Columns("ID").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            edit_Sql = "INSERT INTO  qsldirectory(dir_name, dir_path) VALUES ('" & directoryname.Text & "','" & directorypath.Text & "')"
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

                directory()


                Form1.comboboxdirectory()
                Form1.uploaddatatoCloud()


                cleartextfields()



            Else
                MsgBox("No item record has been saved!!")
            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)
        Finally
            edit_Myconnection.Close()
        End Try
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            edit_Sql = "UPDATE qsldirectory SET dir_name = '" & directoryname.Text & "',dir_path = '" & directorypath.Text & "' WHERE ID = " & getid.Text
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
                directory()

                Form1.comboboxdirectory()
                Form1.uploaddatatoCloud()


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
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Dim ask As MsgBoxResult = MsgBox("Are you sure to delete record?", MsgBoxStyle.YesNo + vbInformation)
        If ask = MsgBoxResult.Yes Then
            Try
                edit_Sql = "DELETE From qsldirectory where ID = " & getid.Text
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
                    directory()
                    table1.Columns("ID").Visible = False

                    Form1.comboboxdirectory()
                    Form1.uploaddatatoCloud()


                    cleartextfields()
                    getid.Text = ""

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



    Public Sub cleartextfields()
        For Each crt As Control In group1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next


        directoryname.Text = ""
        directorypath.Text = ""
        getid.Text = ""
        '       For Each crt2 As Control In GroupBox2.Controls
        'If crt2.GetType Is GetType(Label) Then
        'crt2.Text = Nothing
        'End If
        'Next
    End Sub

    Private Sub table1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles table1.CellClick
        getid.Text = table1.CurrentRow.Cells(0).Value.ToString
        directoryname.Text = table1.CurrentRow.Cells(1).Value.ToString
        directorypath.Text = table1.CurrentRow.Cells(2).Value.ToString

    End Sub

    Private Sub btn_browsepath_Click(sender As Object, e As EventArgs) Handles btn_browsepath.Click


        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then

            directorypath.Text = FolderBrowserDialog1.SelectedPath


        End If
    End Sub
End Class
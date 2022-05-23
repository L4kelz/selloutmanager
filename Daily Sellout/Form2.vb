Imports System.Data.OleDb
Public Class Form2

    Dim conString As String = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source='C:\Users\Bryner\Downloads\OpisSummaryLoadLedger(3).xlsx';Extended Properties=Excel 12.0;"

    Dim con As OleDbConnection
    Dim adapter As OleDbDataAdapter



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        loaddata()
    End Sub


    Private Sub loaddata()
        con = New OleDbConnection(conString)

        Dim query As String = "SELECT * FROM [OpisSummaryLoadLedger$]"

        adapter = New OleDbDataAdapter(query, con)

        Dim ds As DataSet = New DataSet()

        adapter.Fill(ds)

        DataGridView1.DataSource = ds.Tables(0)




    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
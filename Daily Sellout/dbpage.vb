Imports System.Data.OleDb

Public Class dbpage
    Dim dd As Form1 = New Form1
    Dim edit_connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim edit_Myconnection As OleDbConnection
    Dim edit_dbda As OleDbDataAdapter
    Dim edit_dbds As DataSet
    Dim edit_tables As DataTableCollection
    Dim edit_source As New BindingSource
    Public edit_dbcmd As New OleDb.OleDbCommand
    Public edit_result As Integer
    Public edit_Sql As String




End Class

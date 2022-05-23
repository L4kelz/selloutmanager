Imports System.Data.OleDb
Imports System.IO
Imports System.Text


'GOOGLE DRIVE
Imports System.Data.SqlClient
Imports System.Configuration
Imports Google.Apis.Auth
Imports Google.Apis.Download
Imports Google.Apis.Drive.v2
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.Threading
Imports Google.Apis.Drive.v2.Data
Imports File = Google.Apis.Drive.v2.Data.File


Public Class Form1
    Dim dropdown_constring As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb")
    Dim dailyreport_constring As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb")
    Dim edit_connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim edit_Myconnection As OleDbConnection
    Dim edit_dbda As OleDbDataAdapter
    Dim edit_dbds As DataSet
    Dim edit_tables As DataTableCollection
    Dim edit_source As New BindingSource
    Public edit_dbcmd As New OleDb.OleDbCommand
    Public edit_result As Integer
    Public edit_Sql As String

    Dim dr As OleDbDataReader
    Dim daily_dr As OleDbDataReader

    Dim SourcePath As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb"
    Dim SourcePath2 As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\Items.accdb"

    Dim forupload As String = "C:\Users\BRYNER\OneDrive\dailysellout_database"
    Dim forupload2 As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database"


    Dim brynerdatabase As String = "c:\Users\BRYNER\Documents\Visual Studio 2010\Projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim bryneronedrive As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb"
    Dim qslwirelessonedrive As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\Items.accdb"


    Dim pathdirectory As String = "----"

#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Dim Pos As Point
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += Control.MousePosition - Pos
        End If
        Pos = Control.MousePosition
    End Sub


#End Region

    Dim service As New DriveService

    Private Sub createservice()

        Dim clientid = "965624698288-p3nm87d9763tlne2ebqm6lmdb8onpsu9.apps.googleusercontent.com"
        Dim clientsecret = "v8xH72O4HqmH3ce-_8TWLmqJ"

        Dim uc As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(New ClientSecrets() With {.ClientId = clientid, .ClientSecret = clientsecret}, {DriveService.Scope.Drive}, "user", CancellationToken.None).Result
        service = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = uc, .ApplicationName = "Google Drive VB Dot Net"})
    End Sub

    'Change C: to the location of your database file.
    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim Myconnection As OleDbConnection
    Dim dbda As OleDbDataAdapter
    Dim dbds As DataSet
    Dim tables As DataTableCollection
    Dim source As New BindingSource
    Public dbcmd As New OleDb.OleDbCommand
    Public result As Integer
    Public Sql As String

    Dim con As New OleDbConnection

    '==========GENERATE================
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If addcheckbox.CheckState = CheckState.Checked Then
            generate()
            generatewithaddnested()
            'generate1()
            'generate2()
            database_loadform()

            If enable_disable_input.CheckState = CheckState.Checked Then
                'do nothing

            Else

                uploaddailydatatocloud()
            End If

        Else
            stash()
        End If

    End Sub

    Private Sub smartload_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smartload.TextChanged

        If Me.smartload.Text.Contains(",") Or Me.smartload.Text.Contains(".") Then
            smartload.Text = smartload.Text.Replace(",", "").Trim()
            smartload.Text = smartload.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(smartload.Text)
            Dim eto1 As String = a1.ToString("N0")
            sl1.Text = eto1
        End If


    End Sub

    Private Sub sunload_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sunload.TextChanged
        If Me.sunload.Text.Contains(",") Or Me.sunload.Text.Contains(".") Then
            sunload.Text = sunload.Text.Replace(",", "").Trim()
            sunload.Text = sunload.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(sunload.Text)
            Dim eto1 As String = a1.ToString("N0")
            sul1.Text = eto1
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        downloaddatafromCloud()
        comboboxdirectory()

        Try

            dayday.Text = Format(DateTimePicker1.Value, "dddd")
            myday.Text = Format(DateTimePicker1.Value, " d")
            thedateofficial.Text = DateTimePicker1.Value.Date.ToString("MM-dd-yyyy")

            DataGridView1.BackgroundColor = Color.FromArgb(60, 60, 60)

            iloadmoto()
            'iloadmoto2()
            homedb1()
            homedb2()

            dailyreport() 'daily report sa home ito

            ako()
            etoitawagmo()
            mtdrecruitmenttotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        database_loadform()


    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        If btn1.BackColor = Color.WhiteSmoke Then
            btn1.BackColor = Color.Red
            btn1.ForeColor = Color.White
            smarttar.Enabled = True
            suntar.Enabled = True
            smarttar.Select()

            If smarttar.Text <> "" Then
                sTar1.Text = smarttar.Text
            Else
                sTar1.Text = "--"
            End If
            If suntar.Text <> "" Then
                suTar1.Text = suntar.Text
            Else
                suTar1.Text = "--"
            End If
        ElseIf btn1.BackColor = Color.Red Then
            btn1.BackColor = Color.WhiteSmoke
            btn1.ForeColor = Color.Black
            smarttar.Enabled = False
            suntar.Enabled = False
            sTar1.Text = "--"
            suTar1.Text = "--"
            Label12.Text = "--"
            resofper.Text = "--"
            resofper2.Text = "--"
            resofper3.Text = "--"
            def.Text = ""
            def2.Text = ""
            def3.Text = ""
        End If
    End Sub

    Private Sub smarttar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles smarttar.TextChanged


        If Me.smarttar.Text.Contains(",") Or Me.smarttar.Text.Contains(".") Then
            smarttar.Text = smarttar.Text.Replace(",", "").Trim()
            smarttar.Text = smarttar.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(smarttar.Text)
            Dim eto1 As String = a1.ToString("N0")
            sTar1.Text = eto1
        End If

        If smarttar.Text = "" Then
            sTar1.Text = "--"
        End If
    End Sub

    Private Sub suntar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles suntar.TextChanged
        If Me.suntar.Text.Contains(",") Or Me.suntar.Text.Contains(".") Then
            suntar.Text = suntar.Text.Replace(",", "").Trim()
            suntar.Text = suntar.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(suntar.Text)
            Dim eto1 As String = a1.ToString("N0")
            suTar1.Text = eto1
        End If
        If suntar.Text = "" Then
            suTar1.Text = "--"
        End If
    End Sub

    Private Sub tb1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb1.TextChanged

        If Me.tb1.Text.Contains(",") Or Me.tb1.Text.Contains(".") Then
            tb1.Text = tb1.Text.Replace(",", "").Trim()
            tb1.Text = tb1.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(tb1.Text)
            Dim eto1 As String = a1.ToString("N0")
            lbl1.Text = eto1
        End If
        If tb1.Text = "" Then
            lbl1.Text = "--"
        End If
    End Sub

    Private Sub tb2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb2.TextChanged
        If Me.tb2.Text.Contains(",") Or Me.tb2.Text.Contains(".") Then
            tb2.Text = tb2.Text.Replace(",", "").Trim()
            tb2.Text = tb2.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(tb2.Text)
            Dim eto1 As String = a1.ToString("N0")
            lbl2.Text = eto1
        End If
        If tb2.Text = "" Then
            lbl2.Text = "--"
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick


        Dim i As Integer

        For i = 0 To DataGridView1.Columns.Count - 1

            DataGridView1.Columns.Item(i).SortMode = DataGridViewColumnSortMode.Programmatic

        Next i

        tb1.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        tb2.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString

        If dayday.Text = "Sunday" Then

        Else
            etoitawagmo()
        End If
        Label16.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        Label17.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        mtdrecruitmenttotal()
        aomonth.Text = DataGridView1.CurrentRow.Cells(1).Value.ToShortDateString
        ' aomonth.Text = Data.Value.Date.ToString("MM-dd-yyyy")
    End Sub



    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged


        thedateofficial.Text = DateTimePicker1.Value.Date.ToString("MM-dd-yyyy")
        yr.Text = Format(DateTimePicker1.Value, "yyyy")
        mm.Text = Format(DateTimePicker1.Value, "MMMM")
        myM.Text = Format(DateTimePicker1.Value, "MM")
        dayday.Text = Format(DateTimePicker1.Value, "dddd")
        myday.Text = Format(DateTimePicker1.Value, " d")
        myday2.Text = Format(DateTimePicker1.Value, "dd")

        If myday.Text = 1 Then

            'unloadmoto()
            DataGridView1.Enabled = False
            tb1.Text = "0"
            tb2.Text = "0"
            aomonth.Text = "--"
            Label16.Text = "0"
            Label17.Text = "0"
            Label18.Text = "0"
        Else
            DataGridView1.Enabled = True
            'iloadmoto()
            ako()

        End If

        If dayday.Text = "Sunday" Then
            Label13.Text = "--"
            Label14.Text = "--"
            Label15.Text = "--"
            rsmart.Text = Label16.Text
            rsun.Text = Label17.Text
            rtotal.Text = Label18.Text
            zero1.Text = "0"
            zero2.Text = "0"
        Else
            etoitawagmo()
            mtdrecruitmenttotal()
        End If




        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
        con.Open()






        If CheckBox1_Mock.CheckState = CheckState.Checked Then
            Dim cmd_mock As New OleDb.OleDbCommand("select count(*) from MockTarget where M_Month = '" & mm.Text & "' AND M_Year = '" & yr.Text & "' ", con)
            Dim count_mock As Int32 = CInt(cmd_mock.ExecuteScalar)
            If count_mock > 0 Then
                '  con.Open()
                Dim ds As New DataSet
                Dim dt As New DataTable
                ds.Tables.Add(dt)
                Dim da As New OleDbDataAdapter
                da = New OleDbDataAdapter("select * from MockTarget where M_Month = '" & mm.Text & "' AND M_Year = '" & yr.Text & "' ", con)
                da.Fill(dt)
                smarttar.Text = dt.Rows(0).Item(1)
                suntar.Text = dt.Rows(0).Item(2)
                '  con.Close()
                btn1.BackColor = Color.Red
                btn1.ForeColor = Color.White

            Else
                smarttar.Clear()
                suntar.Clear()

                btn1.BackColor = Color.WhiteSmoke
                btn1.ForeColor = Color.Black

            End If
        Else
            Dim cmd As New OleDb.OleDbCommand("select count(*) from target where Tmonth = '" & mm.Text & "' AND Tyear = '" & yr.Text & "' ", con)
            Dim count As Int32 = CInt(cmd.ExecuteScalar)
            If count > 0 Then
                '  con.Open()
                Dim ds As New DataSet
                Dim dt As New DataTable
                ds.Tables.Add(dt)
                Dim da As New OleDbDataAdapter
                da = New OleDbDataAdapter("select * from target where Tmonth = '" & mm.Text & "' AND Tyear = '" & yr.Text & "' ", con)
                da.Fill(dt)
                smarttar.Text = dt.Rows(0).Item(1)
                suntar.Text = dt.Rows(0).Item(2)
                '  con.Close()
                btn1.BackColor = Color.Red
                btn1.ForeColor = Color.White
            Else
                smarttar.Clear()
                suntar.Clear()

                btn1.BackColor = Color.WhiteSmoke
                btn1.ForeColor = Color.Black
            End If

        End If






        con.Close()

        smartload.Select()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            manual1.Text = " (Manual Cmpt)"
        Else
            manual1.Text = ""
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            manual2.Text = " (Manual Cmpt)"
        Else
            manual2.Text = ""
        End If
    End Sub

    Private Sub open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles open.Click
        OpenFileDialog1.Multiselect = True
        OpenFileDialog1.Filter = "Excel Files(*.xlsx;*.xls)|*.xlsx;*.xls;"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then

            fullpath.Text = OpenFileDialog1.FileName

            If fullpath.Text.Contains("OpisSummaryLoadLedger") Then
                fromexcel1()
            ElseIf fullpath.Text.Contains("SubDistributorSellOut") Then
                fromexcel2()
            Else
                MsgBox("Error! Please try again.", vbExclamation)
            End If
        End If
    End Sub



    '==============================================
    '==============================================


    Private Sub etoitawagmo()
        Dim a As Integer

        Dim value As Integer = CInt(Int((9 * Rnd()) + 1))
        Label13.Text = ""
        'Label14.Text = ""
        ' Initialize the random-number generator.
        Randomize()
        ' Genrate random value between 0 and 600.


        Label13.Text = CInt(Int((9 * Rnd()) + 1))
        a = Label13.Text

        If Label13.Text = 5 Then
            Dim a2 As Integer
            Dim value2 As Integer = CInt(Int((5 * Rnd()) + 1))
            Label14.Text = ""
            'Label14.Text = ""
            ' Initialize the random-number generator.
            Randomize()
            ' Genrate random value between 0 and 600.
            Label14.Text = CInt(Int((5 * Rnd()) + 1))
            a2 = Label14.Text
        ElseIf Label13.Text = 6 Then
            Dim a3 As Integer
            Dim value3 As Integer = CInt(Int((4 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((4 * Rnd()) + 1))
            a3 = Label14.Text
        ElseIf Label13.Text = 7 Then
            Dim a4 As Integer
            Dim value4 As Integer = CInt(Int((3 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((3 * Rnd()) + 1))
            a4 = Label14.Text
        ElseIf Label13.Text = 8 Then
            Dim a5 As Integer
            Dim value5 As Integer = CInt(Int((2 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((2 * Rnd()) + 1))
            a5 = Label14.Text
        ElseIf Label13.Text = 9 Then
            Label14.Text = "1"
        ElseIf Label13.Text = 1 Then
            Dim a6 As Integer
            Dim value6 As Integer = CInt(Int((9 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((9 * Rnd()) + 1))
            a6 = Label14.Text
        ElseIf Label13.Text = 2 Then
            Dim a7 As Integer
            Dim value7 As Integer = CInt(Int((8 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((8 * Rnd()) + 1))
            a7 = Label14.Text
        ElseIf Label13.Text = 3 Then
            Dim a8 As Integer
            Dim value8 As Integer = CInt(Int((7 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((7 * Rnd()) + 1))
            a8 = Label14.Text
        ElseIf Label13.Text = 4 Then
            Dim a9 As Integer
            Dim value9 As Integer = CInt(Int((6 * Rnd()) + 1))
            Label14.Text = ""
            Randomize()
            Label14.Text = CInt(Int((6 * Rnd()) + 1))
            a9 = Label14.Text
        End If

        Dim add1 As Integer
        Dim add2 As Integer
        Dim sumrecruitment As Integer

        add1 = Label13.Text
        add2 = Label14.Text
        sumrecruitment = add1 + add2
        Label15.Text = sumrecruitment
        zero1.Text = Label13.Text
        zero2.Text = Label14.Text

    End Sub

    Private Sub mtdrecruitmenttotal()
        'Label16.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        ' Label17.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        Dim a As Integer
        Dim B As Integer
        Dim sum As Integer

        a = Label16.Text
        B = Label17.Text
        sum = a + B
        Label18.Text = sum


        If dayday.Text = "Sunday" Then
            rsmart.Text = Label16.Text
            rsun.Text = Label17.Text
            rtotal.Text = Label18.Text
        Else
            Dim c As Integer
            Dim d As Integer
            Dim e As Integer
            Dim sum2 As Integer
            Dim sum2x As Integer
            Dim sum2xx As Integer
            c = Label13.Text
            d = Label14.Text
            e = Label15.Text

            sum2 = a + c
            sum2x = B + d
            sum2xx = sum + e

            rsmart.Text = sum2
            rsun.Text = sum2x
            rtotal.Text = sum2xx


        End If

    End Sub

    Private Sub ako()
        Label16.Text = DataGridView1.Rows(0).Cells(4).Value.ToString
        Label17.Text = DataGridView1.Rows(0).Cells(5).Value.ToString

        Me.tb1.Text = DataGridView1.Rows(0).Cells(2).Value.ToString()
        Me.tb2.Text = DataGridView1.Rows(0).Cells(3).Value.ToString()

        aomonth.Text = DataGridView1.Rows(0).Cells(1).Value.ToShortDateString

    End Sub

    Private Sub sirmolong()
        Dim xx As Decimal
        Dim z As Integer
        Dim y As Integer

        z = numdays.Text
        y = Label12.Text
        xx = y / z
        Dim output As String = xx.ToString("N0")
        dividetarget.Text = output

        Dim xxx As Decimal
        Dim a As Integer

        a = totalloadlbl.Text
        xxx = a / xx * 100
        Dim output2 As String = xxx.ToString("N2")
        percentageko.Text = output2
    End Sub

    Public Sub iloadmoto2()
        Try
            formloadmonth.Text = Format(DateTimePicker1.Value, "MM")
            formloadyear.Text = Format(DateTimePicker1.Value, "yyyy")

            Dim una22 As String

            If formloadmonth.Text = "01" Then
                una22 = "1"
            ElseIf formloadmonth.Text = "02" Then
                una22 = "2"
            ElseIf formloadmonth.Text = "03" Then
                una22 = "3"
            ElseIf formloadmonth.Text = "04" Then
                una22 = "4"
            ElseIf formloadmonth.Text = "05" Then
                una22 = "5"
            ElseIf formloadmonth.Text = "06" Then
                una22 = "6"
            ElseIf formloadmonth.Text = "07" Then
                una22 = "7"
            ElseIf formloadmonth.Text = "08" Then
                una22 = "8"
            ElseIf formloadmonth.Text = "09" Then
                una22 = "9"
            ElseIf formloadmonth.Text = "10" Then
                una22 = "10"
            ElseIf formloadmonth.Text = "11" Then
                una22 = "11"
            ElseIf formloadmonth.Text = "12" Then
                una22 = "12"
            End If

            Dim finalto As String
            finalto = una22



            DataGridView1.RowHeadersVisible = False
            Myconnection = New OleDbConnection
            Myconnection.ConnectionString = connString
            dbds = New DataSet
            tables = dbds.Tables

            'dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", Myconnection)
            dbda = New OleDbDataAdapter("Select * from tblitems where MONTH(DATE) = '" & finalto & "' and YEAR(DATE) = '" & formloadyear.Text & "' order by DATE Desc", Myconnection)
            dbda.Fill(dbds, "tblitems")
            Dim view As New DataView(tables(0))

            source.DataSource = view
            DataGridView1.DataSource = view

            DataGridView1.Columns("ID").Visible = False
            DataGridView1.Columns("MTDRECRUITMENTSMART").Visible = False
            DataGridView1.Columns("MTDRECRUITMENTSUN").Visible = False
            DataGridView1.Columns("RECRUITMENTSMART").Visible = False
            DataGridView1.Columns("RECRUITMENTSUN").Visible = False
            DataGridView1.Columns("PAR").Visible = False

            DataGridView1.Columns(6).DefaultCellStyle.Format = "N0"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N0"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub homedb1()
        Try

            Dim homedbtaon As String

            homedbtaon = Format(DateTimePicker1.Value, "yyyy")
            homedblbl1.Text = "YTD " & homedbtaon & ""

            '===============================
            'MONTH YTD
            Dim homedbbuwan As Integer
            Dim buwanstring As String
            Dim a As Integer
            a = Format(DateTimePicker1.Value, "MM")
            homedbbuwan = a - 1

            If homedbbuwan = 1 Then
                buwanstring = "--"
            ElseIf homedbbuwan = 2 Then
                buwanstring = "February"
            ElseIf homedbbuwan = 3 Then
                buwanstring = "March"
            ElseIf homedbbuwan = 4 Then
                buwanstring = "April"
            ElseIf homedbbuwan = 5 Then
                buwanstring = "May"
            ElseIf homedbbuwan = 6 Then
                buwanstring = "June"
            ElseIf homedbbuwan = 7 Then
                buwanstring = "July"
            ElseIf homedbbuwan = 8 Then
                buwanstring = "August"
            ElseIf homedbbuwan = 9 Then
                buwanstring = "September"
            ElseIf homedbbuwan = 10 Then
                buwanstring = "October"
            ElseIf homedbbuwan = 11 Then
                buwanstring = "November"
            ElseIf homedbbuwan = 12 Then
                buwanstring = "December"
            End If
            homedbmonthlbl1.Text = "(January - " & buwanstring & ")"
            '===============================================
            DataGridView3.RowHeadersVisible = False
            Myconnection = New OleDbConnection
            Myconnection.ConnectionString = connString
            dbds = New DataSet
            tables = dbds.Tables
            'dbda = New OleDbDataAdapter("Select * from tblitems where DATE BETWEEN CDATE('01/01/2021') AND CDATE('05/20/2021') ORDER BY DATE DESC", Myconnection)
            'dbda = New OleDbDataAdapter("Select SUM(SMARTLOAD) as sum_load from tblitems where DATE BETWEEN CDATE('01/01/2021') AND CDATE('05/20/2021')", Myconnection)
            dbda = New OleDbDataAdapter("SELECT SUM(MTDSMART) as sum_load FROM tblitems o WHERE (YEAR(o.DATE) IN ('" & homedbtaon & "') AND MONTH(o.DATE) IN ('1','3','5','7','8','10','12') AND DAY(o.DATE) = '31')
    OR (YEAR(o.DATE) IN ('" & homedbtaon & "') AND MONTH(o.DATE) IN ('4','6','9','11') AND DAY(o.DATE) = '30')
    OR (YEAR(o.DATE) IN ('" & homedbtaon & "') AND MONTH(o.DATE) IN ('2') AND DAY(o.DATE) = '28')", Myconnection)
            dbda.Fill(dbds, "tblitems")
            Dim view As New DataView(tables(0))

            source.DataSource = view
            DataGridView3.DataSource = view


            DataGridView3.Columns("ID").Visible = False
            DataGridView3.Columns("MTDSUN").Visible = False
            DataGridView3.Columns("SUNLOAD").Visible = False
            DataGridView3.Columns("MTDRECRUITMENTSMART").Visible = False
            DataGridView3.Columns("MTDRECRUITMENTSUN").Visible = False
            DataGridView3.Columns("RECRUITMENTSMART").Visible = False
            DataGridView3.Columns("RECRUITMENTSUN").Visible = False
            DataGridView3.Columns("PAR").Visible = False

            DataGridView3.Columns(6).DefaultCellStyle.Format = "N0"
            DataGridView3.Columns(2).DefaultCellStyle.Format = "N0"



        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub homedb2()
        Try

            Dim homedbtaon As String
            Dim homedbtaonint As Long
            Dim homedbtaona As Long
            Dim homedbtaonx As Long

            homedbtaon = Format(DateTimePicker1.Value, "yyyy")
            homedbtaonint = homedbtaon

            homedbtaona = homedbtaonint - 1

            homedbtaonx = homedbtaona.ToString("#,##0")
            homedblbl2.Text = "YTD " & homedbtaonx & ""

            '===============================
            'MONTH YTD
            Dim homedbbulan As Integer
            Dim buwanstring As String
            Dim k As Integer
            k = Format(DateTimePicker1.Value, "MM")
            homedbbulan = k - 1

            If homedbbulan = 1 Then
                buwanstring = "--"
            ElseIf homedbbulan = 2 Then
                buwanstring = "February"
            ElseIf homedbbulan = 3 Then
                buwanstring = "March"
            ElseIf homedbbulan = 4 Then
                buwanstring = "April"
            ElseIf homedbbulan = 5 Then
                buwanstring = "May"
            ElseIf homedbbulan = 6 Then
                buwanstring = "June"
            ElseIf homedbbulan = 7 Then
                buwanstring = "July"
            ElseIf homedbbulan = 8 Then
                buwanstring = "August"
            ElseIf homedbbulan = 9 Then
                buwanstring = "September"
            ElseIf homedbbulan = 10 Then
                buwanstring = "October"
            ElseIf homedbbulan = 11 Then
                buwanstring = "November"
            ElseIf homedbbulan = 12 Then
                buwanstring = "December"
            End If
            homedbmonthlbl2.Text = "(January - " & buwanstring & ")"
            '===============================================


            '=================================
            'MONTH
            Dim m1 As String
            Dim m2 As String
            Dim m3 As String
            Dim m4 As String
            Dim m5 As String
            Dim m6 As String
            Dim m7 As String
            Dim m8 As String
            Dim m9 As String
            Dim m10 As String
            Dim m11 As String
            Dim m12 As String



            Dim homedbbuwan As Integer
            Dim a As Integer

            a = Format(DateTimePicker1.Value, "MM")

            homedbbuwan = a - 1

            If homedbbuwan = 1 Then
                m1 = "1"
                m2 = ""
                m3 = ""
                m4 = ""
                m5 = ""
                m6 = ""
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 2 Then
                m1 = "1"
                m2 = "2"
                m3 = ""
                m4 = ""
                m5 = ""
                m6 = ""
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 3 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = ""
                m5 = ""
                m6 = ""
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 4 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = ""
                m6 = ""
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 5 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = ""
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 6 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = ""
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 7 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = ""
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 8 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = "8"
                m9 = ""
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 9 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = "8"
                m9 = "9"
                m10 = ""
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 10 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = "8"
                m9 = "9"
                m10 = "10"
                m11 = ""
                m12 = ""
            ElseIf homedbbuwan = 11 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = "8"
                m9 = "9"
                m10 = "10"
                m11 = "11"
                m12 = ""
            ElseIf homedbbuwan = 12 Then
                m1 = "1"
                m2 = "2"
                m3 = "3"
                m4 = "4"
                m5 = "5"
                m6 = "6"
                m7 = "7"
                m8 = "8"
                m9 = "9"
                m10 = "10"
                m11 = "11"
                m12 = "12"
            End If





            '=================================


            DataGridView4.RowHeadersVisible = False
            Myconnection = New OleDbConnection
            Myconnection.ConnectionString = connString
            dbds = New DataSet
            tables = dbds.Tables
            'dbda = New OleDbDataAdapter("Select * from tblitems where DATE BETWEEN CDATE('01/01/2021') AND CDATE('05/20/2021') ORDER BY DATE DESC", Myconnection)
            'dbda = New OleDbDataAdapter("Select SUM(SMARTLOAD) as sum_load from tblitems where DATE BETWEEN CDATE('01/01/2021') AND CDATE('05/20/2021')", Myconnection)
            dbda = New OleDbDataAdapter("SELECT SUM(MTDSMART) as sum_load FROM tblitems o WHERE (YEAR(o.DATE) IN ('" & homedbtaonx & "') AND MONTH(o.DATE) IN ('" & m1 & "','" & m3 & "','" & m5 & "','" & m7 & "','" & m8 & "','" & m10 & "','" & m12 & "') AND DAY(o.DATE) = '31')
    OR (YEAR(o.DATE) IN ('" & homedbtaonx & "') AND MONTH(o.DATE) IN ('" & m4 & "','" & m6 & "','" & m9 & "','" & m11 & "') AND DAY(o.DATE) = '30')
    OR (YEAR(o.DATE) IN ('" & homedbtaonx & "') AND MONTH(o.DATE) IN ('" & m2 & "') AND DAY(o.DATE) = '28')", Myconnection)
            dbda.Fill(dbds, "tblitems")
            Dim view As New DataView(tables(0))

            source.DataSource = view
            DataGridView4.DataSource = view


            DataGridView4.Columns("ID").Visible = False
            DataGridView4.Columns("MTDSUN").Visible = False
            DataGridView4.Columns("SUNLOAD").Visible = False
            DataGridView4.Columns("MTDRECRUITMENTSMART").Visible = False
            DataGridView4.Columns("MTDRECRUITMENTSUN").Visible = False
            DataGridView4.Columns("RECRUITMENTSMART").Visible = False
            DataGridView4.Columns("RECRUITMENTSUN").Visible = False
            DataGridView4.Columns("PAR").Visible = False

            DataGridView4.Columns(6).DefaultCellStyle.Format = "N0"
            DataGridView4.Columns(2).DefaultCellStyle.Format = "N0"

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub iloadmoto()
        Try

            formloadmonth.Text = Format(DateTimePicker1.Value, "MM")
            formloadyear.Text = Format(DateTimePicker1.Value, "yyyy")
            DataGridView1.RowHeadersVisible = False
            Myconnection = New OleDbConnection
            Myconnection.ConnectionString = connString
            dbds = New DataSet
            tables = dbds.Tables
            dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", Myconnection)
            dbda.Fill(dbds, "tblitems")
            Dim view As New DataView(tables(0))

            source.DataSource = view
            DataGridView1.DataSource = view


            DataGridView1.Columns("ID").Visible = False
            DataGridView1.Columns("MTDRECRUITMENTSMART").Visible = False
            DataGridView1.Columns("MTDRECRUITMENTSUN").Visible = False
            DataGridView1.Columns("RECRUITMENTSMART").Visible = False
            DataGridView1.Columns("RECRUITMENTSUN").Visible = False
            DataGridView1.Columns("PAR").Visible = False

            DataGridView1.Columns(6).DefaultCellStyle.Format = "N0"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N0"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub iloadmotoDB()
        Try

            formloadmonth.Text = Format(DateTimePicker1.Value, "MM")
            formloadyear.Text = Format(DateTimePicker1.Value, "yyyy")
            DataGridView2.RowHeadersVisible = False
            Myconnection = New OleDbConnection
            Myconnection.ConnectionString = connString
            dbds = New DataSet
            tables = dbds.Tables
            dbda = New OleDbDataAdapter("Select * from tblitems order by DATE Desc", Myconnection)
            dbda.Fill(dbds, "tblitems")
            Dim view As New DataView(tables(0))

            source.DataSource = view
            DataGridView1.DataSource = view


            DataGridView2.Columns("ID").Visible = False
            DataGridView2.Columns("MTDRECRUITMENTSMART").Visible = False
            DataGridView2.Columns("MTDRECRUITMENTSUN").Visible = False
            DataGridView2.Columns("RECRUITMENTSMART").Visible = False
            DataGridView2.Columns("RECRUITMENTSUN").Visible = False
            DataGridView2.Columns("PAR").Visible = False

            DataGridView1.Columns(6).DefaultCellStyle.Format = "N0"
            DataGridView1.Columns(2).DefaultCellStyle.Format = "N0"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub unloadmoto()
        DataGridView1.DataSource = Nothing
    End Sub

    Private Sub fromexcel1()
        Try
            'Dim path As String = "'C:\Users\Bryner\Downloads\OpisSummaryLoadLedger(3).xlsx'"

            Dim path As String = fullpath.Text
            con.ConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;"
            con.Open()

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds.Tables.Add(dt)
            Dim da As New OleDbDataAdapter
            da = New OleDbDataAdapter("SELECT * FROM [OpisSummaryLoadLedger$]", con)
            da.Fill(dt)
            'DSP SUB TOTAL
            main_1.Text = dt.Rows(115).Item(3)
            main_2.Text = dt.Rows(116).Item(3)
            main_3.Text = dt.Rows(117).Item(3)
            main_4.Text = dt.Rows(118).Item(3)
            main_5.Text = dt.Rows(119).Item(3)
            main_6.Text = dt.Rows(120).Item(3)
            main_7.Text = dt.Rows(121).Item(3)
            main_8.Text = dt.Rows(122).Item(3)
            main_9.Text = dt.Rows(123).Item(3)
            main_10.Text = dt.Rows(124).Item(3)

            'LS2 SUB TOTAL
            main2_1.Text = dt.Rows(165).Item(3)
            main2_2.Text = dt.Rows(166).Item(3)
            main2_3.Text = dt.Rows(167).Item(3)
            main2_4.Text = dt.Rows(168).Item(3)
            main2_5.Text = dt.Rows(169).Item(3)
            main2_6.Text = dt.Rows(170).Item(3)
            main2_7.Text = dt.Rows(171).Item(3)
            main2_8.Text = dt.Rows(172).Item(3)
            main2_9.Text = dt.Rows(173).Item(3)
            main2_10.Text = dt.Rows(174).Item(3)


            'DSP SUB TOTAL
            If main_1.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(115).Item(9)
            ElseIf main_2.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(116).Item(9)
            ElseIf main_3.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(117).Item(9)
            ElseIf main_4.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(118).Item(9)
            ElseIf main_5.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(119).Item(9)
            ElseIf main_6.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(120).Item(9)
            ElseIf main_7.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(121).Item(9)
            ElseIf main_8.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(122).Item(9)
            ElseIf main_9.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(123).Item(9)
            ElseIf main_10.Text = "DSP Sub Total" Then
                L1.Text = dt.Rows(124).Item(9)
            End If

            'LS2 SUB TOTAL
            If main2_1.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(165).Item(9)
            ElseIf main2_2.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(166).Item(9)
            ElseIf main2_3.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(167).Item(9)
            ElseIf main2_4.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(168).Item(9)
            ElseIf main2_5.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(169).Item(9)
            ElseIf main2_6.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(170).Item(9)
            ElseIf main2_7.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(171).Item(9)
            ElseIf main2_8.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(172).Item(9)
            ElseIf main2_9.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(173).Item(9)
            ElseIf main2_10.Text = "LS2 Sub Total" Then
                L2.Text = dt.Rows(174).Item(9)
            End If


            con.Close()
            Dim sum As Long
            Dim a As Long
            Dim b As Long
            a = L1.Text
            b = L2.Text
            sum = a + b

            smartload.Text = sum

        Catch ex As Exception
            MessageBox.Show(ex.Message)


        Finally
            con.Close()
        End Try
    End Sub

    Private Sub fromexcel2()
        Try
            Dim path As String = fullpath.Text
            'C:\Users\Bryner\Downloads\SubDistributorSellOut6142019(1).xls
            con.ConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 8.0"
            con.Open()

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds.Tables.Add(dt)
            Dim da As New OleDbDataAdapter
            da = New OleDbDataAdapter("SELECT * FROM [Sheet1$]", con)
            da.Fill(dt)

            s2.Text = Format(DateTimePicker1.Value, " d")
            Dim b As Integer
            Dim sum As Integer
            b = s2.Text
            sum = b + 5

            find.Text = dt.Rows(71).Item(sum)
            Dim a As Long
            a = find.Text
            Dim output As String = a.ToString("N0")
            sunload.Text = output



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub



    Private Sub generate()
        If smartload.Text = "" Or sunload.Text = "" Or tb1.Text = "" Or tb2.Text = "" Then
            ' MessageBox.Show("Pleas fill in the Smart Load and Sun Load TextBox")
            MsgBox("Pleas fill in the Smart Load and Sun Load textbox", vbExclamation)
        Else
            'sl1.Text = smartload.Text
            'sul1.Text = sunload.Text
            ' mtdrecruitmenttotal()

            datehere.Text = Format(DateTimePicker1.Value, "MMMM d, yyyy")
            day.Text = Format(DateTimePicker1.Value, "dd")



            Dim CurrentMonthDays As Int16 = DateTime.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month)
            numdays.Text = CurrentMonthDays

            Dim result1 As Decimal = onehundred.Text / numdays.Text * day.Text
            'Dim result2 As Integer = result1 * day.Text
            Dim output As String = result1.ToString("N2")
            res.Text = output

            '==============================
            'Sum of smartload and sunload
            '==============================
            Dim sum1 As Long
            Dim sum2 As Long
            Dim sum3 As Long
            Dim a As Long
            Dim b As Long
            Dim cc As Long
            Dim cc2 As Long

            Dim totalactual As Long

            cc = tb1.Text
            cc2 = tb2.Text

            a = smartload.Text
            b = sunload.Text

            sum1 = a + b
            sum2 = a + cc
            sum3 = b + cc2
            totalactual = sum2 + sum3

            Dim output2 As String = sum1.ToString("N0")
            totalloadlbl.Text = output2

            Dim outout As String = sum2.ToString("N0")
            Label10.Text = outout

            Dim outout2 As String = sum3.ToString("N0")
            Label11.Text = outout2


            Dim actualto As String = totalactual.ToString("N0")
            totalofactual.Text = actualto

            If btn1.BackColor = Color.WhiteSmoke Then

            ElseIf btn1.BackColor = Color.Red Then


                Dim sumoftar As Long
                Dim stext1 As Long
                Dim stext2 As Long

                stext1 = smarttar.Text
                stext2 = suntar.Text

                sumoftar = stext1 + stext2
                Dim outputoftarget As String = sumoftar.ToString("N0")
                Label12.Text = outputoftarget
                '==============================
                'percentage of load smart
                '==============================
                Dim per As Decimal

                Dim c As Integer
                Dim d As Integer
                Dim hun As Integer

                c = Label10.Text
                d = smarttar.Text
                hun = 100

                per = c / d * hun
                Dim out As String = per.ToString("N2")
                resofper.Text = out

                Dim aAsDecimal As Decimal = Decimal.Parse(out).ToString("N")

                Dim mother As Decimal

                Dim res22 As Decimal
                res22 = res.Text

                mother = aAsDecimal - res22
                Dim out2 As String = mother.ToString("N2")
                def.Text = out2

                If def.Text.Contains("-") Then
                    fordef.Text = ""
                Else
                    fordef.Text = "+"
                End If

                '==============================
                'percentage of load sun
                '==============================
                'Dim ako As Decimal

                'Dim c2 As Integer
                'Dim d2 As Integer
                'Dim hun2 As Integer

                'c2 = Label11.Text
                'd2 = suntar.Text
                'hun2 = 100

                'ako = c2 / d2 * hun2
                'Dim eto1 As String = ako.ToString("N2")
                'resofper2.Text = eto1

                'Dim aAsDecimal2 As Decimal = Decimal.Parse(eto1).ToString("N")

                'Dim mother2 As Decimal

                'Dim res222 As Decimal
                'res222 = res.Text

                'mother2 = aAsDecimal2 - res222
                'Dim out23 As String = mother2.ToString("N2")
                'def2.Text = out23

                'If def2.Text.Contains("-") Then
                'fordef2.Text = ""
                'Else
                'fordef2.Text = "+"
                'End If


                Dim lastshot As Decimal
                Dim haha As Integer
                Dim res2222 As Decimal
                Dim lastshot2 As Decimal
                res2222 = res.Text

                haha = 100
                lastshot = totalactual / sumoftar * haha

                Dim last1 As String = lastshot.ToString("N2")
                resofper3.Text = last1

                Dim aAsDecimal3 As Decimal = Decimal.Parse(last1).ToString("N")


                lastshot2 = last1 - res2222
                Dim last2 As String = lastshot2.ToString("N2")
                def3.Text = last2

                If def3.Text.Contains("-") Then
                    fordef3.Text = ""
                Else
                    fordef3.Text = "+"
                End If

                sirmolong()






            End If




            OneDriveCreateRecords()


        End If
    End Sub


    Private Sub stash()
        If smartload.Text = "" Or sunload.Text = "" Or tb1.Text = "" Or tb2.Text = "" Then
            ' MessageBox.Show("Pleas fill in the Smart Load and Sun Load TextBox")
            MsgBox("Pleas fill in the Smart Load and Sun Load textbox", vbExclamation)
        Else
            'sl1.Text = smartload.Text
            'sul1.Text = sunload.Text
            ' mtdrecruitmenttotal()

            datehere.Text = Format(DateTimePicker1.Value, "MMMM d, yyyy")
            day.Text = Format(DateTimePicker1.Value, "dd")



            Dim CurrentMonthDays As Int16 = DateTime.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month)
            numdays.Text = CurrentMonthDays

            Dim result1 As Decimal = onehundred.Text / numdays.Text * day.Text
            'Dim result2 As Integer = result1 * day.Text
            Dim output As String = result1.ToString("N2")
            res.Text = output

            '==============================
            'Sum of smartload and sunload
            '==============================
            Dim sum1 As Long
            Dim sum2 As Long
            Dim sum3 As Long
            Dim a As Long
            Dim b As Long
            Dim cc As Long
            Dim cc2 As Long

            Dim totalactual As Long

            cc = tb1.Text
            cc2 = tb2.Text

            a = smartload.Text
            b = sunload.Text

            sum1 = a + b
            sum2 = a + cc
            sum3 = b + cc2
            totalactual = sum2 + sum3

            Dim output2 As String = sum1.ToString("N0")
            totalloadlbl.Text = output2

            Dim outout As String = sum2.ToString("N0")
            Label10.Text = outout

            Dim outout2 As String = sum3.ToString("N0")
            Label11.Text = outout2


            Dim actualto As String = totalactual.ToString("N0")
            totalofactual.Text = actualto

            If btn1.BackColor = Color.WhiteSmoke Then

            ElseIf btn1.BackColor = Color.Red Then


                Dim sumoftar As Long
                Dim stext1 As Long
                Dim stext2 As Long

                stext1 = smarttar.Text
                stext2 = suntar.Text

                sumoftar = stext1 + stext2
                Dim outputoftarget As String = sumoftar.ToString("N0")
                Label12.Text = outputoftarget
                '==============================
                'percentage of load smart
                '==============================
                Dim per As Decimal

                Dim c As Integer
                Dim d As Integer
                Dim hun As Integer

                c = Label10.Text
                d = smarttar.Text
                hun = 100

                per = c / d * hun
                Dim out As String = per.ToString("N2")
                resofper.Text = out

                Dim aAsDecimal As Decimal = Decimal.Parse(out).ToString("N")

                Dim mother As Decimal

                Dim res22 As Decimal
                res22 = res.Text

                mother = aAsDecimal - res22
                Dim out2 As String = mother.ToString("N2")
                def.Text = out2

                If def.Text.Contains("-") Then
                    fordef.Text = ""
                Else
                    fordef.Text = "+"
                End If

                '==============================
                'percentage of load sun
                '==============================






                Dim lastshot As Decimal
                Dim haha As Integer
                Dim res2222 As Decimal
                Dim lastshot2 As Decimal
                res2222 = res.Text

                haha = 100
                lastshot = totalactual / sumoftar * haha

                Dim last1 As String = lastshot.ToString("N2")
                resofper3.Text = last1

                Dim aAsDecimal3 As Decimal = Decimal.Parse(last1).ToString("N")


                lastshot2 = last1 - res2222
                Dim last2 As String = lastshot2.ToString("N2")
                def3.Text = last2

                If def3.Text.Contains("-") Then
                    fordef3.Text = ""
                Else
                    fordef3.Text = "+"
                End If

                sirmolong()

                '======================================
                'DEFICIT
                '======================================
                If CheckBox3.CheckState = CheckState.Checked Then

                    Dim actual As Long
                    Dim target As Long
                    Dim sumdef As Long


                    actual = Label10.Text
                    target = sTar1.Text
                    sumdef = actual - target

                    Dim astala As String = sumdef.ToString("N0")
                    deficit.Text = astala

                    If actual < target Then
                        deficitword.Text = "" & vbCrLf & vbCrLf & "Deficit: "
                    ElseIf actual >= target Then
                        deficitword.Text = "" & vbCrLf & vbCrLf & "Over Target: "
                    End If

                Else

                End If




            End If


            Dim akingstash As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + ""
            If (Not System.IO.Directory.Exists(akingstash)) Then
                System.IO.Directory.CreateDirectory(akingstash)
            End If

            Dim filestash As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\stash.txt"
            If Not System.IO.File.Exists(filestash) Then
                System.IO.File.Create(filestash).Dispose()
            End If

            System.IO.File.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\stash.txt", "")

            My.Computer.FileSystem.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\stash.txt",
            "PD Name: Qsl Wireless." & vbCrLf & "Date: " & datehere.Text & "" & vbCrLf &
            "PAR: " & res.Text & vbCrLf & "Target / Actual/ % Ach" & vbCrLf & vbCrLf & "DAILY Sell out" _
            & vbCrLf & "Smart load : " & sl1.Text & manual2.Text & vbCrLf & vbCrLf &
            "Daily Recruitment" & vbCrLf & "Smart : " & Label13.Text & vbCrLf & vbCrLf &
           "MTD Sell-out" & vbCrLf & "Smart load :  " _
            & sTar1.Text & " / " & Label10.Text & " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & vbCrLf &
            "MTD Recruitment" & vbCrLf & "Smart : " & rsmart.Text & vbCrLf &
            "________________________" & vbCrLf & vbCrLf & _
 _
            "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datehere.Text & vbCrLf &
            "PAR:  " & res.Text & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & sl1.Text & manual2.Text &
            vbCrLf & "-------" & vbCrLf & vbCrLf &
            "L1 Actual: " & totalloadlbl.Text & vbCrLf & "L1 Target: " & dividetarget.Text & vbCrLf & "% Ach : " & percentageko.Text & vbCrLf &
            vbCrLf &
            "MTD" & vbCrLf & "Target / Actual / % Ach " & vbCrLf & "Smart load : " & sTar1.Text & " / " & Label10.Text &
            " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & "__________________________" & deficitword.Text & deficit.Text & unofficialtarget.Text & "", True)

            Process.Start("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\stash.txt")



        End If
    End Sub


    Private Sub generatewithaddnested()
        Try
            Sql = "INSERT INTO  tblitems([DATE], MTDSMART, MTDSUN, MTDRECRUITMENTSMART, MTDRECRUITMENTSUN, SMARTLOAD, SUNLOAD, RECRUITMENTSMART, RECRUITMENTSUN, PAR) VALUES ('" & thedateofficial.Text & "','" & Label10.Text & "', '" & Label11.Text & "','" & rsmart.Text & "','" & rsun.Text & "', '" & sl1.Text & "', '" & sul1.Text & "', '" & zero1.Text & "', '" & zero2.Text & "', '" & res.Text & "' )"
            Myconnection.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = Myconnection
            End With
            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                'MsgBox("New item record has been added!", vbInformation)
                Myconnection.Close()
                DataGridView1.DataSource = Nothing
                iloadmoto()
                'iloadmoto2()
                uploaddatatoCloud()
                dailyreport()
            Else
                MsgBox("No item record has been saved!!", vbExclamation)
            End If

        Catch ex As Exception
            ' MsgBox(ex.Message, vbExclamation)
            ' MsgBox("Record already exist!", vbExclamation)
        Finally
            Myconnection.Close()
        End Try
    End Sub

    Private Sub generate1()

        Dim akingpath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\1"
        If (Not System.IO.Directory.Exists(akingpath)) Then
            System.IO.Directory.CreateDirectory(akingpath)
        End If

        Dim filepath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\1\" + myM.Text + myday2.Text + yr.Text + "1.txt"
        If Not System.IO.File.Exists(filepath) Then
            System.IO.File.Create(filepath).Dispose()
        End If
        System.IO.File.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\1\" + myM.Text + myday2.Text + yr.Text + "1.txt", "")
        My.Computer.FileSystem.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\1\" + myM.Text + myday2.Text + yr.Text + "1.txt",
            "" & vbCrLf & "PD Name: Qsl Wireless." & vbCrLf & "Date: " & datehere.Text & "" & vbCrLf &
            "PAR: " & res.Text & vbCrLf & "Target / Actual/ % Ach" & vbCrLf & vbCrLf & "DAILY Sell out" _
            & vbCrLf & "Smart load : " & sl1.Text & manual2.Text & vbCrLf & vbCrLf &
            "Daily Recruitment" & vbCrLf & "Smart : " & Label13.Text & vbCrLf & vbCrLf &
           "MTD Sell-out" & vbCrLf & "Smart load :  " _
            & sTar1.Text & " / " & Label10.Text & " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & vbCrLf &
            "MTD Recruitment" & vbCrLf & "Smart : " & rsmart.Text & vbCrLf &
            "________________________" & vbCrLf & "", True)

    End Sub

    Private Sub generate2()

        Dim akingpath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\2"
        If (Not System.IO.Directory.Exists(akingpath)) Then
            System.IO.Directory.CreateDirectory(akingpath)
        End If

        Dim filepath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\2\" + myM.Text + myday2.Text + yr.Text + "2.txt"
        If Not System.IO.File.Exists(filepath) Then
            System.IO.File.Create(filepath).Dispose()
        End If
        System.IO.File.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\2\" + myM.Text + myday2.Text + yr.Text + "2.txt", "")
        My.Computer.FileSystem.WriteAllText("C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" + yr.Text + "\" + mm.Text + "\1_2\2\" + myM.Text + myday2.Text + yr.Text + "2.txt",
            "" & vbCrLf & "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datehere.Text & vbCrLf &
            "PAR:  " & res.Text & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & sl1.Text & manual2.Text &
            vbCrLf & "-------" & vbCrLf & vbCrLf &
            "L1 Actual: " & totalloadlbl.Text & vbCrLf & "L1 Target: " & dividetarget.Text & vbCrLf & "% Ach : " & percentageko.Text & vbCrLf &
            vbCrLf &
            "MTD" & vbCrLf & "Target / Actual / % Ach " & vbCrLf & "Smart load : " & sTar1.Text & " / " & Label10.Text &
            " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & "__________________________", True)

    End Sub

    Private Sub downloaddatafromCloud()
        Try
            If System.IO.File.Exists(SourcePath) Then
                My.Computer.FileSystem.DeleteFile(brynerdatabase)
                My.Computer.FileSystem.CopyFile(bryneronedrive, brynerdatabase)
                'MsgBox("Success!", vbInformation)
                iloadmoto()
                'iloadmoto2()
            ElseIf System.IO.File.Exists(SourcePath2) Then
                My.Computer.FileSystem.DeleteFile(brynerdatabase)
                My.Computer.FileSystem.CopyFile(qslwirelessonedrive, brynerdatabase)
                'MsgBox("Success!", vbInformation)
                iloadmoto()
                'iloadmoto2()
            Else
                MsgBox("Could not find" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb", vbExclamation)
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try
    End Sub

    Public Sub uploaddatatoCloud()
        '========================================
        'TO UPLOAD DATA ITEMS TO ONEDRIVE
        '========================================
        Try
            If System.IO.Directory.Exists(forupload) Then
                My.Computer.FileSystem.DeleteFile(bryneronedrive)
                My.Computer.FileSystem.CopyFile(brynerdatabase, bryneronedrive)
                'MsgBox("Success!", vbInformation)
            ElseIf System.IO.Directory.Exists(forupload2) Then
                My.Computer.FileSystem.DeleteFile(qslwirelessonedrive)
                My.Computer.FileSystem.CopyFile(brynerdatabase, qslwirelessonedrive)
                'MsgBox("Success!", vbInformation)
            Else
                MsgBox("Could not find" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database", vbExclamation)
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try


    End Sub
    Private Sub uploaddailydatatocloud()
        Dim DownloadPath As String = fullpath.Text



        Dim BrynerCloud As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\database_DailyLoadLedger\" & yr.Text & "\" & mm.Text & ""
        Dim BrynerCloudtoupload As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\database_DailyLoadLedger\" & yr.Text & "\" & mm.Text & "\" & myM.Text & myday2.Text & yr.Text & ".xlsx"

        Dim qslCloud As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\database_DailyLoadLedger\" & yr.Text & "\" & mm.Text & ""
        Dim qslCloudtoupload As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\database_DailyLoadLedger\" & yr.Text & "\" & mm.Text & "\" & myM.Text & myday2.Text & yr.Text & ".xlsx"

        Try
            If System.IO.Directory.Exists("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\database_DailyLoadLedger") Then
                '=====================
                'QSL CLOUD
                '=====================
                If System.IO.Directory.Exists(qslCloud) Then
                    If System.IO.File.Exists(qslCloudtoupload) Then
                        My.Computer.FileSystem.DeleteFile(qslCloudtoupload)
                        My.Computer.FileSystem.CopyFile(DownloadPath, qslCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                    Else
                        My.Computer.FileSystem.CopyFile(DownloadPath, qslCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                    End If

                ElseIf Not System.IO.Directory.Exists(qslCloud) Then
                    System.IO.Directory.CreateDirectory(qslCloud)
                    My.Computer.FileSystem.CopyFile(DownloadPath, qslCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                Else
                    MsgBox("Could not find haha" & vbCrLf & "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database", vbExclamation)
                End If

            ElseIf System.IO.Directory.Exists("C:\Users\BRYNER\OneDrive\dailysellout_database\database_DailyLoadLedger") Then
                '=====================
                'BRYNER CLOUD
                '=====================
                If System.IO.Directory.Exists(BrynerCloud) Then
                    If System.IO.File.Exists(BrynerCloudtoupload) Then
                        My.Computer.FileSystem.DeleteFile(BrynerCloudtoupload)
                        My.Computer.FileSystem.CopyFile(DownloadPath, BrynerCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                    Else
                        My.Computer.FileSystem.CopyFile(DownloadPath, BrynerCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                    End If
                ElseIf Not System.IO.Directory.Exists(BrynerCloud) Then
                    System.IO.Directory.CreateDirectory(BrynerCloud)
                    My.Computer.FileSystem.CopyFile(DownloadPath, BrynerCloudtoupload, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                Else
                    MsgBox("Could not find haha" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database", vbExclamation)
                End If
            End If


        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try

    End Sub

    Private Sub btn_home_Click(sender As Object, e As EventArgs) Handles btn_home.Click
        TabControl1.SelectedTab = tab_home
    End Sub

    Private Sub btn_sellout_Click(sender As Object, e As EventArgs) Handles btn_sellout.Click
        TabControl1.SelectedTab = tab_sellout
    End Sub

    Private Sub btn_graph_Click(sender As Object, e As EventArgs) Handles btn_graph.Click
        TabControl1.SelectedTab = tab_graph
    End Sub

    Private Sub btn_database_Click(sender As Object, e As EventArgs) Handles btn_database.Click
        TabControl1.SelectedTab = tab_db

    End Sub

    Private Sub btn_settings_Click(sender As Object, e As EventArgs) Handles btn_settings.Click
        TabControl1.SelectedTab = tab_settings
    End Sub

    Private Sub btn_about_Click(sender As Object, e As EventArgs) Handles btn_about.Click
        TabControl1.SelectedTab = tab_about
    End Sub

    Private Sub btn_exit2_Click(sender As Object, e As EventArgs) Handles btn_exit2.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is tab_home Then

            btn_home.BackColor = Color.FromArgb(60, 60, 60)

            btn_sellout.BackColor = Color.FromArgb(30, 30, 30)
            btn_graph.BackColor = Color.FromArgb(30, 30, 30)
            btn_database.BackColor = Color.FromArgb(30, 30, 30)
            btn_settings.BackColor = Color.FromArgb(30, 30, 30)
            btn_about.BackColor = Color.FromArgb(30, 30, 30)

        ElseIf TabControl1.SelectedTab Is tab_sellout Then

            btn_sellout.BackColor = Color.FromArgb(60, 60, 60)

            btn_home.BackColor = Color.FromArgb(30, 30, 30)
            btn_graph.BackColor = Color.FromArgb(30, 30, 30)
            btn_database.BackColor = Color.FromArgb(30, 30, 30)
            btn_settings.BackColor = Color.FromArgb(30, 30, 30)
            btn_about.BackColor = Color.FromArgb(30, 30, 30)

        ElseIf TabControl1.SelectedTab Is tab_graph Then

            btn_graph.BackColor = Color.FromArgb(60, 60, 60)

            btn_home.BackColor = Color.FromArgb(30, 30, 30)
            btn_sellout.BackColor = Color.FromArgb(30, 30, 30)
            btn_database.BackColor = Color.FromArgb(30, 30, 30)
            btn_settings.BackColor = Color.FromArgb(30, 30, 30)
            btn_about.BackColor = Color.FromArgb(30, 30, 30)

        ElseIf TabControl1.SelectedTab Is tab_db Then

            btn_database.BackColor = Color.FromArgb(60, 60, 60)

            btn_home.BackColor = Color.FromArgb(30, 30, 30)
            btn_sellout.BackColor = Color.FromArgb(30, 30, 30)
            btn_graph.BackColor = Color.FromArgb(30, 30, 30)
            btn_settings.BackColor = Color.FromArgb(30, 30, 30)
            btn_about.BackColor = Color.FromArgb(30, 30, 30)

        ElseIf TabControl1.SelectedTab Is tab_settings Then

            btn_settings.BackColor = Color.FromArgb(60, 60, 60)

            btn_home.BackColor = Color.FromArgb(30, 30, 30)
            btn_sellout.BackColor = Color.FromArgb(30, 30, 30)
            btn_graph.BackColor = Color.FromArgb(30, 30, 30)
            btn_database.BackColor = Color.FromArgb(30, 30, 30)
            btn_about.BackColor = Color.FromArgb(30, 30, 30)

        ElseIf TabControl1.SelectedTab Is tab_about Then

            btn_about.BackColor = Color.FromArgb(60, 60, 60)

            btn_home.BackColor = Color.FromArgb(30, 30, 30)
            btn_sellout.BackColor = Color.FromArgb(30, 30, 30)
            btn_graph.BackColor = Color.FromArgb(30, 30, 30)
            btn_database.BackColor = Color.FromArgb(30, 30, 30)
            btn_settings.BackColor = Color.FromArgb(30, 30, 30)
        End If
    End Sub




    '========================================================
    '===================================
    '===================================================================
    'DATABASE
    '=====================================================================
    '==================================
    '========================================================
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
        Try
            Dim a As Long
            Dim b As Long
            Dim sum As Long

            a = dbtb1.Text
            b = dbtbbalawang.Text
            sum = b - a


            Dim output As String = sum.ToString("N0")
            dbtbexcessbalawang.Text = output
        Catch ex As Exception

        End Try
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
    Private Sub dbtbbalawangko()
        If Me.dbtbbalawang.Text.Contains(",") Or Me.dbtbbalawang.Text.Contains(".") Then
            dbtbbalawang.Text = dbtbbalawang.Text.Replace(",", "").Trim()
            dbtbbalawang.Text = dbtbbalawang.Text.Replace(".", "").Trim()
            'MessageBox.Show("Ops")
        Else
            Dim a1 As Decimal
            a1 = Val(dbtbbalawang.Text)
            Dim eto1 As String = a1.ToString("N0")
            dbbalawanglbl.Text = eto1
        End If

        If dbtbbalawang.Text = "" Then
            dbbalawanglbl.Text = "--"
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
        'ExportDialog.ShowDialog()
        ExportSellout.ShowDialog()
    End Sub

    Private Sub dbbtnviewbymonth_Click(sender As Object, e As EventArgs) Handles dbbtnviewbymonth.Click
        Try
            If System.IO.Directory.Exists("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records") Then
                '=====================
                'QSL CLOUD
                '=====================
                Process.Start("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + dby.Text + "\" + dbMMMM.Text + "\sellout" + dbmm.Text + dbdd.Text + dby.Text + ".txt")

            ElseIf System.IO.Directory.Exists("C:\Users\BRYNER\OneDrive\dailysellout_database\records") Then
                '=====================
                'BRYNER CLOUD
                '=====================
                Process.Start("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + dby.Text + "\" + dbMMMM.Text + "\sellout" + dbmm.Text + dbdd.Text + dby.Text + ".txt")
            End If


        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try

    End Sub

    Private Sub dbbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbbtndelete.Click
        '=========================================
        'DELETE DAILY LOAD LEDGER
        '=========================================
        Dim txtmonth As String
        txtmonth = Format(dbDateTimePicker1.Value, "MM")
        Dim txtday As String
        txtday = Format(dbDateTimePicker1.Value, "dd")
        Dim monthmonth As String
        monthmonth = Format(dbDateTimePicker1.Value, "MMMM")
        Dim yryr As String
        yryr = Format(dbDateTimePicker1.Value, "yyyy")
        Dim filename = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\database_DailyLoadLedger\" & yryr & "\" & monthmonth & "\" & txtmonth & txtday & yryr & ".xlsx"
        '=========================================
        '=========================================

        '=========================================
        'DELETE DAILY SELLOUT RECORD
        '=========================================

        Dim pathdailyselloutrecord As String
        pathdailyselloutrecord = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" & yryr & "\" & monthmonth & "\sellout" & txtmonth & txtday & yryr & ".txt"
        '=========================================
        '=========================================

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


                    '=========================================
                    'DELETE DAILY LOAD LEDGER
                    '=========================================
                    If System.IO.File.Exists(filename) Then
                        System.IO.File.Delete(filename)
                    Else
                        MessageBox.Show(filename)
                    End If
                    '=========================================
                    '=========================================



                    '=========================================
                    'DELETE DAILY SELLOUT RECORD
                    '=========================================
                    If System.IO.File.Exists(pathdailyselloutrecord) Then
                        System.IO.File.Delete(pathdailyselloutrecord)
                    Else
                        MessageBox.Show(pathdailyselloutrecord)
                    End If
                    '=========================================
                    '=========================================

                    MsgBox("Record has been deleted!")
                    edit_Myconnection.Close()
                    ' Call btnupdate_Click(sender, e)






                    DataGridView2.DataSource = Nothing
                    Monthandyear()
                    DataGridView2.Columns("ID").Visible = False
                    DataGridView2.Columns("PAR").Visible = False
                    DataGridView2.Columns("MTDRECRUITMENTSMART").Visible = False
                    DataGridView2.Columns("MTDRECRUITMENTSUN").Visible = False
                    DataGridView2.Columns("RECRUITMENTSMART").Visible = False
                    DataGridView2.Columns("RECRUITMENTSUN").Visible = False
                    DataGridView1.DataSource = Nothing
                    iloadmoto()
                    'iloadmoto2()
                    cleartextfields()
                    dbLabel9.Text = ""
                    uploaddatatoCloud()
                    dailyreport()
                    'DELETE RECORD FILE

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

    Private Sub database_loadform()
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

        DataGridView2.RowHeadersVisible = False
        Monthandyear()



        DataGridView2.Columns("ID").Visible = False
        DataGridView2.Columns("PAR").Visible = False
        DataGridView2.Columns("MTDRECRUITMENTSMART").Visible = False
        DataGridView2.Columns("MTDRECRUITMENTSUN").Visible = False
        DataGridView2.Columns("RECRUITMENTSMART").Visible = False
        DataGridView2.Columns("RECRUITMENTSUN").Visible = False

        DataGridView2.Columns(6).ValueType = GetType(Double) ' or Integer, Single, etc.
        DataGridView2.Columns(6).DefaultCellStyle.Format = "N0" ' N(zero) for no digits to the right of the decimal mark

        DataGridView2.Columns(2).ValueType = GetType(Double) ' or Integer, Single, etc.
        DataGridView2.Columns(2).DefaultCellStyle.Format = "N0" ' N(zero) for no digits to the right of the decimal mark
        'DataGridView2.Columns(1).DefaultCellStyle.Format = "dd.MMMM.yyyy"
    End Sub

    Private Sub enable_disable_input_CheckedChanged(sender As Object, e As EventArgs) Handles enable_disable_input.CheckedChanged
        If enable_disable_input.CheckState = CheckState.Checked Then
            smartload.ReadOnly = False
            open.Enabled = False
            smartload.Select()
            smartload.SelectAll()
        Else
            smartload.ReadOnly = True
            open.Enabled = True
        End If
    End Sub

    Private Sub set_target_Click(sender As Object, e As EventArgs) Handles set_target.Click
        targetset.ShowDialog()
    End Sub

    Private Sub CheckBox1_Mock_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1_Mock.CheckedChanged
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
        con.Open()

        If CheckBox1_Mock.CheckState = CheckState.Checked Then

            Dim cmd_mock As New OleDb.OleDbCommand("select count(*) from MockTarget where M_Month = '" & mm.Text & "' AND M_Year = '" & yr.Text & "' ", con)
            Dim count_mock As Int32 = CInt(cmd_mock.ExecuteScalar)
            If count_mock > 0 Then
                '  con.Open()
                Dim ds As New DataSet
                Dim dt As New DataTable
                ds.Tables.Add(dt)
                Dim da As New OleDbDataAdapter
                da = New OleDbDataAdapter("select * from MockTarget where M_Month = '" & mm.Text & "' AND M_Year = '" & yr.Text & "' ", con)
                da.Fill(dt)
                smarttar.Text = dt.Rows(0).Item(1)
                suntar.Text = dt.Rows(0).Item(2)

                btn1.BackColor = Color.Red
                btn1.ForeColor = Color.White
                unofficialtarget.Text = "" & vbCrLf & vbCrLf & "*UNOFFICIAL TARGET*"
            Else
                smarttar.Clear()
                suntar.Clear()
                btn1.BackColor = Color.WhiteSmoke
                btn1.ForeColor = Color.Black

            End If
        Else
            unofficialtarget.Text = "" & vbCrLf & vbCrLf & ""
            Dim cmd As New OleDb.OleDbCommand("select count(*) from target where Tmonth = '" & mm.Text & "' AND Tyear = '" & yr.Text & "' ", con)
            Dim count As Int32 = CInt(cmd.ExecuteScalar)
            If count > 0 Then

                Dim ds As New DataSet
                Dim dt As New DataTable
                ds.Tables.Add(dt)
                Dim da As New OleDbDataAdapter
                da = New OleDbDataAdapter("select * from target where Tmonth = '" & mm.Text & "' AND Tyear = '" & yr.Text & "' ", con)
                da.Fill(dt)
                smarttar.Text = dt.Rows(0).Item(1)
                suntar.Text = dt.Rows(0).Item(2)

                btn1.BackColor = Color.Red
                btn1.ForeColor = Color.White

            Else
                smarttar.Clear()
                suntar.Clear()
                btn1.BackColor = Color.WhiteSmoke
                btn1.ForeColor = Color.Black

            End If

        End If






        con.Close()
    End Sub





    Private Sub sTar1_TextChanged(sender As Object, e As EventArgs) Handles sTar1.TextChanged
        lblDisplay1.Text = sTar1.Text
    End Sub

    Private Sub lbl1_TextChanged(sender As Object, e As EventArgs) Handles lbl1.TextChanged
        lblDisplay2.Text = lbl1.Text
    End Sub

    Private Sub minimize_Click(sender As Object, e As EventArgs) Handles minimize.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub



    Private Sub DataGridView2_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged
        getsum()
        getcount()
        getaverage()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Try

            Dim i As Integer

            For i = 0 To DataGridView1.Columns.Count - 1

                DataGridView2.Columns.Item(i).SortMode = DataGridViewColumnSortMode.Programmatic

            Next i


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

            If CheckBox2_Mock.CheckState = CheckState.Checked Then
                database_unofficialtarget()
            Else
                database_officialtarget()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub database_officialtarget()
        '===================
        'CLEAR ALL TEXTS
        '===================
        dblbltargetcatcher1.Text = "--"
        dblbltargetcatcher2.Text = "--"
        dblblperc.Text = "--"
        dblblach.Text = "--"
        dblbll1target.Text = "--"
        dblblrunrate.Text = "--"
        dbdeficit.Text = ""
        dbdeficit_overtarget.Text = ""

        '======================
        'DATE
        '======================
        Dim datenamed As String

        datenamed = Format(dbDateTimePicker1.Value, "MMMM  d, yyyy")

        '======================
        'KUHA TARGET
        '======================
        Dim dbmonthcatcher As String
        dbmonthcatcher = Format(dbDateTimePicker1.Value, "MMMM")

        Dim dbyearcatcher As String
        dbyearcatcher = Format(dbDateTimePicker1.Value, "yyyy")

        Dim dbsmartcatcher As String
        Dim dbsuncatcher As String


        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
        con.Open()

        Dim cmd As New OleDb.OleDbCommand("select count(*) from target where Tmonth = '" & dbmonthcatcher & "' AND Tyear = '" & dbyearcatcher & "' ", con)
        Dim count As Int32 = CInt(cmd.ExecuteScalar)

        If count > 0 Then
            '  con.Open()
            Dim ds As New DataSet
            Dim dt As New DataTable
            ds.Tables.Add(dt)
            Dim da As New OleDbDataAdapter
            da = New OleDbDataAdapter("select * from target where Tmonth = '" & dbmonthcatcher & "' AND Tyear = '" & dbyearcatcher & "' ", con)
            da.Fill(dt)



            dbsmartcatcher = dt.Rows(0).Item(1)
            dbsuncatcher = dt.Rows(0).Item(2)


            '======================
            'percentage ng MTDactual / target
            '======================
            Dim dbsmartcatcherNumber As Long
            Dim dbsuncatcherNumber As Long

            dbsmartcatcherNumber = Convert.ToInt64(dbsmartcatcher)
            dbsuncatcherNumber = Convert.ToInt64(dbsuncatcher)
            dblbltargetcatcher1.Text = dbsmartcatcherNumber.ToString("N0")
            dblbltargetcatcher2.Text = dbsuncatcherNumber.ToString("N0")


            Dim dbperc As Decimal


            dbperc = dbtb1.Text / dbsmartcatcherNumber * 100
            dblblperc.Text = dbperc.ToString("N2")


            Dim ach As Decimal

            ach = dbperc - dbtb9.Text
            dblblach.Text = ach.ToString("N2")

            '======================
            'l1 target
            '======================
            Dim dbCurrentMonthDays As Int16 = DateTime.DaysInMonth(dbDateTimePicker1.Value.Year, dbDateTimePicker1.Value.Month)
            Dim dbnumdays As String

            dbnumdays = dbCurrentMonthDays

            Dim l1target As Long

            l1target = dbsmartcatcherNumber / dbnumdays
            dblbll1target.Text = l1target.ToString("N0")

            '======================
            'RUN RATE
            '======================
            Dim runrate As Decimal

            runrate = dblbl5.Text / l1target * 100
            dblblrunrate.Text = runrate.ToString("N2")


            '======================
            'DEFICIT/OVER TARGET
            '======================
            Dim deficit_overtarget As String

            Dim deficit As Long
            deficit = dbtb1.Text - dbsmartcatcherNumber
            dbdeficit.Text = deficit.ToString("N0")

            If deficit <= 0 Then
                deficit_overtarget = "Deficit : "
            Else

                deficit_overtarget = "Over Target : "

            End If

            dbdeficit_overtarget.Text = deficit_overtarget

        Else

        End If


        con.Close()


        RichTextBox1.Text = "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datenamed & vbCrLf & "PAR: " & dbtb9.Text & "%" & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & dblbl5.Text & vbCrLf & "-------" & vbCrLf & vbCrLf & "L1 Actual: " & dblbl5.Text & vbCrLf & "L1 Target: " & dblbll1target.Text & vbCrLf & "& Ach : " & dblblrunrate.Text & " %" & vbCrLf & vbCrLf & "MTD" & vbCrLf & "Target / Actual / % Ach" & vbCrLf & "Smart load : " & dblbltargetcatcher1.Text & " / " & dblbl1.Text & " / " & dblblperc.Text & " % ( " & dblblach.Text & "% )" & vbCrLf & "__________________________" & vbCrLf & vbCrLf & dbdeficit_overtarget.Text & dbdeficit.Text & ""
    End Sub

    Private Sub database_unofficialtarget()
        '===================
        'CLEAR ALL TEXTS
        '===================
        dblbltargetcatcher1.Text = "--"
        dblbltargetcatcher2.Text = "--"
        dblblperc.Text = "--"
        dblblach.Text = "--"
        dblbll1target.Text = "--"
        dblblrunrate.Text = "--"
        dbdeficit.Text = ""
        dbdeficit_overtarget.Text = ""

        '======================
        'DATE
        '======================
        Dim datenamed As String

        datenamed = Format(dbDateTimePicker1.Value, "MMMM  d, yyyy")

        '======================
        'KUHA TARGET
        '======================
        Dim dbmonthcatcher As String
        dbmonthcatcher = Format(dbDateTimePicker1.Value, "MMMM")

        Dim dbyearcatcher As String
        dbyearcatcher = Format(dbDateTimePicker1.Value, "yyyy")

        Dim dbsmartcatcher As String
        Dim dbsuncatcher As String


        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
        con.Open()

        Dim cmd As New OleDb.OleDbCommand("select count(*) from MockTarget where M_Month = '" & dbmonthcatcher & "' AND M_Year = '" & dbyearcatcher & "' ", con)
        Dim count As Int32 = CInt(cmd.ExecuteScalar)

        If count > 0 Then
            '  con.Open()
            Dim ds As New DataSet
            Dim dt As New DataTable
            ds.Tables.Add(dt)
            Dim da As New OleDbDataAdapter
            da = New OleDbDataAdapter("select * from MockTarget where M_Month = '" & dbmonthcatcher & "' AND M_Year = '" & dbyearcatcher & "' ", con)
            da.Fill(dt)



            dbsmartcatcher = dt.Rows(0).Item(1)
            dbsuncatcher = dt.Rows(0).Item(2)


            '======================
            'percentage ng MTDactual / target
            '======================
            Dim dbsmartcatcherNumber As Long
            Dim dbsuncatcherNumber As Long

            dbsmartcatcherNumber = Convert.ToInt64(dbsmartcatcher)
            dbsuncatcherNumber = Convert.ToInt64(dbsuncatcher)
            dblbltargetcatcher1.Text = dbsmartcatcherNumber.ToString("N0")
            dblbltargetcatcher2.Text = dbsuncatcherNumber.ToString("N0")


            Dim dbperc As Decimal


            dbperc = dbtb1.Text / dbsmartcatcherNumber * 100
            dblblperc.Text = dbperc.ToString("N2")


            Dim ach As Decimal

            ach = dbperc - dbtb9.Text
            dblblach.Text = ach.ToString("N2")

            '======================
            'l1 target
            '======================
            Dim dbCurrentMonthDays As Int16 = DateTime.DaysInMonth(dbDateTimePicker1.Value.Year, dbDateTimePicker1.Value.Month)
            Dim dbnumdays As String

            dbnumdays = dbCurrentMonthDays

            Dim l1target As Long

            l1target = dbsmartcatcherNumber / dbnumdays
            dblbll1target.Text = l1target.ToString("N0")

            '======================
            'RUN RATE
            '======================
            Dim runrate As Decimal

            runrate = dblbl5.Text / l1target * 100
            dblblrunrate.Text = runrate.ToString("N2")


            '======================
            'DEFICIT/OVER TARGET
            '======================
            Dim deficit_overtarget As String

            Dim deficit As Long
            deficit = dbtb1.Text - dbsmartcatcherNumber
            dbdeficit.Text = deficit.ToString("N0")

            If deficit <= 0 Then
                deficit_overtarget = "Deficit : "
            Else

                deficit_overtarget = "Over Target : "

            End If

            dbdeficit_overtarget.Text = deficit_overtarget


        Else

        End If


        con.Close()
        Dim unofftxt As String
        If CheckBox2_Mock.CheckState = CheckState.Checked Then
            unofftxt = "" & vbCrLf & vbCrLf & "*UNOFFICIAL TARGET*"
        Else
            unofftxt = "" & vbCrLf & vbCrLf & ""
        End If


        RichTextBox1.Text = "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datenamed & vbCrLf & "PAR: " & dbtb9.Text & "%" & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & dblbl5.Text & vbCrLf & "-------" & vbCrLf & vbCrLf & "L1 Actual: " & dblbl5.Text & vbCrLf & "L1 Target: " & dblbll1target.Text & vbCrLf & "& Ach : " & dblblrunrate.Text & " %" & vbCrLf & vbCrLf & "MTD" & vbCrLf & "Target / Actual / % Ach" & vbCrLf & "Smart load : " & dblbltargetcatcher1.Text & " / " & dblbl1.Text & " / " & dblblperc.Text & " % ( " & dblblach.Text & "% )" & vbCrLf & "__________________________" & vbCrLf & vbCrLf & dbdeficit_overtarget.Text & dbdeficit.Text & unofftxt & ""
    End Sub

    Private Sub MTD_info_Click(sender As Object, e As EventArgs) Handles MTD_info.Click


        DateTimePicker2.Value = aomonth.Text


        Dim noofdays As Integer = DateTime.DaysInMonth(DateTimePicker2.Value.Year, DateTimePicker2.Value.Month)
        Dim days As Integer = Format(DateTimePicker2.Value, "dd")


        '===================
        'Trending
        '===================
        Dim trend As Long = lbl1.Text
        Dim sum As Long
        sum = trend / days * noofdays
        Dim valuetrend As Integer = Integer.Parse(sum, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowThousands)
        Dim trend1 As String = valuetrend.ToString("N0")
        '===================
        'Daily Avg
        '===================
        Dim dailyavgcomp As Long
        dailyavgcomp = trend / days
        Dim valuedailyavg As Integer = Integer.Parse(dailyavgcomp, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowThousands)
        Dim dailyavg1 As String = valuedailyavg.ToString("N0")

        '===================
        'Run Rate
        '===================
        Dim runrateCompute As Decimal
        Dim runrate As String
        Dim runrateTEXT As String

        If sTar1.Text = "--" Then
            'Do nothing
            runrateTEXT = ""
        Else
            'Compute here
            runrateCompute = trend1 / sTar1.Text * 100
            runrate = runrateCompute.ToString("N2")

            runrateTEXT = "" & vbCrLf & " Run Rate : " & runrate & "%"
        End If


        '===================
        'L1 Target
        '===================
        Dim l1_targetCompute As Long
        Dim l1_target As String
        Dim l1_targetText As String
        Dim l1_targetConvert As Integer
        If sTar1.Text = "--" Then
            'Do nothing
            l1_targetText = ""
        Else
            'Compute here
            l1_targetCompute = sTar1.Text / noofdays
            l1_targetConvert = Integer.Parse(l1_targetCompute, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowThousands)
            l1_target = l1_targetConvert.ToString("N0")
            l1_targetText = "" & vbCrLf & " L1 Target : " & l1_target & ""
        End If

        Dim ask As MsgBoxResult = MsgBox(" Daily Avg. : " & dailyavg1 & vbCrLf & "  Trending : " & trend1 & runrateTEXT & l1_targetText & "", MsgBoxStyle.OkOnly)

    End Sub

    Private Sub showdetails_Click(sender As Object, e As EventArgs) Handles showdetails.Click

        If showdetails.Text = "Show" Then
            RichTextBox1.Visible = True
            DataGridView2.Width = 427
            showdetails.Text = "Hide"

        Else
            RichTextBox1.Visible = False
            DataGridView2.Width = 790
            showdetails.Text = "Show"
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub OneDriveCreateRecords()
        Dim BrynerCloud As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\records\" & yr.Text & "\" & mm.Text & ""
        Dim qslCloud As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" & yr.Text & "\" & mm.Text & ""

        Try
            If System.IO.Directory.Exists("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records") Then
                '=====================
                'QSL CLOUD
                '=====================
                If System.IO.Directory.Exists(qslCloud) Then

                    'CREATE RECORD FILE

                    qslonedriveCreateRecord()

                ElseIf Not System.IO.Directory.Exists(qslCloud) Then

                    'CREATE DIRECTORY
                    'CREATE RECORD FILE

                    qslonedriveCreateRecord()

                    'MsgBox("Could not find haha" & vbCrLf & "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database", vbExclamation)
                End If

            ElseIf System.IO.Directory.Exists("C:\Users\BRYNER\OneDrive\dailysellout_database\records") Then
                '=====================
                'BRYNER CLOUD
                '=====================
                If System.IO.Directory.Exists(BrynerCloud) Then
                    'CREATE RECORD FILE

                    bryonedriveCreateRecord()

                ElseIf Not System.IO.Directory.Exists(BrynerCloud) Then
                    'CREATE DIRECTORY
                    'CREATE RECORD FILE

                    bryonedriveCreateRecord()

                    'MsgBox("Could not find haha" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database", vbExclamation)
                End If
            End If

            '=======================================
            'OPEN PROCESS PER QSL AND BRY
            '=======================================
            If System.IO.Directory.Exists("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records") Then
                '=====================
                'QSL CLOUD
                '=====================

                Process.Start("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt")

            ElseIf System.IO.Directory.Exists("C:\Users\BRYNER\OneDrive\dailysellout_database\records") Then
                '=====================
                'BRYNER CLOUD
                '=====================

                Process.Start("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt")
            End If



        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try







    End Sub

    Private Sub bryonedriveCreateRecord()
        '==================================================
        'BRY ONE DRIVE CREATE RECORD
        '==================================================

        '======================================
        'TRENDING
        '======================================
        Dim noofdays As Integer = DateTime.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month)
        Dim days As Integer = Format(DateTimePicker1.Value, "dd")
        Dim trend As Long = Label10.Text
        Dim sum As Long
        sum = trend / days * noofdays
        Dim valuetrend As Integer = Integer.Parse(sum, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowThousands)
        Dim trend1 As String = valuetrend.ToString("N0")
        Dim trendingofficial As String
        trendingofficial = "" & vbCrLf & vbCrLf & "Trending: " & trend1 & ""


        '======================================
        'PATH
        '======================================
        Dim directorybry As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\1_2"
        If (Not System.IO.Directory.Exists(directorybry)) Then
            System.IO.Directory.CreateDirectory(directorybry)
        End If

        Dim filebry As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt"
        If Not System.IO.File.Exists(filebry) Then
            System.IO.File.Create(filebry).Dispose()
        End If

        System.IO.File.WriteAllText("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt", "")

        My.Computer.FileSystem.WriteAllText("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt",
            "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datehere.Text & vbCrLf &
            "PAR:  " & res.Text & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & sl1.Text & manual2.Text &
            vbCrLf & "-------" & vbCrLf & vbCrLf &
            "L1 Actual: " & totalloadlbl.Text & vbCrLf & "L1 Target: " & dividetarget.Text & vbCrLf & "% Ach : " & percentageko.Text & vbCrLf &
            vbCrLf &
            "MTD" & vbCrLf & "Target / Actual / % Ach " & vbCrLf & "Smart load : " & sTar1.Text & " / " & Label10.Text &
            " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & "__________________________" & deficitword.Text & deficit.Text & trendingofficial & unofficialtarget.Text & "", True)
        '          "PD Name: Qsl Wireless." & vbCrLf & "Date: " & datehere.Text & "" & vbCrLf &
        '           "PAR: " & res.Text & vbCrLf & "Target / Actual/ % Ach" & vbCrLf & vbCrLf & "DAILY Sell out" _
        '           & vbCrLf & "Smart load : " & sl1.Text & manual2.Text & vbCrLf & vbCrLf &
        '           "Daily Recruitment" & vbCrLf & "Smart : " & Label13.Text & vbCrLf & vbCrLf &
        '          "MTD Sell-out" & vbCrLf & "Smart load :  " _
        '           & sTar1.Text & " / " & Label10.Text & " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & vbCrLf &
        '           "MTD Recruitment" & vbCrLf & "Smart : " & rsmart.Text & vbCrLf &
        '           "________________________" & vbCrLf & vbCrLf & _
        '_


        'Process.Start("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt")

    End Sub
    Private Sub qslonedriveCreateRecord()
        '==================================================
        'QSL ONE DRIVE CREATE RECORD
        '==================================================

        '======================================
        'TRENDING
        '======================================
        Dim noofdays As Integer = DateTime.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month)
        Dim days As Integer = Format(DateTimePicker1.Value, "dd")
        Dim trend As Long = Label10.Text
        Dim sum As Long
        sum = trend / days * noofdays
        Dim valuetrend As Integer = Integer.Parse(sum, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowThousands)
        Dim trend1 As String = valuetrend.ToString("N0")
        Dim trendingofficial As String
        trendingofficial = "" & vbCrLf & vbCrLf & "Trending: " & trend1 & ""


        '======================================
        'DEFICIT
        '======================================
        If CheckBox3.CheckState = CheckState.Checked Then

            Dim actual As Long
            Dim target As Long
            Dim sumdef As Long


            actual = Label10.Text
            target = sTar1.Text
            sumdef = actual - target

            Dim astala As String = sumdef.ToString("N0")
            deficit.Text = astala

            If actual < target Then
                deficitword.Text = "" & vbCrLf & vbCrLf & "Deficit: "
            ElseIf actual >= target Then
                deficitword.Text = "" & vbCrLf & vbCrLf & "Over Target: "
            End If

        Else
            deficitword.Text = ""
            deficit.Text = ""
        End If


        '======================================
        'PATH
        '======================================
        Dim directoryqsl As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\1_2"
        If (Not System.IO.Directory.Exists(directoryqsl)) Then
            System.IO.Directory.CreateDirectory(directoryqsl)
        End If

        Dim fileqsl As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt"
        If Not System.IO.File.Exists(fileqsl) Then
            System.IO.File.Create(fileqsl).Dispose()
        End If

        System.IO.File.WriteAllText("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt", "")

        My.Computer.FileSystem.WriteAllText("C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt",
            "PD Name: Qsl Wireless" & vbCrLf & "Date: " & datehere.Text & vbCrLf &
            "PAR:  " & res.Text & vbCrLf & vbCrLf & "DAILY Sell out" & vbCrLf & "Smart load : " & sl1.Text & manual2.Text &
            vbCrLf & "-------" & vbCrLf & vbCrLf &
            "L1 Actual: " & totalloadlbl.Text & vbCrLf & "L1 Target: " & dividetarget.Text & vbCrLf & "% Ach : " & percentageko.Text & vbCrLf &
            vbCrLf &
            "MTD" & vbCrLf & "Target / Actual / % Ach " & vbCrLf & "Smart load : " & sTar1.Text & " / " & Label10.Text &
            " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & "__________________________" & deficitword.Text & deficit.Text & trendingofficial & unofficialtarget.Text & "", True)
        '          "PD Name: Qsl Wireless." & vbCrLf & "Date: " & datehere.Text & "" & vbCrLf &
        '           "PAR: " & res.Text & vbCrLf & "Target / Actual/ % Ach" & vbCrLf & vbCrLf & "DAILY Sell out" _
        '           & vbCrLf & "Smart load : " & sl1.Text & manual2.Text & vbCrLf & vbCrLf &
        '           "Daily Recruitment" & vbCrLf & "Smart : " & Label13.Text & vbCrLf & vbCrLf &
        '          "MTD Sell-out" & vbCrLf & "Smart load :  " _
        '           & sTar1.Text & " / " & Label10.Text & " / " & resofper.Text & " % ( " & fordef.Text & def.Text & " )" & vbCrLf & vbCrLf &
        '           "MTD Recruitment" & vbCrLf & "Smart : " & rsmart.Text & vbCrLf &
        '           "________________________" & vbCrLf & vbCrLf & _
        '_


        'Process.Start("C:\Users\BRYNER\OneDrive\dailysellout_database\records\" + yr.Text + "\" + mm.Text + "\sellout" + myM.Text + myday2.Text + yr.Text + ".txt")

    End Sub

    Private Sub dbbtnupdate_Click(sender As Object, e As EventArgs) Handles dbbtnupdate.Click
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
                    'DataGridView2.DataSource = Nothing
                    'Form1.iloadmoto2()
                    iloadmotoDB()
                    cleartextfields()
                    uploaddatatoCloud()
                    dailyreport()
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

    Private Sub edit_btnshow_Click(sender As Object, e As EventArgs) Handles edit_btnshow.Click

        If edit_btnshow.Text = "Edit" Then
            dblabelbalawang.Visible = True
            dbtbbalawang.Visible = True
            Label33.Visible = True
            dbtb1.Visible = True
            Label29.Visible = True
            dbtb5.Visible = True
            dbbtnupdate.Visible = True
            btnswap.Visible = True
            dbtbexcessbalawang.Visible = True
            btnexcesscomputer.Visible = True
            edit_btnshow.Text = "Cancel"

        Else
            dblabelbalawang.Visible = False
            dbtbbalawang.Visible = False
            Label33.Visible = False
            dbtb1.Visible = False
            Label29.Visible = False
            dbtb5.Visible = False
            dbbtnupdate.Visible = False
            btnswap.Visible = False
            dbtbexcessbalawang.Visible = False
            btnexcesscomputer.Visible = False
            edit_btnshow.Text = "Edit"
        End If
    End Sub

    Private Sub dbLabel9_TextChanged(sender As Object, e As EventArgs) Handles dbLabel9.TextChanged
        If dbLabel9.Text = "aaa" Then
            dbbtnupdate.Enabled = False

        ElseIf dbLabel9.Text = "" Then
            dbbtnupdate.Enabled = False

        Else
            dbbtnupdate.Enabled = True
        End If
    End Sub




    Private Sub btnswap_Click(sender As Object, e As EventArgs) Handles btnswap.Click
        dbbalawanggaya.Text = dbtbbalawang.Text
        dbtbbalawang.Text = dbtb1.Text
        dbtb1.Text = dbbalawanggaya.Text

        Try
            Dim a As Long
            Dim b As Long
            Dim sum As Long

            a = dbtb1.Text
            b = dbtbbalawang.Text
            sum = b - a


            Dim output As String = sum.ToString("N0")
            dbtbexcessbalawang.Text = output
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dbtbbalawang_TextChanged(sender As Object, e As EventArgs) Handles dbtbbalawang.TextChanged
        Try
            Dim a As Long
            Dim b As Long
            Dim sum As Long

            a = dbtb1.Text
            b = dbtbbalawang.Text
            sum = b - a


            Dim output As String = sum.ToString("N0")
            dbtbexcessbalawang.Text = output
        Catch ex As Exception

        End Try


        dbtbbalawangko()

    End Sub

    Private Sub btnexcesscomputer_Click(sender As Object, e As EventArgs) Handles btnexcesscomputer.Click
        Clipboard.SetText(dbtbexcessbalawang.Text)

    End Sub

    Private Sub lblDisplay1_TextChanged(sender As Object, e As EventArgs) Handles lblDisplay1.TextChanged
        If lblDisplay1.Text = "--" Then
            'do nothing
        Else
            CheckBox3.CheckState = CheckState.Checked
        End If
    End Sub


    Private Sub DataGridView3_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView3.SelectionChanged
        Dim haha As String
        Dim ge As Decimal
        Dim current_row As Integer = DataGridView3.CurrentRow.Index
        Debug.Print(current_row.ToString)



        haha = DataGridView3(0, current_row).Value.ToString
        ge = haha
        homedbtxtbox1.Text = ge.ToString("#,##0")


    End Sub

    Private Sub DataGridView4_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView4.SelectionChanged
        Dim haha As String
        Dim ge As Decimal
        Dim current_row As Integer = DataGridView4.CurrentRow.Index
        Debug.Print(current_row.ToString)



        haha = DataGridView4(0, current_row).Value.ToString
        ge = haha
        homedbtxtbox2.Text = ge.ToString("#,##0")


        Dim awit As Long
        Dim a As Long
        Dim b As Long
        Dim output As String

        a = homedbtxtbox1.Text
        b = homedbtxtbox2.Text
        awit = a - b

        output = awit.ToString("N0")
        txtbox1diff.Text = output



        '============================
        'percentage
        Dim negapasi As String

        Dim percawit As Decimal
        Dim perca As Decimal
        Dim percb As Decimal
        Dim percoutput As String

        perca = txtbox1diff.Text
        percb = homedbtxtbox1.Text
        percawit = perca / percb * 100

        If percawit <= 0 Then
            negapasi = ""
            homedbperc.ForeColor = Color.Red
        Else
            negapasi = "+"
            homedbperc.ForeColor = Color.Green
        End If
        percoutput = percawit.ToString("N2")

        homedbperc.Text = "" & negapasi & "" & percoutput & "%"
    End Sub

    Private Sub btn_directory_settings_Click(sender As Object, e As EventArgs) Handles btn_directory_settings.Click
        qsldirectory.ShowDialog()
    End Sub

    Public Sub comboboxdirectory()

        Try
            dropdown_constring.Open()

            directorydropdown.Items.Clear()
            Dim cmd As New OleDbCommand
            cmd.CommandText = "select * from qsldirectory order by ID asc"
            cmd.Connection = dropdown_constring
            dr = cmd.ExecuteReader
            While dr.Read
                directorydropdown.Items.Add(dr.GetString(1))

            End While
            dr.Close()
            dropdown_constring.Close()
            Me.directorydropdown.Items.Insert(0, "-Select Directory-")
            Me.directorydropdown.SelectedItem = "-Select Directory-"
        Catch ex As Exception

        End Try

    End Sub

    Public Sub comboboxdirectory_path()

        Dim asa As String
        asa = directorydropdown.Text
        If asa = "-Select Directory" Then
            pathdirectory = "----"
        Else
            Try
                dropdown_constring.Open()


                Dim cmd As New OleDbCommand
                cmd.CommandText = "select * from qsldirectory where dir_name = '" & asa & "'"
                cmd.Connection = dropdown_constring
                dr = cmd.ExecuteReader
                While dr.Read

                    pathdirectory = dr.GetString(2)
                End While
                dr.Close()
                dropdown_constring.Close()

            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub directorydropdown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles directorydropdown.SelectedIndexChanged
        If directorydropdown.Text = "-Select Directory-" Then
            pathdirectory = "----"
        Else
            comboboxdirectory_path()
        End If

    End Sub

    Private Sub btn_gotodir_Click(sender As Object, e As EventArgs) Handles btn_gotodir.Click
        If pathdirectory = "----" Then
            'do nothing
        Else
            Try
                Process.Start(pathdirectory)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

    End Sub

    Private Sub dailyreport()



        Try
            dailyreport_constring.Open()


            Dim cmd1 As New OleDbCommand
            'cmd1.CommandText = "Select * from tblitems where DATE = CDATE('5/22/2022')"
            cmd1.CommandText = "SELECT TOP 1 * FROM tblitems ORDER BY ID DESC"
            'cmd1.CommandText = "SELECT * FROM tblitems WHERE id=(SELECT max(id) FROM tblitems)"
            cmd1.Connection = dailyreport_constring
            daily_dr = cmd1.ExecuteReader
            While daily_dr.Read
                Dim comaconverter1 As Integer
                lbldaily1.Text = daily_dr.GetDateTime(1)
                comaconverter1 = daily_dr.GetInt32(6)
                tbdaily1.Text = comaconverter1.ToString("#,##0")
            End While
            daily_dr.Close()
            dailyreport_constring.Close()


            dailyreport_sub()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dailyreport_sub()
        Try
            dailyreport_constring.Open()
            Dim cmd1 As New OleDbCommand
            'cmd1.CommandText = "Select * from tblitems where DATE = CDATE('5/22/2022')"
            cmd1.CommandText = "SELECT TOP 2 * FROM tblitems ORDER BY ID DESC"
            'cmd1.CommandText = "SELECT * FROM tblitems WHERE id=(SELECT max(id) FROM tblitems)"
            cmd1.Connection = dailyreport_constring
            daily_dr = cmd1.ExecuteReader
            While daily_dr.Read
                Dim comaconverter2 As Integer
                lbldaily2.Text = daily_dr.GetDateTime(1)
                comaconverter2 = daily_dr.GetInt32(6)
                tbdaily2.Text = comaconverter2.ToString("#,##0")

            End While
            daily_dr.Close()
            dailyreport_constring.Close()

            'DIFFERENCE
            Dim a As Long
            Dim b As Long
            Dim sum As Long

            a = tbdaily1.Text
            b = tbdaily2.Text

            sum = a - b

            Dim output As String = sum.ToString("N0")
            tbdaily3.Text = output



            'PERCENTAGE
            Dim negapasi As String
            Dim percawit As Decimal
            Dim perca As Decimal
            Dim percb As Decimal
            Dim percoutput As String

            perca = tbdaily3.Text
            percb = tbdaily1.Text
            percawit = perca / percb * 100

            If percawit <= 0 Then
                negapasi = ""
                lbldailypercentage.ForeColor = Color.Red
            Else
                negapasi = "+"
                lbldailypercentage.ForeColor = Color.Green
            End If
            percoutput = percawit.ToString("N2")

            lbldailypercentage.Text = "" & negapasi & "" & percoutput & "%"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

Imports System.Windows.Forms
Imports System.IO
Imports System.Text


Public Class ExportDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        sellout1()
        sellout2()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
    Private Sub ExportDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        myM.Text = Format(EditForm.dbDateTimePicker1.Value, " M")
        myY.Text = Format(EditForm.dbDateTimePicker1.Value, "yyyy")
        ComboBox2.Items.Clear()
        Dim i As Integer
        For i = 2017 To Today.Year
            ComboBox2.Items.Add(i.ToString)
        Next
        formula1()
        'Me.ComboBox2.Items.Insert(0, "--Year--")
        Me.ComboBox2.SelectedItem = myY.Text

        Me.ComboBox1.SelectedItem = mrM.Text

    End Sub

    Private Sub savedialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles savedialog.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            Dim root As Environment.SpecialFolder = FolderBrowserDialog1.RootFolder
        End If
    End Sub





    Private Sub formula1()
        If myM.Text = 1 Then
            mrM.Text = "December"
        ElseIf myM.Text = 2 Then
            mrM.Text = "January"
        ElseIf myM.Text = 3 Then
            mrM.Text = "February"
        ElseIf myM.Text = 4 Then
            mrM.Text = "March"
        ElseIf myM.Text = 5 Then
            mrM.Text = "April"
        ElseIf myM.Text = 6 Then
            mrM.Text = "May"
        ElseIf myM.Text = 7 Then
            mrM.Text = "June"
        ElseIf myM.Text = 8 Then
            mrM.Text = "July"
        ElseIf myM.Text = 9 Then
            ComboBox1.Text = "August"
        ElseIf myM.Text = 10 Then
            mrM.Text = "September"
        ElseIf myM.Text = 11 Then
            mrM.Text = "October"
        ElseIf myM.Text = 12 Then
            mrM.Text = "November"
        End If
    End Sub

    Private Sub sellout1()

        Try
            LstFiles.Items.Clear()
            Dim akingpath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" & ComboBox2.Text & "\" & ComboBox1.Text & "\1_2\1"
            If (Not System.IO.Directory.Exists(akingpath)) Then
                ' System.IO.Directory.CreateDirectory(akingpath)
                MsgBox("Path does not exist", vbExclamation)
            End If
            Dim files() As String
            files = Directory.GetFiles(akingpath, "*.txt")
            Dim i As Integer = 0
            For i = 0 To files.Length - 1
                LstFiles.Items.Add(files(i))
            Next

            lbl_NoOfFiles.Text = CStr(LstFiles.Items.Count)
            lbl_NoOfFiles.Visible = True
            '=======================================
            Dim minepath1 As String = "" & TextBox1.Text & "\" & ComboBox1.Text & " " & ComboBox2.Text & " Daily Sellout.txt"
            Dim FileReader As StreamReader
            Dim ii As Integer = 0
            Dim temp As String
            System.IO.File.WriteAllText("" & TextBox1.Text & "\" & ComboBox1.Text & " " & ComboBox2.Text & " Daily Sellout.txt", "")
            For ii = 0 To LstFiles.Items.Count - 1
                FileReader = File.OpenText(LstFiles.Items.Item(ii))
                temp = FileReader.ReadToEnd
                File.AppendAllText(minepath1, temp)
            Next
            Dim prompt As String
            prompt = String.Concat(lbl_NoOfFiles.Text, " files merged succesfully at " & ComboBox1.Text & " " & ComboBox2.Text & "")
            MsgBox(prompt, MsgBoxStyle.Information, "Merge")
            LstFiles.Items.Clear()
            lbl_NoOfFiles.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub sellout2()

        Try
            LstFiles2.Items.Clear()
            Dim akingpath As String = "C:\Users\Bryner\Documents\Visual Studio 2010\Projects\Daily Sellout\records\" & ComboBox2.Text & "\" & ComboBox1.Text & "\1_2\2"
            If (Not System.IO.Directory.Exists(akingpath)) Then
                ' System.IO.Directory.CreateDirectory(akingpath)
                MsgBox("Path does not exist", vbExclamation)
            End If
            Dim files() As String
            files = Directory.GetFiles(akingpath, "*.txt")
            Dim i As Integer = 0
            For i = 0 To files.Length - 1
                LstFiles2.Items.Add(files(i))
            Next

            lbl_NoOfFiles2.Text = CStr(LstFiles2.Items.Count)
            lbl_NoOfFiles2.Visible = True
            '=======================================
            Dim minepath1 As String = "" & TextBox1.Text & "\Sir Molong " & ComboBox1.Text & " " & ComboBox2.Text & " Daily Sellout.txt"
            Dim FileReader As StreamReader
            Dim ii As Integer = 0
            Dim temp As String
            System.IO.File.WriteAllText("" & TextBox1.Text & "\Sir Molong " & ComboBox1.Text & " " & ComboBox2.Text & " Daily Sellout.txt", "")
            For ii = 0 To LstFiles2.Items.Count - 1
                FileReader = File.OpenText(LstFiles2.Items.Item(ii))
                temp = FileReader.ReadToEnd
                File.AppendAllText(minepath1, temp)
            Next
            Dim prompt As String
            prompt = String.Concat(lbl_NoOfFiles2.Text, " files merged succesfully on Sir Molong")
            MsgBox(prompt, MsgBoxStyle.Information, "Merge")
            LstFiles2.Items.Clear()
            lbl_NoOfFiles2.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class

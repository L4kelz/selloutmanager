Public Class loadup


    Dim SourcePath As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb"
    Dim SourcePath2 As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\Items.accdb"

    Dim forupload As String = "C:\Users\BRYNER\OneDrive\dailysellout_database"
    Dim forupload2 As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database"

    Dim brynerdatabase As String = "c:\Users\BRYNER\Documents\Visual Studio 2010\Projects\Daily Sellout\Daily Sellout\bin\Debug\Items.accdb"
    Dim bryneronedrive As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb"
    Dim qslwirelessonedrive As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\Items.accdb"





    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub cloudload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cloudload.Click

        Try

            If System.IO.File.Exists(SourcePath) Then
                My.Computer.FileSystem.DeleteFile(brynerdatabase)
                My.Computer.FileSystem.MoveFile(bryneronedrive, brynerdatabase)
                MsgBox("Success!", vbInformation)
                Form1.iloadmoto()
            ElseIf System.IO.File.Exists(SourcePath2) Then
                My.Computer.FileSystem.DeleteFile(brynerdatabase)
                My.Computer.FileSystem.MoveFile(qslwirelessonedrive, brynerdatabase)
                MsgBox("Success!", vbInformation)
                Form1.iloadmoto()
            Else
                MsgBox("Could not find" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database\Items.accdb", vbExclamation)
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try


    End Sub
    Private Sub cloadupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cloadupload.Click
        Dim AppDirectory As String = "C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\records"
        Dim AppDirectory2 As String = "C:\Users\BRYNER\Documents\Visual Studio 2010\projects\Daily Sellout\records"

        Dim uploadDirectoryPath As String = "C:\Users\BRYNER\OneDrive\dailysellout_database\records"
        Dim uploadDirectoryPath2 As String = "C:\Users\QSL WIRELESS\OneDrive\dailysellout_database\records"

        Try
            If System.IO.Directory.Exists(uploadDirectoryPath) Then
                'My.Computer.FileSystem.CopyFile(brynerdatabase, bryneronedrive)
                My.Computer.FileSystem.CopyDirectory(AppDirectory, uploadDirectoryPath)
                'MsgBox("Success!", vbInformation)
            ElseIf System.IO.Directory.Exists(uploadDirectoryPath2) Then
                My.Computer.FileSystem.CopyDirectory(AppDirectory2, uploadDirectoryPath2)
                'My.Computer.FileSystem.CopyFile(brynerdatabase, qslwirelessonedrive)
                'MsgBox("Success!", vbInformation)
            Else
                MsgBox("Could not find" & vbCrLf & "C:\Users\BRYNER\OneDrive\dailysellout_database\records", vbExclamation)
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox(ex.Message, vbExclamation)
        End Try
    End Sub


End Class
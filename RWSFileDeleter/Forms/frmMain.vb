Imports System.IO
Imports RWSFileDeleter.CustomFile

Public Class frmMain
    Public folderPath As String
    Public searchPattern As String = "*.rws"
    Public count As Long = 0

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title & " Version " & My.Application.Info.Version.ToString
        TextBox1.Text = "Browse the folder"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim file As String = ofd(OpenFileDialog1)
        If Not String.IsNullOrEmpty(file) Then
            folderPath = IO.Path.GetDirectoryName(file)
            TextBox1.Text = folderPath
            count = 0

            If Not BackgroundWorker1.IsBusy Then
                BackgroundWorker1.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim files As String()

        files = Directory.GetFiles(folderPath, searchPattern)
        count = count + DeleteFiles(files)

        For Each subfolder As String In Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories)
            files = Directory.GetFiles(subfolder, searchPattern)
            count = count + DeleteFiles(files)
        Next
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If count > 0 Then
            STS(ListBox1, Now & vbTab & count & " files deleted.")
            STS(ListBox1, Now & vbTab & "All file(s) with extension of (" & searchPattern & ") in the folder " & folderPath & " has been deleted.")
        Else
            STS(ListBox1, Now & vbTab & "No files deleted.")
        End If
    End Sub

    Public Function DeleteFiles(ByVal files As String()) As Long
        Dim cnt As Long = 0
        Dim cf As New CustomFile()

        For Each f As String In files
            Try
                STS(ListBox1, Now & vbTab & f)
                cf.DeleteExists(f)
                STS(ListBox1, Now & vbTab & "Successfully Deleted.")
                cnt = cnt + 1
            Catch ex As Exception
                STS(ListBox1, ex.Message)
            End Try
        Next
        Return cnt
    End Function

    Public Function GetLastPathSegment(ByVal Path As String)
        Return Path.Split(New String() {"\"}, StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
    End Function

End Class
Imports System.IO

Public Class CustomFile
    Public Function DeleteExists(ByVal filename As String)
        If File.Exists(filename) Then
            File.Delete(filename)
        End If
        Return filename
    End Function
End Class

Module modHelpers

    Delegate Sub Set_Delegate(ByVal [ListBox] As ListBox, ByVal [Text] As String)

    Public Sub STS(ByVal [ListBox] As ListBox, ByVal [Text] As String)
        If [ListBox].InvokeRequired Then
            Dim MyDelegate As New Set_Delegate(AddressOf STS)
            [ListBox].Invoke(MyDelegate, New Object() {[ListBox], [Text]})
        Else
            [ListBox].Items.Add([Text])
            [ListBox].TopIndex = [ListBox].Items.Count - 1
        End If
    End Sub


    Public Function ofd(ByRef ofDialog As OpenFileDialog) As String
        ofDialog.ValidateNames = False
        ofDialog.CheckFileExists = False
        ofDialog.CheckPathExists = True
        ofDialog.FileName = "Select Folder"

        If ofDialog.ShowDialog() = DialogResult.OK Then
            Return ofDialog.FileName
        End If
        Return String.Empty
    End Function
End Module

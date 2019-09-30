Namespace com.lib.process

    Public Class LogControl


        Private _LogFile As com.lib.objects.FileObj
        Private _LogText As New Text.StringBuilder

        Sub New(LogFile As com.lib.objects.FileObj)
            _LogFile = LogFile
        End Sub

        Public Sub Add(message As String)
            _LogText.Append(Date.Now.ToString("dd-MM-yyyy hh:mm:ss") & " >> " & message & vbNewLine)
        End Sub

        Public Sub UpdateLog()
            If IO.File.Exists(_LogFile.FullPathFile) Then
                If _LogText.Length > 0 Then
                    Dim sw As IO.StreamWriter = IO.File.AppendText(_LogFile.FullPathFile)
                    sw.WriteLine(_LogText.ToString)
                    _LogText = New Text.StringBuilder
                    sw.Close()
                End If
            Else
                If _LogText.Length > 0 Then
                    Dim textwriter As New com.lib.file.TextFileWriter(_LogFile)
                    textwriter.AddText(_LogText.ToString)
                    _LogText = New Text.StringBuilder
                    textwriter.CreateFile()
                    textwriter = Nothing
                End If
                
            End If
        End Sub

    End Class
End Namespace
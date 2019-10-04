Imports System.IO
Imports System.Text

Namespace com.lib.file
    Public Class TextFileWriter

        Private _TextFile As com.lib.objects.FileObj
        Private _Texttoadd As New Text.StringBuilder

        Sub New(fileobj As com.lib.objects.FileObj)
            _TextFile = fileobj
        End Sub

        Public Sub AddText(text As String)
            If text.Count > 0 Then
                _Texttoadd.Append(text)
            End If
        End Sub

        Public Sub CreateFile()
            Dim fs As IO.FileStream = IO.File.Create(_TextFile.FullPathFile)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(_Texttoadd.ToString)
            fs.Write(info, 0, info.Length)
            fs.Close()
        End Sub

    End Class
End Namespace

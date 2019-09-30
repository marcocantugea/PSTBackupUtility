
Namespace com.lib.objects
    Public Class FileObjCollection
        Implements ICollection, IEnumerable, IEnumerator

        Private position As Integer = -1
        Private _Items As New List(Of FileObj)
        Private _DateLoaded As Date

        Public Property DateLoaded() As Date
            Get
                Return _DateLoaded
            End Get
            Set(ByVal value As Date)
                _DateLoaded = value
            End Set
        End Property

        Public ReadOnly Property Items() As List(Of FileObj)
            Get
                Return _Items
            End Get
        End Property
        Public ReadOnly Property CurrentPosition() As Integer
            Get
                Return position
            End Get
        End Property

        Public Sub Add(ByVal Item As FileObj)
            If Not IsNothing(Item) Then
                If Not Item.Fileinformation.Extension.Equals("") Then
                    If IO.File.Exists(Item.Fileinformation.FullName) Then
                        If Not ExcludeExt(Item.Fileinformation.Extension) Then
                            _Items.Add(Item)
                            Item.Serial = position
                        End If
                    End If

                End If
            End If
        End Sub

        Public Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo

        End Sub

        Public ReadOnly Property Count() As Integer Implements ICollection.Count
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return CType(Me, IEnumerator)
        End Function

        Public ReadOnly Property Current() As Object Implements IEnumerator.Current
            Get
                Return _Items.GetEnumerator.Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            position += 1
            Return (position <= _Items.Count)
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            position = -1
        End Sub

        Public Function TotalinBytesOfFiles() As Long
            Dim totalbytes As Long
            For Each item In _Items
                Try
                    totalbytes += item.SizeFileinBytes
                Catch ex As Exception
                    totalbytes = 0
                End Try

            Next
            Return totalbytes
        End Function

        Public Function DisplayTotal() As String
            Dim message As String = ""
            Dim totalbytes As Long = TotalinBytesOfFiles()
            Dim totalkb As Long = totalbytes / 1024
            Dim totalmb As Long = totalkb / 1024
            Dim totalgb As Long = totalmb / 1024

            If totalkb < 1024 Then
                message = ("Total Copied:" & FormatNumber(totalkb, 2) & " KB" & vbCrLf)
            End If

            If totalkb > 1024 And totalmb < 1024 Then
                message = ("Total Copied:" & FormatNumber(totalmb, 2) & " MB" & vbCrLf)
            End If

            If totalkb > 1024 And totalmb > 1024 Then
                message = ("Total Copied:" & FormatNumber(totalgb, 2) & " GB" & vbCrLf)
            End If

            Return message
        End Function

        Public Function ExcludeExt(ByVal ext As String) As Boolean
            Dim exclude As Boolean = False
            Dim extentionext() As String = GetExcludedExtention()
            If Not IsNothing(extentionext) Then
                For Each item As String In extentionext
                    If item.Equals(ext) Then
                        exclude = True
                    End If
                Next
            End If
            Return exclude
        End Function

        Public Function GetExcludedExtention() As String()
            Dim ext() As String
            If Not IsNothing(Configuration.ConfigurationSettings.AppSettings("excludedext")) Then
                Dim valueas As String = Configuration.ConfigurationSettings.AppSettings("excludedext")
                ext = valueas.Split("|")
            End If
            Return ext

        End Function

        'functiona added. Remove item from list
        Public Sub Remove(Fileobj As FileObj)
            _Items.Remove(Fileobj)
        End Sub

        Public Sub RemoveByfullPath(fullpathFilename As String)
            Dim index As Integer = -1
            Dim removeitem As Boolean
            Dim indexselected As Integer
            For Each file As FileObj In _Items
                index = index + 1
                If file.FullPathFile = fullpathFilename Then
                    removeitem = True
                    indexselected = index
                End If
            Next

            If removeitem Then
                _Items.RemoveAt(index)
            End If


        End Sub

        Public Sub Remove(filename As String)
            Dim index As Integer = -1
            Dim removeitem As Boolean
            Dim indexselected As Integer
            For Each file As FileObj In _Items
                index = index + 1
                If file.FileName = filename Then
                    removeitem = True
                    indexselected = index
                End If
            Next

            If removeitem Then
                _Items.RemoveAt(index)
            End If


        End Sub

        Public Sub Clear()
            _Items.Clear()
            position = -1
        End Sub

        Public Function ExistFileInItems(fileobj As FileObj) As Boolean
            Dim exist As Boolean = False
            For Each item As FileObj In _Items
                If item.FullPathFile = fileobj.FullPathFile Then
                    exist = True
                End If
            Next
            Return exist
        End Function

    End Class
End Namespace
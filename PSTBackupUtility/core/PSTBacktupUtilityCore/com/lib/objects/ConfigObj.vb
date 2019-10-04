

Namespace com.lib.objects

    Public Class ConfigObj

        Private _Parameter As String
        Private _ParameterValue As String

        Public Property ParameterValue As String
            Get
                Return _ParameterValue
            End Get
            Set(value As String)
                _ParameterValue = value
            End Set
        End Property

        Public Property Parameter As String
            Get
                Return _Parameter
            End Get
            Set(value As String)
                _Parameter = value
            End Set
        End Property

    End Class
End Namespace
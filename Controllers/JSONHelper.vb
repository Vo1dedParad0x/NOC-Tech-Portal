Imports System.Web.Script.Serialization
Imports System.Runtime.CompilerServices

Namespace ObjectExtensions
    Module JSONHelper
        <Extension()>
        Function ToJSON(ByVal obj As Object) As String
            Dim serializer As New JavaScriptSerializer()
            Return serializer.Serialize(obj)
        End Function

        <Extension()>
        Function ToJSON(ByVal obj As Object, recursionDepth As Integer) As String
            Dim serializer As New JavaScriptSerializer With {
                .RecursionLimit = recursionDepth
            }
            Return serializer.Serialize(obj)
        End Function
    End Module

End Namespace
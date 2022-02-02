Imports System.Runtime.CompilerServices


Public Module UserExtensions

    <Extension()>
    Public Function IsInAnyRoles(user As System.Security.Principal.IPrincipal, ParamArray roles() As String) As Boolean
        For Each role As String In roles
            If user.IsInRole(role) Then
                Return True
            End If
        Next
        Return False
    End Function

    <Extension()>
    Public Function HasAllRoles(user As System.Security.Principal.IPrincipal, ParamArray roles() As String) As Boolean
        For Each role As String In roles
            If Not user.IsInRole(role) Then
                Return False
            End If
        Next
        Return True
    End Function

End Module
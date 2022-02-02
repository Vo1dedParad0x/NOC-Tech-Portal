Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq

Partial Public Class AlarmTblDBContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=AlarmTblDBContext")
    End Sub

    Public Overridable Property AMSAlarms As DbSet(Of AMSAlarm)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
    End Sub
End Class

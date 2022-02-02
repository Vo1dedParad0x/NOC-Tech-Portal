Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq

Partial Public Class AMSAlarms
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=AMSAlarms")
    End Sub

    Public Overridable Property AMSAlarms_vw As DbSet(Of AMSAlarms_vw)
    Public Overridable Property AMSAlarmsOLTJoin_vw As DbSet(Of AMSAlarmsOLTJoin_vw)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.CusName) _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.AccountGroupCode) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.Latitude) _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.Longitude) _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.FullAddress) _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarms_vw)() _
            .Property(Function(e) e.AccountStatusCode) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarmsOLTJoin_vw)() _
            .Property(Function(e) e.AccountGroup) _
            .IsUnicode(False)

        modelBuilder.Entity(Of AMSAlarmsOLTJoin_vw)() _
            .Property(Function(e) e.Drop_Type) _
            .IsUnicode(False)
    End Sub
End Class

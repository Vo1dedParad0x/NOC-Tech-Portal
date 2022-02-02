Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class AMSAlarms_vw
    <StringLength(179)>
    Public Property CusName As String

    <Key>
    <Column(Order:=0)>
    <StringLength(6)>
    Public Property AccountGroupCode As String

    <StringLength(11)>
    Public Property Latitude As String

    <StringLength(11)>
    Public Property Longitude As String

    <Key>
    <Column(Order:=1)>
    Public Property AlarmDateTime As Date

    <Key>
    <Column(Order:=2)>
    <StringLength(25)>
    Public Property AlarmID As String

    <Key>
    <Column(Order:=3)>
    <StringLength(50)>
    Public Property AlarmType As String

    <Key>
    <Column(Order:=4)>
    <StringLength(100)>
    Public Property AMSObject As String

    Public Property Notes As String

    <Key>
    <Column(Order:=5)>
    <StringLength(30)>
    Public Property Priority As String

    <StringLength(8000)>
    Public Property FullAddress As String

    <StringLength(50)>
    Public Property EnitityGUID As String

    <Key>
    <Column(Order:=6)>
    <StringLength(1)>
    Public Property AccountStatusCode As String

    <StringLength(100)>
    Public Property LegacyKeyId As String

    Public Property CrmLocationId As Guid

    Public Property CrmAccountId As Guid
End Class

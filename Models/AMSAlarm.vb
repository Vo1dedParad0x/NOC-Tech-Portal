Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class AMSAlarm
    <Key>
    <StringLength(25)>
    Public Property AlarmID As String

    Public Property AlarmDateTime As Date

    <Required>
    <StringLength(50)>
    Public Property AlarmType As String

    <Required>
    <StringLength(100)>
    Public Property AMSObject As String

    Public Property Notes As String

    <Required>
    <StringLength(30)>
    Public Property Priority As String

    <StringLength(50)>
    Public Property EnitityGUID As String
End Class

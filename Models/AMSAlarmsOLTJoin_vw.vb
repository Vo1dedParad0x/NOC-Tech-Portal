Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class AMSAlarmsOLTJoin_vw
    <Key>
    <Column(Order:=0)>
    Public Property AlarmDateTime As Date

    <Key>
    <Column(Order:=1)>
    <StringLength(50)>
    Public Property AlarmType As String

    <StringLength(255)>
    Public Property Address As String

    <StringLength(50)>
    Public Property HUT As String

    <StringLength(50)>
    Public Property LCP As String

    <StringLength(50)>
    Public Property Nap As String

    Public Property Port As Short?

    Public Property Splitter As Short?

    <StringLength(50)>
    Public Property OLTPort As String

    <Key>
    <Column(Order:=2)>
    <StringLength(40)>
    Public Property AccountGroup As String

    <Column("Drop Type")>
    <StringLength(50)>
    Public Property Drop_Type As String
End Class

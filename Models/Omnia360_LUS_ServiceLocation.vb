Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Omnia360_LUS_ServiceLocation
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property LocationID As Integer

    <Column(TypeName:="smalldatetime")>
    Public Property LocationModify As Date?

    <StringLength(64)>
    Public Property LocationModifyUser As String

    <StringLength(100)>
    Public Property LegacyKeyId As String

    Public Property FM_LocationModify As Date?

    <StringLength(8000)>
    Public Property FullAddress As String

    <StringLength(10)>
    Public Property HouseNumber As String

    <StringLength(4)>
    Public Property HouseSuffix As String

    <StringLength(20)>
    Public Property PreDirectional As String

    <StringLength(40)>
    Public Property Street As String

    <StringLength(20)>
    Public Property StreetSuffix As String

    <StringLength(20)>
    Public Property PostDirectional As String

    <StringLength(75)>
    Public Property Apartment As String

    <StringLength(4)>
    Public Property Floor As String

    <StringLength(30)>
    Public Property Room As String

    <StringLength(28)>
    Public Property City As String

    <StringLength(6)>
    Public Property StateAbbreviation As String

    <StringLength(6)>
    Public Property CountryAbbreviation As String

    <StringLength(11)>
    Public Property PostalCode As String

    <StringLength(4)>
    Public Property PostalCodePlus4 As String

    Public Property CrmLocationId As Guid?

    <StringLength(256)>
    Public Property CrmName As String

    <StringLength(115)>
    Public Property CrmAddressLine1 As String

    <StringLength(80)>
    Public Property CrmAddressLine2 As String

    <StringLength(75)>
    Public Property CrmAddressLine3 As String

    <StringLength(28)>
    Public Property CrmCity As String

    <StringLength(7)>
    Public Property CrmState As String

    <StringLength(50)>
    Public Property CrmCountry As String

    <StringLength(80)>
    Public Property LocationDescription As String

    <StringLength(40)>
    Public Property TaxArea As String

    <StringLength(40)>
    Public Property CountyJurisdiction As String

    <StringLength(40)>
    Public Property DistrictJurisdiction As String

    <StringLength(4)>
    Public Property MsagExchange As String

    <StringLength(5)>
    Public Property MsagPsap As String

    <StringLength(5)>
    Public Property MSAGESN As String

    <StringLength(60)>
    Public Property LocationLocation As String

    <StringLength(11)>
    Public Property Latitude As String

    <StringLength(11)>
    Public Property Longitude As String

    <StringLength(7)>
    Public Property CensusTract As String

    <StringLength(4)>
    Public Property CensusBlock As String

    <StringLength(2)>
    Public Property CensusStateCode As String

    <StringLength(3)>
    Public Property CensusCountyCode As String

    <StringLength(100)>
    Public Property Region As String

    Public Property WireCenterID As Integer?

    <StringLength(100)>
    Public Property WireCenter As String
End Class

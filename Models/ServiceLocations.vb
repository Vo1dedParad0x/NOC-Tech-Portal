Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq

Partial Public Class ServiceLocations
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=ServiceLocationsDBContext")
    End Sub

    Public Overridable Property Omnia360_LUS_ServiceLocation As DbSet(Of Omnia360_LUS_ServiceLocation)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.LocationModifyUser) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.FullAddress) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.HouseNumber) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.HouseSuffix) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.PreDirectional) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Street) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.StreetSuffix) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.PostDirectional) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Apartment) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Floor) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Room) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.City) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.StateAbbreviation) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CountryAbbreviation) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.PostalCode) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.PostalCodePlus4) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.LocationDescription) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.TaxArea) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CountyJurisdiction) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.DistrictJurisdiction) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.MsagExchange) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.MsagPsap) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.MSAGESN) _
            .IsFixedLength() _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.LocationLocation) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Latitude) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.Longitude) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CensusTract) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CensusBlock) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CensusStateCode) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Omnia360_LUS_ServiceLocation)() _
            .Property(Function(e) e.CensusCountyCode) _
            .IsUnicode(False)
    End Sub
End Class

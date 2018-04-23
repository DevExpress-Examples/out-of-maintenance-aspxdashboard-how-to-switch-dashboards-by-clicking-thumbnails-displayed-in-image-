Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo
    Public NotInheritable Class EnergyConsumptionDataHelper

        Private Sub New()
        End Sub

        Public Shared Function GenerateTotal(ByVal dataSet As DataSet) As IList(Of EnergyConsumptionTotalDataRow)
            Dim res As IList(Of EnergyConsumptionTotalDataRow) = New List(Of EnergyConsumptionTotalDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("CountriesTotal").Rows
            For Each row As DataRow In data
                res.Add(New EnergyConsumptionTotalDataRow With { _
                    .Country = DirectCast(row("Country"), String), _
                    .Year = DirectCast(row("Year"), Date), _
                    .Consumption = DirectCast(row("Consumption"), Double), _
                    .Latitude = DirectCast(row("Latitude"), Double), _
                    .Longitude = DirectCast(row("Longitude"), Double), _
                    .Production = DirectCast(row("Production"), Double) _
                })
            Next row
            Return res
        End Function
        Public Shared Function GenerateBySector(ByVal dataSet As DataSet) As IList(Of EnergyConsumptionBySectorDataRow)
            Dim res As IList(Of EnergyConsumptionBySectorDataRow) = New List(Of EnergyConsumptionBySectorDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("CountriesBySector").Rows
            For Each row As DataRow In data
                res.Add(New EnergyConsumptionBySectorDataRow With { _
                    .Country = DirectCast(row("Country"), String), _
                    .Sector = DirectCast(row("Sector"), String), _
                    .Year = DirectCast(row("Year"), Date), _
                    .Consumption = If(row("Consumption") IsNot DBNull.Value, DirectCast(row("Consumption"), Double), 0), _
                    .Latitude = DirectCast(row("Latitude"), Double), _
                    .Longitude = DirectCast(row("Longitude"), Double) _
                })
            Next row
            Return res
        End Function
    End Class

    Public Class EnergyConsumptionTotalDataRow
        Public Property Country() As String
        Public Property Year() As Date
        Public Property Consumption() As Double
        Public Property Latitude() As Double
        Public Property Longitude() As Double
        Public Property Production() As Double
    End Class

    Public Class EnergyConsumptionBySectorDataRow
        Public Property Country() As String
        Public Property Sector() As String
        Public Property Year() As Date
        Public Property Consumption() As Double
        Public Property Latitude() As Double
        Public Property Longitude() As Double
    End Class
End Namespace

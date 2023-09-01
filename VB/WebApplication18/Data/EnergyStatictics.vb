Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo

    Public Module EnergyStaticticsDataHelper

        Public Function Generate(ByVal dataSet As DataSet) As IList(Of EnergyStaticticsDataRow)
            Dim res As IList(Of EnergyStaticticsDataRow) = New List(Of EnergyStaticticsDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("Countries").Rows
            For Each row As DataRow In data
                res.Add(New EnergyStaticticsDataRow With {.Country = CStr(row("Country")), .EnergyType = CStr(row("EnergyType")), .Year = CDate(row("Year")), .Import = If(row("Import") IsNot DBNull.Value, CDbl(row("Import")), 0), .Latitude = CDbl(row("Latitude")), .Longitude = CDbl(row("Longitude")), .Production = If(row("Production") IsNot DBNull.Value, CDbl(row("Production")), 0)})
            Next

            Return res
        End Function
    End Module

    Public Class EnergyStaticticsDataRow

        Public Property Country As String

        Public Property EnergyType As String

        Public Property Year As Date

        Public Property Import As Double

        Public Property Latitude As Double

        Public Property Longitude As Double

        Public Property Production As Double
    End Class
End Namespace

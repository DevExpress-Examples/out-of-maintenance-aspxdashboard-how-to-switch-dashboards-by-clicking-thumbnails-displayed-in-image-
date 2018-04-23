Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo
    Public NotInheritable Class EnergyStaticticsDataHelper

        Private Sub New()
        End Sub

        Public Shared Function Generate(ByVal dataSet As DataSet) As IList(Of EnergyStaticticsDataRow)
            Dim res As IList(Of EnergyStaticticsDataRow) = New List(Of EnergyStaticticsDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("Countries").Rows
            For Each row As DataRow In data
                res.Add(New EnergyStaticticsDataRow With { _
                    .Country = DirectCast(row("Country"), String), _
                    .EnergyType = DirectCast(row("EnergyType"), String), _
                    .Year = DirectCast(row("Year"), Date), _
                    .Import = If(row("Import") IsNot DBNull.Value, DirectCast(row("Import"), Double), 0), _
                    .Latitude = DirectCast(row("Latitude"), Double), _
                    .Longitude = DirectCast(row("Longitude"), Double), _
                    .Production = If(row("Production") IsNot DBNull.Value, DirectCast(row("Production"), Double), 0) _
                })
            Next row
            Return res
        End Function
    End Class

    Public Class EnergyStaticticsDataRow
        Public Property Country() As String
        Public Property EnergyType() As String
        Public Property Year() As Date
        Public Property Import() As Double
        Public Property Latitude() As Double
        Public Property Longitude() As Double
        Public Property Production() As Double
    End Class
End Namespace

Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo
    Public NotInheritable Class RevenueByIndustryDataHelper

        Private Sub New()
        End Sub

        Public Shared Function Generate(ByVal dataSet As DataSet) As IList(Of RevenueByIndustryDataRow)
            Dim res As IList(Of RevenueByIndustryDataRow) = New List(Of RevenueByIndustryDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("Statistics").Rows
            For Each row As DataRow In data
                res.Add(New RevenueByIndustryDataRow With { _
                    .City = DirectCast(row("City"), String), _
                    .Industry = DirectCast(row("Industry"), String), _
                    .State = DirectCast(row("State"), String), _
                    .Latitude = DirectCast(row("Latitude"), Double), _
                    .Longitude = DirectCast(row("Longitude"), Double), _
                    .Revenue = DirectCast(row("Revenue"), Decimal) _
                })
            Next row
            Return res
        End Function
    End Class

    Public Class RevenueByIndustryDataRow
        Public Property City() As String
        Public Property Industry() As String
        Public Property State() As String
        Public Property Latitude() As Double
        Public Property Longitude() As Double
        Public Property Revenue() As Decimal
    End Class
End Namespace

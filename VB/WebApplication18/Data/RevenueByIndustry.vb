Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo

    Public Module RevenueByIndustryDataHelper

        Public Function Generate(ByVal dataSet As DataSet) As IList(Of RevenueByIndustryDataRow)
            Dim res As IList(Of RevenueByIndustryDataRow) = New List(Of RevenueByIndustryDataRow)()
            Dim data As DataRowCollection = dataSet.Tables("Statistics").Rows
            For Each row As DataRow In data
                res.Add(New RevenueByIndustryDataRow With {.City = CStr(row("City")), .Industry = CStr(row("Industry")), .State = CStr(row("State")), .Latitude = CDbl(row("Latitude")), .Longitude = CDbl(row("Longitude")), .Revenue = CDec(row("Revenue"))})
            Next

            Return res
        End Function
    End Module

    Public Class RevenueByIndustryDataRow

        Public Property City As String

        Public Property Industry As String

        Public Property State As String

        Public Property Latitude As Double

        Public Property Longitude As Double

        Public Property Revenue As Decimal
    End Class
End Namespace

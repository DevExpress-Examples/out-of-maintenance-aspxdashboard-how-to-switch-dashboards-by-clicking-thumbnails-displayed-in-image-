Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo
    Public Class SalesPerformanceDataGenerator
        Inherits SalesDataGenerator

        Public Class TotalSalesItem
            Private uSoldYTDTarget As Integer
            Private uSoldYTD As Integer
            Private revQTDTarget As Decimal
            Private revQTD As Decimal
            Private revYTDTarget As Decimal
            Private revYTD As Decimal
            Private prod As String
            Private cat As String
            Private st As String

            Public Property State() As String
                Get
                    Return st
                End Get
                Set(ByVal value As String)
                    st = value
                End Set
            End Property
            Public Property Category() As String
                Get
                    Return cat
                End Get
                Set(ByVal value As String)
                    cat = value
                End Set
            End Property
            Public Property Product() As String
                Get
                    Return prod
                End Get
                Set(ByVal value As String)
                    prod = value
                End Set
            End Property
            Public Property RevenueYTD() As Decimal
                Get
                    Return revYTD
                End Get
                Set(ByVal value As Decimal)
                    revYTD = value
                End Set
            End Property
            Public Property RevenueYTDTarget() As Decimal
                Get
                    Return revYTDTarget
                End Get
                Set(ByVal value As Decimal)
                    revYTDTarget = value
                End Set
            End Property
            Public Property RevenueQTD() As Decimal
                Get
                    Return revQTD
                End Get
                Set(ByVal value As Decimal)
                    revQTD = value
                End Set
            End Property
            Public Property RevenueQTDTarget() As Decimal
                Get
                    Return revQTDTarget
                End Get
                Set(ByVal value As Decimal)
                    revQTDTarget = value
                End Set
            End Property
            Public Property UnitsSoldYTD() As Integer
                Get
                    Return uSoldYTD
                End Get
                Set(ByVal value As Integer)
                    uSoldYTD = value
                End Set
            End Property
            Public Property UnitsSoldYTDTarget() As Integer
                Get
                    Return uSoldYTDTarget
                End Get
                Set(ByVal value As Integer)
                    uSoldYTDTarget = value
                End Set
            End Property
        End Class

        Public Class MonthlySalesItem
            Private uSoldTarget As Integer
            Private uSold As Integer
            Private revTarget As Decimal
            Private rev As Decimal
            Private curtDate As Date
            Private cat As String
            Private prod As String
            Private st As String

            Public Property State() As String
                Get
                    Return st
                End Get
                Set(ByVal value As String)
                    st = value
                End Set
            End Property
            Public Property Product() As String
                Get
                    Return prod
                End Get
                Set(ByVal value As String)
                    prod = value
                End Set
            End Property
            Public Property Category() As String
                Get
                    Return cat
                End Get
                Set(ByVal value As String)
                    cat = value
                End Set
            End Property
            Public Property CurrentDate() As Date
                Get
                    Return curtDate
                End Get
                Set(ByVal value As Date)
                    curtDate = value
                End Set
            End Property
            Public Property Revenue() As Decimal
                Get
                    Return rev
                End Get
                Set(ByVal value As Decimal)
                    rev = value
                End Set
            End Property
            Public Property RevenueTarget() As Decimal
                Get
                    Return revTarget
                End Get
                Set(ByVal value As Decimal)
                    revTarget = value
                End Set
            End Property
            Public Property UnitsSold() As Integer
                Get
                    Return uSold
                End Get
                Set(ByVal value As Integer)
                    uSold = value
                End Set
            End Property
            Public Property UnitsSoldTarget() As Integer
                Get
                    Return uSoldTarget
                End Get
                Set(ByVal value As Integer)
                    uSoldTarget = value
                End Set
            End Property
        End Class

        Public Class KeyMetricsItem
            Private marShare As Single
            Private newCustYTDTarget As Integer
            Private newCustYTD As Integer
            Private avgOrdrSizeYTDTarget As Decimal
            Private avgOrdrSizeYTD As Decimal
            Private proYTDTarget As Decimal
            Private proYTD As Decimal
            Private expYTDTarget As Decimal
            Private expYTD As Decimal
            Private revYTDTarget As Decimal
            Private revYTD As Decimal

            Public Property RevenueYTD() As Decimal
                Get
                    Return revYTD
                End Get
                Set(ByVal value As Decimal)
                    revYTD = value
                End Set
            End Property
            Public Property RevenueYTDTarget() As Decimal
                Get
                    Return revYTDTarget
                End Get
                Set(ByVal value As Decimal)
                    revYTDTarget = value
                End Set
            End Property
            Public Property ExpencesYTD() As Decimal
                Get
                    Return expYTD
                End Get
                Set(ByVal value As Decimal)
                    expYTD = value
                End Set
            End Property
            Public Property ExpencesYTDTarget() As Decimal
                Get
                    Return expYTDTarget
                End Get
                Set(ByVal value As Decimal)
                    expYTDTarget = value
                End Set
            End Property
            Public Property ProfitYTD() As Decimal
                Get
                    Return proYTD
                End Get
                Set(ByVal value As Decimal)
                    proYTD = value
                End Set
            End Property
            Public Property ProfitYTDTarget() As Decimal
                Get
                    Return proYTDTarget
                End Get
                Set(ByVal value As Decimal)
                    proYTDTarget = value
                End Set
            End Property
            Public Property AvgOrderSizeYTD() As Decimal
                Get
                    Return avgOrdrSizeYTD
                End Get
                Set(ByVal value As Decimal)
                    avgOrdrSizeYTD = value
                End Set
            End Property
            Public Property AvgOrderSizeYTDTarget() As Decimal
                Get
                    Return avgOrdrSizeYTDTarget
                End Get
                Set(ByVal value As Decimal)
                    avgOrdrSizeYTDTarget = value
                End Set
            End Property
            Public Property NewCustomersYTD() As Integer
                Get
                    Return newCustYTD
                End Get
                Set(ByVal value As Integer)
                    newCustYTD = value
                End Set
            End Property
            Public Property NewCustomersYTDTarget() As Integer
                Get
                    Return newCustYTDTarget
                End Get
                Set(ByVal value As Integer)
                    newCustYTDTarget = value
                End Set
            End Property
            Public Property MarketShare() As Single
                Get
                    Return marShare
                End Get
                Set(ByVal value As Single)
                    marShare = value
                End Set
            End Property
        End Class


        Private ReadOnly monthlySales_Renamed As New List(Of MonthlySalesItem)()

        Private ReadOnly totalSales_Renamed As New List(Of TotalSalesItem)()
        Private ReadOnly item As New KeyMetricsItem()

        Public ReadOnly Property MonthlySales() As IEnumerable(Of MonthlySalesItem)
            Get
                Return monthlySales_Renamed
            End Get
        End Property
        Public ReadOnly Property TotalSales() As IEnumerable(Of TotalSalesItem)
            Get
                Return totalSales_Renamed
            End Get
        End Property
        Public ReadOnly Property KeyMetrics() As IEnumerable(Of KeyMetricsItem)
            Get
                Return New KeyMetricsItem() { item }
            End Get
        End Property

        Public Sub New(ByVal dataSet As DataSet)
            MyBase.New(dataSet)
        End Sub
        Protected Overrides Sub Generate(ByVal context As Context)
            Dim tsItem As TotalSalesItem = New TotalSalesItem With { _
                .State = context.State, _
                .Category = context.CategoryName, _
                .Product = context.ProductName _
            }
            Dim y As Integer = Date.Today.Year - 1
            For month As Integer = 1 To 12
                Dim dt As New Date(y, month, 1)
                context.UnitsSoldGenerator.Next()
                Dim uSold As Integer = context.UnitsSoldGenerator.UnitsSold
                Dim uSoldTarget As Integer = context.UnitsSoldGenerator.UnitsSoldTarget
                Dim rev As Decimal = uSold * context.ListPrice
                Dim revTarget As Decimal = uSoldTarget * context.ListPrice
                monthlySales_Renamed.Add(New MonthlySalesItem With { _
                    .State = context.State, _
                    .Product = context.ProductName, _
                    .Category = context.CategoryName, _
                    .CurrentDate = dt, _
                    .UnitsSold = uSold, _
                    .UnitsSoldTarget = uSoldTarget, _
                    .Revenue = rev, _
                    .RevenueTarget = revTarget _
                })
                tsItem.RevenueYTD += rev
                tsItem.RevenueYTDTarget += revTarget
                tsItem.UnitsSoldYTD += uSold
                tsItem.UnitsSoldYTDTarget += uSoldTarget
                If month >= 10 AndAlso month <= 12 Then
                    tsItem.RevenueQTD += rev
                    tsItem.RevenueQTDTarget += revTarget
                End If
                item.RevenueYTD += rev
                item.RevenueYTDTarget += revTarget
            Next month
            totalSales_Renamed.Add(tsItem)
        End Sub
        Protected Overrides Sub EndGenerate()
            MyBase.EndGenerate()
            item.ExpencesYTD = item.RevenueYTDTarget * 0.2D
            item.ExpencesYTDTarget = item.RevenueYTDTarget * 0.1999D
            item.ProfitYTD = item.RevenueYTD - item.ExpencesYTD
            item.ProfitYTDTarget = item.RevenueYTDTarget - item.ExpencesYTDTarget
            item.AvgOrderSizeYTD = item.RevenueYTD * 0.006D
            item.AvgOrderSizeYTDTarget = item.RevenueYTDTarget * 0.0055D
            item.NewCustomersYTD = CInt((Math.Round(item.RevenueYTD * 0.0013D)))
            item.NewCustomersYTDTarget = CInt((Math.Round(item.RevenueYTDTarget * 0.00125D)))
            item.MarketShare = 0.23F
        End Sub
    End Class
End Namespace

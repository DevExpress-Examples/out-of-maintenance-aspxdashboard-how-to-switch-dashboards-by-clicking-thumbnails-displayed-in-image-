Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo

    Public Class SalesPerformanceDataGenerator
        Inherits DashboardMainDemo.SalesDataGenerator

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

            Public Property State As String
                Get
                    Return Me.st
                End Get

                Set(ByVal value As String)
                    Me.st = value
                End Set
            End Property

            Public Property Category As String
                Get
                    Return Me.cat
                End Get

                Set(ByVal value As String)
                    Me.cat = value
                End Set
            End Property

            Public Property Product As String
                Get
                    Return Me.prod
                End Get

                Set(ByVal value As String)
                    Me.prod = value
                End Set
            End Property

            Public Property RevenueYTD As Decimal
                Get
                    Return Me.revYTD
                End Get

                Set(ByVal value As Decimal)
                    Me.revYTD = value
                End Set
            End Property

            Public Property RevenueYTDTarget As Decimal
                Get
                    Return Me.revYTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.revYTDTarget = value
                End Set
            End Property

            Public Property RevenueQTD As Decimal
                Get
                    Return Me.revQTD
                End Get

                Set(ByVal value As Decimal)
                    Me.revQTD = value
                End Set
            End Property

            Public Property RevenueQTDTarget As Decimal
                Get
                    Return Me.revQTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.revQTDTarget = value
                End Set
            End Property

            Public Property UnitsSoldYTD As Integer
                Get
                    Return Me.uSoldYTD
                End Get

                Set(ByVal value As Integer)
                    Me.uSoldYTD = value
                End Set
            End Property

            Public Property UnitsSoldYTDTarget As Integer
                Get
                    Return Me.uSoldYTDTarget
                End Get

                Set(ByVal value As Integer)
                    Me.uSoldYTDTarget = value
                End Set
            End Property
        End Class

        Public Class MonthlySalesItem

            Private uSoldTarget As Integer

            Private uSold As Integer

            Private revTarget As Decimal

            Private rev As Decimal

            Private curtDate As System.DateTime

            Private cat As String

            Private prod As String

            Private st As String

            Public Property State As String
                Get
                    Return Me.st
                End Get

                Set(ByVal value As String)
                    Me.st = value
                End Set
            End Property

            Public Property Product As String
                Get
                    Return Me.prod
                End Get

                Set(ByVal value As String)
                    Me.prod = value
                End Set
            End Property

            Public Property Category As String
                Get
                    Return Me.cat
                End Get

                Set(ByVal value As String)
                    Me.cat = value
                End Set
            End Property

            Public Property CurrentDate As DateTime
                Get
                    Return Me.curtDate
                End Get

                Set(ByVal value As DateTime)
                    Me.curtDate = value
                End Set
            End Property

            Public Property Revenue As Decimal
                Get
                    Return Me.rev
                End Get

                Set(ByVal value As Decimal)
                    Me.rev = value
                End Set
            End Property

            Public Property RevenueTarget As Decimal
                Get
                    Return Me.revTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.revTarget = value
                End Set
            End Property

            Public Property UnitsSold As Integer
                Get
                    Return Me.uSold
                End Get

                Set(ByVal value As Integer)
                    Me.uSold = value
                End Set
            End Property

            Public Property UnitsSoldTarget As Integer
                Get
                    Return Me.uSoldTarget
                End Get

                Set(ByVal value As Integer)
                    Me.uSoldTarget = value
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

            Public Property RevenueYTD As Decimal
                Get
                    Return Me.revYTD
                End Get

                Set(ByVal value As Decimal)
                    Me.revYTD = value
                End Set
            End Property

            Public Property RevenueYTDTarget As Decimal
                Get
                    Return Me.revYTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.revYTDTarget = value
                End Set
            End Property

            Public Property ExpencesYTD As Decimal
                Get
                    Return Me.expYTD
                End Get

                Set(ByVal value As Decimal)
                    Me.expYTD = value
                End Set
            End Property

            Public Property ExpencesYTDTarget As Decimal
                Get
                    Return Me.expYTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.expYTDTarget = value
                End Set
            End Property

            Public Property ProfitYTD As Decimal
                Get
                    Return Me.proYTD
                End Get

                Set(ByVal value As Decimal)
                    Me.proYTD = value
                End Set
            End Property

            Public Property ProfitYTDTarget As Decimal
                Get
                    Return Me.proYTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.proYTDTarget = value
                End Set
            End Property

            Public Property AvgOrderSizeYTD As Decimal
                Get
                    Return Me.avgOrdrSizeYTD
                End Get

                Set(ByVal value As Decimal)
                    Me.avgOrdrSizeYTD = value
                End Set
            End Property

            Public Property AvgOrderSizeYTDTarget As Decimal
                Get
                    Return Me.avgOrdrSizeYTDTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.avgOrdrSizeYTDTarget = value
                End Set
            End Property

            Public Property NewCustomersYTD As Integer
                Get
                    Return Me.newCustYTD
                End Get

                Set(ByVal value As Integer)
                    Me.newCustYTD = value
                End Set
            End Property

            Public Property NewCustomersYTDTarget As Integer
                Get
                    Return Me.newCustYTDTarget
                End Get

                Set(ByVal value As Integer)
                    Me.newCustYTDTarget = value
                End Set
            End Property

            Public Property MarketShare As Single
                Get
                    Return Me.marShare
                End Get

                Set(ByVal value As Single)
                    Me.marShare = value
                End Set
            End Property
        End Class

        Private ReadOnly monthlySalesField As System.Collections.Generic.List(Of DashboardMainDemo.SalesPerformanceDataGenerator.MonthlySalesItem) = New System.Collections.Generic.List(Of DashboardMainDemo.SalesPerformanceDataGenerator.MonthlySalesItem)()

        Private ReadOnly totalSalesField As System.Collections.Generic.List(Of DashboardMainDemo.SalesPerformanceDataGenerator.TotalSalesItem) = New System.Collections.Generic.List(Of DashboardMainDemo.SalesPerformanceDataGenerator.TotalSalesItem)()

        Private ReadOnly item As DashboardMainDemo.SalesPerformanceDataGenerator.KeyMetricsItem = New DashboardMainDemo.SalesPerformanceDataGenerator.KeyMetricsItem()

        Public ReadOnly Property MonthlySales As IEnumerable(Of DashboardMainDemo.SalesPerformanceDataGenerator.MonthlySalesItem)
            Get
                Return Me.monthlySalesField
            End Get
        End Property

        Public ReadOnly Property TotalSales As IEnumerable(Of DashboardMainDemo.SalesPerformanceDataGenerator.TotalSalesItem)
            Get
                Return Me.totalSalesField
            End Get
        End Property

        Public ReadOnly Property KeyMetrics As IEnumerable(Of DashboardMainDemo.SalesPerformanceDataGenerator.KeyMetricsItem)
            Get
                Return New DashboardMainDemo.SalesPerformanceDataGenerator.KeyMetricsItem() {Me.item}
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            MyBase.New(dataSet)
        End Sub

        Protected Overrides Sub Generate(ByVal context As DashboardMainDemo.SalesDataGenerator.Context)
            Dim tsItem As DashboardMainDemo.SalesPerformanceDataGenerator.TotalSalesItem = New DashboardMainDemo.SalesPerformanceDataGenerator.TotalSalesItem With {.State = context.State, .Category = context.CategoryName, .Product = context.ProductName}
            Dim y As Integer = System.DateTime.Today.Year - 1
            For month As Integer = 1 To 12
                Dim dt As System.DateTime = New System.DateTime(y, month, 1)
                context.UnitsSoldGenerator.[Next]()
                Dim uSold As Integer = context.UnitsSoldGenerator.UnitsSold
                Dim uSoldTarget As Integer = context.UnitsSoldGenerator.UnitsSoldTarget
                Dim rev As Decimal = uSold * context.ListPrice
                Dim revTarget As Decimal = uSoldTarget * context.ListPrice
                Me.monthlySalesField.Add(New DashboardMainDemo.SalesPerformanceDataGenerator.MonthlySalesItem With {.State = context.State, .Product = context.ProductName, .Category = context.CategoryName, .CurrentDate = dt, .UnitsSold = uSold, .UnitsSoldTarget = uSoldTarget, .Revenue = rev, .RevenueTarget = revTarget})
                tsItem.RevenueYTD += rev
                tsItem.RevenueYTDTarget += revTarget
                tsItem.UnitsSoldYTD += uSold
                tsItem.UnitsSoldYTDTarget += uSoldTarget
                If month >= 10 AndAlso month <= 12 Then
                    tsItem.RevenueQTD += rev
                    tsItem.RevenueQTDTarget += revTarget
                End If

                Me.item.RevenueYTD += rev
                Me.item.RevenueYTDTarget += revTarget
            Next

            Me.totalSalesField.Add(tsItem)
        End Sub

        Protected Overrides Sub EndGenerate()
            MyBase.EndGenerate()
            Me.item.ExpencesYTD = Me.item.RevenueYTDTarget * 0.2D
            Me.item.ExpencesYTDTarget = Me.item.RevenueYTDTarget * 0.1999D
            Me.item.ProfitYTD = Me.item.RevenueYTD - Me.item.ExpencesYTD
            Me.item.ProfitYTDTarget = Me.item.RevenueYTDTarget - Me.item.ExpencesYTDTarget
            Me.item.AvgOrderSizeYTD = Me.item.RevenueYTD * 0.006D
            Me.item.AvgOrderSizeYTDTarget = Me.item.RevenueYTDTarget * 0.0055D
            Me.item.NewCustomersYTD = CInt(System.Math.Round(Me.item.RevenueYTD * 0.0013D))
            Me.item.NewCustomersYTDTarget = CInt(System.Math.Round(Me.item.RevenueYTDTarget * 0.00125D))
            Me.item.MarketShare = 0.23F
        End Sub
    End Class
End Namespace

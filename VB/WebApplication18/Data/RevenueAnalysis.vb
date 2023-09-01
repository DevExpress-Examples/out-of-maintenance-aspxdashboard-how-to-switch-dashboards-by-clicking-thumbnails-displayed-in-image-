Imports System
Imports System.Data
Imports System.Collections.Generic

Namespace DashboardMainDemo

    Public Class RevenueAnalysisDataGenerator
        Inherits DashboardMainDemo.SalesDataGenerator

        Public Class DataItem

            Private uSold As Integer

            Private rev As Decimal

            Private prod As String

            Private cat As String

            Private st As String

            Private y As Integer

            Public Property Year As Integer
                Get
                    Return Me.y
                End Get

                Set(ByVal value As Integer)
                    Me.y = value
                End Set
            End Property

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

            Public Property Revenue As Decimal
                Get
                    Return Me.rev
                End Get

                Set(ByVal value As Decimal)
                    Me.rev = value
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
        End Class

        Const YearsCount As Integer = 3

        Private ReadOnly dat As System.Collections.Generic.List(Of DashboardMainDemo.RevenueAnalysisDataGenerator.DataItem) = New System.Collections.Generic.List(Of DashboardMainDemo.RevenueAnalysisDataGenerator.DataItem)()

        Public ReadOnly Property Data As IEnumerable(Of DashboardMainDemo.RevenueAnalysisDataGenerator.DataItem)
            Get
                Return Me.dat
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            MyBase.New(dataSet)
        End Sub

        Protected Overrides Sub Generate(ByVal context As DashboardMainDemo.SalesDataGenerator.Context)
            Dim startYear As Integer = System.DateTime.Today.Year - DashboardMainDemo.RevenueAnalysisDataGenerator.YearsCount
            For i As Integer = 0 To DashboardMainDemo.RevenueAnalysisDataGenerator.YearsCount - 1
                Dim year As Integer = startYear + i
                context.UnitsSoldGenerator.[Next]()
                Dim unitsSold As Integer = context.UnitsSoldGenerator.UnitsSold * 12
                Dim revenue As Decimal = unitsSold * context.ListPrice
                Me.dat.Add(New DashboardMainDemo.RevenueAnalysisDataGenerator.DataItem With {.State = context.State, .Category = context.CategoryName, .Product = context.ProductName, .Year = year, .Revenue = revenue, .UnitsSold = unitsSold})
            Next
        End Sub
    End Class
End Namespace

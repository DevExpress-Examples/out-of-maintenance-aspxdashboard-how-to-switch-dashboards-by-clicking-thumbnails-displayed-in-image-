Imports System
Imports System.Data
Imports System.Collections.Generic

Namespace DashboardMainDemo
    Public Class RevenueAnalysisDataGenerator
        Inherits SalesDataGenerator

        Public Class DataItem
            Private uSold As Integer
            Private rev As Decimal
            Private prod As String
            Private cat As String
            Private st As String
            Private y As Integer

            Public Property Year() As Integer
                Get
                    Return y
                End Get
                Set(ByVal value As Integer)
                    y = value
                End Set
            End Property
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
            Public Property Revenue() As Decimal
                Get
                    Return rev
                End Get
                Set(ByVal value As Decimal)
                    rev = value
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
        End Class

        Private Const YearsCount As Integer = 3

        Private ReadOnly dat As New List(Of DataItem)()

        Public ReadOnly Property Data() As IEnumerable(Of DataItem)
            Get
                Return dat
            End Get
        End Property

        Public Sub New(ByVal dataSet As DataSet)
            MyBase.New(dataSet)
        End Sub
        Protected Overrides Sub Generate(ByVal context As Context)
            Dim startYear As Integer = Date.Today.Year - YearsCount
            For i As Integer = 0 To YearsCount - 1
                Dim year As Integer = startYear + i
                context.UnitsSoldGenerator.Next()
                Dim unitsSold As Integer = context.UnitsSoldGenerator.UnitsSold * 12
                Dim revenue As Decimal = unitsSold * context.ListPrice
                dat.Add(New DataItem With { _
                    .State = context.State, _
                    .Category = context.CategoryName, _
                    .Product = context.ProductName, _
                    .Year = year, _
                    .Revenue = revenue, _
                    .UnitsSold = unitsSold _
                })
            Next i
        End Sub
    End Class
End Namespace

Imports System
Imports System.Data
Imports System.Collections.Generic

Namespace DashboardMainDemo

    Public Class SalesDetailsDataGenerator
        Inherits DashboardMainDemo.SalesDataGenerator

        Public Class DataItem

            Private retTarget As Integer

            Private ret As Integer

            Private uSoldTarget As Integer

            Private uSold As Integer

            Private uReceived As Integer

            Private revTarget As Decimal

            Private rev As Decimal

            Private curtDate As System.DateTime

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

            Public Property Returns As Integer
                Get
                    Return Me.ret
                End Get

                Set(ByVal value As Integer)
                    Me.ret = value
                End Set
            End Property

            Public Property ReturnsTarget As Integer
                Get
                    Return Me.retTarget
                End Get

                Set(ByVal value As Integer)
                    Me.retTarget = value
                End Set
            End Property

            Public Property UnitsReceived As Integer
                Get
                    Return Me.uReceived
                End Get

                Set(ByVal value As Integer)
                    Me.uReceived = value
                End Set
            End Property
        End Class

        Private ReadOnly dat As System.Collections.Generic.List(Of DashboardMainDemo.SalesDetailsDataGenerator.DataItem) = New System.Collections.Generic.List(Of DashboardMainDemo.SalesDetailsDataGenerator.DataItem)()

        Public ReadOnly Property Data As IEnumerable(Of DashboardMainDemo.SalesDetailsDataGenerator.DataItem)
            Get
                Return Me.dat
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            MyBase.New(dataSet)
        End Sub

        Protected Overrides Sub Generate(ByVal context As DashboardMainDemo.SalesDataGenerator.Context)
            Dim rand As System.Random = Me.Random
            Dim year As Integer = System.DateTime.Today.Year - 1
            For month As Integer = 1 To 12
                Dim _date As System.DateTime = New System.DateTime(year, month, 1)
                context.UnitsSoldGenerator.[Next]()
                Dim uSold As Integer = context.UnitsSoldGenerator.UnitsSold
                Dim uSoldTarget As Integer = context.UnitsSoldGenerator.UnitsSoldTarget
                Dim ret As Integer = CInt(System.Math.Round(uSold * rand.NextDouble() * 0.5))
                Dim retTarget As Integer = CInt(System.Math.Round(uSoldTarget * 0.25))
                Dim uReceived As Integer = uSold + rand.[Next](-2, 3)
                Dim rev As Decimal =(uSold - ret) * context.ListPrice
                Dim revTarget As Decimal =(uSoldTarget - retTarget) * context.ListPrice
                Me.dat.Add(New DashboardMainDemo.SalesDetailsDataGenerator.DataItem With {.State = context.State, .Category = context.CategoryName, .Product = context.ProductName, .CurrentDate = _date, .UnitsSold = uSold, .UnitsSoldTarget = uSoldTarget, .Returns = ret, .ReturnsTarget = retTarget, .Revenue = rev, .RevenueTarget = revTarget, .UnitsReceived = uReceived})
            Next
        End Sub
    End Class
End Namespace

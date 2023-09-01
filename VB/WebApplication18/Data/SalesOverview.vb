Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo

    Public Class SalesOverviewDataGenerator
        Inherits DashboardMainDemo.SalesDataGenerator

        Public Class DataItem

            Private sTarget As Decimal

            Private sal As Decimal

            Private curtDate As System.DateTime

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

            Public Property CurrentDate As DateTime
                Get
                    Return Me.curtDate
                End Get

                Set(ByVal value As DateTime)
                    Me.curtDate = value
                End Set
            End Property

            Public Property Sales As Decimal
                Get
                    Return Me.sal
                End Get

                Set(ByVal value As Decimal)
                    Me.sal = value
                End Set
            End Property

            Public Property SalesTarget As Decimal
                Get
                    Return Me.sTarget
                End Get

                Set(ByVal value As Decimal)
                    Me.sTarget = value
                End Set
            End Property
        End Class

        Public Class DataKey

            Private ReadOnly state As String

            Private ReadOnly category As String

            Private ReadOnly dt As System.DateTime

            Public Sub New(ByVal state As String, ByVal category As String, ByVal dt As System.DateTime)
                Me.state = state
                Me.category = category
                Me.dt = dt
            End Sub

            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                Dim key As DashboardMainDemo.SalesOverviewDataGenerator.DataKey = CType(obj, DashboardMainDemo.SalesOverviewDataGenerator.DataKey)
                Return Equals(key.state, Me.state) AndAlso Equals(key.category, Me.category) AndAlso key.dt = Me.dt
            End Function

            Public Overrides Function GetHashCode() As Integer
                Return Me.state.GetHashCode() Xor Me.category.GetHashCode() Xor Me.dt.GetHashCode()
            End Function
        End Class

        Private ReadOnly dat As System.Collections.Generic.Dictionary(Of DashboardMainDemo.SalesOverviewDataGenerator.DataKey, DashboardMainDemo.SalesOverviewDataGenerator.DataItem) = New System.Collections.Generic.Dictionary(Of DashboardMainDemo.SalesOverviewDataGenerator.DataKey, DashboardMainDemo.SalesOverviewDataGenerator.DataItem)()

        Private ReadOnly startDate As System.DateTime

        Private ReadOnly endDate As System.DateTime

        Public ReadOnly Property Data As IEnumerable(Of DashboardMainDemo.SalesOverviewDataGenerator.DataItem)
            Get
                Return Me.dat.Values
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            MyBase.New(dataSet)
            Me.endDate = System.DateTime.Today
            Me.startDate = New System.DateTime(Me.endDate.Year - 2, 1, 1)
        End Sub

        Protected Overrides Sub Generate(ByVal context As DashboardMainDemo.SalesDataGenerator.Context)
            Dim dt As System.DateTime = Me.startDate
            While dt < Me.endDate
                If dt.DayOfWeek = System.DayOfWeek.Monday Then
                    context.UnitsSoldGenerator.[Next]()
                    Dim sales As Decimal = context.UnitsSoldGenerator.UnitsSold * context.ListPrice
                    Dim salesTarget As Decimal = context.UnitsSoldGenerator.UnitsSoldTarget * context.ListPrice
                    Dim datKey As DashboardMainDemo.SalesOverviewDataGenerator.DataKey = New DashboardMainDemo.SalesOverviewDataGenerator.DataKey(context.State, context.CategoryName, dt)
                    Dim datItem As DashboardMainDemo.SalesOverviewDataGenerator.DataItem = Nothing
                    If Not Me.dat.TryGetValue(datKey, datItem) Then
                        datItem = New DashboardMainDemo.SalesOverviewDataGenerator.DataItem With {.CurrentDate = dt, .Category = context.CategoryName, .State = context.State}
                        Me.dat.Add(datKey, datItem)
                    End If

                    datItem.Sales += sales
                    datItem.SalesTarget += salesTarget
                End If

                dt = dt.AddDays(1)
            End While
        End Sub
    End Class
End Namespace

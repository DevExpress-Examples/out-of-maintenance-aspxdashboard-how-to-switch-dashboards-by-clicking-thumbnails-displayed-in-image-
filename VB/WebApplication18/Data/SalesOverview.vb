Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace DashboardMainDemo
    Public Class SalesOverviewDataGenerator
        Inherits SalesDataGenerator

        Public Class DataItem
            Private sTarget As Decimal
            Private sal As Decimal
            Private curtDate As Date
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
            Public Property CurrentDate() As Date
                Get
                    Return curtDate
                End Get
                Set(ByVal value As Date)
                    curtDate = value
                End Set
            End Property
            Public Property Sales() As Decimal
                Get
                    Return sal
                End Get
                Set(ByVal value As Decimal)
                    sal = value
                End Set
            End Property
            Public Property SalesTarget() As Decimal
                Get
                    Return sTarget
                End Get
                Set(ByVal value As Decimal)
                    sTarget = value
                End Set
            End Property
        End Class

        Public Class DataKey
            Private ReadOnly state As String
            Private ReadOnly category As String
            Private ReadOnly dt As Date

            Public Sub New(ByVal state As String, ByVal category As String, ByVal dt As Date)
                Me.state = state
                Me.category = category
                Me.dt = dt
            End Sub
            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                Dim key As DataKey = DirectCast(obj, DataKey)
                Return key.state = state AndAlso key.category = category AndAlso key.dt = dt
            End Function
            Public Overrides Function GetHashCode() As Integer
                Return state.GetHashCode() Xor category.GetHashCode() Xor dt.GetHashCode()
            End Function
        End Class

        Private ReadOnly dat As New Dictionary(Of DataKey, DataItem)()
        Private ReadOnly startDate As Date
        Private ReadOnly endDate As Date

        Public ReadOnly Property Data() As IEnumerable(Of DataItem)
            Get
                Return dat.Values
            End Get
        End Property

        Public Sub New(ByVal dataSet As DataSet)
            MyBase.New(dataSet)
            endDate = Date.Today
            startDate = New Date(endDate.Year - 2, 1, 1)
        End Sub
        Protected Overrides Sub Generate(ByVal context As Context)
            Dim dt As Date = startDate
            Do While dt < endDate
                If dt.DayOfWeek = DayOfWeek.Monday Then
                    context.UnitsSoldGenerator.Next()
                    Dim sales As Decimal = context.UnitsSoldGenerator.UnitsSold * context.ListPrice
                    Dim salesTarget As Decimal = context.UnitsSoldGenerator.UnitsSoldTarget * context.ListPrice
                    Dim datKey As New DataKey(context.State, context.CategoryName, dt)
                    Dim datItem As DataItem = Nothing
                    If Not dat.TryGetValue(datKey, datItem) Then
                        datItem = New DataItem With { _
                            .CurrentDate = dt, _
                            .Category = context.CategoryName, _
                            .State = context.State _
                        }
                        dat.Add(datKey, datItem)
                    End If
                    datItem.Sales += sales
                    datItem.SalesTarget += salesTarget
                End If
                dt = dt.AddDays(1)
            Loop
        End Sub
    End Class
End Namespace

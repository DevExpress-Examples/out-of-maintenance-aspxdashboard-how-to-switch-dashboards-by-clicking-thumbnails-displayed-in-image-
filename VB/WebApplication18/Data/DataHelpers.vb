Imports System.Collections.Generic
Imports System.Data
Imports System.Collections
Imports System

Namespace DashboardMainDemo
    Public NotInheritable Class DataHelper

        Private Sub New()
        End Sub


        Public Shared Function Random(ByVal random_Renamed As Random, ByVal deviation As Double, ByVal positive As Boolean) As Double
            Dim rand As Integer = random_Renamed.Next(If(positive, 0, - 1000000), 1000000)
            Return CDbl(rand) / 1000000 * deviation
        End Function

        Public Shared Function Random(ByVal random_Renamed As Random, ByVal deviation As Double) As Double
            Return DataHelper.Random(random_Renamed, deviation, False)
        End Function
    End Class

    Public Class ProductClass
        Private ReadOnly productIDs As New List(Of Integer)()
        Private ReadOnly minPrice? As Decimal
        Private ReadOnly maxPrice? As Decimal

        Private ReadOnly saleProbability_Renamed As Double

        Public ReadOnly Property SaleProbability() As Double
            Get
                Return saleProbability_Renamed
            End Get
        End Property

        Public Sub New(ByVal minPrice? As Decimal, ByVal maxPrice? As Decimal, ByVal saleProbability As Double)
            Me.minPrice = minPrice
            Me.maxPrice = maxPrice
            Me.saleProbability_Renamed = saleProbability
        End Sub
        Public Function AddProduct(ByVal productID As Integer, ByVal price As Decimal) As Boolean
            Dim satisfyMinPrice As Boolean = Not minPrice.HasValue OrElse price >= minPrice.Value
            Dim satisfyMaxPrice As Boolean = Not maxPrice.HasValue OrElse price < maxPrice.Value
            If satisfyMinPrice AndAlso satisfyMaxPrice Then
                productIDs.Add(productID)
                Return True
            End If
            Return False
        End Function
        Public Function ContainsProduct(ByVal productID As Integer) As Boolean
            Return productIDs.Contains(productID)
        End Function
    End Class

    Public Class ProductClasses
        Inherits List(Of ProductClass)

        Default Public Shadows ReadOnly Property Item(ByVal productID As Integer) As ProductClass
            Get
                For Each productClass As ProductClass In Me
                    If productClass.ContainsProduct(productID) Then
                        Return productClass
                    End If
                Next productClass
                Throw New ArgumentException("procutID")
            End Get
        End Property

        Public Sub New(ByVal products As ICollection)
            Add(New ProductClass(Nothing, 100D, 0.5))
            Add(New ProductClass(100D, 500D, 0.4))
            Add(New ProductClass(500D, 1500D, 0.3))
            Add(New ProductClass(1500D, Nothing, 0.2))
            For Each product As DataRow In products
                Dim productID As Integer = DirectCast(product("ProductID"), Integer)
                Dim listPrice As Decimal = DirectCast(product("ListPrice"), Decimal)
                For Each productClass As ProductClass In Me
                    If productClass.AddProduct(productID, listPrice) Then
                        Exit For
                    End If
                Next productClass
            Next product
        End Sub
    End Class

    Public Class RegionClasses
        Inherits Dictionary(Of Integer, Double)

        Public Sub New(ByVal regions As ICollection)
            Dim numberEmployeesMin? As Integer = Nothing
            For Each region As DataRow In regions
                Dim numberEmployees As Short = DirectCast(region("NumberEmployees"), Short)
                numberEmployeesMin = If(numberEmployeesMin.HasValue, Math.Min(numberEmployeesMin.Value, numberEmployees), numberEmployees)
            Next region
            For Each region As DataRow In regions
                Add(DirectCast(region("RegionID"), Integer), DirectCast(region("NumberEmployees"), Short) / CDbl(numberEmployeesMin.Value))
            Next region
        End Sub
    End Class

    Public Class UnitsSoldRandomGenerator
        Private Const MinUnitsSold As Integer = 5

        Private ReadOnly rand As Random
        Private ReadOnly startUnitsSold As Integer
        Private prevUnitsSold? As Integer
        Private prevPrevUnitsSold? As Integer

        Private unitsSold_Renamed As Integer

        Private unitsSoldTarget_Renamed As Integer
        Private isFirst As Boolean = True

        Public ReadOnly Property UnitsSold() As Integer
            Get
                Return unitsSold_Renamed
            End Get
        End Property
        Public ReadOnly Property UnitsSoldTarget() As Integer
            Get
                Return unitsSoldTarget_Renamed
            End Get
        End Property

        Public Sub New(ByVal rand As Random, ByVal startUnitsSold As Integer)
            Me.rand = rand
            Me.startUnitsSold = Math.Max(startUnitsSold, MinUnitsSold)
        End Sub
        Public Sub [Next]()
            If isFirst Then
                unitsSold_Renamed = startUnitsSold
                isFirst = False
            Else
                unitsSold_Renamed = unitsSold_Renamed + CInt((Math.Round(DataHelper.Random(rand, unitsSold_Renamed * 0.5))))
                unitsSold_Renamed = Math.Max(unitsSold_Renamed, MinUnitsSold)
            End If
            Dim unitsSoldSum As Integer = unitsSold_Renamed
            Dim count As Integer = 1
            If prevUnitsSold.HasValue Then
                unitsSoldSum += prevUnitsSold.Value
                count += 1
            End If
            If prevPrevUnitsSold.HasValue Then
                unitsSoldSum += prevPrevUnitsSold.Value
                count += 1
            End If
            unitsSoldTarget_Renamed = CInt((Math.Round(CDbl(unitsSoldSum) / count)))
            unitsSoldTarget_Renamed = unitsSoldTarget_Renamed + CInt((Math.Round(DataHelper.Random(rand, unitsSoldTarget_Renamed))))
            prevPrevUnitsSold = prevUnitsSold
            prevUnitsSold = unitsSold_Renamed
        End Sub
    End Class
End Namespace

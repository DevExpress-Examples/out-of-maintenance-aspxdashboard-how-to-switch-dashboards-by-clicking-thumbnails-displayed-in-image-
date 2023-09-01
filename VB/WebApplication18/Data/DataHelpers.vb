Imports System.Collections.Generic
Imports System.Data
Imports System.Collections
Imports System

Namespace DashboardMainDemo

    Public Module DataHelper

        Public Function Random(ByVal pRandom As Random, ByVal deviation As Double, ByVal positive As Boolean) As Double
            Dim rand As Integer = pRandom.Next(If(positive, 0, -1000000), 1000000)
            Return CDbl(rand) / 1000000 * deviation
        End Function

        Public Function Random(ByVal pRandom As Random, ByVal deviation As Double) As Double
            Return Random(pRandom, deviation, False)
        End Function
    End Module

    Public Class ProductClass

        Private ReadOnly productIDs As List(Of Integer) = New List(Of Integer)()

        Private ReadOnly minPrice As Decimal?

        Private ReadOnly maxPrice As Decimal?

        Private ReadOnly saleProbabilityField As Double

        Public ReadOnly Property SaleProbability As Double
            Get
                Return saleProbabilityField
            End Get
        End Property

        Public Sub New(ByVal minPrice As Decimal?, ByVal maxPrice As Decimal?, ByVal saleProbability As Double)
            Me.minPrice = minPrice
            Me.maxPrice = maxPrice
            saleProbabilityField = saleProbability
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

        Default Public Overloads ReadOnly Property Item(ByVal productID As Integer) As ProductClass
            Get
                For Each productClass As ProductClass In Me
                    If productClass.ContainsProduct(productID) Then Return productClass
                Next

                Throw New ArgumentException("procutID")
            End Get
        End Property

        Public Sub New(ByVal products As ICollection)
            Add(New ProductClass(Nothing, 100D, 0.5))
            Add(New ProductClass(100D, 500D, 0.4))
            Add(New ProductClass(500D, 1500D, 0.3))
            Add(New ProductClass(1500D, Nothing, 0.2))
            For Each product As DataRow In products
                Dim productID As Integer = CInt(product("ProductID"))
                Dim listPrice As Decimal = CDec(product("ListPrice"))
                For Each productClass As ProductClass In Me
                    If productClass.AddProduct(productID, listPrice) Then Exit For
                Next
            Next
        End Sub
    End Class

    Public Class RegionClasses
        Inherits Dictionary(Of Integer, Double)

        Public Sub New(ByVal regions As ICollection)
            Dim numberEmployeesMin As Integer? = Nothing
            For Each region As DataRow In regions
                Dim numberEmployees As Short = CShort(region("NumberEmployees"))
                numberEmployeesMin = If(numberEmployeesMin.HasValue, Math.Min(numberEmployeesMin.Value, numberEmployees), numberEmployees)
            Next

            For Each region As DataRow In regions
                Add(CInt(region("RegionID")), CShort(region("NumberEmployees")) / CDbl(numberEmployeesMin.Value))
            Next
        End Sub
    End Class

    Public Class UnitsSoldRandomGenerator

        Const MinUnitsSold As Integer = 5

        Private ReadOnly rand As Random

        Private ReadOnly startUnitsSold As Integer

        Private prevUnitsSold As Integer?

        Private prevPrevUnitsSold As Integer?

        Private unitsSoldField As Integer

        Private unitsSoldTargetField As Integer

        Private isFirst As Boolean = True

        Public ReadOnly Property UnitsSold As Integer
            Get
                Return unitsSoldField
            End Get
        End Property

        Public ReadOnly Property UnitsSoldTarget As Integer
            Get
                Return unitsSoldTargetField
            End Get
        End Property

        Public Sub New(ByVal rand As Random, ByVal startUnitsSold As Integer)
            Me.rand = rand
            Me.startUnitsSold = Math.Max(startUnitsSold, MinUnitsSold)
        End Sub

        Public Sub [Next]()
            If isFirst Then
                unitsSoldField = startUnitsSold
                isFirst = False
            Else
                unitsSoldField = unitsSoldField + CInt(Math.Round(DataHelper.Random(rand, unitsSoldField * 0.5)))
                unitsSoldField = Math.Max(unitsSoldField, MinUnitsSold)
            End If

            Dim unitsSoldSum As Integer = unitsSoldField
            Dim count As Integer = 1
            If prevUnitsSold.HasValue Then
                unitsSoldSum += prevUnitsSold.Value
                count += 1
            End If

            If prevPrevUnitsSold.HasValue Then
                unitsSoldSum += prevPrevUnitsSold.Value
                count += 1
            End If

            unitsSoldTargetField = CInt(Math.Round(CDbl(unitsSoldSum) / count))
            unitsSoldTargetField = unitsSoldTargetField + CInt(Math.Round(DataHelper.Random(rand, unitsSoldTargetField)))
            prevPrevUnitsSold = prevUnitsSold
            prevUnitsSold = unitsSoldField
        End Sub
    End Class
End Namespace

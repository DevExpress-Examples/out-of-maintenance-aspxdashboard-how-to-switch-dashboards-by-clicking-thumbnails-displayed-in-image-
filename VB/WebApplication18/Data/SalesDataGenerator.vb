Imports System
Imports System.Data

Namespace DashboardMainDemo
    Public MustInherit Class SalesDataGenerator
        Public Class Context
            Private ReadOnly st As String
            Private ReadOnly prodName As String
            Private ReadOnly catName As String
            Private ReadOnly lPrice As Decimal
            Private ReadOnly uSoldGenerator As UnitsSoldRandomGenerator

            Public ReadOnly Property State() As String
                Get
                    Return st
                End Get
            End Property
            Public ReadOnly Property ProductName() As String
                Get
                    Return prodName
                End Get
            End Property
            Public ReadOnly Property CategoryName() As String
                Get
                    Return catName
                End Get
            End Property
            Public ReadOnly Property ListPrice() As Decimal
                Get
                    Return lPrice
                End Get
            End Property
            Public ReadOnly Property UnitsSoldGenerator() As UnitsSoldRandomGenerator
                Get
                    Return uSoldGenerator
                End Get
            End Property

            Public Sub New(ByVal st As String, ByVal prodName As String, ByVal catName As String, ByVal lPrice As Decimal, ByVal uSoldGenerator As UnitsSoldRandomGenerator)
                Me.st = st
                Me.prodName = prodName
                Me.catName = catName
                Me.lPrice = lPrice
                Me.uSoldGenerator = uSoldGenerator
            End Sub
        End Class

        Protected Shared Function GetState(ByVal region As DataRow) As String
            Return DirectCast(region("Region"), String)
        End Function
        Protected Shared Function GetProductName(ByVal product As DataRow) As String
            Return DirectCast(product("Name"), String)
        End Function
        Protected Shared Function GetListPrice(ByVal product As DataRow) As Decimal
            Return DirectCast(product("ListPrice"), Decimal)
        End Function

        Private ReadOnly ds As DataSet
        Private ReadOnly categoriesTable As DataTable
        Private ReadOnly productsTable As DataTable
        Private ReadOnly regionsTable As DataTable
        Private ReadOnly rand As New Random(1)

        Private ReadOnly prodClasses_Renamed As ProductClasses

        Private ReadOnly regClasses_Renamed As RegionClasses

        Protected ReadOnly Property Regions() As DataRowCollection
            Get
                Return regionsTable.Rows
            End Get
        End Property
        Protected ReadOnly Property Products() As DataRowCollection
            Get
                Return productsTable.Rows
            End Get
        End Property
        Protected ReadOnly Property ProdClasses() As ProductClasses
            Get
                Return prodClasses_Renamed
            End Get
        End Property
        Protected ReadOnly Property RegClasses() As RegionClasses
            Get
                Return regClasses_Renamed
            End Get
        End Property
        Protected ReadOnly Property Random() As Random
            Get
                Return rand
            End Get
        End Property

        Protected Sub New(ByVal ds As DataSet)
            Me.ds = ds
            categoriesTable = ds.Tables("Categories")
            productsTable = ds.Tables("Products")
            regionsTable = ds.Tables("Regions")
            prodClasses_Renamed = New ProductClasses(productsTable.Rows)
            regClasses_Renamed = New RegionClasses(regionsTable.Rows)
        End Sub
        Protected Function GetRegionWeigtht(ByVal region As DataRow) As Double
            Return regClasses_Renamed(DirectCast(region("RegionID"), Integer))
        End Function
        Protected Function GetProductClass(ByVal product As DataRow) As ProductClass
            Return prodClasses_Renamed(DirectCast(product("ProductID"), Integer))
        End Function
        Protected Function GetCategoryName(ByVal product As DataRow) As String
            Return CStr(categoriesTable.Select(String.Format("CategoryID = {0}", product("CategoryID")))(0)("CategoryName"))
        End Function
        Protected Function CreateUnitsSoldGenerator(ByVal regionWeight As Double, ByVal productClass As ProductClass) As UnitsSoldRandomGenerator
            Return New UnitsSoldRandomGenerator(rand, CInt((Math.Ceiling(productClass.SaleProbability * regionWeight))))
        End Function
        Protected MustOverride Sub Generate(ByVal context As Context)
        Protected Overridable Sub EndGenerate()
        End Sub
        Public Sub Generate()
            For Each region As DataRow In Regions
                Dim state As String = GetState(region)
                Dim regionWeight As Double = GetRegionWeigtht(region)
                For Each product As DataRow In Products
                    Dim unitsSoldgenerator As UnitsSoldRandomGenerator = CreateUnitsSoldGenerator(regionWeight, GetProductClass(product))
                    Generate(New Context(state, GetProductName(product), GetCategoryName(product), GetListPrice(product), unitsSoldgenerator))
                Next product
            Next region
            EndGenerate()
        End Sub
    End Class
End Namespace

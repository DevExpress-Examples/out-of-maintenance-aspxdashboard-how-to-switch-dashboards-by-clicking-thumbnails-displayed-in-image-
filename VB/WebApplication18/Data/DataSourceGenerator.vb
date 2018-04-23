Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Public NotInheritable Class DataSourceGenerator

    Private Sub New()
    End Sub

    Public Shared Function CreateNWindDataSource() As DashboardSqlDataSource
        Dim dashboardSqlDataSource1 As New DashboardSqlDataSource("NWind DataSource", "NorthwindConnectionString")
        dashboardSqlDataSource1.DataProcessingMode = DataProcessingMode.Client
        dashboardSqlDataSource1.ComponentName = "dashboardSqlDataSource1"
        dashboardSqlDataSource1.Queries.Add(SelectQueryFluentBuilder.AddTable("Categories").SelectColumns("CategoryName", "Description").Join("Products", "CategoryID").SelectColumns("QuantityPerUnit", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued", "ProductName").SelectColumn("UnitPrice","Products_UnitPrice").Join("OrderDetails", "ProductName").SelectColumns("Quantity", "UnitPrice", "Discount").Join("Orders", "OrderID").SelectColumns("OrderDate", "RequiredDate", "ShippedDate", "ShipVia", "Freight", "ShipName", "ShipAddress", "ShipCity").Join("Customers", "CustomerID").SelectColumns("CompanyName", "ContactName", "ContactTitle", "City", "Region", "Country").Build("Orders"))
        Return dashboardSqlDataSource1
    End Function
End Class

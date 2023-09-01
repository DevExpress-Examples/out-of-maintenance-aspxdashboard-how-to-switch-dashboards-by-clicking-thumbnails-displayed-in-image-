Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.Sql

Public Module DataSourceGenerator

    Public Function CreateNWindDataSource() As DashboardSqlDataSource
        Dim dashboardSqlDataSource1 As DashboardSqlDataSource = New DashboardSqlDataSource("NWind DataSource", "NorthwindConnectionString")
        dashboardSqlDataSource1.DataProcessingMode = DataProcessingMode.Client
        dashboardSqlDataSource1.ComponentName = "dashboardSqlDataSource1"
        dashboardSqlDataSource1.Queries.Add(SelectQueryFluentBuilder.AddTable("Categories").SelectColumns("CategoryName", "Description").Join("Products", "CategoryID").SelectColumns("QuantityPerUnit", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued", "ProductName").SelectColumn("UnitPrice", "Products_UnitPrice").Join("OrderDetails", "ProductName").SelectColumns("Quantity", "UnitPrice", "Discount").Join("Orders", "OrderID").SelectColumns("OrderDate", "RequiredDate", "ShippedDate", "ShipVia", "Freight", "ShipName", "ShipAddress", "ShipCity").Join("Customers", "CustomerID").SelectColumns("CompanyName", "ContactName", "ContactTitle", "City", "Region", "Country").Build("Orders"))
        Return dashboardSqlDataSource1
    End Function
End Module

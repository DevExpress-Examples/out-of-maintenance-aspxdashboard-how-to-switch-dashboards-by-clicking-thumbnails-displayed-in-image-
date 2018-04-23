Imports System.Collections.Generic
Imports System.Data
Imports System
Imports DevExpress.Utils
Imports System.IO
Imports System.Web.Hosting
Imports System.Configuration
Imports DevExpress.DashboardWeb

Namespace DashboardMainDemo
    Public NotInheritable Class DataLoader

        Private Sub New()
        End Sub

        Private Shared Function GetRelativePath(ByVal name As String) As String
            Return Path.Combine(HostingEnvironment.MapPath("~"), "App_Data", "Data", name)
        End Function
        Private Shared Function LoadData(ByVal fileName As String) As DataSet
            Dim path As String = GetRelativePath(String.Format("{0}.xml", fileName))
            Dim ds As New DataSet()
            ds.ReadXml(path, XmlReadMode.ReadSchema)
            Return ds
        End Function
        Public Shared Function LoadSales() As DataSet
            Return LoadData("DashboardSales")
        End Function
        Public Shared Function LoadEmployees() As DataSet
            Return LoadData("DashboardEmployeesAndDepartments")
        End Function
        Public Shared Function LoadCustomerSupport() As DataSet
            Return LoadData("DashboardCustomerSupport")
        End Function
        Public Shared Function LoadRevenueByIndustry() As DataSet
            Return LoadData("DashboardRevenueByIndustry")
        End Function
        Public Shared Function LoadEuroEnergyStatistics() As DataSet
            Return LoadData("DashboardEnergyStatictics")
        End Function
        Public Shared Function LoadEnergyConsumptionTotal() As DataSet
            Return LoadData("DashboardEnergyConsumptionTotal")
        End Function
        Public Shared Function LoadEnergyConsumptionBySector() As DataSet
            Return LoadData("DashboardEnergyConsumptionBySector")
        End Function
        Public Shared Function GetImagesFolder() As String
            Return GetRelativePath("ProductDetailsImages")
        End Function

        Public Shared Sub LoadData(ByVal e As DataLoadingWebEventArgs)
            Select Case e.DataSourceComponentName
                Case "siteVisitosDataSource"
                    Dim data As New WebsiteStatisticsDataGenerator()
                    e.Data = data.WebsiteStatistics
                Case "dsMonthlySales"
                    Dim dataGeneratorMS As New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorMS.Generate()
                    e.Data = dataGeneratorMS.MonthlySales
                Case "dsTotalSales"
                    Dim dataGeneratorTS As New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorTS.Generate()
                    e.Data = dataGeneratorTS.TotalSales
                Case "dsKeyMetrics"
                    Dim dataGeneratorKM As New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorKM.Generate()
                    e.Data = dataGeneratorKM.KeyMetrics
                Case "dsEmployees"
                    Dim humanResourcesDataEmployees As New HumanResourcesData(LoadEmployees())
                    e.Data = humanResourcesDataEmployees.EmployeeData
                Case "dsDepartments"
                    Dim humanResourcesDataDepartments As New HumanResourcesData(LoadEmployees())
                    e.Data = humanResourcesDataDepartments.DepartmentData
                Case "dsCountries"
                    e.Data = EnergyStaticticsDataHelper.Generate(LoadEuroEnergyStatistics())
                Case "dsSales"
                    Dim salesOverviewDataGenerator As New SalesOverviewDataGenerator(LoadSales())
                    salesOverviewDataGenerator.Generate()
                    e.Data = salesOverviewDataGenerator.Data
                Case "dsSalesDetails"
                    Dim salesDetailsDataGenerator As New SalesDetailsDataGenerator(LoadSales())
                    salesDetailsDataGenerator.Generate()
                    e.Data = salesDetailsDataGenerator.Data
                Case "dsStatistics"
                    e.Data = RevenueByIndustryDataHelper.Generate(LoadRevenueByIndustry())
                Case "dsRevenueAnalysis"
                    Dim revenueAnalysisDataGenerator As New RevenueAnalysisDataGenerator(LoadSales())
                    revenueAnalysisDataGenerator.Generate()
                    e.Data = revenueAnalysisDataGenerator.Data
                Case "dsCustomerSupport"
                    Dim customerSupportData As New CustomerSupportData(LoadCustomerSupport(), LoadEmployees())
                    e.Data = customerSupportData.CustomerSupport
                Case "dsConsumptionTotal"
                    e.Data = EnergyConsumptionDataHelper.GenerateTotal(LoadEnergyConsumptionTotal())
                Case "dsConsumptionBySector"
                    e.Data = EnergyConsumptionDataHelper.GenerateBySector(LoadEnergyConsumptionBySector())
            End Select
        End Sub
    End Class
End Namespace

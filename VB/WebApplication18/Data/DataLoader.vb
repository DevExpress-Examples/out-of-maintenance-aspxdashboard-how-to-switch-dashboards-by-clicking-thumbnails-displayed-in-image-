Imports System.Data
Imports System.IO
Imports System.Web.Hosting
Imports DevExpress.DashboardWeb

Namespace DashboardMainDemo

    Public Module DataLoader

        Private Function GetRelativePath(ByVal name As String) As String
            Return Path.Combine(HostingEnvironment.MapPath("~"), "App_Data", "Data", name)
        End Function

        Private Function LoadData(ByVal fileName As String) As DataSet
            Dim path As String = GetRelativePath(String.Format("{0}.xml", fileName))
            Dim ds As DataSet = New DataSet()
            ds.ReadXml(path, XmlReadMode.ReadSchema)
            Return ds
        End Function

        Public Function LoadSales() As DataSet
            Return LoadData("DashboardSales")
        End Function

        Public Function LoadEmployees() As DataSet
            Return LoadData("DashboardEmployeesAndDepartments")
        End Function

        Public Function LoadCustomerSupport() As DataSet
            Return LoadData("DashboardCustomerSupport")
        End Function

        Public Function LoadRevenueByIndustry() As DataSet
            Return LoadData("DashboardRevenueByIndustry")
        End Function

        Public Function LoadEuroEnergyStatistics() As DataSet
            Return LoadData("DashboardEnergyStatictics")
        End Function

        Public Function LoadEnergyConsumptionTotal() As DataSet
            Return LoadData("DashboardEnergyConsumptionTotal")
        End Function

        Public Function LoadEnergyConsumptionBySector() As DataSet
            Return LoadData("DashboardEnergyConsumptionBySector")
        End Function

        Public Function GetImagesFolder() As String
            Return GetRelativePath("ProductDetailsImages")
        End Function

        Public Sub LoadData(ByVal e As DataLoadingWebEventArgs)
            Select Case e.DataSourceComponentName
                Case "siteVisitosDataSource"
                    Dim data As WebsiteStatisticsDataGenerator = New WebsiteStatisticsDataGenerator()
                    e.Data = data.WebsiteStatistics
                Case "dsMonthlySales"
                    Dim dataGeneratorMS As SalesPerformanceDataGenerator = New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorMS.Generate()
                    e.Data = dataGeneratorMS.MonthlySales
                Case "dsTotalSales"
                    Dim dataGeneratorTS As SalesPerformanceDataGenerator = New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorTS.Generate()
                    e.Data = dataGeneratorTS.TotalSales
                Case "dsKeyMetrics"
                    Dim dataGeneratorKM As SalesPerformanceDataGenerator = New SalesPerformanceDataGenerator(LoadSales())
                    dataGeneratorKM.Generate()
                    e.Data = dataGeneratorKM.KeyMetrics
                Case "dsEmployees"
                    Dim humanResourcesDataEmployees As HumanResourcesData = New HumanResourcesData(LoadEmployees())
                    e.Data = humanResourcesDataEmployees.EmployeeData
                Case "dsDepartments"
                    Dim humanResourcesDataDepartments As HumanResourcesData = New HumanResourcesData(LoadEmployees())
                    e.Data = humanResourcesDataDepartments.DepartmentData
                Case "dsCountries"
                    e.Data = EnergyStaticticsDataHelper.Generate(LoadEuroEnergyStatistics())
                Case "dsSales"
                    Dim salesOverviewDataGenerator As SalesOverviewDataGenerator = New SalesOverviewDataGenerator(LoadSales())
                    salesOverviewDataGenerator.Generate()
                    e.Data = salesOverviewDataGenerator.Data
                Case "dsSalesDetails"
                    Dim salesDetailsDataGenerator As SalesDetailsDataGenerator = New SalesDetailsDataGenerator(LoadSales())
                    salesDetailsDataGenerator.Generate()
                    e.Data = salesDetailsDataGenerator.Data
                Case "dsStatistics"
                    e.Data = RevenueByIndustryDataHelper.Generate(LoadRevenueByIndustry())
                Case "dsRevenueAnalysis"
                    Dim revenueAnalysisDataGenerator As RevenueAnalysisDataGenerator = New RevenueAnalysisDataGenerator(LoadSales())
                    revenueAnalysisDataGenerator.Generate()
                    e.Data = revenueAnalysisDataGenerator.Data
                Case "dsCustomerSupport"
                    Dim customerSupportData As CustomerSupportData = New CustomerSupportData(LoadCustomerSupport(), LoadEmployees())
                    e.Data = customerSupportData.CustomerSupport
                Case "dsConsumptionTotal"
                    e.Data = EnergyConsumptionDataHelper.GenerateTotal(LoadEnergyConsumptionTotal())
                Case "dsConsumptionBySector"
                    e.Data = EnergyConsumptionDataHelper.GenerateBySector(LoadEnergyConsumptionBySector())
            End Select
        End Sub
    End Module
End Namespace

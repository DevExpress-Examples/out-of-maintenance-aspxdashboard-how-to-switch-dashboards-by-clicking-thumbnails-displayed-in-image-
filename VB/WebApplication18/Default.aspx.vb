Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.Web
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebApplication18

    Public Partial Class [Default]
        Inherits Page

        Private dashboardsPath As String = "~/App_Data/Dashboards"

        Private thumbnailsPath As String = "~/Thumbnails"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim storage As DashboardFileStorage = New DashboardFileStorage(dashboardsPath)
            ASPxDashboard1.SetDashboardStorage(storage)
            Dim dataSourceStorage As DataSourceInMemoryStorage = New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource(CreateNWindDataSource().SaveToXml())
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
        End Sub

        Protected Sub ImageSlider_ItemDataBound(ByVal source As Object, ByVal e As ImageSliderItemEventArgs)
            e.Item.Name = Path.GetFileNameWithoutExtension(e.Item.ImageUrl)
        End Sub

        Protected Sub ASPxDashboard1_DataLoading(ByVal sender As Object, ByVal e As DataLoadingWebEventArgs)
            DashboardMainDemo.DataLoader.LoadData(e)
        End Sub

        Protected Sub ASPxCallbackPanel1_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
            Dim storage As DashboardFileStorage = New DashboardFileStorage(dashboardsPath)
            Dim dashboards = TryCast(storage, IDashboardStorage).GetAvailableDashboardsInfo().ToList()
            Dim exporter As ASPxDashboardExporter = New ASPxDashboardExporter(ASPxDashboard1)
            Dim path As String = Server.MapPath(thumbnailsPath)
            Dim di As DirectoryInfo = New DirectoryInfo(path)
            For Each file As FileInfo In di.GetFiles()
                file.Delete()
            Next

            For i As Integer = 0 To dashboards.Count - 1
                Dim fullPath As String = String.Format("{0}\{1}.png", path, dashboards(i).ID)
                Using fs As FileStream = New FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)
                    exporter.ExportToImage(dashboards(i).ID, fs, New Size(1000, 1000), Nothing, New DashboardImageExportOptions() With {.Format = DevExpress.DashboardCommon.DashboardExportImageFormat.Png})
                End Using
            Next

            ImageSlider.ImageSourceFolder = thumbnailsPath
        End Sub
    End Class
End Namespace

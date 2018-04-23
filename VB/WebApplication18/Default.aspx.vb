Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DashboardWeb.Native
Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Xml.Linq

Namespace WebApplication18
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Private dashboardsPath As String = "~/App_Data/Dashboards"
        Private thumbnailsPath As String = "~/Thumbnails"


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim storage As New DashboardFileStorage(dashboardsPath)
            ASPxDashboard1.SetDashboardStorage(storage)
            Dim dataSourceStorage As New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource(DataSourceGenerator.CreateNWindDataSource().SaveToXml())
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
        End Sub

        Protected Sub ImageSlider_ItemDataBound(ByVal source As Object, ByVal e As ImageSliderItemEventArgs)
            e.Item.Name = System.IO.Path.GetFileNameWithoutExtension(e.Item.ImageUrl)
        End Sub

        Protected Sub ASPxDashboard1_DataLoading(ByVal sender As Object, ByVal e As DataLoadingWebEventArgs)
            DashboardMainDemo.DataLoader.LoadData(e)
        End Sub

        Protected Sub ASPxCallbackPanel1_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
            Dim storage As New DashboardFileStorage(dashboardsPath)
            Dim dashboards = (TryCast(storage, IDashboardStorage)).GetAvailableDashboardsInfo().ToList()

            Dim exporter As New ASPxDashboardExporter(ASPxDashboard1)
            Dim path As String = Server.MapPath(thumbnailsPath)

            Dim di As New DirectoryInfo(path)
            For Each file As FileInfo In di.GetFiles()
                file.Delete()
            Next file

            For i As Integer = 0 To dashboards.Count - 1
                Dim fullPath As String = String.Format("{0}\{1}.png", path, dashboards(i).ID)
                Using fs As New FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)
                    exporter.ExportToImage(dashboards(i).ID, fs, New Size(1000, 1000), Nothing, New DashboardImageExportOptions() With {.Format = DevExpress.DashboardCommon.DashboardExportImageFormat.Png})
                End Using
            Next i

            ImageSlider.ImageSourceFolder = thumbnailsPath
        End Sub
    End Class
End Namespace
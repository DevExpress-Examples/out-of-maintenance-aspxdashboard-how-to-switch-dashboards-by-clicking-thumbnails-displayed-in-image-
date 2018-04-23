using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DashboardWeb.Native;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication18 {
    public partial class Default : System.Web.UI.Page {
        string dashboardsPath = @"~/App_Data/Dashboards";
        string thumbnailsPath = @"~/Thumbnails";


        protected void Page_Load(object sender, EventArgs e) {
            DashboardFileStorage storage = new DashboardFileStorage(dashboardsPath);
            ASPxDashboard1.SetDashboardStorage(storage);
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            dataSourceStorage.RegisterDataSource(DataSourceGenerator.CreateNWindDataSource().SaveToXml());
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage);
        }

        protected void ImageSlider_ItemDataBound(object source, ImageSliderItemEventArgs e) {
            e.Item.Name = System.IO.Path.GetFileNameWithoutExtension(e.Item.ImageUrl);
        }

        protected void ASPxDashboard1_DataLoading(object sender, DataLoadingWebEventArgs e) {
            DashboardMainDemo.DataLoader.LoadData(e);
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e) {
            DashboardFileStorage storage = new DashboardFileStorage(dashboardsPath);
            var dashboards = (storage as IDashboardStorage).GetAvailableDashboardsInfo().ToList();

            ASPxDashboardExporter exporter = new ASPxDashboardExporter(ASPxDashboard1);
            string path = Server.MapPath(thumbnailsPath);

            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles()) {
                file.Delete();
            }

            for (int i = 0; i < dashboards.Count; i++) {
                string fullPath = string.Format(@"{0}\{1}.png", path, dashboards[i].ID);
                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
                    exporter.ExportToImage(dashboards[i].ID, fs, new Size(1000, 1000), null, new DashboardImageExportOptions() {
                        Format = DevExpress.DashboardCommon.DashboardExportImageFormat.Png
                    });
            }

            ImageSlider.ImageSourceFolder = thumbnailsPath;
        }
    }
}
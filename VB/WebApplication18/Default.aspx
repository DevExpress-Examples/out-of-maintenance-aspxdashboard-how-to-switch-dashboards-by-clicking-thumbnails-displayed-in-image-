<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication18.Default" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Dashboard.v17.1.Web, Version=17.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .OptionsTable {
            margin-bottom: 5px;
        }

        .dxisControl .dxis-nbItem {
            width: 250px;
            height: 250px;
            background-color: transparent;
        }

            .dxisControl .dxis-nbSelectedItem,
            .dxisControl .dxis-nbSelectedItem > div,
            .dxisControl .dxis-nbItem .dxis-nbHoverItem {
                display: none !important; /* Hide Selection Frame */
            }

        .container {
            display: block;
            color: #0068bb;
            line-height: 1.2;
            border: 1px solid #ffffff;
            padding: 20px 25px 25px 25px;
            text-align: center;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
            width: 249px;
            height: 250px;
        }

            .container:hover {
                color: #dd0000;
                border: 1px solid #dddddd;
                background-color: #f8f8f8;
            }

        .name {
            font-size: 13px;
            white-space: normal;
            font-family: helvetica, arial, sans-serif;
            font-family: 'Segoe UI', Helvetica, Tahoma, Geneva, sans-serif;
        }
    </style>

    <script type="text/javascript">
        function onThumbnailItemClick(s, e) {
            dashboard.LoadDashboard(e.item.name);
        }
        function onInit(s, e) {
            callbackPanel.PerformCallback();
        }
    </script>

</head>
<body>

    <form id="form1" runat="server">
        <dx:ASPxSplitter ID="ASPxSplitter1" Orientation="Vertical" runat="server" FullscreenMode="true">
            <Panes>
                <dx:SplitterPane AutoHeight="true" ShowCollapseBackwardButton="True">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callbackPanel" OnCallback="ASPxCallbackPanel1_Callback">
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <dx:ASPxImageSlider ID="ImageSlider" runat="server" EnableViewState="False" EnableTheming="False" ImageSourceFolder="\Thumbnails" Width="100%" ShowImageArea="false" OnItemDataBound="ImageSlider_ItemDataBound">
                                            <ItemThumbnailTemplate>
                                                <div class="container">
                                                    <dx:ASPxImage runat="server" Width="200px" Height="200px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageUrl") %>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "Name") %>' ShowLoadingImage="true"></dx:ASPxImage>
                                                    <span class="name"><%# DataBinder.Eval(Container.DataItem, "Name") %></span>
                                                </div>
                                            </ItemThumbnailTemplate>
                                            <SettingsBehavior ExtremeItemClickMode="Select" />
                                            <SettingsNavigationBar ThumbnailsModeNavigationButtonVisibility="Always" ThumbnailsNavigationButtonPosition="Outside" PagingMode="Single" />
                                            <ClientSideEvents ThumbnailItemClick="onThumbnailItemClick" />
                                        </dx:ASPxImageSlider>
                                    </dx:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents Init="onInit" />
                            </dx:ASPxCallbackPanel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane>
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                            <dx:ASPxDashboard ID="ASPxDashboard1" ClientInstanceName="dashboard" Height="100%" Width="100%" OnDataLoading="ASPxDashboard1_DataLoading" runat="server">
                            </dx:ASPxDashboard>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
    </form>
</body>
</html>

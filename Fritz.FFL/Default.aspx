<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fritz.FFL._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <!--  -->

    <telerik:RadAjaxManager runat="server" ID="ajaxMgr" ClientEvents-OnRequestStart="BeginAjax">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="playerGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="playerGrid" />
                    <telerik:AjaxUpdatedControl ControlID="fflTeam" />
                    <telerik:AjaxUpdatedControl ControlID="teamPlayers" />
                    <telerik:AjaxUpdatedControl ControlID="lblAvgPts" />
                    <telerik:AjaxUpdatedControl ControlID="avgChart" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="thisFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="playerGrid" />
                    <telerik:AjaxUpdatedControl ControlID="thisFilter" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="fflTeam">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="teamPlayers" />
                    <telerik:AjaxUpdatedControl ControlID="lblAvgPts" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="teamPlayers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblAvgPts" />
                    <telerik:AjaxUpdatedControl ControlID="teamPlayers" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadSplitter runat="server" ID="split" Orientation="Vertical" Height="100%" Width="100%">
        <telerik:RadPane runat="server" Width="400" MinWidth="400">

            <telerik:RadComboBox runat="server" Width="100%" ID="fflTeam" ItemType="Fritz.FFL.Data.FantasyTeam" SelectMethod="GetFantasyTeams" DataValueField="Id" DataTextField="Owner" AutoPostBack="true" OnSelectedIndexChanged="fflTeam_SelectedIndexChanged"></telerik:RadComboBox>

            <telerik:RadGrid runat="server" ID="teamPlayers" OnNeedDataSource="teamPlayers_NeedDataSource" ClientSettings-Scrolling-UseStaticHeaders="true" Height="400" ClientSettings-Scrolling-AllowScroll="true">
                <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Bottom" AllowSorting="true">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Pos" HeaderText="Pos" HeaderStyle-Width="30" ItemStyle-Width="30"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ByeWeek" HeaderText="Bye" HeaderStyle-Width="30" ItemStyle-Width="30"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <br />


            Average Points per Week:  <asp:Label runat="server" ID="lblAvgPts"></asp:Label>

            <telerik:RadHtmlChart runat="server" Width="250px" Height="200px" ID="avgChart">
                <PlotArea>
                    <XAxis DataLabelsField="Name"></XAxis>
                    <Series>
                        <telerik:BarSeries DataFieldY="AvgPts"></telerik:BarSeries>
                    </Series>
                </PlotArea>
            </telerik:RadHtmlChart>


        </telerik:RadPane>

        <telerik:RadSplitBar runat="server"></telerik:RadSplitBar>

        <telerik:RadPane runat="server">
             <telerik:RadSplitter runat="server" id="rightSplitter" Orientation="Horizontal">
                <telerik:RadPane runat="server">

                    <telerik:RadFilter runat="server" ID="thisFilter" AllowFilterOnBlur="true" ShowAddGroupExpressionButton="true" ShowApplyButton="true" FilterContainerID="playerGrid"></telerik:RadFilter>

                    <telerik:RadGrid runat="server" ID="playerGrid" SelectMethod="GetPlayers" ItemType="Fritz.FFL.Data.ProPlayer" Width="100%" Height="800" CellSpacing="0" GridLines="None" 
                        UpdateMethod="SavePlayerChanges"
                        AutoGenerateColumns="False">

                        <MasterTableView PageSize="10" AllowPaging="true" AllowSorting="true" CommandItemDisplay="Top" DataKeyNames="Id" ClientDataKeyNames="Id" 
                            EditMode="Batch">
                            <BatchEditingSettings EditType="Cell" OpenEditingEvent="Click" />
                            <CommandItemSettings ShowExportToExcelButton="true" />
                            
                            <Columns>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="Pos" HeaderText="Pos" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="Rank" HeaderText="Rank" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="NflTeam" HeaderText="NFL" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="Name" HeaderText="Name"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="FantasyTeamId" HeaderText="FFL" HeaderStyle-Width="100" ItemStyle-Width="100">
                                    <ItemTemplate><%#: Eval("FantasyOwner") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox runat="server" ID="cmbTeam" ItemType="Fritz.FFL.Data.FantasyTeam" SelectMethod="GetFantasyTeams" DataValueField="Id" DataTextField="Owner"></telerik:RadComboBox> 
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="ByeWeek" HeaderText="Bye" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="Age" HeaderText="Age" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="Exp" HeaderText="Yrs. Exp" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ReadOnly="true" DataField="ProjectedPoints" HeaderText="Pts" ItemStyle-Width="50" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ExportSettings OpenInNewWindow="true" FileName="fflPlayers" IgnorePaging="true">
                            <Excel Format="Biff" />
                        </ExportSettings>
                    </telerik:RadGrid>

                </telerik:RadPane>
            </telerik:RadSplitter>            
        </telerik:RadPane>
    </telerik:RadSplitter>

    <script type="text/javascript"><!--

        function BeginAjax(sender, args) {

            args.set_enableAjax(args.EventTarget.indexOf("ExportToExcelButton") < 1);

        }

    //--></script>

</asp:Content>

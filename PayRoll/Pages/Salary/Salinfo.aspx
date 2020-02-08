<%@ Page AutoEventWireup="true" CodeBehind="~/Salinfo.aspx.cs" Inherits="PayRoll.Pages.Salary.Salinfo" Language="C#" MasterPageFile="~/Site.Master" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 100px">
        <h2><%: Title %></h2>
    </div>


    <div class=" row">
        <div class="col-sm-4"></div>
        <div>

            <div class=" form-horizontal">
                <div class=" row">
                    <br />
                    <div>
                        
                        <asp:Panel id="add_sec_salinfo_panel" runat="server" visible="True" class="form-horizontal" wrap="True">
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="emp" CssClass="col-md-4 control-label" Text="Select Employee" Width="155px" />

                                    <asp:DropDownList runat="server" ID="emp" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="emp_SelectedIndexChanged" />

                                </div>
                            </div>
                            <div id="add_sec_salinfo" runat="server" visible="True" class="form-horizontal" wrap="True">
                            </div>
                            <br />
                            <div class="form-horizontal" wrap="True">
                                <div class="row">
                                    <div class="col-sm-4"></div>
                                    <div style="padding-left: 60px; text-anchor: middle; horiz-align: center">
                                        <asp:Button BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="form-control" ForeColor="White" ID="update_salinfo" ClientIDMode="Inherit" runat="server" value Text="Submit" OnClick="update_salinfo_Click"/>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

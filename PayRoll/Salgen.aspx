<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Salgen.aspx.cs" Inherits="PayRoll.Salgen" %>
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
                        
                        <asp:Panel id="salgen_panel" runat="server" visible="True" class="form-horizontal" wrap="True">
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="emp" CssClass="col-md-4 control-label" Text="Select Employee" Width="155px" />

                                    <asp:DropDownList runat="server" ID="emp" CssClass="form-control"/>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="set_date" CssClass="col-md-4 control-label" Text="Select Month" Width="155px" />

                                    <asp:TextBox runat="server" ID="set_date" CssClass="form-control" TextMode="Date" />

                                </div>
                            </div>
                            <div id="salgen" runat="server" visible="True" class="form-horizontal" wrap="True">
                            </div>
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btn" BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="White" Text="Generate Salary" OnClick="btn_Click"/>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

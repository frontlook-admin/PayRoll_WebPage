<%@ Page Title="Employee Master Create" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeMasterCreate.aspx.cs" Inherits="PayRoll.Pages.EmployeeMaster.EmployeeMasterCreate" %>
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
                        
                        <asp:Panel id="empcreate_panel" runat="server" visible="True" class="form-horizontal" wrap="True">
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    

                                </div>
                            </div>
                            <div ID="empdiv" runat="server" visible="True" class="form-horizontal" wrap="True">
                            </div>
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btn" BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="White" Text="Create Employee" OnClick="btn_Click"/>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

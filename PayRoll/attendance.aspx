<%@ Page AutoEventWireup="true" CodeBehind="attendance.aspx.cs" Inherits="PayRoll.attendance" Language="C#" MasterPageFile="~/Site.Master" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
                        
                        <asp:Panel id="attendance_panel" runat="server" visible="True" class="form-horizontal" wrap="True">
                            <div class="row">
                                <div class="col-sm-2" style="padding-left:265px "></div>
                                <div class="form-group">
                                    <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="emp" CssClass="col-md-4 control-label" Text="Select Employee" Width="155px" />

                                    <asp:DropDownList runat="server" ID="emp" CssClass="form-control" AutoPostBack="True"/>

                                </div>
                            </div>
                            <div id="attendance_form" runat="server" visible="True" class="form-horizontal" wrap="True">
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-sm-4"></div>
                                    <div style="padding-left: 60px; text-anchor: middle; ">
                                        <asp:Button BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="form-control" ForeColor="White" ID="update_attendence" ClientIDMode="Inherit" runat="server" Text="Submit" OnClick="update_attendence_Click"/>
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


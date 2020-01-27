<%@ Page Title="Salary Head" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Salgen.aspx.cs" Inherits="PayRoll.Salgen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="place-items: center" align="center"></div>
    <div style="padding-left: 100px">
        <h2><%: Title %></h2>
    </div>
    <div class="row">
        <div class="col-sm-4"></div>
        <div class="col-sm-6">
            <div class=" row">
                <div class="col-md-8">

                    <div class=" form-horizontal">
                        <div class=" row">
                            <br />
                            <div class="form-group" style="padding-left: 70px;">
                                <asp:Button BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="White" ID="active_add_salaryhead_div" OnClick="Active_add_salaryhead_div_Click" runat="server" Text="Add Salary Head" />
                                <asp:Button BackColor="Silver" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="Black" ID="active_edit_salaryhead_div" OnClick="Active_edit_salaryhead_div_Click" runat="server" Text="Modify Salary Head" />
                            </div>

                            <div>
                                <asp:Panel ID="add_sec_salhead" runat="server" Width="1037px" Wrap="True">
                                    
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="add_name" CssClass="col-md-4 control-label" Text="Name" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="add_name" runat="server" CausesValidation="True" Width="188px" />
                                        </div>
                                    </div>

                                    <!--<div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" CssClass="col-md-4 control-label" AssociatedControlID="add_code" Text="Code" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="add_code" runat="server" Width="188px" />
                                        </div>
                                    </div>-->

                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="add_ddl_group" CssClass="col-md-4 control-label " Text="Group" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:DropDownList CssClass=" form-control" ID="add_ddl_group" runat="server" Width="188px" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="add_checkbox" CssClass="col-md-4 control-label " Text="Add To Salary" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:CheckBox ID="add_checkbox" TextAlign="Left" runat="server" CssClass=" checkbox" Width="188px"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="add_startdate" CssClass="col-md-4 control-label " Text="Start Date" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox ID="add_startdate" runat="server" ClientIDMode="Static" TextMode="Date" CssClass=" form-control" Width="188px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="add_formula" CssClass="col-md-4 control-label " Text="Formula" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass=" form-control" ID="add_formula" runat="server" TextMode="MultiLine" Width="100%" />
                                            <asp:ListBox ID="add_formula_list" AutoPostBack="True" CssClass="form-control list-group btn-group-justified" Width="100%" Height="150px" runat="server" OnSelectedIndexChanged="add_formula_list_SelectedIndexChanged" >
                                            </asp:ListBox>
                                        </div>
                                    </div>

                                    <div style="padding-left: 60px; text-anchor: middle; horiz-align: center">
                                        <asp:Button BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="col-md-4 form-control btn-success" ForeColor="White" ID="save_salhead" OnClick="Save_salhead_Click" runat="server" Text="Submit" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel CssClass="text-justify" HorizontalAlign="Justify" ID="editdel_sec_salhead" runat="server" Width="1035px" Wrap="True">
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="salheadid" CssClass="col-md-4 control-label" Text="Select Name" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:DropDownList AutoPostBack="True" CssClass="form-control" ID="salheadid" OnSelectedIndexChanged="Salheadid_SelectedIndexChanged" runat="server" Width="188px" />
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="edit_name" CssClass="col-md-4 control-label" Text="Name" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="edit_name" runat="server" Width="188px" />
                                            <asp:TextBox CssClass="form-control" ID="edit_oldname" runat="server" Visible="False" Width="188px" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="edit_code" CssClass="col-md-4 control-label" Text="Code" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="edit_code" ReadOnly="True" runat="server" Width="188px" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="edit_ddl_group" CssClass="col-md-4 control-label" Text="Group" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:DropDownList CssClass="col-md-8 form-control" ID="edit_ddl_group" runat="server" Width="188px" />
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="edit_checkbox" CssClass="col-md-4 control-label " Text="Add To Salary" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:CheckBox ID="edit_checkbox" runat="server" CssClass=" checkbox" Width="188px"></asp:CheckBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Height="21px" Text="Start Date" AssociatedControlID="edit_startdate" CssClass="col-md-4 control-label" Width="155px" ForeColor="Black" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="edit_startdate" runat="server" TextMode="Date" Width="188px" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ForeColor="Black" Height="21px" runat="server" AssociatedControlID="edit_formula" CssClass="col-md-4 control-label" Text="Formula" Width="155px" />
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="edit_formula" runat="server" TextMode="MultiLine" Width="300px" />
                                            <asp:ListBox ID="edit_formula_list" AutoPostBack="True" CssClass="form-control list-group btn-group-justified" Width="100%" Height="150px" runat="server" OnSelectedIndexChanged="edit_formula_list_SelectedIndexChanged" >
                                            </asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-left: 150px; text-anchor: middle; horiz-align: center">
                                        <div class="form-group">
                                            <asp:Button BackColor="#0066FF" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="White" ID="update_salhead" OnClick="Update_salhead_Click" runat="server" Text="Update" />
                                            <asp:Button BackColor="#FF3399" BorderStyle="None" Font-Bold="True" CssClass="btn" ForeColor="White" ID="del_salhead" OnClick="Del_salhead_Click" runat="server" Text="Delete" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

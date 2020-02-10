<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeMasterIndex.aspx.cs" Inherits="PayRoll.Pages.EmployeeMaster.EmployeeMasterIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="Employee Code" HeaderText="Employee Code" SortExpression="Employee Code" />
            <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" SortExpression="Employee Name" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
            <asp:BoundField DataField="Permanent Address" HeaderText="Permanent Address" SortExpression="Permanent Address" />
            <asp:BoundField DataField="Mobile No" HeaderText="Mobile No" SortExpression="Mobile No" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
            <asp:BoundField DataField="Email Id" HeaderText="Email Id" SortExpression="Email Id" />
            <asp:BoundField DataField="Join Date" HeaderText="Join Date" SortExpression="Join Date" />
            <asp:BoundField DataField="Last Working Date" HeaderText="Last Working Date" SortExpression="Last Working Date" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:payrollConnectionString %>" ProviderName="<%$ ConnectionStrings:payrollConnectionString.ProviderName %>" SelectCommand="SELECT id, `Employee Code`, `Employee Name`, Gender, `Permanent Address`, `Mobile No`, Designation, Department, `Email Id`, `Join Date`, `Last Working Date` FROM employee"></asp:SqlDataSource>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalGenIndex.aspx.cs" Inherits="PayRoll.Pages.Salary.SalGenIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
   <br/>
    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="Generated Time" HeaderText="Generated Time" SortExpression="Generated Time" />
            <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" SortExpression="Employee Name" />
            <asp:BoundField DataField="Salary Info Id" HeaderText="Salary Info Id" SortExpression="Salary Info Id" />
            <asp:BoundField DataField="Salary Date" HeaderText="Salary Date" SortExpression="Salary Date" />
            <asp:BoundField DataField="Basic Pay" HeaderText="Basic Pay" SortExpression="Basic Pay" />
            <asp:BoundField DataField="HRA" HeaderText="HRA" SortExpression="HRA" />
            <asp:BoundField DataField="DA" HeaderText="DA" SortExpression="DA" />
            <asp:BoundField DataField="Tax Deduction" HeaderText="Tax Deduction" SortExpression="Tax Deduction" />
            <asp:BoundField DataField="Attendance" HeaderText="Attendance" SortExpression="Attendance" />
            <asp:BoundField DataField="Total Salary" HeaderText="Total Salary" SortExpression="Total Salary" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:payrollConnectionString %>" ProviderName="<%$ ConnectionStrings:payrollConnectionString.ProviderName %>" SelectCommand="SELECT salary_generate.id, `Generated Time`, `Employee Name`, `Salary Info Id`, `Salary Date`, `Basic Pay`, HRA, DA, `Tax Deduction`, Attendance,`Total Salary` FROM  salary_generate INNER JOIN employee e ON salary_generate.`Employee Id`=e.id;"></asp:SqlDataSource>
    </asp:Content>

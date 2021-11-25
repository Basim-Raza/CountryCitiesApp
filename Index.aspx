<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CountryCityApp.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Country APP</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">


        <div class="p-3 mb-2 bg-secondary text-white h4">Country and City Codes</div>

        <div>

            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:TextBox ID="CountryCodeTextBox" runat="server" placeholder="Country Code"></asp:TextBox></td>
                    <td class="mr-2 d-flex justify-content-end">
                        <asp:Button ID="FindButton" runat="server" Text="Find" OnClick="FindButton_Click" />

                        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />

                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClick="DeleteButton_Click" />

                        <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="ClearButton_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="ErrorLabel1" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>

                <tr>
                    <td>
                        <asp:TextBox ID="CountryNameTextBox" runat="server" placeholder="Country Name"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="CountryNameLabel" runat="server" Text="" ForeColor="Red"></asp:Label></td>

                </tr>

                <tr>
                    <td>
                        <asp:TextBox ID="CityCodeTextBox" runat="server" placeholder="City Code"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="CityCodeLabel" runat="server" Text="" ForeColor="Red"></asp:Label></td>

                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="CityNameTextBox" runat="server" placeholder="City Name"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="CityNameLabel" runat="server" Text="" ForeColor="Red"></asp:Label></td>

                </tr>
            </table>
            <asp:GridView ID="CityGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Class="m-3">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.min.js" integrity="sha384-VHvPCCyXqtD5DqJeNxl2dtTyhF78xXNXdkwX1CZeRusQfRKp+tA7hAShOK/B/fQ2" crossorigin="anonymous"></script>
</body>
</html>

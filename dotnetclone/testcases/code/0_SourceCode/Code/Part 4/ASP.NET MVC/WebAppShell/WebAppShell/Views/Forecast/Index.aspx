<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Forecastr - The simplest weather forecast system in the Universe!
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="queryText">
<% using (Html.BeginForm("DoQuery","forecast", FormMethod.Post)) {%>
    I want to know the weather forecast for:
    <%= Html.TextBox("LocationQuery") %>
    <input type="submit" value="Get Forecast" />
<% } %>
</div>

</asp:Content>

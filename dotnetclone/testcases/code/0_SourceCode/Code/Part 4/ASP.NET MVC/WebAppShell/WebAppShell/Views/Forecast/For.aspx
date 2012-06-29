<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FSharpBook" %>
<%@ Import Namespace="FSharpBook.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%
        var forecastModel = (Forecast) ViewData.Model; 
    %>
	Forecast for <%= forecastModel.Location %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        var forecastModel = (Forecast) ViewData.Model;
    %>
    <table><tr>
    <td>
    <div id="weatherIcon">
        <img src="../../Content/<%= SkyTypeRenderer.ToImageFileName(forecastModel.Skies) %>.gif" alt="<%= SkyTypeRenderer.ToWeatherString(forecastModel.Skies) %> Graphic" />
    </div>
    <div id="weatherText">
        <%= SkyTypeRenderer.ToWeatherString(forecastModel.Skies)%>
    </div>
    </td>
    <td>
    <div id="weatherIntroText">We are forecasting that the weather in <%= forecastModel.Location %> will be:</div>
    <div id="averageTempratureDisplay">
        <%= forecastModel.AverageTemprature %> degrees with <%= SkyTypeRenderer.ToWeatherString(forecastModel.Skies)%>
    </div>
    Consider wearing <%= AttireRenderer.ToAttireString(forecastModel.ToAttire()) %>
    </td>
    </tr></table>
    <%= Html.ActionLink("Back to Index","Index","Forecast") %>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MeterMaid.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>MeterMaid</title>
    <link rel="Stylesheet" href="/Theme/Stylesheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="phone">
            <h2>
                MeterMaid</h2>
            <br />
            <h1>
                815-600-PARK</h1>
        </div>
        <div id="content">
            <h3>
                Send a text to 815-600-PARK with how much time your parking meter has left and MeterMaid
                will remind you before it expires. Example: "2 hours" or "30 minutes".
            </h3>
        </div>
        <img src="Theme/meter.png" alt="meter" id="meter" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MeterMaid.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MeterMaid</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="Stylesheet" href="/Theme/Stylesheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="phone">
                <h2>MeterMaid</h2>
                <br />
                <h1>815-600-PARK</h1>
            </div>
            <div id="content">
                <h3>Send a text to 815-600-PARK with how much time your parking meter has left and MeterMaid
                will remind you before it expires. Example: "2 hours" or "30 minutes".
                </h3>
            </div>
            <img src="Theme/meter.png" alt="meter" id="meter" />
        </div>
    </form>
</body>
</html>

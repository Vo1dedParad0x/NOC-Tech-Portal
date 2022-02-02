@Code
    ViewData("Title") = "Outage Map"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Section Styles
    <link rel="stylesheet" href="~/Content/leaflet.css" />
End Section

@Section Scripts
    <script src="~/Scripts/leaflet.js"></script>
    <script src="https://unpkg.com/esri-leaflet@2.4.1/dist/esri-leaflet.js" integrity="sha512-xY2smLIHKirD03vHKDJ2u4pqeHA7OQZZ27EjtqmuhDguxiUvdsOuXMwkg16PQrm9cgTmXtoxA6kwr8KBy3cdcw==" crossorigin=""></script>
    <script src="~/Scripts/Page scripts/outtagemap.js"></script>
End Section

<div class="row outage-header">
    <div class="col-xs-12">
        Ticketed Alarms: <span id="ticketedAlarms">0</span>
        Displayed Alarms: <span id="displayedAlarms">0</span>
        Total Alarms: <span id="totalAlarms">0</span>
    </div>
</div>
<div class="row main-container">
    <div class="col-xs-12">
        <div class="overlay">
            Loading...
        </div>
        <div id="map"></div>
    </div>
</div>
<div class="row">
    <div class="col-xs-12">
        <div id="alarmTypeControls">
            <input type="checkbox" id="DNA" name="dna" /><label for="dna">DNA</label>
            <input type="checkbox" id="DG" name="dg" /><label for="dg">DG</label>
            <input type="checkbox" id="EPF" name="epf" /><label for="epf">EPF</label>
            <input type="checkbox" id="OMS" name="oms" /><label for="oms">OMS</label>
        </div>
    </div>
</div>


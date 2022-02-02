@Code
    ViewData("Title") = "TimeMap"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Section Styles
    <link rel="stylesheet" href="~/Content/leaflet.css" />
End Section

@Section Scripts
    <script src="~/Scripts/leaflet.js"></script>
    <script src="https://unpkg.com/esri-leaflet@2.4.1/dist/esri-leaflet.js" integrity="sha512-xY2smLIHKirD03vHKDJ2u4pqeHA7OQZZ27EjtqmuhDguxiUvdsOuXMwkg16PQrm9cgTmXtoxA6kwr8KBy3cdcw==" crossorigin=""></script>
    <script src="~/Scripts/Page scripts/timemap.js"></script>
End Section

<div class="row outage-header">
    <div class="col-xs-12">
        <span id="alarmTotal">Total Alarms: 0</span>
    </div>
</div>
<div class="row main-container">
    <div class="col-xs-12">
        <div id="map"></div>
    </div>
    
</div>
<div class="row picker-container">
    <div class="col-xs-12">
        <input list="rangeOptions" type="range" id="timePicker" value=24 min=1 max=72 disabled />
        <center><output id="range">24 Hours</output></center>
        <datalist id="rangeOptions">
            <option value=4 label="4 Hours"></option>
            <option value=8 label="8 Hours"></option>
            <option value=12 label="12 Hours"></option>
            <option value=24 label="24 Hours"></option>
            <option value=36 label="36 Hours"></option>
            <option value=72 label="72 Hours"></option>
        </datalist>
    </div>
</div>


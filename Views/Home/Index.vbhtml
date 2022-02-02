@ModelType IEnumerable(Of AMSAlarms_vw)

@Code
    ViewData("Title") = "Home Page"
End Code

@section Styles
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.6.0/dist/leaflet.css"
          integrity="sha512-xwE/Az9zrjBIphAcBb3F6JVqxf46+CDLwfLMHloNu6KEQCAWi6HcDUbeOfBIptF7tcCzusKFjFw2yuvEpDL9wQ=="
          crossorigin="" />
    <link href="~/Content/Page styles/index.css" rel="stylesheet" />
end Section

@section Scripts
    <script charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-3-typeahead/4.0.2/bootstrap3-typeahead.min.js"></script>
    <script src="https://unpkg.com/leaflet@1.6.0/dist/leaflet.js"
            integrity="sha512-gZwIG9x3wUXg2hdXF6+rVkLF/0Vi9U8D2Ntg4Ga5I5BZpVkVxlJWbSQtXPSiUTtC0TjtGOmxa1AJPuV0CPthew=="
            crossorigin=""></script>
    <script src="https://unpkg.com/esri-leaflet@2.4.1/dist/esri-leaflet.js" integrity="sha512-xY2smLIHKirD03vHKDJ2u4pqeHA7OQZZ27EjtqmuhDguxiUvdsOuXMwkg16PQrm9cgTmXtoxA6kwr8KBy3cdcw==" crossorigin=""></script>
    <script src="~/Scripts/Page scripts/home.js"></script>
End Section



<div class="row index-header">
    <div class="col-xs-12" id="mapContainer">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#mapPane">Map</a></li>
            <li><a data-toggle="tab" href="#tablePane">Table</a></li>
            <li><a data-toggle="tab" href="#layerSelectPane">Layers</a></li>
        </ul>
        <div class="tab-content content-container">
            <div id="mapPane" class="tab-pane fade in active">
                <div id="map"></div>
                <div id="alarmTypeControls">
                    <input type="checkbox" id="DNA" name="dna" /><label for="dna">DNA</label>
                    <input type="checkbox" id="DG" name="dg" /><label for="dg">DG</label>
                    <input type="checkbox" id="EPF" name="epf" /><label for="epf">EPF</label>
                    <input type="checkbox" id="OMS" name="oms" /><label for="oms">OMS</label>
                </div>
            </div>
            <div id="tablePane" class="tab-pane fade">
                <div Class="table-container" id="tableMainContainer">
                    <Table id="tableMain" class="sortable">
                        <thead>
                            <tr>
                                <th>Alarm Date And Time</th>
                                <th>Alarm Type</th>
                                <th>Customer Name</th>
                                <th>Address</th>
                                <th>AMS Object</th>
                                <th>Alarm ID</th>
                                <th>APID</th>
                                <th>Ticket GUID</th>
                            </tr>
                        </thead>
                        <tbody>
                            @For Each alarm In Model
                                @<tr data-nameGuid="@alarm.CrmAccountId" data-addressGuid="@alarm.CrmLocationId">
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.AlarmDateTime)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.AlarmType)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.CusName)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.FullAddress)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.AMSObject)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.AlarmID)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.LegacyKeyId)
                                    </td>
                                    <td>
                                        @Html.DisplayFor(Function(x) alarm.EnitityGUID)
                                    </td>
                                </tr>
                            Next
                        </tbody>
                    </Table>
                </div>
            </div>
            <div id="layerSelectPane" class="tab-pane fade">
                <div class="select-container">
                    <ul id="outerSelect">
                    </ul>
                </div>
            </div>
        </div>

    </div>
</div>
<div class="row">
    <div class="col-xs-12">
        <button type="button" id="openTicketModal" class="btn btn-default right" data-toggle="modal" data-target="#createTicketModal">Create Ticket</button>
    </div>
</div>
@Html.Partial("Partial Views/TicketModal")

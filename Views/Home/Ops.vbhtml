@ModelType IEnumerable(Of AMSAlarm)

@Code
    ViewData("Title") = "Home Page"
    Layout = "~/Views/Shared/_LayoutNoLink.vbhtml"
End Code

@section Styles
    <link rel="stylesheet" href="~/Content/leaflet.css" />
end Section

@section Scripts
    <script charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-3-typeahead/4.0.2/bootstrap3-typeahead.min.js"></script>
    <script src="~/Scripts/leaflet.js"></script>
    <script src="https://unpkg.com/esri-leaflet@2.4.1/dist/esri-leaflet.js" integrity="sha512-xY2smLIHKirD03vHKDJ2u4pqeHA7OQZZ27EjtqmuhDguxiUvdsOuXMwkg16PQrm9cgTmXtoxA6kwr8KBy3cdcw==" crossorigin=""></script>
    <script src="~/Scripts/Page scripts/ops.js"></script>
End Section



<div class="row index-header">
    <div class="col-xs-12" id="mapContainer">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#mapPane">Map</a></li>
            <li><a data-toggle="tab" href="#tablePane">Alarms</a></li>
        </ul>
        <div class="tab-content content-container">
            <div id="mapPane" class="tab-pane fade in active">
                <div id="map"></div>
            </div>
            <div id="tablePane" class="tab-pane" fade>
                <div Class="table-container">
                    <Table class="tableMain">
                        <thead>
                            <tr>
                                <th> Alarm Date And Time</th>
                                <th> Alarm Type</th>
                                <th> Customer Name</th>
                                <th> Address</th>
                            </tr>
                        </thead>
                        <tbody>
                            @For Each alarm In Model
                                @<tr>
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
                                        @Html.DisplayFor(Function(x) alarm.AlarmID)
                                    </td>
                                </tr>
                            Next
                        </tbody>
                    </Table>
                </div>


            </div>
        </div>

    </div>
</div>

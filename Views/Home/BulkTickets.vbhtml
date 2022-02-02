@Code
    ViewData("Title") = "BulkTickets"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Section Scripts
    <script src="~/Scripts/Page scripts/bulktickets.js"></script>
End Section

<div class="row">
    <div class="col-xs-12">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#create">Create</a></li>
            <li><a data-toggle="tab" href="#delete">Delete</a></li>
        </ul>
        <div class="tab-content">
            <div id="create" class="table-container tab-pane fade in active">
                <table id="unticketedTable">
                    <thead>
                        <tr>
                            <th>Alarm DateTime</th>
                            <th>Address</th>
                            <th>Hut</th>
                            <th>LCP</th>
                            <th>NAP</th>
                            <th>ONT Port</th>
                            <th>Splitter</th>
                            <th>OLT Port</th>
                            <th>Account Type</th>
                            <th>Drop Type</th>
                            <th>Alarm Type</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
            <div id="delete" class="table-container tab-pane fade">
                <table id="ticketedTable">
                    <thead>
                        <tr>
                            <th>Alarm DateTime</th>
                            <th>Address</th>
                            <th>Hut</th>
                            <th>LCP</th>
                            <th>NAP</th>
                            <th>ONT Port</th>
                            <th>Splitter</th>
                            <th>OLT Port</th>
                            <th>Account Type</th>
                            <th>Drop Type</th>
                            <th>Alarm Type</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>


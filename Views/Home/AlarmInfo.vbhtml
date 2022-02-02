@Code
    ViewData("Title") = "AlarmInfo"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Section Scripts
    <script src="~/Scripts/Page scripts/alarmInfo.js"></script>

End Section
<div class="row">
    <div class="col-xs-12">
            <div class="input-group">
                <input type="text" class="form-control" placeholder="Search" id="searchTerms">
                <div class="input-group-btn">
                    <div class="btn-group">
                        <button id="searchType" class="btn btn-default dropdown-toggle" data-toggle="dropdown" type="button">
                            Search By<span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" id="searchOptions">
                            <li><a>Address</a></li>
                            <li><a>Hut</a></li>
                            <li><a>LCP</a></li>
                            <li><a>NAP</a></li>
                            <li><a>ONT Port</a></li>
                            <li><a>Splitter</a></li>
                            <li><a>OLT Port</a></li>
                            <li><a>Account Group</a></li>
                            <li><a>Drop Type</a></li>
                            <li><a>AlarmType</a></li>
                        </ul>
                    </div>
                    <button id="searchBtn" class="btn btn-default" type="button">
                        <i class="glyphicon glyphicon-search"></i>
                    </button>
                </div>
            </div>
    </div>
</div>
<div class="row main-container">
    <div class="col-xs-12">
        <div class="table-container">
            <table id="alarmTable">
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
<div class="row">
    <div class="col-xs-6">
        <label class="checkbox-inline"><input type="checkbox" value="Device Not Active"/>DNA</label>
        <label class="checkbox-inline"><input type="checkbox" value="Dying Gasp"/>DG</label>
        <label class="checkbox-inline"><input type="checkbox" value="External Power Failure"/>EPF</label>
        <label class="checkbox-inline"><input type="checkbox" value="OMS Outage Alarm"/>OMS</label>
    </div>
    <div class="col-xs-6">
        Displayed Alarms:<span id="displayedAlarms"></span>
        Total Alarms:<span id="totalAlarms"></span>
    </div>
</div>


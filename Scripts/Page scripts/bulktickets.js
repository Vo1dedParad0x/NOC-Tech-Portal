var curuntik;
var curtick;

$(document).ready(function () {
    let tasks = [];
    tasks.push($.get('api/AMSAlarms/AMSAlarmInfo', function (data) {
        loadTable($('#unticketedTable'), data);
        curuntik = data;
    }));
    tasks.push($.get('api/AMSAlarms/AMSAlarmTickets', function (data) {
        loadTable($('#ticketedTable'), data);
        curtick = data;
    }));
    await $.when(...tasks);
    




});

function loadTable(table,data) {
    let tbody = document.createElement('tbody');
    for (let i = 0; i < data.length; i++) {
        let tr = document.createElement('tr');
        ["AlarmDateTime", "Address", "Hut", "LCP", "NAP", "Port", "Splitter", "OLTPort", "AccountGroup", "DropType", "AlarmType"].forEach(function (item) {
            let td = document.createElement('td');
            td.appendChild(document.createTextNode(data[i][item]));
            tr.appendChild(td);
        });
        tbody.appendChild(tr);
    }
    table.first('tbody').remove()
    table.append(tbody)
}
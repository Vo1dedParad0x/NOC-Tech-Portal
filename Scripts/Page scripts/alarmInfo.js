var baseHref;
var activeData;
window.onload = function () {
    baseHref = $('base').attr('href');
    $.getJSON(baseHref + 'api/AMSAlarms/AMSAlarmInfo', (data) => {
        activeData = data;
        loadTable(activeData);
        $('#displayedAlarms').text(activeData.length);
        $('#totalAlarms').text(activeData.length);
        $('#searchOptions a').click(function (e) {
            $('#searchType').text($(this).text()).append('<span class="caret"></span>');
        });
        $('#searchBtn').click(function (e) {
            search($('#searchType').text(), $('#searchTerms').val());
        });
        $(':checkbox').change(function (e) {
            search($('#searchType').text(), $('#searchTerms').val());
        });
    });
    
}

function loadTable(data) {
    let tbody = document.createElement('tbody');
    for (let i = 0; i < data.length; i++) {
        let tr = document.createElement('tr');
        ["AlarmDateTime", "Address", "Hut", "LCP", "NAP", "Port", "Splitter", "OLTPort", "AccountGroup", "DropType","AlarmType"].forEach(function (item) {
            let td = document.createElement('td');
            td.appendChild(document.createTextNode(data[i][item]));
            tr.appendChild(td);
        });
        tbody.appendChild(tr);
    }
    $('#alarmTable > tbody').remove()
    $('#alarmTable').append(tbody)
}

function search(type, terms) {
    let re = new RegExp(terms, "i");
    let opts = $(':checked').map(function () { return $(this).val() }).get();
    let res = activeData.filter(function (e) { return re.test(e[type.replace(' ', '')]) });
    if (opts.length > 0) {
        res = res.filter(function (e) { return opts.includes(e.AlarmType) });
    }
    //$(':checked').each(function (i,element) {
    //    res = res.filter(function (e) { return e.AlarmType == $(element).val()});
    //})
    loadTable(res);
    $('#displayedAlarms').text(res.length);

}
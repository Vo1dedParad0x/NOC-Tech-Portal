$(document).ready(function () {
    //Add filter input before all .table-containers
    $('.table-container').before(function () {
        return `<span class='input-group table-search' id='${this.id}-search'>
                    <input type='text' class='form-control'></input>
                    <div class='input-group-btn'>
                        <div class='btn-group'>
                            <div class='dropdown'>
                                <button class="btn btn-default dropdown-toggle" type='button' data-toggle='dropdown'>${$(this).find('th').first().text()}</button>
                                <ul class='dropdown-menu'>
                                    ${$(this).find('th').map((i, e) => `<li><a>${$.trim($(e).text())}</a></li>`).get().join("")}
                                </ul>
                            </div>
                        </div>
                        <button type='button' class='btn btn-default'><i class='glyphicon glyphicon-search'></i></button>
                    </div>
                </span>`;
    });

    $('ul.dropdown-menu a').click(function () { $(this).closest('ul').siblings('button.dropdown-toggle').text($(this).text());} );


    $('.table-search .input-group-btn > button').click(function () {
        refreshTable($(`#${$(this).closest('span.table-search').attr('id').split('-')[0]} table.sortable`)[0]);
    });

    //Setup table sorting
    $('table.sortable').each(function (i, e) {
        $(e).data('orig', getTableData(e));
        $(e).data('sortData', new Map());
        $(e).find('th').click(function () {
            if ($(this).hasClass('asc')) {
                $(this).removeClass('asc');
                $(this).addClass('desc');
                $(this).closest('table').data('sortData').set($(this).text(), 'desc');
                
            }
            else if ($(this).hasClass('desc')) {
                $(this).removeClass('desc');
                $(this).closest('table').data('sortData').delete($(this).text());
            }
            else {
                $(this).addClass('asc');
                $(this).closest('table').data('sortData').set($(this).text(), 'asc');
            }
            refreshTable(e);
        });

    });

    
});

function getTableData(table) {
    let out = [];
    $(table).find('tr').each(function (i) {
        let row = [];
        $(this).find('td,th').each(function (i) {
            row.push($(this).text().trim())
        });
        out.push(row);
    });
    return out;
}

function generateTbody(data) {
    let tbody = document.createElement('tbody');
    for (let i = 0; i < data.length;i++) {
        let tr = document.createElement('tr');
        for(let j = 0; j < data[i].length; j++) {
            let td = document.createElement('td');
            td.appendChild(document.createTextNode(data[i][j]));
            tr.appendChild(td);
        }
        tbody.appendChild(tr);
    }
    //console.log(tbody);
    return tbody;
}

function customSort(jtable) {
    let data = $(jtable).data('orig').slice(1);
    var sortData = jtable.data('sortData');
    if (sortData.size = 0) {
        return data;
    }
    let out = data.sort(function (a, b) {
        let ret = 0;
        for (let key of sortData.keys()) {
            
            let kInd = jtable.find("th:contains('" + key + "')").index();
            if (String(a[kInd]).localeCompare(b[kInd]) == 0) {
                continue;
            }
            if (sortData.get(key) == 'asc') {
                ret = String(a[kInd]).localeCompare(b[kInd]);
                break;
            }
            else if (sortData.get(key) == 'desc') {
                ret = String(b[kInd]).localeCompare(a[kInd]);
                break;
            }
        }
        return ret;
    });
    out.unshift(jtable.data('orig')[0]);
    return out;
}

function search(data, terms, type) {
    if (data.length >= 2) {
        let re = new RegExp(terms, "i");
        typeInd = data[0].indexOf(type)
        let res = data.slice(1).filter(function (e) { return re.test(e[typeInd]) });
        return res;
    }
    else {
        return data.slice(1);
    }
}

function refreshTable(table) {
    let out = customSort($(table));
    out = search(out, $(`#${table.parentElement.id}-search > :text`).val(), $(`#${table.parentElement.id}-search button.dropdown-toggle`).text());
    $(table).find('tbody').replaceWith(generateTbody(out));
}
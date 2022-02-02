var map;
var layers = [];
var dnaMarkers = [];
var dgMarkers = [];
var epfMarkers = [];
var omsMarkers = [];
var baseHref;
var activeAlarms;
var hash;
$(document).ready(() => {
    var defaultLayers = [1, 2, 3, 4, 5, 7, 8];
    defaultLayers = [];
    baseHref = $('base').attr('href');
    initMainMap();

    $('tr[data-nameGuid][data-addressGuid]').each((i, e) => {
        $(e).data("nameGuid", $(e).attr("data-nameGuid")).data("addressGuid", $(e).attr("data-addressGuid")).removeAttr("data-nameGuid").removeAttr("data-addressGuid");
    });

    if (getTableData != undefined) {
        getTableData = function (table) {
            let out = [];
            $(table).find('tr').each(function (i) {
                let row = [];
                $(this).find('td,th').each(function (i) {
                    row.push($(this).text().trim())
                });
                row.push($(this).data("nameGuid"), $(this).data("addressGuid"));
                out.push(row);
            });
            return out;
        }
        $('table.sortable').each((i,e) => $(e).data('orig', getTableData(e)));
    }

    if (generateTbody != undefined) {
        generateTbody = function (data) {
            let tbody = document.createElement('tbody');
            for (let i = 0; i < data.length; i++) {
                let tr = document.createElement('tr');
                for (let j = 0; j < data[i].length - 2; j++) {
                    let td = document.createElement('td');
                    td.appendChild(document.createTextNode(data[i][j]));
                    tr.appendChild(td);
                }
                $(tr).data("nameGuid", data[i][data[i].length - 2]).data("addressGuid", data[i][data[i].length - 1]).click(function () {
                    $('.selected').removeClass('selected');
                    $(this).addClass('selected');
                    $('#openTicketModal').prop("disabled", true);
                    $('#cusName').val(this.cells[2].innerText.trim());
                    $('#cusAddress').val(this.cells[3].innerText.trim());
                    $('#alarmID').val(this.cells[5].innerText.trim())
                    $.getJSON('https://gis.lusfiber.net:6443/arcgis/rest/services/Networks1_Auto_Provisioning_Production/MapServer/exts/ProvisioningSOE/GetPlant?AddressPointID=' + this.cells[6].innerText.trim() + '&f=pjson', (data) => {
                        Object.entries(data).forEach(([key, value]) => {
                            $(`input[name='${key}']`).val(value);
                        });
                        $('#openTicketModal').prop("disabled", false);
                    });
                });
                tbody.appendChild(tr);
            }
            return tbody;
        }
    }

    $.getJSON("https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Map/MapServer?f=pjson", (data) => {
        let layerData = data.layers;
        layers = new Array(layerData.length);
        for (let i = 0; i < layerData.length; i++) {
            addFeatureLayer('https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Map/MapServer/', layerData[i].id, defaultLayers.includes(layerData[i].id) ? true : layerData[i].defaultVisibility);
            if (layerData[i].subLayerIds) {
                $('#outerSelect').append(`<li><input type='checkbox' name='layer${layerData[i].id}' id = 'layer${layerData[i].id}' value = ${layerData[i].id} /><label for='layer${layerData[i].id}'>${layerData[i].name}</label><ul></ul></li>`);
                $(`#layer${layerData[i].id}`).change(function () {
                    if (this.checked) {
                        $(this).siblings('ul').find('input[type=checkbox]').prop('checked', true).trigger("change");
                    }
                    else {
                        $(this).siblings('ul').find('input[type=checkbox]').prop('checked', false).trigger("change");
                    }
                });
                for (let j = 0; j < layerData[i].subLayerIds.length; j++) {
                    let subID = layerData[i].subLayerIds[j];
                    $(`#layer${layerData[i].id}`).siblings('ul').append(`<li><input type='checkbox' name='layer${subID}' id = 'layer${subID}' value = ${subID} /><label for='layer${subID}'>${layerData[subID].name}</label></li>`);
                    $(`#layer${subID}`).change(function () { this.checked ? layers[this.value].addTo(map) : layers[this.value].removeFrom(map) });
                }
            }
            else {
                if (layerData.filter(layer => (layer.subLayerIds || []).includes(layerData[i].id)).length == 0) {
                    $('#outerSelect').append(`<li><input type='checkbox' name='layer${layerData[i].id}' id = 'layer${layerData[i].id}' value = ${layerData[i].id} /><label for='layer${layerData[i].id}'>${layerData[i].name}</label><ul></ul></li>`);
                    $(`#layer${layerData[i].id}`).change(function () { this.checked ? layers[this.value].addTo(map) : layers[this.value].removeFrom(map) });
                }
            }
        }
    });

    let lusIcon = L.icon({ iconUrl: baseHref + "Content/Images/orange-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] })
    L.marker(L.latLng(30.226694, -92.023609),
        {
            title: "LUS Fiber",
            icon: lusIcon
        }).bindPopup("LUS Fiber <br /> 700 St. John Street Suite #300").addTo(map);

    $.getJSON(baseHref + 'api/AMSAlarms/AMSAlarms', (data) => {
        activeData = data;
        createMarkers(data);
        $('#DNA').change(function () {
            if (this.checked) {
                dnaMarkers.forEach(e => e.addTo(map));
            }
            else {
                dnaMarkers.forEach(e => e.removeFrom(map));
            }
        });
        $('#DG').change(function () {
            if (this.checked) {
                dgMarkers.forEach(e => e.addTo(map));
            }
            else {
                dgMarkers.forEach(e => e.removeFrom(map));
            }
        });
        $('#EPF').change(function () {
            if (this.checked) {
                epfMarkers.forEach(e => e.addTo(map));
            }
            else {
                epfMarkers.forEach(e => e.removeFrom(map));
            }
        });
        $('#OMS').change(function () {
            if (this.checked) {
                omsMarkers.forEach(e => e.addTo(map));
            }
            else {
                omsMarkers.forEach(e => e.removeFrom(map));
            }
        });


    });


});

function createMarkers(data) {
    let ticketIcon = L.icon({ iconUrl: baseHref + "Content/Images/green-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    let busIcon = L.icon({ iconUrl: baseHref + "Content/Images/purple-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    var govIcon = L.icon({ iconUrl: baseHref + "Content/Images/cyan-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    let electricIcon = L.icon({ iconUrl: baseHref + "Content/Images/red-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    for (let i = 0; i < data.length; i++) {
        let marker = L.marker(L.latLng(data[i].Latitude, data[i].Longitude)).bindPopup(data[i].CusName + "<br />" + data[i].FullAddress);
        if (data[i].EntityGUID) {
            marker.setIcon(ticketIcon);
        }
        else {
            switch (data[i].AccountGroupCode.trim()) {
                case "BUS":
                    marker.setIcon(busIcon);
                    break;
                case "GOV":
                    marker.setIcon(govIcon);
                    break;
            }
            if (data[i].AlarmType == 'OMS Outage Alarm') {
                marker.setIcon(electricIcon);
            }
        }

        switch (data[i].AlarmType) {
            case "Dying Gasp":
                dgMarkers.push(marker);
                break;
            case "Device Not Active":
                dnaMarkers.push(marker);
                break;
            case "External Power Failure":
                epfMarkers.push(marker);
                break;
            case "OMS Outage Alarm":
                omsMarkers.push(marker);
                break;
        }
    }
}


function addFeatureLayer(featureBase, featureID, add) {
    //Create feature layer and set default visibility
    $.getJSON(featureBase + featureID + '?f=json', (data) => {
        let featureOptions = {
            url: featureBase + featureID
        }
        if (data.parentLayer) {
            if (data.geometryType == 'esriGeometryPolyline') {
                featureOptions.style = (feature) => {
                    let style = {
                        color: '#' + data.drawingInfo.renderer.defaultSymbol.color.reduce((acc, cur) => { return acc.concat(Number(cur).toString(16).padStart(2, '0')) }, ''),
                        weight: 1
                    };
                    let valID = [feature.properties[data.drawingInfo.renderer.field1], feature.properties[data.drawingInfo.renderer.field2], feature.properties[data.drawingInfo.renderer.field3]].filter(e => e != null).join(data.drawingInfo.renderer.fieldDelimiter)
                    let specificStyle = data.drawingInfo.renderer.uniqueValueInfos.find(e => e.value == valID);
                    if (specificStyle != undefined) {
                        style.color = '#' + specificStyle.symbol.color.reduce((acc, cur) => { return acc.concat(Number(cur).toString(16).padStart(2, '0')); }, '');
                        style.weight = specificStyle.symbol.width;
                    }
                    return style;
                };
            }
            else if (data.geometryType == 'esriGeometryPoint') {
                featureOptions.pointToLayer = (feature, latlng) => {
                    let base = 'https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Map/MapServer/' + featureID + '/images/'
                    let style = {
                        iconUrl: base + data.drawingInfo.renderer.defaultSymbol.url,
                        iconSize: [data.drawingInfo.renderer.defaultSymbol.width, data.drawingInfo.renderer.defaultSymbol.height]
                    }
                    let valID = [feature.properties[data.drawingInfo.renderer.field1], feature.properties[data.drawingInfo.renderer.field2], feature.properties[data.drawingInfo.renderer.field3]].filter(e => e != null).join(data.drawingInfo.renderer.fieldDelimiter)
                    let specificStyle = data.drawingInfo.renderer.uniqueValueInfos.find(e => e.value == valID);
                    if (specificStyle != undefined) {
                        url = base + specificStyle.symbol.url;
                        style = {
                            iconUrl: base + specificStyle.symbol.url,
                            iconSize: [specificStyle.symbol.width, specificStyle.symbol.height]
                        };
                    }
                    let marker = L.marker(latlng, { icon: L.icon(style) });
                    return marker;
                };
            }
        }
        let featureLayer = L.esri.featureLayer(featureOptions);
        featureLayer.jsonData = data;
        layers[featureID] = (add ? featureLayer.addTo(map) : featureLayer);
    });
}

function initMainMap() {
    map = L.map('map').setView(L.latLng(30.226694, -92.023609), 15);
    L.esri.basemapLayer('Streets').addTo(map);
}
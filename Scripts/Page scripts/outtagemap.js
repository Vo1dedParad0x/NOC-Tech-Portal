var map;
var layers = [];
var dnaMarkers = [];
var dgMarkers = [];
var epfMarkers = [];
var omsMarkers = [];
var activeCount = 0;
var baseHref;
window.onload = () => {
    baseHref = $('base').attr('href');
    var defaultLayers = [1, 2, 3, 4, 5, 7, 8];
    defaultLayers = [];
    map = L.map('map').setView(L.latLng(30.226694, -92.023609), 15);
    L.esri.basemapLayer('Streets').addTo(map);

    $.getJSON("https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Map/MapServer?f=pjson", (data) => {
        let layerData = data.layers;
        for (let i = 0; i < layerData.length; i++) {
            addFeatureLayer('https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Map/MapServer/', layerData[i].id, defaultLayers.includes(layerData[i].id) ? true : layerData[i].defaultVisibility);
        }
    });


    let lusIcon = L.icon({ iconUrl: baseHref +"Content/Images/orange-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] })
    L.marker(L.latLng(30.226694, -92.023609),
        {
            title: "LUS Fiber",
            icon: lusIcon
        }).bindPopup("LUS Fiber <br /> 700 St. John Street Suite #300").addTo(map);

    $.getJSON(baseHref + 'api/AMSAlarms/AMSAlarms', (data) => {
        activeData = data;
        console.log(data);
        createMarkers(data);
        $('#totalAlarms').text(data.length);
        $('#ticketedAlarms').text(data.filter(function (e) { return e.EnitityGUID != null}).length);
        $('#DNA').change(function () {
            if (this.checked) {
                dnaMarkers.forEach(e => e.addTo(map));
                activeCount += dnaMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
            else {
                dnaMarkers.forEach(e => e.removeFrom(map));
                activeCount -= dnaMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
        });
        $('#DG').change(function () {
            if (this.checked) {
                dgMarkers.forEach(e => e.addTo(map));
                activeCount += dgMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
            else {
                dgMarkers.forEach(e => e.removeFrom(map));
                activeCount -= dgMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
        });
        $('#EPF').change(function () {
            if (this.checked) {
                epfMarkers.forEach(e => e.addTo(map));
                activeCount += epfMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
            else {
                epfMarkers.forEach(e => e.removeFrom(map));
                activeCount -= epfMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
        });
        $('#OMS').change(function () {
            if (this.checked) {
                omsMarkers.forEach(e => e.addTo(map));
                activeCount += omsMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
            else {
                omsMarkers.forEach(e => e.removeFrom(map));
                activeCount -= omsMarkers.length;
                $('#displayedAlarms').text(activeCount);
            }
        });
        $('.overlay').hide();
    });
}

function createMarkers(data) {
    let ticketIcon = L.icon({ iconUrl: baseHref +"Content/Images/green-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    let busIcon = L.icon({ iconUrl: baseHref +"Content/Images/purple-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    var govIcon = L.icon({ iconUrl: baseHref +"Content/Images/cyan-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    let electricIcon = L.icon({ iconUrl: baseHref + "Content/Images/red-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] });
    for (let i = 0; i < data.length; i++) {
        let marker = L.marker(L.latLng(data[i].Latitude, data[i].Longitude)).setZIndexOffset(15000).bindPopup(data[i].CusName + "<br />" + data[i].FullAddress);
        if (data[i].EnitityGUID) {
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
        //marker.addTo(map);
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
        layers[featureID] = add ? featureLayer.addTo(map) : featureLayer;
    });
}
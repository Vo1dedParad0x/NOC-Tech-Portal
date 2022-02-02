var map;
var mapservice;
var layers = [];
var baseHref;
var taData = [];
$(document).ready(() => {
    var defaultLayers = [1,2]
    baseHref = $('base').attr('href');
    map = L.map('map', {preferCanvas:true, minZoom:18, maxZoom:18, zoom:18, zoomControl:false}).setView(L.latLng(30.226694, -92.023609));
    L.esri.basemapLayer('Streets').addTo(map);

    map.setMaxBounds(L.latLngBounds(L.latLng(28.928609, -94.043147), L.latLng(33.019457, 88.817017)));

    $.getJSON("https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Fiber_Serviceability/MapServer?f=pjson", (data) => {
        let layerData = data.layers;
        layers = new Array(layerData.length);
        let jobs = []
        for (let i = 0; i < layerData.length; i++) {
            jobs.push(addFeatureLayer('https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Fiber_Serviceability/MapServer/', layerData[i].id, defaultLayers.includes(layerData[i].id) ? true : layerData[i].defaultVisibility));
        }
        $.when(jobs).done(() => {
            L.esri.Geocoding.geosearch({
                providers: [
                    L.esri.Geocoding.mapServiceProvider({
                        label:'Addresses',
                        url: 'http://gis.lusfiber.net:6080/arcgis/rest/services/LUS_Fiber_Serviceability/MapServer/',
                        layers: [1, 2],
                        searchFields: ['FULLSTREET'],
                        formatSuggestion: (feature) => {
                            return feature.properties.FULLSTREET;
                        }
                    })
                ]
            }).addTo(map).on("results", (data) => {
                map.setZoom(20);
            });
        });
    });

    L.marker(L.latLng(30.226694, -92.023609),
        {
            title: "LUS Fiber",
            icon: L.icon({ iconUrl: baseHref + "Content/Images/orange-marker.png", iconAnchor: [12.5, 40], popupAnchor: [0, -40] })
        }).bindPopup("LUS Fiber <br /> 700 St. John Street Suite #300").addTo(map);



})

function addFeatureLayer(featureBase, featureID, add) {
    //Create feature layer and set default visibility
    return $.getJSON(featureBase + featureID + '?f=json', (data) => {
        let featureOptions = {
            url: featureBase + featureID,
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
                featureOptions.pointToLayer= (feature, latlng) => {
                    let base = 'https://gis.lusfiber.com:6443/arcgis/rest/services/LUS_Fiber_Serviceability/MapServer/' + featureID + '/images/'
                    let height = data.drawingInfo.renderer.symbol.height;
                    let width = data.drawingInfo.renderer.symbol.width
                    let style = {
                        iconUrl: base + data.drawingInfo.renderer.symbol.url,
                        iconSize: [width*2, height*2],
                        iconAnchor: [width, height],
                        popupAnchor: [0, -height]
                    }
                    let marker = L.marker(latlng, { icon: L.icon(style) });
                    marker.bindPopup(feature.properties.FULLSTREET);
                    return marker;
                };
            }
        }
        let featureLayer = L.esri.featureLayer(featureOptions);
        featureLayer.jsonData = data;
        layers[featureID] = (add ? featureLayer.addTo(map) : featureLayer);
    });
}
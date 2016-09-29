var vectorSource = new ol.source.Vector({
    //create empty vector
});

function addMapMarker(markerName, longitude, latitude) {
    var iconFeature = new ol.Feature({
        geometry: new
          ol.geom.Point(ol.proj.transform([longitude, latitude], 'EPSG:4326', 'EPSG:3857')),
        name: markerName
    });
    vectorSource.addFeature(iconFeature);
}

function addMapMarkers(positions) {
    vectorSource.clear();
    $(positions).each(function () {
        addMapMarker(this.CoordinateSystem.Name, this.Coordinates.East, this.Coordinates.North);
    })
}


$(document).ready(function () {

    //create the marker icon
    var iconStyle = new ol.style.Style({
        image: new ol.style.Icon(({
            anchor: [0.5, 46],
            anchorXUnits: 'fraction',
            anchorYUnits: 'pixels',
            opacity: 1,
            src: 'http://openlayers.org/en/v3.9.0/examples/data/icon.png'
        }))
    });


    //add the feature vector to the layer vector, and apply a style to whole layer
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: iconStyle
    });

    var map = new ol.Map({
        layers: [new ol.layer.Tile({ source: new ol.source.OSM() }), vectorLayer],
        target: document.getElementById('map'),
        view: new ol.View({
            center: ol.proj.fromLonLat([10.00, 65.00]),
            zoom: 5
        })
    });


    var element = document.getElementById('popup');

    var popup = new ol.Overlay({
        element: element,
        positioning: 'bottom-center',
        stopEvent: false,
        offset: [0, -50]
    });
    map.addOverlay(popup);

    // display popup on click
    map.on('click', function (evt) {
        var feature = map.forEachFeatureAtPixel(evt.pixel,
            function (feature) {
                return feature;
            });
        if (feature) {
            $(element).popover('destroy');
            var coordinates = feature.getGeometry().getCoordinates();
            popup.setPosition(coordinates);
            $(element).popover({
                'placement': 'top',
                'html': true,
                'content': feature.get('name')
            });
            $(element).popover('show');
        } else {
            $(element).popover('destroy');
        }
    });

    // change mouse cursor when over marker
    map.on('pointermove', function (e) {
        if (e.dragging) {
            $(element).popover('destroy');
            return;
        }
        var pixel = map.getEventPixel(e.originalEvent);
        var hit = map.hasFeatureAtPixel(pixel);
        map.getTarget().style.cursor = hit ? 'pointer' : '';
    });

});
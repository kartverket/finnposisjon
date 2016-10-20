function toRadians(number) {
    return number * Math.PI / 180;
}

function distanceBetweenCoordinates(lat1, lon1, lat2, lon2) {
    var R = 6387497.792; // metres
    var φ1 = toRadians(lat1);
    var φ2 = toRadians(lat2);
    var Δφ = toRadians(lat2 - lat1);
    var Δλ = toRadians(lon2 - lon1);

    var a = Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
            Math.cos(φ1) * Math.cos(φ2) *
            Math.sin(Δλ / 2) * Math.sin(Δλ / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    var d = R * c;
    return d;
}

function getAddress(location) {
    var address = "";
    if (location.adressenavn !== undefined) address = location.adressenavn;
    else if (location.kortadressenavn !== undefined) address = location.kortadressenavn;
    else if (location.adressetilleggsnavn !== undefined) address = location.adressetilleggsnavn;
    else if (location.kortnavn !== undefined) address = location.kortnavn;
    return address;
}

function getPlace(location) {
    var place = "";
    if (location.poststed !== undefined && location.postnr !== undefined) place = location.postnr + " " + location.poststed;
    else if (location.poststed !== undefined) place = location.poststed;
    return place;
}

function getMunicipality(location) {
    var municipality = "";
    if (location.kommunenavn !== undefined) municipality = location.kommunenavn;
    return municipality;
}

function createLocationObject(closestLocation) {
    var location = {
        address: getAddress(closestLocation),
        place: getPlace(closestLocation),
        municipality: getMunicipality(closestLocation)
    }
    return location;
}

function getClosestLocation(locations, longitude, latitude) {
    var addresses = locations.adresser;
    var closestLocation;
    if (Array.isArray(locations.adresser)) {
        $(addresses).each(function (index, address) {
            var distance = distanceBetweenCoordinates(latitude, longitude, address.nord, address.aust);
            address.distanse = distance;
        });
        addresses.sort(function (a, b) {
            return a.distanse - b.distanse;
        });
        closestLocation = addresses[0];
    } else {
        closestLocation = addresses;
    }
    console.log(closestLocation);
    var location = createLocationObject(closestLocation);
    return location;
}

function clearPositionList(location) {
    $("#position-list").html("");
}

function populatePositionList(location) {
    var htmlContent = "<div class='list-item'>"
                    + "<div class='list-item-text'>"
                    + "<p class='list-item-title'>"
                    + "<span class='list-item-number'></span>"
                    + "<span class='list-item-address'>" + location.address + "</span>"
                    + "<span class='list-item-place'>" + location.place + "</span>"
                    + "<span class='list-item-municipality'>" + location.municipality + "</span>"
                    + "</p></div></div></div>";
    $("#position-list").append(htmlContent);
}



var vectorSource = new ol.source.Vector({
    //create empty vector
});

function addMapMarker(longitude, latitude) {
    longitude = parseFloat(longitude);
    latitude = parseFloat(latitude);
    var locations = {};
    var maxRequests = 4;
    var requests = 0;
    function getJsonData(radius) {
        var apiUrl = "http://ws.geonorge.no/AdresseWS/adresse/radius?nord=" + latitude + "&aust=" + longitude + "&radius=" + radius + "&antPerSide=200";

        $.getJSON(apiUrl, function (data) {
            locations = data;
        }).done(function (locations) {
            requests += 1;
            console.log(requests + ". forsøk med radius " + radius + "km")
            var address = "";
            var municipality = "";

            if (locations.totaltAntallTreff == 0 && requests <= maxRequests) {
                getJsonData(radius * 5);
            }
            else if (requests <= maxRequests) {
                console.log(apiUrl);
                console.log("Treff: " + locations.totaltAntallTreff);
                closestLocation = getClosestLocation(locations, longitude, latitude);
                populatePositionList(closestLocation);
                address = closestLocation.address;
                place = closestLocation.place;
                municipality = closestLocation.municipality;

            }
            var iconFeature = new ol.Feature({
                geometry: new
                  ol.geom.Point(ol.proj.transform([longitude, latitude], 'EPSG:4326', 'EPSG:3857')),
                name: address + " " + municipality + " " + place
            });
            vectorSource.addFeature(iconFeature);
        });
    };

    getJsonData(0.2);




}

function addMapMarkers(positions) {
    vectorSource.clear();
    clearPositionList();
    $(positions).each(function () {
        addMapMarker(this.Coordinates.East, this.Coordinates.North);
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
    /* WMS */
    /*  var wmsSource = new ol.source.TileWMS({
          url: 'http://demo.boundlessgeo.com/geoserver/wms',
          params: { 'request': 'GetMap' },
          serverType: 'geoserver',
          crossOrigin: 'anonymous'
      });
  
      var wmsLayer = new ol.layer.Tile({
          source: wmsSource
      });
  
      var view = new ol.View({
          center: [10.00, 65.00],
          zoom: 5
      });
  
      var map = new ol.Map({
          layers: [wmsLayer],
          target: 'map',
          view: view
      });
      */


    /* --- WMS --- */


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
    /* map.on('pointermove', function (e) {
         if (e.dragging) {
             $(element).popover('destroy');
             return;
         }
         var pixel = map.getEventPixel(e.originalEvent);
         var hit = map.hasFeatureAtPixel(pixel);
         map.getTarget().style.cursor = hit ? 'pointer' : '';
     });*/

});
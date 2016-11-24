var defaultMarkerIcon = "marker-icon.png";
var hoverMarkerIcon = "marker-icon-hover.png";
var selectedMarkerIcon = "marker-icon-selected.png";

function addListItemEvent(event, eventName) {
    var className = "",
        icon = "",
        identifier = (event.target.options.identifier !== undefined) ? event.target.options.identifier : false,
        listItems = $(".position-list .list-item"),
        selectedListItem = $("#list-item-" + identifier);

    if (eventName == "select") { className = "active", icon = selectedMarkerIcon }
    if (eventName == "hover") { className = "hover", icon = hoverMarkerIcon }

    $(".position-list .list-item").removeClass(className);
    if (identifier) {
        $("#list-item-" + identifier).toggleClass(className);
        setMarkerIcon(getMarker(identifier), icon);
    }
}

function removeListItemEvent(event, eventName) {
    var className = "",
        icon = defaultMarkerIcon,
        identifier = (event.target.options.identifier !== undefined) ? event.target.options.identifier : false,
        listItems = $(".position-list .list-item"),
        selectedListItem = $("#list-item-" + identifier);

    if (eventName == "select") { className = "active" }
    if (eventName == "hover") { className = "hover" }

    if (identifier) {
        $("#list-item-" + identifier).removeClass(className);
        setMarkerIcon(getMarker(identifier), icon);
    }
}

function selectFromMap(event) {
    deSelectMarkers(getMarkers());
    addListItemEvent(event, "select");
    var identifier = (event.target.options.identifier !== undefined) ? event.target.options.identifier : false;
    if (identifier) app.activeMarkerIdentifier = identifier;
}

function hoverFromMap(event) {
    addListItemEvent(event, "hover");
}

function hoverLeaveFromMap(event) {
    removeListItemEvent(event, "hover")
    if (app.activeMarkerIdentifier) setMarkerIcon(getMarker(app.activeMarkerIdentifier), selectedMarkerIcon);
}

function getMarkers() {
    var markers = [];
    map.eachLayer(function (layer) {
        if (layer.options.name === 'marker') {
            markers.push(layer);
        }
    });
    return markers;
}
function getMarker(identifier) {
    var markers = getMarkers();
    var selectedMarker = {};
    markers.forEach(function (marker) {
        if (marker.options.identifier == identifier) selectedMarker = marker;
    });
    return selectedMarker;
}

function setMarkerIcon(marker, icon) {
    var markerIcon = marker._icon;
    var imageSource = $(markerIcon).attr("src");
    var imagePath = imageSource.substr(0, imageSource.lastIndexOf('/'));
    $(markerIcon).attr("src", imagePath + "/" + icon);
}

function deSelectMarkers(markers) {
    markers.forEach(function (marker) {
        setMarkerIcon(marker, defaultMarkerIcon);
    });

}

function selectMarker(identifier) {
    deSelectMarkers(getMarkers());
    setMarkerIcon(getMarker(identifier), icon);
}
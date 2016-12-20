var defaultMarkerIcon = "map-marker.svg";
var hoverMarkerIcon = "map-marker-hover.svg";
var selectedMarkerIcon = "map-marker-active.svg";


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
    showSidebar();
    deSelectMarkers(getMarkers());
    addListItemEvent(event, "select");
    var identifier = (event.target.options.identifier !== undefined) ? event.target.options.identifier : false;
    if (identifier) {
        scrollToPositionListItem(identifier);
        app.activeMarkerIdentifier = identifier;
    }
}

function hoverFromMap(event) {
    var iconSrc = event.target._icon.currentSrc !== undefined ? event.target._icon.currentSrc : false;
    //Get iconSrc in IE browsers: 
    if (!iconSrc) { iconSrc = event.target._icon.href !== undefined ? event.target._icon.href : false };


    if (iconSrc && iconSrc.indexOf("active.svg") == -1) {
        addListItemEvent(event, "hover");
    }
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
    var marker = getMarker(identifier);
    setMarkerIcon(marker, selectedMarkerIcon);
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function parametersIsSet(parameterNames) {
    var allParametersSet = true;
    parameterNames.forEach(function (parameterName) {
        if (getParameterByName(parameterName).length == 0) {
            allParametersSet = false;
        }
    });
    return allParametersSet;
}

function getRootUrl() {
    return window.location.protocol + "//" + window.location.host;
}

function resetSidebar() {
    $("body").removeClass("has-sidebar");
}

function resetDropdown() {
    $("body").removeClass("has-dropdown");
    $(".toggle-dropdown").removeClass("active");
    $(".toggle-coordinates-input").removeClass("active");
    $(".dropdown-container").removeClass("active");
}

function resetModal() {
    $(".modal").removeClass("active");
}

function resetModalBodyScroll() {
    $(".modal-body").css("max-height", "none");
    $(".modal-body").css("overflow-y", "hidden");
}

function addModalBodyScroll(element) {
    resetModalBodyScroll();
    if (modalIsOverflowed(element)) {
        var modalBody = $(element).find(".modal-body");
        $(modalBody[0]).css("max-height", element.clientHeight - 40 + "px");
        $(modalBody[0]).css("overflow-y", "auto");
    }
}

function resetDropdownScroll() {
    $(".dropdown-body").css("max-height", "none");
    $(".dropdown-body").css("overflow-y", "hidden");
}

function addDropdownScroll(element) {
    resetDropdownScroll();
    if (dropdownIsOverflowed(element)) {
        var dropdownContent = $(element).find(".dropdown-body");
        $(dropdownContent[0]).css("max-height", $(window).height() - 50 + "px");
        $(dropdownContent[0]).css("overflow-y", "auto");
    }
}

function modalIsOverflowed(element) {
    return element.scrollHeight > element.clientHeight;
}

function dropdownIsOverflowed(element) {
    return element.scrollHeight > $(window).height() - 50;
}

function showSidebar() {
    resetDropdown();
    $("body").addClass("has-sidebar");
}

function toggleSidebar() {
    resetDropdown();
    $("body").toggleClass("has-sidebar");
}

function showCoordinatesInput() {
    resetDropdown();
    $("body").addClass("has-dropdown");
    $(".toggle-coordinates-input").addClass("active");
}

function scrollToPositionListItem(identifier) {
    var positionList = $("#results").find("#position-list");

    var scrollTopPosition = positionList.find("#list-item-A").offset().top;
    var listItemPositionVertical = positionList.find("#list-item-" + identifier).offset().top;

    var listItemPositionVerticalInsideList = listItemPositionVertical - scrollTopPosition;
    positionList.animate({
        scrollTop: listItemPositionVerticalInsideList
    }, 500);
}

function setModalBodyScroll() {
    var modalContainer = $(document).find(".modal.active .modal-container");
    addModalBodyScroll(modalContainer[0]);
}

function toggleReadMore(element) {
    var readMoreElement = $("#" + element.data("read-more"));
    element.toggleClass("active");
    readMoreElement.toggleClass("active");
}

function getBoundsPadding() {
    return $(window).width() < 520 ? [200, 0] : [300, 0];
}

$(document).on("click", ".list-item-link", function () {
    var modalContainer = $(this).closest(".list-item").find(".modal-container");
    addModalBodyScroll(modalContainer[0]);
});

$(document).on("click", ".toggle-dropdown", function () {
    var dropdownContainer = document.getElementById($(this).data("toggle"));
    addDropdownScroll(dropdownContainer);
});


$(document).on("click", ".dropdown-container .toggle-read-more", function () {
    toggleReadMore($(this));
    var dropdownContainer = $(this).closest(".dropdown-container");
    setTimeout(function () {
        addDropdownScroll(dropdownContainer[0]);
    }, 1000);
});

$(document).ready(function () {
    $(".toggle-dropdown").each(function () {
        var dropdownContainer = document.getElementById($(this).data("toggle"));
        addDropdownScroll(dropdownContainer);
    });
    if (localStorage.getItem("dont-show-description-on-startup") == undefined) {
        localStorage.setItem("dont-show-description-on-startup", false);
    }
    $("#dont-show-description-on-startup").prop("checked", localStorage.getItem("dont-show-description-on-startup") === "true");
    $("#dont-show-description-on-startup").click(function () {
        localStorage.setItem("dont-show-description-on-startup", $(this).prop("checked"));
    });
    $(document).on("click", ".toggle-coordinates-input", function () {
        resetModal();
        resetSidebar();
        if ($(".toggle-coordinates-input").hasClass("active")) {
            resetDropdown();
        } else {
            resetDropdown();
            $("body").addClass("has-dropdown");
            $(".toggle-coordinates-input").addClass("active");
        }
    });

    $(document).on("click", ".show-print", function () {
        $("body").addClass("print");
        var activeMarker = getMarker(app.activeMarkerIdentifier);
        map.invalidateSize();
        map.fitBounds(new L.LatLngBounds([activeMarker._latlng]), { paddingTopLeft: [0, 0] });
    });

    $(document).on("click", ".hide-print", function () {
        $("body").removeClass("print");
        var activeMarker = getMarker(app.activeMarkerIdentifier);
        map.invalidateSize();
        map.fitBounds(new L.LatLngBounds([activeMarker._latlng]), { paddingTopLeft: getBoundsPadding() });
    });

    $("#find-position").click(function () {
        resetDropdown();
    })

    $(".toggle-sidebar").click(function () {
        toggleSidebar();
    });

    $(".hide-dropdown").on("click", function () {
        resetDropdown();
    });

    $(".toggle-dropdown").on("click", function () {
        resetSidebar();
        resetModal();
        var elementId = $(this).data("toggle");
        if ($(this).hasClass("active")) {
            resetDropdown();
        } else {
            resetDropdown();
            $("#" + elementId).addClass("active");
            $(this).addClass("active");
        }
    });
});
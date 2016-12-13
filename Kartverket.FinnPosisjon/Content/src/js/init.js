var requiredUrlParameters = ['x', 'y'];

$(document).ready(function () {
    var autoTriggerSearch = parametersIsSet(requiredUrlParameters);
    var comprehesiveSearch = getParameterByName('comprehensive') == "true" ? true : false;
    if (autoTriggerSearch) {
        app.findPositions(comprehesiveSearch);
    }
    else {
        $("#dropdown-container-page-description").addClass("active");
        if (localStorage.getItem("dont-show-description-on-startup") === "false") {
            $("#show-page-description").addClass("active");
        } else {
            showCoordinatesInput();
        }
    }
});
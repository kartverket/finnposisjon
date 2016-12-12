var requiredUrlParameters = ['x', 'y'];

$(document).ready(function () {
    var autoTriggerSearch = parametersIsSet(requiredUrlParameters);
    if (autoTriggerSearch) app.findPositions(false);
    if (!autoTriggerSearch) {
        $("#dropdown-container-page-description").addClass("active");
        if (localStorage.getItem("dont-show-description-on-startup") === "false") {
            $("#show-page-description").addClass("active");
        } else {
            showCoordinatesInput();
        }
    }
});
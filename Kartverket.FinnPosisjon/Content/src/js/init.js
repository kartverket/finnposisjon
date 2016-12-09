var requiredUrlParameters = ['x', 'y'];

$(document).ready(function () {
    var autoTriggerSearch = parametersIsSet(requiredUrlParameters);
    if (autoTriggerSearch) app.findPositions(false);
    if (!autoTriggerSearch) {
        $("#dropdown-container-page-description").addClass("active");
        $("#show-page-description").addClass("active");
    }
});
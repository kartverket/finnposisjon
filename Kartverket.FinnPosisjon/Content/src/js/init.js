var requiredUrlParameters = ['x', 'y'];

$(document).ready(function () {
    var autoTriggerSearch = parametersIsSet(requiredUrlParameters);
    if (autoTriggerSearch) app.findPositions(false);
});
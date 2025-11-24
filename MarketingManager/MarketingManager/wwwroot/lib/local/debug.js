
const debugging = true;

function debugConsoleLog(text) {

    if (!debugging) {
        return;
    }

    console.log(text);
}

function debugDumpContactMap(title) {
    debugConsoleLog('');

    if (title) {
        debugConsoleLog('Dump Contact Map: ' + title);
        debugConsoleLog('');
    }

    contactMap.forEach(function (value) {
        debugConsoleLog('contactId: ' + value.contactId + ', companyName: ' + value.companyName + ', markerId: ' + value.markerId);
    });
}

function debugDumpMarkerMap(title) {
    debugConsoleLog('');

    if (title) {
        debugConsoleLog('Dump Marker Map: ' + title);
        debugConsoleLog('');
    }

    markerMap.forEach(function (value, key) {
        debugConsoleLog('key:: ' + key + 'value:: markerId: ' + value.markerId + ', contactId: ' + value.contactId + ', position: ' + value.position + ', draggable: ' + value.draggable);
    });
}

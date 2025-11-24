
class markerClass {
    userId;
    markerId;
    contactId;
    locationId;
    imageId;
    googlemapsMarker;
    constructor(
        userId,
        markerId,
        contactId,
        locationId,
        imageId
    ) {
        this.userId = userId;
        this.markerId = markerId;
        this.contactId = contactId;
        this.locationId = locationId;
        this.imageId = imageId;
    }

    toJson() {
        return {
            userId: this.userId,
            markerId: this.markerId,
            contactId: this.contactId,
            locationId: this.locationId,
            imageId: this.imageId
        };
    }


    addToServer() {
        let request = new XMLHttpRequest;

        request.open('POST', 'Marker/AddMarker', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&markerId=' + this.markerId + '&markerJson=' + this.toJson());
    }

    updateOnServer() {
        let request = new XMLHttpRequest;

        request.open('POST', 'Marker/UpdateMarker', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&markerId=' + this.markerId + '&markerJson=' + this.toJson());
    }

    deleteFromServer() {
        let request = new XMLHttpRequest;

        request.open('POST', 'Marker/DeleteMarker', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&markerId=' + this.markerId);
    }
}

function addMarker(userId, position, contactId,  map) {

    let location = addLocation(userId, position);

    let markerId = getNewGuid(userId);

    let marker = new markerClass(
        userId,
        markerId,
        contactId,
        location.locationId,
        null
    );

    let googlemapsMarker = new google.maps.Marker({
        position: position,
        map: map,
        markerId: markerId,
        contactId: contactId,
        draggable: true
    });

    marker.googlemapsMarker = googlemapsMarker;

    marker.addToServer();

    googlemapsMarker.addListener('dragend', () => markerDragendEventHandler(marker));

    googlemapsMarker.addListener('rightclick', () => showInfoWindow(marker));

    googlemapsMarker.addListener('mouseover', () => showInfoWindow(marker));

    googlemapsMarker.addListener('mouseout', () => closeInfoWindow());

    markerMap.set(marker.markerId, marker);

    return marker;
}

function markerDragendEventHandler(marker) {

    console.log('drag end for marker ' + marker.markerId);

    if (!marker.locationId) {
        return;
    }

    location = locationMap.get(marker.locationId);

    if (!location) {
        return;
    }

    let googlemapsMarker = marker.googlemapsMarker;

    if (!googlemapsMarker) {
        return;
    }

    location.lat = googlemapsMarker.position.lat();

    location.lng = googlemapsMarker.position.lng();

    location.updateOnServer();
}

var currInfoWindow = null;


function showInfoWindow(marker) {

    const contact = contactMap.get(marker.contactId);

    if (contact == undefined) {
        currInfoWindow = null;

        return;
    }

    var contentString = contact.infoWindowHtml;

    currInfoWindow = new google.maps.InfoWindow({
        content: contentString
    });

    currInfoWindow.open({ anchor: marker, map });
}

function closeInfoWindow(marker) {
    if (currInfoWindow === null) {
        return;
    }

    currInfoWindow.close();
}

function deleteMarker(markerId) {

    let marker = markerMap.get(markerId);

    if (!marker) {
        return;
    }

    let googlemapsMarker = marker.googlemapsMarker;

    if (googlemapsMarker) {
        googlemapsMarker.setMap(null);
    }
    
    let locationId = marker.locationId;

    if (locationId) {
        deleteLocation(locationId)
    }

    marker.deleteFromServer();

    markerMap.delete(markerId);
}

markerMap = new Map();
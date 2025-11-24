
class locationClass {
    userId;
    locationId;
    lat;
    lng;
    locationName;
    comments;
    constructor(
        userId,
        locationId,
        lat,
        lng,
        locationName,
        comments
    ) {
        this.userId = userId;
        this.locationId = locationId;
        this.lat = lat;
        this.lng = lng;
        this.locationName = locationName;
        this.comments = comments;
    }

    toJson() {
        return {
            userId: this.userId,
            locationId: this.locationId,
            lat: this.lat,
            lng: this.lng,
            locationName: this.locationName,
            comments: comments
        };
    }

    updateOnServer() {
        let request = new XMLHttpRequest;

        request.open('POST', 'Location/UpdateLocation', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&locationId=' + this.locationId + '&locationJson=' + this.toJson());
    }

    deleteFromServer() {
        let request = new XMLHttpRequest;

        request.open('POST', 'Location/DeleteLocation', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&locationId=' + this.locationId);
    }
}

function addLocation(userId, position, contactId, map) {

    let locationId = getNewGuid(userId);

    let lat = position.Lat();

    let lng = position.Lng();

    let location = new locationClass(
        userId,
        locationId,
        lat,
        lng,
        null,
        null
    );

    let googlemapsMarker = new google.maps.Location({
        position: position,
        map: map,
        markerId: markerId,
        contactId: contactId,
        draggable: true
    });

    location.addToServer();

    locationMap.set(location.locationId, location);

    return location;
}

function deleteLocation(locationId) {

    let location = markerMap.get(locationId);

    if (!location) {
        return;
    }

    location.deleteFromServer();

    locationMap.delete(locationId);
}

locationMap = new Map();

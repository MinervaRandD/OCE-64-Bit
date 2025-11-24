
var map;

var homeLat;
var homeLng;

function initializeMap(position) {
    var mapProp = {
        center: position,
        zoom: 14,
        fullscreenControl: false,
            streetViewControl: false
    };

    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    const homeButton = createHomeIcon(map);

    // Create the DIV to hold the control.
    const homeControlDiv = document.createElement("div");

    homeControlDiv.style.marginRight = "8px";

    // Create the control.
    const homeControl = createHomeIcon();

    // Append the control to the DIV.

    homeControlDiv.appendChild(homeControl);

    map.controls[google.maps.ControlPosition.TOP_RIGHT].push(homeControlDiv);

    map.addListener('mouseover', function (event) { mouseoverEventHandler(event) });

    var searchBox = new google.maps.places.SearchBox(document.getElementById('pac-input'));

    map.controls[google.maps.ControlPosition.TOP_CENTER].push(document.getElementById('pac-input'));

    searchBox.set('map', map);

    google.maps.event.addListener(searchBox, 'places_changed', function () {

        var places = searchBox.getPlaces();


        var bounds = new google.maps.LatLngBounds();
        var i, place;
        for (i = 0; place = places[i]; i++) {
            (function (place) {

                bounds.extend(place.geometry.location);


            }(place));
        }
        map.fitBounds(bounds);
        searchBox.set('map', map);
        map.setZoom(Math.min(map.getZoom(), 12));

    });

    map.addListener('dragend', mapDragendEventHandler);

    map.addListener('rightclick', mapRightClickListener);

    map.setZoom(Math.min(map.getZoom(), 12));
}

function createHomeIcon()
{
    const controlImage = document.createElement("img");

    // Set CSS for the control.
    controlImage.style.backgroundColor = "#fff";
    controlImage.style.border = "2px solid #fff";
    controlImage.style.borderRadius = "3px";
    controlImage.style.boxShadow = "0 2px 6px rgba(0,0,0,.3)";
    controlImage.style.color = "rgb(25,25,25)";
    controlImage.style.cursor = "pointer";
    controlImage.style.fontFamily = "Roboto,Arial,sans-serif";
    controlImage.style.fontSize = "16px";
    controlImage.style.lineHeight = "38px";
    controlImage.style.margin = "8px 0 22px";
    controlImage.style.padding = "0 5px";
    controlImage.style.textAlign = "center";

    controlImage.src = "/img/HomeIcon.png";

    controlImage.style.height = "40px";
    controlImage.style.width = "48px";

    controlImage.title = "Click to home map";
    controlImage.type = "button";

    controlImage.addEventListener("click", () => {
        moveMapHome();
    });

    return controlImage;
}

var draggingPin = false;

var contactId;

function pinDragStart(cid) {

    draggingPin = true;

    contactId = cid;

    console.log("Dragging pin " + cid)
}

function mouseoverEventHandler(event) {
    console.log("mouse over");

    if (draggingPin) {

        let marker = addMarker(event.latLng, contactId, map);

        let markerId = marker.markerId;

        console.log('mouseoverEventHandler, contactId = ' + contactId);

        let contact = contactMap.get(contactId);

        if (contact) {

            contact.markerId = markerId;

            addMarkerToContact(contact);
        }


        draggingPin = false;

        contactId = null;
    }
}

function mouseMoveEventHandler(coords) {
    console.log("mouse move " + coords);

    map.allowDrop();
}

function mapRightClickListener(mapsMouseEvent) {
    addMarker(mapsMouseEvent.latLng, null, this);
}

function mapDragendEventHandler() {
    recordMapCurrentCenter(this.center.lat(), this.center.lng());
}

function moveMapHome() {
    map.setCenter(new google.maps.LatLng(homeLat, homeLng));

    recordMapCurrentCenter(homeLat, homeLng);
}

function moveMapToPin(contactId) {
    console.log("move map to pin");

    const contact = contactMap.get(contactId);

    if (contact === undefined) {
        console.log('move map to pin contact not found.');
        return;
    }

    let latLng = contact.location();

    if (latLng == null) {
        console.log('move map to pin latlng not defined.');

        return;
    }

    map.setCenter(new google.maps.LatLng(latLng));

    recordMapCurrentCenter(latLng.lat(), latLng.lng());
}


class contactClass {
    userId;
    contactId;
    pointOfContact;
    companyName;
    companyAddress1;
    companyAddress2;
    companyCity;
    companyStateOrRegion;
    companyCountry;
    companyPostalCode;
    contactPhone;
    contactEmail;
    contactWebSite;
    comments;
    markerId;
    locationId;
    status;
    infoWindowHtml;
    constructor(
        userId,
        contactId,
        pointOfContact,
        companyName,
        companyAddress1,
        companyAddress2,
        companyCity,
        companyStateOrRegion,
        companyCountry,
        companyPostalCode,
        contactPhone,
        contactEmail,
        contactWebSite,
        comments,
        markerId,
        locationId,
        status
    ) {
        this.userId = userId;
        this.contactId = contactId;
        this.pointOfContact = pointOfContact;
        this.companyName = companyName;
        this.companyAddress1 = companyAddress1;
        this.companyAddress2 = companyAddress2;
        this.companyCity = companyCity;
        this.companyStateOrRegion = companyStateOrRegion;
        this.companyCountry = companyCountry;
        this.companyPostalCode = companyPostalCode;
        this.contactPhone = contactPhone;
        this.contactEmail = contactEmail;
        this.contactWebSite = contactWebSite;
        this.comments = comments;
        this.markerId = markerId;
        this.locationId = locationId;
        this.status = status;

        console.log(companyName);

        this.infoWindowHtml = generateInfoWindowHtml();
    }

    generateInfoWindowHtml() {

        var infoWindowHtmlStr = this.companyName + '<br\>';

        if (this.companyAddress1) {
            infoWindowHtmlStr += '<br\>' + this.companyAddress1;
        }

        if (this.companyAddress2) {
            infoWindowHtmlStr += '<br\>' + this.companyAddress2;
        }

        infoWindowHtmlStr += '<br\>' + this.companyCity;

        infoWindowHtmlStr += ', ' + this.companyStateOrRegion;

        if (this.contactPhone) {
            infoWindowHtmlStr += '<br\><br\>' + this.contactPhone;
        }

        return infoWindowHtmlStr;
    }

    location() {

        if (!this.locationId) {
            return null;
        }

        location = locationMap.get(this.locationId);

        return location;
    }

    toJson() {
        return {
            userId: userId,
            contactId: contactId,
            pointOfContact: pointOfContact,
            companyName: companyName,
            companyAddress1: companyAddress1,
            companyAddress2: companyAddress2,
            companyCity: companyCity,
            companyStateOrRegion: companyStateOrRegion,
            companyCountry: companyCountry,
            companyPostalCode: companyPostalCode,
            contactPhone: contactPhone,
            contactEmail: contactEmail,
            contactWebSite: contactWebSite,
            comments: comments,
            markerId: markerId,
            locationId: locationId,
            status: status
        };
    }

    updateOnServer() {

        let request = new XMLHttpRequest;

        request.open('POST', 'Contact/UpdateContact', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&contactId=' + this.contactId + '&contactJson=' + this.toJson());
    }

    deleteFromServer() {

        let request = new XMLHttpRequest;

        request.open('POST', 'Contact/DeleteContact', true);

        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.send('userId=' + this.userId + '&contactId=' + this.contactId);
    }

    addMarker(markerId) {

        this.markerId = markerId;

        updateOnServer();
    }
}

function deleteContact(contactId) {

    let contact = contactMap.get(contactId);

    if (!contact) {
        return;
    }

    let locationId = contact.locationId;

    if (locationId) {
        deleteLocation(locationId);
    }

    let markerId = contact.markerId;

    if (markerId) {
        deleteMarker(markerId);
    }

    contact.deleteFromServer();

    contactMap.delete(ContactId);
}


const contactMap = new Map();



function addMarkerToContact(contact) {
    console.log('Add marker to contact');

    document.getElementById('pin-' + contact.contactId).innerHTML =
        '<img src="img/RedLocationPin.png" draggable="false" style="user-select:none;height:24px"/>';

    let request = new XMLHttpRequest;

    request.open('POST', 'Contact/ContactAddedToMarker', true);

    request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

    request.send('userId=' + '@ViewBag.userId' + '&contactId=' + contact.contactId + '&markerId=' + contact.markerId);

}

function loadContactMapFromTextFile(text, clearCurrentMap) {

    if (!text) {
        return;
    }

    if (clearCurrentMap) {
        contactMap.clear();
    }

    const textLines = text.split(/\r?\n/);

    textLines.forEach(function (value) {

        contact = contactFromText(value);

        if (contact) {

            if (contactMap.has(contact.contactId)) {
                contactMap.delete(contact.contactId);
            }

            contactMap.set(contact.contactId, contact);
        }
    });
}


function generateInfoWindowHtml() {

    var infoWindowHtmlStr = this.companyName + '<br\>';

    if (this.companyAddress1) {
        infoWindowHtmlStr += '<br\>' + this.companyAddress1;
    }

    if (this.companyAddress2) {
        infoWindowHtmlStr += '<br\>' + this.companyAddress2;
    }

    infoWindowHtmlStr += '<br\>' + this.companyCity;

    infoWindowHtmlStr += ', ' + this.companyStateOrRegion;

    if (this.contactPhone) {
        infoWindowHtmlStr += '<br\><br\>' + this.contactPhone;
    }

    return infoWindowHtmlStr;
}

function contactFromText(text) {

    const field = text.split('\t');

    userId = field[0];
    contactId = field[1];
    pointOfContact = field[2];
    companyName = field[3];
    companyAddress1 = field[4];
    companyAddress2 = field[5];
    companyCity = field[6];
    companyStateOrRegion = field[7];
    companyCountry = field[8];
    companyPostalCode = field[9];
    contactPhone = field[10];
    contactEmail = field[11];
    contactWebSite = field[12];
    comments = field[13];
    markerId = field[14];
    locationId = field[15];
    status = field[16];

    contact = new contactClass(
        userId,
        contactId,
        pointOfContact,
        companyName,
        companyAddress1,
        companyAddress2,
        companyCity,
        companyStateOrRegion,
        companyCountry,
        companyPostalCode,
        contactPhone,
        contactEmail,
        contactWebSite,
        comments,
        markerId,
        locationId,
        status
    );

    return contact;
}

function getNewGuid(userId) {

    if (!userId) {
        return undefined;
    }

    let request = new XMLHttpRequest;

    request.open('POST', 'Common/GetNewGuid', false);

    request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

    request.send('userId=' + userId);

    guid = request.responseText;

}
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }
}
function showPosition(position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    $.ajax({
        url: '/Conhecer/SalvarCoordenadas',
        type: 'POST',
        data: JSON.stringify({ Latitude: lat, Longitude: lon }),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            //alert(data);
        },
        error: function (request, status, error) {
            //alert('oh, errors here. The call to the server is not working.')
        }
    });
}
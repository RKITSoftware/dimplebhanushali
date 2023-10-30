$(document).ready(function () {
    const apiKey = 'e0bad7f17abb489992172733231710'; // Replace with your Weather API key

    function getWeatherData(city) {
        const apiUrl = `https://api.weatherapi.com/v1/current.json?key=${apiKey}&q=${city}`;

        return $.ajax({
            url: apiUrl,
            method: 'GET',
        });
    }

    function updateWeatherUI(data) {
        $('.city').text(data.location.name);
        $('.temp').html(`${data.current.temp_c}&deg;C`);
        $('.humidity').text(`Humidity: ${data.current.humidity}%`);
        $('.wind').text(`Wind Speed: ${data.current.wind_kph} km/h`);

        const iconUrl = data.current.condition.icon;
        $('.weather-icon').attr('src', `http:${iconUrl}`);

        // Display the local time from the Weather API
        $('.local-time').text(`Local Time: ${data.location.localtime}`);
    }

    function handleError(error) {
        alert(`Error: ${error.responseJSON.error.message}`);
    }

    $('button').click(function () {
        const cityName = $('#cityName').val();
        if (cityName) {
            getWeatherData(cityName)
                .done(function (data) {
                    updateWeatherUI(data);
                })
                .fail(function (error) {
                    handleError(error);
                });
        }
    });

    $('#cityName').keypress(function (e) {
        if (e.which === 13) {
            $('button').click();
        }
    });
});

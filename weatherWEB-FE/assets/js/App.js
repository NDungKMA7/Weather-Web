const date = document.getElementById("currentDate"),
TemperatureWeather = document.getElementById("TemperatureWeather"),
WeatherText = document.getElementById("WeatherText"),
MainIcon =  document.getElementById("iconWeather"),
TemperatureWeatherTomorrow = document.getElementById("TemperatureWeatherTomorrow"),
WeatherTextTomorrow = document.getElementById("WeatherTextTomorrow"),
MainIconTomorrow =  document.getElementById("iconWeatherTomorrow"),
WarningText = document.getElementById("Warning-text"),
NumberWarningText = document.getElementById("number-warning-text"),
AirNumber = document.getElementById("air-card-number"), 
humidityMaxNumber = document.getElementById("Humidity-card-max-number"), 
humidityMinNumber = document.getElementById("Humidity-card-min-number"),
temperatureMaxNumber = document.getElementById("Temperature-card-max-number"), 
temperatureMinNumber = document.getElementById("Temperature-card-min-number"),
forecastList = document.getElementById("forecast-list")

function getDateTime() {
  let now = new Date(),
    hour = now.getHours(),
    minute = now.getMinutes();

  let days = [
    "Thứ Hai",
    "Thứ Ba",
    "Thứ Tư",
    "Thứ Năm",
    "Thứ Sáu",
    "Thứ Bảy",
    "Chủ nhật",
  ];
  // 12 hours format
  hour = hour % 12;
  if (hour < 10) {
    hour = "0" + hour;
  }
  if (minute < 10) {
    minute = "0" + minute;
  }
  let dayString = days[now.getDay()];
  return `${dayString}, ${hour}:${minute}`;
}
date.innerText = getDateTime();
setInterval(() => {
  date.innerText = getDateTime();
}, 1000);

function getDayName(date) {
  let day = new Date(date);
  let days = [
    "Chủ nhật",
    "Thứ Hai",
    "Thứ Ba",
    "Thứ Tư",
    "Thứ Năm",
    "Thứ Sáu",
    "Thứ Bảy",
  ];
  return days[day.getDay()];
}
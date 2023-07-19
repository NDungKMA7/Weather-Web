let connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7296/signalServer').build();
const axiosInstance = axios.create({
    baseURL: 'https://localhost:7296/'
});

connection.start();

connection.on("refreshData", function () {
    loadData();
});

connection.on("GetOnlyDay", (message) => {
    changeHighlightsCard(message);
});
connection.on("GetDaily", (message) => {
    showWeeksData(message);
});
connection.on("GetHourly", (message) => {
    showHoursData(message);
    changeCurrentCard(message[message.length - 1]);
});


async function loadData() {
    connection.invoke("GetDaily").catch(function (err) {
        console.error(err);
    });
    connection.invoke("GetOnlyDay").catch(function (err) {
        console.error(err);
    });
    connection.invoke("GetHourly").catch(function (err) {
        console.error(err);
    });
  
    
}
function showWeeksData(data) {
    forecastList.innerHTML = "";
    for (let i = 0; i < 6; i++) {
        let cardItem = document.createElement("li");
        cardItem.classList.add("card-item");
        let iconSrc = getIcon(data[i].rainSensor);
        let temperature = data[i].temperatureMax;
        let dayName = getDayName(data[i].day);
        let dayNumber = data[i].day;

        cardItem.innerHTML = `

        <div class="icon-wrapper">
            <img src="${iconSrc}" alt="overcast clouds"
                class="weather-icon" width="36" height="36" title="overcast clouds">

            <span class="span">
                <p class="title-2">${temperature}°<sup>c</sup></p>
            </span>
        </div>

        <p class="label-1">${dayNumber}</p>

        <p class="label-1">${dayName}</p>
  
        `;
        forecastList.appendChild(cardItem);
    }
}

let chartInstance = null;
function showHoursData(dataRc) {
    const labels = [];
    const humidity = [];
    const temperature = [];
    const rain = [];
    const air = [];

    for (let i = 0; i < dataRc.length; i++) {
        const item = dataRc[i];
        labels.push(item.hour);
        humidity.push(item.humidity);
        temperature.push(item.temperature);
        rain.push(item.rain);
        air.push(item.air);
    }

    const data = {
        labels: labels,
        humidity: humidity,
        temperature: temperature,
        rain: rain,
        air: air
    };

    const ctx = document.getElementById('chart');
    if (chartInstance != null) {
        chartInstance.destroy();
    }
    chartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            labels: data.labels,
            datasets: [
                {
                    label: 'Humidity (%)',
                    data: data.humidity,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1,
                    fill: false
                },
                {
                    label: 'Temperature (°C)',
                    data: data.temperature,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                    fill: false

                },
                {
                    label: 'Rain (mm)',
                    data: data.rain,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1,
                    fill: false
                },
                {
                    label: 'Air Quality',
                    data: data.air,
                    backgroundColor: 'rgba(255, 206, 86, 0.2)',
                    borderColor: 'rgba(255, 206, 86, 1)',
                    borderWidth: 1,
                    fill: false
                }
            ]
        },
        options: {
            legend: { display: false }
        }
    });




}

function changeCurrentCard(data) {
    MainIcon.src = getIcon(data.rain);
    WeatherText.innerText = Rain(data.rain);
    TemperatureWeather.innerText = data.temperature + "°c";
    WarningText.innerText = "Đang xử lý thông tin và đưa ra cảnh báo!";
    // GetWaringOpenAI(data, data.rain);
    NumberWarningText.innerText = `Nhiệt độ: ${data.temperature} °c, Độ ẩm: ${data.humidity} %, Không Khí: ${data.air}: `;
}
function changeTomorrowCard(data) {

    TemperatureWeatherTomorrow.innerText = data.temperature;

}
function changeHighlightsCard(data) {
    AirNumber.innerText = data.airSensor;
    humidityMaxNumber.innerText = data.humidityMax;
    humidityMinNumber.innerText = data.humidityMin;
    temperatureMaxNumber.innerText = data.temperatureMax;
    temperatureMinNumber.innerText = data.temperatureMin;
}


function getIcon(condition) {
    if (condition == 1) {
        return './assets/images/weather_icons/02d.png';
    } else {
        return './assets/images/weather_icons/09d.png';
    }
}

function Rain(condition) {
    if (condition == 1) {
        return "trời nắng";
    } else {
        return "trời mưa";
    }
}

async function GetWaringOpenAI(data, rain) {
    try {
        const response = await axios.post('https://localhost:7296/Home/GetResultOpenAI', {
            "body": `Hãy phân tích vấn đề về sức khỏe dựa trên các thông số về thời tiết như sau: Độ ẩm:  ${data.humidity}, Nhiệt độ  ${data.temperature} ${rain}, chất lượng không khí : ${data.air} ppm/co2 . Câu trả lời chỉ giới hạn 50 từ`,
        });
        WarningText.innerText = response.data;
    } catch (error) {
        if (error.response && error.response.status !== 200) {
            WarningText.innerText = "Chúng tôi đang phân tích!";
        }
    }
}
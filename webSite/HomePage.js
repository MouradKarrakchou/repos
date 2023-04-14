
let display;
let headersButton;
let ageButton;
let responseTimeButton;
let securityCheckButton;
let cacheCheckButton;

window.addEventListener('load', function() {
    display=document.getElementById("display");


    headersButton=document.getElementById("headersButton");
    ageButton=document.getElementById("ageButton");
    responseTimeButton=document.getElementById("responseTimeButton");
    securityCheckButton=document.getElementById("securityCheckButton");
    cacheCheckButton=document.getElementById("cacheCheckButton");

    headersButton.addEventListener('click', ()=>callToServer(valueHandler,'values'));
    ageButton.addEventListener('click', ()=>callToServer(ageHandler,'age'));
    responseTimeButton.addEventListener('click', ()=>callToServer(timeResponseHandler,'timeresponse'));
    securityCheckButton.addEventListener('click',()=>callToServer(securityCheckHandler,'securitytest'));
        cacheCheckButton.addEventListener('click',()=>callToServer(cacheHandler,'cache'))

});

function callToServer(func,adress) {
    fetch('http://localhost:9000/api/'+adress)
        .then(response => response.json())
        .then(func)
        .catch(error => {
            console.error(error);
        });
    pending()
}

function timeResponseHandler(data){
    display.innerHTML='';
    let dataParsed=JSON.parse(data);
    displayChart(convertToSeconds(dataParsed),0.01,'Response time of each url(with 5 tries each)');
    console.log(dataParsed);
}


function valueHandler(data){
        display.innerHTML='';
        displayChart(JSON.parse(data),1,'Number of websites with this type of Server');
}
function securityCheckHandler(data){
    display.innerHTML='';
    displayChart(JSON.parse(data),1,'Security check on 4 headers');
}
function cacheHandler(data){
    display.innerHTML='';
    displayChart(JSON.parse(data),1,'Number of occurrence of cache directives');
}

function ageHandler(data){
    {
        display.innerHTML='';
        const heading = document.createElement("h2");
        let dataParsed=JSON.parse(data);
        let average=dataParsed.average;
        let standardDeviation=dataParsed.standardDeviation;
        heading.textContent = "The average age is " + average.hour + " hours, " + average.minute + " minutes, and " + average.second + " seconds. The standard deviation is " + standardDeviation.hour + " hours, " + standardDeviation.minute + " minutes, and " + standardDeviation.second + " seconds.";
        display.appendChild(heading);
    }
}







function pending(){
    display.innerHTML='';
    const heading = document.createElement("h2");
    heading.textContent = "Pending";
    display.appendChild(heading);
}
function displayChart(data,stepSize,label){
    display.innerHTML='<canvas id="myChart"></canvas>';
    const ctx = document.getElementById('myChart');

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Object.keys(data),
            datasets: [{
                label: label,
                data: Object.values(data),
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: stepSize
                    }
                }
            }
        }
    })
}
function convertToSeconds(data) {
    const newData = {};
    for (const [key, value] of Object.entries(data)) {
        const [hours, minutes, seconds] = value.split(':').map(parseFloat);
        const totalSeconds = hours * 3600 + minutes * 60 + seconds;
        newData[key] = totalSeconds.toFixed(2);
    }
    return newData;
}

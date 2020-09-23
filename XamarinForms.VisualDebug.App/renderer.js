// This file is required by the index.html file and will
// be executed in the renderer process for that window.
// No Node.js APIs are available in this process because
// `nodeIntegration` is turned off. Use `preload.js` to
// selectively enable features needed in the rendering
// process.
window.$ = window.jQuery = require('jquery');
var data = require("./data.js");
var orgChart = require("./chart.js");




//
document.addEventListener('DOMContentLoaded', function () {
    //const lineBtn = document.getElementById('myButton1');

    //lineBtn.addEventListener('click', function (event) {
    //    orgChart.initTree({ data: data.u_data, modus: "line", loadFunc: data.loadChilds, id: '#body' });
    //});

    //const diagonalBtn = document.getElementById('myButton2');

    //diagonalBtn.addEventListener('click', function (event) {
    //    orgChart.initTree({ data: data.u_data, modus: "diagonal", loadFunc: data.loadChilds, id: '#body' });
    //});

    orgChart.initTree({ data: data.u_data, modus: "diagonal", loadFunc: data.loadChilds, id: '#body' });

});
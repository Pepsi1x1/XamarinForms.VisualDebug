const { ipcMain } = require('electron')
const express = require('express');
const https = require('https');
const bodyParser = require('body-parser');
const orgChart = require("../renderer/chart.js");
const fs = require('fs');
const app = express();
const port = 3000;
const { inspect } = require('util');

let treedatas = [];

var setupServer = function (window) {
    
    app.use(bodyParser.json({ limit: '50mb' }));

    const replacer = function (key, value) {
        if (key == 'parent') {
            return undefined;
        }
        return value;
    };

    app.route('/treedatas')
        .get((req, res) => {
            //console.log(JSON.stringify(treedatas, replacer));
            res.send(JSON.stringify(treedatas, replacer));
        })
        .post((req, res) => {
            //console.log(req.body);
            //const newdata = JSON.parse(req.body);
            const newdata = req.body;
            const newElement = { ...newdata, id: treedatas.length + 1 };
            treedatas = [...treedatas, newElement];
            
            sendTreeDataToRenderer(newdata, window);

            res.status(200);
            res.send();
            
        });

    var key = fs.readFileSync(__dirname + '/../certs/selfsigned.key');
    var cert = fs.readFileSync(__dirname + '/../certs/selfsigned.crt');
    var options = {
        key: key,
        cert: cert
    };

    var treeServer = https.createServer(options, app).listen(port, () => console.log(`Example app listening on port ${port}!`));
    //app.listen(port, () => console.log(`Example app listening on port ${port}!`));

}

var sendLastTreeDataToRenderer = function (window) {
    console.log('treeDataChanged sent');
    const treedata = treedatas[treedatas.length - 1];
    window.webContents.send('treeDataChanged', treedata);
    //orgChart.initTree({ data: treedata, modus: "diagonal", loadFunc: loadChilds, id: '#body' });
};

var sendTreeDataToRenderer = function (newdata, window) {
    console.log('treeDataChanged sent');
    window.webContents.send('treeDataChanged', newdata);
    //orgChart.initTree({ data: newdata, modus: "diagonal", loadFunc: loadChilds, id: '#body' });
};

module.exports = {
    setup(window) {
        setupServer(window);
    }
};


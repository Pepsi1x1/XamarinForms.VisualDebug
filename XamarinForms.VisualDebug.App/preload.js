// All of the Node.js APIs are available in the preload process.
// It has the same sandbox as a Chrome extension.
window.addEventListener('DOMContentLoaded', () => {
    const replaceText = (selector, text) => {
        const element = document.getElementById(selector)
        if (element) element.innerText = text
    }

    for (const type of ['chrome', 'node', 'electron']) {
        replaceText(`${type}-version`, process.versions[type])
    }
})

//const myserver = require("./main/server.js");

//myserver.setup();

const polo = require('polo');
const apps = polo({
    host: '224.0.0.251',
    port: 5353
});

console.log(polo);

apps.put({
    name: '_treeserver._tcp.local.',
    port: 3000
});
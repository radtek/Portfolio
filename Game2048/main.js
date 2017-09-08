const {app, BrowserWindow, Menu, MenuItem} = require('electron')
const url = require('url')
const path = require('path')
const config = require('./package.json')

let win

app.on('ready', () => {	
    createWindow()
})

function createWindow() {
   win = new BrowserWindow({//titleBarStyle: 'hidden',
        width: 530, 
        height: 740, 
        minWidth: 530,
        minHeight: 740,
        resizable: false,
        title: config.windowtitle,
        //backgroundColor: '#312450',
        //show: false,
        icon: path.join(__dirname, 'build/icon.icns') })
   //win.setTitle(require('./package.json').name);
   win.loadURL(url.format ({
      pathname: path.join(__dirname, 'index.html'),
      protocol: 'file:',
      slashes: true
   }))
   setMainMenu();
}

function setMainMenu() {
    const template = [
        {
            label: '2048',
            submenu: [
                {
                    role: 'undo'
                }
            ]
        },
        {
            label: 'View',
            submenu: [
                {
                    role: 'reload'
                },
                {
                    role: 'toggledevtools'
                },
                {
                    type: 'separator'
                },
                {
                    role: 'resetzoom'
                },
                {
                    type: 'separator'
                }
            ]
        },
        {
            role: 'window',
            submenu: [
                {
                    role: 'minimize'
                },
                {
                    role: 'close'
                }
            ]
        },
        {
            label: 'Help',
            role: 'help',
            submenu: [{
                label: 'Learn More',
                click: function () {
                    electron.shell.openExternal('http://jojozhuang.github.io/blog/2016/11/03/build-cross-platform-apps-with-electron/')
                }
            }]
       }
    ];
    const menu = Menu.buildFromTemplate(template)
    Menu.setApplicationMenu(menu)
}
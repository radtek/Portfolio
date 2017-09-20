var path = require("path");
var index = require('./model/index');
var screenimage = require('./model/screenimage');
var wbdata = require('./model/wbdata');
var filehelper = require('./util/filehelper');
var fs = require('fs');

//var hashTable = require("node-hashtable");

var ssIndexFile = path.join(__dirname, '/204304/ScreenShot/High/package.pak');
var unzippedSsIndexFile = path.join(__dirname, '/204304/ScreenShot/High/unzippedindex.pak');
var imagedatafile = path.join(__dirname, '/204304/ScreenShot/High/1.pak');
var wbImageIndexFile = path.join(__dirname, '/204304/WB/1/VectorImage/package.pak');
var unzippedWbImageIndexFile = path.join(__dirname, '/204304/WB/1/VectorImage/unzippedindex.pak');
var wbImageDataFile = path.join(__dirname, '/204304/WB/1/VectorImage/1.pak');
var wbSequenceIndexFile = path.join(__dirname, '/204304/WB/1/VectorSequence/package.pak');
var unzippedWbSequenceIndexFile = path.join(__dirname, '/204304/WB/1/VectorSequence/unzippedindex.pak');
var wbSequenceDataFile = path.join(__dirname, '/204304/WB/1/VectorSequence/1.pak');

exports.getImageData = function (second, callback) {
  //filehelper.unzipIndexFile(ssIndexFile, unzippedSsIndexFile);
  var buffer = filehelper.getIndexFile(unzippedSsIndexFile);
  //console.log(buffer.length);
  //console.log(buffer);
  var indexList = filehelper.getIndexArray(buffer);
  //console.log(indexList.length);
  var count =0;
  var hashmap = [];
  for (var i = 0; i < indexList.length; i++)
  {
    if(!hashmap[indexList[i].timestamp]) {
      count++;
      hashmap[indexList[i].timestamp] = i;
      ///console.log('hashmap.length=' + hashmap.length);
      //if (count < 10) {
      //  console.log(indexList[i].timestamp + '-' + i);
        //console.log(hashmap[indexList[i].timestamp]);
      //}
    }
  }
  /*
  var hashtable = [];
  var index = 0;
  for (var i = 0; i < hashmap.length; i++) {
    if (hashmap[i]) {
      hashtable[index] = hashmap[i];
      index++;
    }
  }
  //hashtable.sort(sortNumber);
  console.log(hashtable[3]);
  for (var i = 0; i < hashtable.length; i++) {
    if (i > 10) {
      break;
    }
    console.log(hashtable[i]);
  }
  console.log(hashtable.length);
  */

  var imageIndex = filehelper.getImageIndex(hashmap, indexList, second);

  filehelper.getImageData(imagedatafile, imageIndex, callback);

}

exports.getWhiteBoardData = function (second, callback) {
  var lines = getWBImageData(second);
  var events = getWBSequenceData(second);
  var res = new wbdata(second, lines, events);
  var json = JSON.stringify(res);
  callback(json);
  /*
  if (lines&&lines.length>0) {
    for (var i = 0; i < lines.length; i++) {
      var line = lines[i];
      callback(getColor(line.color), getWidth(line.color), line.x0, line.y0, line.x1, line.y1);
    }
    //callback2();
  }*/
  //callback(-1, 1, 1, 2, 3, 666);
}

function getWBImageData(second) {
  //console.log(second);
  //filehelper.unzipIndexFile(wbImageIndexFile, unzippedWbImageIndexFile);
  var buffer = filehelper.getIndexFile(unzippedWbImageIndexFile);
  //console.log(buffer.length);
  //console.log(buffer);
  var indexList = filehelper.getIndexArray(buffer);
  //console.log(indexList.length);
  var count =0;
  var hashmap = [];
  for (var i = 0; i < indexList.length; i++)
  {
    if(!hashmap[indexList[i].timestamp]) {
      count++;
      hashmap[indexList[i].timestamp] = i;
      ///console.log('hashmap.length=' + hashmap.length);
      //if (count < 10) {
      //  console.log(indexList[i].timestamp + '-' + i);
        //console.log(hashmap[indexList[i].timestamp]);
      //}
    }
  }


  var wbImageIndex = filehelper.getWBIndex(indexList);

  //console.log('wbImageIndex.length:' + wbImageIndex.length);

  var wbimagedata = filehelper.getWBImageData(wbImageDataFile, wbImageIndex, indexList, second);
  console.log('wbimagedata.length:' + wbimagedata.length);
  return wbimagedata;
}

function getWBSequenceData (second) {
  //filehelper.unzipIndexFile(wbSequenceIndexFile, unzippedWbSequenceIndexFile);
  var buffer = filehelper.getIndexFile(unzippedWbSequenceIndexFile);
  //console.log('buffer.length:'+buffer.length);
  //console.log(buffer.length);
  //console.log(buffer);
  var indexList = filehelper.getIndexArray(buffer);
  //console.log(indexList.length);
  /*var count =0;
  var hashmap = [];
  for (var i = 0; i < indexList.length; i++)
  {
    if(!hashmap[indexList[i].timestamp]) {
      count++;
      hashmap[indexList[i].timestamp] = i;
      ///console.log('hashmap.length=' + hashmap.length);
      //if (count < 10) {
      //  console.log(indexList[i].timestamp + '-' + i);
        //console.log(hashmap[indexList[i].timestamp]);
      //}
    }
  }*/


  var wbSequenceIndex = filehelper.getWBIndex(indexList);
  //console.log('indexList.length:'+indexList.length);

  //console.log('wbSequenceIndex.length:'+wbSequenceIndex.length);

   return filehelper.getWBSequenceData(wbSequenceDataFile, wbSequenceIndex, indexList, second);

}

function sortNumber(a,b) {
    return a - b;
}

function getColor(color) {
  switch (color) {
    case -1:
        return -1;
    case -2:
        return -2;
    case -3:
        return -3;
    case -8:
        return -8;
    case -9:
        return -9;
    case -10:
        return -10;
    default:
        return -10;
  }
}

function getWidth(color) {
  switch (color) {
    case -1:
        return 1;
    case -2:
        return 1;
    case -3:
        return 1;
    case -8:
        return 1;
    case -9:
        return 8 * 10 / 12;
    case -10:
        return 39 * 10 / 12;
    default:
        return 1;
  }
}

//module.exports = getImageData;

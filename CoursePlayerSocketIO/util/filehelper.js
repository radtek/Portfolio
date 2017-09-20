var fs = require('fs');
var zlib = require('zlib');
var binutils = require('binutils');
var index = require('../model/index');
var screenimage = require('../model/screenimage');
var wbline = require('../model/wbline');
var wbevent = require('../model/wbevent');
var constants = require('./constants');
var MAX_ROW_NO = 8;
var MAX_COL_NO = 8;

exports.unzipIndexFile = function(originalfile, unzippedfile){
  var readstream = fs.createReadStream(originalfile);
  readstream
      .pipe(zlib.createInflate())
      .pipe(fs.createWriteStream(unzippedfile));
};

exports.getIndexFile = function(unzippedfile) {
  var buffer = fs.readFileSync(unzippedfile);
  //console.log(buffer.length);
  return buffer;
};

exports.getImageIndex = function(ht, indexarr, second){
  var foundset = [];
  for(var i = 0; i < MAX_ROW_NO * MAX_COL_NO; i++) {
    foundset[i] = false;
  }
  var res = [];
  var index = 0;
  var firstItem = 0;
  var firstSecond = second;
  //console.log(firstSecond);
  //console.log('ht.length:' + ht.length);
  for (; firstSecond >= 0; firstSecond--) {
    if(ht[firstSecond]) {
        firstItem = ht[firstSecond];
        break;
    }
  }

  while (firstItem < indexarr.length && indexarr[firstItem].timestamp == firstSecond) {
      firstItem++;
  }

  //console.log('firstItem:' + firstItem);
  //firstItem = 1;
  if (firstItem > 0) {
    //console.log(indexarr.length);
      for (var i = firstItem - 1; i >= 0; i--) {

        //console.log(indexarr[i]);
        var row = indexarr[i].grid >> 4;
        var col = indexarr[i].grid & 0xf;
        //console.log(MAX_ROW_NO);
        var value = row * MAX_ROW_NO + col;
        //console.log('value' + value);

        if (!foundset[value]) {
            foundset[value] = true;
            res[index]=indexarr[i];
            index++;
        }
        if (res.length == MAX_ROW_NO * MAX_COL_NO) {
            break;
        }
      }
  }
  //console.log('image index length=:' + res.length);
  return res;
}
exports.getImageData = function(imagedatafile, imageindex, callback) {
  var res = [];
  var index = 0;
  var fd = fs.openSync(imagedatafile, 'r');
  var i = 0;
  for (i = 0; i < imageindex.length; i++) {
    var imageobj = imageindex[i];
    //console.log('grid' + imageobj.grid);
    var row = imageobj.grid >> 4;
    var col = imageobj.grid & 0xf;

    var offset = imageindex[i].offset;
    var length = imageindex[i].length;
    var buffer = new Buffer(length);
    fs.readSync(fd, buffer, 0, length, offset);
    var base64 = buffer.toString('base64');
    res[index]= new screenimage(row, col, base64);
    index++;
  }
  var json = JSON.stringify(res);
  callback(json);
  //use callback, don't write code here.
}

exports.getIndexArray = function(buffer){
  //var reader = new binutils.BinaryReader(buffer);
  //console.log(buffer.length);
  var arr = [];
  var ix = 0;
  var pos = 0;
  while (pos < buffer.length) {
    /*
    var buffer = new Buffer([3,0,51, 0,0,0,0,212,0,0,0])
    var pos = 0;
    console.log(buffer.readUInt16LE(0));
    pos = pos+2;
    console.log(buffer.readInt8(2));
    pos= pos+1;
    console.log(buffer.readInt32LE(3));
    pos = pos+4;
    console.log(buffer.readUInt32LE(7));*/
    var idxobj = new index(buffer.readUInt16LE(pos), buffer.readInt8(pos+2), buffer.readInt32LE(pos+3), buffer.readUInt32LE(pos+7));
    if (ix < 0) {
      console.log(idxobj.timestamp);
      console.log(idxobj.grid);
      console.log(idxobj.offset);
      console.log(idxobj.length);
    }

    arr[ix] = idxobj;
    ix++;
    pos = pos + 11;
  }

  for (var i = 0; i < arr.length; i++) {
    if (arr[i].offset == -1 && i > 0) {
      //console.log(i);
      arr[i].offset = arr[i - 1].offset;
      arr[i].length = arr[i - 1].length;
    }
  }
  arr.sort(compare);
  //console.log(arr[117].timestamp);
  //console.log(arr[117].grid);
  //console.log(arr[117].offset);
  //console.log(arr[117].length);
  //console.log(arr.length);
  return arr;
}

function compare(a,b) {
  var ret = a.timestamp - b.timestamp;
  if (ret == 0) {
      ret = a.grid - b.grid;
  }
  return ret;
}

exports.getWBIndex = function(indexarr){
  var res = [];
  //console.log(firstSecond);
  //console.log('ht.length:' + ht.length);
  for (var i = 0; i < indexarr.length; i++) {
    if(!res[indexarr[i].timestamp]) {
        res[indexarr[i].timestamp] = i;
    }
  }
  //console.log('image index length=:' + res.length);
  return res;
}
exports.getWBImageData = function(wbImageDataFile, wbImageIndex, indexList, second) {
  var res = [];
  var indeximage;
  var minutes = Math.floor(second / 60);
  if (wbImageIndex[minutes]) {
    indeximage = indexList[wbImageIndex[minutes]];
  }
  if (indeximage && indeximage.length>0) {
    var fd = fs.openSync(wbImageDataFile, 'r');
    var length = indeximage.length
    var buffer = new Buffer(length);
    fs.readSync(fd, buffer, 0, length, indeximage.offset);

    var ix = 0;
    var pos = 0;
    while (pos < buffer.length) {
      var wbl = new wbline(buffer.readUInt16LE(pos), buffer.readUInt16LE(pos+2), buffer.readUInt16LE(pos+4), buffer.readUInt16LE(pos+6),buffer.readInt16LE(pos+8), buffer.readUInt16LE(pos+10));
      if (ix < 0) {
        console.log(wbl.x0);
        console.log(wbl.y0);
        console.log(wbl.x1);
        console.log(wbl.y1);
        console.log(wbl.color);
        console.log(wbl.reserved);
      }

      res[ix] = wbl;
      ix++;
      pos = pos + 12;
    }
  }
  console.log('WBImageData.length:' + res.length)
  return res;
}

exports.getWBSequenceData = function(wbSequenceDataFile, wbSequenceIndex, indexList, second) {
  var res = [];
  var indexsequence;
  var minutes = Math.floor(second / 60);

  if (wbSequenceIndex[minutes]) {
    indexsequence = indexList[wbSequenceIndex[minutes]];
  }
  //console.log('indexsequence.length:'+ indexsequence.length);
  //console.log('indexsequence.offset:'+ indexsequence.offset);
  if (indexsequence && indexsequence.length>0) {
    var fd = fs.openSync(wbSequenceDataFile, 'r');
    var length = indexsequence.length
    var buffer = new Buffer(length);
    fs.readSync(fd, buffer, 0, length, indexsequence.offset);

    //console.log('buffer.length:'+ buffer.length);
    var ix = 0;
    var pos = 0;
    while (pos < buffer.length) {
      //console.log('pos:'+pos);
      //console.log('buffer.length:'+buffer.length);
      var wbe = new wbevent(buffer.readUInt16LE(pos), buffer.readUInt16LE(pos+2), buffer.readInt16LE(pos+4), buffer.readInt16LE(pos+6));
      if (ix < 0) {
        console.log(wbe.timestamp);
        console.log(wbe.reserved);
        console.log(wbe.x);
        console.log(wbe.y);
      }
      res[ix] = wbe;
      ix++;
      pos = pos + 8;
    }
  }
  console.log('WBSequenceData.length:' + res.length)
  return res;
}

/*var Index = new function() {
  this.timestamp = 0; //in minute for whiteboard, in second for screenshot
  this.grid = 0;
  this.offset = 0;
  this.length = 0;
  this.setvalue = function (timestamp, grid, offset, length) {
    this.timestamp = timestamp;
    this.grid = grid;
    this.offset = offset;
    this.length = length;
  };
  this.getTimestamp = function() {
    return this.timestamp;
  };
  this.getGrid = function() {
    return this.grid;
  };
  this.getOffset = function() {
    return this.offset;
  };
  this.getLength = function() {
    return this.length;
  };
}*/
function Index(timestamp, grid, offset, length) {
  this.timestamp = timestamp;
  this.grid = grid;
  this.offset = offset;
  this.length = length;
  this.row = function() {
    return this.grid >> 4;
  }
  this.col = function() {
    return this.grid & 0xf;
  }
}


module.exports = Index;

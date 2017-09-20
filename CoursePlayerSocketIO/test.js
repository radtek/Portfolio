var colds = require('./colds');
colds.getImageData(1080, showdata);
//console.log(showdata);
function showdata(json) {
  console.log('json.length:'+ json.length);
}

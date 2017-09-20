jQuery(function ($) {
    var canvas = $('#draw')[0].getContext('2d');
    canvas.strokeStyle = "#137";
    canvas.lineCap = 'round';
    canvas.lineWidth = 4;
    canvas.beginPath();
    $('#draw')
       .drag("start", function (ev, dd) {
           canvas.moveTo(
              ev.pageX - dd.originalX,
              ev.pageY - dd.originalY
           );
       })
       .drag(function (ev, dd) {
           canvas.lineTo(
              ev.pageX - dd.originalX,
              ev.pageY - dd.originalY
           );
           canvas.clearRect(0, 0, 680, 300);
           canvas.stroke();
       });

    var mywb = $('#mywb')[0].getContext('2d');
    mywb.fillStyle = "solid";
    mywb.strokeStyle = "#ECD018";
    mywb.lineCap = 'round';
    mywb.lineWidth = 5;
    $('#mywb')
       .drag("start", function (ev, dd) {
           mywb.beginPath();
           mywb.moveTo(
              ev.pageX - dd.originalX,
              ev.pageY - dd.originalY
           );
       })
       .drag(function (ev, dd) {
           mywb.lineTo(
              ev.pageX - dd.originalX,
              ev.pageY - dd.originalY
           );
           mywb.clearRect(0, 0, mywb.width, mywb.height);
           mywb.stroke();
       })
       .drag("end", function (ev, dd) {
           mywb.closePath();
       });
});
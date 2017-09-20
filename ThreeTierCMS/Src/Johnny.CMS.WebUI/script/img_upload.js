function BrowserServerImageFolder(imageid,textboxid)
{
	var folder = 'upload';
	var galleryscript = 'ftb.imagegallery.aspx?rif='+folder+'&cif='+folder;
	imgArr = showModalDialog(galleryscript,window,'dialogWidth:560px; dialogHeight:500px;help:0;status:0;resizeable:1;');
	if (imgArr != null) {
	    document.getElementById(imageid).src=imgArr['filename'];
	    document.getElementById(imageid).width=60;
	    document.getElementById(textboxid).value=imgArr['filename'];
//		eval("image" + index + ".src = imgArr['filename'];");
//	    eval("image" + index + ".width=60;");
//	    eval("document.Form1." + textbox + ".value = imgArr['filename'];");
	}
}
function OpenClientUploadFilePage(index,textbox)
{

 var str=window.showModalDialog('../Utility/CommonPage.aspx',window,'dialogWidth:600px; dialogHeight:200px;help:0;status:0;resizeable:0;');
 if(str!=null)
 {
     eval("image" + index + ".src = str;");
	 eval("image" + index + ".width=60;");	
     eval("document.Form1." + textbox + ".value = str;");
 }
}

function OpenUploadFilePage(textbox)
{

 var str=window.showModalDialog('Utility/CommonPage.aspx',window,'dialogWidth:600px; dialogHeight:200px;help:0;status:0;resizeable:0;');
 if(str!=null)
 {
     eval("document.Form1." + textbox + ".value = str;");
 }
}

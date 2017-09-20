//---------------------选择一个选项-----------------------------
function SelectOne(leftlistid,rightlistid,savedhdnid)
{
    var leftlist = document.getElementById(leftlistid);
    var rightlist = document.getElementById(rightlistid);
    var savedhdn = document.getElementById(savedhdnid);
    
    var s=0;
    //--------------------正则--------------------------------
    var re = /┝/g;
    var re1 = /┉/g;
    //--------------------判断是否选中选项--------------------
    for(var i=0;i<leftlist.length;i++)
    { 
        if (leftlist.options[i].selected){s+=1;} 
    }      
    if (s==0){
        return;
    }  
    var text = leftlist.options[leftlist.selectedIndex].text;
    text = text.replace(re,"");
    text = text.replace(re1,"");
    //--------------------判断右边列表框中否包含此项----------
    for (var i=0;i<rightlist.length;i++)
    {
        if(rightlist.options[i].text==text)
        {
            return;
        }
    }
    //-------------------添加到右边选项框----------------------
    savedhdn.value = savedhdn.value +  leftlist.options[leftlist.selectedIndex].value + ",";//给隐藏域赋值
    rightlist.options[rightlist.length] = new Option(text,leftlist.options[leftlist.selectedIndex].value);
    //-------------------判断是否到最后一项,如果不是则焦点移到下一项
    if(leftlist.selectedIndex<leftlist.length)
    {
        leftlist.selectedIndex = leftlist.selectedIndex + 1; 
    }
}
//---------------------选择一个选项结束-------------------------
//-----------------------选择所有--------------------------------
function SelectAll(leftlistid,rightlistid,savedhdnid)
{
    var leftlist = document.getElementById(leftlistid);
    var rightlist = document.getElementById(rightlistid);
    var savedhdn = document.getElementById(savedhdnid);
    
    clearall(rightlist);         //清空右边的列表框
    //---------------------正则-----------------------------
    var re = /┝/g;
    var re1 = /┉/g;
    //---------------------循环获取左边列表框值-------------
    savedhdn.value = "";
    for(var i=0;i<leftlist.length;i++)
    {
        var text = leftlist.options[i].text;
        //----------------正则替换-------------------------
        text = text.replace(re,"");
        text = text.replace(re1,"");
        savedhdn.value = savedhdn.value +  leftlist.options[i].value + ","; //给隐藏域赋值
        rightlist.options[i] = new Option(text,leftlist.options[i].value);//-添加选项到右边列表框
    }
    //---------------------循环获取左边列表框值结束---------
}
//---------------------选择所有结束-----------------------------
//---------------------取消一个---------------------------------
function UnSelectOne(rightlistid,savedhdnid)
{
    var rightlist = document.getElementById(rightlistid);
    var savedhdn = document.getElementById(savedhdnid);
    
    var s=0;
    //-----------------判断是否选中------------------------
    for(var i=0;i<rightlist.length;i++)
    {
        if (rightlist.options[i].selected){s+=1;}
    }
    if (s==0){
        return;
    }
    //-----------------移除选中的选项----------------------
    savedhdn.value = savedhdn.value.replace(rightlist.options[rightlist.selectedIndex].value + ",",""); //给隐藏域赋值
    rightlist.options[rightlist.selectedIndex]=null;
    //-----------------判断是否还有选项,如有则移到最后-----
    if(rightlist.length > 0)
    {
        rightlist.options[rightlist.length-1].selected=true;
    }
}
//---------------------取消一个结束-----------------------------
//---------------------取消选择所有-----------------------------
function UnSelectAll(rightlistid,savedhdnid)
{    
    var rightlist = document.getElementById(rightlistid);
    var savedhdn = document.getElementById(savedhdnid);
    clearall(rightlist);
    savedhdn.value = "";                   //给隐藏域赋值
}
//---------------------取消选择所有结束-------------------------
//---------------------移除右边列表框所有选项-------------------
function clearall(obj)
{
    var testnum=obj.length;
    for(var j=testnum-1;j>=0;j--)
    {
        obj.options[j]=null;
    }
}
//--------------------移除右边列表框所有选项结束----------------
//--------------------提交表单信息------------------------------
function Save(cmbrolesid,arrhdnid,savedhdnid)
{
    var savedhdn = document.getElementById(savedhdnid);
    var hdnClientID = arrhdnid.split(',');
    savedhdn.value='';
    for (var ix=0;ix<hdnClientID.length;ix++)
    {
        if (document.getElementById(hdnClientID[ix])!=null)
            savedhdn.value=savedhdn.value+document.getElementById(hdnClientID[ix]).value;
    }    
   
    if(document.getElementById(cmbrolesid).value=='')
    {
        return false;
    }
    return true;
}
//--------------------提交表单信息结束-------------------------
//--------------------重置右边列表框---------------------------
function listClear(arrrightid,arrhdnid,savedhdnid)
{
    var savedhdn = document.getElementById(savedhdnid);
    var rightid = arrrightid.split(',');
    var hdnid = arrhdnid.split(',');
    savedhdn.value='';
    for (var ix=0;ix<rightid.length;ix++)
    {
        if (document.getElementById(rightid[ix])!=null)
            UnSelectAll(rightid[ix],hdnid[ix]);
    }
}
//--------------------重置右边列表框结束-----------------------
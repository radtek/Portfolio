/*-------------- 函数检索 --------------
trim函数:                         trim() lTrim() rTrim()
校验字符串是否为空:                 checkIsNotEmpty(str)
校验字符串是否为整型:               checkIsInteger(str)
校验整型最小值:                    checkIntegerMinValue(str,val)
校验整型最大值:                    checkIntegerMaxValue(str,val) 
校验整型是否为非负数:               isNotNegativeInteger(str)
校验字符串是否为浮点型:             checkIsDouble(str) 
校验浮点型最小值:                  checkDoubleMinValue(str,val)
校验浮点型最大值:                  checkDoubleMaxValue(str,val)
校验浮点型是否为非负数:             isNotNegativeDouble(str)
校验字符串是否为日期型:             checkIsValidDate(str)
校验两个日期的先后:                checkDateEarlier(strStart,strEnd)
校验字符串是否为email型:           checkEmail(str)

校验字符串是否为中文:               checkIsChinese(str)
计算字符串的长度，一个汉字两个字符:   realLength()
校验字符串是否符合自定义正则表达式:   checkMask(str,pat)
得到文件的后缀名:                   getFilePostfix(oFile)  
按键屏蔽:                                   KeyDown()
-------------- 函数检索 --------------
*/

/**
* added by LxcJie 2004.6.25
* 去除多余空格函数
* trim:去除两边空格 lTrim:去除左空格 rTrim: 去除右空格
* 用法：
*     var str = "  hello ";
*     str = str.trim();
*/
String.prototype.trim = function()
{
    return this.replace(/(^[\\s]*)|([\\s]*$)/g, "");
}
String.prototype.lTrim = function()
{
    return this.replace(/(^[\\s]*)/g, "");
}
String.prototype.rTrim = function()
{
    return this.replace(/([\\s]*$)/g, "");
}
/********************************** Empty **************************************/
/**
*校验字符串是否为空
*返回值：
*如果不为空，定义校验通过，返回true
*如果为空，校验不通过，返回false               参考提示信息：输入域不能为空！
*/
function checkIsNotEmpty(str)
{
    if(str.trim() == "")
        return false;
    else
        return true;
}//~~~
/*--------------------------------- Empty --------------------------------------*/
/********************************** Integer *************************************/
/**
*校验字符串是否为整型
*返回值：
*如果为空，定义校验通过，      返回true
*如果字串全部为数字，校验通过，返回true
*如果校验不通过，              返回false     参考提示信息：输入域必须为数字！
*/
function checkIsInteger(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(/^(\\-?)(\\d+)$/.test(str))
        return true;
    else
        return false;
}//~~~
/**
*校验整型最小值
*str：要校验的串。  val：比较的值
*
*返回值：
*如果为空，定义校验通过，                返回true
*如果满足条件，大于等于给定值，校验通过，返回true
*如果小于给定值，                        返回false              参考提示信息：输入域不能小于给定值！
*/
function checkIntegerMinValue(str,val)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(typeof(val) != "string")
        val = val + "";
    if(checkIsInteger(str) == true)
    {
        if(parseInt(str,10)>=parseInt(val,10))
            return true;
        else
            return false;
    }
    else
        return false;
}//~~~
/**
*校验整型最大值
*str：要校验的串。  val：比较的值
*
*返回值：
*如果为空，定义校验通过，                返回true
*如果满足条件，小于等于给定值，校验通过，返回true
*如果大于给定值，                        返回false       参考提示信息：输入值不能大于给定值！
*/
function checkIntegerMaxValue(str,val)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(typeof(val) != "string")
        val = val + "";
    if(checkIsInteger(str) == true)
    {
        if(parseInt(str,10)<=parseInt(val,10))
            return true;
        else
            return false;
    }
    else
        return false;
}//~~~
/**
*校验整型是否为非负数
*str：要校验的串。
*
*返回值：
*如果为空，定义校验通过，返回true
*如果非负数，            返回true
*如果是负数，            返回false               参考提示信息：输入值不能是负数！
*/
function isNotNegativeInteger(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(checkIsInteger(str) == true)
    {
        if(parseInt(str,10) < 0)
            return false;
        else
            return true;
    }
    else
        return false;
}//~~~
/*--------------------------------- Integer --------------------------------------*/
/********************************** Double ****************************************/
/**
*校验字符串是否为浮点型
*返回值：
*如果为空，定义校验通过，      返回true
*如果字串为浮点型，校验通过，  返回true
*如果校验不通过，              返回false     参考提示信息：输入域不是合法的浮点数！
*/
function checkIsDouble(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    //如果是整数，则校验整数的有效性
    if(str.indexOf(".") == -1)
    {
        if(checkIsInteger(str) == true)
            return true;
        else
            return false;
    }
    else
    {
        if(/^(\\-?)(\\d+)(.{1})(\\d+)$/g.test(str))
            return true;
        else
            return false;
    }
}//~~~
/**
*校验浮点型最小值
*str：要校验的串。  val：比较的值
*
*返回值：
*如果为空，定义校验通过，                返回true
*如果满足条件，大于等于给定值，校验通过，返回true
*如果小于给定值，                        返回false              参考提示信息：输入域不能小于给定值！
*/
function checkDoubleMinValue(str,val)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(typeof(val) != "string")
        val = val + "";
    if(checkIsDouble(str) == true)
    {
        if(parseFloat(str)>=parseFloat(val))
            return true;
        else
            return false;
    }
    else
        return false;
}//~~~
/**
*校验浮点型最大值
*str：要校验的串。  val：比较的值
*
*返回值：
*如果为空，定义校验通过，                返回true
*如果满足条件，小于等于给定值，校验通过，返回true
*如果大于给定值，                        返回false       参考提示信息：输入值不能大于给定值！
*/
function checkDoubleMaxValue(str,val)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(typeof(val) != "string")
        val = val + "";
    if(checkIsDouble(str) == true)
    {
        if(parseFloat(str)<=parseFloat(val))
            return true;
        else
            return false;
    }
    else
        return false;
}//~~~
/**
*校验浮点型是否为非负数
*str：要校验的串。
*
*返回值：
*如果为空，定义校验通过，返回true
*如果非负数，            返回true
*如果是负数，            返回false               参考提示信息：输入值不能是负数！
*/
function isNotNegativeDouble(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if(checkIsDouble(str) == true)
    {
        if(parseFloat(str) < 0)
            return false;
        else
            return true;
    }
    else
        return false;
}//~~~
/*--------------------------------- Double ---------------------------------------*/
/********************************** date ******************************************/
/**
*校验字符串是否为日期型
*返回值：
*如果为空，定义校验通过，           返回true
*如果字串为日期型，校验通过，       返回true
*如果日期不合法，                   返回false    参考提示信息：输入域的时间不合法！（yyyy-MM-dd）
*/
function checkIsValidDate(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    var pattern = /^((\\d{4})|(\\d{2}))-(\\d{1,2})-(\\d{1,2})$/g;
    if(!pattern.test(str))
        return false;
    var arrDate = str.split("-");
    if(parseInt(arrDate[0],10) < 100)
        arrDate[0] = 2000 + parseInt(arrDate[0],10) + "";
    var date =  new Date(arrDate[0],(parseInt(arrDate[1],10) -1)+"",arrDate[2]);
    if(date.getYear() == arrDate[0]
       && date.getMonth() == (parseInt(arrDate[1],10) -1)+""
       && date.getDate() == arrDate[2])
        return true;
    else
        return false;
}//~~~
/**
*校验两个日期的先后
*返回值：
*如果其中有一个日期为空，校验通过,          返回true
*如果起始日期早于等于终止日期，校验通过，   返回true
*如果起始日期晚于终止日期，                 返回false    参考提示信息： 起始日期不能晚于结束日期。
*/
function checkDateEarlier(strStart,strEnd)
{
    if(checkIsValidDate(strStart) == false || checkIsValidDate(strEnd) == false)
        return false;
    //如果有一个输入为空，则通过检验
    if (( strStart == "" ) || ( strEnd == "" ))
        return true;
    var arr1 = strStart.split("-");
    var arr2 = strEnd.split("-");
    var date1 = new Date(arr1[0],parseInt(arr1[1].replace(/^0/,""),10) - 1,arr1[2]);
    var date2 = new Date(arr2[0],parseInt(arr2[1].replace(/^0/,""),10) - 1,arr2[2]);
    if(arr1[1].length == 1)
        arr1[1] = "0" + arr1[1];
    if(arr1[2].length == 1)
        arr1[2] = "0" + arr1[2];
    if(arr2[1].length == 1)
        arr2[1] = "0" + arr2[1];
    if(arr2[2].length == 1)
        arr2[2]="0" + arr2[2];
    var d1 = arr1[0] + arr1[1] + arr1[2];
    var d2 = arr2[0] + arr2[1] + arr2[2];
    if(parseInt(d1,10) > parseInt(d2,10))
       return false;
    else
       return true;
}//~~~
/*--------------------------------- date -----------------------------------------*/
/********************************** email *****************************************/
/**
*校验字符串是否为email型
*返回值：
*如果为空，定义校验通过，           返回true
*如果字串为email型，校验通过，      返回true
*如果email不合法，                  返回false    参考提示信息：Email的格式不正確！
*/
function checkEmail(str)
{
    //如果为空，则通过校验
    if(str == "")
        return true;
    if (str.charAt(0) == "." || str.charAt(0) == "@" || str.indexOf(\'@\', 0) == -1
        || str.indexOf(\'.\', 0) == -1 || str.lastIndexOf("@") == str.length-1 || str.lastIndexOf(".") == str.length-1)
        return false;
    else
        return true;
}//~~~
/*--------------------------------- email ----------------------------------------*/
/********************************** chinese ***************************************/
/**
*校验字符串是否为中文
*返回值：
*如果为空，定义校验通过，           返回true
*如果字串为中文，校验通过，         返回true
*如果字串为非中文，             返回false    参考提示信息：必须为中文！
*/
function checkIsChinese(str)
{
    //如果值为空，通过校验
    if (str == "")
        return true;
    var pattern = /^([\\u4E00-\\u9FA5]|[\\uFE30-\\uFFA0])*$/gi;
    if (pattern.test(str))
        return true;
    else
        return false;
}//~~~
/**
* 计算字符串的长度，一个汉字两个字符
*/
String.prototype.realLength = function()
{
  return this.replace(/[^\\x00-\\xff]/g,"**").length;
}
/*--------------------------------- chinese --------------------------------------*/
/********************************** mask ***************************************/
/**
*校验字符串是否符合自定义正则表达式
*str 要校验的字串  pat 自定义的正则表达式
*返回值：
*如果为空，定义校验通过，           返回true
*如果字串符合，校验通过，           返回true
*如果字串不符合，                   返回false    参考提示信息：必须满足***模式
*/
function checkMask(str,pat)
{
    //如果值为空，通过校验
    if (str == "")
        return true;
    var pattern = new RegExp(pat,"gi")
    if (pattern.test(str))
        return true;
    else
        return false;
}//~~~
/*--------------------------------- mask --------------------------------------*/
/********************************** file ***************************************/
/**
* added by LxcJie 2004.6.25
* 得到文件的后缀名
* oFile为file控件对象
*/
function getFilePostfix(oFile)
{
    if(oFile == null)
        return null;
    var pattern = /(.*)\\.(.*)$/gi;
    if(typeof(oFile) == "object")
    {
        if(oFile.value == null || oFile.value == "")
            return null;
        var arr = pattern.exec(oFile.value);
        return RegExp.$2;
    }
    else if(typeof(oFile) == "string")
    {
        var arr = pattern.exec(oFile);
        return RegExp.$2;
    }
    else
        return null;
}//~~~
/*--------------------------------- file --------------------------------------*/

*---------------------------------按键屏蔽-----------------------------------*/
function KeyDown(){ 
//屏蔽鼠标右键、Ctrl+n、shift+F10、F5刷新、退格键 
//alert("ASCII代码是："+event.keyCode); 
if ((window.event.altKey)&& 
((window.event.keyCode==37)|| //屏蔽 Alt+ 方向键 ← 
(window.event.keyCode==39))){ //屏蔽 Alt+ 方向键 → 
alert("不准你使用ALT+方向键前进或后退网页！"); 
event.returnValue=false; 
} 
if ((event.keyCode==8) || //屏蔽退格删除键 
(event.keyCode==116)|| //屏蔽 F5 刷新键 
(event.keyCode==112)|| //屏蔽 F1 刷新键 
(event.ctrlKey && event.keyCode==82)){ //Ctrl + R 
event.keyCode=0; 
event.returnValue=false; 
} 
if ((event.ctrlKey)&&(event.keyCode==78)) //屏蔽 Ctrl+n 
event.returnValue=false; 
if ((event.shiftKey)&&(event.keyCode==121)) //屏蔽 shift+F10 
event.returnValue=false; 
if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
window.event.returnValue = false; //屏蔽 shift 加鼠标左键新开一网页 
if ((window.event.altKey)&&(window.event.keyCode==115)){ //屏蔽Alt+F4 
window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px"); 
return false;} 
} 




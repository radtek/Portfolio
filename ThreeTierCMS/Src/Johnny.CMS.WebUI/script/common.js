/*-------------- �������� --------------
trim����:                         trim() lTrim() rTrim()
У���ַ����Ƿ�Ϊ��:                 checkIsNotEmpty(str)
У���ַ����Ƿ�Ϊ����:               checkIsInteger(str)
У��������Сֵ:                    checkIntegerMinValue(str,val)
У���������ֵ:                    checkIntegerMaxValue(str,val) 
У�������Ƿ�Ϊ�Ǹ���:               isNotNegativeInteger(str)
У���ַ����Ƿ�Ϊ������:             checkIsDouble(str) 
У�鸡������Сֵ:                  checkDoubleMinValue(str,val)
У�鸡�������ֵ:                  checkDoubleMaxValue(str,val)
У�鸡�����Ƿ�Ϊ�Ǹ���:             isNotNegativeDouble(str)
У���ַ����Ƿ�Ϊ������:             checkIsValidDate(str)
У���������ڵ��Ⱥ�:                checkDateEarlier(strStart,strEnd)
У���ַ����Ƿ�Ϊemail��:           checkEmail(str)

У���ַ����Ƿ�Ϊ����:               checkIsChinese(str)
�����ַ����ĳ��ȣ�һ�����������ַ�:   realLength()
У���ַ����Ƿ�����Զ���������ʽ:   checkMask(str,pat)
�õ��ļ��ĺ�׺��:                   getFilePostfix(oFile)  
��������:                                   KeyDown()
-------------- �������� --------------
*/

/**
* added by LxcJie 2004.6.25
* ȥ������ո���
* trim:ȥ�����߿ո� lTrim:ȥ����ո� rTrim: ȥ���ҿո�
* �÷���
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
*У���ַ����Ƿ�Ϊ��
*����ֵ��
*�����Ϊ�գ�����У��ͨ��������true
*���Ϊ�գ�У�鲻ͨ��������false               �ο���ʾ��Ϣ����������Ϊ�գ�
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
*У���ַ����Ƿ�Ϊ����
*����ֵ��
*���Ϊ�գ�����У��ͨ����      ����true
*����ִ�ȫ��Ϊ���֣�У��ͨ��������true
*���У�鲻ͨ����              ����false     �ο���ʾ��Ϣ�����������Ϊ���֣�
*/
function checkIsInteger(str)
{
    //���Ϊ�գ���ͨ��У��
    if(str == "")
        return true;
    if(/^(\\-?)(\\d+)$/.test(str))
        return true;
    else
        return false;
}//~~~
/**
*У��������Сֵ
*str��ҪУ��Ĵ���  val���Ƚϵ�ֵ
*
*����ֵ��
*���Ϊ�գ�����У��ͨ����                ����true
*����������������ڵ��ڸ���ֵ��У��ͨ��������true
*���С�ڸ���ֵ��                        ����false              �ο���ʾ��Ϣ����������С�ڸ���ֵ��
*/
function checkIntegerMinValue(str,val)
{
    //���Ϊ�գ���ͨ��У��
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
*У���������ֵ
*str��ҪУ��Ĵ���  val���Ƚϵ�ֵ
*
*����ֵ��
*���Ϊ�գ�����У��ͨ����                ����true
*�������������С�ڵ��ڸ���ֵ��У��ͨ��������true
*������ڸ���ֵ��                        ����false       �ο���ʾ��Ϣ������ֵ���ܴ��ڸ���ֵ��
*/
function checkIntegerMaxValue(str,val)
{
    //���Ϊ�գ���ͨ��У��
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
*У�������Ƿ�Ϊ�Ǹ���
*str��ҪУ��Ĵ���
*
*����ֵ��
*���Ϊ�գ�����У��ͨ��������true
*����Ǹ�����            ����true
*����Ǹ�����            ����false               �ο���ʾ��Ϣ������ֵ�����Ǹ�����
*/
function isNotNegativeInteger(str)
{
    //���Ϊ�գ���ͨ��У��
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
*У���ַ����Ƿ�Ϊ������
*����ֵ��
*���Ϊ�գ�����У��ͨ����      ����true
*����ִ�Ϊ�����ͣ�У��ͨ����  ����true
*���У�鲻ͨ����              ����false     �ο���ʾ��Ϣ���������ǺϷ��ĸ�������
*/
function checkIsDouble(str)
{
    //���Ϊ�գ���ͨ��У��
    if(str == "")
        return true;
    //�������������У����������Ч��
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
*У�鸡������Сֵ
*str��ҪУ��Ĵ���  val���Ƚϵ�ֵ
*
*����ֵ��
*���Ϊ�գ�����У��ͨ����                ����true
*����������������ڵ��ڸ���ֵ��У��ͨ��������true
*���С�ڸ���ֵ��                        ����false              �ο���ʾ��Ϣ����������С�ڸ���ֵ��
*/
function checkDoubleMinValue(str,val)
{
    //���Ϊ�գ���ͨ��У��
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
*У�鸡�������ֵ
*str��ҪУ��Ĵ���  val���Ƚϵ�ֵ
*
*����ֵ��
*���Ϊ�գ�����У��ͨ����                ����true
*�������������С�ڵ��ڸ���ֵ��У��ͨ��������true
*������ڸ���ֵ��                        ����false       �ο���ʾ��Ϣ������ֵ���ܴ��ڸ���ֵ��
*/
function checkDoubleMaxValue(str,val)
{
    //���Ϊ�գ���ͨ��У��
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
*У�鸡�����Ƿ�Ϊ�Ǹ���
*str��ҪУ��Ĵ���
*
*����ֵ��
*���Ϊ�գ�����У��ͨ��������true
*����Ǹ�����            ����true
*����Ǹ�����            ����false               �ο���ʾ��Ϣ������ֵ�����Ǹ�����
*/
function isNotNegativeDouble(str)
{
    //���Ϊ�գ���ͨ��У��
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
*У���ַ����Ƿ�Ϊ������
*����ֵ��
*���Ϊ�գ�����У��ͨ����           ����true
*����ִ�Ϊ�����ͣ�У��ͨ����       ����true
*������ڲ��Ϸ���                   ����false    �ο���ʾ��Ϣ���������ʱ�䲻�Ϸ�����yyyy-MM-dd��
*/
function checkIsValidDate(str)
{
    //���Ϊ�գ���ͨ��У��
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
*У���������ڵ��Ⱥ�
*����ֵ��
*���������һ������Ϊ�գ�У��ͨ��,          ����true
*�����ʼ�������ڵ�����ֹ���ڣ�У��ͨ����   ����true
*�����ʼ����������ֹ���ڣ�                 ����false    �ο���ʾ��Ϣ�� ��ʼ���ڲ������ڽ������ڡ�
*/
function checkDateEarlier(strStart,strEnd)
{
    if(checkIsValidDate(strStart) == false || checkIsValidDate(strEnd) == false)
        return false;
    //�����һ������Ϊ�գ���ͨ������
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
*У���ַ����Ƿ�Ϊemail��
*����ֵ��
*���Ϊ�գ�����У��ͨ����           ����true
*����ִ�Ϊemail�ͣ�У��ͨ����      ����true
*���email���Ϸ���                  ����false    �ο���ʾ��Ϣ��Email�ĸ�ʽ�����_��
*/
function checkEmail(str)
{
    //���Ϊ�գ���ͨ��У��
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
*У���ַ����Ƿ�Ϊ����
*����ֵ��
*���Ϊ�գ�����У��ͨ����           ����true
*����ִ�Ϊ���ģ�У��ͨ����         ����true
*����ִ�Ϊ�����ģ�             ����false    �ο���ʾ��Ϣ������Ϊ���ģ�
*/
function checkIsChinese(str)
{
    //���ֵΪ�գ�ͨ��У��
    if (str == "")
        return true;
    var pattern = /^([\\u4E00-\\u9FA5]|[\\uFE30-\\uFFA0])*$/gi;
    if (pattern.test(str))
        return true;
    else
        return false;
}//~~~
/**
* �����ַ����ĳ��ȣ�һ�����������ַ�
*/
String.prototype.realLength = function()
{
  return this.replace(/[^\\x00-\\xff]/g,"**").length;
}
/*--------------------------------- chinese --------------------------------------*/
/********************************** mask ***************************************/
/**
*У���ַ����Ƿ�����Զ���������ʽ
*str ҪУ����ִ�  pat �Զ����������ʽ
*����ֵ��
*���Ϊ�գ�����У��ͨ����           ����true
*����ִ����ϣ�У��ͨ����           ����true
*����ִ������ϣ�                   ����false    �ο���ʾ��Ϣ����������***ģʽ
*/
function checkMask(str,pat)
{
    //���ֵΪ�գ�ͨ��У��
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
* �õ��ļ��ĺ�׺��
* oFileΪfile�ؼ�����
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

*---------------------------------��������-----------------------------------*/
function KeyDown(){ 
//��������Ҽ���Ctrl+n��shift+F10��F5ˢ�¡��˸�� 
//alert("ASCII�����ǣ�"+event.keyCode); 
if ((window.event.altKey)&& 
((window.event.keyCode==37)|| //���� Alt+ ����� �� 
(window.event.keyCode==39))){ //���� Alt+ ����� �� 
alert("��׼��ʹ��ALT+�����ǰ���������ҳ��"); 
event.returnValue=false; 
} 
if ((event.keyCode==8) || //�����˸�ɾ���� 
(event.keyCode==116)|| //���� F5 ˢ�¼� 
(event.keyCode==112)|| //���� F1 ˢ�¼� 
(event.ctrlKey && event.keyCode==82)){ //Ctrl + R 
event.keyCode=0; 
event.returnValue=false; 
} 
if ((event.ctrlKey)&&(event.keyCode==78)) //���� Ctrl+n 
event.returnValue=false; 
if ((event.shiftKey)&&(event.keyCode==121)) //���� shift+F10 
event.returnValue=false; 
if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
window.event.returnValue = false; //���� shift ���������¿�һ��ҳ 
if ((window.event.altKey)&&(window.event.keyCode==115)){ //����Alt+F4 
window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px"); 
return false;} 
} 




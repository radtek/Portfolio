function zrWebCheckBox_Check(item, group, isparent)
 {
     var itemIsParent = (isparent.toLowerCase() == "true") ? true : false;
     var itemChecked = item.checked;
     var objArray;
     try
     {
         objArray = eval(group);
     }
     catch (e)
     {
         return;
     }
     if (objArray == null || objArray.length == 0)
     {
         return;
     }
     if (itemIsParent)
     {
         zrWebCheckBox_CheckAll(objArray, itemChecked);
     }
     else
     {
         zrWebCheckBox_CheckIt(objArray, itemChecked);
     }
 }
 
 function zrWebCheckBox_CheckIt(newArray, itemChecked)
 {
     if (!itemChecked)
     {
         for (var i = 0; i < newArray.length; i++)
         {
             var e = newArray[i];
             var parentid = document.getElementById("zrWebCheckBoxParentId").value;
             var isParent = (parentid==e.id) ? true : false;
             if (isParent)
             {
                 e.checked = false;
             }
         }
     }
     else
     {
         var objAll;
         var allChecked = 0;
         for (var i = 0; i < newArray.length; i++)
         {
             var e = newArray[i];
             var parentid = document.getElementById("zrWebCheckBoxParentId").value;
             var isParent = (parentid==e.id) ? true : false;
             if (e.checked && !isParent)
             {
                 allChecked++;
             }
             else if (isParent)
             {
                objAll = e;
            }
         }
         if (allChecked == newArray.length - 1)
         {
             objAll.checked = true;
         }
     }
 }
 
 function zrWebCheckBox_CheckAll(newArray, itemChecked)
 {
     for (var i = 0; i < newArray.length; i++)
     {
         var e = newArray[i];
         e.checked = itemChecked;
     }
 }

function zrWebCheckBox_CheckHasData(group)
{
    var objArray;
    var hasData = false;
    try
    {
        objArray = eval(group);
    }
    catch (e)
    {
        return hasData;
    }
    if (objArray == null || objArray.length == 0)
    {
        return hasData;
    }
    for (var i = 0; i < objArray.length; i++)
    {
        var e = objArray[i];
        if (e.checked)
        {
            hasData = true;
            break;
        }
    }
    return hasData;
}


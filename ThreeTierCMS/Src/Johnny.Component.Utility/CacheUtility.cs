using System;
using System.Web;
using System.Web.Caching;

namespace Johnny.Component.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CacheUtility
    {
        private const int TIME_LIMITED = 300;
        public CacheUtility()
        {
        }

        public static void InsertCache(string key, object value)
        {
            if (key != null && key.Length != 0 && value != null)
            {
                //建立回调委托的一个实例
                CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

                //以Identify为标志，将userInfo存入Cache
                HttpContext.Current.Cache.Insert(key, value, null,
                     System.DateTime.Now.AddSeconds(TIME_LIMITED),
                     System.Web.Caching.Cache.NoSlidingExpiration,
                     System.Web.Caching.CacheItemPriority.Default,
                     callBack);
            }
        }

        //判断存储的"一键一值"值是否还存在（有没有过期失效或从来都未存储过）
        public static bool ExistCache(string key)
        {
            if (HttpContext.Current.Cache[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //插入"一键多值"方法
        //***其中 StorageInfType是一个Enum,里面存有三种类型: UserInf SysInf PageInf 
        //这个枚举如下:

        public enum StorageInfType
        {
            /// <summary>用户信息</summary>
            UserInf = 0,

            /// <summary>页面信息</summary>
            PageInf = 1,

            /// <summary>系统信息</summary>
            SysInf = 2
        }
        //此枚举是自己定义的.可根据需要定义不同的枚举  
        //加个枚举目的是实现“一键多值”存储方法，事实上Cache中是存放了多个变量的，只不过被这个类封装了，
        //程序员感到就好像是“一键一值”.   这样做目的是可以简化开发操作,否则程序员要存储几个变量就得定义几个Identify.
        public static bool InsertCommonInf(string strIdentify, StorageInfType enumInfType, object objValue)
        {
            if (strIdentify != null && strIdentify != "" && strIdentify.Length != 0 && objValue != null)
            {
                //RemoveCommonInf(strIdentify,enumInfType); 

                //建立回调委托的一个实例
                CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

                if (enumInfType == StorageInfType.UserInf)
                {
                    //以用户UserID+信息标志(StorageInfType枚举)，将userInfo存入Cache
                    HttpContext.Current.Cache.Insert(strIdentify + StorageInfType.UserInf.ToString(), objValue, null,
                        System.DateTime.Now.AddSeconds(18000),       //单位秒
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Default,
                        callBack);
                }
                if (enumInfType == StorageInfType.PageInf)
                {
                    //以用户UserID+信息标志(StorageInfType枚举)，将PageInfo存入Cache
                    HttpContext.Current.Cache.Insert(strIdentify + StorageInfType.PageInf.ToString(), objValue, null,
                         System.DateTime.Now.AddSeconds(18000),
                         System.Web.Caching.Cache.NoSlidingExpiration,
                         System.Web.Caching.CacheItemPriority.Default,
                         callBack);
                }
                if (enumInfType == StorageInfType.SysInf)
                {
                    //以用户UserID+信息标志(StorageInfType枚举)，将SysInfo存入Cache
                    HttpContext.Current.Cache.Insert(strIdentify + StorageInfType.SysInf.ToString(), objValue, null,
                         System.DateTime.Now.AddSeconds(18000),
                            System.Web.Caching.Cache.NoSlidingExpiration,
                         System.Web.Caching.CacheItemPriority.Default,
                         callBack);
                }
                return true;
            }
            return false;
        }
        //读取“一键多值”Identify的值
        public static object GetCache(string key)
        {
            //取出值
            if (HttpContext.Current.Cache[key] != null)
            {
                return HttpContext.Current.Cache[key];
            }
            else
            {
                return null;
            }
        }

        //手动移除“一键一值”对应的值
        public static bool RemoveIdentify(string strIdentify)
        {
            //取出值
            if (HttpContext.Current.Cache[strIdentify] != null)
            {
                HttpContext.Current.Cache.Remove(strIdentify);
            }
            return true;
        }

        //此方法在值失效之前调用，可以用于在失效之前更新数据库，或从数据库重新获取数据
        private static void onRemove(string strIdentify, object userInfo, CacheItemRemovedReason reason)
        {

        }
    }
}

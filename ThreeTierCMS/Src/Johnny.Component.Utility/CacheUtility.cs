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
                //�����ص�ί�е�һ��ʵ��
                CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

                //��IdentifyΪ��־����userInfo����Cache
                HttpContext.Current.Cache.Insert(key, value, null,
                     System.DateTime.Now.AddSeconds(TIME_LIMITED),
                     System.Web.Caching.Cache.NoSlidingExpiration,
                     System.Web.Caching.CacheItemPriority.Default,
                     callBack);
            }
        }

        //�жϴ洢��"һ��һֵ"ֵ�Ƿ񻹴��ڣ���û�й���ʧЧ�������δ�洢����
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

        //����"һ����ֵ"����
        //***���� StorageInfType��һ��Enum,���������������: UserInf SysInf PageInf 
        //���ö������:

        public enum StorageInfType
        {
            /// <summary>�û���Ϣ</summary>
            UserInf = 0,

            /// <summary>ҳ����Ϣ</summary>
            PageInf = 1,

            /// <summary>ϵͳ��Ϣ</summary>
            SysInf = 2
        }
        //��ö�����Լ������.�ɸ�����Ҫ���岻ͬ��ö��  
        //�Ӹ�ö��Ŀ����ʵ�֡�һ����ֵ���洢��������ʵ��Cache���Ǵ���˶�������ģ�ֻ������������װ�ˣ�
        //����Ա�е��ͺ����ǡ�һ��һֵ��.   ������Ŀ���ǿ��Լ򻯿�������,�������ԱҪ�洢���������͵ö��弸��Identify.
        public static bool InsertCommonInf(string strIdentify, StorageInfType enumInfType, object objValue)
        {
            if (strIdentify != null && strIdentify != "" && strIdentify.Length != 0 && objValue != null)
            {
                //RemoveCommonInf(strIdentify,enumInfType); 

                //�����ص�ί�е�һ��ʵ��
                CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

                if (enumInfType == StorageInfType.UserInf)
                {
                    //���û�UserID+��Ϣ��־(StorageInfTypeö��)����userInfo����Cache
                    HttpContext.Current.Cache.Insert(strIdentify + StorageInfType.UserInf.ToString(), objValue, null,
                        System.DateTime.Now.AddSeconds(18000),       //��λ��
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Default,
                        callBack);
                }
                if (enumInfType == StorageInfType.PageInf)
                {
                    //���û�UserID+��Ϣ��־(StorageInfTypeö��)����PageInfo����Cache
                    HttpContext.Current.Cache.Insert(strIdentify + StorageInfType.PageInf.ToString(), objValue, null,
                         System.DateTime.Now.AddSeconds(18000),
                         System.Web.Caching.Cache.NoSlidingExpiration,
                         System.Web.Caching.CacheItemPriority.Default,
                         callBack);
                }
                if (enumInfType == StorageInfType.SysInf)
                {
                    //���û�UserID+��Ϣ��־(StorageInfTypeö��)����SysInfo����Cache
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
        //��ȡ��һ����ֵ��Identify��ֵ
        public static object GetCache(string key)
        {
            //ȡ��ֵ
            if (HttpContext.Current.Cache[key] != null)
            {
                return HttpContext.Current.Cache[key];
            }
            else
            {
                return null;
            }
        }

        //�ֶ��Ƴ���һ��һֵ����Ӧ��ֵ
        public static bool RemoveIdentify(string strIdentify)
        {
            //ȡ��ֵ
            if (HttpContext.Current.Cache[strIdentify] != null)
            {
                HttpContext.Current.Cache.Remove(strIdentify);
            }
            return true;
        }

        //�˷�����ֵʧЧ֮ǰ���ã�����������ʧЧ֮ǰ�������ݿ⣬������ݿ����»�ȡ����
        private static void onRemove(string strIdentify, object userInfo, CacheItemRemovedReason reason)
        {

        }
    }
}

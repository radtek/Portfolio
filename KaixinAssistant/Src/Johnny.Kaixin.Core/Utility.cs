using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Johnny.Kaixin.Core
{
    public sealed class Utility
    {
        private Utility() { }

        public static string GetAssistantConfig()
        {
            return GetResource("Johnny.Kaixin.Core.Resources.AssistantConfig.xml");
        }

        public static string GetGroupConfig()
        {
            return GetResource("Johnny.Kaixin.Core.Resources.GroupConfig.xml");
        }

        public static string GetAccountConfig(string email, string password)
        {
            return GetResource("Johnny.Kaixin.Core.Resources.OperationConfig.xml", "#Email#", email, "#Password#", password);
        }

        public static string GetTaskConfig(string taskid, string taskname)
        {
            return GetResource("Johnny.Kaixin.Core.Resources.TaskConfig.xml", "#TaskId#", taskid, "#TaskName#", taskname);
        }

        //public static string GetCarsInMarketMasterData()
        //{
        //    return GetResource("Johnny.Kaixin.Core.Resources.CarsInMarketMasterData.xml");
        //}

        //public static string GetSeedsMasterData()
        //{
        //    return GetResource("Johnny.Kaixin.Core.Resources.SeedsInShopMasterData.xml");
        //}

        //public static string GetCalfsMasterData()
        //{
        //    return GetResource("Johnny.Kaixin.Core.Resources.CalfsInShopMasterData.xml");
        //}

        //public static string GetRankSeedsMasterData()
        //{
        //    return GetResource("Johnny.Kaixin.Core.Resources.RankSeedsMasterData.xml");
        //}

        public static string GetMasterDataFile(string file)
        {
            return GetResource("Johnny.Kaixin.Core.Resources." + file);
        }

        internal static string GetResource(string name, string oldValue, string newValue, string oldValue2, string newValue2)
        {
            string returnValue = GetResource(name);
            returnValue = returnValue.Replace(oldValue, newValue);
            return returnValue.Replace(oldValue2, newValue2);
        }

        internal static string GetResource(string name, string oldValue, string newValue)
        {
            string returnValue = GetResource(name);
            return returnValue.Replace(oldValue, newValue);
        }

        internal static string GetResource(string name)
        {
            using (StreamReader streamReader = new StreamReader(GetResourceAsStream(name)))
            {
                return streamReader.ReadToEnd();
            }
        }

        internal static Stream GetResourceAsStream(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }
    }
}

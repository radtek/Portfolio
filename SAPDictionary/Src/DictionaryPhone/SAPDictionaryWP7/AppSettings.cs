using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;

namespace Johnny.WP7.Dictionary
{
    public class AppSettings
    {
        // Our isolated storage settings
        IsolatedStorageSettings isolatedStore;

        // The isolated storage key names of our settings
        const string EnableRemoteKeyName = "EnableRemoteKeyName";
        const string LangKeyName = "LangKeyName";
        const string StartViewKeyName = "StartViewKeyName";
                
        // The default value of our settings
        const bool EnableRemoteDefault = false;
        const string LangDefault = "en-US";
        const string StartViewDefault = "Search";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            try
            {
                // Get the settings for this application.
                isolatedStore = IsolatedStorageSettings.ApplicationSettings;

                // Current culture
                System.Globalization.CultureInfo lCulture = null;
                if (isolatedStore.Contains(LangKeyName))
                {
                    lCulture = new System.Globalization.CultureInfo(isolatedStore[LangKeyName].ToString());
                }
                else
                {
                    lCulture = System.Globalization.CultureInfo.CurrentUICulture;
                }

                switch (lCulture.Name)
                {
                    case "en-US": 
                    case "de-DE":
                        Lang = lCulture.Name;
                        break;
                    default:
                        Lang = LangDefault;
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e.ToString());
            }
        }

        #region internal
        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (isolatedStore.Contains(Key))
            {
                // If the value has changed
                if (isolatedStore[Key] != value)
                {
                    // Store the new value
                    isolatedStore[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                isolatedStore.Add(Key, value);
                valueChanged = true;
            }

            return valueChanged;
        }


        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public valueType GetValueOrDefault<valueType>(string Key, valueType defaultValue)
        {
            valueType value;

            // If the key exists, retrieve the value.
            if (isolatedStore.Contains(Key))
            {
                value = (valueType)isolatedStore[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }

            return value;
        }


        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            isolatedStore.Save();
        }
        #endregion

        /// <summary>
        /// Property to get and set EnableRemote Setting Key.
        /// </summary>
        public bool EnableRemote
        {
            get
            {
                return GetValueOrDefault<bool>(EnableRemoteKeyName, EnableRemoteDefault);
            }
            set
            {
                AddOrUpdateValue(EnableRemoteKeyName, value);
                Save();
            }
        } 

        /// <summary>
        /// Property to get and set Lang Setting Key.
        /// </summary>
        public string Lang
        {
            get
            {
                return GetValueOrDefault<string>(LangKeyName, LangDefault);
            }
            set
            {
                AddOrUpdateValue(LangKeyName, value);
                Save();
            }
        }

        /// <summary>
        /// Property to get and set StartView Setting Key.
        /// </summary>
        public string StartView
        {
            get
            {
                return GetValueOrDefault<string>(StartViewKeyName, StartViewDefault);
            }
            set
            {
                AddOrUpdateValue(StartViewKeyName, value);
                Save();
            }
        } 
    }
}

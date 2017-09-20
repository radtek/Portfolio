using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Johnny.WP7.Dictionary
{
    public class LocalizedStrings
    {
        //#region Design Time
        //public LocalizedStrings()
        //{
        //}

        //private static Johnny.WP7.Dictionary.AppResources localizedresources = new Johnny.WP7.Dictionary.AppResources();

        //public Johnny.WP7.Dictionary.AppResources Localizedresources { get { return localizedresources; } }
        //#endregion

        //#region Design Time
        //public LocalizedStrings()
        //{
        //}

        //private static Johnny.WP7.Dictionary.AppResourcesDE localizedresourcesDE = new Johnny.WP7.Dictionary.AppResourcesDE();

        //public Johnny.WP7.Dictionary.AppResourcesDE Localizedresources { get { return localizedresourcesDE; } }
        //#endregion

        #region Run Time

        public LocalizedStrings()
        {

        }

        private static AppResources localizedresources = new AppResources();
        private static AppResourcesDE localizedresourcesDe = new AppResourcesDE();

        private object gResx = null;

        public object Localizedresources
        {
            get
            {
                switch (App.ConfigSettings.Lang)
                {
                    case "fr-FR":
                        gResx = localizedresources;
                        break;
                    case "de-DE":
                        gResx = localizedresourcesDe;
                        break;
                    case "it-IT":
                        gResx = localizedresources;
                        break;
                    case "es-ES":
                        gResx = localizedresources;
                        break;
                    case "en-GB":
                        gResx = localizedresources;
                        break;
                    case "en-US":
                        gResx = localizedresources;
                        break;
                    case "da-DK":
                        gResx = localizedresources;
                        break;
                    default:
                        gResx = localizedresources;
                        break;
                }
                return gResx;
            }
        }
        #endregion
    }
}

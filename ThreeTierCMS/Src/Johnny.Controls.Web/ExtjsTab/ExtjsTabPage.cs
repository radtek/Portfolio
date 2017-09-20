using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.ExtjsTab
{
    /// <summary>
    /// A MenuItem represents a single item in a menu.</summary>
    /// <remarks>A MenuItem is a single "item" in a menu.  Typically a MenuItem will have some <see cref="Text"/>
    /// associated with it, and often a <see cref="Url"/> or <see cref="CommandName"/>.  MenuItems can also optionally
    /// have a set of <see cref="SubItems"/>, which represents a nested submenu.</remarks>
    [ToolboxItem(false)]
    public class ExtjsTabPage : WebControl
    {     
        #region Contructors
        /// <summary>
        /// Creates a new MenuItem instance.
        /// </summary>
        public ExtjsTabPage() { }		// empty, default constructor        
        /// <summary>
        /// Creates a new MenuItem instance.
        /// </summary>
        public ExtjsTabPage(string itemText, string itemToolTip, string itemImage)
        {
            if ((itemText == string.Empty || itemText == null) && (itemImage == string.Empty || itemImage == null))
                throw new ArgumentException("Either itemText or itemImage must be specified.");
            Text = itemText;
            ToolTip = itemToolTip;
            Image = itemImage;
        }
        #endregion

        #region MenuItem Properties

        #region ToolTip
        /// <summary>
        /// Gets or sets the text displayed when the mouse pointer hovers over the Web server control.
        /// </summary>
        public new string ToolTip
        {
            get
            {
                object o = ViewState["MenuItemToolTip"];

                if (o != null)
                    return (string)o;
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MenuItemToolTip"] = value;
                ViewState.SetItemDirty("MenuItemToolTip", true);
            }
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets or sets the MenuItem's image.
        /// </summary>
        /// <remarks>If both of the MenuItem's <see cref="Text"/> and <see cref="Image"/> properties are specified, 
        /// <see cref="Text"/> will be used.</remarks>
        [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true),
        Description("The MenuItem's image.")
        ]
        public virtual string Image
        {
            get
            {
                object o = ViewState["ItemImage"];

                if (o != null)
                    return (string)o;
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ItemImage"] = value;
                ViewState.SetItemDirty("ItemImage", true);
            }
        }
        #endregion

        #region ImageAltText
        /// <summary>
        /// Gets or sets the alternate text for the MenuItem's image.
        /// </summary>
        /// <remarks>This value, if specified, is rendered as the <b>alt</b> attribute in the generated
        /// <b>&lt;img&gt;</b> element.</remarks>
        [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true),
        Description("The alternate text for the MenuItem's image.  Output as the Alt tag.")
        ]
        public virtual string ImageAltText
        {
            get
            {
                object o = ViewState["ItemImageAltText"];

                if (o != null)
                    return (string)o;
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ItemImageAltText"] = value;
                ViewState.SetItemDirty("ItemImageAltText", true);
            }
        }
        #endregion

        #region Text
        /// <summary>
        /// Gets or sets the MenuItem's text content.
        /// </summary>
        /// <remarks>For total customization of the appearance of the MenuItem, the <b>Text</b> property can have 
        /// HTML content.</remarks>
        /// <example>
        /// The following example illustrates using HTML content in the <b>Text</b> property:<p />[C#]
        /// <code>
        /// MenuItem mi = new MenuItem();
        /// mi.Text = "&lt;b&gt;This will be bold!&lt;/b&gt;";
        /// </code>
        /// </example>
        [
        Description("The MenuItem's text."),
        NotifyParentProperty(true),
        DefaultValue("")
        ]
        public virtual string Text
        {
            get
            {
                object o = ViewState["ItemText"];
                if (o != null)
                    return (string)o;
                else
                    return String.Empty;
            }
            set
            {
                ViewState["ItemText"] = value;
                ViewState.SetItemDirty("ItemText", true);
            }
        }
        #endregion

        #region TabPageID
        /// <summary>
        /// Gets or sets the MenuItem's ID.  It is not recommended that this be set directly.
        /// </summary>
        /// <remarks>The <b>ID</b> for each MenuItem is programmatically set in the <see cref="Menu"/> class's
        /// <see cref="Menu.BuildMenuItem"/> method.</remarks>
        [Browsable(false)]
        public virtual string TabPageID
        {
            get
            {
                object o = ViewState["TabPageID"];
                if (o != null)
                    return (string)o;
                else
                    return "ExtjsTabPageId";
            }
            set
            {
                ViewState["TabPageID"] = value;
                ViewState.SetItemDirty("TabPageID", true);
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Gets or sets the MenuItem's Url.
        /// </summary>
        /// <remarks>If a MenuItem has a <b>Url</b> value, the MenuItem is "clickable."  That is, the end user
        /// will be able to click the MenuItem and be whisked to the specified URL.<p />The <b>Url</b> value can use
        /// the ~ notation.  For this to work, though, the Menu class's <b>DefaultResolveUrl</b> property must be
        /// set to True.</remarks>
        /// <value>Specifies the URL the user will be whisked to when the MenuItem is clicked.  The default value
        /// is <b>String.Empty</b>.  The <b>Url</b> property is optional.</value>
        [
        Description("The optional URL for the MenuItem."),
        NotifyParentProperty(true),
        DefaultValue("")
        ]
        public virtual string Url
        {
            get
            {
                object o = ViewState["ItemURL"];
                if (o != null)
                    return (string)o;
                else
                    return String.Empty;
            }
            set
            {
                ViewState["ItemURL"] = value;
                ViewState.SetItemDirty("ItemURL", true);
            }
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets or sets the MenuItem's image.
        /// </summary>
        /// <remarks>If both of the MenuItem's <see cref="Text"/> and <see cref="Image"/> properties are specified, 
        /// <see cref="Text"/> will be used.</remarks>
        [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true),
        Description("The MenuItem's selected.")
        ]
        public virtual bool Selected
        {
            get
            {
                object o = ViewState["Selected"];

                if (o != null)
                    return (bool)o;
                else
                    return false;
            }
            set
            {
                ViewState["Selected"] = value;
                ViewState.SetItemDirty("Selected", true);
            }
        }
        #endregion

        #endregion
    }
}

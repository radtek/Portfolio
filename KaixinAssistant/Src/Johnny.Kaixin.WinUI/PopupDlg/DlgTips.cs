using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;


namespace Johnny.Kaixin.WinUI
{
    public partial class DlgTips : Form
    {
        private Dictionary<string, string> _versionList;

        public DlgTips()
        {
            InitializeComponent();
            _versionList = new Dictionary<string, string>();
        }

        private void DlgTips_Load(object sender, EventArgs e)
        {   
            try
            {
                string strxml = "";
                using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Johnny.Kaixin.WinUI.Resources.Versions.config")))
                {
                    strxml = streamReader.ReadToEnd();
                }

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);

                if (objXmlDoc == null)
                    return;

                DataView dv = GetData(objXmlDoc, "ZrAssistant/Versions");

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    _versionList.Add(dv.Table.Rows[ix][0].ToString(), dv.Table.Rows[ix][1].ToString());
                    cmbVersion.Items.Add(dv.Table.Rows[ix][0].ToString());
                }

                chkNeverDisplay.Checked = Properties.Settings.Default.NeverDisplay;
                cmbVersion.SelectedIndex = 0;
                SetTextValue();
                btnOk.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgTips", ex);
            }
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {              
                SetTextValue();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgTips", ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.NeverDisplay = chkNeverDisplay.Checked;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgTips", ex);
            }            
        }

        private void SetTextValue()
        {
            string description = "";
            _versionList.TryGetValue(cmbVersion.Text, out description);
            txtUpdateInfo.Text = description;
        }

        private static DataView GetData(XmlDocument xmldoc, string XmlPathNode)
        {
            //get data from xml file
            DataSet ds = new DataSet();
            DataView dv = new DataView();

            XmlNode node = xmldoc.SelectSingleNode(XmlPathNode);
            if (node == null)
                dv.Table = new DataTable("table0");
            else
            {
                StringReader read = new StringReader(node.OuterXml);

                ds.ReadXml(read);
                if (ds.Tables.Count < 1)
                    dv.Table = new DataTable("table0");
                else
                    dv = ds.Tables[0].DefaultView;
            }

            return dv;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

using Johnny.Kaixin.Helper;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.AccountTree
{
	/// <summary>
	/// Summary description for Nodes.
	/// </summary>
	public class BaseNode : TreeNode {        
        private NodeType _nodeType;
		public BaseNode(){}

        public BaseNode(string Text)
        {
            _nodeType = NodeType.Base;
			this.Text = Text;
            this.ToolTipText = Text;
            this.ImageIndex = DataConvert.GetInt32(IconType.Root);
            this.SelectedImageIndex = DataConvert.GetInt32(IconType.Root);
		}

        public NodeType NodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }
	}

    public class GroupNode : BaseNode
    {
        private Guid _groupid;
        private string _groupName;

        public GroupNode() { }

        public GroupNode(string groupName)
        {
            base.NodeType = NodeType.Group;
            _groupid = new Guid();
            _groupName = groupName;
            this.Tag = _groupid;
            this.Text = _groupName;
            this.ToolTipText = _groupName;
            this.ImageIndex = DataConvert.GetInt32(IconType.Users);
            this.SelectedImageIndex = DataConvert.GetInt32(IconType.Users);
        }

        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
    }    

    public class AccountNode : BaseNode
    {
        private AccountInfo _account;

        public AccountNode() { }

        public AccountNode(AccountInfo ai)
        {
            base.NodeType = NodeType.Account;
            _account = ai;
            if (ai.UserName == null || ai.UserName == string.Empty)
                this.Text = ai.Email;
            else
                this.Text = ai.UserName;
            this.ToolTipText = ai.Email;
            this.ImageIndex = DataConvert.GetInt32(IconType.User);
            this.SelectedImageIndex = DataConvert.GetInt32(IconType.User);
        }

        public AccountInfo Account
        {
            get { return _account; }
            set { _account = value; }
        }
    }    
}

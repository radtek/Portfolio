using System;
using System.Text;

namespace Johnny.Controls.Web.Calendar
{
	/// <summary>
	/// �� �� �ţ�JavaScriptWriter
	/// �� �� �ƣ�JavaScriptWriter
	/// ����ժҪ��   ��Ҫ������ʵ�����ڿؼ��ͻ��˽ű������
	/// </summary>
	internal class JavaScriptWriter
	{
		public JavaScriptWriter() {}

		#region ������

		private StringBuilder sb = new StringBuilder();
		private int currIndent = 0;
		private int openBlocks = 0;
		private bool format = false;

		#endregion		

		#region ������

		/// <summary>
		/// �����뵽ҳ��ʱ�Ƿ���Ҫ��ʽ
		/// </summary>
		/// <param name="Formatted">��Ҫ��ʽ?</param>
		public JavaScriptWriter(bool Formatted)
		{
			format = Formatted;
		}

		/// <summary>
		/// ��ǰ���������
		/// </summary>
		public int Indent
		{
			get { return currIndent; }
			set { currIndent = value; }
		}

		/// <summary>
		/// ����һ��javascript����
		/// </summary>
		/// <param name="parts">�����ִ�������</param>
		public void AddLine(params string[] parts)
		{
			// ����и�ʽ���ã��������������
			if (format)
				for (int i=0; i < currIndent; i++)
					sb.Append("\t");

				foreach (string part in parts)
				sb.Append(part);

			if (format)
				sb.Append(Environment.NewLine);
			else
				if (parts.Length > 0)
				sb.Append(" ");
		}

		/// <summary>
		/// ����"{"����ʹ�������һ��
		/// </summary>
		public void OpenBlock()
		{
			AddLine("{");
			currIndent++;
			openBlocks++;
		}

		/// <summary>
		/// ����"{"����ʹ�����չһ��
		/// </summary>
		public void CloseBlock()
		{
			// ���һ��function��û��"{"
			if (openBlocks < 1)
				throw new InvalidOperationException("�ڵ���JavaScriptWriter.CloseBlock()ʱû����ǰ��JavaScriptWriter.OpenBlock()����");

			currIndent--;
			openBlocks--;
			AddLine("}");
		}

		/// <summary>
		/// ����ע���(Ϊjavascript����ע��)
		/// </summary>
		/// <param name="CommentText">ע����ִ�����.</param>
		public void AddCommentLine(params string[] CommentText)
		{
			if (format)
			{
				for (int i=0; i < currIndent; i++)
					sb.Append("\t");

				sb.Append ("// ");

				foreach (string part in CommentText)
					sb.Append(part);

				sb.Append(Environment.NewLine);
			}
		}

		/// <summary>
		/// ת�������ͽ�����javascript�ı�ǣ������м�����Ѽ����javascrpt�Ĵ���
		/// </summary>
		/// <returns>���ر�׼��javascript����</returns>
		public override string ToString()
		{
			if (openBlocks > 0)
				throw new InvalidOperationException("JavaScriptWriter: û����Ӧ�Ĺرձ�ʶ");

			return String.Format(
				"<script language=\"javascript\" type=\"text/javascript\">{0}{1}</script>", 
				Environment.NewLine, 
				sb
				);
		}

		#endregion
	}
}

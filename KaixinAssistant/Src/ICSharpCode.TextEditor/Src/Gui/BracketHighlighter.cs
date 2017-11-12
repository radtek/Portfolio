﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 2659 $</version>
// </file>

using System;
using System.Drawing;
using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor
{
	public class BracketHighlight
	{
        //johnny
        //public TextLocation OpenBrace { get; set; }
        private TextLocation _openBrace;
        public TextLocation OpenBrace { get { return _openBrace; } set { _openBrace = value; } }
        //public TextLocation CloseBrace { get; set; } 
        private TextLocation _closeBrace;
        public TextLocation CloseBrace { get { return _closeBrace; } set { _closeBrace = value; } }
		
		public BracketHighlight(TextLocation openBrace, TextLocation closeBrace)
		{
			this.OpenBrace = openBrace;
			this.CloseBrace = closeBrace;
		}
	}
	
	public class BracketHighlightingSheme
	{
		char opentag;
		char closingtag;
		
		public char OpenTag {
			get {
				return opentag;
			}
			set {
				opentag = value;
			}
		}
		
		public char ClosingTag {
			get {
				return closingtag;
			}
			set {
				closingtag = value;
			}
		}
		
		public BracketHighlightingSheme(char opentag, char closingtag)
		{
			this.opentag    = opentag;
			this.closingtag = closingtag;
		}
		
		public BracketHighlight GetHighlight(IDocument document, int offset)
		{
			int searchOffset;
			if (document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After) {
				searchOffset = offset;
			} else {
				searchOffset = offset + 1;
			}
			char word = document.GetCharAt(Math.Max(0, Math.Min(document.TextLength - 1, searchOffset)));
			
			TextLocation endP = document.OffsetToPosition(searchOffset);
			if (word == opentag) {
				if (searchOffset < document.TextLength) {
					int bracketOffset = TextUtilities.SearchBracketForward(document, searchOffset + 1, opentag, closingtag);
					if (bracketOffset >= 0) {
						TextLocation p = document.OffsetToPosition(bracketOffset);
						return new BracketHighlight(p, endP);
					}
				}
			} else if (word == closingtag) {
				if (searchOffset > 0) {
					int bracketOffset = TextUtilities.SearchBracketBackward(document, searchOffset - 1, opentag, closingtag);
					if (bracketOffset >= 0) {
						TextLocation p = document.OffsetToPosition(bracketOffset);
						return new BracketHighlight(p, endP);
					}
				}
			}
			return null;
		}
	}
}
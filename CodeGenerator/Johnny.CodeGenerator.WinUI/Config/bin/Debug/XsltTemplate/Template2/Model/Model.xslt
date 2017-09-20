﻿<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output omit-xml-declaration="yes" indent="no" method="text" encoding="UTF-8"/>
	<xsl:template match="/">
		<xsl:text>using System;

namespace </xsl:text>
		<xsl:value-of select="entity/@namespace"/>
		<xsl:text>.Model
{
</xsl:text>
		<xsl:apply-templates select="entity"/>
		<xsl:text>
}
</xsl:text>
	</xsl:template>
	<xsl:template match="entity">
	/// &lt;summary&gt;
	/// <xsl:value-of select="@name"/> is an entity class that represents <xsl:value-of select="@description"/>
	/// &lt;/summary&gt;
	[Serializable]
	public class <xsl:value-of select="@modelName"/>
	{
		#region declaration
		private string _TableName = "<xsl:value-of select="@name"/>";
        private string _PrimaryKey = "<xsl:for-each select="./primaryKey/column">
            <xsl:value-of select="@columnName"/></xsl:for-each>";<xsl:for-each select="./columns/property">
		private <xsl:value-of select="@type"/><xsl:text> </xsl:text>
		<xsl:value-of select="@field"/>;</xsl:for-each>
		#endregion

		#region constructors
		/// &lt;summary&gt;
        /// Default constructor
        /// &lt;/summary&gt;
		public <xsl:value-of select="@modelName"/>() { }
		
		/// &lt;summary&gt;
        /// Full constructor
        /// &lt;/summary&gt;
		public <xsl:value-of select="@modelName"/>(<xsl:for-each select="./columns/property">
			<xsl:value-of select="@type"/>
			<xsl:text> </xsl:text>
			<xsl:value-of select="@lowerName"/>			
			<xsl:if test="position()!=last()">
				<xsl:text>, </xsl:text>
			</xsl:if>
		</xsl:for-each>)
        {<xsl:for-each select="./columns/property">
			this.<xsl:value-of select="@field"/> = <xsl:value-of select="@lowerName"/>
			<xsl:text>;</xsl:text>
		</xsl:for-each>
        }        
		#endregion

		#region property
		/// &lt;summary&gt;
        /// TableName of dbo.Province
        /// &lt;/summary&gt;
        public string TableName
        {
            get { return _TableName; }
        }
        /// &lt;summary&gt;
        /// PrimaryKey of dbo.Province
        /// &lt;/summary&gt;
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
        }
		<xsl:for-each select="./columns/property">
		/// &lt;summary&gt;
		/// <xsl:value-of select="@name"/>
			<xsl:text> is a </xsl:text>
			<xsl:value-of select="@type"/>
			<xsl:text> property that represents </xsl:text>
			<xsl:value-of select="@description"/>
		/// &lt;/summary&gt;
		public <xsl:value-of select="@type"/><xsl:text> </xsl:text>
			<xsl:value-of select="@name"/>
		{
			get	{ return <xsl:value-of select="@field"/>; }
			set	{ <xsl:value-of select="@field"/> = value; }
		}</xsl:for-each>
		#endregion		
	}</xsl:template>
</xsl:stylesheet>
SELECT   
Table_Name=d.name, 
Table_Description=isnull(f.value, ' '), 
[Sequence]=a.colorder, 
Column_Name=a.name, 
IsIdentity=case   when   COLUMNPROPERTY(a.id,a.name, 'IsIdentity')=1 then '1' else '0' end, 
IsPrimaryKey=case   when   exists(SELECT   1   FROM   sysobjects   where   xtype= 'PK '   and   name   in   ( 
SELECT   name   FROM   sysindexes   WHERE   indid   in( 
SELECT   indid   FROM   sysindexkeys   WHERE   id   =   a.id   AND   colid=a.colid 
)))   then   '1'   else   '0'   end, 
Data_Type=b.name, 
Column_Length=a.length, 
Precision_Length=COLUMNPROPERTY(a.id,a.name, 'PRECISION'), 
Scale=isnull(COLUMNPROPERTY(a.id,a.name, 'Scale'),0), 
IsNullable=case   when   a.isnullable=1   then   '1 'else   '0'   end, 
Default_Value=isnull(e.text, ' '), 
Column_Description=isnull(g.[value], ' ')
FROM   syscolumns   a 
left   join   systypes   b   on   a.xtype=b.xusertype 
inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype= 'U '   and   d.status> =0 
left   join   syscomments   e   on   a.cdefault=e.id 
left   join   sys.extended_properties   g   on   a.id=g.major_id   and   a.colid=g.minor_id   
left   join   sys.extended_properties   f   on   d.id=f.major_id   and   f.minor_id=0 
where   d.name= '#TableName#'         --���ֻ��ѯָ����,���ϴ����� 
order   by   a.id,a.colorder

/*SELECT
	INFORMATION_SCHEMA.COLUMNS.*,
	COL_LENGTH('#TableName#', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IS_COMPUTED,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IS_ROWGUIDCOL,	
	IS_PRIMARYKEY = 
	CASE 
	    WHEN INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = pk_table.COLUMN_NAME THEN '1'
	    ELSE '0'
	  END,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'Precision') AS PRECISION_LEN,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'Scale') AS SCALE
FROM
	INFORMATION_SCHEMA.COLUMNS,
	(SELECT
	 T.TABLE_NAME,
	 COALESCE( CU.CONSTRAINT_NAME , '(no primary key)') 
	    AS PRIMARY_KEY_NAME,
	 CU.COLUMN_NAME 
	FROM INFORMATION_SCHEMA.TABLES AS T
	LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
	 ON TC.TABLE_CATALOG = T.TABLE_CATALOG
	 AND TC.TABLE_SCHEMA = T.TABLE_SCHEMA
	 AND TC.TABLE_NAME = T.TABLE_NAME
	 AND TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
	LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS CU
	 ON CU.CONSTRAINT_CATALOG = TC.CONSTRAINT_CATALOG
	 AND CU.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA
	 AND CU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
	WHERE
	 T.TABLE_TYPE = 'BASE TABLE'
	 AND T.TABLE_NAME = '#TableName#') AS pk_table 
WHERE
	INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '#TableName#'
ORDER BY ORDINAL_POSITION*/


/*SELECT
	INFORMATION_SCHEMA.COLUMNS.*,
	COL_LENGTH('#TableName#', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IS_COMPUTED,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IS_ROWGUIDCOL,	
	IS_PRIMARYKEY = 
	CASE 
	    WHEN INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = pk_table.COLUMN_NAME THEN '1'
	    ELSE '0'
	  END,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'Precision') AS PRECISION_LEN,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'Scale') AS SCALE
FROM
	INFORMATION_SCHEMA.COLUMNS,(select COLUMN_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE a
inner join INFORMATION_SCHEMA.TABLE_CONSTRAINTS b
on a.CONSTRAINT_NAME = b.CONSTRAINT_NAME
where a.table_name = '#TableName#' and constraint_type = 'Primary key') AS pk_table
WHERE
	INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '#TableName#'
ORDER BY ORDINAL_POSITION*/

/*SELECT
	INFORMATION_SCHEMA.COLUMNS.*,
	COL_LENGTH('#TableName#', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IS_COMPUTED,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY,
	COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IS_ROWGUIDCOL
FROM
	INFORMATION_SCHEMA.COLUMNS
WHERE
	INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '#TableName#'*/
	
/*
SELECT   
����=case   when   a.colorder=1   then   d.name   else   ' '   end, 
��˵��=case   when   a.colorder=1   then   isnull(f.value, ' ')   else   ' '   end, 
�ֶ����=a.colorder, 
�ֶ���=a.name, 
��ʶ=case   when   COLUMNPROPERTY(   a.id,a.name, 'IsIdentity ')=1   then   '�� 'else   ' '   end, 
����=case   when   exists(SELECT   1   FROM   sysobjects   where   xtype= 'PK '   and   name   in   ( 
SELECT   name   FROM   sysindexes   WHERE   indid   in( 
SELECT   indid   FROM   sysindexkeys   WHERE   id   =   a.id   AND   colid=a.colid 
)))   then   '�� '   else   ' '   end, 
����=b.name, 
ռ���ֽ���=a.length, 
����=COLUMNPROPERTY(a.id,a.name, 'PRECISION '), 
С��λ��=isnull(COLUMNPROPERTY(a.id,a.name, 'Scale '),0), 
�����=case   when   a.isnullable=1   then   '�� 'else   ' '   end, 
Ĭ��ֵ=isnull(e.text, ' '), 
�ֶ�˵��=isnull(g.[value], ' '), 
��������=isnull(h.��������, ' '), 
����˳��=isnull(h.����, ' ') 
FROM   syscolumns   a 
left   join   systypes   b   on   a.xtype=b.xusertype 
inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype= 'U '   and   d.status> =0 
left   join   syscomments   e   on   a.cdefault=e.id 
left   join   sys.extended_properties   g   on   a.id=g.id   and   a.colid=g.smallid     
left   join   sys.extended_properties   f   on   d.id=f.id   and   f.smallid=0 
left   join(--�ⲿ����������Ϣ,���Ҫ��ʾ��������ֶεĶ�Ӧ��ϵ,����ֻҪ�˲��� 
select   ��������=a.name,c.id,d.colid 
,����=case   indexkey_property(c.id,b.indid,b.keyno, 'isdescending ') 
when   1   then   '���� '   when   0   then   '���� '   end 
from   sysindexes   a 
join   sysindexkeys   b   on   a.id=b.id   and   a.indid=b.indid 
join   (--������������ж������ʱ,ȡ��������С���Ǹ� 
select   id,colid,indid=min(indid)   from   sysindexkeys 
group   by   id,colid)   b1   on   b.id=b1.id   and   b.colid=b1.colid   and   b.indid=b1.indid 
join   sysobjects   c   on   b.id=c.id   and   c.xtype= 'U '   and   c.status> =0 
join   syscolumns   d   on   b.id=d.id   and   b.colid=d.colid 
where   a.indid   not   in(0,255) 
)   h   on   a.id=h.id   and   a.colid=h.colid 
--where   d.name= 'Ҫ��ѯ�ı� '         --���ֻ��ѯָ����,���ϴ����� 
order   by   a.id,a.colorder
*/
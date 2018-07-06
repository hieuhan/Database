Imports CodeSmith.Engine
Imports SchemaExplorer
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports Microsoft.VisualBasic 

Public Class Utilities
	Inherits CodeTemplate

#Region "Private members"
	Const DEFAULT_STRING_VALUE As String = "'d3fault!-!str1ng!-!valu3'"
	Const DEFAULT_DATETIME_VALUE As String = "'12/31/9999 12:00:00 AM'"
	Const DEFAULT_INT_VALUE As String = "-2147483648"
#End Region

#Region "Protected methods"

	Protected Function GetHelper(ByVal table As TableSchema) As TemplateHelper
		Return TemplateHelper.Instance(table)
	End Function
#End Region

#Region "Public methods"

	Public Function MakeFirstUpper(ByVal name As String) As String
		If name.Length <= 1 Then
			Return name.ToUpper()
		End If

		Dim letters As Char() = name.ToCharArray()
		letters(0) = [Char].ToUpper(letters(0))

		Return New String(letters)
	End Function

	Protected Function GetTablesToJoin(ByVal helper As TemplateHelper) As List(Of TableJoin)
		Dim tableJoins As New List(Of TableJoin)()

		For Each column As RenderedColumn In helper.ForeignColumns
			Dim columnName As String = IIf(column.Name.ToLower().EndsWith("id"),column.Name.Substring(0, column.Name.Length - 2),column.Name)

			Dim tableJoin As New TableJoin()
			tableJoin.TableName = helper.Table.FullName
			tableJoin.TableShortName = helper.Table.Name
			tableJoin.TableColumn = column.Name
			tableJoin.ForeignTableName = column.PrimaryKeyTableFullName
			tableJoin.ForeignTableColumn = column.PrimaryKeyColumnName

			If columnName.Substring(0, columnName.Length - 2) <> tableJoin.TableName Then
				tableJoin.ForeignTableAlias = columnName
			End If

			tableJoin.TableColumnAlias = String.Concat(tableJoin.ForeignTableAlias, "_", tableJoin.ForeignTableColumn)
			tableJoins.Add(tableJoin)

			If column.CanBeNull Then
				tableJoin.IsLeftOuterJoin = True
			End If
		Next

		Return tableJoins
	End Function

	Public Shared Function GetCamelCaseName(ByVal value As String) As String
		Return String.Concat(value.Substring(0, 1).ToLower(), value.Substring(1))
	End Function

	Public Function GetCamelCaseClassName(ByVal table As TableSchema) As String
		Return String.Concat(table.Name.Substring(0, 1).ToLower(), table.Name.Substring(1))
	End Function

	Public Shared Function GetClassName(ByVal table As TableSchema) As String
		Return table.Name
	End Function

	Public Shared Function GetClassListName(ByVal table As TableSchema) As String
		Return GetClassListName(table.Name)
	End Function

	Public Shared Function GetClassListName(ByVal tableName As String) As String
		If tableName.EndsWith("ss") OrElse tableName.EndsWith("us") Then
			Return String.Concat(tableName, "es")
		Else
			Return String.Concat(tableName, "s")
		End If
	End Function

	Public Shared Function GetIndexes(ByVal table As TableSchema) As ListDictionary
		Dim indexes As New ListDictionary()

		Dim i As Int32 = 0
		While i < table.Indexes.Count
			If (Not indexes.Contains(table.Indexes(i).MemberColumns(0).Name)) AndAlso (table.Indexes(i).MemberColumns(0).Name <> table.PrimaryKey.MemberColumns(0).Name) Then
				indexes.Add(table.Indexes(i).MemberColumns(0).Name, table.Indexes(i).MemberColumns(0))
			End If
			System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
		End While
		
		i = 0
		While i < table.ForeignKeys.Count
			If Not indexes.Contains(table.ForeignKeys(i).ForeignKeyMemberColumns(0).Name) Then
				indexes.Add(table.ForeignKeys(i).ForeignKeyMemberColumns(0).Name, table.ForeignKeys(i).ForeignKeyMemberColumns(0))
			End If
			System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
		End While

		Return indexes
	End Function

	Public Shared Function GetStringWithSpaces(ByVal camelCaseString As String) As String
		Dim stringWithSpaces As String = String.Empty

		If camelCaseString.Length > 0 Then
			Dim stringBuilder As New StringBuilder()
			stringBuilder.Append(camelCaseString.Substring(0, 1))

			Dim i As Int32 = 1
			While i < camelCaseString.Length
				Dim character As Char = Convert.ToChar(camelCaseString.Substring(i, 1))

				If [Char].IsUpper(character) Then
					stringBuilder.Append(" ")
				End If

				stringBuilder.Append(character)
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While

			stringWithSpaces = stringBuilder.ToString()
		End If

		Return stringWithSpaces
	End Function

	Public Function GetMemberVariableName(ByVal column As ColumnSchema) As String
		Dim memberVariableName As String = String.Empty
		Dim propertyName As String = GetPropertyName(column)

		If Not propertyName.StartsWith("_") Then
			memberVariableName = String.Concat("_", propertyName)
		Else
			memberVariableName = propertyName
		End If

		Return memberVariableName
	End Function

	Public Function GetDefaultSQLValue(ByVal nativeType As String, ByVal includeComparison As Boolean, ByVal length As Int32) As String
		Select Case nativeType
			Case "int"
				Return String.Concat(IIf(includeComparison,"!= ",String.Empty), DEFAULT_INT_VALUE)
			Case "nvarchar", "varchar"
				If DEFAULT_STRING_VALUE.Length > length Then
					Return String.Concat(IIf(includeComparison,"!= ",String.Empty), IIf(length > 1,DEFAULT_STRING_VALUE.Substring(0, length - 1),"'"), "'")
				Else
					Return String.Concat(IIf(includeComparison,"!= ",String.Empty), DEFAULT_STRING_VALUE)
				End If
			Case "text", "ntext"
				Return IIf(includeComparison,"Is Not Null","Null")
			Case "datetime"
				Return String.Concat(IIf(includeComparison,"!= ",String.Empty), DEFAULT_DATETIME_VALUE)
			Case "bit"
				Return String.Concat(IIf(includeComparison,"Is Not Null","Null"))
			Case Else
				Return IIf(includeComparison,"Is Not Null","Null")
		End Select
	End Function

	Public Function GetDefaultSQLValue(ByVal nativeType As String, ByVal includeComparison As Boolean) As String
		Return GetDefaultSQLValue(nativeType, includeComparison, False)
	End Function

	Public Function GetDefaultSQLValue(ByVal nativeType As String, ByVal includeComparison As Boolean, ByVal isForeignKey As Boolean) As String
		Select Case nativeType
			Case "int"
				Return String.Concat(IIf(includeComparison, IIf(isForeignKey,"Is Not ","!= "),String.Empty), IIf(isForeignKey,"Null",DEFAULT_INT_VALUE))
			Case "nvarchar", "varchar"
				Return String.Concat(IIf(includeComparison,"!= ",String.Empty), DEFAULT_STRING_VALUE)
			Case "text", "ntext"
				Return IIf(includeComparison,"Is Not Null","Null")
			Case "datetime"
				Return String.Concat(IIf(includeComparison,"!= ",String.Empty), DEFAULT_DATETIME_VALUE)
			Case Else
				Return IIf(includeComparison,"Is Not Null","Null")
		End Select
	End Function

	Public Function GetPrimaryKeyName(ByVal table As TableSchema) As String
		If table.PrimaryKey IsNot Nothing Then
			If table.PrimaryKey.MemberColumns.Count = 1 Then
				Return table.PrimaryKey.MemberColumns(0).Name
			Else
				Throw New ApplicationException("This template will not work on primary keys with more than one member column.")
			End If
		Else
			Throw New ApplicationException("This template will only work on tables with a primary key.")
		End If
	End Function

	Public Function GetPropertyName(ByVal column As ColumnSchema) As String
		Dim propertyName As String = column.Name

		If propertyName = String.Concat(column.Table.Name, "Descriptor") Then
			Return "Descriptor"
		End If

		If propertyName.StartsWith("_") Then
			propertyName = propertyName.Substring(1)
		End If

		Return propertyName
	End Function

	Protected Function GetManagerDataSetAssignmentStatement(ByVal column As RenderedColumn) As String
		Dim statement As New StringBuilder()

		If column.Column.NativeType.ToLower().Equals("xml") Then
			statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(".LoadXml(CStr(row(""").Append(column.Name).Append(""")))")
		End If

		If column.Column.DataType = DbType.Guid Then
			statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(" = ")
			statement.Append("new Guid((CStr(row(String.Concat(prefix, """).Append(column.Name).Append("""))))")
		End If

		If (Not column.Column.NativeType.ToLower().Equals("xml")) AndAlso (column.Column.DataType <> DbType.Guid) Then
			statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(" = ")
			statement.Append("System.Convert.To").Append(GetVBVariableType(column.Column)).Append("(row(String.Concat(prefix, """).Append(column.Name).Append(""")))")
		End If

		Return statement.ToString()
	End Function

	Protected Function GetManagerReaderAssignmentStatement(ByVal column As RenderedColumn) As String
		Dim statement As New StringBuilder()

		If column.Column.DataType = DbType.Guid Then
			statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(" = ")
			statement.Append("reader.GetGuid(reader.GetOrdinal(String.Concat(prefix, """).Append(column.Name).Append(""")))")
		Else

			If column.Column.NativeType.ToLower().Equals("xml") Then
				statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(".LoadXml(CStr(reader(""").Append(column.Name).Append("""))) ")
			Else
				statement.Append(GetCamelCaseClassName(column.Column.Table)).Append(".").Append(column.Name).Append(" = ")
				statement.Append("System.Convert.To").Append(GetVBVariableType(column.Column)).Append("(reader(String.Concat(prefix, """).Append(column.Name).Append(""")))")
			End If
		End If

		Return statement.ToString()
	End Function

	Public Function GetSqlDbType(ByVal column As ColumnSchema) As String
		Select Case column.NativeType
			Case "bigint"
				Return "BigInt"
			Case "binary"
				Return "Binary"
			Case "bit"
				Return "Bit"
			Case "char"
				Return "Char"
			Case "datetime"
				Return "DateTime"
			Case "decimal"
				Return "Decimal"
			Case "float"
				Return "Float"
			Case "image"
				Return "Image"
			Case "int"
				Return "Int"
			Case "money"
				Return "Money"
			Case "nchar"
				Return "NChar"
			Case "ntext", "richtext"
				Return "NText"
			Case "numeric"
				Return "Decimal"
			Case "nvarchar", "picture"
				Return "NVarChar"
			Case "phone"
				Return "NVarChar"
			Case "real"
				Return "Real"
			Case "smalldatetime"
				Return "SmallDateTime"
			Case "smallint"
				Return "SmallInt"
			Case "smallmoney"
				Return "SmallMoney"
			Case "sql_variant"
				Return "Variant"
			Case "sysname"
				Return "NChar"
			Case "text"
				Return "Text"
			Case "timestamp"
				Return "Timestamp"
			Case "tinyint"
				Return "TinyInt"
			Case "uniqueidentifier"
				Return "UniqueIdentifier"
			Case "varbinary"
				Return "VarBinary"
			Case "varchar"
				Return "VarChar"
			Case "xml"
				Return "Xml"
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Shared Function GetSqlLength(ByVal column As ColumnSchema) As String
		Select Case column.NativeType.ToLower()
			Case "bigint", "binary", "bit", "datetime", "decimal", "float", _
				"int", "money", "ntext", "richtext", "numeric", "picture", _
				"phone", "real", "smalldatetime", "smallint", "smallmoney", "sql_variant", _
				"text", "timestamp", "tinyint", "uniqueidentifier", "varbinary", "xml"
				Return String.Empty
			Case "char", "image", "nchar", "nvarchar", "sysname", "varchar"
				Return String.Concat("(", column.Size, ")")
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Function GetVBVariableType(ByVal column As ColumnSchema) As String
		Select Case column.DataType
			Case DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.StringFixedLength, DbType.String
				Return "String"
			Case DbType.Binary
				Return "Byte()"
			Case DbType.[Byte]
				Return "Byte"
			Case DbType.[Boolean]
				Return "Boolean"
			Case DbType.Currency, DbType.[Single], DbType.[Decimal], DbType.[Double]
				Return "Double"
			Case DbType.[Date], DbType.DateTime
				Return "DateTime"
			Case DbType.Guid
				Return "Guid"
			Case DbType.Int16
				Return "Int16"
			Case DbType.Int32
				Return "Int32"
			Case DbType.Int64
				Return "Int64"
			Case DbType.[SByte]
				Return "SByte"
			Case DbType.Time
				Return "TimeSpan"
			Case DbType.UInt16
				Return "UShort"
			Case DbType.UInt32
				Return "UInt"
			Case DbType.UInt64
				Return "ULong"
			Case DbType.VarNumeric
				Return "Decimal"
			Case DbType.[Object]
				If column.NativeType.ToLower().Equals("xml") Then
					Return "XmlDocument"
				Else
					Return "Object"
				End If
			Case DbType.Xml
				Return "XmlDocument"
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

    Public Function GetVBVariableDefaultValue(ByVal column As ColumnSchema) As String
		Select Case column.DataType
			Case DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.StringFixedLength, DbType.String
				Return " = " + chr(34) + chr(34)
			Case DbType.Binary
				Return ""
			Case DbType.[Byte]
				Return " = 0"
			Case DbType.[Boolean]
				Return " = False"
			Case DbType.Currency, DbType.[Single], DbType.[Decimal], DbType.[Double]
				Return " = 0"
			Case DbType.[Date], DbType.DateTime
				Return " = DateTime.MinValue"
			Case DbType.Guid
				Return ""
			Case DbType.Int16
				Return " = 0"
			Case DbType.Int32
				Return " = 0"
			Case DbType.Int64
				Return " = 0"
			Case DbType.[SByte]
				Return ""
			Case DbType.Time
				Return ""
			Case DbType.UInt16
				Return " = 0"
			Case DbType.UInt32
				Return " = 0"
			Case DbType.UInt64
				Return " = 0"
			Case DbType.VarNumeric
				Return " = 0"
			Case DbType.[Object]
				If column.NativeType.ToLower().Equals("xml") Then
					Return ""
				Else
					Return ""
				End If
			Case DbType.Xml
				Return ""
			Case Else
				Return ""
		End Select
	End Function
    
	Public Shared Function GetControl(ByVal column As ColumnSchema) As String
		If column.IsForeignKeyMember Then
			Return "System.Web.UI.WebControls.DropDownList"
		End If

		Select Case column.NativeType
			Case "bigint", "binary", "image", "nchar", "real", "sql_variant", _
				"sysname", "timestamp", "uniqueidentifier", "varbinary"
				Return String.Empty
			Case "decimal", "numeric", "smallint", "tinyint", "nvarchar", "varchar", _
				"xml", "phone", "char", "float", "int", "money"
				Return "System.Web.UI.WebControls.TextBox"
			Case "bit"
				Return "System.Web.UI.WebControls.CheckBox"
			Case "datetime", "smalldatetime"
				Return "System.Web.UI.WebControls.TextBox"
			Case "ntext", "richtext", "text"
				Return "System.Web.UI.WebControls.TextBox"
			Case "smallmoney"
				Return "SmallMoney"
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Shared Function GetShortControl(ByVal column As ColumnSchema) As String
		Return GetShortControl(column, column.IsForeignKeyMember)
	End Function

	Public Shared Function GetShortControl(ByVal column As ColumnSchema, ByVal isForeignColumn As Boolean) As String
		If isForeignColumn Then
			Return "DropDownList"
		End If

		Select Case column.NativeType
			Case "binary", "image", "nchar", "real", "smalldatetime", "sql_variant", _
				"sysname", "timestamp", "uniqueidentifier", "varbinary"
				Return ""
			Case "decimal", "numeric", "smallint", "tinyint", "bigint", "char", _
				"float", "int", "money", "nvarchar", "phone", "varchar", _
				"xml"
				Return "TextBox"
			Case "bit"
				Return "CheckBox"
			Case "datetime"
				'Return "DateTextBox"
				Return "TextBox"
			Case "smallmoney"
				Return "SmallMoney"
			Case "ntext", "richtext", "text"
				'return "WebEditor"
				Return "TextBox"
			Case Else

				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Shared Function GetDataFieldName(ByVal column As ColumnSchema) As String
		Return GetDataFieldName(column, column.IsForeignKeyMember)
	End Function

	Public Shared Function GetDataFieldName(ByVal column As ColumnSchema, ByVal isForeignKey As Boolean) As String
		If isForeignKey Then
			Return "SelectedValue"
		End If

		Select Case column.NativeType
			Case "binary", "image", "real", "sql_variant", "sysname", "timestamp", "uniqueidentifier", "varbinary"
				Return String.Empty
			Case "tinyint", "smallint", "decimal", "numeric", "bigint", "char", "smalldatetime", "datetime", "float", _
				"int", "money", "nchar", "ntext", "richtext", "nvarchar", "phone", "smallmoney", "text", "varchar", "xml"
				Return "Text"
			Case "bit"
				Return "Checked"
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Shared Function GetDataFieldEmpty(ByVal column As ColumnSchema) As String
		Select Case column.NativeType
			Case "bigint", "binary", "char", "datetime", "decimal", "image", "numeric", "real", "smalldatetime", _
				"smallint", "sql_variant", "sysname", "timestamp", "tinyint", "uniqueidentifier", "varbinary", "xml"
				Return ""
			Case "float", "int", "money"
				Return "= 0"
			Case "nchar", "ntext", "richtext", "nvarchar", "phone", "text", "varchar"
				Return "= String.Empty"
			Case "smallmoney"
				Return "SmallMoney"
			Case "bit"
				Return "= false"
			Case Else
				Return String.Concat("__UNKNOWN__", column.NativeType)
		End Select
	End Function

	Public Shared Function GetDataFieldForm(ByVal column As RenderedColumn, ByVal tabIndex As Int32) As String
		Dim fieldText As New StringBuilder()

		If column.IsForeignKey Then
			Dim columnName As String = IIf(column.Name.ToLower().EndsWith("id"),column.Name.Substring(0, column.Name.Length - 2), column.Name)

			fieldText.Append(String.Format("<asp:dropdownlist id=""{0}DropDownList"" tabindex=""{1}"" datavaluefield=""Id"" datatextfield=""Descriptor"" datasource='<%# Get{2}{3}("" Select {4} "") %>' runat=""server"" CssClass=""formselect"" originalvalue='<%# Controller.{5}.Id %>'></asp:dropdownlist>", _
				column.Name, tabindex, column.PrimaryKeyTableName, IIf(column.PrimaryKeyTableName.EndsWith("ss") OrElse column.PrimaryKeyTableName.EndsWith("us"),"es","s"), Utilities.GetStringWithSpaces(column.Name), columnName))
			
			Return fieldText.ToString()
		Else
			Select Case column.Column.NativeType.ToLower()
				Case "bit"
					fieldText.Append("<asp:checkbox id=""").Append(column.Name).Append("CheckBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formoption"" Checked=""<%# Controller.").Append(column.Name).Append(" %>"" originalvalue='<%# Controller.").Append(column.Name).Append(" %>' />")
					Return fieldText.ToString()
				Case "char", "varchar", "nvarchar", "xml"
					fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# Controller.").Append(column.Name).Append(" %>' originalvalue='<%# Controller.").Append(column.Name).Append(" %>'></asp:textbox>")
					Return fieldText.ToString()
				Case "decimal", "numeric", "float", "bigint", "int", "phone", _
					"money"
					fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), String.Empty, Convert.ToString(Controller.").Append(column.Name).Append(")) %>' originalvalue='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), ""0"", Convert.ToString(Controller.").Append(column.Name).Append(")) %>'></asp:textbox>")
					Return fieldText.ToString()
				Case "richtext", "text", "ntext"
					fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), String.Empty, Convert.ToString(Controller.").Append(column.Name).Append(")) %>' originalvalue='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), ""0"", Convert.ToString(Controller.").Append(column.Name).Append(")) %>' Columns=""100"" Rows=""10"" TextMode=""MultiLine""></asp:textbox>")
					Return fieldText.ToString()
				Case "datetime"
					fieldText.Append(String.Format("<asp:TextBox id=""{0}TextBox"" runat=""server"" CssClass=""formtextbox"" tabindex=""{1}"" Text='<%# IIf(Controller.{0}.Equals(System.DateTime.MinValue), String.Empty, Controller.{0}.ToShortDateString()) %>' originalvalue='<%# IIf(Controller.{0}.Equals(System.DateTime.MinValue), String.Empty, Controller.{0}.ToShortDateString()) %>' />", column.Name, tabindex)).Append(System.Environment.NewLine & vbTab & vbTab & vbTab)
					fieldText.Append(String.Format("<asp:ImageButton id=""{0}TextBoxCalendarImage"" runat=""server"" ImageUrl=""~/lib/images/Calendar.gif"" AlternateText=""Click to show calendar"" />", column.Name)).Append(System.Environment.NewLine & vbTab & vbTab & vbTab)
					fieldText.Append(String.Format("<ajaxToolkit:CalendarExtender id=""{0}TextBoxCalendarExtender"" runat=""server"" TargetControlID=""{0}TextBox"" PopupButtonID=""{0}TextBoxCalendarImage"" />", column.Name))
					Return fieldText.ToString()
				Case Else
					Return String.Empty
			End Select
		End If
	End Function

	Public Shared Function GetDataFieldForm(ByVal column As ColumnSchema, ByVal tabIndex As Int32) As String
		Dim fieldText As New StringBuilder()

		If column.IsForeignKeyMember Then
			Dim table As TableSchema = column.Table
			Dim columnName As String = IIf(column.Name.ToLower().EndsWith("id"),column.Name.Substring(0, column.Name.Length - 2),column.Name)
			Dim KeyTableName As String = String.Empty

			Dim keyCounter As Int32 = 0
			While keyCounter < table.ForeignKeys.Count
				For Each columnSchema As ColumnSchema In table.ForeignKeys(keyCounter).ForeignKeyMemberColumns
					If columnSchema.Name = column.Name Then
						KeyTableName = table.ForeignKeys(keyCounter).PrimaryKeyMemberColumns(0).Table.Name
					End If
				Next
				System.Math.Max(System.Threading.Interlocked.Increment(keyCounter),keyCounter - 1)
			End While

			fieldText.Append(String.Format("<asp:dropdownlist id=""{0}DropDownList"" tabindex=""{1}"" datavaluefield=""Id"" datatextfield=""Descriptor"" datasource='<%# Get{2}{3}("" Select {4} "") %>' runat=""server"" originalvalue='<%# Controller.{5}.Id %>'></asp:dropdownlist>", _
				column.Name, tabindex, KeyTableName, IIf(KeyTableName.EndsWith("ss") OrElse KeyTableName.EndsWith("us"),"es","s"), Utilities.GetStringWithSpaces(column.Name), columnName))

			Return fieldText.ToString()
		End If

		Select Case column.NativeType.ToLower()
			Case "bit"
				Return fieldText.Append("<asp:checkbox id=""").Append(column.Name).Append("CheckBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" Checked=""<%# Controller.").Append(column.Name).Append(" %>"" originalvalue='<%# Controller.").Append(column.Name).Append(" %>' />").ToString()
			Case "char", "varchar", "nvarchar", "xml"
				Return fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# Controller.").Append(column.Name).Append(" %>' originalvalue='<%# Controller.").Append(column.Name).Append(" %>'></asp:textbox>").ToString()
			Case "float", "int", "phone", "money"
				Return fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), String.Empty, Convert.ToString(Controller.").Append(column.Name).Append(")) %>' originalvalue='<%# Controller.").Append(column.Name).Append(" %>'></asp:textbox>").ToString()
			Case "richtext", "text", "ntext"
				Return fieldText.Append("<asp:textbox id=""").Append(column.Name).Append("TextBox"" tabindex=""").Append(tabIndex).Append("""").Append(" runat=""server"" CssClass=""formtextbox"" Text='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), String.Empty, Convert.ToString(Controller.").Append(column.Name).Append(")) %>' originalvalue='<%# IIf(Controller.").Append(column.Name).Append(".Equals(0), ""0"", Convert.ToString(Controller.").Append(column.Name).Append(")) %>' Columns=""50"" Rows=""20""></asp:textbox>").ToString()
			Case "smalldatetime", "datetime"
				fieldText.Append(String.Format("<asp:TextBox id=""{0}TextBox"" tabindex=""{1}"" runat=""server"" Text='<%# IIf(Controller.{0}.Equals(System.DateTime.MinValue), String.Empty, Controller.{0}.ToShortDateString()) %>' originalvalue='<%# Controller.{0} %>' />", column.Name, tabindex)).Append(System.Environment.NewLine & vbTab & vbTab & vbTab)
				fieldText.Append(String.Format("<asp:ImageButton id=""{0}TextBoxCalendarImage"" runat=""server"" ImageUrl=""~/lib/images/Calendar.gif"" AlternateText=""Click to show calendar"" />", column.Name)).Append(System.Environment.NewLine & vbTab & vbTab & vbTab)
	            fieldText.Append(String.Format("<ajaxToolkit:CalendarExtender id=""{0}TextBoxCalendarExtender"" runat=""server"" TargetControlID=""{0}TextBox"" PopupButtonID=""{0}TextBoxCalendarImage"" />", column.Name))
				Return fieldText.ToString()
			Case Else
				Return String.Empty
		End Select
	End Function

	Public Shared Function GetPrimaryKeyTableFullName(ByVal column As ColumnSchema) As String
		Dim foreignColumns As New CrossDatabaseForeignColumnCollection(column.Table)
		Dim table As TableSchema = column.Table
		Dim keyTableName As String = String.Empty

		If column.IsForeignKeyMember Then
			Dim keyCounter As Int32 = 0
			While keyCounter < table.ForeignKeys.Count
				For Each member As ColumnSchema In table.ForeignKeys(keyCounter).ForeignKeyMemberColumns
					If member.Name = column.Name Then
						Return table.ForeignKeys(keyCounter).PrimaryKeyTable.FullName
					End If
				Next
				System.Math.Max(System.Threading.Interlocked.Increment(keyCounter),keyCounter - 1)
			End While
		Else
			If foreignColumns.IndexOf(column.Name) > -1 Then
				Return String.Concat(foreignColumns(column.Name).ForeignKeyDatabase, ".dbo.", foreignColumns(column.Name).ForeignKeyTableName)
			Else
				Return column.Table.FullName
			End If
		End If

		Return String.Empty
	End Function

	Public Shared Function GetPrimaryKeyTableName(ByVal column As ColumnSchema) As String
		Dim table As TableSchema = column.Table
		Dim foreignColumns As New CrossDatabaseForeignColumnCollection(table)
		Dim keyTableName As String = String.Empty

		If column.IsForeignKeyMember Then
			Dim keyCounter As Int32 = 0
			While keyCounter < table.ForeignKeys.Count
				For Each member As ColumnSchema In table.ForeignKeys(keyCounter).ForeignKeyMemberColumns
					If member.Name = column.Name Then
						Return table.ForeignKeys(keyCounter).PrimaryKeyTable.Name
					End If
				Next
				System.Math.Max(System.Threading.Interlocked.Increment(keyCounter),keyCounter - 1)
			End While
		Else
			If foreignColumns.IndexOf(column.Name) > -1 Then
				Return foreignColumns(column.Name).ForeignKeyTableName
			Else
				Return column.Table.Name
			End If
		End If

		Return String.Empty
	End Function

	Public Function IncrementCounter(ByVal value As Int32) As Int32
		Return System.Math.Max(System.Threading.Interlocked.Increment(value),value - 1)
	End Function
#End Region

#Region "Protected classes"

#Region "TemplateHelper class"

	Protected NotInheritable Class TemplateHelper
#Region "Private member variables"
		Private Shared _Instance As TemplateHelper
#End Region

#Region "Constructors, destructors and initializers"

		Private Sub New(ByVal table As TableSchema)
			CrossDatabaseForeignColumns = New CrossDatabaseForeignColumnCollection(table)
			InitializeColumnCollections(table)
			Me.table = table
			database = table.Database
			className = table.Name
			qualifiedDatabaseName = String.Concat(database.Name, ".", table.FullName)
			classListName = Utilities.GetClassListName(table)
			instanceName = Utilities.GetCamelCaseName(className)
		End Sub

		Private Sub InitializeColumnCollections(ByVal table As SchemaExplorer.TableSchema)
			Me.columns = New RenderedColumnCollection
			Me.PrimaryKey = New RenderedColumn(table.PrimaryKey.MemberColumns(0))
			Me.primaryColumns = New RenderedColumnCollection

			Dim i As Int32 = 0
			While i < table.PrimaryKey.MemberColumns.Count
				Dim pKey As New RenderedColumn(table.PrimaryKey.MemberColumns(i))
				Me.primaryColumns.Add(pKey)
				Me.columns.Add(pKey)
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While

			Me.foreignColumns = New RenderedColumnCollection
			Me.memberColumns = New RenderedColumnCollection
			Me.attributeColumns = New RenderedColumnCollection

			dependents = New DependentCollection

			For Each key As TableKeySchema In table.PrimaryKeys
				dependents.Add(New Dependent(key))
			Next

			Dim indexes As New System.Collections.SortedList()

			i = 0
			While i < table.NonPrimaryKeyColumns.Count
				If Not table.NonPrimaryKeyColumns(i).Name.StartsWith("_") Then
					columns.Add(New RenderedColumn(table.NonPrimaryKeyColumns(i)))

					If table.NonPrimaryKeyColumns(i).Name = "Name" Then
						Me.ContainsNameColumn = True
					End If

					If table.NonPrimaryKeyColumns(i).IsForeignKeyMember OrElse (Me.CrossDatabaseForeignColumns.IndexOf(table.NonPrimaryKeyColumns(i).Name) > -1) Then
						If Me.CrossDatabaseForeignColumns.IndexOf(table.NonPrimaryKeyColumns(i).Name) > -1 Then
							Me.foreignColumns.Add(New RenderedColumn(table.NonPrimaryKeyColumns(i), CrossDatabaseForeignColumns(table.NonPrimaryKeyColumns(i).Name)))
						Else
							Me.foreignColumns.Add(New RenderedColumn(table.NonPrimaryKeyColumns(i), True))
						End If

						indexes(table.NonPrimaryKeyColumns(i).Name) = table.NonPrimaryKeyColumns(i)
					Else
						Me.memberColumns.Add(New RenderedColumn(table.NonPrimaryKeyColumns(i)))
					End If
				Else
					Me.attributeColumns.Add(New RenderedColumn(table.NonPrimaryKeyColumns(i)))
				End If
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While


			For Each index As IndexSchema In table.Indexes
				For Each column As ColumnSchema In index.MemberColumns
					indexes(column.Name) = column
				Next
			Next

			indexColumns = New RenderedColumnCollection

			For Each item As System.Collections.DictionaryEntry In indexes
				indexColumns.Add(New RenderedColumn(DirectCast(item.Value, ColumnSchema)))
			Next

			filters = New RenderedColumnCollection

			For Each item As System.Collections.DictionaryEntry In indexes
				If Not (DirectCast(item.Value, ColumnSchema)).Name.StartsWith("_") AndAlso Not (DirectCast(item.Value, ColumnSchema)).IsPrimaryKeyMember AndAlso Not (DirectCast(item.Value, ColumnSchema)).IsForeignKeyMember AndAlso Not IsCrossDatabaseForeignKey((DirectCast(item.Value, ColumnSchema)).Name) Then
					filters.Add(New RenderedColumn(DirectCast(item.Value, ColumnSchema)))
				End If
			Next

			For Each member As RenderedColumn In ForeignColumns
				filters.Add(member)
			Next

			For Each member As RenderedColumn In MemberColumns
				If member.Column.NativeType.ToLower().Equals("bit") Then
					If filters.IndexOf(member) = -1 Then 'ERROR?
						filters.Add(member)
					End If
				End If
			Next
		End Sub
#End Region

#Region "Private Methods"

		Public Function IsCrossDatabaseForeignKey(ByVal name As String) As Boolean
			Return CrossDatabaseForeignColumns.IndexOf(name) > -1
		End Function
#End Region

#Region "Public methods"

		Public Shared Function Instance(ByVal table As TableSchema) As TemplateHelper
			If Nothing Is _Instance Then
				Return New TemplateHelper(table)
			Else
				Return _Instance
			End If
		End Function
#End Region

#Region "Public properties"

		Private _PrimaryKey As RenderedColumn
		<Browsable(False)> _
		Public Property PrimaryKey() As RenderedColumn
			Get
				Return _PrimaryKey
			End Get
			Set
				_PrimaryKey = value
			End Set
		End Property

		Private _AttributeColumns As RenderedColumnCollection
		Public Property AttributeColumns() As RenderedColumnCollection
			Get
				Return _AttributeColumns
			End Get
			Set
				_AttributeColumns = value
			End Set
		End Property

		Private _Database As DatabaseSchema
		<Browsable(False)> _
		Public Property Database() As DatabaseSchema
			Get
				Return _Database
			End Get
			Set
				_Database = value
			End Set
		End Property

		Private _ContainsNameColumn As Boolean = False
		<Browsable(False)> _
		Public Property ContainsNameColumn() As Boolean
			Get
				Return _ContainsNameColumn
			End Get
			Set
				_ContainsNameColumn = value
			End Set
		End Property

		Private _PrimaryColumns As RenderedColumnCollection
		<Browsable(False)> _
		Public Property PrimaryColumns() As RenderedColumnCollection
			Get
				Return _PrimaryColumns
			End Get
			Set
				_PrimaryColumns = value
			End Set
		End Property

		Private _ForeignColumns As RenderedColumnCollection
		<Browsable(False)> _
		Public Property ForeignColumns() As RenderedColumnCollection
			Get
				Return _ForeignColumns
			End Get
			Set
				_ForeignColumns = value
			End Set
		End Property

		Private _CrossDatabaseForeignColumns As CrossDatabaseForeignColumnCollection
		Public Property CrossDatabaseForeignColumns() As CrossDatabaseForeignColumnCollection
			Get
				If Nothing Is _CrossDatabaseForeignColumns Then
					_CrossDatabaseForeignColumns = New CrossDatabaseForeignColumnCollection()
				End If

				Return _CrossDatabaseForeignColumns
			End Get
			Set
				_CrossDatabaseForeignColumns = value
			End Set
		End Property

		Private _MemberColumns As RenderedColumnCollection
		<Browsable(False)> _
		Public Property MemberColumns() As RenderedColumnCollection
			Get
				Return _MemberColumns
			End Get
			Set
				_MemberColumns = value
			End Set
		End Property

		Private _Filters As RenderedColumnCollection
		Public Property Filters() As RenderedColumnCollection
			Get
				Return _Filters
			End Get
			Set
				_Filters = value
			End Set
		End Property

		Private _Columns As RenderedColumnCollection
		Public Property Columns() As RenderedColumnCollection
			Get
				Return _Columns
			End Get
			Set
				_Columns = value
			End Set
		End Property

		Private _Dependents As DependentCollection
		Public Property Dependents As DependentCollection
			Get
				If Nothing Is _Dependents Then
					_Dependents = New DependentCollection
				End If

				Return _Dependents
			End Get
			Set
				_Dependents = value
			End Set
		End Property

		Private _IndexColumns As RenderedColumnCollection
		<Browsable(False)> _
		Public Property IndexColumns() As RenderedColumnCollection
			Get
				Return Me._IndexColumns
			End Get
			Set
				Me._IndexColumns = value
			End Set
		End Property

		Private _ClassName As String
		Public Property ClassName() As String
			Get
				Return _ClassName
			End Get
			Set
				_ClassName = value
			End Set
		End Property

		Private _QualifiedDatabaseName As String
		Public Property QualifiedDatabaseName() As String
			Get
				Return _QualifiedDatabaseName
			End Get
			Set
				_QualifiedDatabaseName = value
			End Set
		End Property

		Private _ClassListName As String
		Public Property ClassListName() As String
			Get
				Return _ClassListName
			End Get
			Set
				_ClassListName = value
			End Set
		End Property

		Private _InstanceName As String
		Public Property InstanceName() As String
			Get
				Return _InstanceName
			End Get
			Set
				_InstanceName = value
			End Set
		End Property

		Private _Table As TableSchema
		Public Property Table() As TableSchema
			Get
				Return _Table
			End Get
			Set
				_Table = value
			End Set
		End Property
#End Region
	End Class
#End Region

#Region "CrossDatabaseForeignColumnCollection Class"
	Protected Class CrossDatabaseForeignColumnCollection
		Inherits CollectionBase

#Region "Constructors, destructors and initializers"

		Public Sub New()
		End Sub

		Public Sub New(ByVal table As TableSchema)
			If table.Database.Tables.Contains("CrossDatabaseForeignColumn") Then
				Dim dataTable As DataTable = table.Database.Tables("CrossDatabaseForeignColumn").GetTableData()

				For Each row As DataRow In dataTable.Rows
					If String.Compare(row("TableName").ToString().ToLower(), table.Name.ToLower()) = 0 Then
						Me.Add(New CrossDatabaseForeignColumn(row))
					End If
				Next
			End If
		End Sub
#End Region

#Region "Public methods"

		Public Function Add(ByVal val As CrossDatabaseForeignColumn) As Int32
			Return (List.Add(val))
		End Function

		Public Function IndexOf(ByVal val As CrossDatabaseForeignColumn) As Int32
			Return (List.IndexOf(val))
		End Function

		Public Function IndexOf(ByVal name As String) As Int32
			Dim i As Int32 = 0
			While i < List.Count
				Dim item As CrossDatabaseForeignColumn = DirectCast(List(i), CrossDatabaseForeignColumn)

				If item.TableColumn = name Then
					Return i
				End If
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While

			Return -1
		End Function

		Public Sub Insert(ByVal index As Int32, ByVal val As CrossDatabaseForeignColumn)
			List.Insert(index, val)
		End Sub

		Public Sub Remove(ByVal val As CrossDatabaseForeignColumn)
			List.Remove(val)
		End Sub

		Public Function Contains(ByVal val As CrossDatabaseForeignColumn) As Boolean
			Return (List.Contains(val))
		End Function
#End Region

#Region "Public properties"

		Public Default Property Item(ByVal index As Int32) As CrossDatabaseForeignColumn
			Get
				Return (DirectCast(List(index), CrossDatabaseForeignColumn))
			End Get
			Set
				List(index) = value
			End Set
		End Property

		Public Default Property Item(ByVal name As String) As CrossDatabaseForeignColumn
			Get
				Return (DirectCast(List(IndexOf(name)), CrossDatabaseForeignColumn))
			End Get
			Set
				List(IndexOf(name)) = value
			End Set
		End Property
#End Region

	End Class
#End Region

#Region "RenderedColumnCollection class"

	Public Class RenderedColumnCollection
		Inherits CollectionBase

#Region "Private methods"

		Private Function CreateCommaDelimitedList(ByVal includePrimary As Boolean) As String
			Dim sb As New StringBuilder()
			Dim first As Boolean = True

			For Each column As RenderedColumn In InnerList
				If includePrimary OrElse Not column.IsPrimaryKey Then
					If Not first Then
						sb.Append(", ")
					Else
						first = False
					End If

					sb.Append(column.Name)
				End If
			Next

			Return sb.ToString()
		End Function

		Private Function CreateCommaDelimitedSqlVariableList(ByVal includePrimary As Boolean) As String
			Dim sb As New StringBuilder()
			Dim first As Boolean = True

			For Each column As RenderedColumn In InnerList
				If includePrimary OrElse Not column.IsPrimaryKey Then
					If Not first Then
						sb.Append(", ")
					Else
						first = False
					End If

					sb.Append("@").Append(column.Name)
				End If
			Next

			Return sb.ToString()
		End Function

		Private Function CreateCommaDelimitedSqlVariableTypeList() As String
			Dim sb As New StringBuilder()
			Dim first As Boolean = True

			For Each column As RenderedColumn In InnerList
				If Not first Then
					sb.Append(", ")
				Else
					first = False
				End If

				sb.Append("@").Append(column.Name).Append(" ").Append(column.Column.NativeType).Append(Utilities.GetSqlLength(column.Column))
			Next

			Return sb.ToString()
		End Function
#End Region

#Region "Public methods"

		Public Function Add(ByVal val As RenderedColumn) As Int32
			Return (List.Add(val))
		End Function

		Public Function IndexOf(ByVal val As RenderedColumn) As Int32
			Return (List.IndexOf(val))
		End Function

		Public Function IndexOf(ByVal name As String) As Int32
			Dim i As Int32 = 0
			While i < List.Count
				Dim item As RenderedColumn = DirectCast(List(i), RenderedColumn)

				If item.Name = name Then
					Return i
				End If
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While

			Return -1
		End Function

		Public Sub Insert(ByVal index As Int32, ByVal val As RenderedColumn)
			List.Insert(index, val)
		End Sub

		Public Sub Remove(ByVal val As RenderedColumn)
			List.Remove(val)
		End Sub

		Public Function Contains(ByVal val As RenderedColumn) As Boolean
			Return (List.Contains(val))
		End Function


		Public Overloads Function ToString(ByVal stringType As String) As String
			Select Case stringType.ToLower()
				Case "commadelimitedlist"
					Return CreateCommaDelimitedList(True)
				Case "commadelimitedsqlvariablelist"

					Return CreateCommaDelimitedSqlVariableList(True)
				Case "commadelimitedsqlvariabletypelist"

					Return CreateCommaDelimitedSqlVariableTypeList()
				Case "commadelimitedlistnoprimarykey"

					Return CreateCommaDelimitedList(False)
				Case "commadelimitedsqlvariablelistnoprimarykey"

					Return CreateCommaDelimitedSqlVariableList(False)
				Case Else
					Return String.Empty
			End Select
		End Function
#End Region

#Region "Public properties"

		Public Default Property Item(ByVal index As Int32) As RenderedColumn
			Get
				Return (DirectCast(List(index), RenderedColumn))
			End Get
			Set
				List(index) = value
			End Set
		End Property

		Public Default Property Item(ByVal name As String) As RenderedColumn
			Get
				Return (DirectCast(List(IndexOf(name)), RenderedColumn))
			End Get
			Set
				List(IndexOf(name)) = value
			End Set
		End Property
#End Region

	End Class
#End Region

#Region "DepedentCollection class"

	Protected Class DependentCollection
		Inherits CollectionBase

#Region "Public methods"

		Public Function Add(ByVal val As Dependent) As Int32
			Return (List.Add(val))
		End Function

		Public Function IndexOf(ByVal val As Dependent) As Int32
			Return (List.IndexOf(val))
		End Function

		Public Function IndexOf(ByVal name As String) As Int32
			Dim i As Int32 = 0
			While i < List.Count
				Dim item As Dependent = DirectCast(List(i), Dependent)

				If item.VariableName = name Then
					Return i
				End If
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While

			Return -1
		End Function

		Public Sub Insert(ByVal index As Int32, ByVal val As Dependent)
			List.Insert(index, val)
		End Sub

		Public Sub Remove(ByVal val As Dependent)
			List.Remove(val)
		End Sub

		Public Function Contains(ByVal val As Dependent) As Boolean
			Return (List.Contains(val))
		End Function
#End Region

#Region "Public properties"

		Public Default Property Item(ByVal index As Int32) As Dependent
			Get
				Return (DirectCast(List(index), Dependent))
			End Get
			Set
				List(index) = value
			End Set
		End Property

		Public Default Property Item(ByVal name As String) As Dependent
			Get
				Return (DirectCast(List(IndexOf(name)), Dependent))
			End Get
			Set
				List(IndexOf(name)) = value
			End Set
		End Property
#End Region

	End Class
#End Region

#Region "Dependent class"

	Protected Class Dependent
#Region "Constructors, destructors and initializers"

		Public Sub New()
		End Sub

		Public Sub New(ByVal key As TableKeySchema)
			foreignKeyTable = key.ForeignKeyTable
			foreignKeyColumn = key.ForeignKeyMemberColumns(0)
			primaryKeyColumn = key.PrimaryKeyMemberColumns(0)
			Dim foreignKeyName As String = key.ForeignKeyMemberColumns(0).Name

			If String.Compare(foreignKeyColumn.Name, String.Concat(primaryKeyColumn.Table.Name, primaryKeyColumn.Name)) = 0 Then
				variableName = Utilities.GetClassListName(key.ForeignKeyTable.Name)
				displayName = Utilities.GetStringWithSpaces(Utilities.GetClassListName(key.ForeignKeyTable))
				displaySingleName = Utilities.GetStringWithSpaces(Utilities.GetClassName(key.ForeignKeyTable))
			Else
				If foreignKeyColumn.Name.ToLower().EndsWith("id") Then
					variableName = String.Concat(foreignKeyColumn.Name.Substring(0, foreignKeyColumn.Name.Length - 2), Utilities.GetClassListName(key.ForeignKeyTable))
					displayName = String.Concat(Utilities.GetStringWithSpaces(foreignKeyColumn.Name.Substring(0, foreignKeyColumn.Name.Length - 2)), " ", Utilities.GetStringWithSpaces(Utilities.GetClassListName(key.ForeignKeyTable)))
					displaySingleName = Utilities.GetStringWithSpaces(Utilities.GetClassName(key.ForeignKeyTable))
				Else
					variableName = String.Concat(key.ForeignKeyMemberColumns(0).Name, Utilities.GetClassListName(key.ForeignKeyTable.Name))
					displayName = String.Concat(Utilities.GetStringWithSpaces(foreignKeyColumn.Name), " ", Utilities.GetStringWithSpaces(Utilities.GetClassListName(key.ForeignKeyTable)))
					displaySingleName = String.Concat(Utilities.GetStringWithSpaces(foreignKeyColumn.Name), " ", Utilities.GetStringWithSpaces(Utilities.GetClassName(key.ForeignKeyTable)))
				End If
			End If
		End Sub
#End Region

#Region "Public properties"

		Private _PrimaryKeyColumn As ColumnSchema
		Public Property PrimaryKeyColumn() As ColumnSchema
			Get
				Return _PrimaryKeyColumn
			End Get
			Set
				_PrimaryKeyColumn = value
			End Set
		End Property


		Private _ForeignKeyColumn As ColumnSchema
		Public Property ForeignKeyColumn() As ColumnSchema
			Get
				Return _ForeignKeyColumn
			End Get
			Set
				_ForeignKeyColumn = value
			End Set
		End Property


		Private _VariableName As String
		Public Property VariableName() As String
			Get
				Return _VariableName
			End Get
			Set
				_VariableName = value
			End Set
		End Property


		Private _DisplayName As String = String.Empty
		Public Property DisplayName() As String
			Get
				Return _DisplayName
			End Get
			Set
				_DisplayName = value
			End Set
		End Property


		Private _DisplaySingleName As String = String.Empty
		Public Property DisplaySingleName() As String
			Get
				Return _DisplaySingleName
			End Get
			Set
				_DisplaySingleName = value
			End Set
		End Property

		Private _ForeignKeyTable As TableSchema
		Public Property ForeignKeyTable() As TableSchema
			Get
				Return _ForeignKeyTable
			End Get
			Set
				_ForeignKeyTable = value
			End Set
		End Property
#End Region

	End Class
#End Region

#Region "RenderedColumn class"

	Public Class RenderedColumn

#Region "Private declarations, variables"

		Private _CanBeNull As Boolean = False
		Private _ControlName As String = String.Empty
		Private _Column As ColumnSchema
		Private _DataFieldEmpty As String = String.Empty
		Private _DataFieldForm As String = String.Empty
		Private _DataFieldName As String = String.Empty
		Private _FullControlName As String = String.Empty
		Private _InstanceName As String = String.Empty
		Private _Label As String = String.Empty
		Private _IsForeignKey As Boolean = False
		Private _IsPrimaryKey As Boolean = False
		Private _PrimaryKeyTableName As String = String.Empty
		Private _PrimaryKeyTableFullName As String = String.Empty
		Private _Name As String = String.Empty
		Private _SystemType As String = String.Empty
		Private _TabIndex As Int32
		Private _VariableName As String
#End Region

#Region "Constructors, destructors, and initialize"

		Public Sub New()
		End Sub

		Public Sub New(ByVal column As ColumnSchema, ByVal foreignKey As CrossDatabaseForeignColumn)
			Me.CrossDatabaseForeignColumn = foreignKey
			InitializeRenderedColumn(column, True)
		End Sub

		Public Sub New(ByVal column As ColumnSchema, ByVal isForeignKey As Boolean)
			InitializeRenderedColumn(column, isForeignKey)
		End Sub

		Public Sub New(ByVal column As ColumnSchema)
			InitializeRenderedColumn(column, column.IsForeignKeyMember)
		End Sub

		Public Sub InitializeRenderedColumn(ByVal column As ColumnSchema, ByVal isForeignColumn As Boolean)
			Me.column = column

			If column IsNot Nothing Then
				canBeNull = column.AllowDBNull
				dataFieldEmpty = Utilities.GetDataFieldEmpty(column)
				controlName = Utilities.GetShortControl(column)
				fullControlName = Utilities.GetControl(column)
				instanceName = String.Concat(column.Name, Utilities.GetShortControl(column, isForeignColumn))
				Me.IsForeignKey = isForeignColumn
				dataFieldName = Utilities.GetDataFieldName(column, isForeignColumn)
				isPrimaryKey = column.IsPrimaryKeyMember

				If Not IsForeignKey Then
					label = Utilities.GetStringWithSpaces(column.Name)
				Else
					If column.Name.ToLower().EndsWith("id") Then
						label = GetStringWithSpaces(column.Name.Substring(0, column.Name.Length - 2))
					Else
						label = Utilities.GetStringWithSpaces(column.Name)
					End If
				End If

				primaryKeyTableName = Utilities.GetPrimaryKeyTableName(column)
				primaryKeyTableFullName = Utilities.GetPrimaryKeyTableFullName(column)
				name = column.Name
				systemType = IIf((String.Compare(column.NativeType, "xml", True) <> 0),column.SystemType.ToString().Replace("System.", String.Empty),"XmlDocument")

				If CrossDatabaseForeignColumn IsNot Nothing Then
					Me.Database = CrossDatabaseForeignColumn.ForeignKeyDatabase
					Me.PrimaryKeyTableName = CrossDatabaseForeignColumn.ForeignKeyTableName
					Me.PrimaryKeyTableFullName = String.Concat(CrossDatabaseForeignColumn.ForeignKeyDatabase, ".dbo.", CrossDatabaseForeignColumn.ForeignKeyTableName)
					Me.PrimaryKeyColumnName = CrossDatabaseForeignColumn.ForeignKeyTableColumn
				ElseIf column.IsForeignKeyMember Then
					For Each key As TableKeySchema In Me.Column.Table.ForeignKeys
						If key.ForeignKeyMemberColumns(0).Equals(Me.Column) Then
							Me.PrimaryKeyColumnName = key.PrimaryKeyMemberColumns(0).Name
							Me.Database = key.Database.Name
							Exit For
						End If
					Next
				Else
					Me.Database = column.Table.Database.Name
				End If

				If isForeignKey AndAlso column.Name.EndsWith("Id") Then
					variableName = String.Concat(column.Name.Substring(0, 1).ToLower(), column.Name.Substring(1, column.Name.Length - 3))
				Else
					variableName = String.Concat(column.Name.Substring(0, 1).ToLower(), column.Name.Substring(1, column.Name.Length - 1))
				End If
			End If
		End Sub
#End Region

#Region "Private methods"

		Private Function CreateSqlUpdateStatement() As String
			Return String.Concat(Me.name, " = @", Me.name)
		End Function
#End Region

#Region "Public methods"
		Public Overloads Function ToString(ByVal stringType As String) As String
			Select Case stringType.ToLower()
				Case "sqlupdatestatement"
					Return CreateSqlUpdateStatement()
				Case Else
					Return String.Empty
			End Select
		End Function
#End Region

#Region "Public properties"

		Public Property CanBeNull() As Boolean
			Get
				Return Me._CanBeNull
			End Get
			Set
				Me._CanBeNull = value
			End Set
		End Property


		Public Property Column() As ColumnSchema
			Get
				Return _Column
			End Get
			Set
				_Column = value
			End Set
		End Property


		Public Property ControlName() As String
			Get
				Return Me._ControlName
			End Get
			Set
				Me._ControlName = value
			End Set
		End Property


		Private _CrossDatabaseForeignColumn As CrossDatabaseForeignColumn
		Public Property CrossDatabaseForeignColumn() As CrossDatabaseForeignColumn
			Get
				Return _CrossDatabaseForeignColumn
			End Get
			Set
				_CrossDatabaseForeignColumn = value
			End Set
		End Property


		Public Property DataFieldEmpty() As String
			Get
				Return Me._DataFieldEmpty
			End Get
			Set
				Me._DataFieldEmpty = value
			End Set
		End Property


		Public Property DataFieldForm() As String
			Get
				If _DataFieldForm.Length.Equals(0) Then
					_DataFieldForm = Utilities.GetDataFieldForm(Me, TabIndex)
				End If

				Return Me._DataFieldForm
			End Get
			Set
				Me._DataFieldForm = value
			End Set
		End Property


		Public Property DataFieldName() As String
			Get
				Return Me._DataFieldName
			End Get
			Set
				Me._DataFieldName = value
			End Set
		End Property


		Private _Database As String
		Public Property Database() As String
			Get
				Return _Database
			End Get
			Set
				_Database = value
			End Set
		End Property

		Public Property FullControlName() As String
			Get
				Return _FullControlName
			End Get
			Set
				_FullControlName = value
			End Set
		End Property


		Public Property InstanceName() As String
			Get
				Return _InstanceName
			End Get
			Set
				_InstanceName = value
			End Set
		End Property


		Public Property IsForeignKey() As Boolean
			Get
				Return _IsForeignKey
			End Get
			Set
				_IsForeignKey = value
			End Set
		End Property


		Public Property IsPrimaryKey() As Boolean
			Get
				Return _IsPrimaryKey
			End Get
			Set
				_IsPrimaryKey = value
			End Set
		End Property


		Public Property Label() As String
			Get
				Return _Label
			End Get
			Set
				_Label = value
			End Set
		End Property


		Public Property PrimaryKeyTableFullName() As String
			Get
				Return _PrimaryKeyTableFullName
			End Get
			Set
				_PrimaryKeyTableFullName = value
			End Set
		End Property


		Public Property PrimaryKeyTableName() As String
			Get
				Return _PrimaryKeyTableName
			End Get
			Set
				_PrimaryKeyTableName = value
			End Set
		End Property


		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set
				_Name = value
			End Set
		End Property


		Public Property SystemType() As String
			Get
				Return _SystemType
			End Get
			Set
				_SystemType = value
			End Set
		End Property


		Public Property TabIndex() As Int32
			Get
				Return _TabIndex
			End Get
			Set
				_TabIndex = value
			End Set
		End Property


		Public Property VariableName() As String
			Get
				Return _VariableName
			End Get
			Set
				_VariableName = value
			End Set
		End Property


		Private _PrimaryKeyColumnName As String
		Public Property PrimaryKeyColumnName() As String
			Get
				Return _PrimaryKeyColumnName
			End Get
			Set
				_PrimaryKeyColumnName = value
			End Set
		End Property
#End Region

	End Class
#End Region

#Region "TableJoin class"
	Protected Class TableJoin

		Public ForeignTableAlias As String = String.Empty
		Public TableName As String = String.Empty
		Public TableShortName As String = String.Empty
		Public TableColumn As String = String.Empty
		Public ForeignTableName As String = String.Empty
		Public ForeignTableColumn As String = String.Empty
		Public TableColumnAlias As String = String.Empty
		Public IsLeftOuterJoin As Boolean = False

		Public Overloads Function ToString(ByVal type As String) As String
			Dim stringBuilder As New StringBuilder()

			Select Case type.ToLower()
				Case "column"
					stringBuilder.Append(Me.TableShortName).Append(".").Append(Me.TableColumn).Append(" As ").Append(Me.TableColumnAlias)
					Exit Select
				Case "join"

					stringBuilder.Append(IIf(Me.IsLeftOuterJoin,"Left Outer Join ","Join "))
					stringBuilder.Append(Me.ForeignTableName).Append(" ")
					stringBuilder.Append((IIf(Me.ForeignTableAlias = Me.ForeignTableName,String.Empty,String.Concat(Me.ForeignTableAlias, " (nolock) "))))
					stringBuilder.Append("On ").Append(Me.ForeignTableAlias).Append(".").Append(Me.ForeignTableColumn).Append(" = ").Append(Me.TableShortName).Append(".").Append(Me.TableColumn)
					Exit Select
			End Select

			Return stringBuilder.ToString()
		End Function
	End Class
#End Region

#Region "CrossDatabaseForeignColumn Class"
	Public Class CrossDatabaseForeignColumn
		Public Sub New()
		End Sub


		Public Sub New(ByVal row As DataRow)
			Me.TableColumn = row("TableColumn").ToString()
			Me.TableName = row("TableName").ToString()
			Me.ForeignKeyDatabase = row("ForeignKeyDatabase").ToString()
			Me.ForeignKeyTableColumn = row("ForeignKeyTableColumn").ToString()
			Me.ForeignKeyTableName = row("ForeignKeyTableName").ToString()
		End Sub


		Private _ForeignKeyDatabase As String
		Public Property ForeignKeyDatabase() As String
			Get
				Return _ForeignKeyDatabase
			End Get
			Set
				_ForeignKeyDatabase = value
			End Set
		End Property


		Private _ForeignKeyTableColumn As String
		Public Property ForeignKeyTableColumn() As String
			Get
				Return _ForeignKeyTableColumn
			End Get
			Set
				_ForeignKeyTableColumn = value
			End Set
		End Property


		Private _ForeignKeyTableName As String
		Public Property ForeignKeyTableName() As String
			Get
				Return _ForeignKeyTableName
			End Get
			Set
				_ForeignKeyTableName = value
			End Set
		End Property


		Private _TableColumn As String
		Public Property TableColumn() As String
			Get
				Return _TableColumn
			End Get
			Set
				_TableColumn = value
			End Set
		End Property


		Private _TableName As String
		Public Property TableName() As String
			Get
				Return _TableName
			End Get
			Set
				_TableName = value
			End Set
		End Property
	End Class
#End Region	
	
#End Region
End Class
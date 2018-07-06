Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports CodeSmith.Engine
Imports SchemaExplorer

Public Class Master
	Inherits CodeTemplate
	Public Sub New()
		MyBase.New()
	End Sub

	Private _outputDirectory As String = [String].Empty

	<Editor(GetType(System.Windows.Forms.Design.FolderNameEditor), GetType(System.Drawing.Design.UITypeEditor))> _
	<[Optional]()> _
	<Category("Output")> _
	<Description("The directory to output the results to.")> _
	Public Property CodeOutput() As String
		Get
			' default to the directory that the template is located in
			If _outputDirectory.Length = 0 Then
				Return Path.Combine(Me.CodeTemplateInfo.DirectoryName, "output\\")
			End If

			Return _outputDirectory
		End Get
		Set
			_outputDirectory = value
		End Set
	End Property

	Public Sub OutputTemplate(ByVal template As CodeTemplate)
		Me.CopyPropertiesTo(template)
		template.Render(Me.Response)
	End Sub
End Class

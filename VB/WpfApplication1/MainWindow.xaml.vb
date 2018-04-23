Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.ComponentModel
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Hierarchy

Namespace WpfApplication1
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = New List(Of TestData) (New TestData() {New TestData() With {.Text = "row1", .Number = 1}, New TestData() With {.Text = "row2", .Number = 2}})
		End Sub
	End Class
	Public Class TestData
		Private privateNumber As Integer
		Public Property Number() As Integer
			Get
				Return privateNumber
			End Get
			Set(ByVal value As Integer)
				privateNumber = value
			End Set
		End Property
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
	End Class
	Public Class LengthValidationRule
		Inherits ValidationRule
		Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
			Dim str As String = TryCast(value, String)
			If String.IsNullOrEmpty(str) Then
				Return New ValidationResult(False, "Value shouldn't be empty")
			End If
			Return ValidationResult.ValidResult
		End Function
	End Class
End Namespace

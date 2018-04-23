Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Controls
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Globalization

Namespace WpfApplication1
	Public Class ValidationRulesCollection
		Inherits List(Of ValidationRule)
	End Class
	Public NotInheritable Class GridColumnValidationHelper


		Private Sub New()
		End Sub
		Public Shared Function GetValidationRules(ByVal obj As GridColumn) As ValidationRulesCollection
			Return CType(obj.GetValue(ValidationRulesProperty), ValidationRulesCollection)
		End Function

		Public Shared Sub SetValidationRules(ByVal obj As GridColumn, ByVal value As ValidationRulesCollection)
			obj.SetValue(ValidationRulesProperty, value)
		End Sub

		Public Shared ReadOnly ValidationRulesProperty As DependencyProperty = DependencyProperty.RegisterAttached("ValidationRules", GetType(ValidationRulesCollection), GetType(GridColumnValidationHelper), New PropertyMetadata(Nothing,AddressOf OnValidationRulesChanged))

		Private Shared Sub OnValidationRulesChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim column As GridColumn = TryCast(d, GridColumn)
			If column Is Nothing Then
				Return
			End If
			RemoveHandler column.Validate, AddressOf OnColumnValidate
			If e.NewValue IsNot Nothing Then
				AddHandler column.Validate, AddressOf OnColumnValidate
			End If
		End Sub

		Private Shared Sub OnColumnValidate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
			Dim column As GridColumn = CType(sender, GridColumn)
			For Each rule As ValidationRule In GetValidationRules(column)
				Dim result As ValidationResult = rule.Validate(e.Value, CultureInfo.CurrentCulture)
				If (Not result.IsValid) Then
					e.IsValid = False
					e.ErrorContent = result.ErrorContent
					Exit For
				End If
			Next rule

		End Sub

	End Class
End Namespace

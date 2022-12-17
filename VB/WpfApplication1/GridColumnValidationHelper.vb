Imports System.Collections.Generic
Imports System.Windows.Controls
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Globalization

Namespace WpfApplication1

    Public Class ValidationRulesCollection
        Inherits List(Of ValidationRule)

    End Class

    Public Module GridColumnValidationHelper

        Public Function GetValidationRules(ByVal obj As GridColumn) As ValidationRulesCollection
            Return CType(obj.GetValue(ValidationRulesProperty), ValidationRulesCollection)
        End Function

        Public Sub SetValidationRules(ByVal obj As GridColumn, ByVal value As ValidationRulesCollection)
            obj.SetValue(ValidationRulesProperty, value)
        End Sub

        Public ReadOnly ValidationRulesProperty As DependencyProperty = DependencyProperty.RegisterAttached("ValidationRules", GetType(ValidationRulesCollection), GetType(GridColumnValidationHelper), New PropertyMetadata(Nothing, AddressOf OnValidationRulesChanged))

        Private Sub OnValidationRulesChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim column As GridColumn = TryCast(d, GridColumn)
            If column Is Nothing Then Return
            RemoveHandler column.Validate, New GridCellValidationEventHandler(AddressOf OnColumnValidate)
            If e.NewValue IsNot Nothing Then AddHandler column.Validate, New GridCellValidationEventHandler(AddressOf OnColumnValidate)
        End Sub

        Private Sub OnColumnValidate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim column As GridColumn = CType(sender, GridColumn)
            For Each rule As ValidationRule In GetValidationRules(column)
                Dim result As ValidationResult = rule.Validate(e.Value, CultureInfo.CurrentCulture)
                If Not result.IsValid Then
                    e.IsValid = False
                    e.ErrorContent = result.ErrorContent
                    Exit For
                End If
            Next
        End Sub
    End Module
End Namespace

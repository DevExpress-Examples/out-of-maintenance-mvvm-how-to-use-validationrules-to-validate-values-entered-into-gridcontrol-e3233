Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.ComponentModel

Namespace WpfApplication1

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.grid.ItemsSource = New List(Of TestData)() From {New TestData() With {.Text = "row1", .Number = 1}, New TestData() With {.Text = "row2", .Number = 2}}
        End Sub
    End Class

    Public Class TestData

        Public Property Number As Integer

        Public Property Text As String
    End Class

    Public Class LengthValidationRule
        Inherits ValidationRule

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As Globalization.CultureInfo) As ValidationResult
            Dim str As String = TryCast(value, String)
            If String.IsNullOrEmpty(str) Then Return New ValidationResult(False, "Value shouldn't be empty")
            Return ValidationResult.ValidResult
        End Function
    End Class
End Namespace

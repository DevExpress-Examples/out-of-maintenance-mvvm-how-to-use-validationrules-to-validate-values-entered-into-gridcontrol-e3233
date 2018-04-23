Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Dynamic
Imports System.ComponentModel

Namespace WpfApplication1
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Inherits Application
		Protected Overrides Sub OnActivated(ByVal e As EventArgs)
			MyBase.OnActivated(e)
			' Creating a dynamic dictionary.
			'dynamic person = new DynamicDictionary();

			'person.FirstName = "Ellen";
			'person.LastName = "Adams";

			'Console.WriteLine(person.firstname + " " + person.lastname);

			'Console.WriteLine(
			'    "Number of dynamic properties:" + person.Count);

			'foreach(var item in person.GetDynamicMemberNames()) {

			'}

		End Sub
	End Class
	' The class derived from DynamicObject.
	Public Class DynamicDictionary
		Inherits DynamicObject
		' The inner dictionary.
		Private dictionary As New Dictionary(Of String, Object)()

		' This property returns the number of elements
		' in the inner dictionary.
		Public ReadOnly Property Count() As Integer
			Get
				Return dictionary.Count
			End Get
		End Property

		' If you try to get a value of a property 
		' not defined in the class, this method is called.
		Public Overrides Function TryGetMember(ByVal binder As GetMemberBinder, <System.Runtime.InteropServices.Out()> ByRef result As Object) As Boolean
			' Converting the property name to lowercase
			' so that property names become case-insensitive.
			Dim name As String = binder.Name.ToLower()

			' If the property name is found in a dictionary,
			' set the result parameter to the property value and return true.
			' Otherwise, return false.
			Return dictionary.TryGetValue(name, result)
		End Function

		' If you try to set a value of a property that is
		' not defined in the class, this method is called.
		Public Overrides Function TrySetMember(ByVal binder As SetMemberBinder, ByVal value As Object) As Boolean
			' Converting the property name to lowercase
			' so that property names become case-insensitive.
			dictionary(binder.Name.ToLower()) = value

			' You can always add a value to a dictionary,
			' so this method always returns true.
			Return True
		End Function
	End Class

	'class Program {
	'    static void Main(string[] args) {

	'        // The following statement throws an exception at run time.
	'        // There is no "address" property,
	'        // so the TryGetMember method returns false and this causes a
	'        // RuntimeBinderException.
	'        // Console.WriteLine(person.address);
	'    }
	'}


End Namespace

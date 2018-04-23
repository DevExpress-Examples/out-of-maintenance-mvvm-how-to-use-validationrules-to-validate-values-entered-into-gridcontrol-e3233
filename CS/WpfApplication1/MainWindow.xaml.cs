using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Hierarchy;

namespace WpfApplication1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = new List<TestData>() { new TestData() { Text = "row1", Number = 1 }, new TestData() { Text = "row2", Number = 2 } };
        }
    }
    public class TestData {
        public int Number { get; set; }
        public string Text { get; set; }
    }
    public class LengthValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            string str = value as string;
            if(string.IsNullOrEmpty(str))
                return new ValidationResult(false, "Value shouldn't be empty");
            return ValidationResult.ValidResult;
        }
    }
}

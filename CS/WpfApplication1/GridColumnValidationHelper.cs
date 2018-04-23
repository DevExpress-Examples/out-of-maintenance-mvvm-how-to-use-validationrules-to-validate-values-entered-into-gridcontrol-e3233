using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Globalization;

namespace WpfApplication1 {
    public class ValidationRulesCollection : List<ValidationRule> { }
    public static class GridColumnValidationHelper {


        public static ValidationRulesCollection GetValidationRules(GridColumn obj) {
            return (ValidationRulesCollection)obj.GetValue(ValidationRulesProperty);
        }

        public static void SetValidationRules(GridColumn obj, ValidationRulesCollection value) {
            obj.SetValue(ValidationRulesProperty, value);
        }

        public static readonly DependencyProperty ValidationRulesProperty =
            DependencyProperty.RegisterAttached("ValidationRules", typeof(ValidationRulesCollection), typeof(GridColumnValidationHelper), new PropertyMetadata(null, OnValidationRulesChanged));

        static void OnValidationRulesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            GridColumn column = d as GridColumn;
            if(column == null)
                return;
            column.Validate -= new GridCellValidationEventHandler(OnColumnValidate);
            if(e.NewValue != null)
                column.Validate += new GridCellValidationEventHandler(OnColumnValidate);
        }

        static void OnColumnValidate(object sender, GridCellValidationEventArgs e) {
            GridColumn column = (GridColumn)sender;
            foreach(ValidationRule rule in GetValidationRules(column)) {
                ValidationResult result = rule.Validate(e.Value, CultureInfo.CurrentCulture);
                if(!result.IsValid) {
                    e.IsValid = false;
                    e.ErrorContent = result.ErrorContent;
                    break;
                }
            }
             
        }
     
    }
}

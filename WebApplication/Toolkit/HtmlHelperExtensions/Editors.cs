#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Utilities.Extensions;

#endregion

namespace WebApplication.Toolkit.HtmlHelperExtensions
{
    public static class Editors
    {
        /// <summary>
        ///     Returns an HTML input element for each property in the object that is represented by the expression, using
        ///     additional view data. If the user does not have edit right it is disabled.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="disabled">True if the editor should be disabled.</param>
        /// <param name="attributes">The attributes define if a control is 
        ///                             - Read-only (disabled)
        ///                             - Required, </param>
        ///                             - Automatically focus (at the beginning)
        /// <param name="autofocus"></param>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>
        ///     An HTML input element for each property in the object that is represented by the expression. If the user does
        ///     not have edit right it is disabled.
        /// </returns>
        public static IHtmlString CustomEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool disabled = false, IDictionary<string, object> attributes = null, bool autofocus = false)
        {
            ClearNull(ref attributes);
            AddAngularNgModelAttribute(expression, attributes);
            AddRequiredAttribute(expression, attributes);
            AddDisabledAttribute(html, attributes, disabled);
            AddDataTypeAttributes(expression, attributes);
            AddRangeAttributes(expression, attributes);
            AddStringLengthAttributes(expression, attributes);
            AddAutofocusAttribute(attributes, autofocus);
            var propertyName = GetSelectListValuesName(expression);

            return html.EditorFor(expression, new
            {
                htmlAttributes = attributes
            });
        }

        public static IHtmlString CustomTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool disabled = false, IDictionary<string, object> attributes = null, bool autofocus = false)
        {
            ClearNull(ref attributes);
            AddAngularNgModelAttribute(expression, attributes);
            AddRequiredAttribute(expression, attributes);
            AddDisabledAttribute(html, attributes, disabled);
            AddDataTypeAttributes(expression, attributes);
            AddRangeAttributes(expression, attributes);
            AddAutofocusAttribute(attributes, autofocus);
            //attributes.Add(new { class = "form-control" });
            return html.TextAreaFor(expression, new
            {
                htmlAttributes = attributes
            });
        }

        public static IHtmlString CustomTextFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            ClearNull(ref attributes);
            AddAngularNgModelAttribute(expression, attributes);
            AddRequiredAttribute(expression, attributes);
            AddDisabledAttribute(html, attributes);
            AddConvertToNumberAttribute<TProperty>(attributes);

            var propertyName = GetSelectListValuesName(expression);
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (propertyName.StartsWith("Car.CarModelId"))
                {
                    //propertyName = propertyName.Replace("Car.", "");
                    attributes.Add("disabled", "disabled");
                    //attributes.Add(new { disabled = "disabled", @readonly = "readonly" });
                }
            }
            return html.TextBoxFor(expression, new { htmlAttributes = attributes });
        }
        public static IHtmlString CustomDropDownListDisabled<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            ClearNull(ref attributes);
            AddAngularNgModelAttribute(expression, attributes);
            AddRequiredAttribute(expression, attributes);
            attributes.Add("disabled", "disabled");
            AddConvertToNumberAttribute<TProperty>(attributes);

            var propertyName = GetSelectListValuesName(expression);

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (propertyName.StartsWith("Car."))
                {
                    propertyName = propertyName.Replace("Car.", "");
                }
                else if (propertyName.SequenceEqual("Accident.AccidentTypeId"))
                {
                    propertyName = propertyName.Replace("Accident.AccidentTypeId", "AccidentTypeId");
                }
                if (propertyName.SequenceEqual("CarModel.VehicleTypeId"))
                {
                    propertyName = propertyName.Replace("CarModel.VehicleTypeId", "VehicleTypeId");
                }
                else if (propertyName.SequenceEqual("CarModel.VehicleCategoryId"))
                {
                    propertyName = propertyName.Replace("CarModel.VehicleCategoryId", "VehicleCategoryId");
                }
            }

            return html.DropDownList(propertyName, null, String.Empty, attributes);
        }
       
        /// <summary>
        ///     Returns an HTML dropdown list
        ///     additional view data. If the user does not have edit right it is disabled.
        /// </summary>
        /// <param name="html">This is the web page</param>
        /// <param name="expression">Contains the field</param>
        /// <param name="attributes">It has several angular events. For instance it uses ng-change and if it has changed value,
        ///     it informs several fields like YEB Rate of the client in the case of offer/order etc
        /// </param>
        /// <returns></returns>
        public static IHtmlString CustomDropDownList<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            ClearNull(ref attributes);
            AddAngularNgModelAttribute(expression, attributes);
            AddRequiredAttribute(expression, attributes);
            AddDisabledAttribute(html, attributes);
            AddConvertToNumberAttribute<TProperty>(attributes);

            var propertyName = GetSelectListValuesName(expression);

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (propertyName.StartsWith("Car."))
                {
                    propertyName = propertyName.Replace("Car.", "");
                }
                else if (propertyName.SequenceEqual("Accident.AccidentTypeId"))
                {
                    propertyName = propertyName.Replace("Accident.AccidentTypeId", "AccidentTypeId");
                }
                else if (propertyName.SequenceEqual("CarModel.VehicleTypeId"))
                {
                    propertyName = propertyName.Replace("CarModel.VehicleTypeId", "VehicleTypeId");
                }
                else if (propertyName.SequenceEqual("RepairShop.CityId"))
                {
                    propertyName = propertyName.Replace("RepairShop.CityId", "CityId");
                }
            }

            return html.DropDownList(propertyName, null, String.Empty, attributes);
        }

        /// <summary>
        ///     Returns a disabled textbox that displays the uploaded file's name
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString DocumentName(this HtmlHelper html, string propertyName)
        {
            var textBox = $"<input type='text' disabled='disabled' ng-model='ctrl.model.{propertyName}.Name'/>";
            return new MvcHtmlString(textBox);
        }

        /// <summary>
        ///     Returns a disabled textbox that displays the uploaded file's name
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString ChildDocumentName(this HtmlHelper html, string propertyName, string childEntity)
        {
            var textBox = $"<input type='text' disabled='disabled' ng-model='ctrl.model.{childEntity}.{propertyName}.Name'/>";
            return new MvcHtmlString(textBox);
        }

        public static IHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var label = html.LabelFor(expression, new { @class = "col-md-4 control-label" });
            return label;//html.LabelFor(expression, new { @class = "col-md-4 control-label" });
        }

        public static IHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            //return html.ValidationMessageFor(expression);
            string ctrl = html.ViewBag.ControllerName;
            var propName = expression.Body.ToString().Replace("model.", "");
            var propDisplayName = typeof(TModel).GetSubProperty(propName).GetDisplayName();

            //string processedPropName = string.Empty;
            //if (!string.IsNullOrWhiteSpace(propName) && propName.Contains("."))
            //{
            //    string[] splitted = propName.Split('.');
            //    if (splitted.Length == 2)
            //    {
            //        processedPropName = propName.Split('.').ElementAt(1);
            //    }
            //    else Debug.Assert(false);
            //}
            string controllerFormAndPropertyName = $"{ctrl}Form.{propName}.";

            // TODO: check if everything is needed, in order to save bandwidth
            var requiredValidation = $"<div ng-show='!{controllerFormAndPropertyName}$pristine && {controllerFormAndPropertyName}$error.required' style='height: 0'>{propDisplayName} is required.</div>";
            var mailValid = $"<div ng-show='{controllerFormAndPropertyName}$error.email' style='height: 0'>{propDisplayName} is not a valid email address.</div>";
            var urlValid = $"<div ng-show='{controllerFormAndPropertyName}$error.url' style='height: 0'>{propDisplayName} is not a valid url.</div>";
            var stringLength = expression.GetStringLengthAttribute();
            if (stringLength != null)
            {
                var minlength = $"<div ng-show='{controllerFormAndPropertyName}$error.minlength' style='height: 0'>{propDisplayName} should be equal or greater  than {stringLength.MinimumLength}.</div>";
                var maxlength = $"<div ng-show='{controllerFormAndPropertyName}$error.maxlength' style='height: 0'>{propDisplayName} should be equal or less than {stringLength.MaximumLength}.</div>";
                requiredValidation += minlength;
                requiredValidation += maxlength;
            } 
            var range = expression.GetRangeAttribute();
            if (range == null)
                return new MvcHtmlString(requiredValidation + mailValid + urlValid);
            var minValid = $"<div ng-show='{controllerFormAndPropertyName}$error.min' style='height: 0'>{propDisplayName} should be greater than {range.Minimum}.</div>";
            var maxValid = $"<div ng-show='{controllerFormAndPropertyName}$error.max' style='height: 0'>{propDisplayName} should be less than {range.Maximum}.</div>";
  
            //max //maxlength //min //minlength //number //pattern //required //url //datetimelocal //time //week //month

            return new MvcHtmlString(requiredValidation + mailValid + urlValid + minValid + maxValid);
        }

        private static void ClearNull(ref IDictionary<string, object> attributes)
        {
            attributes = attributes ?? new Dictionary<string, object>();
        }

        private static void AddAngularNgModelAttribute<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes)
        {
            // When using ng-value, ng model should not be used since it runs in a diferent priority level and negates the effect of ng-value expression
            if (attributes.ContainsKey("ng-value") || attributes.ContainsKey("ng-model")) return;
            attributes.Add("ng-model", $"ctrl.{expression?.Body}");
        }

        private static void AddRequiredAttribute<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes)
        {
            if (!expression.IsRequired()) return;
            attributes.Add("required", "required");
        }

        private static void AddDisabledAttribute<TModel>(HtmlHelper<TModel> html, IDictionary<string, object> attributes, bool disabled = false)
        {
            attributes.Add("disabled", "disabled");
        }

        private static void AddAutofocusAttribute(IDictionary<string, object> attributes, bool autofocus)
        {
            if (!autofocus || attributes.ContainsKey("autofocus")) return;
            attributes.Add("autofocus", null);
        }

        private static void AddConvertToNumberAttribute<TProperty>(IDictionary<string, object> attributes)
        {
            if (typeof(TProperty) == typeof(string)) return;
            attributes.Add("convert-to-number", "convert-to-number");
        }

        private static void AddRangeAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes)
        {
            var range = expression.GetRangeAttribute();
            if (range == null) return;

            attributes?.Add("min", range.Minimum);
            attributes?.Add("max", range.Maximum);
        }
        private static void AddStringLengthAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes)
        {
            var range = expression.GetStringLengthAttribute();
            if (range == null) return;

            attributes?.Add("minlength", range.MinimumLength);
            attributes?.Add("maxlength", range.MaximumLength);
        }
        private static void AddDataTypeAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributesToAdd)
        {
            string field = expression.ToString();
            var dataType = expression.GetDataTypeAttribute();
            if (dataType is null) return;

            AddDataTypeAttributes(attributesToAdd, dataType);
            AddCustomDataTypeAttributes(attributesToAdd, dataType);
        }

        private static void AddDataTypeAttributes(IDictionary<string, object> attributesToAdd, DataTypeAttribute dataType)
        {
            if (dataType.DataType == DataType.Date)
            {
                attributesToAdd?.Add("type", "date");
            }
        }

        private static void AddCustomDataTypeAttributes(IDictionary<string, object> attributesToAdd, DataTypeAttribute dataType)
        {
            if (String.IsNullOrWhiteSpace(dataType.CustomDataType)) return;

            switch (dataType.CustomDataType)
            {
                case nameof(Decimal):
                    attributesToAdd?.Add("type", "number");
                    break;
                case nameof(Int32):
                    attributesToAdd?.Add("type", "text");
                    attributesToAdd?.Add("number", "");
                    break;
                default:
                    break;
            }
        }

        private static string GetSelectListValuesName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var propertyName = expression.Body.ToString();
            propertyName = propertyName.Substring(propertyName.IndexOf('.') + 1);
            return propertyName;
        }
    }
}

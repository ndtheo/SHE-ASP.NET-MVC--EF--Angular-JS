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
    /// <summary>
    /// This class creates custom HTML controls (inputs, selects, text areas etc) adding suitable
    /// attributes (such as date time pickers, required validation) where applicable.
    /// 
    /// Methods are extensions methods of <see cref="HtmlHelper"/> class.
    /// 
    /// Through this class, we could facilitate scaffolding, since the type of a property will automatically have
    /// changes in the control's behavior in the GUI.
    /// 
    /// Furthermore, through these methods, we could implement field-related rights for the application (hiding/disabling) 
    /// fields for a particular user or role in the future. 
    /// </summary>
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
            //AddDisabledAttribute(html, attributes, disabled);
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

        /// <summary>
        /// Custom Text Area for larger inputs.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="disabled"></param>
        /// <param name="attributes"></param>
        /// <param name="autofocus"></param>
        /// <returns></returns>
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

            return html.TextBoxFor(expression, new { htmlAttributes = attributes });
        }
       
        /// <summary>
        ///     Returns an HTML dropdown list
        ///     additional view data.
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
            AddConvertToNumberAttribute<TProperty>(attributes);

            var propertyName = GetSelectListValuesName(expression);

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (propertyName.SequenceEqual("Incident.IncidentTypeId"))
                {
                    propertyName = propertyName.Replace("Incident.IncidentTypeId", "IncidentTypeId");
                }
            }

            return html.DropDownList(propertyName, null, String.Empty, attributes);
        }


        public static IHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var label = html.LabelFor(expression, new { @class = "col-md-4 control-label" });
            return label;
        }

        public static IHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            string ctrl = html.ViewBag.ControllerName;
            var propName = expression.Body.ToString().Replace("model.", "");
            var propDisplayName = typeof(TModel).GetSubProperty(propName).GetDisplayName();

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
            if (field.EndsWith("Date"))
            {
                attributesToAdd?.Add("type", "date");
                dataType = new DataTypeAttribute(DataType.Date); 
            }
            else if (field.EndsWith("Time"))
            {
                attributesToAdd?.Add("type", "time");
                dataType = new DataTypeAttribute(DataType.Time);
            }
            else
            {
                dataType = expression.GetDataTypeAttribute();
                if (dataType is null) return;
            }

            AddDataTypeAttributes(attributesToAdd, dataType);
            AddCustomDataTypeAttributes(attributesToAdd, dataType);
        }

        private static void AddDataTypeAttributes(IDictionary<string, object> attributesToAdd, DataTypeAttribute dataType)
        {
            if (dataType.DataType == DataType.Date && attributesToAdd!=null && !attributesToAdd.ContainsKey("type"))
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
                case nameof(DateTime):
                    attributesToAdd?.Add("type", "date");
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

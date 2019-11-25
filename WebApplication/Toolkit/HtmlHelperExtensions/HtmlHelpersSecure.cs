#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Core.Entities;
using Database.Models.DbContext;
using Microsoft.Ajax.Utilities;
using Utilities.Extensions;
using WebApplication.VisionToolkit.ExtensionMethods;

#endregion

namespace WebApplication.VisionToolkit.HtmlHelperExtensions
{
    /// <summary>
    ///     Contains extention method for the htmlhelper class, with security and rights cheking.
    /// </summary>
    public static class HtmlHelpersSecure
    {
        /// <summary>
        ///     Returns the Rights of this user for the current controller. The current controller name and the UserRights are
        ///     assigned in the base controller before the action gets executed
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static List<Right> GetRights(this HtmlHelper html) => html.GetRights((string) html.ViewBag.ControllerName);

        private static string ControllerName(this HtmlHelper html) => (string) html?.ViewBag?.ControllerName ?? "";

        /// <summary>
        ///     Returns the Rights of this user for a given controller. The current controller name and the UserRights are
        ///     assigned in the base controller before the action gets executed
        /// </summary>
        /// <param name="html"></param>
        /// <param name="targetController">The name of the controller we search the rights for.</param>
        /// <returns></returns>
        private static List<Right> GetRights(this HtmlHelper html, string targetController)
        {
            List<Right> userRights = html.ViewBag.UserRights;
            IEnumerable<Right> rights = userRights?.Where(r => r != null && r.MenuItem?.ControllerName == targetController);
            return rights?.ToList();
        }

        /// <summary>
        ///     True if the htmlhelper has to check rights for edit. EditMode is assigned in most of the details actions.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static bool IsEditMode(this HtmlHelper html) => html?.ViewBag?.EditMode ?? false;

        /// <summary>
        ///     True if the a view is loaded in tab mode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static bool IsTab(this HtmlHelper html) => html?.ViewBag?.IsTab ?? false;

        /// <summary>
        ///     Returns the first name of a user given his id.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="userId">The id of a user</param>
        /// <returns></returns>
        public static string GetUserName(this HtmlHelper html, string userId)
        {
            return ApplicationCache.Users.Entities?.FirstOrDefault(x => x.Id == userId)?.Name?.Split(' ')[0] ?? string.Empty;
        }

        /// <summary>
        ///     <para>
        ///         Returns an HTML button that triggers a function named New() in an AngularJS controller, used to add a lookup
        ///         item in a dropdownList.
        ///     </para>
        ///     <para>
        ///         If we want a default parent for the item created we assign the name of the parentVariable in the
        ///         parentVarName parameter. (ClientId for $scope.model.ParentId).
        ///     </para>
        ///     <para>
        ///         If we are adding to a select option created from an array using ng-option we should set the arrays's name as
        ///         a the arrayName parameter (adunits for $scope.adunit).
        ///     </para>
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="elementId">The id/varName of the dropdown list/modelItem that will be passed to the angular function.</param>
        /// <param name="targetController">The name of the controller this action will trigger for rights check.</param>
        /// <param name="parentVarName">
        ///     The name of the parentVariable in the parentVarName parameter. (ClientId for
        ///     $scope.model.ParentId).
        /// </param>
        /// <param name="arrayName">The arrayName parameter (adunits for $scope.adunit).</param>
        /// <returns>An HTML button that trigger a function named New() in an AngularJS controller.</returns>
        public static IHtmlString NewRelatedEntityButton(this HtmlHelper html, string elementId, string targetController, string parentVarName = null, string arrayName = null)
        {
            List<Right> rights = html.GetRights(targetController);

            if (rights == null || !rights.Any(r => r != null && r.Create)) return null;

            var tb = new TagBuilder("span");
            tb.AddCssClass("glyphicon glyphicon-plus");
            tb.Attributes.Add("style", "cursor: pointer");
            parentVarName = parentVarName != null ? $",'{parentVarName}'" : ", null";
            arrayName = arrayName != null ? $",'{arrayName}'" : ", null";
            tb.Attributes.Add("ng-click", $"NewRelatedEntity('{elementId}', '{targetController}'{parentVarName}{arrayName})");
            tb.Attributes.Add("id", $"NewRelated_{targetController}");
            return new MvcHtmlString(tb.ToString());
        }

        /// <summary>
        ///     Returns an HTML button that trigger a function named EditRelatedEntity() in an AngularJS controller.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="elementId">The id of the dropdown list that will be passed to the angular function.</param>
        /// <param name="targetController">The name of the controller this action will trigger for rights check.</param>
        /// <returns>An HTML button that trigger a function named Edit() in an AngularJS controller.</returns>
        public static IHtmlString EditRelatedEntityButton(this HtmlHelper html, string elementId, string targetController)
        {
            List<Right> rights = html.GetRights(targetController);

            if (rights == null || !rights.Any(r => r != null && r.Edit))
                return null;

            var tb = new TagBuilder("span");
            tb.AddCssClass("glyphicon glyphicon-pencil");
            tb.Attributes.Add("style", "cursor: pointer");
            tb.Attributes.Add("ng-click", $"EditRelatedEntity('{elementId}', '{targetController}')");
            tb.Attributes.Add("ng-show", $"model.{elementId} > 0");
            tb.Attributes.Add("id", $"EditRelated_{targetController}");
            return new MvcHtmlString(tb.ToString());
        }

        /// <summary>
        ///     Returns and html Modal Header with a title and a CloseModalButton
        /// </summary>
        /// <param name="html"></param>
        /// <param name="modelHeader">The modals Header</param>
        /// <param name="ngClick"></param>
        /// <returns></returns>
        public static IHtmlString ModalHeader(this HtmlHelper html, string modelHeader, string ngClick = "CloseModal()")
        {
            string header = $"<div id=\"{html.ControllerName()}_DetailsFormHeader\" class=\"modal-header\">" +
                            $"<button id=\"{html.ControllerName()}_CloseTopButton\" type = \"button\" class=\"close\" ng-click=\"{ngClick}\">" +
                            $"<span class=\"glyphicon glyphicon-remove\" style=\"margin-top: 10px\"></span>" +
                            $"</button>" +
                            $"<h2 class=\"modal-title\">{modelHeader}</h2>" +
                            $"</div>";
            return new MvcHtmlString(header);
        }

        /// <summary>
        ///     Returns an HTML button element for saving in angular ui modals, if the user has the right.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="label"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IHtmlString ModalSaveButton(this HtmlHelper html, string label = "Save", IDictionary<string, object> attributes = null)
        {
            attributes = attributes ?? new Dictionary<string, object>();
            attributes.Add("id", $"{html.ControllerName()}_SaveButton");
            if (attributes.ContainsKey("ng-click"))
                attributes["ng-click"] += "; Save();";
            else
                attributes.Add("ng-click", "Save()");
            return html.ModalSaveAndCloseButton(label, attributes);
        }

        /// <summary>
        ///     Returns an HTML button element for canceling angular ui modals.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="attributes"></param>
        /// <returns>An HTML button element for canceling angular ui modals.</returns>
        public static IHtmlString ModalCloseButton(this HtmlHelper html, IDictionary<string, object> attributes = null)
        {
            var tb = new TagBuilder("button");
            tb.AddCssClass("btn btn-warning");
            tb.Attributes.Add("id", $"{html.ControllerName()}_CloseModal");
            tb.Attributes.Add("type", "button");
            tb.AttributesFromDic(attributes);
            if (!tb.Attributes.ContainsKey("ng-click"))
                tb.Attributes.Add("ng-click", "CloseModal()");
            tb.InnerHtml = "Close";

            return new MvcHtmlString(tb.ToString());
        }

        /// <summary>
        ///     Returns an HTML button element for saving and closing in angular ui modals, if the user has the right.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="label"></param>
        /// <param name="attributes"></param>
        /// <param name="formValidation"></param>
        /// <returns>An HTML button element for saving in angular ui modals, if the user has the right.</returns>
        public static IHtmlString ModalSaveAndCloseButton(this HtmlHelper html, string label = "Save and Close", IDictionary<string, object> attributes = null, bool formValidation = true)
        {
            List<Right> rights = html.GetRights();

            if (rights == null ||
                html.IsEditMode() && !rights.Any(r => r != null && r.Edit) ||
                !html.IsEditMode() && !rights.Any(r => r != null && r.Create))
                return null;
            string ctrlName = html.ViewBag.ControllerName;
            var tb = new TagBuilder("button");

            tb.Attributes.Add("type", "button");
            tb.AddCssClass("btn btn-primary");
            tb.AttributesFromDic(attributes);

            if (!tb.Attributes.ContainsKey("id"))
                tb.Attributes.Add("id", $"{html.ControllerName()}_SaveAndCloseModal");

            if (!tb.Attributes.ContainsKey("ng-click"))
                tb.Attributes.Add("ng-click", "SaveAndCloseModal()");

            if (formValidation)
            {
                if (!tb.Attributes.ContainsKey("ng-disabled"))
                    tb.Attributes.Add("ng-disabled", $"{ctrlName}Form.$invalid || {ctrlName}Form.$pristine ");
                else
                    tb.Attributes["ng-disabled"] += $" || {ctrlName}Form.$invalid || {ctrlName}Form.$pristine ";
            }

            tb.InnerHtml = label;

            return new MvcHtmlString(tb.ToString());
        }

        /// <summary>
        ///     Returns a button thet runs the folowing expresion on clink: "model = {PageSize: model.PageSize, Page:1}", clearing the search criteria
        /// </summary>
        /// <param name="html"></param>
        /// <param name="label"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IHtmlString ClearSearchCiteriaButton(this HtmlHelper html, string label = "Clear Search Criteria", IDictionary<string, object> attributes = null)
        {
            List<Right> rights = html.GetRights();

            if (rights == null ||
                html.IsEditMode() && !rights.Any(r => r != null && r.Edit) ||
                !html.IsEditMode() && !rights.Any(r => r != null && r.Create))
                return null;

            var tb = new TagBuilder("button");

            tb.AttributesFromDic(attributes);

            tb.AddCssClass("btn btn-primary");
            if (!tb.Attributes.ContainsKey("ng-click"))
                tb.Attributes.Add("ng-click", "model = {PageSize: model.PageSize, Page:1, ShowAllOrders: 'true'}");
            tb.InnerHtml = label;

            return new MvcHtmlString(tb.ToString());
        }

        /// <summary>
        ///     Returns an HTML input element for each property in the object that is represented by the expression, using
        ///     additional view data. If the user does not have edit right it is disabled.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="disabled">True if the editor should be disabled.</param>
        /// <param name="attributes"></param>
        /// <param name="autofocus"></param>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>
        ///     An HTML input element for each property in the object that is represented by the expression. If the user does
        ///     not have edit right it is disabled.
        /// </returns>
        public static IHtmlString CustomEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool disabled = false, IDictionary<string, object> attributes = null, bool autofocus = false)
        {
            

            IDictionary<string, object> attributesToAdd = attributes ?? new Dictionary<string, object>();

            // When using ng-value, ng model should not be used since it runs in a diferent priority level and negates the effect of ng-value expression
            if (!attributes?.ContainsKey("ng-value") ?? true)
                attributesToAdd.Add("ng-model", expression?.Body.ToString() ?? string.Empty);

            HtmlHelpersSecure.AddDataTypeAttributes(expression, attributesToAdd);
            HtmlHelpersSecure.AddRangeAttributes(expression, attributesToAdd);

            if (ShouldDisable<TModel, TValue>(html, disabled))
                attributesToAdd.Add("disabled", "disabled");

            if (expression.IsRequired())
                attributesToAdd.Add("required", "required");

            if (autofocus)
                attributesToAdd.Add("autofocus", null);

            return html.EditorFor(expression, new
            {
                htmlAttributes = attributesToAdd
            });
        }

        private static bool ShouldDisable<TModel, TValue>(HtmlHelper<TModel> html, bool disabled)
        {
            List<Right> rights = html.GetRights();
            return rights == null || html.IsEditMode() && !rights.Any(r => r != null && r.Edit) || !html.IsEditMode() && !rights.Any(r => r != null && r.Create) || disabled;
        }

        /// <summary>
        ///     Returns an HTML dropdown list
        ///     additional view data. If the user does not have edit right it is disabled.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IHtmlString CustomDropDownList<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            List<Right> rights = html.GetRights();

            IDictionary<string, object> htmlAttributes = new Dictionary<string, object> { ["ng-model"] = expression.Body.ToString() };

            attributes?.ForEach(a => htmlAttributes.Add(a));

            if (typeof(TProperty) != typeof(string))
                htmlAttributes.Add("convert-to-number", "convert-to-number");

            if (!htmlAttributes.ContainsKey("disabled"))
            {
                if (rights == null || html.IsEditMode() && !rights.Any(r => r != null && r.Edit) || !html.IsEditMode() && !rights.Any(r => r != null && r.Create))
                    htmlAttributes.Add("disabled", "disabled");
            }

            if (expression.IsRequired())
                htmlAttributes.Add("required", "required");

            string propertyName = expression.Body.ToString();
            propertyName = propertyName.Substring(propertyName.IndexOf('.') + 1);

            return html.DropDownList(propertyName, null, string.Empty, htmlAttributes);
        }

        private static void AddRangeAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes)
        {
            RangeAttribute range = expression.GetRangeAttribute();
            if (range == null) return;
            attributes?.Add("min", range.Minimum);
            attributes?.Add("max", range.Maximum);
        }

        private static void AddDataTypeAttributes<TModel, TValue>(Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributesToAdd)
        {
            DataTypeAttribute dataType = expression.GetDataTypeAttribute();
            if (dataType?.DataType == DataType.Date)
            {
                attributesToAdd?.Add("type", "date");
                //attributesToAdd?.Add("ignore-utc", "");
            }
            if (string.IsNullOrWhiteSpace(dataType?.CustomDataType)) return;
            if (dataType.CustomDataType == nameof(Decimal))
                attributesToAdd?.Add("type", "number");
            if (dataType.CustomDataType == nameof(Int32))
            {
                attributesToAdd?.Add("type", "text");
                attributesToAdd?.Add("number", "");
            }
        }

        public static IHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            //return html.ValidationMessageFor(expression);
            string ctrl = html.ViewBag.ControllerName;
            string propName = expression.Body.ToString().Replace("model.", "");
            string propDisplayName = typeof(TModel).GetSubProperty(propName).GetDisplayName();

            // TODO: check if everything is needed, in order to save bandwidth
            string requiredValidation = $"<div ng-show='!{ctrl}Form.{propName}.$pristine && {ctrl}Form.{propName}.$error.required' style='height: 0'>{propDisplayName} is required.</div>";
            string mailValid = $"<div ng-show='{ctrl}Form.{propName}.$error.email' style='height: 0'>{propDisplayName} is not a valid email address.</div>";
            string urlValid = $"<div ng-show='{ctrl}Form.{propName}.$error.url' style='height: 0'>{propDisplayName} is not a valid url.</div>";

            RangeAttribute range = expression.GetRangeAttribute();
            if (range == null)
                return new MvcHtmlString(requiredValidation + mailValid + urlValid);
            string minValid =
                $"<div ng-show='{ctrl}Form.{propName}.$error.min' style='height: 0'>{propDisplayName} should be greater than {range.Minimum}.</div>";
            string maxValid =
                $"<div ng-show='{ctrl}Form.{propName}.$error.max' style='height: 0'>{propDisplayName} should be less than {range.Maximum}.</div>";

            //max //maxlength //min //minlength //number //pattern //required //url //datetimelocal //time //week //month

            return new MvcHtmlString(requiredValidation + mailValid + urlValid + minValid + maxValid);
        }

        public static IHtmlString NewEntityButton(this HtmlHelper html, string label = "Add New", IDictionary<string, object> attributes = null)
        {
            // TODO:var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            List<Right> rights = html.GetRights();

            if (rights == null || !rights.Any(r => r.Create))
                return null;
            var i = new TagBuilder("i");
            i.AddCssClass("fic glyphicon glyphicon-plus-sign");

            var a = new TagBuilder("a");
            a.AddCssClass("btn btn-xs btn-primary");
            a.AttributesFromDic(attributes);
            a.Attributes.Add("id", $"{html.ControllerName()}_NewEntityButton");

            if (!a.Attributes.ContainsKey("ng-click"))
                a.Attributes.Add("ng-click", "NewEntity()");
            a.InnerHtml = $"{i} {label}";

            var div = new TagBuilder("div");
            div.AddCssClass("pull-left");
            div.Attributes?.Add("style", "padding-left: 15px;");
            div.InnerHtml = a.ToString();

            return new MvcHtmlString(div.ToString());
        }

        /// <summary>
        ///     <para>
        ///         Returns a button that when clicked displays a prompt window with the default message, and if accepted
        ///         triggers the DeleteEntity() function.
        ///     </para>
        ///     <para>Promt Message and ngClick action can be changed using the parameters</para>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="promtMessage"></param>
        /// <param name="ngClick"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IHtmlString DeleteEntityButton(this HtmlHelper html, string promtMessage = "Are you sure you want to delete this object?", string ngClick = "DeleteEntity()",
            IDictionary<string, object> attributes = null)
        {
            List<Right> rights = html.GetRights();

            if (rights == null || !rights.Any(r => r != null && r.Delete)) return null;

            // TODO: recreate the tag using the tag builder
            string delButtonInner = $"<a class='btn btn-xs btn-danger' id='{html.ControllerName()}_DeleteButton' ng-disabled='SelectedId === null' data-nq-modal-box='' " +
                                    $"data-box-type='confirm' data-qs-content='{promtMessage}' data-after-confirm='{ngClick}'>" +
                                    "<i class='fic glyphicon glyphicon-remove-circle'></i> Remove</a>";
            var div = new TagBuilder("div");
            div.AddCssClass("pull-left");
            div.Attributes?.Add("style", "padding-left: 15px;");

            div.AttributesFromDic(attributes);
            div.InnerHtml = delButtonInner;
            return new MvcHtmlString(div.ToString());
        }

        /// <summary>
        ///     Returns a button that triggers the RefreshGrid function
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IHtmlString RefreshGridButton(this HtmlHelper html)
        {
            string refButton = "<div class='pull-left' style='padding-left: 15px;'>" +
                               $"<a id=\"{html.ControllerName()}_RefreshGrid\" class='btn btn-xs btn-info' ng-click='RefreshGrid()'><i class='fic glyphicon glyphicon-refresh'></i> Refresh</a>" +
                               "</div>";
            return new MvcHtmlString(refButton);
        }

        /// <summary>
        ///     Returns a button that trigers the OpenSearchCriteria function
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IHtmlString OpenSearchCriteriaButton(this HtmlHelper html)
        {
            string button = "<div class='pull-left' style='padding-left: 15px;'>" +
                            $"<a id=\"{html.ControllerName()}_OpenSearchCriteria\" class='btn btn-xs btn-info' ng-click='OpenSearchCriteria()'><i class='fic glyphicon glyphicon-search'></i> Full Search</a>" +
                            "</div>";
            return new MvcHtmlString(button);
        }

        /// <summary>
        ///     Returns a button that trigers the ExportExcel function if the user has the Export right
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IHtmlString ExportGridToExcelButton(this HtmlHelper html)
        {
            List<Right> rights = html.GetRights();
            if (rights == null || !rights.Any(r => r != null && r.Export))
                return null;
            var expButton =
                "<button class='btn btn-default' type='button' ng-click='ExportGridToExcel()' style='margin-left: 10px'><span class='glyphicon glyphicon-floppy-save'></span></button>";
            return new MvcHtmlString(expButton);
        }

        public static IHtmlString SortIcon(this HtmlHelper html, string property)
        {
            string sort =
                $"<span class='glyphicon' ng-class=\"{{'glyphicon-sort': SearchCriteria.OrderBy != '{property}', " +
                $"'glyphicon-sort-by-alphabet': SearchCriteria.OrderBy === '{property}', " +
                $"'glyphicon-sort-by-alphabet-alt': SearchCriteria.OrderBy === '{property} desc'}}\"></span>";
            return new MvcHtmlString(sort);
        }

        public static IHtmlString PanelFooter(this HtmlHelper html)
        {
            string footer = "<div class='col-sm-6' style='height:30px'>" +
                            "<span style='position:absolute;bottom:0px;'>" +
                            "<b>Total Records:</b> <span ng-bind = 'TotalRecords'></span>" +
                            "<b> PageSize:</b> <select class='PagingSelect' ng-options = 'size for size in [10,20,50,100,\"All\"]' ng-model = 'SearchCriteria.PageSize' ng-change = 'SearchCriteria.Page=1; RefreshGrid();'></select>" +
                            "</span>" +
                            "</div>" +
                            "<div class='col-sm-6'><ul style='float:right;margin:0px' uib-pagination total-items='TotalRecords' items-per-page='SearchCriteria.PageSize' ng-model='SearchCriteria.Page' ng-change = 'RefreshGrid()' max-size='5' class='pagination-sm' boundary-link-numbers='true'></ul></div>"
                            +
                            "<div class='clearfix'></div>";

            //"<b> Page:</b> <select class='PagingSelect'  ng-options = 'page for page in Pages' ng-model = 'SearchCriteria.Page' ng-change = 'RefreshGrid()'></select>";
            return new MvcHtmlString(footer);
        }

        /// <summary>
        ///     Returns a disabled textbox that displays the uploaded file's name
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString DocumentName(this HtmlHelper html, string propertyName)
        {
            string textBox = $"<input type='text' disabled='disabled' ng-model='model.{propertyName}.Name'/>";
            return new MvcHtmlString(textBox);
        }

        /// <summary>
        ///     Returns a button used to trigger the upload file function for a given propertyName
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString DocumentUploadButton(this HtmlHelper html, string propertyName)
        {
            List<Right> rights = html.GetRights();

            if (rights == null ||
                html.IsEditMode() && !rights.Any(r => r != null && r.Edit) ||
                !html.IsEditMode() && !rights.Any(r => r != null && r.Create))
                return null;
            string uplButton = $"<span class='glyphicon glyphicon-upload' style='cursor: pointer' ng-click=\"UploadFile('{propertyName}')\"></span>";
            return new MvcHtmlString(uplButton);
        }

        public static IHtmlString DocumentDownloadButton(this HtmlHelper html, string propertyName)
        {
            List<Right> rights = html.GetRights();

            if (rights == null || !rights.Any(r => r != null && r.View))
                return null;
            string downButton =
                $"<span class='glyphicon glyphicon-download' style='cursor: pointer' ng-click=\"DownloadFile('{propertyName}')\"" +
                $"ng-show='model.{propertyName}.Id > 0'></span>";
            return new MvcHtmlString(downButton);
        }

        public static IHtmlString DocumentDeleteButton(this HtmlHelper html, string propertyName)
        {
            List<Right> rights = html.GetRights();

            if (rights == null || !rights.Any(r => r != null && r.Edit))
                return null;
            string delButton =
                "<span class='glyphicon glyphicon-remove' style='cursor: pointer' data-nq-modal-box='' data-box-type='confirm' " +
                $"data-after-confirm = \"DeleteFile('{propertyName}')\" data-qs-content='Are you sure you want to delete this document?' " +
                $"ng-show = 'model.{propertyName}.Id > 0'></span>";
            return new MvcHtmlString(delButton);
        }

        public static IHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return html.LabelFor(expression, new
            {
                @class = "col-md-4 control-label"
            });
        }

        public static IHtmlString TabPage(this HtmlHelper html, int index, string controller, bool parentHasStringId = false, string tabHeader = null)
        {
            List<Right> rights = html.GetRights(controller);

            if (rights == null || !rights.Any(r => r.View))
                return null;
            string check = parentHasStringId ? "Id" : "Id > 0";

            if (tabHeader == null)
                tabHeader = controller;

            string tab = $"<uib-tab ng-show='{check}' index = '{index}' heading = '{tabHeader}' >" +
                         $"<div ng-include = \"'../{controller}?isTab=true'\" ></div>" + // Is tab is used so that the index will not use a layout file
                         "</uib-tab>";
            return new MvcHtmlString(tab);
        }

        public static List<MenuItem> GetAvailableMenuItems<TModel>(this HtmlHelper<TModel> html)
        {
            IEnumerable<MenuItem> menuItems = ((List<Right>) html.ViewBag.UserRights)?
                .Where(r => r.View)
                .Select(r => r.MenuItem)
                .DistinctBy(m => m.Id);
            return menuItems?.ToList() ?? new List<MenuItem>();
        }

        #region Helper Methods

        // ================ Helper methods   ============================================================================

        private static void AttributesFromDic(this TagBuilder tb, IDictionary<string, object> attributes)
        {
            attributes?.ForEach(a => tb?.Attributes?.Add(a.Key, a.Value.ToString()));
        }

        public static IHtmlString ActionLinkSecure(this HtmlHelper html, string linkText, string actionName,
            string controllerName)
        {
            List<Right> rights = html.GetRights(controllerName);

            if (rights == null || !rights.Any(r => r.View))
                return null;
            return html.AngularActionLink(linkText, actionName, controllerName);
        }

        private static IHtmlString AngularActionLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            actionName = actionName == "Index" ? string.Empty : $"/{actionName}";
            return new HtmlString($"<a href='#{controllerName}{actionName}'>{linkText}</a>");
        }

        private static bool IsRequired<T, TV>(this Expression<Func<T, TV>> expression)
        {
            var memberExpression = expression?.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<RequiredAttribute>() != null;
        }

        private static bool IsReadOnly<T, TV>(this Expression<Func<T, TV>> expression)
        {
            var memberExpression = expression?.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<ReadOnlyAttribute>()?.IsReadOnly ?? false;
        }

        private static DataTypeAttribute GetDataTypeAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            var memberExpression = expression?.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<DataTypeAttribute>();
        }

        private static DefaultValueAttribute GetDefaultValueAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            var memberExpression = expression?.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<DefaultValueAttribute>();
        }

        private static RangeAttribute GetRangeAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            var memberExpression = expression?.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<RangeAttribute>();
        }

        private static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            object[] attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }

        #endregion
    }
}
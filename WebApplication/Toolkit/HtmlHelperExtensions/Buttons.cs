using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Entities;
using Microsoft.Ajax.Utilities;

namespace WebApplication.VisionToolkit.HtmlHelperExtensions {
    public static class Buttons {
            /// <summary>
            ///     <para>
            ///         Returns an HTML button that triggers a function named NewRelatedEntity() in an AngularJS controller, used to add a lookup item in a dropdown List.
            ///     </para>
            ///     <para>
            ///         If we want a default parent for the item created we assign the name of the parent Variable in the "parentVarName" parameter. (ClientId for $scope.model.ParentId).
            ///     </para>
            ///     <para>
            ///         If we are adding to a select option created from an array using ng-option we should set the arrays's name as a the "arrayName" parameter (adunit for ctrl.adunits).
            ///     </para>
            /// </summary>
            /// <param name="html">The HTML html instance that this method extends.</param>
            /// <param name="elementId">The id/varName of the dropdown list/modelItem that will be passed to the angular function.</param>
            /// <param name="targetController">The name of the controller this action will trigger for rights check.</param>
            /// <param name="parentVarName">The name of the parentVariable in the parentVarName parameter. (ClientId for ctrl.model.ParentId).</param>
            /// <param name="arrayName">The arrayName parameter (adunit for $scope.adunits).</param>
            /// <returns>An HTML button that trigger a function named NewRelatedEntity() in an AngularJS controller.</returns>
            public static IHtmlString NewRelatedEntityButton(this HtmlHelper html, string elementId, string targetController, string parentVarName = null, string arrayName = null, string labelProperty = "Name")
            {
                parentVarName = parentVarName != null ? $",'{parentVarName}'" : ", null";
                arrayName = arrayName != null ? $",'{arrayName}'" : ", null";

                var span = new TagBuilder("span");
                span.AddCssClass("glyphicon glyphicon-plus");
                span.Attributes.Add("style", "cursor: pointer");
                span.Attributes.Add("ng-click", $"ctrl.NewRelatedEntity('{elementId}', '{targetController}'{parentVarName}{arrayName}, '{labelProperty}')");
                span.Attributes.Add("id", $"NewRelated_{targetController}");

                return new MvcHtmlString(span.ToString());
            }

        /// <summary>
        ///     Returns an HTML button that trigger a function named EditRelatedEntity() in an AngularJS controller.
        /// </summary>
        /// <param name="html">The HTML html instance that this method extends.</param>
        /// <param name="elementId">The id of the dropdown list that will be passed to the angular function.</param>
        /// <param name="targetController">The name of the controller this action will trigger for rights check.</param>
        /// <returns>An HTML button that trigger a function named Edit() in an AngularJS controller.</returns>
        public static IHtmlString EditRelatedEntityButton(this HtmlHelper html, string elementId, string targetController, string labelProperty = "Name")
        {
            var span = new TagBuilder("span");
            span.AddCssClass("glyphicon glyphicon-pencil");
            span.Attributes.Add("style", "cursor: pointer");
            span.Attributes.Add("ng-click", $"ctrl.EditRelatedEntity('{elementId}', '{targetController}', '{labelProperty}')");
            span.Attributes.Add("ng-show", $"ctrl.model.{elementId} > 0");
            span.Attributes.Add("id", $"EditRelated_{targetController}");

            return new MvcHtmlString(span.ToString());
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
                attributes["ng-click"] += "; ctrl.Save();";
            else
                attributes.Add("ng-click", "ctrl.Save()");
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
                tb.Attributes.Add("ng-click", "ctrl.CloseModal()");
            tb.InnerHtml = "Close"; //"Close";

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
            string ctrlName = html.ViewBag.ControllerName;
            var button = new TagBuilder("button");

            button.Attributes.Add("type", "button");
            button.AddCssClass("btn btn-primary");
            button.AttributesFromDic(attributes);

            if (!button.Attributes.ContainsKey("id"))
                button.Attributes.Add("id", $"{html.ControllerName()}_SaveAndCloseModal");

            if (!button.Attributes.ContainsKey("ng-click"))
                button.Attributes.Add("ng-click", "ctrl.SaveAndCloseModal()");

            if (formValidation)
            {
                if (!button.Attributes.ContainsKey("ng-disabled"))
                    button.Attributes.Add("ng-disabled", $"{ctrlName}Form.$invalid || {ctrlName}Form.$pristine ");
                else
                    button.Attributes["ng-disabled"] += $" || {ctrlName}Form.$invalid || {ctrlName}Form.$pristine ";
            }

            button.InnerHtml = label;

            return new MvcHtmlString(button.ToString());
        }

        /// <summary>
        ///     Returns a button thet runs the folowing expresion on clink: "model = {PageSize: model.PageSize, Page:1}", clearing the search criteria
        /// </summary>
        /// <param name="html"></param>
        /// <param name="label"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IHtmlString ClearSearchCiteriaButton(this HtmlHelper html, string label ="Clear Search Criteria", IDictionary<string, object> attributes = null)
        {
            var button = new TagBuilder("button");
            button.AttributesFromDic(attributes);

            button.AddCssClass("btn btn-primary");
            if (!button.Attributes.ContainsKey("ng-click"))
                button.Attributes.Add("ng-click", "ctrl.model = {PageSize: ctrl.model.PageSize, Page:1, ShowAllOrders: 'true'}");
            button.InnerHtml = label;

            return new MvcHtmlString(button.ToString());
        }

        public static IHtmlString NewEntityButton(this HtmlHelper html, string label = "", IDictionary<string, object> attributes = null)
        {
            var i = new TagBuilder("i");
            i.AddCssClass("fic glyphicon glyphicon-plus-sign");

            var a = new TagBuilder("a");
            a.AddCssClass("btn btn-xs btn-primary");
            a.AttributesFromDic(attributes);
            a.Attributes.Add("id", $"{html.ControllerName()}_NewEntityButton");

            if (!a.Attributes.ContainsKey("ng-click"))
                a.Attributes.Add("ng-click", "ctrl.NewEntity()");
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
        public static IHtmlString DeleteEntityButton(this HtmlHelper html, string promtMessage = "Are you sure you want to delete this record?", string ngClick = "ctrl.DeleteEntity()", IDictionary<string, object> attributes = null)
        {
            // TODO: recreate the tag using the tag builder
            var delButtonInner = $"<a class='btn btn-xs btn-danger' id='{html.ControllerName()}_DeleteButton' ng-disabled='ctrl.SelectedId === null' data-nq-modal-box='' " +
                                    $"data-box-type='confirm' data-qs-content='{promtMessage}' data-after-confirm='{ngClick}'>" +
                                    $"<i class='fic glyphicon glyphicon-remove-circle'></i>Delete</a>";
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
            string refreshLabel = "Refresh";
            var refButton = "<div class='pull-left' style='padding-left: 15px;'>" +
                               $"<a id=\"{html.ControllerName()}_RefreshGrid\" class='btn btn-xs btn-info' ng-click='ctrl.RefreshGrid()'><i class='fic glyphicon glyphicon-refresh'></i> {refreshLabel}</a>" +
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
            var button = "<div class='pull-left' style='padding-left: 15px;'>" +
                            $"<a id=\"{html.ControllerName()}_OpenSearchCriteria\" class='btn btn-xs btn-info' ng-click='ctrl.OpenSearchCriteria()' title='Search Criteria'><i class='fic glyphicon glyphicon-search'></i>Search</a>" +
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
            var expButton ="<button class='btn btn-default' type='button' ng-click='ctrl.ExportGridToExcel()' style='margin-left: 10px' title='Excel'><span class='glyphicon glyphicon-floppy-save'></span></button>";
            return new MvcHtmlString(expButton);
        }

        /// <summary>
        ///     Returns a button that selects the fields that are going to be shown (and exported to Excel if applicable)
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IHtmlString SelectFieldsButton(this HtmlHelper html)
        {
            var expButton = $"<button class=\"btn btn-default\" type=\"button\" uib-dropdown-toggle style=\"margin-left:10px\" title=\"Select Shown Fields\"><span class=\"glyphicon glyphicon-cog\"></span></button>";
            return new MvcHtmlString(expButton);
        }

        /// <summary>
        ///     Returns a button used to trigger the upload file function for a given propertyName
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString DocumentUploadButton(this HtmlHelper html, string propertyName)
        {
            var uplButton = $"<span class='glyphicon glyphicon-upload' style='cursor: pointer' ng-click=\"ctrl.UploadFile('{propertyName}')\"></span>";
            return new MvcHtmlString(uplButton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IHtmlString DocumentDownloadButton(this HtmlHelper html, string propertyName)
        {

            var downButton =
                $"<span class='glyphicon glyphicon-download' style='cursor: pointer' ng-click=\"ctrl.DownloadFile('{propertyName}')\"" +
                $" ng-show='ctrl.model.{propertyName}.Id > 0'></span>";
            return new MvcHtmlString(downButton);
        }

        /// <summary>
        /// Creates a download attachment button, in the case that the user has the related rights.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="propertyName"></param>
        /// <param name="childEntity">In the case that we open the Report and we have an attachment of the Car (for instance the Contract), then the method should be notified of that throught this field</param>
        /// <returns></returns>
        public static IHtmlString DocumentDownloadButton(this HtmlHelper html, string propertyName, string childEntity)
        {

            if (!string.IsNullOrWhiteSpace(childEntity))
            {
                ///childEntity = "." + childEntity;
            }
            else childEntity = "";
            propertyName = childEntity + "." + propertyName;

            var downButton =
                $"<span class='glyphicon glyphicon-download' style='cursor: pointer' ng-click=\"ctrl.DownloadFile('{propertyName}')\"" +
                $" ng-show='ctrl.model.{propertyName}.Id>0'></span>";
            return new MvcHtmlString(downButton);
        }

        public static IHtmlString DocumentDeleteButton(this HtmlHelper html, string propertyName)
        {

            var delButton =
                "<span class='glyphicon glyphicon-remove' style='cursor: pointer' data-nq-modal-box='' data-box-type='confirm' " +
                $"data-after-confirm = \"ctrl.DeleteFile('{propertyName}')\" data-qs-content='Are you sure you want to delete this document?' " +
                $"ng-show='ctrl.model.{propertyName}.Id > 0'></span>";
            return new MvcHtmlString(delButton);
        }

        private static void AttributesFromDic(this TagBuilder tb, IDictionary<string, object> attributes)
        {
            attributes?.ForEach(a => tb?.Attributes?.Add(a.Key, a.Value.ToString()));
        }
    }
}
#region Using Directives

using Core.Entities;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

#endregion

namespace WebApplication.VisionToolkit.HtmlHelperExtensions
{
    /// <summary>
    ///     Contains extention method for the htmlhelper class, with security and rights cheking.
    /// </summary>
    public static class ViewParts
    {
        /// <summary>
        ///     Returns and html Modal Header with a title and a CloseModalButton
        /// </summary>
        /// <param name="html"></param>
        /// <param name="modelHeader">The modals Header</param>
        /// <param name="ngClick"></param>
        /// <returns></returns>
        public static IHtmlString ModalHeader(this HtmlHelper html, string modelHeader, string ngClick = "ctrl.CloseModal()",bool createSaveButttons=true, IHtmlString buttonsMarkup=null)
        {
            IHtmlString saveButtons = null;//string.Empty;
            const string SPACE = "&nbsp;";
            // string QUOTE = '"'.ToString();
            if (createSaveButttons)
            {
                if (null==buttonsMarkup)
                {
                    string divStart = "<div align=\"right\">";
                    string divFinish = "</div>";
                    var saveButton = Buttons.ModalSaveAndCloseButton(html);
                    var saveAndCloseButton = Buttons.ModalSaveButton(html);
                    var closeButton = Buttons.ModalCloseButton(html);
                    saveButtons = new HtmlString(divStart + saveButton.ToHtmlString()  + SPACE + saveAndCloseButton.ToHtmlString() + SPACE + closeButton.ToHtmlString() + divFinish);
                }
                else
                {
                    saveButtons = buttonsMarkup;
                }
            }
            string saveButtonsFinal = string.Empty;
            if (null !=saveButtons)
            {
                saveButtonsFinal = saveButtons.ToString();
            }

            var header = ngClick != "" ? $"<div id=\"{html.ControllerName()}_DetailsFormHeader\" class=\"modal-header\">" +
                           $"<button id=\"{html.ControllerName()}_CloseTopButton\" type = \"button\" class=\"close\" ng-click=\"{ngClick}\">" +
                            $"<span class=\"glyphicon glyphicon-remove\" style=\"margin-top: 10px\"></span>" +
                            $"</button>" +
                            $"<h2 class=\"modal-title\">{modelHeader}</h2>" +
                            saveButtonsFinal +
                            $"</div>"
                            : $"<div id=\"{html.ControllerName()}_DetailsFormHeader\" class=\"modal-header\">" +
                            $"<h2 class=\"modal-title\">{modelHeader}</h2>" +
                            saveButtonsFinal +
                            $"</div>";
            return new MvcHtmlString(header);
        }

        public static IHtmlString SortIcon(this HtmlHelper html, string property)
        {
            var sort =
                $"<span class='glyphicon' ng-class=\"{{'glyphicon-sort': ctrl.SearchCriteria.OrderBy != '{property}', " +
                $"'glyphicon-sort-by-alphabet': ctrl.SearchCriteria.OrderBy === '{property}', " +
                $"'glyphicon-sort-by-alphabet-alt': ctrl.SearchCriteria.OrderBy === '{property} desc'}}\"></span>";
            return new MvcHtmlString(sort);
        }

        public static IHtmlString PanelFooter(this HtmlHelper html)
        {
            var footer = "<div class='col-sm-6' style='height:30px'>" +
                            "<span style='position:absolute;bottom:0px;'>" +
                            "<b>Total Records:</b> <span ng-bind = 'ctrl.TotalRecords'></span>" +
                            "<b> PageSize:</b> <select class='PagingSelect' ng-options = 'size for size in [10,20,50,100,\"All\"]' ng-model = 'ctrl.SearchCriteria.PageSize' ng-change = 'ctrl.SearchCriteria.Page=1; ctrl.RefreshGrid();'></select>" +
                            "</span>" +
                            "</div>" +
                            "<div class='col-sm-6'><ul style='float:right;margin:0px' uib-pagination total-items='ctrl.TotalRecords' items-per-page='ctrl.SearchCriteria.PageSize' ng-model='ctrl.SearchCriteria.Page' ng-change='ctrl.RefreshGrid()' max-size='5' class='pagination-sm' boundary-link-numbers='true'></ul></div>"
                            +
                            "<div class='clearfix'></div>";

            //"<b> Page:</b> <select class='PagingSelect'  ng-options = 'page for page in Pages' ng-model = 'SearchCriteria.Page' ng-change = 'RefreshGrid()'></select>";
            return new MvcHtmlString(footer);
        }

        //public static IHtmlString TabPage(this HtmlHelper html, int index, string controller, bool parentHasStringId = false, string tabHeader = null, bool userParentId=false)
        //{
        //    string check = string.Empty;
        //    check = parentHasStringId ? "ctrl.Id" : "ctrl.Id > 0";

        //    tabHeader = tabHeader ?? controller;

        //    var tab = $@"<uib-tab ng-show='{check}' index = '{index}' heading = '{tabHeader}' >" +
        //                 $"<div ng-include = \"'../{controller}?isTab=true'\" ></div>" + // Is tab is used so that the index will not use a layout file
        //                 "</uib-tab>";
        //    return new MvcHtmlString(tab);
        //}
        //public static IHtmlString TabPage(this HtmlHelper html, int index, string controller,string action, bool parentHasStringId = false, string tabHeader = null, bool userParentId = false)
        //{
        //    var rights = html.GetRights(controller);

        //    if (rights == null || !rights.Any(r => r.View)) return null;
        //    string check = string.Empty;
        //    check = parentHasStringId ? "ctrl.Id" : "ctrl.Id > 0";

        //    tabHeader = tabHeader ?? controller;

        //    var tab = $@"<uib-tab ng-show='{check}' index = '{index}' heading = '{tabHeader}' >" +
        //                 $"<div ng-include = \"'../{controller}/{action}?isTab=true'\" ></div>" + // Is tab is used so that the index will not use a layout file
        //                 "</uib-tab>";
        //    return new MvcHtmlString(tab);
        //}
        public static List<MenuItem> GetAvailableMenuItems<TModel>(this HtmlHelper<TModel> html)
        {
            //var menuItems = ((List<Right>) html.ViewBag.UserRights)?
            //    .Where(r => r.View)
            //    .Select(r => r.MenuItem)
            //    .DistinctBy(m => m.Id);
            //return menuItems?.ToList() ?? new List<MenuItem>();
            return null;
        }

        #region Helper Methods

        // ================ Helper methods   ============================================================================

        public static IHtmlString ActionLinkSecure(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            return html.AngularActionLink(linkText, actionName, controllerName);
        }

        private static IHtmlString AngularActionLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            actionName = actionName == "Index" ? string.Empty : $"/{actionName}";
            return new HtmlString($"<a href='#{controllerName}{actionName}'>{linkText}</a>");
        }

        #endregion
    }
}
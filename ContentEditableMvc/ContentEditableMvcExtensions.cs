using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ContentEditableMvc
{
    public static class ContentEditableMvcExtensions
    {
        public static IHtmlString ContentEditableFor<T>(this HtmlHelper<T> htmlHelper, bool enableEditing,
            string entityId, Expression<Func<T, object>> modelProperty)
        {
            //  Get the content value.
            var contentValue = ModelMetadata.FromLambdaExpression(
                modelProperty, htmlHelper.ViewData
                ).Model;
            var contentValueText = contentValue != null ? contentValue.ToString() : string.Empty;

            //  If we're not editable, we've got a trivial case of just returning the content.
            if(enableEditing == false)
                return new MvcHtmlString(contentValueText);

            var savechanges = new TagBuilder("a");
            savechanges.AddCssClass("cem-savechanges");
            savechanges.SetInnerText("Save");
            savechanges.Attributes["href"] = "#";

            var discardchanges = new TagBuilder("a");
            discardchanges.AddCssClass("cem-discardchanges");
            discardchanges.SetInnerText("Discard");
            discardchanges.Attributes["href"] = "#";

            var contenteditable = new TagBuilder("span");
            contenteditable.Attributes["contenteditable"] = "true";
            contenteditable.AddCssClass("cem-content");
            contenteditable.Attributes["data-property-name"] = ExpressionHelper.GetExpressionText(modelProperty);
            contenteditable.Attributes["data-edit-url"] = (new UrlHelper(htmlHelper.ViewContext.RequestContext)).Action("EditContent");
            contenteditable.Attributes["data-entity-id"] = entityId;
            contenteditable.SetInnerText(contentValueText);

            var wrapper = new TagBuilder("span");
            wrapper.AddCssClass("cem-wrapper");

            wrapper.InnerHtml = contenteditable.ToString();
            wrapper.InnerHtml += savechanges.ToString();
            wrapper.InnerHtml += discardchanges.ToString();

            return new MvcHtmlString(wrapper.ToString());

        }
    }
}
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

            var builder = new TagBuilder("span");
            builder.Attributes["contenteditable"] = "true";
            builder.AddCssClass("contenteditablemvc");
            builder.Attributes["data-property-name"] = ExpressionHelper.GetExpressionText(modelProperty);
            builder.Attributes["data-edit-url"] = (new UrlHelper(htmlHelper.ViewContext.RequestContext)).Action("EditContent");
            builder.Attributes["data-entity-id"] = entityId;
            builder.SetInnerText(contentValueText);
            return new MvcHtmlString(builder.ToString());

        }
    }
}
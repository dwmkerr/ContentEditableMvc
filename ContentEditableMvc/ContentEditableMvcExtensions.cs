using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContentEditableMvc
{
    /// <summary>
    /// Contains the ContentEditableMvc extension methods for the Html Helper.
    /// ContentEditableFor should follow the parameter pattern:
    /// ActionName ControllerName ModelProperty ModelData EnableEditing
    /// </summary>
    public static class ContentEditableMvcExtensions
    {
        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString ContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<T, object>>  expression, object modelData, bool enableEditing)
        {
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, enableEditing, false);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString ContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<T, object>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, true, false);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString ContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, 
                                                        Expression<Func<T, object>> expression, object modelData, bool enableEditing)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, enableEditing, false);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString ContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName,
                                                        Expression<Func<T, object>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, false);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString MultilineContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<T, object>> expression, object modelData, bool enableEditing)
        {
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, enableEditing, true);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString MultilineContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<T, object>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, true, true);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString MultilineContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName,
                                                        Expression<Func<T, object>> expression, object modelData, bool enableEditing)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, enableEditing, true);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlString MultilineContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName,
                                                        Expression<Func<T, object>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, true);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <param name="allowMultiline">if set to <c>true</c> allow multiline.</param>
        /// <returns>
        /// The Html for the content, which can be edited.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// An Action Name must be specified to allow content to be edited.
        /// or
        /// An expression that selects the model property must be provided.
        /// </exception>
        private static IHtmlString InternalContentEditableFor<T>(this HtmlHelper<T> htmlHelper, string actionName, string controllerName,
                                                                 Expression<Func<T, object>> expression, object modelData, bool enableEditing,
                                                                 bool allowMultiline)
        {
            //  First, we'll create the URL to the action.
            if (string.IsNullOrEmpty(actionName))
                throw new ArgumentException("An Action Name must be specified to allow content to be edited.", "actionName");

            //  Create the action URL, optionally using the controller name.
            var actionUrl = string.IsNullOrEmpty(controllerName)
                                   ? (new UrlHelper(htmlHelper.ViewContext.RequestContext)).Action(actionName)
                                   : (new UrlHelper(htmlHelper.ViewContext.RequestContext)).Action(actionName, controllerName);

            //  Get the editable content.
            if (expression == null)
                throw new ArgumentException("An expression that selects the model property must be provided.", "expression");
            var expressionResult = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            var editableContent = expressionResult != null ? expressionResult.ToString() : string.Empty;
            var editableContentPropertyName = ExpressionHelper.GetExpressionText(expression);

            //  If we are not editable, we can simply return the editable content, without wrapping it in a span.
            if (enableEditing == false)
                return new MvcHtmlString(editableContent);

            //  Create the model data json.
            var modelDataJson = string.Empty;
            if (modelData != null)
                modelDataJson = (new JavaScriptSerializer()).Serialize(modelData);

            //  Return the Content Editable element.
            return CreateContentEditableHtml(actionUrl, editableContentPropertyName, editableContent, modelDataJson, allowMultiline);
        }

        /// <summary>
        /// Creates the content editable HTML.
        /// </summary>
        /// <param name="actionUrl">The action URL.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="content">The content.</param>
        /// <param name="modelDataJson">The model data json.</param>
        /// <param name="allowMultiline">if set to <c>true</c> allow multiline.</param>
        /// <returns>Html for the content editable element.</returns>
        private static IHtmlString CreateContentEditableHtml(string actionUrl, string propertyName, string content, string modelDataJson, bool allowMultiline)
        {
            var savechanges = new TagBuilder("a");
            savechanges.AddCssClass("cem-savechanges");
            savechanges.Attributes["href"] = "#";

            var discardchanges = new TagBuilder("a");
            discardchanges.AddCssClass("cem-discardchanges");
            discardchanges.Attributes["href"] = "#";

            var toolbar = new TagBuilder("div");
            toolbar.AddCssClass("cem-toolbar");
            toolbar.InnerHtml = discardchanges.ToString();
            toolbar.InnerHtml += savechanges.ToString();

            var contenteditable = new TagBuilder("div");
            contenteditable.Attributes["contenteditable"] = "true";
            contenteditable.AddCssClass("cem-content");
            contenteditable.Attributes["data-property-name"] = propertyName;
            contenteditable.Attributes["data-edit-url"] = actionUrl;
            contenteditable.Attributes["data-model-data"] = modelDataJson;
            contenteditable.Attributes["data-multiline"] = allowMultiline ? "true" : "false";
            contenteditable.SetInnerText(content);

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("cem-wrapper");

            wrapper.InnerHtml = contenteditable.ToString();
            wrapper.InnerHtml += toolbar.ToString();

            return new MvcHtmlString(wrapper.ToString());
        }
    }
}
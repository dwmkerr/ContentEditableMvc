using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContentEditableMvc
{
    /// <summary>
    /// The ContentEditModel represents the parameters of a content edit operation - the 
    /// property to change, the new value and any other data provided.
    /// </summary>
    public class ContentEditModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentEditModel"/> class.
        /// </summary>
        public ContentEditModel()
        {
            //  The model data dictionary is created by deserializing the raw model data.
            lazyModelData = new Lazy<Dictionary<string, string>>(
                () => (new JavaScriptSerializer()).Deserialize<Dictionary<string, string>>(RawModelData));
        }

        private readonly Lazy<Dictionary<string, string>> lazyModelData;

        /// <summary>
        /// Gets or sets the name of the property that is being edited.
        /// </summary>
        /// <value>
        /// The name of the property that is being edited.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the new value for the property.
        /// </summary>
        /// <value>
        /// The new value for the property.
        /// </value>
        [AllowHtml]
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the raw model data. This is a JSON string, to access elements
        /// use the <see cref="ModelData"/> dictionary.
        /// </summary>
        /// <value>
        /// The raw model data.
        /// </value>
        public string RawModelData { get; set; }

        /// <summary>
        /// Gets the model data.
        /// </summary>
        /// <value>
        /// The model data.
        /// </value>
        public Dictionary<string, string> ModelData { get { return lazyModelData.Value; } }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ContentEditableMvcSample.Models
{
    public class ExampleRepository
    {
        public ExampleModel LoadModel()
        {
            if (File.Exists(GetModelPath()) == false)
                return new ExampleModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Example Model",
                        Subtitle = "Example Subtitle",
                        ParagraphText = "Example paragraph."
                    };

            using(var stream = new FileStream(GetModelPath(), FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof (ExampleModel));
                return (ExampleModel)serializer.Deserialize(stream);
            }
        }

        public void SaveModel(ExampleModel model)
        {
            using (var stream = new FileStream(GetModelPath(), FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof (ExampleModel));
                serializer.Serialize(stream, model);
            }
        }

        private string GetModelPath()
        {
            var tempfile = Path.GetRandomFileName();
            var path = Path.Combine(Path.GetDirectoryName(tempfile), "ContentEditableMvcSampleModel.xml");
            return path;
        }
    }
}
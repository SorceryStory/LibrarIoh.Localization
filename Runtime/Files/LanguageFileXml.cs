using SorceressSpell.LibrarIoh.Xml;
using System.Collections.Generic;
using System.Xml;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class LanguageFileXml : LanguageFile
    {
        #region Fields

        private string _content;

        #endregion Fields

        #region Constructors

        public LanguageFileXml(string content)
            : base()
        {
            _content = content;
        }

        #endregion Constructors

        #region Methods

        public override void LoadBasicInfo(ref string tag, ref string name, ref string nativeName)
        {
            XmlDocument xmlDocument = LoadContent();
            XmlElement languageElement = xmlDocument.DocumentElement;

            tag = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.Tag, "");

            name = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.Name, "");
            nativeName = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.NativeName, "");
        }

        public override void LoadStrings(ref Dictionary<string, string> strings)
        {
            XmlDocument xmlDocument = LoadContent();
            XmlElement element = xmlDocument.DocumentElement;

            foreach (XmlElement stringElement in element.SelectNodes(LocalizationXmlNames.Tag.String))
            {
                string stringId = stringElement.GetAttributeValue(LocalizationXmlNames.Attribute.Id, "");
                string stringValue = stringElement.InnerText;

                if (strings.ContainsKey(stringId))
                {
                    strings[stringId] = stringValue;
                }
                else
                {
                    strings.Add(stringId, stringValue);
                }
            }
        }

        private XmlDocument LoadContent()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(_content);
            return xmlDocument;
        }

        #endregion Methods
    }
}

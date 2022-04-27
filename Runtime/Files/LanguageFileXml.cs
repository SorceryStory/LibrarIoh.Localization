using SorceressSpell.LibrarIoh.Xml;
using System.Collections.Generic;
using System.Xml;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class LanguageFileXml : ILanguageFile
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

        public void AddString(string stringId, string stringValue)
        {
            XmlDocument xmlDocument = LoadContent();
            XmlElement languageElement = xmlDocument.DocumentElement;

            LanguageFileOperations.SaveStringXml(xmlDocument, languageElement, stringId, stringValue);

            _content = xmlDocument.DocumentElement.OuterXml;
        }

        public void LoadBasicInfo(ref string tag, ref string name, ref string nativeName)
        {
            LanguageFileOperations.GetBasicInfoXml(LoadContent().DocumentElement, ref tag, ref name, ref nativeName);
        }

        public void LoadStringsTo(Dictionary<string, string> stringDictionary)
        {
            foreach (XmlElement stringElement in LoadContent().DocumentElement.SelectNodes(LocalizationXmlNames.Tag.String))
            {
                string stringId = stringElement.GetAttributeValue(LocalizationXmlNames.Attribute.Id, "");
                string stringValue = stringElement.InnerText;

                if (stringDictionary.ContainsKey(stringId))
                {
                    stringDictionary[stringId] = stringValue;
                }
                else
                {
                    stringDictionary.Add(stringId, stringValue);
                }
            }
        }

        public void SaveStringsXml(XmlDocument xmlDocument, XmlElement xmlElement)
        {
            Dictionary<string, string> stringDictionary = new Dictionary<string, string>();
            LoadStringsTo(stringDictionary);

            foreach (KeyValuePair<string, string> entry in stringDictionary)
            {
                LanguageFileOperations.SaveStringXml(xmlDocument, xmlElement, entry.Key, entry.Value);
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

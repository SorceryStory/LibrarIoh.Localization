using SorceressSpell.LibrarIoh.Core;
using SorceressSpell.LibrarIoh.Xml;
using System.Collections.Generic;
using System.Xml;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class Language : ILoadFrom<XmlDocument>, ISaveAs<XmlDocument>
    {
        #region Fields

        private readonly ILanguageFile _file;
        private string _name;
        private string _nativeName;
        private string _tag;

        #endregion Fields

        #region Properties

        public string Name
        {
            get { return _name; }
        }

        public string NativeName
        {
            get { return _nativeName; }
        }

        public string Tag
        {
            get { return _tag; }
        }

        #endregion Properties

        #region Constructors

        public Language(ILanguageFile file)
        {
            _file = file;
            _file.LoadBasicInfo(ref _tag, ref _name, ref _nativeName);
        }

        #endregion Constructors

        #region Methods

        public void LoadFrom(XmlDocument xmlDocument)
        {
            XmlElement languageElement = xmlDocument.DocumentElement;
            LanguageFileOperations.GetBasicInfoXml(languageElement, ref _tag, ref _name, ref _nativeName);

            foreach (XmlElement stringElement in languageElement.SelectNodes(LocalizationXmlNames.Tag.String))
            {
                string stringId = stringElement.GetAttributeValue(LocalizationXmlNames.Attribute.Id, "");
                string stringValue = stringElement.InnerText;

                _file.AddString(stringId, stringValue);
            }
        }

        public void LoadStringsTo(Dictionary<string, string> stringDictionary)
        {
            _file.LoadStringsTo(stringDictionary);
        }

        public XmlDocument Save()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlOperations.AppendNewDeclaration(xmlDocument, "1.0", "UTF-8", false);

            XmlElement languageElem = XmlOperations.AppendNewRootElement(xmlDocument, "", LocalizationXmlNames.Tag.Language, "");
            XmlOperations.AppendNewAttribute(xmlDocument, languageElem, LocalizationXmlNames.Attribute.Tag, _tag);
            XmlOperations.AppendNewAttribute(xmlDocument, languageElem, LocalizationXmlNames.Attribute.Name, _name);
            XmlOperations.AppendNewAttribute(xmlDocument, languageElem, LocalizationXmlNames.Attribute.NativeName, _nativeName);

            _file.SaveStringsXml(xmlDocument, languageElem);

            return xmlDocument;
        }

        #endregion Methods
    }
}

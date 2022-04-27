using SorceressSpell.LibrarIoh.Xml;
using System.Xml;

namespace SorceressSpell.LibrarIoh.Localization
{
    public static class LanguageFileOperations
    {
        #region Methods

        public static void GetBasicInfoXml(XmlElement languageElement, ref string tag, ref string name, ref string nativeName)
        {
            tag = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.Tag, "");

            name = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.Name, "");
            nativeName = languageElement.GetAttributeValue(LocalizationXmlNames.Attribute.NativeName, "");
        }

        public static void SaveStringXml(XmlDocument xmlDocument, XmlElement xmlElement, string id, string value)
        {
            XmlElement stringElement = XmlOperations.AppendNewElement(xmlDocument, xmlElement, "", LocalizationXmlNames.Tag.String, "");

            XmlOperations.AppendNewAttribute(xmlDocument, stringElement, LocalizationXmlNames.Attribute.Id, id);
            stringElement.InnerText = value;
        }

        #endregion Methods
    }
}

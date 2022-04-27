using System.Collections.Generic;
using System.Xml;

namespace SorceressSpell.LibrarIoh.Localization
{
    public interface ILanguageFile
    {
        #region Methods

        void AddString(string stringId, string stringValue);

        void LoadBasicInfo(ref string tag, ref string name, ref string nativeName);

        void LoadStringsTo(Dictionary<string, string> stringDictionary);

        void SaveStringsXml(XmlDocument xmlDocument, XmlElement xmlElement);

        #endregion Methods
    }
}

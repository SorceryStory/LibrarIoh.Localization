using System.Collections.Generic;

namespace SorceressSpell.LibrarIoh.Localization
{
    public abstract class LanguageFile
    {
        #region Methods

        public abstract void LoadBasicInfo(ref string tag, ref string name, ref string nativeName);

        public abstract void LoadStrings(ref Dictionary<string, string> strings);

        #endregion Methods
    }
}

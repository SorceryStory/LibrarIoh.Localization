using System.Collections.Generic;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class Language
    {
        #region Fields

        private LanguageFile _file;
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

        public Language(LanguageFile file)
        {
            _file = file;
            _file.LoadBasicInfo(ref _tag, ref _name, ref _nativeName);
        }

        #endregion Constructors

        #region Methods

        public void LoadStrings(ref Dictionary<string, string> strings)
        {
            _file.LoadStrings(ref strings);
        }

        #endregion Methods
    }
}

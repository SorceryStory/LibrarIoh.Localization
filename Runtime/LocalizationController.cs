using System.Collections.Generic;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class LocalizationController
    {
        #region Fields

        private Dictionary<string, string> _strings;

        #endregion Fields

        #region Properties

        public Language CurrentLanguage { private set; get; }
        public Dictionary<string, Language> Languages { private set; get; }

        public Dictionary<string, string> Strings
        {
            get { return _strings; }
        }

        #endregion Properties

        #region Events

        public event OnLanguageChangedEvent OnLanguageChanged;

        #endregion Events

        #region Constructors

        public LocalizationController()
        {
            Languages = new Dictionary<string, Language>();

            _strings = new Dictionary<string, string>();

            CurrentLanguage = null;
        }

        #endregion Constructors

        #region Methods

        public void AddLanguage(ILanguageFile languageFile)
        {
            Language language = new Language(languageFile);

            if (!Languages.ContainsKey(language.Tag))
            {
                Languages.Add(language.Tag, language);
            }
        }

        public string GetGenderedString(string stringId, Gender gender, string defaultValue)
        {
            string genderedStringId = GetGenderedStringId(stringId, gender);
            return _strings.ContainsKey(genderedStringId) ? _strings[genderedStringId] : defaultValue;
        }

        public string GetString(string stringId, string defaultValue)
        {
            return _strings.ContainsKey(stringId) ? _strings[stringId] : defaultValue;
        }

        public void SetLanguage(string languageTag)
        {
            // If language doesn't exist, keep the old one
            if (Languages.ContainsKey(languageTag))
            {
                CurrentLanguage = Languages[languageTag];
                CurrentLanguage.LoadStringsTo(_strings);

                OnLanguageChanged?.Invoke(new LanguageChangedEventArgs(this, CurrentLanguage));
            }
        }

        public bool TryGetGenderedString(string stringId, Gender gender, out string localizedString)
        {
            if (_strings.TryGetValue(GetGenderedStringId(stringId, gender), out string value))
            {
                localizedString = value;
                return true;
            }

            return TryGetString(stringId, out localizedString);
        }

        public bool TryGetString(string stringId, out string localizedString)
        {
            if (_strings.TryGetValue(stringId, out string value))
            {
                localizedString = value;
                return true;
            }

            localizedString = stringId;
            return false;
        }

        private string GetGenderedStringId(string stringId, Gender gender)
        {
            switch (gender)
            {
                case Gender.Male: return stringId + "_m";
                case Gender.Female: return stringId + "_f";
                default: return stringId;
            }
        }

        #endregion Methods
    }
}

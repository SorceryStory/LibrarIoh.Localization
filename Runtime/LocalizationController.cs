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

        public void AddLanguage(LanguageFile languageFile)
        {
            Language language = new Language(languageFile);

            if (!Languages.ContainsKey(language.Tag))
            {
                Languages.Add(language.Tag, language);
            }
        }

        public void SetLanguage(string languageTag)
        {
            // If language doesn't exist, keep the old one
            if (Languages.ContainsKey(languageTag))
            {
                CurrentLanguage = Languages[languageTag];
                CurrentLanguage.LoadStrings(ref _strings);

                OnLanguageChanged?.Invoke(new LanguageChangedEventArgs(this, CurrentLanguage));
            }
        }

        public bool TryGetGenderedString(string stringId, PlayerGender playerGender, out string localizedString)
        {
            if (_strings.TryGetValue(GetGenderedStringId(stringId, playerGender), out string value))
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

        private string GetGenderedStringId(string stringId, PlayerGender playerGender)
        {
            switch (playerGender)
            {
                case PlayerGender.Male: return stringId + "_m";
                case PlayerGender.Female: return stringId + "_f";
                default: return stringId;
            }
        }

        #endregion Methods
    }
}

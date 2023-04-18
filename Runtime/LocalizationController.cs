using System.Collections.Generic;

namespace SorceressSpell.LibrarIoh.Localization
{
    public class LocalizationController
    {
        #region Properties

        public Language CurrentLanguage { private set; get; }
        public Dictionary<string, Language> Languages { get; }

        public Dictionary<string, string> Strings { get; }

        #endregion Properties

        #region Events

        public event OnLanguageChangedEvent OnLanguageChanged;

        #endregion Events

        #region Constructors

        public LocalizationController()
        {
            Languages = new Dictionary<string, Language>();

            Strings = new Dictionary<string, string>();

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
            return Strings.ContainsKey(genderedStringId) ? Strings[genderedStringId] : defaultValue;
        }

        public string GetString(string stringId, string defaultValue)
        {
            return Strings.ContainsKey(stringId) ? Strings[stringId] : defaultValue;
        }

        public void SetLanguage(string languageTag)
        {
            // If language doesn't exist, keep the old one
            if (Languages.ContainsKey(languageTag))
            {
                CurrentLanguage = Languages[languageTag];
                CurrentLanguage.LoadStringsTo(Strings);

                OnLanguageChanged?.Invoke(new LanguageChangedEventArgs(this, CurrentLanguage));
            }
        }

        public bool TryGetGenderedString(string stringId, Gender gender, out string localizedString)
        {
            if (Strings.TryGetValue(GetGenderedStringId(stringId, gender), out string value))
            {
                localizedString = value;
                return true;
            }

            return TryGetString(stringId, out localizedString);
        }

        public bool TryGetString(string stringId, out string localizedString)
        {
            if (Strings.TryGetValue(stringId, out string value))
            {
                localizedString = value;
                return true;
            }

            localizedString = stringId;
            return false;
        }

        private string GetGenderedStringId(string stringId, Gender gender)
        {
            return gender switch
            {
                Gender.Male => stringId + "_m",
                Gender.Female => stringId + "_f",
                _ => stringId,
            };
        }

        #endregion Methods
    }
}

namespace SorceressSpell.LibrarIoh.Localization
{
    public class LanguageChangedEventArgs
    {
        #region Fields

        public readonly Language Language;
        public readonly LocalizationController LocalizationController;

        #endregion Fields

        #region Constructors

        public LanguageChangedEventArgs(LocalizationController localizationController, Language language)
            : this()
        {
            LocalizationController = localizationController;
            Language = language;
        }

        private LanguageChangedEventArgs()
        { }

        #endregion Constructors
    }
}

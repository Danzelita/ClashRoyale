using System.IO;

public static class URLLibrary
{
        private const string SET_SELECTED_CARDS = "Game/setSelectedCards.php";
        private const string MAIN = "http://localhost/Projects/Test/";
        private const string AUTORIZATION = "Authorization/autorization.php";
        private const string REGISTRATION = "Authorization/registration.php";
        private const string GET_DECK_INFO = "Game/getDeckInfo.php";

        public static string Autorization => Path.Combine(MAIN, AUTORIZATION);
        public static string Registration => Path.Combine(MAIN, REGISTRATION);
        public static string GetDeckInfo => Path.Combine(MAIN, GET_DECK_INFO);
        public static string SetSelectedCards => Path.Combine(MAIN, SET_SELECTED_CARDS);
}
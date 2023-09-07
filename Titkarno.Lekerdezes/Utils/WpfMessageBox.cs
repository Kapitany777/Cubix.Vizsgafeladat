using System;
using System.Windows;

namespace Titkarno.Lekerdezes.Utils
{
    public static class WpfMessageBox
    {
        /// <summary>
        /// Hibaüzenet küldése
        /// </summary>
        /// <param name="text">A hibaüzenet szövege</param>
        public static void MsgError(string text)
        {
            MessageBox.Show(text, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Két soros hibaüzenet küldése
        /// </summary>
        /// <param name="text1">A hibaüzenet első sorának szövege</param>
        /// <param name="text2">A hibaüzenet második sorának szövege</param>
        public static void MsgError(string text1, string text2)
        {
            MsgError($"{text1}{Environment.NewLine}{text2}");
        }

        /// <summary>
        /// Két soros hibaüzenet küldése egy kivétel üzenetével együtt
        /// </summary>
        /// <param name="text">A hibaüzenet szövege</param>
        /// <param name="ex">A kivétel</param>
        public static void MsgError(string text, Exception ex)
        {
            MsgError(text, ex.Message);
        }

        /// <summary>
        /// Információs üzenet küldése
        /// </summary>
        /// <param name="text">Az információs üzenet szövege</param>
        public static void MsgInfo(string text)
        {
            MessageBox.Show(text, "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

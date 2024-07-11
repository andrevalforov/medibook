//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;

//namespace MediBook
//{
//	public static class CultureManager
//	{
//		private static readonly Dictionary<string, int> Cultures = new Dictionary<string, int>
//		{
//			{ "ru", 3 },
//			{ "uk", 4 },
//		};

//		public static int GetCurrentCultureId()
//		{
//			string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
//			return Cultures.ContainsKey(culture) ? Cultures[culture] : 1;
//		}
//	}

//	public static class DictionaryExtensions
//	{
//		public static string GetLocalization(this Dictionary dictionary)
//		{
//			int cultureId = CultureManager.GetCurrentCultureId();
//			return dictionary?.Localizations?.FirstOrDefault(l => l.CultureId == cultureId)?.Value;
//		}
//	}
//}
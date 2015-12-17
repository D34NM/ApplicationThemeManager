using System;
using System.IO;
using System.Windows;

namespace ThemeManager.Manager
{
	public class ThemeLoader
	{
		#region Public methods

		public string[] GetAvailableThemes(string themeDirectoryPath)
		{
			if (themeDirectoryPath == null)
			{
				throw new MissingMemberException("Theme directory needs to be provided.");
			}

			return Directory.GetFiles(themeDirectoryPath);
		}

		public ResourceDictionary GetResourceDictionaryFor(string theme)
		{
			ResourceDictionaryReader reader = new ResourceDictionaryReader();
			return reader.ReadFrom(theme);
		}

		#endregion
	}
}

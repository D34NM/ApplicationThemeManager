using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace ThemeManager.Manager
{
	public class ResourceDictionaryReader
	{
		#region ReadFrom

		public ResourceDictionary ReadFrom(string xamlFile)
		{
			if (string.IsNullOrEmpty(xamlFile))
			{
				throw new MissingMemberException("Path to XAML file must be provided.");
			}

			string contents = File.ReadAllText(xamlFile);
			return (ResourceDictionary)XamlReader.Parse(contents);
		}

		#endregion
	}
}

using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace ThemeManager.Manager
{
	public static class ThemeManager
	{
		#region Declarations

		private static readonly string CONFIG_FILE = @"Configuration\configuration.json";
		private static readonly string FULL_PATH_TO_CONFIG = string.Concat(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE);

		#endregion

		#region Attached properties

		public static string GetTheme(DependencyObject dependencyObject)
		{
			return (string)dependencyObject.GetValue(ThemeProperty);
		}
		
		public static void SetTheme(DependencyObject dependencyObject, string value)
		{
			dependencyObject.SetValue(ThemeProperty, value);
		}

		public static readonly DependencyProperty ThemeProperty =
			DependencyProperty.RegisterAttached("Theme", typeof(string), typeof(ThemeManager),
													new FrameworkPropertyMetadata(string.Empty,
													new PropertyChangedCallback(OnThemeChanged)));

		#endregion

		#region OnThemeChanged Callback

		private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			string theme = e.NewValue as string;

			if (string.IsNullOrEmpty(theme))
			{
				return;
			}

			string content = File.ReadAllText(FULL_PATH_TO_CONFIG);
			dynamic configuration = JsonConvert.DeserializeObject(content);

			ThemeLoader loader = new ThemeLoader();
			ResourceDictionary dictionary = loader.GetResourceDictionaryFor(theme);

			Application app = Application.Current;

			if (dictionary != null)
			{
				app.Resources.MergedDictionaries.Clear();
				app.Resources.MergedDictionaries.Add(dictionary);
			}
		}

		#endregion

		#region GetAvailableThemes

		public static string[] GetAvailableThemes()
		{
			string content = File.ReadAllText(FULL_PATH_TO_CONFIG);
			dynamic configuration = JsonConvert.DeserializeObject(content);
			
			return Directory.GetFiles((string)configuration.ThemeDirectory);
		}

		#endregion

		#region SetActiveTheme

		public static void SetActiveTheme(string theme)
		{
			dynamic configuration = JsonConvert.DeserializeObject(FULL_PATH_TO_CONFIG);
			configuration.Theme = theme;
			File.WriteAllText(FULL_PATH_TO_CONFIG, JsonConvert.SerializeObject(configuration));
		}

		#endregion
	}
}

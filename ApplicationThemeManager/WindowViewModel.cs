using System.Collections.ObjectModel;

namespace ApplicationThemeManager
{
	public class WindowViewModel
	{
		public WindowViewModel()
		{
			Themes = new ObservableCollection<string>(ThemeManager.Manager.ThemeManager.GetAvailableThemes());
			Theme = Themes[0];
		}

		public string Theme { get; set; }

		public ObservableCollection<string> Themes { get; set; }
	}
}

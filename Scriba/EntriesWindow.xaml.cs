namespace Scriba {
	/// <summary>
	/// Interaction logic for EntriesWindow.xaml
	/// </summary>
	public partial class EntriesWindow {
		public EntriesWindow() {
			InitializeComponent();
			((EntriesViewModel)DataContext).Window = this;
		}
	}
}

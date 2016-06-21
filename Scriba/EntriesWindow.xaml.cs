using System.Windows;

namespace Scriba {
	/// <summary>
	/// Interaction logic for EntriesWindow.xaml
	/// </summary>
	public partial class EntriesWindow : Window {
		public EntriesWindow() {
			InitializeComponent();
			((EntriesViewModel)this.DataContext).window = this;
		}
	}
}

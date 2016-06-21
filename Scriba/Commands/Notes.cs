namespace Scriba.Commands {
	class Notes : ICommand {
		public string[] Identifiers { get; } = {"notes", "entries"};

		public void Execute(string args) {
			((App)System.Windows.Application.Current).ShowEntriesWindow();
			((App)System.Windows.Application.Current).MainWindow.Hide();
		}
	}
}

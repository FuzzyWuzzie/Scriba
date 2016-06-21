namespace Scriba.Commands {
	class Notes : Command {
		readonly string[] ids = {"notes", "entries"};
		public string[] Identifiers {
			get {
				return ids;
			}
		}

		public void Execute(string args) {
			((App)App.Current).ShowEntriesWindow();
			((App)App.Current).MainWindow.Hide();
		}
	}
}

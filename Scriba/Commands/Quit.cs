namespace Scriba.Commands {
	class Quit : Command {
		readonly string[] ids = { "quit", "exit" };
		public string[] Identifiers {
			get {
				return ids;
			}
		}

		public void Execute(string args) {
			((App)App.Current).ExitApplication();
		}
	}
}

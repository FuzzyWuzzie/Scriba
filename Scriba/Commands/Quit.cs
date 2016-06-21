namespace Scriba.Commands {
	class Quit : ICommand {
		public string[] Identifiers { get; } = { "quit", "exit" };

		public void Execute(string args) {
			((App)System.Windows.Application.Current).ExitApplication();
		}
	}
}

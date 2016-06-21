namespace Scriba {
	public interface ICommand {
		string[] Identifiers { get; }
		void Execute(string args);
	}
}

namespace Scriba {
	public interface Command {
		string[] Identifiers { get; }
		void Execute(string args);
	}
}

using System;
namespace Scriba {
	public class Entry {
		public string entry { get; private set; }
		public DateTime time { get; private set; }

		public Entry(string entry) {
			this.entry = entry;
			time = DateTime.Now;
		}

		public Entry(string entry, DateTime time) {
			this.entry = entry;
			this.time = time;
		}
	}
}

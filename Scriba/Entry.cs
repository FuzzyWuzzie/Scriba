using System;
namespace Scriba {
	public class Entry {
		public string entry = "";
		public DateTime time;

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

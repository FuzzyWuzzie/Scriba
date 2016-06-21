using System;
namespace Scriba {
	public class Entry {
		public string Note { get; private set; }
		public DateTime Time { get; private set; }

		public Entry(string entry) {
			Note = entry;
			Time = DateTime.Now;
		}

		public Entry(string entry, DateTime time) {
			Note = entry;
			Time = time;
		}
	}
}

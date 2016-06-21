using System.Collections.Generic;
using System.ComponentModel;

namespace Scriba {
	class EntriesViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private List<Entry> _entries = new List<Entry>();
		public List<Entry> Entries {
			get { return _entries; }
			set {
				if (value == _entries) return;
				_entries = value; NotifyPropertyChanged("Entries");
			}
		}

		public EntriesViewModel() {
			Entries = App.Db.GetLatest(100);
		}

		public EntriesWindow Window { get; set; }
		public System.Windows.Input.ICommand Close {
			get {
				return new DelegateCommand(context => {
					Window?.Close();
				});
			}
		}
	}
}

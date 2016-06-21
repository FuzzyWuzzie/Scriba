using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Scriba {
	class EntriesViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private List<Entry> entries = new List<Entry>();
		public List<Entry> Entries {
			get { return entries; }
			set { if(value != entries) { entries = value; NotifyPropertyChanged("Entries"); } }
		}

		public EntriesViewModel() {
			Entries = App.db.GetLatest(100);
		}

		public EntriesWindow window;
		public ICommand Close {
			get {
				return new DelegateCommand<object>(context => {
					if(window != null) {
						window.Close();
					}
				});
			}
		}
	}
}

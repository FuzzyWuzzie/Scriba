using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Scriba {
	class MainViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public string CurrentTime {
			get {
				DateTime now = DateTime.Now;
				return now.ToShortTimeString();
			}
		}

		private string currentEntry = "";
		public string CurrentEntry {
			get { return currentEntry; }
			set {
				currentEntry = value;
				NotifyPropertyChanged("CurrentEntry");
			}
		}

		public ICommand Hide {
			get {
				return new DelegateCommand<object>(context => {
					App.Current.MainWindow.Hide();
				});
			}
		}

		public ICommand SaveEntry {
			get {
				return new DelegateCommand<object>(context => {
					string entry = CurrentEntry;
					App.db.Add(new Entry(entry, DateTime.Now));

					CurrentEntry = "";
					App.Current.MainWindow.Hide();
				});
			}
		}
	}
}

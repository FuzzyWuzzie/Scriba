using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Scriba {
	class MainViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private List<Command> commands = new List<Command>();
		public MainViewModel() {
			commands.Add(new Commands.Notes());
			commands.Add(new Commands.Quit());
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

		private Command GetCommand(string entry) {
			// make sure it starts with the command signal
			if(entry.Length < 2 || entry[0] != '/') return null;
			
			string entryLowerCase = entry.Substring(1).ToLower();
			foreach(Command c in commands) {
				foreach(string i in c.Identifiers) {
					if(entryLowerCase == i || entryLowerCase.StartsWith(i + " ")) {
						return c;
					}
				}
			}
			return null;
		}

		public ICommand SaveEntry {
			get {
				return new DelegateCommand<object>(context => {
				Command cmd = GetCommand(CurrentEntry);
				if(cmd != null) {
						// get the 'args' portion
						string[] parts = CurrentEntry.Split(new char[] { ' ' }, 2);
						cmd.Execute(parts.Length > 1 ? parts[1] : "");
					}
					else {
						string entry = CurrentEntry;
						App.db.Add(new Entry(entry, DateTime.Now));
						App.Current.MainWindow.Hide();
					}

					CurrentEntry = "";
				});
			}
		}
	}
}

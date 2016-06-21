using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scriba {
	class MainViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private readonly List<ICommand> _commands = new List<ICommand>();
		public MainViewModel() {
			_commands.Add(new Commands.Notes());
			_commands.Add(new Commands.Quit());
		}

		public string CurrentTime {
			get {
				DateTime now = DateTime.Now;
				return now.ToShortTimeString();
			}
		}

		private string _currentEntry = "";
		public string CurrentEntry {
			get { return _currentEntry; }
			set {
				_currentEntry = value;
				NotifyPropertyChanged("CurrentEntry");
			}
		}

		public System.Windows.Input.ICommand Hide {
			get {
				return new DelegateCommand(context => {
					System.Windows.Application.Current.MainWindow.Hide();
				});
			}
		}

		private ICommand GetCommand(string entry) {
			// make sure it starts with the command signal
			if(entry.Length < 2 || entry[0] != '/') return null;
			
			string entryLowerCase = entry.Substring(1).ToLower();
			foreach(ICommand c in _commands) {
				foreach(string i in c.Identifiers) {
					if(entryLowerCase == i || entryLowerCase.StartsWith(i + " ")) {
						return c;
					}
				}
			}
			return null;
		}

		public System.Windows.Input.ICommand SaveEntry {
			get {
				return new DelegateCommand(context => {
				var cmd = GetCommand(CurrentEntry);
				if(cmd != null) {
						// get the 'args' portion
						string[] parts = CurrentEntry.Split(new[] { ' ' }, 2);
						cmd.Execute(parts.Length > 1 ? parts[1] : "");
					}
					else {
						var entry = CurrentEntry;
						App.Db.Add(new Entry(entry, DateTime.Now));
						System.Windows.Application.Current.MainWindow.Hide();
					}

					CurrentEntry = "";
				});
			}
		}
	}
}

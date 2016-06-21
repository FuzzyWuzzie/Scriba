using System.Windows;
using System.Windows.Forms;

namespace Scriba {
	public partial class App {
		private NotifyIcon _notifyIcon;
		private bool _isExit;
		private HotKey _showEntry;

		private EntriesWindow _entriesWindow;

		public static EntryDatabase Db = new EntryDatabase();

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			// setup the main window
			MainWindow = new MainWindow();
			MainWindow.Closing += MainWindow_Closing;

			// add a notification icon
			_notifyIcon = new NotifyIcon();
			_notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
			_notifyIcon.Icon = Scriba.Properties.Resources.scriba;
			_notifyIcon.Visible = true;

			// add the system-wide hotkey for showing our entry window
			_showEntry = new HotKey(System.Windows.Input.Key.Space, KeyModifier.Alt, OnShowEntryHotKey);

			// initialize the database
			Db.Initialize();

			CreateContextMenu();
		}

		private void CreateContextMenu() {
			_notifyIcon.ContextMenuStrip = new ContextMenuStrip();
			_notifyIcon.ContextMenuStrip.Items.Add("New Entry").Click += (s, e) => ShowMainWindow();
			_notifyIcon.ContextMenuStrip.Items.Add("Show Entries").Click += (s, e) => ShowEntriesWindow();
			_notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			_notifyIcon.ContextMenuStrip.Items.Add("Settings");
			_notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			_notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
		}

		public void ShowMainWindow() {
			if(MainWindow.IsVisible) {
				if(MainWindow.WindowState == WindowState.Minimized) {
					MainWindow.WindowState = WindowState.Normal;
				}
				MainWindow.Activate();
			}
			else {
				MainWindow.Show();
			}
		}

		public void ShowEntriesWindow() {
			if(_entriesWindow == null) {
				_entriesWindow = new EntriesWindow();
				_entriesWindow.Show();
				_entriesWindow.Closed += (s, e) => {
					_entriesWindow = null;
				};
			}
		}

		private void OnShowEntryHotKey(HotKey hotKey) {
			ShowMainWindow();
		}

		public void ExitApplication() {
			_isExit = true;
			MainWindow.Close();
			_entriesWindow?.Close();
			_notifyIcon.Dispose();
			_showEntry.Dispose();
			Db.Dispose();
			_notifyIcon = null;
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(!_isExit) {
				e.Cancel = true;
				MainWindow.Hide();
			}
		}
	}
}

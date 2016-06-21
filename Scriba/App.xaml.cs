using System.Windows;
using System.Windows.Forms;

namespace Scriba {
	public partial class App : System.Windows.Application {
		private NotifyIcon notifyIcon;
		private bool isExit = false;
		private UnManaged.HotKey showEntry;

		private EntriesWindow entriesWindow;

		public static EntryDatabase db = new EntryDatabase();

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			// setup the main window
			MainWindow = new MainWindow();
			MainWindow.Closing += MainWindow_Closing;

			// add a notification icon
			notifyIcon = new NotifyIcon();
			notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
			notifyIcon.Icon = Scriba.Properties.Resources.scriba;
			notifyIcon.Visible = true;

			// add the system-wide hotkey for showing our entry window
			showEntry = new UnManaged.HotKey(System.Windows.Input.Key.Space, UnManaged.KeyModifier.Alt, OnShowEntryHotKey);

			// initialize the database
			db.Initialize();

			CreateContextMenu();
		}

		private void CreateContextMenu() {
			notifyIcon.ContextMenuStrip = new ContextMenuStrip();
			notifyIcon.ContextMenuStrip.Items.Add("New Entry").Click += (s, e) => ShowMainWindow();
			notifyIcon.ContextMenuStrip.Items.Add("Show Entries").Click += (s, e) => ShowEntriesWindow();
			notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			notifyIcon.ContextMenuStrip.Items.Add("Settings");
			notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
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
			if(entriesWindow == null) {
				entriesWindow = new EntriesWindow();
				entriesWindow.Show();
				entriesWindow.Closed += (s, e) => {
					entriesWindow = null;
				};
			}
		}

		private void OnShowEntryHotKey(UnManaged.HotKey hotKey) {
			ShowMainWindow();
		}

		public void ExitApplication() {
			isExit = true;
			MainWindow.Close();
			if(entriesWindow != null) {
				entriesWindow.Close();
			}
			notifyIcon.Dispose();
			showEntry.Dispose();
			db.Dispose();
			notifyIcon = null;
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(!isExit) {
				e.Cancel = true;
				MainWindow.Hide();
			}
		}
	}
}

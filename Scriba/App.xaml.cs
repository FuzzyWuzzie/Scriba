using System.Windows;

namespace Scriba {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private bool isExit = false;
		private UnManaged.HotKey showEntry;

		public static EntryDatabase db = new EntryDatabase();

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			// setup the main window
			MainWindow = new MainWindow();
			MainWindow.Closing += MainWindow_Closing;

			// add a notification icon
			notifyIcon = new System.Windows.Forms.NotifyIcon();
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
			notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			notifyIcon.ContextMenuStrip.Items.Add("Show").Click += (s, e) => ShowMainWindow();
			notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
		}

		private void ShowMainWindow() {
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

		private void OnShowEntryHotKey(UnManaged.HotKey hotKey) {
			ShowMainWindow();
		}

		private void ExitApplication() {
			isExit = true;
			MainWindow.Close();
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

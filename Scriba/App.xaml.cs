using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Scriba {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private bool isExit = false;

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			MainWindow = new MainWindow();
			MainWindow.Closing += MainWindow_Closing;

			notifyIcon = new System.Windows.Forms.NotifyIcon();
			notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
			notifyIcon.Icon = Scriba.Properties.Resources.scriba;
			notifyIcon.Visible = true;

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

		private void ExitApplication() {
			isExit = true;
			MainWindow.Close();
			notifyIcon.Dispose();
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

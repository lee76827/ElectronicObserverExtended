using ElectronicObserver.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicObserver.Utility;

namespace ElectronicObserver
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{

			Application.ThreadException += Application_ThreadException;

			var mutex = new System.Threading.Mutex(false, Application.ExecutablePath.Replace('\\', '/'));

			if (!mutex.WaitOne(0, false))
			{
				// 多重起動禁止
				MessageBox.Show("既に起動しています。\r\n多重起動はできません。", "七四式電子観測儀", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());

			mutex.ReleaseMutex();
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.ToString(), "ElectronicObserverExtended", MessageBoxButtons.OK, MessageBoxIcon.Error);
			ErrorReporter.SendErrorReport(e.Exception, "Error in thread: " + e.Exception.Message);
		}

		public static System.Drawing.Font Window_Font;
	}
}

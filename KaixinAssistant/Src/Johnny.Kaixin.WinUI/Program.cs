using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Johnny.Kaixin.Core;
using Johnny.Library.Helper;
using Johnny.Controls.Windows.CommonMessageBox;

namespace Johnny.Kaixin.WinUI
{
    static class Program
    {
        public delegate void TryCatach();
        
        private delegate void ExceptionDelegate(Exception ex);
        static private MainForm _mainform;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //log4net.Config.BasicConfigurator.Configure(new log4net.Appender.FileAppender(new log4net.Layout.PatternLayout("%d [%t] %-5p %c [%x] - %m%n"), "log-file.txt")); 
            Application.EnableVisualStyles();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledException);
            Application.SetCompatibleTextRenderingDefault(false);
            //解决Cross-thread operation not valid的问题
            //Control.CheckForIllegalCrossThreadCalls = false;

            _mainform = new MainForm();
            Application.Run(_mainform);            
        }
        
        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {            
            Exception exception;
            exception = e.ExceptionObject as Exception;
            if (exception == null)
            { 
                // this is an unmanaged exception, you may want to handle it differently    
                return;
            }
            PublishOnMainThread(exception);
        }
        private static void PublishOnMainThread(Exception exception)
        {
            if (_mainform.InvokeRequired)
            {
                // Invoke executes a delegate on the thread that owns _MainForms's underlying window handle.    
                _mainform.Invoke(new ExceptionDelegate(HandleException), new object[] { exception });
            }
            else
            {
                HandleException(exception);
            }
        }
        private static void HandleException(Exception exception)
        {
            

            //System.ComponentModel.CancelEventArgs e= new System.ComponentModel.CancelEventArgs();
            //e.Cancel = true;
            //Application.Exit(e);
          
            //Application.Exit();
            //Environment.Exit(0);

            if (SystemInformation.UserInteractive)
            {
                ShowMessageBox("UnhandledException", exception);
                
                //return;
                using (ThreadExceptionDialog dialog = new ThreadExceptionDialog(exception))
                {
                    if (dialog.ShowDialog() == DialogResult.Cancel)
                        return;
                }
                Application.Exit();
                Environment.Exit(0);
            }
        }

        public static void ShowMessageBox(string module, Exception ex)
        {
            ShowMessageBox(module, "", ex);
        }

        public static void ShowMessageBox(string module, string troubleshooting, Exception ex)
        {
            LogHelper.Write(module, ex);
            //MessageBox.Show(strMsg, Constants.MSG_SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            FrmMessageBox frmMsg = new FrmMessageBox();
            frmMsg.Module = module;
            frmMsg.Exception = ex;
            frmMsg.TroubleShooting = troubleshooting;
            frmMsg.ShowDialog();
        }

        //public static void ExecuteTryCatch(TryCatach method)
        //{
        //    try
        //    {
        //        method();
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.ShowMessageBox(ex);
        //    }
        //}

    }
}
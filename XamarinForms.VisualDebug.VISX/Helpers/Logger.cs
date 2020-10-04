using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace XamarinForms.VisualDebug.VsixExtension.Helpers
{
    public static class Logger
    {
        private static IVsOutputWindowPane pane;

        private static object _syncRoot = new object();

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Microsoft.VisualStudio.Shell.Interop.IVsOutputWindowPane.OutputString(System.String)")]
        public static void Log(string message)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                if (EnsurePane())
                {
                    pane.OutputString(DateTime.Now.ToString() + ": " + message + Environment.NewLine);
                }
            }
            catch
            {
                // Do nothing
            }
        }

        public static void Log(Exception ex)
        {
            if (ex != null)
            {
                Log(ex.ToString());
                Telemetry.TrackException(ex);
            }
        }

        public static void ShowMessage(string message, string title = Vsix.Name,
            MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Warning,
            MessageBoxDefaultButton messageBoxDefaultButton = MessageBoxDefaultButton.Button1)
        {
            Log(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", title, message));
        }

        private static bool EnsurePane()
        {
            if (pane == null)
            {
                lock (_syncRoot)
                {
                    if (pane == null)
                    {
                        pane = VISXPackage.Instance.GetOutputPane(VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid, Vsix.Name);
                    }
                }
            }

            return pane != null;
        }
    }
}
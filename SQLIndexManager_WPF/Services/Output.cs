using System;
using System.IO;

using DevExpress.Xpf.Bars;

namespace SQLIndexManager_WPF.Services
{
    public class OutputEvent
    {

        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public DateTime? Duration { get; set; }

    }

    //This is temporary solution
    //TODO Here is too many responsibilities. This class shall be split among model, viewmodel and log service is provided by DI container from Microsoft
    public class Output
    {

        private static Output _log;
        private BarStaticItem _control;
        private AssemblyDataService _assembly;

        public static Output Current => _log;

        internal Output(AssemblyDataService assembly)
        {
            _assembly = assembly;

            if (File.Exists(_assembly.GetLogFileName()))
            {
                try
                {
                    File.Delete(_assembly.GetLogFileName());
                }
                catch { }
            }
        }

        public void SetOutputControl(BarStaticItem control)
        {
            _control = control;
        }

        public void AddCaption(string message)
        {
            try
            {
                _control.Content = message;
            }
            catch { }
        }

        public void Add(string message, string message2 = null, long? elapsedMilliseconds = null)
        {
            DateTime now = DateTime.Now;
            DateTime? duration = null;
            string msg = message;

            if (elapsedMilliseconds >= 0)
            {
                duration = new DateTime(0).AddMilliseconds((double)elapsedMilliseconds);
                msg = $"Elapsed time: {duration:HH:mm:ss:fff}. {message}";
            }

            var ev = new OutputEvent
            {
                DateTime = now,
                Message = string.IsNullOrEmpty(message2) ? message : $"{message}{Environment.NewLine}{message2}",
                Duration = duration
            };

            try
            {
                if (_control != null)
                {
                    _control.Content = msg;
                }

                using (StreamWriter sw = File.AppendText(_assembly.GetLogFileName()))
                {
                    sw.WriteLine($"{now:HH:mm:ss.fff} - {msg}");
                    if (!string.IsNullOrEmpty(message2))
                        sw.WriteLine(message2);
                }
            }
            catch { }
        }
    }
}

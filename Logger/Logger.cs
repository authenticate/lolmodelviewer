using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;

namespace CSharpLogger
{
    public class Logger
    {
        private String filePath;
        private StreamWriter writer;
        private StringBuilder builder;
        private bool flushHold;

        /// <summary>
        /// Allows easy logging of errors, warnings, and events
        /// </summary>
        /// <param name="filePath"></param>
        public Logger(String filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Call to close the filestream
        /// </summary>
        public void Close()
        {
            writer.Close();
        }

        private void Flush()
        {
            if (writer == null || writer.BaseStream == null)
                writer = new StreamWriter(filePath, true);
            writer.Write(builder);
            builder.Clear();
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The message to explain the error</param>
        public void Error(String message)
        {
            if (builder == null)
                builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString("MM-dd-yy HH:mm:ss") + " - Error: " + message);
            if (!flushHold)
                Flush();
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The message to explain the warning</param>
        public void Warning(String message)
        {
            if (builder == null)
                builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString("MM-dd-yy HH:mm:ss") + " - Warning: " + message);
            if (!flushHold)
                Flush();
        }

        /// <summary>
        /// Logs an event
        /// </summary>
        /// <param name="message">The message to explain the event</param>
        public void Event(String message)
        {
            if (builder == null)
                builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString("MM-dd-yy HH:mm:ss") + " - Event: " + message);
            if (!flushHold)
                Flush();
        }

        /// <summary>
        /// Stops all filewriting and holds all logging in a buffer until RestartFlushes() is called
        /// </summary>
        public void HoldFlushes()
        {
            flushHold = true;
        }

        /// <summary>
        /// Logging will flush to file after each log call. Forces an immediate flush
        /// </summary>
        public void RestartFlushes()
        {
            flushHold = false;
            Flush();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Logger
{
    public class Logger
    {
        String filePath;
        private StreamWriter writer;
        private StringBuilder builder;

        /// <summary>
        /// Allows easy logging of errors, warnings, and events
        /// </summary>
        /// <param name="filePath"></param>
        public Logger(String filePath)
        {
            this.filePath = filePath;
        }

        public void Error(String message)
        {
            if (builder == null)
                builder = new StringBuilder();
            builder.AppendLine(DateTime.Now.ToString("MM-dd-yy HH:mm:ss"));
        }


    }
}

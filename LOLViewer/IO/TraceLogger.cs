
/*
LOLViewer
Copyright 2011-2012 James Lammlein 

 

This file is part of LOLViewer.

LOLViewer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

LOLViewer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with LOLViewer.  If not, see <http://www.gnu.org/licenses/>.

*/


//
// This class logs events.  It forwards trace messages to a file.
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Diagnostics;

namespace LOLViewer.IO
{
    public class TraceLogger
    {
        TextWriterTraceListener writer;

        public TraceLogger()
        {
            Trace.Listeners.Clear();
            Trace.AutoFlush = true;
        }

        public bool Open(String fileName)
        {
            bool result = true;

            try
            {
                writer = new TextWriterTraceListener(fileName);
                writer.Name = "LOLViewer Logger";
                writer.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

                Trace.Listeners.Add(writer);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void Close()
        {
            if (writer != null)
            {
                writer.Close();
            }
        }

        public void LogError(string error)
        {
            Trace.WriteLine("Error: " + error);
        }

        public void LogWarning(string warning)
        {
            Trace.WriteLine("Warning: " + warning);
        }

        public void LogEvent(string e)
        {
            Trace.WriteLine("Event: " + e);
        }
    }
}

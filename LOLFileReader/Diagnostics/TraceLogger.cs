


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
// Internal class for diagnostics.
//
// Use a trace listener to recieve the log messages.
//



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace LOLFileReader
{
    namespace Diagnostics
    {
        internal class TraceLogger
        {
            private bool isLogging;

            public TraceLogger(bool enableLogging)
            {
                isLogging = enableLogging;
            }

            public void LogError(string error)
            {
                if (isLogging == true)
                {
                    Trace.WriteLine("Error: " + error);
                }
            }

            public void LogWarning(string warning)
            {
                if (isLogging == true)
                {
                    Trace.WriteLine("Warning: " + warning);
                }
            }

            public void LogEvent(string e)
            {
                if (isLogging == true)
                {
                    Trace.WriteLine("Event: " + e);
                }
            }
        }
    }
}

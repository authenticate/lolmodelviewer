


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
// Logs events.
//



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace LOLFileReader
{
    public class EventLogger
    {
        private TextWriter file;

        public EventLogger() {}

        public bool Open( string fileName )
        {
            bool result = true;

            try
            {
                file = new StreamWriter(fileName);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void Close()
        {
            if (file != null)
            {
                file.Close();
            }
        }

        public void LogError(string error)
        {
            if (file != null)
            {
                file.WriteLine("Error: " + error);
            }
        }

        public void LogWarning(string warning)
        {
            if (file != null)
            {
                file.WriteLine("Warning: " + warning);
            }
        }

        public void LogEvent(string e)
        {
            if (file != null)
            {
                file.WriteLine("Event: " + e);
            }
        }
    }
}

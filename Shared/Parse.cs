using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Parse
    {
        public static DateTime Date(string value)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return DateTime.Now;
			}
        }
    }
}

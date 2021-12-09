using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class CharExtensions
    {
        public static long ToInt(this char c) => int.Parse(new string(c, 1));
    }
}

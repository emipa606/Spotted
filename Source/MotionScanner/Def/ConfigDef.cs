using System.Collections.Generic;
using Verse;

namespace Spotted
{
    class ConfigDef : Def
    {
        private List<string> args = new List<string>();

        public List<string> GetArgs()
        {
            return args;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Core.Common
{
    public class StringObjectPairs : Dictionary<string, object>
    {
        public StringObjectPairs() : this(StringComparer.OrdinalIgnoreCase)
        {

        }

        public StringObjectPairs(IEqualityComparer<string> comparer) : base(comparer)
        {

        }
    }
}

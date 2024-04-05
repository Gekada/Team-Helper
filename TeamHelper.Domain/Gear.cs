using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHelper.Domain
{
    public class Gear
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
    }
}

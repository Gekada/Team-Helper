using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamHelper.Domain
{
    public class IndicatorsData
    {
        public Guid Id { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
    }
}

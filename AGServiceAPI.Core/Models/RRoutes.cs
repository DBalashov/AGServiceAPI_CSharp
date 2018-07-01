using System;

namespace AutoGRAPHService
{
    public class RRouteSegment
    {
        public RPoint[] Points { get; set; }
        public double Distance { get; set; }
        public TimeSpan? Duration { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        
#if DEBUG
        public override string ToString() => $"From/To={AddressFrom}/{AddressTo}, Points={Points.Length}, Distance={Distance}";
#endif
    }

    public enum RouterType : int
    {
        Google = 0,
        Progorod = 1,
        GraphHopper = 2,
        OSRM = 3
    }
}
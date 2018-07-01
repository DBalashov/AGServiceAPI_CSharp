using System;

namespace AutoGRAPHService
{
    public class RTrackInfo
    {
        public int Index { get; set; }
        public DateTime[] DT { get; set; }
        public double[] Speed { get; set; }
        public double[] Lat { get; set; }
        public double[] Lng { get; set; }

#if DEBUG
        public override string ToString() => $"{Index}: Points={DT?.Length.ToString() ?? "NULL"}";
#endif
    }
}
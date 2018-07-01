using System;

namespace AutoGRAPHService
{
    public class RTripTableItem
    {
        public int Index { get; set; }
        public DateTime SD { get; set; }
        public DateTime ED { get; set; }
        public RPoint PointStart { get; set; }
        public RPoint PointEnd { get; set; }
        public DateTime[] DT { get; set; }
        public RTripTableValues[] Values { get; set; }

#if DEBUG
        public override string ToString() => Index + ": " + SD.ToString("dd.MM.yyyy HH:mm:ss") + " - " + ED.ToString("dd.MM.yyyy HH:mm:ss") + ", DT=" + DT.Length + ", Values=" + Values.Length;
#endif
    }

    public class RTripTables
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Serial { get; set; }

        public RTripTableItem[] Trips { get; set; }

#if DEBUG
        public override string ToString() => "Trips=" + Trips.Length;
#endif
    }
}

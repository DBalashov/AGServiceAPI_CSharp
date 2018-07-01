using System;

namespace AutoGRAPHService
{
    public class RTripItemContainer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string[] Params { get; set; }
        public ReturnType[] ParamTypes { get; set; }
        public RTripItem[] Items { get; set; }

#if DEBUG
        public override string ToString() => Name + ": Params=" + Params.Length + ", Items=" + Items.Length;
#endif
    }

    public class RTripItem
    {
        public int TripIndex { get; set; }
        public string Stage { get; set; }
        public object DT { get; set; }
        public object Duration { get; set; }
        public string Caption { get; set; }
        public int Status { get; set; }
        public Guid StatusID { get; set; }

        public object[] Values { get; set; }

#if DEBUG
        public override string ToString() => TripIndex + ": " + Stage + ", " + DT  + " (" + Duration + ")" + ", Caption=" + Caption + ", Status[" + StatusID + "]=" + Status;
#endif
    }
}

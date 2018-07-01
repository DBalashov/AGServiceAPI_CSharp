using System;

namespace AutoGRAPHService
{
    public class REnumDevices
    {
        public string ID { get; set; }
        public RGroupItem[] Groups { get; set; }
        public RDeviceItem[] Items { get; set; }

#if DEBUG
        public override string ToString() => $"Groups={Groups?.Length.ToString() ?? "NULL"}, Items={Items?.Length.ToString() ?? "NULL"}";
#endif
    }

    public class RDeviceItem : RGroupItem
    {
        public int Serial { get; set; }
        public bool Allowed { get; set; }
        public RProperty[] Properties { get; set; }
        public RPoint FixedLocation { get; set; }
        public string Image { get; set; }
        public RTripSplitter[] TripSplitters { get; set; }
        public bool IsAreaEnabled { get; set; }

#if DEBUG
        public override string ToString() => $"{ID} [ {Serial}]: {Name}, Properties={Properties?.Length.ToString() ?? "NULL"}";
#endif
    }

    public class RTripSplitter
    {
        public int ID { get; set; }
        public string Name { get; set; }

#if DEBUG
        public override string ToString() => $"{ID}: {Name}";
#endif
    }
}

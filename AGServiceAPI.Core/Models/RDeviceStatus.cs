using System;

namespace AutoGRAPHService
{
    public class RDeviceStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool Enabled { get; set; }

#if DEBUG
        public override string ToString() => $"{ID}: {Name} ({ImageName})";
#endif
    }

    public class RDeviceStatusItem
    {
        public int ID { get; set; }
        public RDeviceStatus Status { get; set; }
        public DateTime From { get; set; }

#if DEBUG
        public override string ToString() => $"{ID}: {From} {(Status != null ? ", " + Status.Name : "")}";
#endif
    }
}
using System;

namespace AutoGRAPHService
{
    public class REnumGeofences
    {
        public string ID { get; set; }
        public RGroupItem[] Groups { get; set; }
        public RGeofenceItem[] Items { get; set; }
    }

    public class RGeofenceItem : RGroupItem
    {
        public RProperty[] Properties { get; set; }
        public string Image { get; set; }        
    }
}
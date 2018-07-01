using System;
using System.Runtime.Serialization;

namespace AutoGRAPHService
{    
    public class RDeviceInfo
    {
        public RDeviceStage[] Stages { get; set; }
    }

    public class RDeviceStage
    {
        public string Name { get; set; }
        public string Parameter { get; set; }
        public bool IsGroup { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }

#if DEBUG
        public override string ToString() => $"{Name} ({Caption}) {(IsGroup ? " [GROUP]" : "")}: {Parameter}";
#endif
    }
}
using System;

namespace AutoGRAPHService
{
    public class RSchema
    {
        public string ID { get; set; }
        public string Name { get; set; }

#if DEBUG
        public override string ToString() => $"{ID}: {Name}";
#endif
    }
}
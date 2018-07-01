using System;

namespace AutoGRAPHService
{
    public class RParameters
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public RParameter[] FinalParams { get; set; }
        public RParameter[] OnlineParams { get; set; }
        public RParameter[] TripsParams { get; set; }

#if DEBUG
        public override string ToString() => $"{ID} [{Name}]: FinalParams={FinalParams?.Length.ToString() ?? "NULL"}, OnlineParams={OnlineParams?.Length.ToString() ?? "NULL"}";
#endif
    }

    public class RBaseParameterValue
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Alias { get; set; }
        public ReturnType ReturnType { get; set; }
        public AddValueType ValueType { get; set; }
        public string Unit { get; set; }
        public string Format { get; set; }
        public RParameterStatus[] Statuses { get; set; }

#if DEBUG
        public override string ToString() => $"{Name} [{ReturnType}]: ({Alias}) {Caption}, Unit={Unit}, Statuses={Statuses?.Length.ToString() ?? ""}";
#endif
    }

    public class RParameter : RBaseParameterValue
    {
        public string GroupName { get; set; }        
    }

    public class RParameterStatus
    {
        public int Value { get; set; }
        public string Caption { get; set; }
        public Guid ReferenceID { get; set; }
        public Guid[] ReferenceIDs { get; set; }

#if DEBUG
        public override string ToString() => $"{Value}: {Caption}";
#endif
    }
}
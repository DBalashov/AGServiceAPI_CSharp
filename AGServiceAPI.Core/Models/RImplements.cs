using System;

namespace AutoGRAPHService
{
    public class REnumImplements
    {
        public string ID { get; set; }
        public RGroupItem[] Groups { get; set; }
        public RImplementItem[] Items { get; set; }

#if DEBUG
        public override string ToString() => "Groups=" + (Groups?.Length.ToString() ?? "NULL") + ", " +
                                             "Items=" + (Items?.Length.ToString() ?? "NULL");
#endif
    }
   
    public class RImplementItem : RGroupItem
    {
        public string ImplementID { get; set; }
        public RProperty[] Properties { get; set; }
        public string Image { get; set; }

#if DEBUG
        public override string ToString() => $"{ID} [{ImplementID}]: {Name}, Properties={Properties?.Length.ToString() ?? "NULL"}";
#endif
    }
}
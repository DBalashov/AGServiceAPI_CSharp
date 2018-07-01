using System;
using System.Runtime.CompilerServices;

namespace AutoGRAPHService
{
    public class RGroupItem
    {
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public string Name { get; set; }

#if DEBUG
        public override string ToString() => $"{ID}: {Name}";
#endif
    }

    public class RPoint
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public RPoint() { }        

        public RPoint(double _lat, double _lng)
        {
            Lat = Math.Round(_lat, 8);
            Lng = Math.Round(_lng, 8);
        }

        public override string ToString() => $"{Lat} / {Lng}";
    }

    public static class TypeExtenders
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object TimeSpanToSeconds(this object o) => o is TimeSpan span ? span.TotalSeconds : o;
    }
    
    public enum ReturnType : int
    {
        /// <summary> 0 </summary>
        Boolean = 0,

        /// <summary> 1 </summary>
        Byte = 1,

        /// <summary> 2 </summary>
        Int32 = 2,

        /// <summary> 3 </summary>
        Int64 = 3,

        /// <summary> 4 </summary>
        Double = 4,

        /// <summary> 5 </summary>
        DateTime = 5,

        /// <summary> 6 </summary>
        TimeSpan = 6,

        /// <summary> 7 </summary>
        Guid = 7,

        /// <summary> 8 </summary>
        Guid4 = 8,

        /// <summary> 9 </summary>
        String = 9,
        
        /// <summary> 10, for DataTable only </summary>
        Image = 10,
        
        /// <summary> 11  </summary>
        Coordinates = 11,

        /// <summary> 12 </summary>
        Location = 12
    }

    public enum AddValueType : int
    {
        /// <summary> 0 </summary>
        Curr = 0,

        /// <summary> 1 </summary>
        First = 1,

        /// <summary> 2 </summary>
        Last = 2,

        /// <summary> 3 </summary>
        Diff = 3
    }
}
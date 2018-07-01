using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace AutoGRAPHService
{
    public class RGetOnlineInfoResult
    {
        public ROnlineInfo[] Items { get; set; }
    }

    public class ROnlineInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public RPoint LastPosition { get; set; }
        
        /// <summary> UTC </summary>
        public DateTime DT { get; set; }

        public ROnlineState State { get; set; }
        public double Speed { get; set; }
        public double Course { get; set; }
        public string Address { get; set; }
        public Dictionary<string, object> Final { get; set; }
        public object LastCoords { get; set; }
        public DateTime? _LastCoords;
        public object LastData { get; set; }
        public DateTime? _LastData;

        public ROnlineInfo() { }
        public ROnlineInfo([NotNull] ROnlineInfo from, [CanBeNull] Dictionary<string, object> newFinal)
        {
            ID = from.ID;
            Name = from.Name;
            LastPosition = from.LastPosition;
            DT = from.DT;
            State = from.State;
            Speed = from.Speed;
            Course = from.Course;
            Address = from.Address;
            LastCoords = from.LastCoords;
            _LastCoords = from._LastCoords;
            LastData = from.LastData;
            _LastData = from._LastData;
            
            Final = newFinal;
        }
        
#if DEBUG
        public override string ToString() => $"{Name}: LastPosition={(LastPosition?.ToString() ?? "")}" +
                                             $", DT={DT.ToString("dd.MM.yyyy HH:mm")}" +
                                             $", State={State}" +
                                             $", Speed={Speed.ToString("F1", CultureInfo.InvariantCulture)}";
#endif
    }

    public enum ROnlineState
    {
        Park = 0,
        Move = 1,
        Flight = 2
    }
}
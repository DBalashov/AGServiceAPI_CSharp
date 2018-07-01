using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AutoGRAPHService
{
    public class RTripTableValues : RBaseParameterValue
    {
        public object[] Values { get; set; }

#if DEBUG
        public override string ToString() => $"{Name} ({Caption}): Values={Values.Length}";
#endif
    }

    public class RTripSummary
    {
        public RTripStage[] Stages { get; set; }
        public Dictionary<string, object> Total { get; set; }
    }

    public class RTrip
    {
        public int Index { get; set; }
        public object SD { get; set; }
        public object ED { get; set; }
        public RPoint PointStart { get; set; }
        public RPoint PointEnd { get; set; }

        public RTripStage[] Stages { get; set; }

        public Dictionary<string, object> Total { get; set; }
        public RTripArea[] Areas { get; set; }

        /// <summary> UTC </summary>
        public DateTime _SD;

        /// <summary> UTC </summary>
        public DateTime _ED;

        public RTrip() { }
        public RTrip(RTrip from, RTripStage[] newStages, Dictionary<string, object> newTotal)
        {
            Index = from.Index;
            SD = from.SD;
            ED = from.ED;
            PointStart = from.PointStart;
            PointEnd = from.PointEnd;

            _SD = from._SD;
            _ED = from._ED;

            Stages = newStages;
            Total = newTotal;
            Areas = from.Areas;
        }
        
#if DEBUG
        public override string ToString() => $"{Index}: {SD}  -  {ED}, Stages={Stages.Length}";
#endif
    }

    public class RTripArea
    {
        public byte ColorR { get; set; }
        public byte ColorG { get; set; }
        public byte ColorB { get; set; }
        public double[][][] Polygons { get; set; }
    }

    public class RTripsSummary
    {
        public Dictionary<string, object> Total { get; set; }
    }

    public class RTrips
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Serial { get; set; }
        public string VRN { get; set; }
        public DateTime SD { get; set; }
        public DateTime ED { get; set; }
        public RTrip[] Trips { get; set; }
        public Dictionary<string, object> Total { get; set; }

        public RPoint LastPosition { get; set; }
        
        public object LastCoords { get; set; }
        public DateTime? _LastCoords;
        
        public object LastData { get; set; }
        public DateTime? _LastData;

        public TimeSpan processingTime;

        public RTrips() { }
        public RTrips([NotNull] RTrips from, [NotNull] RTrip[] newTrips, Dictionary<string, object> newTotal)
        {
            ID = from.ID;
            Name = from.Name;
            VRN = from.VRN;
            SD = from.SD;
            ED = from.ED;
            Serial = from.Serial;
            LastPosition = from.LastPosition;
            LastCoords = from.LastCoords;
            _LastCoords = from._LastCoords;            
            LastData = from.LastData;
            _LastData = from._LastData;
            
            processingTime = from.processingTime;
            
            Trips = newTrips;
            Total = newTotal;
        }
        
#if DEBUG  
        public override string ToString() => $"Trips={Trips.Length}, Total={Total.Count}";
#endif
    }

    public class RTripRequest
    {
        public Guid ID { get; set; }

        /// local time
        public DateTime SD { get; set; }

        /// local time
        public DateTime ED { get; set; }

        /// -1 if no splitting by trips (will return always 1 total trip). 0 or more - index of trip splitter
        public int tripSplitterIndex { get; set; }

        /// true, if no stage items (only total values)
        public bool onlyTotal { get; set; }

        /// if null - no area fields calculation. if !=null - must be contain ID of geofences for fields calculation
        public Guid[] areaIDs { get; set; }

        public Guid[] geofenceIDs { get; set; }

        /// <summary> if mobile checkpoints query </summary>
        public bool IsMain;
    }

    public class RTripStage
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string[] Params { get; set; }
        public ReturnType[] ParamTypes { get; set; }
        public RTripStageItem[] Items { get; set; }
        public RParameterStatus[] Statuses { get; set; }
        public Dictionary<string, object> Total { get; set; }

        public RTripStage() { }
        
        public RTripStage([NotNull] RTripStage from,
            [CanBeNull] IEnumerable<string> newParams, [CanBeNull] IEnumerable<ReturnType> newParamTypes,
            [CanBeNull] IEnumerable<RTripStageItem> newItems, [CanBeNull] Dictionary<string, object> newTotal)
        {
            Name = from.Name;
            Alias = from.Alias;
            Params = newParams?.ToArray();
            ParamTypes = newParamTypes?.ToArray();
            Items = newItems?.ToArray();
            Statuses = from.Statuses;
            Total = newTotal;
        }
        
#if DEBUG
        public override string ToString() => $"{Name} [{Alias}]: Params={Params.Length}, Items={Items.Length}, Statuses={(Statuses?.Length.ToString() ?? "null")}";
#endif
    }

    public class RTripStageItem
    {
        public int Index { get; set; }
        public object SD { get; set; }
        public object ED { get; set; }

        public int Status { get; set; }
        public Guid StatusID { get; set; }
        public Guid[] StatusIDs { get; set; }

        public RPoint StartPoint { get; set; }
        public RPoint EndPoint { get; set; }

        public string Caption { get; set; }
        public object[] Values { get; set; }

        /// <summary> UTC </summary>
        public DateTime _SD;

        /// <summary> UTC </summary>
        public DateTime _ED;
        
        public RTripStageItem() { }

        public RTripStageItem([NotNull] RTripStageItem from, [NotNull] object[] newValues)
        {
            Index = from.Index;
            SD = from.SD;
            ED = from.ED;
            Status = from.Status;
            StatusID = from.StatusID;
            StatusIDs = from.StatusIDs;

            StartPoint = from.StartPoint;
            EndPoint = from.EndPoint;
            Caption = from.Caption;

            _SD = from._SD;
            _ED = from._ED;

            Values = newValues;            
        }

#if DEBUG
        public override string ToString() => $"{Index}: {SD}  -  {ED}, Caption={Caption}, Status={Status}, Values={Values.Length}";
#endif
    }
}
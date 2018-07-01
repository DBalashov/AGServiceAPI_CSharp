using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoGRAPHService;
using JetBrains.Annotations;

namespace AGServiceAPI
{
    public class AGServiceClientAsync : AGServiceBase
    {
        public AGServiceClientAsync([NotNull] string uri) : base(uri) { }

        [NotNull]
        public async Task Login([NotNull] string UserName, [NotNull] string Password, int? UTCOffset = null)
        {
            var arguments = new Dictionary<string, string>()
            {
                ["UserName"] = UserName,
                ["Password"] = Password
            };
            if (UTCOffset.HasValue)
                arguments.Add("UTCOffset", UTCOffset.Value.ToString());

            var response = await client.PostAsync(Uri + "Login", new FormUrlEncodedContent(arguments));
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                client.DefaultRequestHeaders.Add(HEADER_TOKEN, token);
            }
            else throw new UnauthorizedAccessException("Invalid username or password");
        }

        #region EnumXXXX
        [NotNull]
        public async Task<RSchema[]> EnumSchemas()
        {
            return await exec<RSchema[]>("EnumSchemas", new Dictionary<string, object>());
        }

        [NotNull]
        public async Task<REnumDevices> EnumDevices([NotNull] string schemaID)
        {
            return await exec<REnumDevices>("EnumDevices", new Dictionary<string, object>() { {"schemaID", schemaID} });
        }
        
        [NotNull]
        public async Task<REnumGeofences> EnumGeofences([NotNull] string schemaID)
        {
            return await exec<REnumGeofences>("EnumGeofences", new Dictionary<string, object>() { {"schemaID", schemaID} });
        }
        
        [NotNull]
        public async Task<REnumImplements> EnumImplements([NotNull] string schemaID)
        {
            return await exec<REnumImplements>("EnumImplements", new Dictionary<string, object>() { {"schemaID", schemaID} });
        }
        
        [NotNull]        
        public async Task<REnumDrivers> EnumDrivers([NotNull] string schemaID)
        {
            return await exec<REnumDrivers>("EnumDrivers", new Dictionary<string, object>() { {"schemaID", schemaID} });
        }
        
        [NotNull]        
        public async Task<RDeviceStatus[]> EnumStatuses([NotNull] string schemaID)
        {
            return await exec<RDeviceStatus[]>("EnumStatuses", new Dictionary<string, object>() { {"schemaID", schemaID} });
        }

        [NotNull]
        public async Task<Dictionary<Guid, RParameters>> EnumParameters([NotNull] string schemaID, [NotNull] Guid[] IDs)
        {
            return await exec<Dictionary<Guid, RParameters>>("EnumParameters", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs}
            });
        }
        #endregion

        #region GetTrips
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTrips([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED,
                                                             int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTrips", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTripsTotal([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED,
                                                                  int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTripsTotal", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTripsCustom([NotNull] string schemaID, [NotNull] Guid[] IDs, [NotNull] Guid[] geofenceIDs, DateTime SD, DateTime ED,
                                                                   int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTripsCustom", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"geofencesIDs", geofenceIDs},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }

        [NotNull]
        public async Task<Dictionary<Guid, RTripItemContainer>> GetTripItems([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED,
                                                                             int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTripItemContainer>>("GetTripItems", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
                
        [NotNull]
        public async Task<Dictionary<Guid, RTripTables>> GetTripTables([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED, int tripSplitterIndex, string[] onlineParams)
        {
            return await exec<Dictionary<Guid, RTripTables>>("GetTripTables", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex},
                {"onlineParams", onlineParams}
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RTripStage>> GetStage([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED, int tripSplitterIndex,
                                                                 [NotNull] string stageName, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTripStage>>("GetStage", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"stageName", stageName},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
        #endregion
        
        #region GetTripsArea / GetTripsAreaTotal
        /// <param name="areaGeofenceIDs">null if ALL geofences in schema will used, !=null if only selected geofences will used</param>
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTripsArea([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED, [CanBeNull] Guid[] areaGeofenceIDs,
                                                                 int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTripsArea", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"areaIDs", areaGeofenceIDs},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
        
        /// <param name="areaGeofenceIDs">null if ALL geofences in schema will used, !=null if only selected geofences will used</param>
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTripsAreaTotal([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED, [CanBeNull] Guid[] areaGeofenceIDs,
                                                                      int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTripsAreaTotal", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"areaIDs", areaGeofenceIDs},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }        
        #endregion
        
        /// <param name="masterID">master car ID</param>
        /// <param name="IDs">mobile checkpoints car IDs</param>
        [NotNull]
        public async Task<Dictionary<Guid, RTrips>> GetTripsMobileCheckpoints([NotNull] string schemaID, Guid masterID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED,
                                                                              int tripSplitterIndex = -1, string[] tripParams = null, string[] tripTotalParams = null)
        {
            return await exec<Dictionary<Guid, RTrips>>("GetTripsMobileCheckpoints", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", new[] {masterID}.Concat(IDs).ToArray()},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex},
                {"tripParams", tripParams},
                {"tripTotalParams", tripTotalParams}
            });
        }
        
        #region GetOnlineInfo / GetOnlineInfoAll 
        [NotNull]
        public async Task<Dictionary<Guid, ROnlineInfo>> GetOnlineInfo([NotNull] string schemaID, [NotNull] Guid[] IDs, string[] finalParams = null)
        {
            return await exec<Dictionary<Guid, ROnlineInfo>>("GetOnlineInfo", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"finalParams", finalParams}
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, ROnlineInfo>> GetOnlineInfoAll([NotNull] string schemaID, string[] finalParams = null)
        {
            return await exec<Dictionary<Guid, ROnlineInfo>>("GetOnlineInfoAll", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"finalParams", finalParams}
            });
        }
        #endregion
        
        [NotNull]
        public async Task<Dictionary<Guid, RTrackInfo[]>> GetTrack([NotNull] string schemaID, [NotNull] Guid[] IDs, DateTime SD, DateTime ED, int tripSplitterIndex = -1)
        {
            return await exec<Dictionary<Guid, RTrackInfo[]>>("GetTrack", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"SD", SD},
                {"ED", ED},
                {"tripSplitterIndex", tripSplitterIndex}
            });
        }
        
        [NotNull]
        public async Task<RDeviceInfo> GetDevicesInfo([NotNull] string schemaID)
        {
            return await exec<RDeviceInfo>("GetDevicesInfo", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
            });
        }
        
        [NotNull]
        public async Task<RRouteSegment[]> GetRoute(RouterType routerType, params RPoint[] waypoints)
        {
            return await exec<RRouteSegment[]>("GetRoute", new Dictionary<string, object>()
            {
                {"id", (int) routerType},
                {"waypoints", waypoints
                        .Select(p => p.Lat.ToString(CultureInfo.InvariantCulture) + "," + p.Lng.ToString(CultureInfo.InvariantCulture))
                        .Aggregate((acc, sel) => acc + ";" + sel)}
            });
        }
        
        #region GetProperties
        [NotNull]
        async Task<Dictionary<Guid, RProperties>> getProperties(string methodName, string schemaID, Guid[] IDs)
        {
            return await exec<Dictionary<Guid, RProperties>>(methodName, new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RProperties>> GetProperties(string schemaID, Guid[] IDs)
        {
            return await getProperties("GetProperties", schemaID, IDs);
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RProperties>> GetGFProperties(string schemaID, Guid[] IDs)
        {
            return await getProperties("GetGFProperties", schemaID, IDs);
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, RProperties>> GetDriverProperties(string schemaID, Guid[] IDs)
        {
            return await getProperties("GetDriverProperties", schemaID, IDs);
        }
        #endregion

        #region GetProperty
        [NotNull]
        async Task<Dictionary<Guid, object>> getProperty(string methodName, string schemaID, Guid[] IDs, [NotNull] string propertyName)
        {
            return await exec<Dictionary<Guid, object>>(methodName, new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
                {"propertyName", propertyName}
            });
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, object>> GetProperty(string schemaID, Guid[] IDs, [NotNull] string propertyName)
        {
            return await getProperty("GetProperty", schemaID, IDs, propertyName);
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, object>> GetGFProperty(string schemaID, Guid[] IDs, [NotNull] string propertyName)
        {
            return await getProperty("GetGFProperty", schemaID, IDs, propertyName);
        }
        
        [NotNull]
        public async Task<Dictionary<Guid, object>> GetDriverProperty(string schemaID, Guid[] IDs, [NotNull] string propertyName)
        {
            return await getProperty("GetDriverProperty", schemaID, IDs, propertyName);
        }
        #endregion
        
        [NotNull]
        public async Task<Dictionary<Guid, RGeofence>> GetGeofences([NotNull] string schemaID, [NotNull] Guid[] IDs)
        {
            return await exec<Dictionary<Guid, RGeofence>>("GetGeofences", new Dictionary<string, object>()
            {
                {"schemaID", schemaID},
                {"IDs", IDs},
            });
        }
    }
}
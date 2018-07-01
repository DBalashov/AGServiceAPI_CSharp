using System;
using System.Linq;
using AGServiceAPI;
using AutoGRAPHService;

namespace Examples.Core
{
    class Program
    {
        static int FIRST_N = 10;
        
        static void Main(string[] args)
        {
            try
            {
                var c = new AGServiceClientAsync("http://localhost/Dev/ServiceJSON");
                c.Login("demo", "demo").Wait();

                RSchema firstSchema = null;
                run("EnumSchemas",
                    () =>
                    {
                        var schemas = c.EnumSchemas().Result;
                        foreach (var item in schemas)
                            Console.WriteLine("{0}: {1}", item.ID, item.Name);
                        firstSchema = schemas[0];
                    });

                Guid[] firstNDevices = null;
                run("EnumDevices",
                    () =>
                    {
                        var result = c.EnumDevices(firstSchema.ID).Result;
                        firstNDevices = result.Items.Where(p => p.Serial == 9999999 || p.Serial == 9999998).Select(p => p.ID).ToArray();
                        // firstNDevices = result.Items.Take(FIRST_N).Select(p => p.ID).ToArray();
                        
                        Console.WriteLine("Devices: {0}", result.Items.Length);
                        Console.WriteLine("Groups : {0}", result.Groups.Length);
                        
                        Console.WriteLine("\nFirst {0} items", FIRST_N);
                        foreach (var item in result.Items.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2} (image={3}, serial={4})", item.ID, item.ParentID, item.Name, item.Image, item.Serial);
                        Console.WriteLine("\nFirst {0} groups", FIRST_N);
                        foreach (var item in result.Groups.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2}", item.ID, item.ParentID, item.Name);
                    });
                
                run("EnumDrivers",
                    () =>
                    {
                        var result = c.EnumDrivers(firstSchema.ID).Result;
                        Console.WriteLine("Drivers: {0}", result.Items.Length);
                        Console.WriteLine("Groups : {0}", result.Groups.Length);
                        
                        Console.WriteLine("\nFirst {0} items", FIRST_N);
                        foreach (var item in result.Items.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2} (image={3}, driverId={4})", item.ID, item.ParentID, item.Name, item.Image, item.DriverID);
                        Console.WriteLine("\nFirst {0} groups", FIRST_N);
                        foreach (var item in result.Groups.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2}", item.ID, item.ParentID, item.Name);
                    });
                
                run("EnumImplements",
                    () =>
                    {
                        var result = c.EnumImplements(firstSchema.ID).Result;
                        Console.WriteLine("Implements: {0}", result.Items.Length);
                        Console.WriteLine("Groups    : {0}", result.Groups.Length);
                        
                        Console.WriteLine("\nFirst {0} items", FIRST_N);
                        foreach (var item in result.Items.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2} (image={3})", item.ID, item.ParentID, item.Name, item.Image);
                        Console.WriteLine("\nFirst {0} groups", FIRST_N);
                        foreach (var item in result.Groups.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2}", item.ID, item.ParentID, item.Name);
                    });

                Guid[] firstNGeofences = null;
                run("EnumGeofences",
                    () =>
                    {
                        var result = c.EnumGeofences(firstSchema.ID).Result;
                        firstNGeofences = result.Items.Take(FIRST_N).Select(p => p.ID).ToArray();
                        Console.WriteLine("Geofences: {0}", result.Items.Length);
                        Console.WriteLine("Groups   : {0}", result.Groups.Length);
                        
                        Console.WriteLine("\nFirst {0} items", FIRST_N);
                        foreach (var item in result.Items.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2} (image={3})", item.ID, item.ParentID, item.Name, item.Image);
                        Console.WriteLine("\nFirst {0} groups", FIRST_N);
                        foreach (var item in result.Groups.Take(FIRST_N))
                            Console.WriteLine("{0} (parentId={1}): {2}", item.ID, item.ParentID, item.Name);
                    });
                
                run("EnumStatuses",
                    () =>
                    {
                        var result = c.EnumStatuses(firstSchema.ID).Result;
                        Console.WriteLine("Statuses: {0}", result.Length);
                        foreach (var item in result)
                            Console.WriteLine("{0} (Enabled={1}): {2} (image={3})", item.ID, item.Enabled, item.Name, item.ImageName);
                    });
                
                run("GetDevicesInfo",
                    () =>
                    {
                        var result = c.GetDevicesInfo(firstSchema.ID).Result;
                        Console.WriteLine("Stages: {0}", result.Stages.Length);
                        foreach (var item in result.Stages)
                            Console.WriteLine("{0,-12}: {1,-16}, Parameter={2,-16}, IsGroup={3}, Image={4}",
                                item.Name, item.Caption, item.Parameter, item.IsGroup, item.Image);
                    });

                string[] onlineParams = null;
                run("EnumParameters",
                    () =>
                    {
                        var result = c.EnumParameters(firstSchema.ID, firstNDevices).Result;
                        Console.WriteLine("\nFirst {0} items", FIRST_N);
                        onlineParams = result.First().Value.OnlineParams.Select(p => p.Name).ToArray();
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1,-32}, OnlineParams={2,3}, TripParams={3,3}, FinalParams={4,3}",
                                item.Key, item.Value.Name, item.Value.OnlineParams.Length, item.Value.TripsParams.Length, item.Value.FinalParams.Length);
                    });
                    
                #region GetTrips
                run("GetTrips",
                    () =>
                    {
                        var result = c.GetTrips(firstSchema.ID, firstNDevices, DateTime.Now.AddDays(-3).Date, DateTime.Now, 0).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                        {
                            Console.WriteLine("{0}: {1,-32}, Trips={2,3}", item.Key, item.Value.Name, item.Value.Trips.Length);
                            foreach (var trip in item.Value.Trips)
                            {
                                Console.WriteLine("  {0}: {1} - {2}, Move duration={3}, distance={4}, Point start/end: {5}, {6}",
                                    trip.Index, trip.SD, trip.ED, trip.Total["MoveDuration"], trip.Total["TotalDistance"], trip.PointStart, trip.PointEnd);
                                
                                foreach (var stage in trip.Stages)
                                    Console.WriteLine("     {0,-16}, Params: {1,2}, Total params: {2,2}", stage.Name, stage.Params.Length, stage.Total.Count);
                            }
                        }
                    });
                
                run("GetTripItems",
                    () =>
                    {
                        var result = c.GetTripItems(firstSchema.ID, firstNDevices, DateTime.Now.AddDays(-3).Date, DateTime.Now, 0).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1,-32}, Params={2,3}, Items={3,3}", item.Key, item.Value.Name, item.Value.Params.Length, item.Value.Items.Length);
                    });
                
                run("GetTripTables",
                    () =>
                    {
                        var result = c.GetTripTables(firstSchema.ID, firstNDevices, DateTime.Now.AddDays(-3).Date, DateTime.Now, 0, onlineParams).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                        {
                            Console.WriteLine("{0}: {1,-32}, Trips={2,3}", item.Key, item.Value.Name, item.Value.Trips.Length);
                            foreach (var trip in item.Value.Trips)
                            {
                                Console.WriteLine("  {0}: {1} - {2}: Params={3}, Param values count={4}",
                                    trip.Index, trip.SD, trip.ED, trip.Values.Length, trip.Values[0].Values.Length);
                            }
                        }
                    });
                #endregion
                
                run("GetStage",
                    () =>
                    {
                        var result = c.GetStage(firstSchema.ID, firstNDevices, DateTime.Now.AddDays(-3).Date, DateTime.Now, 0, "Motion").Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1,-32}, Items={2,3}, Statuses={3,3}", item.Key, item.Value.Name, item.Value.Items.Length, item.Value.Statuses?.Length ?? 0);
                    });
                
                run("GetOnlineInfo",
                    () =>
                    {
                        var result = c.GetOnlineInfo(firstSchema.ID, firstNDevices).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1,-32}, LastDateTime={2,3}, Speed={3}, State={4}, Coords={5}, Address={6}",
                                item.Key, item.Value?.Name ?? "NO DATA", item.Value?.DT, item.Value?.Speed, item.Value?.State, item.Value?.LastPosition, item.Value?.Address);
                    });
                
                run("GetOnlineInfoAll",
                    () =>
                    {
                        var result = c.GetOnlineInfoAll(firstSchema.ID).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1,-32}, LastDateTime={2,3}, Speed={3}, State={4}, Coords={5}, Address={6}",
                                item.Key, item.Value?.Name ?? "NO DATA", item.Value?.DT, item.Value?.Speed, item.Value?.State, item.Value?.LastPosition, item.Value?.Address);
                    });
                
                run("GetTrack",
                    () =>
                    {
                        var result = c.GetTrack(firstSchema.ID, firstNDevices, DateTime.Now.AddDays(-3).Date, DateTime.Now, 0).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                        {
                            Console.WriteLine("{0}: Trips={1}", item.Key, item.Value != null ? item.Value.Length.ToString() : "NO DATA");
                            if(item.Value != null)
                                foreach (var trip in item.Value)
                                    Console.WriteLine("  {0}: Points={1,-5}, {2} - {3}", trip.Index, trip.DT.Length, trip.DT.First(), trip.DT.Last());
                        }
                    });

                run("GetRoute",
                    () =>
                    {
                        var result = c.GetRoute(RouterType.Google,
                            new RPoint(52.517037, 13.388860),
                            new RPoint(52.529407, 13.397634),
                            new RPoint(52.523219, 13.428555),
                            new RPoint(52.47232480481697, 13.447952270507812),
                            new RPoint(52.5314697078057, 13.333969116210938)).Result;
                        Console.WriteLine("Segments: {0}", result.Length);
                        for (var i = 0; i < result.Length; i++)
                        {
                            var item = result[i];
                            Console.WriteLine("[{0,2}] Address from/to={1}/{2}, Distance/Duration={3}/{4}, points: {5}", i, item.AddressFrom, item.AddressTo, item.Distance, item.Duration, item.Points.Length);

                        }
                    });
                
                run("GetProperties",
                    () =>
                    {
                        var result = c.GetProperties(firstSchema.ID, firstNDevices).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                        {
                            Console.WriteLine("{0}: {1,-32}, Props={2}", item.Key, item.Value.Name, item.Value.Properties.Count);
                            foreach (var prop in item.Value.Properties)
                                Console.WriteLine("  {0,-20} ({1,-10}): {2}", prop.Key, prop.Value != null ? prop.Value.GetType().Name : "NULL", prop.Value);
                        }
                    });
                
                run("GetProperty",
                    () =>
                    {
                        var result = c.GetProperty(firstSchema.ID, firstNDevices, "VehicleRegNumber").Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                            Console.WriteLine("{0}: {1}", item.Key, item.Value);
                    });

                run("GetGeofences",
                    () =>
                    {
                        var result = c.GetGeofences(firstSchema.ID, firstNGeofences).Result;
                        Console.WriteLine("Items: {0}", result.Count);
                        foreach (var item in result)
                        {
                            Console.WriteLine("{0}: {1}, Image={2}", item.Key, item.Value, item.Value.ImageName);
                            for (var i = 0; i < item.Value.Lat.Length; i++)
                                Console.WriteLine("  {0,2}: {1} / {2}", i, item.Value.Lat[i], item.Value.Lng[i]);
                        }
                    });
                
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("All methods completed");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static void run(string caption, Action action)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n{0} ---------------------------------------------------------------------------", caption);
            Console.ForegroundColor = prevColor;

            action();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace FloodApp
{
    public static class BusinessLogic
    {
        private static string sourceFolder = Utility.ReadSetting("sourceFolderPath");
        private static string dataFileFilter = Utility.ReadSetting("dataFileFilter");
        private static string deviceFileFilter = Utility.ReadSetting("deviceFileFilter");
      /// <summary>
      /// This is used to process results for the Data files
      /// </summary>
        public static void ProcessResults()
        {
            try
            {
                DirectoryInfo diSourceFolder = new DirectoryInfo(sourceFolder);

                List<Device> deviceRecords = GetDeviceRecords(sourceFolder + @"\" + deviceFileFilter);


                List<DeviceRainfall> lstRainfall = new List<DeviceRainfall>();
                DateTime recordCurrenTime, last4hoursTime;
                int totalRainfallrecords = 0;
                int totalRainfall = 0;
                double averageRainfall = 0;

                foreach (var sourceFileInfo in diSourceFolder.GetFiles(dataFileFilter))
                {

                    lstRainfall = GetDeviceRainfallRecords(sourceFileInfo.FullName);
                    recordCurrenTime = lstRainfall.OrderByDescending(t => t.Time).ToList()[0].Time;
                    last4hoursTime = recordCurrenTime.AddMinutes(-240);



                    //Filter by Device Id
                    foreach (var record in deviceRecords)
                    {
                        var filteredResult = lstRainfall.OfType<DeviceRainfall>().Where(s => s.ID == record.ID && s.Time > last4hoursTime && s.Time < recordCurrenTime).ToList();

                        Console.WriteLine();
                        Console.WriteLine("Device Name: " + record.Name);
                        Console.WriteLine("Location: " + record.Location);
                        //Check if any Rainfall is greater then 30mm
                        if (filteredResult.Where(q => q.Rainfall > 30).Count() > 0)
                        {
                            Console.WriteLine("Threshold: " + Threshold.Red.ToString());
                        }
                        else
                        {

                            totalRainfallrecords = filteredResult.Count;
                            if (totalRainfallrecords > 0)
                            {
                                totalRainfall = filteredResult.Sum(item => item.Rainfall);
                                averageRainfall = totalRainfall / totalRainfallrecords;

                                Console.WriteLine("Threshold: " + GetThreshold(averageRainfall).ToString());
                            }
                            else
                            {
                                Console.WriteLine("Threshold: " + Threshold.None.ToString());
                            }
                        }

                        Console.WriteLine("".PadRight(24, '-'));





                    }
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();



                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This is used to get Threshold values
        /// </summary>
        /// <param name="averageRainfall"></param>
        /// <returns></returns>
        private static Threshold GetThreshold(double averageRainfall)
        {
            try
            {
                if (averageRainfall < 10)
                {
                    return Threshold.Green;
                }
                else if (averageRainfall < 15)

                {
                    return Threshold.Amber;
                }
                else if (averageRainfall >= 15)
                {
                    return Threshold.Red;
                }
                else
                {
                    return Threshold.None;
                }

            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        /// <summary>
        /// This is used to get the Device Reocords
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static List<Device> GetDeviceRecords(string filepath)
        {
            List<Device> deviceRecords;
            try
            {
               
                using (var streamReader = new StreamReader(filepath))
                {
                    using (var csvreader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        deviceRecords = csvreader.GetRecords<Device>().ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ;
            }

            return deviceRecords;
        }

        /// <summary>
        /// This is used to reuturn the list of Device Rainfall
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        private static List<DeviceRainfall> GetDeviceRainfallRecords(string fileFullName)
        {
            List<DeviceRainfall> lstDeviceRainfallRecords = new List<DeviceRainfall>();
            try
            {
                using (var reader = new StreamReader(fileFullName))
                // using (var reader = new StreamReader(@"C:\Pandhi\personal\Interfuze\Data1.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<DeviceRainfall>();


                    lstDeviceRainfallRecords.AddRange(records);


                }
            }
            catch (Exception ex)
            {
                throw ;
            }
            return lstDeviceRainfallRecords;
        }

    }
}

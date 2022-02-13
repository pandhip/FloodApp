using System;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace FloodApp
{
    public class DeviceRainfall
    {
        [Name("Device ID")]
        public int? ID { get; set; }
        [Name("Time")]
        public DateTime Time { get; set; }
        [Name("Rainfall")]
        public int Rainfall { get; set; }

    }
}

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace FloodApp
{
    public class Device
    {
        [Name("Device ID")]
        public int? ID { get; set; }
        [Name("Device Name")]
        public string Name { get; set; }
        [Name("Location")]
        public string Location { get; set; }

    }
}

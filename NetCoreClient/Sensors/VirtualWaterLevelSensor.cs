using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualWaterLevelSensor : IWaterLevelSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualWaterLevelSensor()
        {
            Random = new Random();  
        }

        public int WaterLevel()
        {
            return new WaterLevel(Random.Next(2000)).Value;
        }
         
        public string ToJson()
        {
            return JsonSerializer.Serialize(WaterLevel());
        }

        public string GetSlug()
        {
            return "WaterLevel";
        }
    }
}

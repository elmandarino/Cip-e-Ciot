using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualWaterPressionSensor : IWaterPressionSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualWaterPressionSensor()
        {
            Random = new Random();
        }

        public int WaterPression()
        {
            return new WaterPression(Random.Next(3)).Value;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(WaterPression());
        }

        public string GetSlug()
        {
            return "WaterPressure";
        }
    }
}

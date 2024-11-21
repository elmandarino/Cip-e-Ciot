//using NetCoreClient.ValueObjects;
//using System.Text.Json;

//namespace NetCoreClient.Sensors
//{
//    class VirtualLightSensor : ILightSensorInterface, ISensorInterface
//    {
//        private readonly Random Random;

//        public VirtualLightSensor()
//        {
//            //Random = new Random();
//        }ì

//        public int Light()
//        {
//            return new WaterLevel(Random.Next(2000)).Value;
//        }

//        public string ToJson()
//        {
//            return JsonSerializer.Serialize(WaterLevel());
//        }

//        public string GetSlug()
//        {
//            return "WaterLevel";
//        }
//    }
//}

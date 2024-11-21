using NetCoreClient.Sensors;
using NetCoreClient.Protocols;

// define sensors
List<ISensorInterface> sensors = new();
sensors.Add(new VirtualWaterTempSensor());
sensors.Add(new VirtualWaterLevelSensor());
sensors.Add(new VirtualWaterPressionSensor());

// define protocol
//ProtocolInterface protocol = new Http("http://1ea7-185-122-225-105.ngrok-free.app/water_coolers/123");
ProtocolInterface protocol = new Mqtt("test.mosquitto.org");

// send data to server
while (true)
{
    foreach (ISensorInterface sensor in sensors)
    {
        var sensorValue = sensor.ToJson();

        protocol.Send(sensorValue, sensor.GetSlug());

        Console.WriteLine("Dati: " + sensorValue);

        Thread.Sleep(1000);
    }

}
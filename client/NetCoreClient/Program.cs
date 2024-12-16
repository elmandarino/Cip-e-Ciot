using NetCoreClient.Sensors;
using NetCoreClient.Protocols;
using System;
using System.Collections.Generic;
using System.Threading;
using RabbitMQ.Client;

// define sensors
List<ISensorInterface> sensors = new();
sensors.Add(new VirtualWaterTempSensor());
sensors.Add(new VirtualWaterLevelSensor());
sensors.Add(new VirtualWaterPressionSensor());

// define protocol
string endpoint = " amqps://nkvkqygh:pn2t9KPbYjkAlf4UmYa6DerstlMppFDA@rat.rmq2.cloudamqp.com/nkvkqygh";

var protocol = new NetCoreClient.Protocols.RabbitMQ(endpoint);

// send data to server
while (true)
{
    foreach (ISensorInterface sensor in sensors)
    {
        var sensorValue = sensor.ToJson();

        protocol.Send(sensorValue, sensor.GetSlug());

        Console.WriteLine("Dati: " + sensorValue + ", " + "Nome Sensore: " + sensor.GetSlug());

        Thread.Sleep(1000);
    }
}

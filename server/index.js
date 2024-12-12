var restify = require('restify');
var amqp = require('amqplib/callback_api');

var server = restify.createServer();
server.use(restify.plugins.bodyParser());

// RabbitMQ setup
const EXCHANGE_NAME = 'exchange_water_coolers';
const QUEUE_NAME = 'water_coolers';
let coolersData = {}; // Store the data we receive from RabbitMQ

// Connect to RabbitMQ and set up consumer
amqp.connect('amqp://localhost', function (error0, connection) {
    if (error0) {
        throw error0;
    }
    connection.createChannel(function (error1, channel) {
        if (error1) {
            throw error1;
        }

        channel.assertExchange(EXCHANGE_NAME, 'topic', { durable: false });
        channel.assertQueue(QUEUE_NAME, { durable: false });

        // Bind the queue to the exchange with routing key
        channel.bindQueue(QUEUE_NAME, EXCHANGE_NAME, 'exchange_water_coolers');

        console.log("Waiting for messages in %s", QUEUE_NAME);

        // Consume messages
        channel.consume(QUEUE_NAME, function (msg) {
            if (msg.content) {
                let message = msg.content.toString();
                let routingKey = msg.fields.routingKey;
                let sensor = routingKey.split('.').pop(); // Assuming the sensor is the last part of the routing key

                // Save the message under the specific sensor id
                coolersData[sensor] = message;

                console.log("Received: '%s' with routingKey: '%s'", message, routingKey);
            }
        }, {
            noAck: true
        });
    });
});

// REST API Routes

// Get all coolers
server.get('/water_coolers', function (req, res, next) {
    res.send({ coolers: Object.keys(coolersData) });
    return next();
});

// Get data for a specific cooler
server.get('/water_coolers/:id', function (req, res, next) {
    const coolerId = req.params['id'];
    const coolerData = coolersData[coolerId];

    if (coolerData) {
        res.send(`Current values for cooler ${coolerId}: ${coolerData}`);
    } else {
        res.send(`No data found for cooler ${coolerId}`);
    }

    return next();
});

// POST to receive data (could also be used for other data sources)
server.post('/water_coolers/:id', function (req, res, next) {
    const coolerId = req.params['id'];
    const receivedData = req.body;

    // Store data received in REST API
    coolersData[coolerId] = receivedData;

    res.send(`Data received from cooler ${coolerId}: ${JSON.stringify(receivedData)}`);
    console.log(`Received data for cooler ${coolerId}:`, receivedData);

    return next();
});

server.listen(8011, function () {
    console.log('%s listening at %s', server.name, server.url);
});

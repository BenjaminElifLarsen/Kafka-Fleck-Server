const express = require('express')
const app = express()
const port = 8090
const { Kafka } = require('kafkajs')

const kafka = new Kafka({
    clientId: 'pos-bel',
    brokers: ['172.16.250.14:9092', '172.16.250.13:9092', '172.16.250.15:9092', '172.16.250.16:9092', '172.16.250.17:9092', '172.16.250.18:9092', '172.16.250.19:9092', '172.16.250.20:9092'],
})
const producer = kafka.producer()

app.use(function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    res.header('Access-Control-Allow-Methods', 'PUT, POST, GET, DELETE, OPTIONS');
    next();
})
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// /opt/kafka/bin# ./kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic pos-datalake --from-beginning
app.post('/', async (req, res) => {
    await producer.connect()
    await producer.send({
        topic: 'pos-datalake',
        messages: [
            { value: req.body.data },
        ],
    })
    console.log(req.body.data)
    res.send('OK')
    await producer.disconnect()
})


//await producer.disconnect()

app.listen(port, () => {
    console.log(`Example app listening on port ${port}`)
})

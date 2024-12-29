Deni Humblla
Pietro Ciot

Progetto
Il nostro progetto tratta di una casetta dell'acqua che va ad utilizzare vari sensori per monitorare i vari dati ricevuti durante il suo tempo di attività.
I dati che la casetta va a ricavare tramite i sensori sono:
-Temperatura dell'acqua
-Livello dell'acqua
-Pressione dell'acqua

Architettura
Il sistema è suddiviso in due parti:
-Client: Raccoglie i dati dai sensori durante il suo periodo di attività e li invia al server.
-Server: Riceve i dati inviati dal client, li elabora e li memorizza in un database.

Suddivisione dei ruoli:
-Deni: parte client, sviluppo codice per la creazione di sensori, la ricezione dei dati e l'invio degli stessi al server tramite i protocolli:
  -http: utilizzo di Ngrock per l'invio di dati 
  -mqtt: broker Mosquitto per l'invio di dati
  -Amqp: RabbitMQ per l'invio dei dati

-Pietro: parte server, sviluppo codice per la ricezione dei dati inviati dal client tramite i protocolli:
  -http: in cui abbiamo utilizzato Ngrock (un reverse proxy) dove è possibile esporre un server locale tramite un tunnel sicuro
  -mqtt: in cui abbiamo usato Mosquitto come broker per l'invio e la ricezione di dati
  -Amqp: in cui abbiamo utilizzato RabbitMQ per l'invio e la ricezione di dati 

tutti i dati inviati tramite i diversi protocolli vengono poi salvati in un database SQL



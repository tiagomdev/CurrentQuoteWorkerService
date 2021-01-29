# CurrentQuoteWorkerService

## Start on RabbitMQ
### Start Container
```sh
$ docker run -d --hostname rabbit-server --name rabbit-svc -p 15672:15672 -p 5672:5672 -p 25676:25676 rabbitmq:3-management
```

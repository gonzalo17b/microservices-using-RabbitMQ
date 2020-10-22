# Introducción a RabbitMQ

This tiny project is just a first try of building Microservices using RabbitMQ.

Development is very simple. We have a rabbit den and you can check the den and its members, review each rabbit, or add a new member.

To do this, two microservices have been created that are APIS in NET Core 3.1 independently that communicate with each other through RabbitMQ.

The services as you can see are very basic since we basically wanted to test how they communicate with each other through RabbitMQ.

## RabbitMQ Setup:
Go to https://www.rabbitmq.com/download.html

You will have to install RabbitMQ and earlang (https://www.rabbitmq.com/which-erlang.html)

By default RabbitMQ gets up at: http://localhost:15672/ 

    User: guest
    Password: guest

The console is located:
*C:\ProgramFiles\RabbitMQServer\rabbitmq_server-3.8.9\sbin*

**COMMANDS:**
```
To enable dashboard:
rabbitmq-plugins enable rabbitmq_management

Start:
rabbitmqctl start_app

To turn off:
rabbitmqctl stop_app

To RESET COMPLETELY:
rabbitmqctl stop_app
rabbitmqctl reset
rabbitmqctl start_app

```
The nuguet used in this project is RabbitMQ.Client.

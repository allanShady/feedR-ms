## 💻 FeedR ms
**Sample** rather **simple** .NET6 **microservices** solution which acts as the data aggregator for the different feeds.
The **purpose** of this solution is to **explore** the latest framework and **play** with different patterns, tools & libraries that can be useful when building **distributed applications**.

The overall repository structure consists of the following projects located under `src` directory:

- **Gateway** - API gateway providing a public facade for the underlying, internal services
- **Aggregator** - core service responsible for aggregating the data from different feeds
- **Notifier** - supporting service responsible for sending the notifications
- **Feeds**
  - **News** - sample feed providing the worldwide news
  - **Quotes** - sample feed providing the quotes (e.g. currency)
  - **Weather** - sample feed providing the weather related data

## 🚀 Techs

- [Linux maybe](https://ubuntu.com/)
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://docs.docker.com/get-docker)
- [Yarp](https://microsoft.github.io/reverse-proxy/index.html)
- [PM2](https://pm2.keymetrics.io/)
- [Apache Pulsar](https://pulsar.apache.org/)
- [Redis](https://redis.io/)

> To start the infrastructure via Docker, type the following command 
```shell 
  docker-compose -f compose/infrastructure.yml up -d
```

> **To un the services, type the following command** 
```bash 
  docker-compose -f compose/services.yml up -d
```

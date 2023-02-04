# Play.Api Documentation

## Table of Contents
- [Introduction](#Introduction)
- [Database Design](#Database-Design)
- [Development Setup](#Development-Setup)
- [Testing](#Testing)
- [Deployment](#Deployment)
- [Logging](#Logging)




# Introduction

### What is Play.Api?
> The Play.Api is a monolithic ASP.NET Web Api that integrates multiple Playsystems services into a single Api

### What services are integrated?
> The Play.Api integrates the following services:
> - EDI (Electronic Data Interchange) - Used by customers via the web portal to send documents (app.playsystems.io)
> - User management - On premise user management service to allow for JWT authentication for app-manage web portals.
> - 20i integration - Used to integrate with 20i webhosting for managing hosting packages and customers via the management portal.
> - Whmcs integration - Used to integrate with Whmcs for managing hosting packages and customers via the management portal.
> - Pylon integration - Used to integrate with Pylon for performing CRM related operations via the management portal.
> - Malwarebytes integration - Used to integrate with Malwarebytes for performing MSP related operations via the management portal.
> - EPP integration - Used to integrate with EPP(ITE FORTH) for performing domain related operations via the management portal and whmcs.
> - Uptime Kuma integration - Used to integrate with Uptime Kuma for performing uptime monitoring related operations via the management portal.

### How are the services integrated?
> All integrations with 3rd party vendors are done via seperate nuget packages built and maintaned by the playsystems team.
> These packages are uploaded to the private Github Package Registry of the playsystems organisation and are later bootstraped into the Play.Api project via nuget.

### Why not microservices?
> Microservice development has been kept aside for now as the Play.Api would need heavier infra and development support to make the use of microservices meaningful.
> Still the Play.Api is designed to be easily split into microservices in the future if the need arises, having a in memory bus in place to allow for easy communication between services which could easily be replaced by a message broker such as RabbitMQ or Kafka.

### What is the architecture of the Play.Api?
> The architecture is based upon the Clean Architecture principles and is split into 3 layers:
> - Application
> - Domain
> - Infrastructure (Data , Cross Cutting Concerns)  

> The Application layer is the entry point for all requests and is responsible for handling the request and returning the response.
> - **DDD** (Domain Driven Design) is used to model the domain and the domain layer is responsible for all business logic.
> - **CQRS** (Command Query Responsibility Segregation) is used to seperate the read and write operations of the domain.
> - **MediatR** is used to implement the CQRS pattern and to allow for easy communication between the application and domain layers.
> - **AutoMapper** is used to map the domain models to the application models and vice versa.
> - **FluentValidation** is used to validate the application models.
> - **Swagger** is used to generate the api documentation.
> - **Serilog** is used to log all requests and exceptions.
> - **JWT** is used for authentication and authorization.
> - **Entity Framework** is used to persist the domain models to the database.
> - **In memory bus** is used to allow for easy communication between services.
> - **Hangfire** is used to schedule background tasks.


# Database Design

### What database is used?
> The Play.Api uses a MSSQL express database to persist the domain models. Contexts are split into seperate files for each aggregate root and are registered in the Infrastructure layer.
> The changes on the schema are tracked via EF migrations and should not be manually edited.
> Keep in mind that in the future this should be become either an on premise MSSQL database or a cloud based MSSQL database (azure , aws , gcp).


# Development Setup

> The Play.Api is developed using Rider IDE and can be opened by opening the Play.Api.sln file.  
> That said, you are not limited to using Rider and can use any IDE of your choice. But keep in mind that the Play.Api is developed using .NET 7.0 and you will need to have the latest version of .NET installed on your machine.  
> If you take a look at the entry point project (Play.Services.Api) you will find 4 appsettings files.
> - appsettings.Development.json
> - appsettings.Staging.json
> - appsettings.Testing.json
> - appsettings.json

> There are launch profiles for each of the above appsettings files. Do not use the production appsettings file for development as it contains sensitive information.

# Testing
> XUnit is used for unit testing and should be used for testing all the application logic.
> The current test coverage is low and should definitely be improved in the future. When deploying new features or fixing bugs it's always a good idea to add a test for the new feature or bug fix.
> The Play.Api uses a seperate database for testing and the connection string is stored in the appsettings.Devlopment.json file. This should be changed to a local MSSQL database for testing.

# Deployment
> Currently the deployment workflow is as follows:
> 1. A PR is created and reviewed by the team.
> 2. The PR is merged into the main branch.
> 3. The main branch is picked up by **Jenkins** and a new docker image is built and pushed to the private docker registry.
> 4. The docker image is pulled by a **Jenkins** job that connects via SSH to the server and runs the docker image.
> 5. The docker image is run in a docker container and the container is exposed to the outside world via a reverse proxy (traefik).  
> *There are also staging pipelines setup in jenkins for testing purposes.*


# Logging
> Serilog is used for logging and the logs are fed to **Seq** for easy viewing and searching.
> When working on development logs are pushed to the console and to a local folder but not to Seq.

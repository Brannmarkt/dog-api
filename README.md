# Dog house service
This repository contains a REST API built with C# that interacts with a Microsoft SQL database table named "dogs.".
## Clone the repository
```
https://github.com/Brannmarkt/dog-api.git
```
## Overview 
The project consists of 4 parts: 
1. Application contains all interfaces and DTOs. It has reference to Domain layer 
2. Domain layer holds all entities. It doesn't depend on any other layer
3. Infrastructure layer deals with database and contains implementations of interfaces defined in the application layer. It references the application layer
4. WebApi is layer that is responsible for handling user interactions and delivering data to the user interface. It depends on application and infrastructure layers.

## Errors handling
The api handles various errors that can occur during its work. Possible cases: Invalid weigth or tail length (negative number or zero). Already existing dof with certain name. Invalid JSON in the request body.

## Handling too many requests
The api includes logic to handle the situation when there are too many requests (10 requests per 1 second).

## Used technologies 
The api was developed with the help of following tools and technologies:
* Asp.Net Core 8
* Repository Pattern 
* Code First principles
* MS SQL
* Entity Framework Core
* XUnit
* Moq
* FluentAssertion

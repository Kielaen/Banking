# Banking API with Dapr

## Project Overview

The **Banking API** is a microservices-based system designed to simulate a simple banking application. The system provides functionality to perform operations like withdrawals and event publishing. The API is built using **ASP.NET Core** and leverages **Dapr** for scalable and distributed event-driven architecture.

## Key Features

- **Event-driven architecture**: The system is designed to handle banking events like withdrawals using Dapr's pub/sub capabilities.
- **Microservices**: The API is split into multiple services such as event publishing, and pub/sub interaction.
- **Scalability and Reliability**: By using Dapr, we ensure that the system can scale efficiently across distributed services with the help of Dapr's state management, pub/sub, and service invocation patterns.

## Why Dapr?

Dapr (Distributed Application Runtime) is an open-source, event-driven runtime that simplifies the development of microservices by abstracting complex tasks and enabling seamless communication between distributed components. 

Here’s why Dapr was chosen for this project:

### 1. **Pub/Sub Model for Event-driven Communication**
In this application, we use **RabbitMQ** (Can also use Kafka) as the message broker for event publishing and consumption. Dapr provides a unified API for pub/sub operations, making it easy to send and receive events across services.

For instance, when a withdrawal event occurs, it is published to a **RabbitMQ** topic via Dapr. The receiving services can subscribe to this topic and take the appropriate action, such as updating account balances or logging the event.

### 2. **Simplified Microservices Communication**
Instead of managing the complexity of direct inter-service communication, Dapr allows the services to communicate via **service invocation**. This abstraction simplifies service-to-service communication and provides flexibility to use different protocols without tightly coupling the services together.

### 3. **State Management**
Dapr's state management feature allows us to persist state, although not used here, it is particularly useful for scenarios like account balances or transaction histories where state needs to be maintained across multiple services.

### 4. **Fault Tolerance and Reliability**
With Dapr, the system is resilient to failures. Dapr automatically retries failed message deliveries and provides built-in support for distributed tracing, making the system robust and easy to debug. It also integrates with existing tools like **Zipkin** for distributed tracing.

## How Dapr is Used in the Project

### Components
In the `components` folder, you’ll find a configuration for the Dapr **pub/sub component**:

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: withdrawalpubsub
scopes:
  - banking-api
spec:
  type: pubsub.rabbitmq
  version: v1
  metadata:
  - name: host
    value: "amqp://rabbitmq:5672"

```

# Running the Project

1. Clone the repository.
2. Build the Docker containers:
   ```bash
   docker-compose up --build
   ```
3. Access the Swagger UI at http://localhost:5001/swagger to interact with the API.
4. Publish events using the /withdrawals/publish/{numberOfEvents} endpoint.

### Technologies used:
- ASP.NET Core for building the API.
- Dapr for microservices communication and event-driven architecture.
- RabbitMQ as the message broker for pub/sub events.
- Docker for containerizing the application.
- Zipkin for distributed tracing.

### Key Points:

1. **Why Dapr**: The primary reason for using Dapr is to simplify communication between services, abstracting complexities like service invocation, pub/sub messaging, and state management in the future.
2. **Component Configuration**: The `components` section configures Dapr to connect to RabbitMQ and publish events.
3. **Event Handling**: The API exposes an endpoint for publishing withdrawal events, demonstrating how the system integrates with Dapr's pub/sub.
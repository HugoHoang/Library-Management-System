# Library Management System
## Overview

This Library Management System is a microservices-based application designed to streamline library operations. It comprises two key microservices: the Book Service and the Loan Service. Both services are developed using .NET 6/7, dockerized for containerization, and orchestrated using Kubernetes. This setup ensures scalability, high availability, and efficient resource management.
## Architecture
### Services

The system is structured into three microservices:
1. Book Service
    - Functionality: Manages book inventory, including adding, updating, and removing books.
    - Kubernetes Deployment: books-depl
    - Services:
      - books-clusterip-srv (ClusterIP): For internal cluster communication.
      - booknpservice-srv (NodePort): Exposes the service on each node's IP at a static port.

2. Loan Service
    - Functionality: Handles loan operations such as borrowing and returning books and tracking due dates. Communicates synchronously with book service using a Http client on inventory change.
    - Kubernetes Deployment: loans-depl
    - Service: loans-clusterip-srv (ClusterIP): Accessible within the cluster for internal communication.
3. User Service(Work in Progress)
    - Functionality: Manages user accounts, loans, reminders, and handles authentication/authorization.
   
### Database
- PostgreSQL Database: The system uses PostgreSQL for data persistence.
- Kubernetes Deployment: postgres-depl
- Services:
  - postgres-clusterip-srv (ClusterIP): Internal database access.
  - postgres-loadbalancer (LoadBalancer): External access configured for convenience.

### Ingress
NGINX Ingress Controller: Utilized for managing external access to the services. It routes traffic from outside the Kubernetes cluster to the service endpoints.

# Crime Reporting API
This is a simple project that allows you to track crime events. The events are stored in a MongoDB collection while officer information in a MS SQL Server database.

## How to run this project using Docker Compose on a Windows machine

### Prerequisites
1. Docker Desktop installed and running. See [Installing Docker Desktop on Windows](https://docs.docker.com/desktop/install/windows-install/)

### Procedure
1. Clone the repository to your local Windows machine.
2. In your CLI of choice change to the directory that contains the CrimeReporting.sln file.
3. Enter the following command: `docker-compose up`
4. Wait for the Docker images to build and run.
5. Now you can send requests to the Crime API at http://localhost:8080 and Law Enforcement API at http://localhost:8888
6. Swagger is also available for both of these APIs at http://localhost:8080/swagger and http://localhost:8888/swagger respectively.

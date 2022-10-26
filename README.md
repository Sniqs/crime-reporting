# Crime Reporting API
This is a simple project that allows you to track crime events. The events are stored in a MongoDB collection while the officer information is persisted in an MS SQL Server database.

## How to access a running instance of this project
* The Crime API is available at http://crime.sniqs.pl. Go to http://crime.sniqs.pl/swagger for Swagger
* The Law Enforcement API is available at http://le.sniqs.pl. Go to http://le.sniqs.pl/swagger for Swagger
* The Seq server is available at http://seq.sniqs.pl.

## How to run this project using Docker Compose on a Windows machine

### Prerequisites
1. Docker Desktop installed and running. For more information see: [Install Docker Desktop on Windows](https://docs.docker.com/desktop/install/windows-install/)

### Procedure
1. Clone the repository to your local Windows machine.
2. In your CLI of choice change to the directory that contains the `CrimeReporting.sln` file.
3. Enter the following command: `docker-compose up`
4. Wait for the Docker images to build and run.
5. Now you can send requests to the Crime API at http://localhost:8080 and Law Enforcement API at http://localhost:8888
6. Swagger is also available for both of these APIs at http://localhost:8080/swagger and http://localhost:8888/swagger, respectively.


## How to run this project on your own Azure Kubernetes Service cluster

### Prerequisites
1. Azure SQL Server database. For more information see: [Create a single database - Azure SQL Database](https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal)
2. Azure Cosmos DB for MongoDB. For more information see: [Azure Cosmos DB for MongoDB for .NET with the MongoDB driver](https://learn.microsoft.com/en-us/azure/cosmos-db/mongodb/quickstart-dotnet?tabs=azure-cli%2Cwindows)
3. Azure Container Registry. For more information see: [Deploy and use Azure Container Registry](https://learn.microsoft.com/en-us/azure/aks/tutorial-kubernetes-prepare-acr?tabs=azure-cli)
4. Azure Kubernetes Service. For more information see: [Deploy an Azure Kubernetes Service (AKS) cluster](https://learn.microsoft.com/en-us/azure/aks/tutorial-kubernetes-deploy-cluster?tabs=azure-cli)
5. Docker Desktop installed and running. For more information see: [Install Docker Desktop on Windows](https://docs.docker.com/desktop/install/windows-install/)
6. Azure CLI installed. For more information see: [How to install the Azure CLIHow to install the Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)

### Procedure
1. Clone the repository to your local machine.
2. In your CLI of choice change to the directory that contains the `CrimeReporting.sln` file.
3. Log in to Azure by issuing the following command: `az login`
4. Log in to Azure Container Registry by issuing the following command: `az acr login`
5. Issue the following commands, replacing `<acr-name>` with the name of your Azure Container Registry: 
```
docker build -t <acr-name>/crimeapi -f .\CrimeApi\Dockerfile .
docker push <acr-name>/crimeapi
docker build -t <acr-name>/lawenforcementapi -f .\LawEnforcementApi\Dockerfile .
docker push <acr-name>/lawenforcementapi
```
6. In `Azure\CR-secrets.yml` replace the value for `data.CRIME_COSMOS_DB` and `data.LAWENFORCEMENT_SQL_DB` with Base64 encoded connection strings to your Azure Cosmos DB and SQL database, respectively.
7. In `Azure\CR-CrimeAPI-depl.yml` under `spec.template.spec.containers.image` replace `crime.azurecr.io/crimeapi` with `<acr-name>/crimeapi` where `<acr-name>` is the name of your Azure Container Registry.
8. In `Azure\CR-LawEnforcementAPI-depl.yml` under `spec.template.spec.containers.image` replace `crime.azurecr.io/lawenforcementapi` with `<acr-name>/lawenforcementapi` where `<acr-name>` is the name of your Azure Container Registry.
9. In `CR-ingress.yml` under the three `spec.rules.host` nodes replace the domains with your own domains.
10. Issue the following command, replacing `<resource-group>` and `<aks-cluster>` with your resource group and aks cluster names, respectively: `az aks get-credentials --resource-group <resource-group> --name <aks-cluster>`
11. Change to the `Azure` folder and apply all the yml files by issuing the following commands in this order:
```
kubectl apply -f CR-namespace.yml
kubectl apply -f CR-ingress-controller.yml
kubectl apply -f CR-ingress.yml
kubectl apply -f CR-secrets.yml
kubectl apply -f CR-configmaps.yml
kubectl apply -f CR-CrimeAPI-depl.yml
kubectl apply -f CR-LawEnforcementAPI-depl.yml
kubectl apply -f CR-seq-depl.yml
```
12. If any of the commands fail, this may be due to some pods not yet being ready. Wait a moment and retry.
13. Using your domain provider's application redirect the domains you specified in the `CR-ingress.yml` file to the AKS ingress public IP address.
14. The APIs are now available for you to use under the specified domains.

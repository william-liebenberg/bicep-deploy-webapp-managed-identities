# Basic WebApp with Azure BICEP

This sample project demonstrates how to:

1. provision Azure resources used by a typical WebApp using [Azure BICEP](https://github.com/Azure/bicep)
2. assign roles to a managed identity (the webapp) to securely interact with a storage account
3. build and deploy (CI/CD) the WebApp using [Azure Pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/?view=azure-devops).

All the BICEP and Pipeline files are located in the `.azure` folder.

## Azure Resources

The Sample project provisions the following resources in Azure:

1. Azure App Service Plan (server)
2. Azure App Service (webapp)
3. Azure Application Insights
4. Azure KeyVault
5. Azure Storage Account (for blob storage)
6. Azure SQL Server
7. Azure SQL Database

## Infrastructure Validation Process

We run a set of tests on the BICEP files to ensure they conform to our required standards.

1. run `build.ps1` to build and validate the BICEP files into the common ARM Templates JSON files
2. run `clean.ps1` to remove all temporary files

## Deploying the BICEP Template

BICEP files are used to describe the Azure resource we require for this project and how they are related to one another.

To deploy the BICEP files

1. run `deploy.ps1` with all the required parameters

For example:

```ps
./deploy.ps1 -resourceGroup "Bicep-demo" `
    -location "australiaeast" `
    -environmentName "demo" `
    -secretApiKey "<s3cr3t>" `
    -bicepFile
    -bicepParametersFile .\main.parameters.demo.json
```

> Remember: Delete your resources if you aren't going to be using them for a while -- it will save you a few dollars every day that you aren't using it.

## Managed Identities and RBAC

In this project we make use of Managed Identities and Role-based Access Control (RBAC) to allow the WebApp to securely interact with other resources using the minimim set of operation permissions without us having to maintain a security key or connection string.

> Note: The following section is only really relevant for applications that have been deployed to Azure and for authoring the .BICEP files - not for local development.

To obtain a full list of available roles:

```ps
az role definition list --output json
```

You can also filter out individual fields when querying roles:

```ps
az role definition list --output json --query '[].{roleName:roleName, description:description, name:name}'
```

To query a specific role, add the `--name <role name>` flag

```ps
az role definition list --name "Storage Blob Data Contributor" --output json --query '[].{roleName:roleName, description:description, name:name}'
```

To get the UUID of a specific role:
> set output is `tsv` (tab separated values)

```ps
az role definition list --name "Storage Blob Data Contributor" --output tsv --query '[].{name:name}'
```

DONE!

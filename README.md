# DeploymentInfo
Simple way of obtaining deployment information from CI in an Azure App Service (ReleaseId, BuildId, CommitId, ReleaseName, RepoName)



# DeploymentInfo
Super simple ClassLibrary for Azure App Service (Linux Web Apps, Function Apps, Windows Web Apps, Web Jobs) to get the (current) deployment information

## Information available with Azure Devops CI

* DeploymentID
* BuildId
* ReleaseId
* ReleaseName
* BuildNumber
* ReleaseNumber
* CommitId
* RepoName
* RepoProvider
* SlotName

## Installation

Install the NugetPackage Sustainable.Web.DeploymentInfo

## Usage

To get the full Deployment object for the current deployment, simply do:

```csharp
var deployment = DeploymentInfo.DeploymentInfo.CurrentDeployment;
```

For a quick and dirty full assembly version with ReleaseName tagged on do this:

```csharp
using DeploymentInfo;
public class Startup {

    public void Configure() {
        //...

        var version = DeploymentInfo.DeploymentInfo.CurrentDeployment.GetFullAzureDeploymentVersion<Startup>();
        // this generic Startup reference is so we do not have to discuss the 
        // executing assembly or referencing assembly, we just take it 
        // from the current class type

        //...
    }
}
```
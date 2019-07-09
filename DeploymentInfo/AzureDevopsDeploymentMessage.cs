using Newtonsoft.Json;
using System;

namespace DeploymentInfo
{

    public partial class AzureDevopsDeploymentMessage : IDeploymentMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("commitId")]
        public string CommitId { get; set; }

        [JsonProperty("buildId")]
        public long BuildId { get; set; }

        [JsonProperty("releaseId")]
        public long ReleaseId { get; set; }

        [JsonProperty("buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty("releaseName")]
        public string ReleaseName { get; set; }

        [JsonProperty("repoProvider")]
        public string RepoProvider { get; set; }

        [JsonProperty("repoName")]
        public string RepoName { get; set; }

        [JsonProperty("collectionUrl")]
        public Uri CollectionUrl { get; set; }

        [JsonProperty("teamProject")]
        public Guid TeamProject { get; set; }

        [JsonProperty("slotName")]
        public string SlotName { get; set; }
    }

}

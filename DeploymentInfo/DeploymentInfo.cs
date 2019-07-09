using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DeploymentInfo
{
    public static class DeploymentInfo
    {


        public static readonly string DeploymentsPath = Path.Combine(Environment.ExpandEnvironmentVariables("%HOME%"), "site", "deployments");
        public static readonly string ActiveDeploymentFile = Path.Combine(DeploymentsPath, "active");

        private static string _currentDeploymentId;
        public static string CurrentDeploymentId {
            get {
                if (string.IsNullOrWhiteSpace(_currentDeploymentId) && System.IO.File.Exists(ActiveDeploymentFile))
                    _currentDeploymentId = System.IO.File.ReadAllText(ActiveDeploymentFile);

                return _currentDeploymentId;
            }
        }

        private static Deployment _currentDeployment;
        public static Deployment CurrentDeployment {
            get {
                if (_currentDeployment == null)
                    _currentDeployment = GetDeployment(CurrentDeploymentId);

                return _currentDeployment;
            }
        }

        public static Deployment GetDeployment(string deploymentId)
        {
            var deploymentStatusFile = Path.Combine(DeploymentsPath, deploymentId, "status.xml");
            if (System.IO.File.Exists(deploymentStatusFile))
            {
                var xml = System.IO.File.ReadAllText(deploymentStatusFile);
                return ParseXml<Deployment>(xml);
            }
            return null;
        }


        internal static T ParseXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(Deployment));

            using (var reader = System.Xml.XmlReader.Create(new System.IO.StringReader(xml),
                new System.Xml.XmlReaderSettings()
                {
                    ConformanceLevel = System.Xml.ConformanceLevel.Auto
                }))
            {
                var result = (T)serializer.Deserialize(reader);
                reader.Close();
                return result;
            }
        }


        internal static bool TryParseJson<T>(this string obj, out T result)
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(obj, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }

        public static string GetFullAzureDeploymentVersion<T>(this Deployment deployment)
        {

            var assemblyVersion = typeof(T).Assembly.GetName().Version.ToString();
            if (deployment == null)
                return assemblyVersion + "-local";

            if (deployment.Message is AzureDevopsDeploymentMessage azureDevopsDeploymentMessage)
                if (!string.IsNullOrWhiteSpace(azureDevopsDeploymentMessage.ReleaseName))
                {
                    return assemblyVersion + "-" + azureDevopsDeploymentMessage.ReleaseName;
                }
                else
                {
                    return assemblyVersion + "-" + azureDevopsDeploymentMessage.BuildId;
                }

            return assemblyVersion + "-unknown";
        }


    }
}

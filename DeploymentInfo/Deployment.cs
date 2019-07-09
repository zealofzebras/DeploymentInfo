using System;
using System.Xml.Serialization;

namespace DeploymentInfo
{

    [XmlRoot(ElementName = "deployment")]
    public class Deployment
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "author")]
        public string Author { get; set; }
        [XmlElement(ElementName = "deployer")]
        public string Deployer { get; set; }
        [XmlElement(ElementName = "authorEmail")]
        public string AuthorEmail { get; set; }

        private string _messageRaw;
        [XmlElement(ElementName = "message")]
        public string MessageRaw { get => _messageRaw; set {

                _messageRaw = value;

                if (DeploymentInfo.TryParseJson<AzureDevopsDeploymentMessage>(value, out var azureDevopsDeploymentMessage))
                    Message = azureDevopsDeploymentMessage;

            } }


        [XmlIgnore]
        public IDeploymentMessage Message { get; private set; }

        [XmlElement(ElementName = "progress")]
        public string Progress { get; set; }
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "statusText")]
        public string StatusText { get; set; }
        [XmlElement(ElementName = "lastSuccessEndTime")]
        public DateTime LastSuccessEndTime { get; set; }
        [XmlElement(ElementName = "receivedTime")]
        public DateTime ReceivedTime { get; set; }
        [XmlElement(ElementName = "startTime")]
        public DateTime StartTime { get; set; }
        [XmlElement(ElementName = "endTime")]
        public DateTime EndTime { get; set; }
        [XmlElement(ElementName = "complete")]
        public string CompleteRaw { get; set; }
        public bool Complete { get => bool.Parse(CompleteRaw); set => CompleteRaw = value.ToString(); }

        [XmlElement(ElementName = "is_temp")]
        public string IsTempRaw { get; set; }
        public bool IsTemp { get => bool.Parse(IsTempRaw); set => IsTempRaw = value.ToString(); }

        [XmlElement(ElementName = "is_readonly")]
        public string IsReadonlyRaw { get; set; }
        public bool IsReadonly { get => bool.Parse(IsReadonlyRaw); set => IsReadonlyRaw = value.ToString(); }

    }

}

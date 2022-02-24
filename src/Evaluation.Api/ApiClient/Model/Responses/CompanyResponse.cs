using System.Xml.Serialization;

namespace Evaluation.Api.ApiClient.Model.Responses
{
    [XmlRoot("Data")]
    public class CompanyResponse
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}

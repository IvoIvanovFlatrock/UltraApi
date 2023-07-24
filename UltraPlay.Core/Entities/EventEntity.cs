using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace UltraPlay.Core.Entities
{
	[XmlRoot(ElementName = "Event")]
	public class EventEntity : IEquatable<EventEntity>
	{
		public EventEntity() { }

		[XmlAttribute(AttributeName = "ID")]
		[Key]
		public int Id { get; set; }

		[XmlIgnore]
		public int SportId { get; set; }

		[XmlIgnore]
		[ForeignKey(nameof(SportId))]
		public SportEntity Sport { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "IsLive")]
		public bool IsLive { get; set; }

		[XmlAttribute(AttributeName = "CategoryID")]
		public int CategoryID { get; set; }

		[XmlElement(ElementName = "Match")]
		public List<MatchEntity> Matches { get; set; }

		public bool Equals(EventEntity? other)
		{
			if (other != null && this.Id == other.Id
				&& this.Name == other.Name
				&& this.IsLive == other.IsLive
				&& this.Name == other.Name
				&& this.CategoryID == other.CategoryID) return true;
			return false;
		}
	}
}

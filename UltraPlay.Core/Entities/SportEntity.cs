using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace UltraPlay.Core.Entities
{
	[XmlRoot(ElementName = "Sport")]
	public class SportEntity : IEquatable<SportEntity>
	{
		public SportEntity() { }

		[XmlAttribute(AttributeName = "ID")]
		[Key]
		public int Id { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "Event")]
		public List<EventEntity> Events { get; set; }

		public bool Equals(SportEntity? other)
		{
			if (other != null && this.Id == other.Id && this.Name == other.Name) return true;
			else return false;
		}
	}
}

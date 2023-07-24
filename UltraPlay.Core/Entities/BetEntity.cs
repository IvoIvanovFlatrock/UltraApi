using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace UltraPlay.Core.Entities
{
	[XmlRoot(ElementName = "Bet")]
	public class BetEntity : IEquatable<BetEntity>
	{
		public BetEntity() { }

		[XmlAttribute(AttributeName = "ID")]
		[Key]
		public int Id { get; set; }

		[XmlIgnore]
		public int MatchId { get; set; }

		[XmlIgnore]
		[ForeignKey(nameof(MatchId))]
		public MatchEntity Match { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "IsLive")]
		public bool IsLive { get; set; }

		[XmlElement(ElementName = "Odd")]
		public List<OddEntity> Odds { get; set; }

		public bool Equals(BetEntity? other)
		{
			if (other != null && this.Id == other.Id
				&& this.Name == other.Name
				&& this.IsLive == other.IsLive
				&& this.Name == other.Name) return true;
			else return false;
		}
	}
}

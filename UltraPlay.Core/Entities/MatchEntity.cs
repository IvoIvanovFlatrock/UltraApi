using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace UltraPlay.Core.Entities
{
	[XmlRoot(ElementName = "Match")]
	public class MatchEntity : IEquatable<MatchEntity>
	{
		public MatchEntity() { }

		[XmlAttribute(AttributeName = "ID")]
		[Key]
		public int Id { get; set; }

		[XmlIgnore]
		public int EventId { get; set; }

		[XmlIgnore]
		[ForeignKey(nameof(EventId))]
		public EventEntity Event { get; set; }

		[XmlAttribute(AttributeName = "StartDate")]
		public DateTime StartDate { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "MatchType")]
		public string MatchType { get; set; }

		[XmlElement(ElementName = "Bet")]
		public List<BetEntity> Bets { get; set; }

		public int CompareTo(MatchEntity? other)
		{
			if (other != null && this.Id == other.Id
				&& this.Name == other.Name
				&& this.StartDate == other.StartDate
				&& this.Name == other.Name
				&& this.MatchType == other.MatchType) return 0;
			else return 1;
		}

		public bool Equals(MatchEntity? other)
		{
			if (other != null && this.Id == other.Id
				&& this.Name == other.Name
				&& this.StartDate == other.StartDate
				&& this.Name == other.Name
				&& this.MatchType == other.MatchType) return true;
			return false;
		}
	}
}

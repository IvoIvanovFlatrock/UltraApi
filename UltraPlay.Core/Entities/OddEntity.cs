using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace UltraPlay.Core.Entities
{
	[XmlRoot(ElementName = "Odd")]
	public class OddEntity : IEquatable<OddEntity>
	{
		public OddEntity() { }

		[XmlAttribute(AttributeName = "ID")]
		[Key]
		public int Id { get; set; }

		[XmlIgnore]
		public int BetId { get; set; }

		[XmlIgnore]
		[ForeignKey(nameof(BetId))]
		public BetEntity Bet { get; set; }

		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "Value")]
		public double Value { get; set; }

		[XmlAttribute(AttributeName = "SpecialBetValue")]
		public double SpecialBetValue { get; set; }

		public bool Equals(OddEntity? other)
		{
			if (other != null && this.Id == other.Id
				&& this.Name == other.Name
				&& this.Value == other.Value) return true;
			return false;
		}
	}
}

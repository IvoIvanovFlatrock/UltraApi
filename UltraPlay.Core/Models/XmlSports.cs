using System.Xml.Serialization;
using UltraPlay.Core.Entities;

namespace UltraPlay.Core.Models
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(XmlSports));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (XmlSports)serializer.Deserialize(reader);
	// }

	//[XmlRoot(ElementName = "Odd")]
	//public class Odd
	//{
	//	[XmlAttribute(AttributeName = "Name")]
	//	public string Name { get; set; }

	//	[XmlAttribute(AttributeName = "ID")]
	//	public int ID { get; set; }

	//	[XmlAttribute(AttributeName = "Value")]
	//	public double Value { get; set; }

	//	[XmlAttribute(AttributeName = "SpecialBetValue")]
	//	public double SpecialBetValue { get; set; }
	//}

	//[XmlRoot(ElementName = "Bet")]
	//public class Bet
	//{

	//	[XmlElement(ElementName = "Odd")]
	//	public List<Odd> Odd { get; set; }

	//	[XmlAttribute(AttributeName = "Name")]
	//	public string Name { get; set; }

	//	[XmlAttribute(AttributeName = "ID")]
	//	public int ID { get; set; }

	//	[XmlAttribute(AttributeName = "IsLive")]
	//	public bool IsLive { get; set; }
	//}

	//[XmlRoot(ElementName = "Match")]
	//public class Match
	//{

	//	[XmlElement(ElementName = "Bet")]
	//	public List<Bet> Bet { get; set; }

	//	[XmlAttribute(AttributeName = "Name")]
	//	public string Name { get; set; }

	//	[XmlAttribute(AttributeName = "ID")]
	//	public int ID { get; set; }

	//	[XmlAttribute(AttributeName = "StartDate")]
	//	public DateTime StartDate { get; set; }

	//	[XmlAttribute(AttributeName = "MatchType")]
	//	public string MatchType { get; set; }
	//}

	//[XmlRoot(ElementName = "Event")]
	//public class Event
	//{

	//	[XmlElement(ElementName = "Match")]
	//	public List<Match> Match { get; set; }

	//	[XmlAttribute(AttributeName = "Name")]
	//	public string Name { get; set; }

	//	[XmlAttribute(AttributeName = "ID")]
	//	public int ID { get; set; }

	//	[XmlAttribute(AttributeName = "IsLive")]
	//	public bool IsLive { get; set; }

	//	[XmlAttribute(AttributeName = "CategoryID")]
	//	public int CategoryID { get; set; }
	//}

	//[XmlRoot(ElementName = "Sport")]
	//public class Sport
	//{
	//	[XmlElement(ElementName = "Event")]
	//	public List<Event> Event { get; set; }

	//	[XmlAttribute(AttributeName = "Name")]
	//	public string Name { get; set; }

	//	[XmlAttribute(AttributeName = "ID")]
	//	public int Id { get; set; }
	//}

	[XmlRoot(ElementName = "XmlSports")]
	public class XmlSports
	{

		[XmlElement(ElementName = "Sport")]
		public SportEntity Sport { get; set; }

		[XmlAttribute(AttributeName = "xsd")]
		public string Xsd { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "CreateDate")]
		public DateTime CreateDate { get; set; }
	}
}

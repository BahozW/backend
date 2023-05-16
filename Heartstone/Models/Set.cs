using System.Text.Json.Serialization;

namespace Heartstone.Models
{
    public class Set {
	public int Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	[JsonPropertyName("collectibleCount")]
	public int CardCount { get; set; }
}
}
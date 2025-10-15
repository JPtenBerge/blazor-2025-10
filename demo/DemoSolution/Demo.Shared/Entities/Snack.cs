using System.ComponentModel.DataAnnotations;

namespace Demo.Shared.Entities;

public class Snack
{
	public int Id { get; set; }
	
	[Required]
	[RegularExpression("^[a-zA-Z -]{3,}$", ErrorMessage = "Alleen letters en spaties graag")]
	public string Name { get; set; }
	
	[Range(1,5)]
	public decimal Rating { get; set; }
	
	[Required]
	public string PhotoUrl { get; set; }
}
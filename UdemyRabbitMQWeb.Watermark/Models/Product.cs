using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyRabbitMQWeb.Watermark.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "18,2")]
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string PictureUrl { get; set; }
}
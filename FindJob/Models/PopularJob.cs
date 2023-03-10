namespace Recruitment.Models;

public class PopularJob : SearchBase
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string MyProperty { get; set; }

    public int DataCount { get; set; }
    //[NotMapped, Required]
    //public IFormFile Photo { get; set; }
}
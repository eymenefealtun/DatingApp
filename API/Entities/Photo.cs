using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")] //we made EF to create table with the name of Photos instead of Photo with this attribute
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
    public int AppUserId { get; set; }      //If we didn't add this and below row EFCore is going to create the relationship between Photo and AppUser but it is going to set the AppUserId  nullable. To prevent this I've added this two lines of code
    public AppUser AppUser { get; set; }
}

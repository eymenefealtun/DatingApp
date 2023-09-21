using API.Extensions;

namespace API.Entities;

public class AppUser
{

    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; } //not unique
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interest { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new(); //  new (); == new List<Photo>(); 

    // public int GetAge() // AutoMapper is going to recognize the age properti inside of the MemberDto and going to map that age property using this method (method name is crucial it must be like that)
    // {
    //     return DateOfBirth.CalculateAge();  //Istead of using this here we add mapping into AutoMapperProfile.cs
    // }
}


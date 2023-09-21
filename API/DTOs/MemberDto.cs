namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string PhotoUrl { get; set; } // we configured AutoMapper inside of the AutoMapperProfile.cs. Because, autoMapper could not recognize this automatically

        public int Age { get; set; }
        public string KnownAs { get; set; } //not unique
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }


}
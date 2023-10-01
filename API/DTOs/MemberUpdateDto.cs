namespace API.DTOs
{
    public class MemberUpdateDto //We've mapped this place inside of the automapper profiler
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DotnetTaskAPI.Models
{
    public class ApplicationDTO
    {
        public string ProgramId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Profiles Profile { get; set; }
    }
}
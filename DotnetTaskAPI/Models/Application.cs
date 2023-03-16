using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DotnetTaskAPI.Models
{
    public class Application
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("personalInformation")]
        public PersonalInformation PersonalInformation { get; set; }

        [JsonProperty("profile")]
        public Profiles Profile { get; set; }
    }

    public class PersonalInformation
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNo { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public ICollection<AdditionalQuestion> AdditionalQuestions { get; set; }
    }

    public class Profiles
    {
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public string? Resume { get; set; }
        public ICollection<AdditionalQuestion> AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestion
    {
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
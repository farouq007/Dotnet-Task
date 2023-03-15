using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DotnetTaskAPI.Models
{
    public class ProgramDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("keySkills")]
        [Required, MaxLength(100)]
        public string? KeySkills { get; set; }

        [JsonProperty("benefits")]
        public string? Benefits { get; set; }

        [JsonProperty("applicationCriteria")]
        public string? ApplicationCriteria { get; set; }

        [JsonProperty("programType")]
        [Required]
        public string ProgramType { get; set; }

        [JsonProperty("programStartDate")]
        public DateTime? ProgramStartDate { get; set; }

        [JsonProperty("applicationStartDate")]
        [Required]
        public DateTime ApplicationStartDate { get; set; }

        [JsonProperty("applicationEndDate")]
        [Required]
        public DateTime ApplicationEndDate { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("location")]
        [Required]
        public Location Location { get; set; }

        [JsonProperty("minimumQualification")]
        public string? MinimumQualification { get; set; }

        [JsonProperty("maximumNoOfApplication")]
        public int? MaximumNoOfApplication { get; set; }
    }

    public class Location
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("fullyRemote")]
        public bool FullyRemote { get; set; }
    }
}
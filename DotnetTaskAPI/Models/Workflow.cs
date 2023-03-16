using Newtonsoft.Json;

namespace DotnetTaskAPI.Models
{
    public class Workflow
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("stages")]
        public ICollection<Stages> Stages { get; set; }
    }

    public class Stages
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Video? Video { get; set; }
    }

    public class Video
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Explanation { get; set; }
        public int? Duration { get; set; }
        public string? DurationDescription { get; set; }
        public int? Deadline { get; set; }
    }
}
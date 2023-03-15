using System.ComponentModel.DataAnnotations;

namespace DotnetTaskAPI.Models
{
    public class ProgramDetailsDTO
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        public string? Summary { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, MaxLength(100)]
        public string? KeySkills { get; set; }

        public string? Benefits { get; set; }
        public string? ApplicationCriteria { get; set; }

        [Required]
        public string ProgramType { get; set; }

        public DateTime? ProgramStartDate { get; set; }

        [Required]
        public DateTime ApplicationStartDate { get; set; }

        [Required]
        public DateTime ApplicationEndDate { get; set; }

        public int? Duration { get; set; }

        [Required]
        public Location Location { get; set; }

        public string? MinimumQualification { get; set; }
        public int? MaximumNoOfApplication { get; set; }
    }
}
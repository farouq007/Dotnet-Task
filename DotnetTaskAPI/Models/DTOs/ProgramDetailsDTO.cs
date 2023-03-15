using System.ComponentModel.DataAnnotations;

namespace DotnetTaskAPI.Models
{
    public class ProgramDetails
    {
        public int Id { get; set; }

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
        public ProgramTypes ProgramType { get; set; }

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

    public class Location
    {
        public string Address { get; set; }
        public bool FullyRemote { get; set; }
    }

    public enum ProgramTypes
    {
        FullTime = 1,
        PartTime
    }

    public enum QualificationTypes
    {
        Highschool = 1,
        Degree,
        Masters
    }
}
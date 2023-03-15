namespace DotnetTaskAPI.Models
{
    public class Workflow
    {
    }

    public class Stages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public enum StageTypes
    {
        Shortlisting = 1,
        VideoInterview,
        Placement
    }
}
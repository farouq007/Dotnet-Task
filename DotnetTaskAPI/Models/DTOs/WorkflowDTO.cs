namespace DotnetTaskAPI.Models
{
    public class WorkflowDTO
    {
    }

    public class StagesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public enum StageTypesDTO
    {
        Shortlisting = 1,
        VideoInterview,
        Placement
    }
}
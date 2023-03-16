using Newtonsoft.Json;

namespace DotnetTaskAPI.Models
{
    public class WorkflowDTO
    {
        public string ProgramId { get; set; }
        public ICollection<Stages>? Stages { get; set; }
    }
}
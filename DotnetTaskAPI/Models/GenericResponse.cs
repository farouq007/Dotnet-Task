namespace DotnetTaskAPI.Models
{
    public class GenericResponse
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public object Data { get; set; }
    }

    public class _Constants
    {
        public static readonly string _SUCCESS_ = "SUCCESS";
        public static readonly string _FAILED_ = "FAILED";
    }
}
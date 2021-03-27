#nullable enable

namespace Blog.Models
{
    public class ResponseModel<T> where T : class
    {
        public bool Error { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
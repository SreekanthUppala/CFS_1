namespace CFO_Task.Common
{
    public class ApiResult<T>
    {
        public T? Item { get; set; }
        public bool Succeded { get; set; }
        public Error Error { get; set; }
    }
}

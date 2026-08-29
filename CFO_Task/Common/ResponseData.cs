namespace CFO_Task.Common
{
    public class ResponseData<T> : ResponseBase
    {
        public T? Data { get; set; }
    }
}

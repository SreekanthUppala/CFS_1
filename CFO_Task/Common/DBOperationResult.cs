using CFO_Task.Common.Enum;

namespace CFO_Task.Common
{
    public class DBOperationResult
    {
        public bool Succeeded { get; set; }
        public int RowsEffected { get; set; }
        public Error ErrorMessage { get; set; }
    }
    public class Error
    {
        public ErrorCode Code { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}

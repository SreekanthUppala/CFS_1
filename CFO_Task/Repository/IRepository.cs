using CFO_Task.Common;
using CFO_Task.Models;

namespace CFO_Task.Repository
{
    public interface IRepository
    {
        Task<ApiResult<List<User>>> GetAllUsersFromDB();
        Task<DBOperationResult> AddUserToDB(User user);
        Task<DBOperationResult> DeleteUserFromDB(User user);
        Task<DBOperationResult> UpdateUserInDB(int id, User user);
        Task<ResponseData<User>> GetUserByID(int id);
    }
}

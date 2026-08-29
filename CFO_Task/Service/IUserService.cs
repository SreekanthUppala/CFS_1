using CFO_Task.Common;
using CFO_Task.Models;

namespace CFO_Task.Service
{
    public interface IUserService
    {
        Task<ResponseData<List<User>>> GetAllUsers();
        Task<ResponseBase> AddUser(User user);
        Task<ResponseBase> UpdateUser(int id, User user);
        Task<ResponseBase> DeleteUser(User user);
        Task<ResponseData<User>> GetUser(int id);
    }
}

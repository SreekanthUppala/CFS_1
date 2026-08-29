using CFO_Task.Common;
using CFO_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace CFO_Task.Repository
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly AppDbContext _context;
        public Repository(ILogger<Repository> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<DBOperationResult> AddUserToDB(User user)
        {
            try
            {
                _context.Users.Add(user);

                int rowsAffected = await _context.SaveChangesAsync();
                return new DBOperationResult
                {
                    RowsEffected = rowsAffected,
                    Succeeded = true,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while adding user to db : {0}", ex.ToString());
                return new DBOperationResult
                {
                    Succeeded = false,
                    ErrorMessage = new Error { Code = Common.Enum.ErrorCode.ExceptionOccuredWhileAddingUserToDB, Messages = new List<string> { ex.Message } }

                };
            }
        }

        public async Task<DBOperationResult> DeleteUserFromDB(User user)
        {
            try
            {
                _context.Users.Remove(user);
                int rowsAffected = await _context.SaveChangesAsync();
                return new DBOperationResult
                {
                    RowsEffected = rowsAffected,
                    Succeeded = true,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while deleting user from db : {0}", ex.ToString());
                return new DBOperationResult
                {
                    Succeeded = false,
                    ErrorMessage = new Error
                    {
                        Messages = new List<string> { $"Exception occurred while deleting user from db : {ex.Message}" },
                        Code = Common.Enum.ErrorCode.ExceptionOccuredInDeleteUserFromDB
                    }
                };
            }
        }

        public async Task<ApiResult<List<User>>> GetAllUsersFromDB()
        {
            try
            {
                var users = await _context.Users
                .OrderBy(u => u.Id)
                .ToListAsync();

                return new ApiResult<List<User>>
                {
                    Item = users,
                    Succeded = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while reading details from db : {0}", ex.ToString());
                return new ApiResult<List<User>>
                {
                    Item = null,
                    Succeded = false,
                    Error = new Error
                    {
                        Code = Common.Enum.ErrorCode.ExceptionOccuredWhileFetchingTheDbResponse,
                        Messages = new List<string> { $"Exception occurred while reading details from db : {ex.Message}" }
                    }
                };
            }
        }

        public async Task<ResponseData<User>> GetUserByID(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return new ResponseData<User>
                    {
                        Data = null,
                        Succeeded = false,
                        Error = new Error
                        {
                            Code = Common.Enum.ErrorCode.NotFoundUserInDBResponse,
                            Messages = new List<string> { "User not found" }
                        }
                    };
                }

                return new ResponseData<User>
                {
                    Data = user,
                    Succeeded = true
                };
            }
            catch (Exception ex) 
            {
                _logger.LogError("Exception occurred while reading user details from db : {0}", ex.ToString());
                return new ResponseData<User>
                {
                    Data = null,
                    Succeeded = false,
                    Error = new Error
                    {
                        Code = Common.Enum.ErrorCode.ExceptionOccuredWhileFetchingTheDbResponse,
                        Messages = new List<string> { $"Exception occurred while reading user details from db : {ex.Message}" }
                    }
                };
            }
        }

        public async Task<DBOperationResult> UpdateUserInDB(int id, User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    return new DBOperationResult
                    {
                        Succeeded = false,
                        ErrorMessage = new Error
                        {
                            Messages = new List<string> { "User not found" },
                            Code = Common.Enum.ErrorCode.NotFoundUserInDBResponse,
                        }
                    };
                }

                existingUser.Name = user.Name;
                existingUser.Age = user.Age;
                existingUser.City = user.City;
                existingUser.State = user.State;
                existingUser.Pincode = user.Pincode;

                var rowsAffected = await _context.SaveChangesAsync();

                return new DBOperationResult
                {
                    RowsEffected = rowsAffected,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while updating user in db : {0}", ex.ToString());
                return new DBOperationResult
                {
                    Succeeded = false,
                    ErrorMessage = new Error
                    {
                        Messages = new List<string> { $"Exception occurred while updating user in db : {ex.Message}" },
                        Code = Common.Enum.ErrorCode.ExceptionOccuredInUpdateUserInDB
                    }
                };
            }
        }


    }
}

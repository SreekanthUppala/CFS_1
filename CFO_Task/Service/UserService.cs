using CFO_Task.Common;
using CFO_Task.Common.Enum;
using CFO_Task.Models;
using CFO_Task.Repository;

namespace CFO_Task.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ResponseBase> AddUser(User user)
        {
            try
            {
                var result = await _repository.AddUserToDB(user);
                if (!result.Succeeded)
                {
                    _logger.LogError($"Failed to add user. Error: {string.Join(", ", result.ErrorMessage.Messages)}");
                    return new ResponseBase { Succeeded = false, Error = result.ErrorMessage };
                }
                else
                {
                    _logger.LogInformation($"User added successfully. Rows affected: {result.RowsEffected}");
                    return new ResponseBase { Succeeded = true };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while adding user: {ex.Message}");
                return new ResponseBase
                {
                    Succeeded = false,
                    Error = new Error {Code=ErrorCode.ErrorOccuredWhileCommunicatingTheDB, Messages = new List<string> { "An error occurred while adding the user." } }
                    
                };
            }
        }

        public async Task<ResponseBase> DeleteUser(User user)
        {
            try
            {
                var result = await _repository.DeleteUserFromDB(user);
                if (!result.Succeeded)
                {
                    _logger.LogError($"Failed to delete user. Error: {string.Join(", ", result.ErrorMessage.Messages)}");
                    return new ResponseBase { Succeeded = false, Error = result.ErrorMessage };
                }
                else
                {
                    _logger.LogInformation($"User deleted successfully. Rows affected: {result.RowsEffected}");
                    return new ResponseBase { Succeeded = true };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception occurred while deleting user: {ex.Message}");
                return new ResponseBase
                {
                    Succeeded = false,
                    Error = new Error { Code = ErrorCode.ErrorOccuredWhileCommunicatingTheDB, Messages = new List<string> { "An error occurred while deleting the user." } }
                };
            }
        }

        public async Task<ResponseData<List<User>>> GetAllUsers()
        {
            try
            {
                var result = await _repository.GetAllUsersFromDB();
                if (result.Succeded)
                {
                    return new ResponseData<List<User>>
                    {
                        Succeeded = true,
                        Data = result.Item
                    };
                }
                else
                {
                    return new ResponseData<List<User>>
                    {
                        Succeeded = false,
                        Error = result.Error
                    };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception occurred while retrieving users: {ex.Message}");
                return new ResponseData<List<User>>
                {
                    Succeeded = false,
                    Error = new Error { Code = ErrorCode.ErrorOccuredWhileCommunicatingTheDB, Messages = new List<string> { "An error occurred while retrieving users." } }
                };
            }
            
        }

        public async Task<ResponseData<User>> GetUser(int id)
        {
            try
            {
                var response= await _repository.GetUserByID(id);
                if (response.Succeeded)
                {
                    return new ResponseData<User>
                    {
                        Succeeded = true,
                        Data = response.Data
                    };
                }
                else
                {
                    return new ResponseData<User>
                    {
                        Succeeded = false,
                        Error = response.Error
                    };
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while retrieving user with ID {id}: {ex.Message}");
                return new ResponseData<User>
                {
                    Succeeded = false,
                    Error = new Error { Code = ErrorCode.ErrorOccuredWhileCommunicatingTheDB, Messages = new List<string> { "An error occurred while retrieving the user." } }
                };
            }
        }

        public async Task<ResponseBase> UpdateUser(int id, User user)
        {
            try
            {
                var result = await _repository.UpdateUserInDB(id,user);
                if (!result.Succeeded)
                {
                    _logger.LogError($"Failed to update user. Error: {string.Join(", ", result.ErrorMessage.Messages)}");
                    return new ResponseBase { Succeeded = false, Error = result.ErrorMessage };
                }
                else
                {
                    _logger.LogInformation($"User updated successfully. Rows affected: {result.RowsEffected}");
                    return new ResponseBase { Succeeded = true };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while updating user: {ex.Message}");
                return new ResponseBase
                {
                    Succeeded = false,
                    Error = new Error { Code = ErrorCode.ErrorOccuredWhileCommunicatingTheDB, Messages = new List<string> { "An error occurred while updating the user." } }
                };
            }
        }
    }
}

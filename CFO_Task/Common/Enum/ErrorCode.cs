namespace CFO_Task.Common.Enum
{
    public enum ErrorCode
    {
        Unauthorised=401,
        BadRequest = 400,
        NotFound = 404,
        ExceptionOccuredWhileFetchingTheDbResponse = 1001,
        ExceptionOccuredInDeleteUserFromDB=1002,
        ExceptionOccuredInUpdateUserInDB = 1003,
        NotFoundUserInDBResponse= 1004,
        ExceptionOccuredWhileAddingUserToDB= 1005,
        ErrorOccuredWhileCommunicatingTheDB = 1006,
    }
}

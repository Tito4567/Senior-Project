using LacesRepo;
using LacesRepo.Attributes;
using System;

namespace LacesDataModel.User
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_USERS)]
    [PrimaryKeyName("UserId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class User : DataObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int UsersFollowed { get; set; }
        public int UsersFollowing { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastAlertCheck { get; set; }

        public User() { }

        public User(int id) : base(id) {  }

        public override void Load(int id)
        {
            User temp = GetByValue<User>("UserId", Convert.ToString(id), Constants.TABLE_USERS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                UserId = temp.UserId;
                UserName = temp.UserName;
                Password = temp.Password;
                DisplayName = temp.DisplayName;
                Description = temp.Description;
                Email = temp.Email;
                UsersFollowed = temp.UsersFollowed;
                UsersFollowing = temp.UsersFollowing;
                CreatedDate = temp.CreatedDate;
                UpdatedDate = temp.UpdatedDate;
                LastAlertCheck = temp.LastAlertCheck;
            }
            else
            {
                throw new Exception("Could not find user with Id " + id);
            }
        }

        // User must use a stored procedure so that the SQL server can handle password encryption.
        public override bool Add()
        {
            bool result;

            StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_ADD_USER);

            proc.AddInput("@userName", UserName.Trim(), System.Data.SqlDbType.VarChar);
	        proc.AddInput("@email", Email.Trim(), System.Data.SqlDbType.VarChar);
	        proc.AddInput("@password", Password.Trim(), System.Data.SqlDbType.VarChar);
	        proc.AddInput("@displayName", DisplayName.Trim(), System.Data.SqlDbType.VarChar);
	        proc.AddInput("@description", Description.Trim(), System.Data.SqlDbType.VarChar);

            System.Data.SqlClient.SqlParameter idParam = proc.AddOutput("@userId", System.Data.SqlDbType.Int);

            result = proc.Execute();

            if (result)
            {
                UserId = Convert.ToInt32(idParam.Value);
            }

            return result;
        }

        public override bool Update()
        {
            bool result;

            StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_UPDATE_USER);

            proc.AddInput("@UserId", UserId, System.Data.SqlDbType.Int);
            proc.AddInput("@displayName", DisplayName.Trim(), System.Data.SqlDbType.VarChar);
            proc.AddInput("@description", Description.Trim(), System.Data.SqlDbType.VarChar);
            proc.AddInput("@usersFollowed", UsersFollowed, System.Data.SqlDbType.Int);
            proc.AddInput("@usersFollowing", UsersFollowing, System.Data.SqlDbType.Int);
            proc.AddInput("@lastAlertCheck", LastAlertCheck, System.Data.SqlDbType.DateTime);

            result = proc.Execute();

            return result;
        }

        public bool UpdatePassword(string oldPassword)
        {
            bool result;

            StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_CHANGE_PASSWORD);

            proc.AddInput("@userId", UserId, System.Data.SqlDbType.Int);
            proc.AddInput("@oldPassword", oldPassword.Trim(), System.Data.SqlDbType.VarChar);
            proc.AddInput("@newPassword", Password.Trim(), System.Data.SqlDbType.VarChar);

            System.Data.SqlClient.SqlParameter resultParam = proc.AddOutput("@result", System.Data.SqlDbType.Bit);

            result = proc.Execute();

            if (result)
            {
                result = Convert.ToBoolean(resultParam.Value);

                if (!result)
                {
                    throw new Exception("Old password incorrect.");
                }
            }

            return result;
        }

        public bool UserNameInUse()
        {
            return UsernameOrEmailInUse("UserName", UserName);
        }

        public bool EmailInUse()
        {
            return UsernameOrEmailInUse("Email", Email);
        }

        private bool UsernameOrEmailInUse(string column, string value)
        {
            bool result = false;

            User response = GetByValue<User>(column, value.Trim(), Constants.TABLE_USERS, Constants.SCHEMA_DEFAULT);

            if (response != null)
            {
                result = true;
            }

            return result;
        }

        public bool ValidateLogin()
        {
            bool result;

            StoredProcedure proc = new StoredProcedure(Constants.CONNECTION_STRING, Constants.STORED_PROC_VALIDATE_LOGIN);

            proc.AddInput("@userName", UserName.Trim(), System.Data.SqlDbType.VarChar);
            proc.AddInput("@password", Password.Trim(), System.Data.SqlDbType.VarChar);

            System.Data.SqlClient.SqlParameter idParam = proc.AddOutput("@userId", System.Data.SqlDbType.Int);

            result = proc.Execute();

            if (result)
            {
                if (idParam.Value != DBNull.Value)
                {
                    UserId = Convert.ToInt32(idParam.Value);
                }
                else
                {
                    UserId = 0;
                }
            }
            else
            {
                UserId = 0;
            }

            return result;
        }
    }
}

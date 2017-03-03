using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

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
        public DateTime CreatedDate { get; set; }

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
            proc.AddInput("@createdDate", CreatedDate, System.Data.SqlDbType.DateTime);

            System.Data.SqlClient.SqlParameter idParam = proc.AddOutput("@userId", System.Data.SqlDbType.Int);

            result = proc.Execute();

            if (result)
            {
                UserId = Convert.ToInt32(idParam.Value);
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

            SearchEntity search = new SearchEntity();

            search.ColumnsToReturn = new List<string>();
            search.ColumnsToReturn.Add(column);

            search.Conditions = new List<Condition>();

            Condition searchCond = new Condition();
            searchCond.Column = column;
            searchCond.Operator = Condition.Operators.EqualTo;
            searchCond.Value = value.Trim();

            search.Conditions.Add(searchCond);

            search.PageSizeLimit = 1;
            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_USERS;

            List<User> response = new GenericRepository<User>().Read(search);

            if (response.Count > 0)
            {
                result = true;
            }

            return result;
        }
    }
}

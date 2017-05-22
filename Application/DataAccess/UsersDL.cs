using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MarkdownNotes.DataAccess
{
    public class UsersDL
    {

        public static UsersDL GetInstance;
        
        /// <summary>
        /// Constructor
        /// </summary>
        static UsersDL()
        {

            GetInstance = new UsersDL();
        }

        /// <summary>
        /// Gets the UserId RoleID,User Name and Role Description 
        /// based on User Code.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserPassword(string userName)
        {
            var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_UserGet_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("Name", userName)
                });

            if (!string.IsNullOrEmpty(returnValue))
            {
                return returnValue;
            }
            return string.Empty;
        }

        public string AddNewUser(string name, string password)
        {
            var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_UserAdd_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("password", password)
                });

            if (!string.IsNullOrEmpty(returnValue))
            {
                return returnValue;
            }
            return string.Empty;
        }

        public User[] GetUserlist( )
        {
            return MasterDL.GetInstance.GetUserlist();
        }
    }
}

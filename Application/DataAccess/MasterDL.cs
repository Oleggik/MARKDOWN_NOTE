using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Annotations;
using System.Windows.Documents;

namespace MarkdownNotes.DataAccess
{
    public class MasterDL
    {

        string connectionString = ConfigurationManager.ConnectionStrings["SQlconnectionString"].ConnectionString;


        public static MasterDL GetInstance;


        /// <summary>
        /// Constructor
        /// </summary>
        static MasterDL()
        {

            GetInstance = new MasterDL();
        }

        /// <summary>
        /// Gets the UserId RoleID,User Name and Role Description 
        /// based on User Code.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="nameValuePairs"></param>
        /// <returns></returns>
        public T ExecuteStoredProcedure<T>(string spName,  List<KeyValuePair<string, object>> nameValuePairs ) 
        {
            object returnValue; 
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(spName, conn) {CommandType = CommandType.StoredProcedure} )
                {
                    foreach (KeyValuePair<string, object> nameValuePair in nameValuePairs)
                    {
                        cmd.Parameters.AddWithValue(nameValuePair.Key, nameValuePair.Value);
                    }

                    var returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(returnValueParam);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = returnValueParam.Value;

                }
            }

            return (T)Convert.ChangeType(returnValue, typeof(T));
        }

        /// <summary>
        /// Sets all previously cached application values
        /// </summary>
        public  Note[] GetNotelist(string userName)
        {
            List<Note> notes = new List<Note>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("dc_NotelistGet_1", conn) { CommandType = CommandType.StoredProcedure })
                {


                    cmd.Parameters.AddWithValue("@UserName", userName);

                    var returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(returnValueParam);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            int fieldIDIndex = reader.GetOrdinal("ID");
                            int fieldNameIndex = reader.GetOrdinal("Name");

                            while (reader.Read())
                            {
                                Note note = new Note()
                                {
                                    Id = reader.GetInt32(fieldIDIndex),
                                    Name = reader.GetString(fieldNameIndex)
                                };
                                notes.Add(note);
                            }
                        }
                    }

                }
            }

            return notes.ToArray();
        }

        /// <summary>
        /// Sets all previously cached application values
        /// </summary>
        public Note GetNote(int noteID)
        {
            Note note = null;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("dc_NoteGet_1", conn) { CommandType = CommandType.StoredProcedure })
                {


                    cmd.Parameters.AddWithValue("@NoteID", noteID);

                    var returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(returnValueParam);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            int fieldIDIndex = reader.GetOrdinal("ID");
                            int fieldNameIndex = reader.GetOrdinal("Name");
                            int fielTextIndex = reader.GetOrdinal("Text");
                            int fieldIsHiddenIndex = reader.GetOrdinal("IsHidden");
                            int fieldSharedToEveryoneIndex = reader.GetOrdinal("SharedToEveryone");
                            int fieldOwnerIDIndex = reader.GetOrdinal("OwnerID");

                            reader.Read();
                             note = new Note()
                                {
                                    Id = reader.GetInt32(fieldIDIndex),
                                    Name = reader.GetString(fieldNameIndex),
                                    Text = reader.GetString(fielTextIndex),
                                    IsHidden = reader.GetBoolean(fieldIsHiddenIndex),
                                    SharedToEveryone = reader.GetBoolean(fieldSharedToEveryoneIndex),
                                    OwnerId = reader.GetInt32(fieldOwnerIDIndex)
                                };

                        }
                    }

                }
            }

            return note;
        }


        /// <summary>
        /// Sets all previously cached application values
        /// </summary>
        public User[] GetUserlist( )
        {
            List<User> notes = new List<User>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("dc_UserListGet_1", conn) { CommandType = CommandType.StoredProcedure })
                {
                    var returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(returnValueParam);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            int fieldIDIndex = reader.GetOrdinal("UserID");
                            int fieldNameIndex = reader.GetOrdinal("Name");

                            while (reader.Read())
                            {
                                User note = new User()
                                {
                                    Id = reader.GetInt32(fieldIDIndex),
                                    Name = reader.GetString(fieldNameIndex)
                                };
                                notes.Add(note);
                            }
                        }
                    }

                }
            }

            return notes.ToArray();
        }
    }
}

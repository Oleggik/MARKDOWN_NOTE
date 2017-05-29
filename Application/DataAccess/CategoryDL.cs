using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownNotes.DataAccess
{
    public class CategoryDL
    {

            public static CategoryDL GetInstance;

            /// <summary>
            /// Constructor
            /// </summary>
            static CategoryDL()
            {

                GetInstance = new CategoryDL();
            }

            /// <summary>
            /// Gets the UserId RoleID,User Name and Role Description 
            /// based on User Code.
            /// </summary>
            /// <param name="userName"></param>
            /// <returns></returns>
            public Category[] GetCategoryList(string UserName)
            {
                return MasterDL.GetInstance.GetCategorylist(UserName);
            }            

            public string AddCategory(Category category, string userName)
            {
                var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_CategoryAdd_1",
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("Name", category.Name),
                        new KeyValuePair<string, object>("UserName", userName),
                    });

                if (!string.IsNullOrEmpty(returnValue))
                {
                    return returnValue;
                }
                return string.Empty;
            }

            public string DelCategory(int categoryId)
            {
                var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_CategoryDel_1",
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("ID", categoryId)
                    });
                if (!string.IsNullOrEmpty(returnValue))
                {
                    return returnValue;
                }
                return string.Empty;
            }

            public void RenameCategory(int CategoryId, string name)
            {
                MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_CategoryRename_1",
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("ID", CategoryId),
                        new KeyValuePair<string, object>("Name", name)
                    });
            }

            public string SetCategory(int noteID, int CategoryID)
            {
                var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_CategorySet_1",
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("NoteID", noteID),
                        new KeyValuePair<string, object>("ID", CategoryID)
                    });
                if (!string.IsNullOrEmpty(returnValue))
                {
                    return returnValue;
                }
                return string.Empty;
            }
        }
    }

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MarkdownNotes.DataAccess
{
    public class NotesDL
    {

        public static NotesDL GetInstance;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        static NotesDL()
        {

            GetInstance = new NotesDL();
        }

        /// <summary>
        /// Gets the UserId RoleID,User Name and Role Description 
        /// based on User Code.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Note[] GetNotesList(string userName)
        {
            return MasterDL.GetInstance.GetNotelist(userName);
        }

        public Note GetNote(int noteID)
        {
            return MasterDL.GetInstance.GetNote(noteID);
        }

        public Note[] GetNoteInCategorylist(string userName, int categoryID)
        {
            return MasterDL.GetInstance.GetNoteInCategorylist(userName, categoryID);
        }

        public string AddNote(string userName, Note note)
        {
            var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_NoteAdd_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("UserName", userName),
                    new KeyValuePair<string, object>("NoteName", note.Name),
                    new KeyValuePair<string, object>("NoteText", note.Text),
                    //new KeyValuePair<string, object>("CategoryID", note.ID),
                    new KeyValuePair<string, object>("IsHidden", note.IsHidden),
                    new KeyValuePair<string, object>("SharedToEveryone", note.SharedToEveryone)
                });

            if (!string.IsNullOrEmpty(returnValue))
            {
                return returnValue;
            }
            return string.Empty;
        }

        public string AddNoteSharing(int userId, int noteId)
        {
            var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_NoteSharingAdd_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("UserId", userId),
                    new KeyValuePair<string, object>("NoteId", noteId)
                });

            if (!string.IsNullOrEmpty(returnValue))
            {
                return returnValue;
            }
            return string.Empty;
        }


        public string DelNote(int noteId)
        {
            var returnValue = MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_NoteDel_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("NoteID", noteId)
                });
            if (!string.IsNullOrEmpty(returnValue))
            {
                return returnValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the UserId RoleID,User Name and Role Description 
        /// based on User Code.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public void UpdateNoteText(int noteId, string text)
        {
            MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_NoteUpdate_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("NoteID", noteId),
                    new KeyValuePair<string, object>("NoteText", text)
                });
        }

        public void UpdateNoteName(int noteId, string name)
        {
            MasterDL.GetInstance.ExecuteStoredProcedure<string>("dc_NoteRename_1",
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("NoteID", noteId),
                    new KeyValuePair<string, object>("NoteName", name)
                });
        }
    }
}

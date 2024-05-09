using System;
using System.Collections.Generic;
using System.Web.Http;
using Virtual_Diary.BasicAuth;
using Virtual_Diary.BL;
using Virtual_Diary.Helper;
using Virtual_Diary.Logging;
using Virtual_Diary.Models;

namespace Virtual_Diary.Controllers
{
    /// <summary>
    /// Controller for managing diary entries with authentication and caching.
    /// </summary>
    [RoutePrefix("api/v1")]
    [BasicAuthentication]
    public class CLDiaryEntryV1Controller : ApiController
    {
        #region Private Members
        private static BLDiaryEntryManager _diaryManager;
        #endregion

        #region Public Members
        public Response response;
        #endregion

        #region Constructor
        static CLDiaryEntryV1Controller()
        {
            _diaryManager = new BLDiaryEntryManager();
        }
        #endregion

        /// <summary>
        /// Retrieves all diary entries.
        /// </summary>
        /// <returns>List of diary entries.</returns>
        [HttpGet]
        [Route("diary")]
        [BasicAuthorisationAttribute(Roles = "admin")]
        public IEnumerable<DiaryEntry> GetAllDiaryEntries()
        {
            try
            {
                var diaryEntries = _diaryManager.GetDiaryEntriesFromCacheOrSource();
                return diaryEntries;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error occurred while retrieving diary entries.", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Retrieves a diary entry by its ID.
        /// </summary>
        /// <param name="id">ID of the diary entry.</param>
        /// <returns>Diary entry with the specified ID.</returns>
        [HttpGet]
        [Route("diary/{id}")]
        [BasicAuthorisationAttribute(Roles = "user")]
        public IHttpActionResult GetDiaryEntryById(int id)
        {
            try
            {
                DiaryEntry diaryEntry = _diaryManager.GetDiaryEntryById(id);
                return Ok(diaryEntry);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while retrieving diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Creates a new diary entry.
        /// </summary>
        /// <param name="newDiaryEntry">New diary entry to be created.</param>
        /// <returns>Created diary entry.</returns>
        [HttpPost]
        [Route("diary")]
        [BasicAuthorisationAttribute(Roles = "user")]
        public IHttpActionResult CreateDiaryEntry([FromBody] DiaryEntry newDiaryEntry)
        {
            response = new Response();

            _diaryManager.operation = enmOperations.I;
            _diaryManager.PreSave(newDiaryEntry);
            response = _diaryManager.Validate();
            if (!response.IsError)
            {
                response = _diaryManager.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Edits an existing diary entry.
        /// </summary>
        /// <param name="id">ID of the diary entry to be edited.</param>
        /// <param name="updatedDiaryEntry">Updated details for the diary entry.</param>
        /// <returns>Edited diary entry.</returns>
        [HttpPut]
        [Route("diary/{id}")]
        [BasicAuthorisationAttribute(Roles = "admin,superadmin")]
        public IHttpActionResult EditDiaryEntry([FromBody] DiaryEntry updatedDiaryEntry)
        {
            response = new Response();

            _diaryManager.operation = enmOperations.U;
            _diaryManager.PreSave(updatedDiaryEntry);
            response = _diaryManager.Validate();
            if (!response.IsError)
            {
                response = _diaryManager.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a diary entry by its ID.
        /// </summary>
        /// <param name="id">ID of the diary entry to be deleted.</param>
        /// <returns>Deleted diary entry.</returns>
        [HttpDelete]
        [Route("diary/{id}")]
        [BasicAuthorisationAttribute(Roles = "admin,superadmin")]
        public IHttpActionResult DeleteDiaryEntry(int id)
        {
            response = new Response();
            _diaryManager.operation = enmOperations.D;

            response = _diaryManager.ValidateOnDelete(id);
            if (!response.IsError)
            {
                response = _diaryManager.DeleteDiaryEntry(id);
            }

            return Ok(response);
        }
    }
}

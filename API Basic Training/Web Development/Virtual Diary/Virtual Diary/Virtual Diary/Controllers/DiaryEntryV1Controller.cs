using System;
using System.Collections.Generic;
using System.Web.Http;
using Virtual_Diary.BasicAuth;
using Virtual_Diary.BL;
using Virtual_Diary.Logging;
using Virtual_Diary.Models;

namespace Virtual_Diary.Controllers
{
    /// <summary>
    /// Controller for managing diary entries with authentication and caching.
    /// </summary>
    [RoutePrefix("api/v1")]
    [BasicAuthentication]
    public class DiaryEntryV1Controller : ApiController
    {
        private const string CacheKey = "DiaryEntriesCache";
        private static DiaryEntryManagerBL _diaryManager;

        static DiaryEntryV1Controller()
        {
            _diaryManager = new DiaryEntryManagerBL();
        }

        
        /// <summary>
        /// Retrieves all diary entries.
        /// </summary>
        /// <returns>List of diary entries.</returns>
        [HttpGet]
        [Route("diary")]
        [BasicAuthorisationAttribute(Roles = "user")]
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
            try
            {
                _diaryManager.CreateDiaryEntry(newDiaryEntry);
                return Ok(newDiaryEntry);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error occurred while creating a new diary entry.", ex);
                throw; // Rethrow the exception
            }
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
        public IHttpActionResult EditDiaryEntry(int id, [FromBody] DiaryEntry updatedDiaryEntry)
        {
            try
            {
                _diaryManager.EditDiaryEntry(id, updatedDiaryEntry);
                return Ok(updatedDiaryEntry);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while editing diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
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
            try
            {
                DiaryEntry deletedDiaryEntry = _diaryManager.GetDiaryEntryById(id);
                _diaryManager.DeleteDiaryEntry(id);
                return Ok(deletedDiaryEntry);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
        }
    }
}

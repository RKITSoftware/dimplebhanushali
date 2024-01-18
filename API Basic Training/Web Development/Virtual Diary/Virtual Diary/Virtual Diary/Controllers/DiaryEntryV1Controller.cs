using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;
using Virtual_Diary.BasicAuth;
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

        /// <summary>
        /// Retrieves all diary entries from cache or the source.
        /// </summary>
        /// <returns>List of diary entries.</returns>
        private List<DiaryEntry> GetDiaryEntriesFromCacheOrSource()
        {
            Logger.LogInfo("Attempting to retrieve diary entries from cache.");

            var cache = MemoryCache.Default;
            var diaryEntries = cache.Get(CacheKey) as List<DiaryEntry>;

            if (diaryEntries == null)
            {
                Logger.LogInfo("Cache miss. Retrieving diary entries from the source.");

                // If not in cache, retrieve from the source (e.g., database)
                diaryEntries = DiaryEntry.lstDiaryEntries.ToList();

                // Cache the result for a specific duration (e.g., 10 minutes)
                cache.Add(CacheKey, diaryEntries, DateTimeOffset.Now.AddMinutes(10));

                Logger.LogInfo("Diary entries added to cache.");
            }
            else
            {
                Logger.LogInfo("Diary entries retrieved from cache.");
            }

            return diaryEntries;
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
                var diaryEntries = GetDiaryEntriesFromCacheOrSource();
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
                var diaryEntries = GetDiaryEntriesFromCacheOrSource();
                var diaryEntry = diaryEntries.FirstOrDefault(d => d.Id == id);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {id}");
                    return BadRequest($"No Entry Found With Id => {id}");
                }

                Logger.LogInfo($"Diary entry with Id {id} retrieved successfully.");

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
                Logger.LogInfo("Attempting to create a new diary entry.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                newDiaryEntry.Id = DiaryEntry.lstDiaryEntries.Count + 1;
                DiaryEntry.lstDiaryEntries.Add(newDiaryEntry);

                Logger.LogInfo("New diary entry created successfully.");

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
                Logger.LogInfo($"Attempting to edit diary entry with Id: {id}.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                var diaryEntryToEdit = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == id);

                if (diaryEntryToEdit == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {id}");
                    return BadRequest($"No Entry Found With Id => {id}");
                }

                // Update diary entry details
                diaryEntryToEdit.Content = updatedDiaryEntry.Content;
                diaryEntryToEdit.DateCreated = updatedDiaryEntry.DateCreated;
                // Assuming Tasks is a list that needs to be updated
                diaryEntryToEdit.Tasks = updatedDiaryEntry.Tasks;

                Logger.LogInfo($"Diary entry with Id {id} edited successfully.");

                return Ok(diaryEntryToEdit);
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
                Logger.LogInfo($"Attempting to delete diary entry with Id: {id}.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                var diaryEntryToRemove = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == id);

                if (diaryEntryToRemove == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {id}");
                    return BadRequest($"No Entry Found With Id => {id}");
                }

                DiaryEntry.lstDiaryEntries.Remove(diaryEntryToRemove);

                Logger.LogInfo($"Diary entry with Id {id} deleted successfully.");

                return Ok(diaryEntryToRemove);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
        }
    }
}

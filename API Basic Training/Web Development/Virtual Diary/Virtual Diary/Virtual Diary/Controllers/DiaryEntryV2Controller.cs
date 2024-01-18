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
    /// Controller for managing tasks within diary entries with authentication and caching.
    /// </summary>
    [RoutePrefix("api/v2")]
    public class DiaryEntryV2Controller : ApiController
    {
        private const string CacheKey = "DiaryEntriesCache";

        /// <summary>
        /// Adds a new task to a diary entry.
        /// </summary>
        /// <param name="id">ID of the diary entry.</param>
        /// <param name="newTask">New task to be added.</param>
        /// <returns>Added task.</returns>
        [HttpPost]
        [Route("diary/{id}/tasks")]
        [BasicAuthorisationAttribute(Roles = "user")]
        public IHttpActionResult AddTaskToDiaryEntry(int id, [FromBody] TaskDetail newTask)
        {
            try
            {
                Logger.LogInfo($"Attempting to add a new task to diary entry with Id: {id}.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                var diaryEntry = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == id);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {id}");
                    return BadRequest($"No Entry Found With Id => {id}");
                }

                newTask.Id = diaryEntry.Tasks.Count + 1;
                diaryEntry.Tasks.Add(newTask);

                Logger.LogInfo($"New task added to diary entry with Id {id} successfully.");

                return Ok(newTask);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while adding a new task to diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Retrieves a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Task within the specified diary entry.</returns>
        [HttpGet]
        [Route("diary/{entryId}/tasks/{taskId}")]
        [BasicAuthorisationAttribute(Roles = "user")]
        public IHttpActionResult GetTaskWithinDiaryEntry(int entryId, int taskId)
        {
            try
            {
                Logger.LogInfo($"Attempting to retrieve task with Id: {taskId} within diary entry with Id: {entryId} from cache.");

                var cache = MemoryCache.Default;
                var diaryEntries = cache.Get(CacheKey) as List<DiaryEntry>;

                if (diaryEntries == null)
                {
                    Logger.LogInfo("Cache miss. Retrieving all diary entries from the source.");

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

                var diaryEntry = diaryEntries.FirstOrDefault(d => d.Id == entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {entryId}");
                    return BadRequest($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    return BadRequest($"No Task Found With Id => {taskId}");
                }

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} retrieved successfully.");

                return Ok(task);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while retrieving task with Id: {taskId} within diary entry with Id: {entryId}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Updates a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <param name="updatedTask">Updated details for the task.</param>
        /// <returns>Updated task.</returns>
        [HttpPut]
        [Route("diary/{entryId}/tasks/{taskId}")]
        [BasicAuthorisationAttribute(Roles = "admin,superadmin")]
        public IHttpActionResult UpdateTaskWithinDiaryEntry(int entryId, int taskId, [FromBody] TaskDetail updatedTask)
        {
            try
            {
                Logger.LogInfo($"Attempting to update task with Id: {taskId} within diary entry with Id: {entryId}.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                var diaryEntry = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {entryId}");
                    return BadRequest($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    return BadRequest($"No Task Found With Id => {taskId}");
                }

                // Update task details
                task.TaskName = updatedTask.TaskName;
                task.IsCompleted = updatedTask.IsCompleted;

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} updated successfully.");

                return Ok(task);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while updating task with Id: {taskId} within diary entry with Id: {entryId}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Deletes a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Result of the deletion operation.</returns>
        [HttpDelete]
        [Route("diary/{entryId}/tasks/{taskId}")]
        [BasicAuthorisationAttribute(Roles = "admin,superadmin")]
        public IHttpActionResult DeleteTaskWithinDiaryEntry(int entryId, int taskId)
        {
            try
            {
                Logger.LogInfo($"Attempting to delete task with Id: {taskId} within diary entry with Id: {entryId}.");

                var cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                var diaryEntry = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {entryId}");
                    return BadRequest($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    return BadRequest($"No Task Found With Id => {taskId}");
                }

                // Remove task
                diaryEntry.Tasks.Remove(task);

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} deleted successfully.");

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting task with Id: {taskId} within diary entry with Id: {entryId}", ex);
                throw; // Rethrow the exception
            }
        }
    }
}

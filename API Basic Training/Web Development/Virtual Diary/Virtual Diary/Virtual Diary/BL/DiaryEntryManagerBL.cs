using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Virtual_Diary.Logging;
using Virtual_Diary.Models;

namespace Virtual_Diary.BL
{
    /// <summary>
    /// Business logic class for managing diary entries and tasks.
    /// </summary>
    public class DiaryEntryManagerBL
    {
        private const string CacheKey = "DiaryEntriesCache";

        /// <summary>
        /// Retrieves diary entries from cache or source.
        /// </summary>
        /// <returns>List of diary entries.</returns>
        public List<DiaryEntry> GetDiaryEntriesFromCacheOrSource()
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
        /// Retrieves a diary entry by its ID.
        /// </summary>
        /// <param name="id">ID of the diary entry.</param>
        /// <returns>Diary entry with the specified ID.</returns>
        public DiaryEntry GetDiaryEntryById(int id)
        {
            var diaryEntries = GetDiaryEntriesFromCacheOrSource();
            return diaryEntries.FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Creates a new diary entry.
        /// </summary>
        /// <param name="newDiaryEntry">New diary entry to be created.</param>
        public void CreateDiaryEntry(DiaryEntry newDiaryEntry)
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
        public void EditDiaryEntry(int id, DiaryEntry updatedDiaryEntry)
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
                    throw new InvalidOperationException($"No Entry Found With Id => {id}");
                }

                // Update diary entry details
                diaryEntryToEdit.Content = updatedDiaryEntry.Content;
                diaryEntryToEdit.DateCreated = updatedDiaryEntry.DateCreated;
                // Assuming Tasks is a list that needs to be updated
                diaryEntryToEdit.Tasks = updatedDiaryEntry.Tasks;

                Logger.LogInfo($"Diary entry with Id {id} edited successfully.");
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
        public void DeleteDiaryEntry(int id)
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
                    throw new InvalidOperationException($"No Entry Found With Id => {id}");
                }

                DiaryEntry.lstDiaryEntries.Remove(diaryEntryToRemove);

                Logger.LogInfo($"Diary entry with Id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting diary entry with Id: {id}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Adds a new task to a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="newTask">New task to be added.</param>
        public void AddTaskToDiaryEntry(int entryId, TaskDetail newTask)
        {
            try
            {
                Logger.LogInfo($"Attempting to add a new task to diary entry with Id: {entryId}.");

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
                    throw new InvalidOperationException($"No Entry Found With Id => {entryId}");
                }

                newTask.Id = diaryEntry.Tasks.Count + 1;
                diaryEntry.Tasks.Add(newTask);

                Logger.LogInfo($"New task added to diary entry with Id {entryId} successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while adding a new task to diary entry with Id: {entryId}", ex);
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Retrieves a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Task within the specified diary entry.</returns>
        public TaskDetail GetTaskWithinDiaryEntry(int entryId, int taskId)
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
                    throw new InvalidOperationException($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    throw new InvalidOperationException($"No Task Found With Id => {taskId}");
                }

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} retrieved successfully.");

                return task;
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
        public TaskDetail UpdateTaskWithinDiaryEntry(int entryId, int taskId, TaskDetail updatedTask)
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
                    throw new InvalidOperationException($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    throw new InvalidOperationException($"No Task Found With Id => {taskId}");
                }

                // Update task details
                task.TaskName = updatedTask.TaskName;
                task.IsCompleted = updatedTask.IsCompleted;

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} updated successfully.");

                return task;
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
        public void DeleteTaskWithinDiaryEntry(int entryId, int taskId)
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
                    throw new InvalidOperationException($"No Entry Found With Id => {entryId}");
                }

                var task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    throw new InvalidOperationException($"No Task Found With Id => {taskId}");
                }

                // Remove task
                diaryEntry.Tasks.Remove(task);

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} deleted successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting task with Id: {taskId} within diary entry with Id: {entryId}", ex);
                throw; // Rethrow the exception
            }
        }
    }
}

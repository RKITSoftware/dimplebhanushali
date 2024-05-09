using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Virtual_Diary.Helper;
using Virtual_Diary.Logging;
using Virtual_Diary.Models;

namespace Virtual_Diary.BL
{
    /// <summary>
    /// Business logic class for managing diary entries and tasks.
    /// </summary>
    public class BLDiaryEntryManager
    {
        #region Private Members
        private const string CacheKey = "DiaryEntriesCache";
        private DiaryEntry _objDiary;
        private TaskDetail _objTask;
        private int _entryId;
        #endregion

        #region Public Memebers
        public EnumMessage operation;
        #endregion

        #region Public Methods
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
        /// Presave on insert
        /// </summary>
        /// <param name="objDiary"> Diary Object </param>
        public void PreSave(DiaryEntry objDiary)
        {
            _objDiary = objDiary;
            if (operation == EnumMessage.I)
            {
                _objDiary.DateCreated = DateTime.Now;
            }
        }

        /// <summary>
        /// Validation that checks for a duplicate entry.
        /// </summary>
        /// <param name="newDiaryEntry">New diary entry to be validated.</param>
        /// <returns>A response indicating whether the entry is valid.</returns>
        public Response Validate()
        {
            Response response = new Response();
            try
            {
                Logger.LogInfo("Attempting to validate new diary entry for duplicates.");

                var diaryEntries = GetDiaryEntriesFromCacheOrSource();

                // Check if any existing entry matches the content and date of the new entry
                var duplicateEntry = diaryEntries.FirstOrDefault(d =>
                    d.Content.Equals(_objDiary.Content, StringComparison.OrdinalIgnoreCase) &&
                    d.DateCreated == _objDiary.DateCreated);

                if (duplicateEntry != null)
                {
                    // If a duplicate entry is found
                    Logger.LogWarn("Duplicate diary entry found.");

                    response.IsError = false;
                    response.Message = "Duplicate entry found.";

                }
                Logger.LogInfo("No duplicate diary entry found. Validation successful.");
            }
            catch (Exception ex)
            {
                response.IsError = false;
                response.Message = ex.Message;
                Logger.LogError("Error occurred while validating new diary entry for duplicates.", ex);
                throw; // Rethrow the exception
            }

            return response;
        }

        /// <summary>
        /// Saves the diary entry.
        /// </summary>
        /// <returns>A response indicating the save operation result.</returns>
        public Response Save()
        {
            Response response = new Response();
            Logger.LogInfo("Attempting to create a new diary entry.");
            if (operation == EnumMessage.I)
            {
                _objDiary.Id = DiaryEntry.lstDiaryEntries.Count + 1;
                DiaryEntry.lstDiaryEntries.Add(_objDiary);
                response.Message = EnumMessage.I.GetMessage();
                Logger.LogInfo("New diary entry created successfully.");
            }
            else if (operation == EnumMessage.U)
            {
                var diaryEntryToEdit = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == _objDiary.Id);

                if (diaryEntryToEdit == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {_objDiary.Id}");
                    throw new InvalidOperationException($"No Entry Found With Id => {_objDiary.Id}");
                }

                // Update diary entry details
                diaryEntryToEdit.Content = _objDiary.Content;
                diaryEntryToEdit.DateCreated = _objDiary.DateCreated;
                // Assuming Tasks is a list that needs to be updated
                diaryEntryToEdit.Tasks = _objDiary.Tasks;

                response.Message = EnumMessage.U.GetMessage();
                Logger.LogInfo($"Diary entry with Id {_objDiary.Id} edited successfully.");
            }

            return response;
        }

        /// <summary>
        /// Validates the deletion of a diary entry based on the entry ID.
        /// </summary>
        /// <param name="id">The ID of the diary entry to be deleted.</param>
        /// <returns>A response indicating whether the deletion is valid.</returns>
        public Response ValidateOnDelete(int id)
        {
            Response response = new Response();
            try
            {
                // Check if the entry with the given ID exists in the list
                var diaryEntryToDelete = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == id);

                if (diaryEntryToDelete != null)
                {
                    // If the entry exists, it can be deleted
                    response.Message = $"Diary entry with ID {id} exists and can be deleted.";
                }
                else
                {
                    // If no entry is found with the given ID, it cannot be deleted
                    response.IsError = true;
                    response.Message = $"Diary entry with ID {id} does not exist.";
                }
            }
            catch (Exception ex)
            {
                // Error occurred during validation
                response.IsError = true;
                response.Message = ex.Message;
                Logger.LogError($"Error occurred while validating deletion of diary entry with ID {id}.", ex);
                throw; // Rethrow the exception
            }
            return response;
        }

        /// <summary>
        /// Deletes a diary entry by its ID.
        /// </summary>
        /// <param name="id">ID of the diary entry to be deleted.</param>
        /// <returns>A response indicating the result of the deletion.</returns>
        public Response DeleteDiaryEntry(int id)
        {
            Response response = new Response();
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

                DiaryEntry.lstDiaryEntries.Remove(diaryEntryToRemove);

                Logger.LogInfo($"Diary entry with Id {id} deleted successfully.");

                response.Message = $"Diary entry with Id {id} deleted successfully.";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting diary entry with Id: {id}", ex);
                response.IsError = true;
                response.Message = ex.Message;
                throw; // Rethrow the exception
            }

            return response;
        }

        /// <summary>
        /// Pre-save task method to prepare task details before saving.
        /// </summary>
        /// <param name="entryId">The ID of the diary entry to which the task belongs.</param>
        /// <param name="taskDetail">The details of the task to be saved.</param>
        public void PreSaveTask(int entryId, TaskDetail taskDetail)
        {
            _objTask = taskDetail;
            _objDiary = DiaryEntry.lstDiaryEntries.FirstOrDefault(d => d.Id == entryId);
            _objTask.Id = _objDiary.Tasks.Count + 1;
        }

        /// <summary>
        /// Validates the task before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateTask()
        {
            Response response = new Response();
            try
            {
                Logger.LogInfo($"Attempting to validate new task within diary entry with Id: {_entryId}.");

                var diaryEntry = GetDiaryEntryById(_entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {_entryId}");
                    response.IsError = true;
                }

                // Check if any existing task matches the new task's details
                var duplicateTask = diaryEntry.Tasks.FirstOrDefault(t =>
                    t.TaskName.Equals(_objTask.TaskName, StringComparison.OrdinalIgnoreCase));

                if (duplicateTask != null)
                {
                    // If a duplicate task is found
                    Logger.LogWarn("Duplicate task found within diary entry.");

                    response.IsError = true;
                    response.Message = "Duplicate task found within diary entry.";
                }

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
                Logger.LogError($"Error occurred while validating task within diary entry with Id: {_entryId}.", ex);
                throw; // Rethrow the exception
            }

            return response;
        }

        /// <summary>
        /// Saves the task into the diary entry.
        /// </summary>
        /// <returns>A response indicating the save operation result.</returns>
        public Response SaveTask()
        {
            Response response = new Response();
            Logger.LogInfo($"Attempting to save task within diary entry with Id: {_entryId}.");

            // Check if the operation is insert or update
            if (operation == EnumMessage.I)
            {
                // Inserting a new task
                _objTask.Id = _objDiary.Tasks.Count + 1;
                _objDiary.Tasks.Add(_objTask);

                response.Message = "Task Inserted Successfully";
                Logger.LogInfo($"New task added to diary entry with Id {_entryId} successfully.");
            }
            else if (operation == EnumMessage.U)
            {
                // Updating an existing task
                var existingTask = _objDiary.Tasks.FirstOrDefault(t => t.Id == _objTask.Id);
                if (existingTask == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {_objTask.Id}");
                    response.IsError = true;
                    response.Message = "";
                }

                // Update task details
                existingTask.TaskName = _objTask.TaskName;
                existingTask.IsCompleted = _objTask.IsCompleted;

                response.Message = "Task Updated Successfully";
                Logger.LogInfo($"Task with Id {_objTask.Id} within diary entry with Id {_entryId} updated successfully.");
            }

            return response;
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
                throw;
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
        /// <returns>A response indicating the result of the deletion.</returns>
        public Response DeleteTaskWithinDiaryEntry(int entryId, int taskId)
        {
            Response response = new Response();
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

                // Remove task
                _objDiary.Tasks.Remove(_objTask);

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} deleted successfully.");

                response.Message = $"Task with Id {taskId} within diary entry with Id {entryId} deleted successfully.";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while deleting task with Id: {taskId} within diary entry with Id: {entryId}", ex);
                response.IsError = true;
                response.Message = ex.Message;
            }

            return response;
        }


        /// <summary>
        /// Validates the deletion of a task within a diary entry based on the entry ID and task ID.
        /// </summary>
        /// <param name="entryId">The ID of the diary entry containing the task.</param>
        /// <param name="taskId">The ID of the task to be deleted.</param>
        /// <returns>A response indicating whether the deletion is valid.</returns>
        public Response ValidateOnDeleteTask(int entryId, int taskId)
        {
            Response response = new Response();
            try
            {
                Logger.LogInfo($"Attempting to validate deletion of task with ID {taskId} within diary entry with ID {entryId}.");

                _objDiary = GetDiaryEntryById(entryId);

                if (_objDiary == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {entryId}");
                    response.IsError = true;
                    response.Message = $"No Entry Found With Id => {entryId}";
                    return response;
                }

                // Check if the task exists within the diary entry
                _objTask = _objDiary.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (_objTask == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId} within diary entry with Id => {entryId}");
                    response.IsError = true;
                    response.Message = $"No Task Found With Id => {taskId} within diary entry with Id => {entryId}";
                    return response;
                }

                // Task exists and can be deleted
                response.Message = $"Task with ID {taskId} exists within diary entry with ID {entryId} and can be deleted.";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
                Logger.LogError($"Error occurred while validating deletion of task with ID {taskId} within diary entry with ID {entryId}.", ex);
                throw; // Rethrow the exception
            }

            return response;
        }

        #endregion
    }
}

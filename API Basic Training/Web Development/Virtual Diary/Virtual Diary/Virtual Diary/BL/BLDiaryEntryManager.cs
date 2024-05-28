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

        /// <summary>
        /// Instance of Cache
        /// </summary>
        private const string CacheKey = "DiaryEntriesCache";

        /// <summary>
        /// Instance of Diary Entry
        /// </summary>
        private DiaryEntry _objDiary;

        /// <summary>
        /// Instance of TaskDetail.
        /// </summary>
        private TaskDetail _objTask;

        /// <summary>
        /// Entry Id
        /// </summary>
        private int _entryId;

        /// <summary>
        /// A predefined list of sample diary entries for demonstration purposes.
        /// </summary>
        private static List<DiaryEntry> lstDiaryEntries = new List<DiaryEntry>
        {
            // Simple Diary Entries
            new DiaryEntry
            {
                Id = 1,
                Content = "Today was a great day!",
                DateCreated = DateTime.Now.AddDays(-3), // 3 days ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Write diary entry", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Go for a walk", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 2,
                Content = "Worked on a new project.",
                DateCreated = DateTime.Now.AddDays(-2), // 2 days ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Meeting with the team", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Code review", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 3,
                Content = "Explored new hiking trails.",
                DateCreated = DateTime.Now.AddDays(-2),
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Hike in the mountains", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Capture scenic views", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 4,
                Content = "Relaxing weekend at home.",
                DateCreated = DateTime.Now.AddDays(-1), // 1 day ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Read a book", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Watch a movie", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 5,
                Content = "Productive day at work.",
                DateCreated = DateTime.Now.AddDays(-1),
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Finish project report", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Prepare for client meeting", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 6,
                Content = "Experimented with new recipes.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Cook a new dish", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Bake a dessert", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 7,
                Content = "Visited a local art exhibition.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Explore art gallery", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Attend art workshop", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 8,
                Content = "Gardening in the backyard.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Plant flowers", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Trim bushes", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 9,
                Content = "Exercise and fitness routine.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Morning jog", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Strength training", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 10,
                Content = "Learning a new musical instrument.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Practice guitar", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Learn new chords", IsCompleted = false },
                }
            },
         };

        #endregion

        #region Public Members
        /// <summary>
        /// Enm Operation for specifying inser , update and delete operation.
        /// </summary>
        public enmOperations operation;

        /// <summary>
        /// Response instance
        /// </summary>
        public Response response;
        #endregion

        #region Public Methods

        #region Diary Entry
        /// <summary>
        /// Retrieves diary entries from cache or source.
        /// </summary>
        /// <returns>List of diary entries.</returns>
        public List<DiaryEntry> GetDiaryEntriesFromCacheOrSource()
        {
            //Logger.LogInfo("Attempting to retrieve diary entries from cache.");

            MemoryCache cache = MemoryCache.Default;
            List<DiaryEntry> diaryEntries = cache.Get(CacheKey) as List<DiaryEntry>;

            if (diaryEntries == null)
            {
                Logger.LogInfo("Cache miss. Retrieving diary entries from the source.");

                // If not in cache, retrieve from the source (e.g., database)
                diaryEntries = lstDiaryEntries.ToList();

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
            List<DiaryEntry> diaryEntries = GetDiaryEntriesFromCacheOrSource();
            return diaryEntries.FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Presave on insert
        /// </summary>
        /// <param name="objDiary"> Diary Object </param>
        public void PreSave(DiaryEntry objDiary)
        {
            _objDiary = objDiary;
            if (operation == enmOperations.I)
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
                //Logger.LogInfo("Attempting to validate new diary entry for duplicates.");

                List<DiaryEntry> diaryEntries = GetDiaryEntriesFromCacheOrSource();

                // Check if any existing entry matches the content and date of the new entry
                DiaryEntry duplicateEntry = diaryEntries.FirstOrDefault(d =>
                    d.Content.Equals(_objDiary.Content, StringComparison.OrdinalIgnoreCase) &&
                    d.DateCreated == _objDiary.DateCreated);

                if (duplicateEntry != null)
                {
                    // If a duplicate entry is found
                    Logger.LogWarn("Duplicate diary entry found.");

                    response.IsError = false;
                    response.Message = "Duplicate entry found.";

                }

                //Logger.LogInfo("No duplicate diary entry found. Validation successful.");
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

            //Logger.LogInfo("Attempting to create a new diary entry.");
            if (operation == enmOperations.I)
            {
                _objDiary.Id = lstDiaryEntries.Count + 1;
                lstDiaryEntries.Add(_objDiary);
                GetDiaryEntriesFromCacheOrSource();
                response.Message = enmOperations.I.GetMessage();
                Logger.LogInfo("New diary entry created successfully.");
            }
            else if (operation == enmOperations.U)
            {
                DiaryEntry diaryEntryToEdit = lstDiaryEntries.FirstOrDefault(d => d.Id == _objDiary.Id);

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

                response.Message = enmOperations.U.GetMessage();
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
                DiaryEntry diaryEntryToDelete = lstDiaryEntries.FirstOrDefault(d => d.Id == id);

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
                //Logger.LogInfo($"Attempting to delete diary entry with Id: {id}.");

                MemoryCache cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                int removedEntriesCount = lstDiaryEntries.RemoveAll(d => d.Id == id);

                if (removedEntriesCount > 0)
                {
                    Logger.LogInfo($"Diary entry with Id {id} deleted successfully.");
                    response.Message = enmOperations.D.GetMessage();
                }
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

        #endregion

        #region Task Details

        /// <summary>
        /// Pre-save task method to prepare task details before saving.
        /// </summary>
        /// <param name="entryId">The ID of the diary entry to which the task belongs.</param>
        /// <param name="taskDetail">The details of the task to be saved.</param>
        public void PreSaveTask(int entryId, TaskDetail taskDetail)
        {
            _objTask = taskDetail;
            _objDiary = lstDiaryEntries.FirstOrDefault(d => d.Id == entryId);

            if (operation == enmOperations.I)
            {
                if (_objDiary.Tasks == null)
                {
                    _objDiary.Tasks = new List<TaskDetail>();
                }

                _objTask.Id = _objDiary.Tasks.Count + 1;
            }
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
                //Logger.LogInfo($"Attempting to validate new task within diary entry with Id: {_entryId}.");

                DiaryEntry diaryEntry = GetDiaryEntryById(_entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {_entryId}");
                    response.IsError = true;
                    return response;
                }

                // Ensure diaryEntry.Tasks is not null
                if (diaryEntry.Tasks == null)
                {
                    diaryEntry.Tasks = new List<TaskDetail>();
                }

                // Check if any existing task matches the new task's details
                TaskDetail duplicateTask = diaryEntry.Tasks.FirstOrDefault(t =>
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
            //Logger.LogInfo($"Attempting to save task within diary entry with Id: {_entryId}.");

            // Check if the operation is insert or update
            if (operation == enmOperations.I)
            {
                // Inserting a new task
                _objTask.Id = _objDiary.Tasks.Count + 1;
                _objDiary.Tasks.Add(_objTask);

                response.Message = enmOperations.I.GetMessage();
                Logger.LogInfo($"New task added to diary entry with Id {_entryId} successfully.");
            }
            else if (operation == enmOperations.U)
            {
                response = UpdateTaskWithinDiaryEntry(_objDiary.Id, _objTask);

                //// Updating an existing task
                //TaskDetail existingTask = _objDiary.Tasks.FirstOrDefault(t => t.Id == _objTask.Id);

                //// Update task details
                //existingTask.TaskName = _objTask.TaskName;
                //existingTask.IsCompleted = _objTask.IsCompleted;

                //response.Message = enmOperations.U.GetMessage();

                //Logger.LogInfo($"Task with Id {_objTask.Id} within diary entry with Id {_entryId} updated successfully.");
            }

            return response;
        }

        /// <summary>
        /// Retrieves a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <returns>Task within the specified diary entry.</returns>
        public Response GetTaskWithinDiaryEntry(int entryId, int taskId)
        {
            response = new Response();
            try
            {
                //Logger.LogInfo($"Attempting to retrieve task with Id: {taskId} within diary entry with Id: {entryId} from cache.");

                MemoryCache cache = MemoryCache.Default;
                List<DiaryEntry> diaryEntries = cache.Get(CacheKey) as List<DiaryEntry>;

                if (diaryEntries == null)
                {
                    Logger.LogInfo("Cache miss. Retrieving all diary entries from the source.");

                    // If not in cache, retrieve from the source (e.g., database)
                    diaryEntries = lstDiaryEntries.ToList();

                    // Cache the result for a specific duration (e.g., 10 minutes)
                    cache.Add(CacheKey, diaryEntries, DateTimeOffset.Now.AddMinutes(10));

                    Logger.LogInfo("Diary entries added to cache.");
                }
                else
                {
                    Logger.LogInfo("Diary entries retrieved from cache.");
                }

                DiaryEntry diaryEntry = diaryEntries.FirstOrDefault(d => d.Id == entryId);

                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Entry Found With Id => {entryId}");
                    throw new InvalidOperationException($"No Entry Found With Id => {entryId}");
                }

                TaskDetail task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {taskId}");
                    throw new InvalidOperationException($"No Task Found With Id => {taskId}");
                }

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} retrieved successfully.");

                response.Data = task;

                return response;
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
        public Response UpdateTaskWithinDiaryEntry(int entryId, TaskDetail updatedTask)
        {
            response = new Response();

            try
            {
                //Logger.LogInfo($"Attempting to update task with Id: {taskId} within diary entry with Id: {entryId}.");

                MemoryCache cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                // Ensure lstDiaryEntries is not null
                if (lstDiaryEntries == null)
                {
                    //Logger.LogError("Diary entries list is null.");
                    throw new InvalidOperationException("Diary entries list is null.");
                }

                DiaryEntry diaryEntry = lstDiaryEntries.FirstOrDefault(d => d.Id == entryId);

                // Check if diaryEntry is null
                if (diaryEntry == null)
                {
                    Logger.LogWarn($"No Diary Entry Found With Id => {entryId}");
                    throw new InvalidOperationException($"No Diary Entry Found With Id => {entryId}");
                }

                // Ensure diaryEntry.Tasks is not null
                if (diaryEntry.Tasks == null)
                {
                    //Logger.LogError($"Tasks list for diary entry with Id {entryId} is null.");
                    throw new InvalidOperationException($"Tasks list for diary entry with Id {entryId} is null.");
                }

                TaskDetail task = diaryEntry.Tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

                if (task == null)
                {
                    Logger.LogWarn($"No Task Found With Id => {updatedTask.Id}");
                    throw new InvalidOperationException($"No Task Found With Id => {updatedTask.Id}");
                }

                // Update task details
                task.TaskName = updatedTask.TaskName;
                task.IsCompleted = updatedTask.IsCompleted;

                Logger.LogInfo($"Task with Id {updatedTask.Id} within diary entry with Id {entryId} updated successfully.");

                response.Data = task;

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while updating task with Id: {updatedTask.Id} within diary entry with Id: {entryId}", ex);
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

                MemoryCache cache = MemoryCache.Default;

                // If in cache, invalidate the cache
                if (cache.Contains(CacheKey))
                {
                    cache.Remove(CacheKey);
                    Logger.LogInfo("Diary entries cache invalidated.");
                }

                ////
                // Remove task
                //_objDiary.Tasks.Remove(_objTask);
                _objDiary.Tasks.RemoveAll(task => task.Id == _objTask.Id);

                Logger.LogInfo($"Task with Id {taskId} within diary entry with Id {entryId} deleted successfully.");

                response.Message = enmOperations.D.GetMessage();

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

        #endregion

    }
}

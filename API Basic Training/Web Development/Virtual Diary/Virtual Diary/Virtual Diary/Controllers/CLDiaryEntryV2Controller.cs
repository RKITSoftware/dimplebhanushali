using System.Web.Http;
using Virtual_Diary.BasicAuth;
using Virtual_Diary.BL;
using Virtual_Diary.Helper;
using Virtual_Diary.Models;

namespace Virtual_Diary.Controllers
{
    /// <summary>
    /// Controller for managing tasks within diary entries with authentication and caching.
    /// </summary>
    [RoutePrefix("api/v2")]
    [BasicAuthentication]
    //[AllowAnonymous]
    public class CLDiaryEntryV2Controller : ApiController
    {
        #region Private Members
        /// <summary>
        /// Instance of BLDiaryManager.
        /// </summary>
        private static BLDiaryEntryManager _diaryManager;
        #endregion

        #region Public Members
        /// <summary>
        /// Instance of Response for setting response as output in methods.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for initialising BLDiaryManager's Instance.
        /// </summary>
        static CLDiaryEntryV2Controller()
        {
            _diaryManager = new BLDiaryEntryManager();
        }
        #endregion

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
            response = new Response();
            _diaryManager.operation = enmOperations.I;
            _diaryManager.PreSaveTask(id, newTask);

            response = _diaryManager.ValidateTask();
            if (!response.IsError)
            {
                response = _diaryManager.SaveTask();
            }
            return Ok(response);
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
            return Ok(_diaryManager.GetTaskWithinDiaryEntry(entryId, taskId));
        }

        /// <summary>
        /// Updates a task within a diary entry.
        /// </summary>
        /// <param name="entryId">ID of the diary entry.</param>
        /// <param name="taskId">ID of the task.</param>
        /// <param name="updatedTask">Updated details for the task.</param>
        /// <returns>Updated task.</returns>
        [HttpPut]
        [Route("diary/{entryId}/tasks")]
        [BasicAuthorisationAttribute(Roles = "admin,superadmin")]
        public IHttpActionResult UpdateTaskWithinDiaryEntry(int entryId, [FromBody] TaskDetail updatedTask)
        {
            response = new Response();
            _diaryManager.operation = enmOperations.U;
            _diaryManager.PreSaveTask(entryId, updatedTask);

            response = _diaryManager.ValidateTask();
            if (!response.IsError)
            {
                response = _diaryManager.SaveTask();
            }
            return Ok(response);
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
            response = new Response();
            _diaryManager.operation = enmOperations.D;

            response = _diaryManager.ValidateOnDeleteTask(entryId, taskId);
            if (!response.IsError)
            {
                response = _diaryManager.DeleteTaskWithinDiaryEntry(entryId, taskId);
            }
            return Ok(response);
        }
    }
}

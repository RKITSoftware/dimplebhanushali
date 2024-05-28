using System.Collections.Generic;
using System.Web.Http;
using Virtual_Diary.BasicAuth;
using Virtual_Diary.BL;
using Virtual_Diary.Helper;
using Virtual_Diary.Models;

namespace Virtual_Diary.Controllers
{
    /// <summary>
    /// Controller for managing diary entries with authentication and caching.
    /// </summary>
    [RoutePrefix("api/v1")]
    [BasicAuthentication]
    //[AllowAnonymous]
    public class CLDiaryEntryV1Controller : ApiController
    {
        #region Private Members
        /// <summary>
        /// Instance of BLDiary Manager.
        /// </summary>
        private static BLDiaryEntryManager _diaryManager;
        #endregion

        #region Public Members
        /// <summary>
        /// Instance of response for Setting response.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for initialising BLDiaryManager instance.
        /// </summary>
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
                List<DiaryEntry> diaryEntries = _diaryManager.GetDiaryEntriesFromCacheOrSource();
                return diaryEntries;
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
                DiaryEntry diaryEntry = _diaryManager.GetDiaryEntryById(id);
                return Ok(diaryEntry);
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

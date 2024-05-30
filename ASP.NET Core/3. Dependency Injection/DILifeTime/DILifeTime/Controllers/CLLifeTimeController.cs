using DILifeTime.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DILifeTime.Controllers
{
    /// <summary>
    /// Controller to demonstrate the difference in lifetimes of dependency injections.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLLifeTimeController : ControllerBase
    {
        #region Private Members
        /// <summary>
        /// Transient Instance 1
        /// </summary>
        private readonly ITransientService _transientService1;

        /// <summary>
        /// Transient Instance 2
        /// </summary>
        private readonly ITransientService _transientService2;

        /// <summary>
        /// Scoped Instance 1
        /// </summary>
        private readonly IScopedService _scopedService1;

        /// <summary>
        /// Scoped Instance 2
        /// </summary>
        private readonly IScopedService _scopedService2;

        /// <summary>
        /// Singleton Instance 1
        /// </summary>
        private readonly ISingletonService _singletonService1;

        /// <summary>
        /// Singleton Instance 2
        /// </summary>
        private readonly ISingletonService _singletonService2;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for <see cref="CLLifeTimeController"/>.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="transientService1">Transient service instance 1.</param>
        /// <param name="transientService2">Transient service instance 2.</param>
        /// <param name="scopedService1">Scoped service instance 1.</param>
        /// <param name="scopedService2">Scoped service instance 2.</param>
        /// <param name="singletonService1">Singleton service instance 1.</param>
        /// <param name="singletonService2">Singleton service instance 2.</param>
        public CLLifeTimeController(ILogger<CLLifeTimeController> logger,
            ITransientService transientService1,
            ITransientService transientService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ISingletonService singletonService1,
            ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }
        #endregion

        /// <summary>
        /// Action method to get the difference in lifetimes of injected services.
        /// </summary>
        /// <returns>An IActionResult containing the difference in lifetimes.</returns>
        [HttpGet("Difference")]
        public IActionResult Difference()
        {
            var data = new
            {
                transient1 = _transientService1.GetOperationID().ToString(),
                transient2 = _transientService2.GetOperationID().ToString(),
                scoped1 = _scopedService1.GetOperationID().ToString(),
                scoped2 = _scopedService2.GetOperationID().ToString(),
                singleton1 = _singletonService1.GetOperationID().ToString(),
                singleton2 = _singletonService2.GetOperationID().ToString()
            };

            return Ok(data);
        }
    }
}

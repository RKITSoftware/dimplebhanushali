using System;

namespace Final_Demo
{
    /// <summary>
    /// Represents a vehicle with license plate, model, and parking ticket number.
    /// </summary>
    public class Vehicle
    {
        #region Properties
        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the parking ticket number assigned to the vehicle.
        /// </summary>
        public string ParkingTicketNumber { get; set; } = Guid.NewGuid().ToString();

        #endregion
    }
}

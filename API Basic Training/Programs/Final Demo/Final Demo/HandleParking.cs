using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Final_Demo
{
    /// <summary>
    /// Handles parking operations such as adding, updating, and removing vehicles.
    /// Also, provides methods to display parked vehicles, download parking tickets, and get user input for vehicles.
    /// </summary>
    public class HandleParking
    {
        #region Fields

        private static List<Vehicle> parkedVehicles = new List<Vehicle>();

        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a vehicle to the list of parked vehicles.
        /// </summary>
        /// <param name="vehicle">The vehicle to be parked.</param>
        public void AddVehicle(Vehicle vehicle)
        {
            parkedVehicles.Add(vehicle);
            Console.WriteLine("Vehicle parked successfully.");
            Console.WriteLine($"Parking Ticket: {vehicle.ParkingTicketNumber}");
        }

        /// <summary>
        /// Updates the information of an existing vehicle based on its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to be updated.</param>
        /// <param name="updatedVehicle">The updated vehicle information.</param>
        public void UpdateVehicle(string licensePlate, Vehicle updatedVehicle)
        {
            Vehicle existingVehicle = parkedVehicles.Find(vehicle => vehicle.LicensePlate == licensePlate);

            if (existingVehicle != null)
            {
                existingVehicle.LicensePlate = updatedVehicle.LicensePlate;
                existingVehicle.Model = updatedVehicle.Model;
                Console.WriteLine("Vehicle information updated successfully.");
            }
            else
            {
                Console.WriteLine($"No vehicle with license plate {licensePlate} found.");
            }
        }

        /// <summary>
        /// Removes a vehicle from the list of parked vehicles based on its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to be removed.</param>
        public void RemoveVehicle(string licensePlate)
        {
            int indexToRemove = parkedVehicles.FindIndex(vehicle => vehicle.LicensePlate == licensePlate);

            if (indexToRemove != -1)
            {
                parkedVehicles.RemoveAt(indexToRemove);
                Console.WriteLine("Vehicle removed successfully.");
            }
            else
            {
                Console.WriteLine($"No vehicle with license plate {licensePlate} found.");
            }
        }

        /// <summary>
        /// Displays the list of parked vehicles in a sorted manner using DataView.
        /// </summary>
        public void DisplayVehicles()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("License Plate", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Parking Ticket", typeof(string));

            foreach (Vehicle vehicle in parkedVehicles)
            {
                dataTable.Rows.Add(vehicle.LicensePlate, vehicle.Model, vehicle.ParkingTicketNumber);
            }

            // Create a DataView to sort the data by License Plate
            DataView dataView = new DataView(dataTable);
            dataView.Sort = "License Plate";

            Console.WriteLine("Parked Vehicles (Sorted by License Plate):");
            Console.WriteLine("| License Plate | Model | Parking Ticket |");
            Console.WriteLine("|---------------|-------|-----------------|");

            foreach (DataRowView rowView in dataView)
            {
                DataRow row = rowView.Row;
                Console.WriteLine($"| {row["License Plate"],-13} | {row["Model"],-5} | {row["Parking Ticket"],-15} |");
            }
        }

        /// <summary>
        /// Downloads a parking ticket for a given vehicle.
        /// The file is saved with the format "{ParkingTicketNumber}.txt" in the current directory.
        /// </summary>
        /// <param name="vehicle">The vehicle for which the parking ticket is downloaded.</param>
        public void DownloadParkingTicket(Vehicle vehicle)
        {
            string fileName = $"{vehicle.ParkingTicketNumber}.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Vehicle Details:");
                writer.WriteLine($"License Plate: {vehicle.LicensePlate}");
                writer.WriteLine($"Model: {vehicle.Model}");
                writer.WriteLine($"Parking Ticket: {vehicle.ParkingTicketNumber}");
                writer.WriteLine($"Date and Time: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

                Console.WriteLine($"Parking ticket downloaded successfully. File saved as {filePath}");
            }
        }

        /// <summary>
        /// Gets user input for a new vehicle, including license plate and model.
        /// </summary>
        /// <returns>The vehicle created based on user input.</returns>
        public Vehicle GetUserVehicleInput()
        {
            Console.Write("Enter License Plate: ");
            string licensePlate = Console.ReadLine();
            Console.Write("Enter Car Model: ");
            string model = Console.ReadLine();

            return new Vehicle { LicensePlate = licensePlate, Model = model };
        }

        #endregion
    }
}
    
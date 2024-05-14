using Historical_Events.Data;
using Historical_Events.DL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using Historical_Events.User_Validation;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace Historical_Events.BL
{
    public class BLUser
    {
        private static string _connection;
        private usr01 _objUser;

        public Response response;
        public enmOperation operation;

        public BLUser(string connectionString)
        {
            _connection = connectionString;
        }

        public string RegisterUser(usr01 objUser)
        {
            if (objUser == null)
            {
                return "Invalid details";
            }
            InsertUser(objUser);
            return "User added";
        }

        public void PreSave(DTOUSR01 objUser)
        {
            if (_objUser == null)
            {
                _objUser = new usr01();
            }

            objUser.Map(_objUser);

            _objUser.r01f06 = BLAES.Encrypt(_objUser.r01f06);

            if (operation == enmOperation.I)
            {
                _objUser.CreatedAt = DateTime.Now;
            }
            else if (operation == enmOperation.U)
            {
                _objUser.UpdatedAt = DateTime.Now;
            }
        }

        public Response Validate()
        {
            response = new Response();

            if (operation == enmOperation.I)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    // Check if the table exists
                    if (!db.TableExists<usr01>())
                    {
                        response.isError = true;
                        response.Message = "Table does not exist.";
                        return response;
                    }

                    bool isDuplicate = db.Exists<usr01>(x => x.r01f03 == _objUser.r01f03 || x.r01f05 == _objUser.r01f05 || x.r01f04 == _objUser.r01f04);
                    if (isDuplicate)
                    {
                        response.isError = true;
                        response.Message = "Duplicate entry found.";
                    }
                }
            }
            if(operation == enmOperation.U)
            {
                using(IDbConnection db = MyDbContext.CreateConnection())
                {
                    // Check if user ID exists in database 
                    bool isUserIdExists = db.Exists<usr01>(x => x.r01f01 == _objUser.r01f01);
                    if (!isUserIdExists)
                    {
                        response.isError = true;
                        response.Message = "User ID not exists.";
                        return response;
                    }
                }
            }
            return response;
        }

        public Response Save()
        {
            response = new Response();

            if (operation == enmOperation.I)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Insert(_objUser);
                    response.Message = enmOperation.I.GetMessage();
                }
            }
            else if (operation == enmOperation.U)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Update(_objUser);
                    response.Message = enmOperation.U.GetMessage();
                }
            }
            return response;
        }

        public void CreateTables()
        {
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                db.DropAndCreateTable<usr01>();
            }
        }

        public Response ValidateOnDelete(int id)
        {
            response = new Response();

            if(operation == enmOperation.D)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    usr01 existingObj = db.SingleById<usr01>(id);

                    if (existingObj == null)
                    {
                        response.isError = true;
                        response.Message = $"No record found with Id => {id}.";
                    }
                    else
                    {
                        response.Message = $"Record with Id => {id} exists and can be deleted.";
                    }
                }
            }

            return response;
        }

        public Response LoginUser(string userName, string password)
        {
            BLValidateUser blValidateUser = new BLValidateUser();

            bool isCredentialCorrect = blValidateUser.IsLogin(userName, password);
            if (!isCredentialCorrect)
            {
                response.isError = true;
                response.Message = "Invalid Credential";
                return response;
            }

            string token = blValidateUser.GenerateJwtToken(userName);
            
            response.isError = false;
            response.Message = "Token generated";
            response.Data = token;
            return response;
        }

        public Response GetAllUsers()
        {
            DbUsr01Context dbUsr01Context = new DbUsr01Context(_connection);
            response = new Response
            {
                isError = false,
                Message = "All users are fetched",
                Data = dbUsr01Context.GetAllUsers()
            };
            return response;
        }

        public Response GetUserById(int id)
        {
            DbUsr01Context dbUsr01Context = new DbUsr01Context(_connection);
            response = new Response
            {
                isError = false,
                Message = "Fetched user by user id",
                Data = dbUsr01Context.GetUserById(id)
            };
            return response;
        }

        public Response DeleteUser(int id)
        {
            Response response = new Response();
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                // Deleting the record with the specified id from the database table corresponding to type T
                int rowsAffected = db.DeleteById<usr01>(id);
                response.Message = $"Record with id {id} deleted successfully.";
            }
            return response;
        }

        private void InsertUser(usr01 user)
        {
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                db.Insert(user);
            }
        }
    }
}
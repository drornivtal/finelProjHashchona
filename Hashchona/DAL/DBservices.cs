
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml.Linq;
using Hashchona.BL;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;


namespace Hashchona.DAL
{
    public class DBservices
    {
        public DBservices() {}

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

              
        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (2)
        //---------------------------------------------------------------------------------
        private SqlCommand SelectUserByEmailWithStoredProcedureWithParameters(String spName, SqlConnection con,  string email)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

  
            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }

     


        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (4)
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }





        //------------------//
        //     User:        //
        //------------------//
        //--------------------------------------------------------------------------------------------------
        // read all users
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadAllUsers()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = buildReadStoredProcedureCommand("spReadAllUsers", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User s = new User();                
                   
                    
                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar( dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    usersList.Add(s);
                   
               
                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand buildReadStoredProcedureCommand(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read all users
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadManagersCommunity(int CommunityID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = buildReadByStatusUsersForCommunityStoredProcedureCommand("spReadAllManagersCommunity", con, CommunityID);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User s = new User();


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    //s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    usersList.Add(s);


                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //--------------------------------------------------------------------------------------------------
        // read all users that got approved to the community
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadAllPendingUsersForCommunity(int CommunityID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = buildReadByStatusUsersForCommunityStoredProcedureCommand("spGetAllUsersPendingToCommunity", con, CommunityID);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User s = new User();


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    usersList.Add(s);


                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // read all users that got approved to the community
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadAllApprovedUsersForCommunity(int CommunityID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = buildReadByStatusUsersForCommunityStoredProcedureCommand("spGetAllUsersAcceptedToCommunity", con, CommunityID);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User s = new User();


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    usersList.Add(s);


                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand buildReadByStatusUsersForCommunityStoredProcedureCommand(String spName, SqlConnection con, int CommunityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@CommunityID", CommunityID);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read specific User
        //--------------------------------------------------------------------------------------------------
        public User ReadUser(string phoneNum)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadUserStoredProcedureCommand("spReadUser", con, phoneNum);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User s = new User();

                while (dataReader.Read())
                {


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    


                }
                return s;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadUserStoredProcedureCommand(String spName, SqlConnection con, string phoneNum)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNum);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read specific Manager for specific community 
        //--------------------------------------------------------------------------------------------------
        public User ReadManagerForSpecificCommunity(int communityId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadManagerForSpecificCommunityProcedureCommand("spReadManagerForSpecificCommunity", con, communityId);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User s = new User();

                while (dataReader.Read())
                {


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);



                }
                return s;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadManagerForSpecificCommunityProcedureCommand(String spName, SqlConnection con, int communityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@communityID", communityID);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // Registration --> This method Inserts a user to the users table 
        //--------------------------------------------------------------------------------------------------
        public int InsertUser(User user, int communityId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserInsertCommandWithStoredProcedure("spInsertNewUser", con, user, communityId); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (1-Registration & Details update)
        //----------------------------------------------------------------------------------------
        private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user, int communityId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);

            cmd.Parameters.AddWithValue("@LastName", user.LastName);

            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNum);
            cmd.Parameters.AddWithValue("@uPassword", user.Password);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Street", user.Street);
            cmd.Parameters.AddWithValue("@HomeNumber", user.HomeNum);
            cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            cmd.Parameters.AddWithValue("@uDescription", user.Description);
            cmd.Parameters.AddWithValue("@ProfilePic", user.Description);
            cmd.Parameters.AddWithValue("@CommunityId", communityId);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // LogIn --> This method reads user by phoneNum and password from the database 
        //--------------------------------------------------------------------------------------------------
        public User Login(string phoneNum, string password, int communityId)
        {

            SqlConnection con;
            SqlCommand cmd;


            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = SelectUserToLogInWithStoredProcedureWithParameters("spLoginUserActiveUserU", con, phoneNum, password,communityId);   // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                User s = new User();

                string outputMessage = "";
                while (dataReader.Read())
                {

                   // outputMessage = dataReader.GetString(0); // Assuming the stored procedure returns a single string column


                    s.UserId = Convert.ToInt32(dataReader["UserID"]);
                    s.FirstName = dataReader["FirstName"].ToString();
                    s.LastName = dataReader["LastName"].ToString();
                    s.PhoneNum = dataReader["PhoneNumber"].ToString();
                    s.Password = dataReader["uPassword"].ToString();
                    s.Gender = Convert.ToChar(dataReader["Gender"]);
                    s.City = dataReader["City"].ToString();
                    s.Street = dataReader["Street"].ToString();
                    s.HomeNum = Convert.ToInt16(dataReader["HomeNumber"]);
                    s.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    s.Description = dataReader["uDescription"].ToString();
                    // s.ProfilePic = dataReader["ProfilePic"].ToString();
                    s.Score = Convert.ToInt32(dataReader["Score"]);
                    s.Rating = Convert.ToDouble(dataReader["Rating"]);
                    s.Password = dataReader["uPassword"].ToString();
                    s.IsActive = Convert.ToChar(dataReader["isInHold"]);
                    //s.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                }
                //return outputMessage;
                return s;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand SelectUserToLogInWithStoredProcedureWithParameters(String spName, SqlConnection con, string phoneNum, string password, int communityId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNum);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@CommunityID", communityId);
            

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // Details update --> This method Inserts a user to the users table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateUserDetails(User user) //(CCEC)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = UpdateUserDetailsCommandWithStoredProcedure("spUpdateUserDetails", con, user); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand UpdateUserDetailsCommandWithStoredProcedure(String spName, SqlConnection con, User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNum);
            cmd.Parameters.AddWithValue("@uPassword", user.Password);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Street", user.Street);
            cmd.Parameters.AddWithValue("@HomeNumber", user.HomeNum);
            cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            cmd.Parameters.AddWithValue("@uDescription", user.Description);
            cmd.Parameters.AddWithValue("@ProfilePic", user.Description);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // updat user approval to specific community, if this is only community and get deny the user will be delete
        //--------------------------------------------------------------------------------------------------
        public int UpdateUserApprovalStatus(int userId, int communityId, string approvalStatus)  
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = UpdateUserApprovalStatusWithStoredProcedure("spUpdateUserApprovalStatus", con, userId, communityId, approvalStatus); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand UpdateUserApprovalStatusWithStoredProcedure(String spName, SqlConnection con, int userId, int communityId, string approvalStatus)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@CommunityID", communityId);
            cmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
           
            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // Delete a user 
        //--------------------------------------------------------------------------------------------------
        public int DeleteUser(string phoneNum)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = DeleteUserCommandWithStoredProcedure("spDeleteUserFromUsers", con, phoneNum ); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (1-Registration & Details update)
        //----------------------------------------------------------------------------------------
        private SqlCommand DeleteUserCommandWithStoredProcedure(String spName, SqlConnection con, string phoneNum)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text       

            cmd.Parameters.AddWithValue("@PhoneNum", phoneNum);
         
            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // read all users Communities
        //--------------------------------------------------------------------------------------------------
        public List<Community> ReadUsercommunities(int userID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Community> communityList = new List<Community>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllUserCommunitiesStoredProcedureCommand("spReadAllUserCommunities", con, userID);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Community s = new Community();


                    s.CommunityId = Convert.ToInt32(dataReader["CommunityID"]);
                    s.Name = dataReader["cName"].ToString();
                    s.City = dataReader["City"].ToString();
                    s.Location = dataReader["cLocation"].ToString();
                    s.Description = dataReader["CommunityDescription"].ToString();
                    s.PrimaryPic = dataReader["PrimaryPic"].ToString();
                    communityList.Add(s);

                }
                return communityList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadAllUserCommunitiesStoredProcedureCommand(String spName, SqlConnection con, int userID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@userID", userID);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        //Community
        //--------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------
        // read all Communitis
        //--------------------------------------------------------------------------------------------------
        public List<Community> ReadAllCommunitis()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Community> communityList = new List<Community>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllCommunitisStoredProcedureCommand("spReadAllCommunitis", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Community s = new Community();


                    s.CommunityId = Convert.ToInt32(dataReader["CommunityID"]);
                    s.Name = dataReader["cName"].ToString();
                    s.City = dataReader["City"].ToString();
                    s.Location = dataReader["cLocation"].ToString();
                    s.Description = dataReader["CommunityDescription"].ToString();
                    s.PrimaryPic = dataReader["PrimaryPic"].ToString();
                    s.Status = dataReader["CommunityStatus"].ToString();
                    communityList.Add(s);

                }
                return communityList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadAllCommunitisStoredProcedureCommand(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read all Accepted Communities
        //--------------------------------------------------------------------------------------------------
        public List<Community> ReadAllApprovedCommunitis()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Community> communityList = new List<Community>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllCommunitisStoredProcedureCommand("spReadAcceptedCommunities", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Community s = new Community();


                    s.CommunityId = Convert.ToInt32(dataReader["CommunityID"]);
                    s.Name = dataReader["cName"].ToString();
                    s.City = dataReader["City"].ToString();
                    s.Location = dataReader["cLocation"].ToString();
                    s.Description = dataReader["CommunityDescription"].ToString();
                    s.PrimaryPic = dataReader["PrimaryPic"].ToString();
                    s.Status = dataReader["CommunityStatus"].ToString();
                    communityList.Add(s);

                }
                return communityList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // read all Pending  Communities
        //--------------------------------------------------------------------------------------------------
        public List<Community> ReadAllPendingCommunities()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Community> communityList = new List<Community>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllCommunitisStoredProcedureCommand("spReadPendingCommunities", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Community s = new Community();


                    s.CommunityId = Convert.ToInt32(dataReader["CommunityID"]);
                    s.Name = dataReader["cName"].ToString();
                    s.City = dataReader["City"].ToString();
                    s.Location = dataReader["cLocation"].ToString();
                    s.Description = dataReader["CommunityDescription"].ToString();
                    s.PrimaryPic = dataReader["PrimaryPic"].ToString();
                    s.Status = dataReader["CommunityStatus"].ToString();

                    communityList.Add(s);

                }
                return communityList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // read specific  Community
        //--------------------------------------------------------------------------------------------------
        public Community ReadSpecificCommunity(int communityId)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadSpecificCommunitisStoredProcedureCommand("spGetSpecificCommunity", con, communityId);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Community s = new Community();
                while (dataReader.Read())
                {


                    s.CommunityId = Convert.ToInt32(dataReader["CommunityID"]);
                    s.Name = dataReader["cName"].ToString();
                    s.City = dataReader["City"].ToString();
                    s.Location = dataReader["cLocation"].ToString();
                    s.Description = dataReader["CommunityDescription"].ToString();
                    s.PrimaryPic = dataReader["PrimaryPic"].ToString();
                    s.Status = dataReader["CommunityStatus"].ToString();



                }
                return s;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadSpecificCommunitisStoredProcedureCommand(String spName, SqlConnection con, int communityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@communityID", communityID);


            return cmd;
        }
        //--------------------------------------------------------------------------------------------------
        // insert new Community
        //--------------------------------------------------------------------------------------------------

        public int insertNewCommunity(Community community, User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommunityInsertCommandWithStoredProcedure("spInsertNewCommunity", con, community, user); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        //  SqlCommand using a stored procedur
        //----------------------------------------------------------------------------------------
        private SqlCommand CreateCommunityInsertCommandWithStoredProcedure(String spName, SqlConnection con, Community community ,User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNum);
            cmd.Parameters.AddWithValue("@uPassword", user.Password);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@CityUser", user.City);
            cmd.Parameters.AddWithValue("@Street", user.Street);
            cmd.Parameters.AddWithValue("@HomeNumber", user.HomeNum);
            cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            cmd.Parameters.AddWithValue("@uDescription", user.Description);
            cmd.Parameters.AddWithValue("@ProfilePic", user.Description);

            cmd.Parameters.AddWithValue("@cName", community.Name);
            cmd.Parameters.AddWithValue("@City", community.City);
            cmd.Parameters.AddWithValue("@cLocation", community.Location);
            cmd.Parameters.AddWithValue("@CommunityDescription", community.Description);
            cmd.Parameters.AddWithValue("@PrimaryPic", community.PrimaryPic);
          

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // updat community approval 
        //--------------------------------------------------------------------------------------------------
        public int UpdateCommunityApprovalStatus (int communityId, string approvalStatus)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = UpdateCommunityApprovalStatusWithStoredProcedure("spUpdateCommunityApprovalStatus", con, communityId, approvalStatus); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand UpdateCommunityApprovalStatusWithStoredProcedure(String spName, SqlConnection con, int communityId, string approvalStatus)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@CommunityID", communityId);
            cmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // update community Details 
        //--------------------------------------------------------------------------------------------------
        public int UpdateCommunityDetails(Community community)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = UpdateCommunityDetailsWithStoredProcedure("spUpdateCommunityDetails", con, community); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand UpdateCommunityDetailsWithStoredProcedure(String spName, SqlConnection con, Community community)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@CommunityID", community.CommunityId);
            cmd.Parameters.AddWithValue("@cName", community.Name);
            cmd.Parameters.AddWithValue("@City", community.City);
            cmd.Parameters.AddWithValue("@cLocation", community.Location);
            cmd.Parameters.AddWithValue("@CommunityDescription", community.Description);
            cmd.Parameters.AddWithValue("@PrimaryPic", community.PrimaryPic);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // Delete a Community 
        //--------------------------------------------------------------------------------------------------
        public int deleteCommunity(int CommunityID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = DeleteCommunityCommandWithStoredProcedure("spDeleteCommunity", con, CommunityID); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure to delete a community
        //----------------------------------------------------------------------------------------
        private SqlCommand DeleteCommunityCommandWithStoredProcedure(String spName, SqlConnection con, int CommunityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text       

            cmd.Parameters.AddWithValue("@CommunityID", CommunityID);

            return cmd;
        }


        //----------------------------
        //Request for help
        //----------------------------

        //--------------------------------------------------------------------------------------------------
        // insert new Request
        //--------------------------------------------------------------------------------------------------

        public int InsertNewReq(RequestForHelp request)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReqInsertCommandWithStoredProcedure("spInsertRequest", con, request); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        //  SqlCommand using a stored procedur
        //----------------------------------------------------------------------------------------
        private SqlCommand ReqInsertCommandWithStoredProcedure(String spName, SqlConnection con, RequestForHelp request)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            //cmd.Parameters.AddWithValue("@PostDate", request.PostDate);
            //cmd.Parameters.AddWithValue("@PostTime", request.PostTime);
            cmd.Parameters.AddWithValue("@DueDate", request.DueDate);
            cmd.Parameters.AddWithValue("@DueTime", Convert.ToDateTime(request.DueDate.ToString("HH:mm")).TimeOfDay);
            cmd.Parameters.AddWithValue("@ReqDescription", request.Description);
            cmd.Parameters.AddWithValue("@CategoryID", request.CategoryId);
            cmd.Parameters.AddWithValue("@UserID", request.UserReqID);   
            cmd.Parameters.AddWithValue("@CommunityID", request.CommunityID);   


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // Delete Request
        //--------------------------------------------------------------------------------------------------

        public int DeleteReq(int requestId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReqDeletCommandWithStoredProcedure("spDeleteSpecificReq", con, requestId); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        //  SqlCommand using a stored procedur
        //----------------------------------------------------------------------------------------
        private SqlCommand ReqDeletCommandWithStoredProcedure(String spName, SqlConnection con, int reqID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@ReqID", reqID);   



            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read Active Req by category
        //--------------------------------------------------------------------------------------------------
        public List<RequestForHelp> readAllActiveCategoryReq(int CategoryID, int CommunityID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<RequestForHelp> ReqList = new List<RequestForHelp>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadActiveReqByCategoryStoredProcedureCommand("spFilterRequestsByCategoryID", con, CategoryID, CommunityID);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    RequestForHelp request = new RequestForHelp();

                    request.ReqID = Convert.ToInt32(dataReader["ReqID"]);
                    request.CommunityID = Convert.ToInt32(dataReader["CommunityID"]);
                    request.DueDate = Convert.ToDateTime(dataReader["DueDate"]);
                    request.DueTime = dataReader["DueTime"].ToString(); 
                    request.PostDate = Convert.ToDateTime(dataReader["PostDate"]);
                    request.PostTime = dataReader["PostTime"].ToString();
                    request.Description = dataReader["ReqDescription"].ToString();
                    request.CategoryId = Convert.ToInt32(dataReader["CategoryID"]);
                    request.UserReqID = Convert.ToInt32(dataReader["UserID"]);




                    ReqList.Add(request);

                }
                return ReqList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadActiveReqByCategoryStoredProcedureCommand(String spName, SqlConnection con, int categoryID, int CommunityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
            cmd.Parameters.AddWithValue("@CommunityID", CommunityID);


            return cmd;
        }
        //--------------------------------------------------------------------------------------------------
        // read all categories
        //--------------------------------------------------------------------------------------------------
        public List<Category> GetAllCategories()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Category> CategoryList = new List<Category>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllCategoryStoredProcedureCommand("spReadAllCategories", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Category category = new Category();

                    category.CategoyID = Convert.ToInt32(dataReader["CategoryID"]);
                    category.CatName = dataReader["catName"].ToString();
                    category.MaxScore = Convert.ToInt32(dataReader["MaxScore"]);


                    CategoryList.Add(category);

                }
                return CategoryList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadAllCategoryStoredProcedureCommand(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

          

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read All Active Req 
        //--------------------------------------------------------------------------------------------------
        public List<RequestForHelp> GetAllActiveReqInCommunity(int communityID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<RequestForHelp> ReqList = new List<RequestForHelp>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            cmd = ReadActiveReqStoredProcedureCommand("spGetActiveRequests", con, communityID);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    RequestForHelp request = new RequestForHelp();

                    request.ReqID = Convert.ToInt32(dataReader["ReqID"]);
                    request.CommunityID = Convert.ToInt32(dataReader["CommunityID"]);
                    
                    request.DueDate = Convert.ToDateTime(dataReader["DueDate"]);
                    request.DueTime = dataReader["DueTime"].ToString();
                    request.PostDate = Convert.ToDateTime(dataReader["PostDate"]);
                    request.PostTime = dataReader["PostTime"].ToString(); 
                    //request.PostTime = dataReader.GetTimeSpan(2);
                    request.Description = dataReader["ReqDescription"].ToString();
                    // request.GotHelp = Convert.ToChar(dataReader["GotHelp"]);
                    request.CategoryId = Convert.ToInt32(dataReader["CategoryID"]);
                    request.UserReqID = Convert.ToInt32(dataReader["UserID"]);




                    ReqList.Add(request);

                }
                return ReqList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadActiveReqStoredProcedureCommand(String spName, SqlConnection con, int communityID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@communityID", communityID);


            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // Details update of the request
        //--------------------------------------------------------------------------------------------------
        public int UpdateRequestDetails(RequestForHelp request) //(CCEC)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReqUpdateCommandWithStoredProcedure("spUpdateRequest", con, request); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private SqlCommand ReqUpdateCommandWithStoredProcedure(String spName, SqlConnection con, RequestForHelp request)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            //cmd.Parameters.AddWithValue("@PostDate", request.PostDate);
            cmd.Parameters.AddWithValue("@ReqID", request.ReqID);
            cmd.Parameters.AddWithValue("@DueDate", request.DueDate);
            cmd.Parameters.AddWithValue("@DueTime", Convert.ToDateTime(request.DueDate.ToString("HH:mm")).TimeOfDay);
            cmd.Parameters.AddWithValue("@ReqDescription", request.Description);
            cmd.Parameters.AddWithValue("@CategoryID", request.CategoryId);
            cmd.Parameters.AddWithValue("@CommunityID", request.CommunityID);


            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // Details update user status: Reject', 'accepted', 'closed'. reject-delete from wantToAsisst table, accept-change the status,
        // closed- GotHelp in RequestForAssistanceApp tern to 't' and all the other people that want to asisst are delete exept the ont
        // that close
        //--------------------------------------------------------------------------------------------------
        public int UpdateWantToAsisstStatus(int userId, int reqId, string approvalStatus) //(CCEC)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = UpdateWantToAsisstStatusCommandWithStoredProcedure("spUpdateWantToAsisstStatus", con, userId, reqId, approvalStatus); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private SqlCommand UpdateWantToAsisstStatusCommandWithStoredProcedure(String spName, SqlConnection con, int userId, int reqId, string approvalStatus)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            //cmd.Parameters.AddWithValue("@PostDate", request.PostDate);
            cmd.Parameters.AddWithValue("@ReqID", reqId);
            cmd.Parameters.AddWithValue("@Status", approvalStatus);
            cmd.Parameters.AddWithValue("@UserID", userId);
         


            return cmd;
        }




        //--------------------------------------------------------------------------------------------------
        // insert new user who want to assist
        //--------------------------------------------------------------------------------------------------

        public int InsertNewUserWantToAssist(int UserID, int ReqID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReqInsertNewUserWantToAssistCommandWithStoredProcedure("spInsertUsersWantToAssist", con, UserID, ReqID); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //----------------------------------------------------------------------------------------
        //  Insert New User Want To Assist
        //----------------------------------------------------------------------------------------
        private SqlCommand ReqInsertNewUserWantToAssistCommandWithStoredProcedure(String spName, SqlConnection con, int UserID, int ReqID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@ReqID", ReqID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
       

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // read all the user that want to assist and waiting
        //--------------------------------------------------------------------------------------------------
        public List<User> GetAllWantToAssistPending(int reqID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> users = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllUserWantToAssistStoredProcedureCommand("spReadAllPandingWantToAssist", con,reqID);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User user = new User();

                    user.UserId = Convert.ToInt32(dataReader["UserID"]);                   
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                  




                    users.Add(user);

                }
                return users;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand ReadAllUserWantToAssistStoredProcedureCommand(String spName, SqlConnection con, int reqID)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@ReqID", reqID);
           


            return cmd;
        }
        //--------------------------------------------------------------------------------------------------
        //read all the user that want to assist and they accepted
        //--------------------------------------------------------------------------------------------------
        public List<User> AllWantToAssistAccepted(int reqID)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> users = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = ReadAllUserWantToAssistStoredProcedureCommand("spReadAllAcceptedWantToAssist", con, reqID);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User user = new User();

                    user.UserId = Convert.ToInt32(dataReader["UserID"]);
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();


                    users.Add(user);


                }
                return users;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }     


    }
}

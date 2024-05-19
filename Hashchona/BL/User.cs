using Hashchona.DAL;

namespace Hashchona.BL
{
    public class User
    {
        int userId;
        string firstName;
        string lastName;
        string phoneNum;
        string password;
        char gender;
        string city;
        string street;
        int homeNum;
        DateTime birthDate;
        string description;        
        //string profilePicture;
        int score;
        double rating;
        char isActive;

        //List<Community> communiyList = new List<Community>();

        public int UserId { get => userId; set => userId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Password { get => password; set => password = value; }
        public char Gender { get => gender; set => gender = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public int HomeNum { get => homeNum; set => homeNum = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Description { get => description; set => description = value; }
        public int Score { get => score; set => score = value; }
        public double Rating { get => rating; set => rating = value; }
        public char IsActive { get => isActive; set => isActive = value; }
      //  public List<Community> CommuniyList { get => communiyList; set => communiyList = value; }

        public User() { }

        public User(int userId, string firstName, string lastName, string phoneNum, string password, char gender, string city, string street,
                    int homeNum, DateTime birthDate, string description, int score, double rating, char isActive )
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNum = phoneNum;
            Password = password;
            Gender = gender;
            City = city;
            Street = street;
            HomeNum = homeNum;
            BirthDate = birthDate;
            Description = description;
            Score = score;
            Rating = rating;
            IsActive = isActive;
            //CommuniyList = communiyList;

        }



        //Insert new user
        public int Insert(int communityID)
        {
            DBservices db = new DBservices();
            return db.InsertUser(this, communityID);
        }

        //Read all users
        public List<User> ReadAllUsers() 
        {
            DBservices db = new DBservices();

            return db.ReadAllUsers(); 
        } 
        
        //Get all the managers of the community 
        public List<User> ReadManagersCommunity(int CommunityID) 
        {
            DBservices db = new DBservices();

            return db.ReadManagersCommunity(CommunityID); 
        }

        //Read User Community
        public List<Community> ReadUsercommunity(int userID)
        {
            DBservices dB = new DBservices();
            return dB.ReadUsercommunities(userID);
        }

        //Read all users that approved in the community
        public List<User> ReadApprovedUsersForCommunity(int communityID) 
        {
            DBservices db = new DBservices();

            return db.ReadAllApprovedUsersForCommunity(communityID); 
        }
        
        //Read all users that waiting to be approved
        public List<User> ReadAllPendingUsersForCommunity(int communityID) 
        {
            DBservices db = new DBservices();

            return db.ReadAllPendingUsersForCommunity(communityID); 
        }

        public User ReadUser(string phoneNum) 
        {
            DBservices db = new DBservices();

            return db.ReadUser(phoneNum); 
        }

        //Update user deatils
        public int UpdateUserDetails() 
        {
            DBservices db = new DBservices();
            return db.UpdateUserDetails(this);
        }
        //Delete user
        public int DeleteForGood(string phoneNum) 
        { 
            DBservices dB = new DBservices();
            return dB.DeleteUser(phoneNum); 
        }

        //Add user to community, the admin is approve the new user to enter the community
        public int addUserToCommunity()
        {
            DBservices db = new DBservices();
            return 1;
        }

        //Remove user from community
        public int removeFromCommmunity()
        {
            DBservices db = new DBservices();
            return 1;
        }
        //Login to community
        public User Login(string phoneNum, string password, int communityId)
        {
            DBservices db = new DBservices();
            return db.Login(phoneNum, password, communityId);
        }

        //Login to community
        //public string Login(string phoneNum, string password, int communityId)
        //{
        //    DBservices db = new DBservices();
        //    return db.Login(phoneNum, password, communityId);
        //}

        //Update User Approval Status
        public int UpdateUserApprovalStatus(int userId, int communityId, string approvalStatus) 
        {
            DBservices db = new DBservices();
           int NumEffected =  db.UpdateUserApprovalStatus(userId, communityId, approvalStatus);

            if (approvalStatus == "Approved")
            {
                Community community = new Community();
                community = db.ReadSpecificCommunity(communityId);
                //communiyList.Add(community);
            }

            return NumEffected; 
        }    

      

       
        
    }

    public class UserLogin
    {
    
        public int CommunityID { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }

    }

    public class InsertUser
    {
        public User userToRegister { get; set; }
        public int communityId { get; set; }
    }
}

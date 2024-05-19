using Hashchona.DAL;

namespace Hashchona.BL
{
    public class RequestForHelp
    {
        int reqID;
        int categoryId;
        int communityID;
        DateTime dueDate;
        string dueTime;  
        DateTime postDate;
        string postTime;
        string description;
        bool gotHelp;
        int userReqID;

        public int ReqID { get => reqID; set => reqID = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public string DueTime { get => dueTime; set => dueTime = value; }
        public DateTime PostDate { get => postDate; set => postDate = value; }
        public string PostTime { get => postTime; set => postTime = value; }
        public string Description { get => description; set => description = value; }
        public bool GotHelp { get => gotHelp; set => gotHelp = value; }
        public int UserReqID { get => userReqID; set => userReqID = value; }
        public int CommunityID { get => communityID; set => communityID = value; }

        public RequestForHelp() { }

        public RequestForHelp(int reqID, int communityID, int categoryId, DateTime dueDate, DateTime postDate,
                                 string description, bool gotHelp, int userReqID)
        {
            ReqID = reqID;
            CategoryId = categoryId;
            DueDate = dueDate;
            DueTime = dueDate.ToString("HH:mm");
            PostDate = postDate;
            PostTime = dueDate.ToString("HH:mm");
            Description = description;
            GotHelp = gotHelp;
            UserReqID = userReqID;
            CommunityID = communityID;
        }


        //insert new request
        public int InsertNewReq(RequestForHelp req)
        {
            DBservices db = new DBservices();
            return db.InsertNewReq(req);
        }
        
        //Get all the categories
        public List<Category> GetAllCategories()
        {
            DBservices db = new DBservices();
            return db.GetAllCategories();
        }
        
        //read all the user that want to assist and waiting
        public List<User> GetAllWantToAssistPending(int reqID)
        {
            DBservices db = new DBservices();
            return db.GetAllWantToAssistPending(reqID);
        }

        //read all the user that want to assist and they accepted
        public List<User> AllWantToAssistAccepted(int reqID)
        {
            DBservices db = new DBservices();
            return db.AllWantToAssistAccepted(reqID);
        }

        //Delete request
        public int DeleteReq(RequestForHelp request)
        {
            DBservices db = new DBservices();   
            return db.DeleteReq(request.ReqID);
        }
        //Delete request
        public List<RequestForHelp> readAllActiveCategoryReq(int categoryID, int CommunityID)
        {
            DBservices db = new DBservices();   
            return db.readAllActiveCategoryReq(categoryID, CommunityID);
        }

        public int UpdateRequest()
        {
            DBservices db = new DBservices();
            return db.UpdateRequestDetails(this);
        }

        public List<RequestForHelp> GetAllActiveReqInCommunity(int CommunityID)
        {
            DBservices db = new DBservices();
            return db.GetAllActiveReqInCommunity(CommunityID);
        }

    }

    public class UsersWantToAssist
    {
        public int UserID { get; set; }
        public int RequestForHelpID { get; set; }

        public int InsertNewUserWantToAssist(int UserID, int ReqID)
        {
            DBservices db = new DBservices();
            return db.InsertNewUserWantToAssist(UserID, ReqID); 
        }
    }

    public class StatusUpdateToReq 
    {
        public int UserID { get; set; }
        public int RequestForHelpID { get; set; }
        public string StatusApproval {  get; set; }

        public int UpdateUserStatusToReq(int userId, int reqId, string approvalStatus)
        {
            DBservices db = new DBservices();
            return db.UpdateWantToAsisstStatus(userId, reqId, approvalStatus);
        }

    }

}

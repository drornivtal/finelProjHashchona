using Hashchona.BL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hashchona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        [Route("ReadAllUsers")]
        public IEnumerable<User> Get()
        {
            User user = new User();
            return user.ReadAllUsers();
        }

        [HttpPost]
        [Route("ReadAllApprovedUsersForCommunity")]
        public IEnumerable<User> ApprovedUsersForCommunity(JsonElement jsonElement)
        {
            int CommunityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());

            User user = new User();
            return user.ReadApprovedUsersForCommunity(CommunityID);
        }

        [HttpPost]
        [Route("ReadAllPendingUsers")]
        public IEnumerable<User> GetAllPendingUsers(JsonElement jsonElement)
        {
            int CommunityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());

            User user = new User();
            return user.ReadAllPendingUsersForCommunity(CommunityID);
        }

        [HttpPost]
        [Route("GetSpecificUser")]
        public User GetSpecificUser(JsonElement jsonElement)
        {
            string phoneNum = Convert.ToString(jsonElement.GetProperty("phoneNum").GetString());
            User user = new User();
            return user.ReadUser(phoneNum);
        } 
        
        [HttpPost]
        [Route("GetAllUserCommunity")]
        public IEnumerable<Community> GetUserCommunity(JsonElement jsonElement)
        {
            int UserId = Convert.ToInt32(jsonElement.GetProperty("UserId").GetInt32());

            User user = new User();
            return user.ReadUsercommunity(UserId);
        }  
        
        [HttpPost]
        [Route("ManagersCommunity")]
        public IEnumerable<User> GetManagersCommunity(JsonElement jsonElement)
        {
            int CommunityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());

            User user = new User();
            return user.ReadManagersCommunity(CommunityID);
        }


        //POST api/<UsersController>
        [HttpPost]
        [Route("InsertNewUser")]
        public int Post(InsertUser insertUser)
        {
            int numEffected = insertUser.userToRegister.Insert(insertUser.communityId);
            return numEffected;
        }

        // POST api/<UsersController>
        //[HttpPost]
        //[Route("InsertNewUser")]

        //public int Post(JsonElement jsonElement)
        //{
        //    JsonElement jsonElement2 = JsonDocument.Parse(jsonElement["userToRegister"]).RootElement;
        //    int communityID = Convert.ToInt32(jsonElement.GetProperty("communityId").GetInt32());
        //    User user = jsonElement.GetProperty("userToRegister").Deserialize<User>();        

        //    int numEffected = user.Insert(communityID);
        //    return numEffected;
        //}



        // PUT api/<UsersController>/5
        [HttpPut]
        [Route("UpdateUserDetails")]
        public IActionResult Put([FromBody]User user)
        {
            int res = user.UpdateUserDetails();
            if (res == 0)
            {
                return NotFound("Failed to update details,Try again!");
            }
            else
                return Ok(res);
        }

       
        // PUT api/<UsersController>/5
        //[HttpPut] 
        //[Route("UpdateUserApproval")]
        //public IActionResult Put(int userId, int communityId, string approvalStatus)
        //{

        //    User user = new User();
        //    int res = user.UpdateUserApprovalStatus(userId, communityId, approvalStatus);
        //    if (res == 0)
        //    {
        //        return NotFound("Failed to update Status,Try again!");
        //    }
        //    else
        //        return Ok(res);
        //} 
        
        [HttpPut] 
        [Route("UpdateUserApproval")]
        public IActionResult Put(JsonElement jsonElement)
        {
            int userId = Convert.ToInt32(jsonElement.GetProperty("userId").GetInt32());
            int communityId = Convert.ToInt32(jsonElement.GetProperty("communityID").GetInt32());
            string approvalStatus = jsonElement.GetProperty("approvalStatus").GetString();

            User user = new User();

            int res = user.UpdateUserApprovalStatus(userId, communityId, approvalStatus);

            if (res == 0)
            {
                return NotFound("Failed to update Status,Try again!");
            }
            else
                return Ok(res);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("DeleteUserForGood")]
        public int Delete(JsonElement jsonElement)
        {
            string phoneNum = Convert.ToString(jsonElement.GetProperty("phoneNum").GetString());

            User user = new User();
            return user.DeleteForGood(phoneNum);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            User resUser = new User();
            resUser = resUser.Login(userLogin.PhoneNum, userLogin.Password, userLogin.CommunityID);
            if (resUser.UserId == 0)
            {
                return NotFound("The user is not registered in the system,try again!");               
            }            
            else
                return Ok(resUser);

        }
    
    }
}

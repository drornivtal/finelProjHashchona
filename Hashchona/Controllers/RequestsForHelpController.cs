using Hashchona.BL;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hashchona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsForHelpController : ControllerBase
    {
        // GET: api/<AssimentsController>
        [HttpGet]
        [Route("ActiveReqByCommunity")]
        public IEnumerable<RequestForHelp> GetAllActiveReqInCommunity(JsonElement jsonElement)
        {
            int communityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());
            RequestForHelp assistance = new RequestForHelp();

            return assistance.GetAllActiveReqInCommunity(communityID);
        }

        // GET api/<AssimentsController>/5
        [HttpGet]
        [Route("GetAllCategories")]
        public IEnumerable<Category> GetAllCategories()
        {
            RequestForHelp assistance = new RequestForHelp();

            return assistance.GetAllCategories();
        }


        //// GET api/<AssimentsController>/5
        //[HttpGet]
        //[Route("ActiveCategoryReq")]
        //public IEnumerable<RequestForHelp> GetAllActiveCategoryReq(int CategoryID, int CommunityID)
        //{

        //    RequestForHelp assistance = new RequestForHelp();
        //    return assistance.readAllActiveCategoryReq(CategoryID, CommunityID);
        //}
        
        // GET api/<AssimentsController>/5
        [HttpGet]
        [Route("ActiveCategoryReq")]
        public IEnumerable<RequestForHelp> GetAllActiveCategoryReq(JsonElement jsonElement)
        {
            int CategoryID = Convert.ToInt32(jsonElement.GetProperty("CategoryID").GetInt32());
            int CommunityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());
            RequestForHelp assistance = new RequestForHelp();
            return assistance.readAllActiveCategoryReq(CategoryID, CommunityID);
        }


        // GET api/<AssimentsController>/5
        [HttpGet]
        [Route("AllWantToAssistPending")]
        public IEnumerable<User> GetAllWantToAssistPending(JsonElement jsonElement)
        {
            int reqID = Convert.ToInt32(jsonElement.GetProperty("reqID").GetInt32());

            RequestForHelp assistance = new RequestForHelp();
            return assistance.GetAllWantToAssistPending(reqID);
        }  
        // GET api/<AssimentsController>/5
        [HttpGet]
        [Route("AllWantToAssistAccepted")]
        public IEnumerable<User> AllWantToAssistAccepted(JsonElement jsonElement)
        {
            int reqID = Convert.ToInt32(jsonElement.GetProperty("reqID").GetInt32());
            RequestForHelp assistance = new RequestForHelp();
            return assistance.AllWantToAssistAccepted(reqID);
        }

        // POST api/<AssimentsController>
        [HttpPost]
        [Route("postNewReq")]
        public int PostNewReq([FromBody] RequestForHelp request)
        {

            return request.InsertNewReq(request);
        }
        
        
        // POST api/<AssimentsController>
        [HttpPost]
        [Route("usersWantToAssist")]
        public int PostNewAssistUser([FromBody] UsersWantToAssist usersWantToAssist)
        {
            return usersWantToAssist.InsertNewUserWantToAssist(usersWantToAssist.UserID, usersWantToAssist.RequestForHelpID);
        }

        // PUT api/<AssimentsController>/5
        [HttpPut]
        [Route("updateUserStatusToReq")]
        public IActionResult PutUserStatusToReq(StatusUpdateToReq statusUpdateToReq)
        {
            int res = statusUpdateToReq.UpdateUserStatusToReq(statusUpdateToReq.UserID, statusUpdateToReq.RequestForHelpID, statusUpdateToReq.StatusApproval);
            if (res == 0)
            {
                return NotFound("Failed to update details,Try again!");
            }
            else
                return Ok(res);
        }

          // PUT api/<AssimentsController>/5
        [HttpPut]
        [Route("updateReqDeatails")]
        public IActionResult PutReq(RequestForHelp request)
        {
            int res = request.UpdateRequest();
            if (res == 0)
            {
                return NotFound("Failed to update details,Try again!");
            }
            else
                return Ok(res);
        }

        // DELETE api/<AssimentsController>/5
        [HttpDelete]
        [Route("DeleteReq")]
        public int DeleteReq(RequestForHelp request)
        {
            return request.DeleteReq(request);
        }
    }
}

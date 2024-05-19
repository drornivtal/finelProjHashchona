using Hashchona.BL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hashchona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        //Get all the Communities
        // GET: api/<CommunitiesController>
        [HttpGet]
        [Route("ReadAllCommunities")]
        public IEnumerable<Community> GetAllCommunities()
        {
            Community community = new Community();
            return community.ReadAllCommunities();
        }

        // GET api/<CommunitiesController>/5
        [HttpGet]
        [Route("ReadApprovedCommunities")]
        public IEnumerable<Community> GetApprovedCommunities()
        {
            Community community = new Community();
            return community.ReadAllApprovedCommunities();
        }        
      

        // GET api/<CommunitiesController>/5
        [HttpGet]
        [Route("ReadPendingCommunities")]
        public IEnumerable<Community> GetPendingCommunities()
        {
            Community community = new Community();
            return community.ReadAllPendingCommunities();
        }

        // POST api/<CommunitiesController>
        [HttpPost]
        [Route("InsertNewCommunity")]

        public int Post(InsertCommunity insertCommunity)
        {
            return insertCommunity.Insert(insertCommunity.UserManager, insertCommunity.Community);
        }

        // PUT api/<CommunitiesController>/5
        [HttpPut]
        [Route("UpdateCommunityApprovedStatus")]
        public int PutCommunityApprovedStatus(CommunityApprovedStatus communityApprovedStatus)
        {
            Community community = new Community();
            return community.UpdateCommunityApprovalStatus(communityApprovedStatus.communityID, communityApprovedStatus.approvalStatus);
        }

        // PUT api/<CommunitiesController>/5
        //[HttpPut]
        //[Route("UpdateCommunityApprovedStatus")]
        //public int PutCommunityApprovedStatus(JsonElement jsonElement)
        //{
        //    int communityID = Convert.ToInt32(jsonElement.GetProperty("CommunityID").GetInt32());
        //    string approvalStatus = jsonElement.GetProperty("approvalStatus").GetString();
        //    Community community = new Community();
        //    return community.UpdateCommunityApprovalStatus(communityID, approvalStatus);
        //}

        // PUT api/<CommunitiesController>/5
        [HttpPut]
        [Route("UpdateCommunityDetails")]
        public int CommunityDetails(Community community)
        {
            return community.UpdateCommunityDetails();
        }

        // DELETE api/<CommunitiesController>/5
        [HttpDelete]
        [Route("DeleteCommunity")]
        public int Delete(Community community)
        {
            return community.deleteCommunity();
        }
    }
}

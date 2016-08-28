﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventTracker.Models;
using Microsoft.AspNet.Identity;

namespace EventTracker.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost] public IHttpActionResult Follow ([FromBody]string followeeId)

        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followeeId))
                return BadRequest("Attendance already exists!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followeeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}

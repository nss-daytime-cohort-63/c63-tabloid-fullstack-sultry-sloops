﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Tabloid.Models;
using Tabloid.Repositories;

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }
        [HttpGet("Admins")]
        public IActionResult GetAdmins()
        {
            return Ok(_userProfileRepository.GetAllAdmins());
        }

        [HttpGet("Deactive")]
        public IActionResult GetDeactive()
        {
            return Ok(_userProfileRepository.GetAllDeactive());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userProfileRepository.Get(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("Firebase/{firebaseUserId}")]
        public IActionResult GetUserProfile(string firebaseUserId)
        {
            return Ok(_userProfileRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }
            _userProfileRepository.Update(user);
            return NoContent();
        }

        [HttpPut("Deactivate/{id}")]
        public IActionResult DeactivateUser(int id, UserProfile user) {
            if(id != user.Id)
            {
                return BadRequest();
            }
            _userProfileRepository.DeactivateUser(user);
            return NoContent();
        }

        [HttpPut("Reactivate/{id}")]
        public IActionResult ReactivateUser(int id, UserProfile user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }
            _userProfileRepository.ReactivateUser(user);
            return NoContent();
        }


        [HttpPost]
        public IActionResult Post(UserProfile userProfile)
        {
            userProfile.CreateDateTime = DateTime.Now;
            userProfile.UserTypeId = UserType.AUTHOR_ID;
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                nameof(GetUserProfile),
                new { firebaseUserId = userProfile.FirebaseUserId },
                userProfile);
        }

        [HttpGet("Me")]
        public IActionResult Me()
        {
            var userProfile = GetCurrentUserProfile();
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}

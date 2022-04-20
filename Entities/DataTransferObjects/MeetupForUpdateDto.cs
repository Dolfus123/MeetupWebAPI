﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MeetupWebAPI.Entities.DataTransferObjects
{
    public class MeetupForUpdateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Date of Meet is required")]
        public DateTime DateOfMeet { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters")]
        public string Description { get; set; }
    }
}

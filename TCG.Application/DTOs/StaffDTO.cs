/*
Program: Local Games Store Management System
Filename: StaffDTO.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using TCG.Application.Interfaces;

namespace TCG.Application.Dtos
{
    public class StaffDto : IHasId
    {
        public int StaffId { get; set; }
        
        int IHasId.Id => StaffId;

        [Required(ErrorMessage = "First name is required.")]
        public string StaffFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        public string StaffSurname { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address.")]
        public string StaffEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string StaffPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^(\+?[1-9]\d{1,14}|0\d{9,14})$", ErrorMessage = "Invalid mobile number.")]
        public string StaffMobile { get; set; } = string.Empty;
    }
}

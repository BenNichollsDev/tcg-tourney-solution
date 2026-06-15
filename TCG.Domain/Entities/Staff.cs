//
// Program: Local Games Store Management System
// Filename: Staff.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//
    using System.ComponentModel.DataAnnotations.Schema;

    namespace TCG.Domain.Entities
    {
        public partial class Staff
        {
            [Column("staff_id")]
            public int StaffId { get; set; }

            [Column("staff_first_name")]
            public string StaffFirstName { get; set; } = string.Empty;

            [Column("staff_surname")]
            public string StaffSurname { get; set; } = string.Empty;

            [Column("staff_email")]
            public string StaffEmail { get; set; } = string.Empty;

            [Column("staff_password")]
            public string StaffPassword { get; set; } = string.Empty;

            [Column("staff_mobile")]
            public string StaffMobile { get; set; } = string.Empty;
        }
    }


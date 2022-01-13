﻿using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Note
    {
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public int TicketId { get; set; }
    }
}

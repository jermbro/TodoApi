﻿using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public partial class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsComplete { get; set; }
    }
}

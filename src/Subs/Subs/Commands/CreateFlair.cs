﻿using System;
using Infrastructure.Messaging;

namespace Subs.Commands
{
    public class CreateFlair : ICommandReturns<CreateFlairResponse>
    { 
        public Guid? FlairId { get; set; }

        public string Text { get; set; }

        public string CssClass { get; set; }

        public bool TextEditable { get; set; }

        public int Type { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public string IpAddress { get; set; } 

    }

    public class CreateFlairResponse
    {
        public string Error { get; set; }

        public Guid? FlairId { get; set; }

        public string Text { get; set; }

        public string CssClass { get; set; }
    }
}
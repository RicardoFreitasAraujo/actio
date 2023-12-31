﻿using Actio.Common.Exceptions;
using System;
using System.Data;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Activity()
        {

        }

        public Activity(Guid id, string name, string category, string description, Guid userId, DateTime createdAt)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_activiyy_name", "Activity name can not be empty.");
            }

            Id = id;
            Name = name;
            Category = category;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}

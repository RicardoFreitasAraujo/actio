using Actio.Common.Exceptions;
using System;

namespace Actio.Api.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Activity()
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

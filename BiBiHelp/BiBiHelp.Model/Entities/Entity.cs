using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Interfaces.Common;

namespace BiBiHelp.Model.Entities
{
    public class Entity : IEntity, IEquatable<Entity>, IEqualityComparer<Entity>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool Equals(Entity other)
        {
            return other != null && other.Id == this.Id;
        }

        public bool Equals(Entity x, Entity y)
        {
            return x != null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(Entity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

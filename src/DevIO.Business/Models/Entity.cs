using System;

namespace DevIO.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        /* EF Relations */
        public Guid Id { get; set; }
    }
}

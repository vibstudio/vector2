namespace Eng.Vector.Domain.Model
{
    #region Namespaces

    using System;
    using System.Collections.Generic;

    #endregion
    
    public class ElaborationKey
    {
        public Guid ID { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public static ElaborationKey CreateKey()
        {
            return new ElaborationKey();
        }

        private ElaborationKey()
        {
            ID = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

        public class EqualityComparer : IEqualityComparer<ElaborationKey>
        {
            public bool Equals(ElaborationKey x, ElaborationKey y)
            {
                return x.ID == y.ID;
            }

            public int GetHashCode(ElaborationKey obj)
            {
                return obj.CreatedOn.GetHashCode() * 3729;
            }
        }
    }
}

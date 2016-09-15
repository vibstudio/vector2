#region Namespaces

using System.Collections.Generic;
using System.Linq;

using Eng.Collections;

#endregion

namespace Eng.Vector.Domain.Model
{
    public class TransferOutputResultCollection : ResultCollectionBase<OperationResult<string>>
    {
        public override bool ThereAreErrors
        {
            get { return this.Any(x => !x.IsPerformed); }
        }

        protected override IList<string> Errors
        {
            get { return (from x in this where !x.IsPerformed select x.Message).ToList(); }
        }

        public override string ToString()
        {
            return FlatErrors;
        }
    }
}
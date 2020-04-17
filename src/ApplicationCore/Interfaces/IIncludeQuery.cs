using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Helpers.Query;

namespace PharmacyNetwork.ApplicationCore.Interfaces
{
    public interface IIncludeQuery
    {
        Dictionary<IIncludeQuery, string> PathMap { get; }

        IncludeVisitor Visitor { get; }

        HashSet<string> Paths { get; }
    }

    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
    {

    }
}

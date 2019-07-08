using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface IParentRelationship<T> : IParentRelationshipSetter
    {
        T Parent { get; set; }
    }
}

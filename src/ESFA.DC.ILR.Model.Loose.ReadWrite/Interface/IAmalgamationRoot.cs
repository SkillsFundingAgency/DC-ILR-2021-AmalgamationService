using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface IAmalgamationRoot
    {
        string Filename { get; set; }

        ILooseMessage Message { get; set; }
    }
}

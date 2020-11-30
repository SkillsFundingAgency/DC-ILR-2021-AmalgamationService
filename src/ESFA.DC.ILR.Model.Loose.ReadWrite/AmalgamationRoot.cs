using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public class AmalgamationRoot : IAmalgamationRoot
    {
        public string Filename { get; set; }
        public Message Message { get; set; }
    }
}

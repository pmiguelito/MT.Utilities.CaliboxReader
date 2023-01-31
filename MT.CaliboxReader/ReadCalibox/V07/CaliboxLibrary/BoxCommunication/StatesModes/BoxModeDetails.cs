using System;

namespace CaliboxLibrary
{
    public abstract class BoxModeDetails
    {
        public BoxModeDetails()
        {
            Hex = "FF";
            Desc = "NotDefined";
            IsError = true;
        }
        public BoxModeDetails(string hex, string desc, bool isError=false)
        {
            Hex = hex;
            Desc = desc;
            IsError = isError;
        }

        public override string ToString()
        {
            return Desc;
        }

        public virtual bool Equals(BoxModeDetails x, BoxModeDetails y)
        {
            return x.Hex == y.Hex;
        }
        public virtual bool Equals(BoxModeDetails x)
        {
            return this.Hex == x.Hex;
        }

        public readonly string Hex;
        public readonly string Desc;
        public readonly bool IsError;
        
        
        
    }
}

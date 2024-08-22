using System;

namespace CaliboxLibrary
{

    /// <summary>
    /// used for <see cref="BoxMode"/>
    /// used for <see cref="BoxCalMode"/>
    /// used for <see cref="BoxErrorMode"/>
    /// </summary>
    public abstract class BoxModeDetails : IEquatable<BoxModeDetails>
    {
        public readonly string Hex;
        public readonly string Desc;
        public readonly bool IsError;

        public BoxModeDetails()
        {
            Hex = "FF";
            Desc = "NotDefined";
            IsError = true;
        }

        public BoxModeDetails(string hex, string desc, bool isError = false)
        {
            Hex = hex;
            Desc = desc;
            IsError = isError;
        }

        public override string ToString()
        {
            return Desc;
        }

        public static bool operator ==(BoxModeDetails x, BoxModeDetails y)
        {
            if (y is null)
            {
                return x is null;
            }
            return y.Hex == x.Hex;
        }

        public static bool operator !=(BoxModeDetails x, BoxModeDetails y)
        {
            return (x == y) == false;
        }

        public virtual bool Equals(BoxModeDetails x, BoxModeDetails y)
        {
            return x.Hex == y.Hex;
        }

        public virtual bool Equals(BoxModeDetails x)
        {
            return this.Hex == x.Hex;
        }

    }
}

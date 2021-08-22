using System;

namespace LoxoneParser.Model
{
    [System.Diagnostics.DebuggerDisplay("{value}")]
    public struct LoxoneId : IComparable
    {
        private string value;

        public LoxoneId(string value)
        {
            this.value = value;
        }

        public static implicit operator LoxoneId(string id)
        {
            return new LoxoneId(id);
        }

        public static implicit operator string(LoxoneId id)
        {
            return id.value;
        }

        public override string ToString()
        {
            return value;
        }

        public int CompareTo(object obj)
        {
            return value.CompareTo(obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        public int Length { get; }
        public byte this[int index] => values[index];

        private readonly byte[] values;
        private readonly int primeNumber = 1039;
        private int hashCode;

        public ReadonlyBytes(params byte[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            hashCode = 0;
            Length = values.Length;
            this.values = values.ToArray();
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
                yield return values[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => 
            GetEnumerator();

        IEnumerator<byte> IEnumerable<byte>.GetEnumerator() => 
            GetEnumerator();

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType()) return false;
            var other = obj as ReadonlyBytes;
            if (Length != other.Length) return false;
            for (var i = 0; i < Length; ++i)
                if (values[i] != other[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            if (hashCode != 0)
                return hashCode;

            unchecked
            {
                var powerOfPrimeNumber = 1;
                for (var i = 0; i < Length; ++i)
                {
                    hashCode += (values[i] + 1) * powerOfPrimeNumber;
                    powerOfPrimeNumber *= primeNumber;
                }
                return hashCode;
            }
        }

        public override string ToString()
        {
            var valuesToStr = values.Select(value => value.ToString());
            return $"[{string.Join(", ", valuesToStr)}]";
        }
    }
}
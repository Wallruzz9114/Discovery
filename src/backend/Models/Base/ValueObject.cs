using System.Collections.Generic;
using System.Linq;

namespace Models.Base
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null) return false;
            return left is null || right is null;
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object randomObject)
        {
            if (randomObject is null || randomObject.GetType() != GetType()) return false;

            ValueObject valueObject = (ValueObject)randomObject;
            IEnumerator<object> currentValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> valueObjectValues = valueObject.GetAtomicValues().GetEnumerator();

            while (currentValues.MoveNext() && valueObjectValues.MoveNext())
            {
                if (currentValues.Current is null ^ valueObjectValues.Current is null) return false;
                if (currentValues.Current is not null && !currentValues.Current.Equals(valueObjectValues.Current))
                {
                    return false;
                }
            }

            return !currentValues.MoveNext() && !valueObjectValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
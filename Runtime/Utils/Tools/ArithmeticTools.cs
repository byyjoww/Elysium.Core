using UnityEngine;

namespace Elysium.Utils.Arithmetic
{
    public abstract class Number<T> where T : Number<T>
    {
        public abstract T Add(T _other);
        public abstract T Subtract(T _other);
        public abstract T Multiply(T _other);
        public abstract T Divide(T _other);

        public abstract float ToFloat();
    }

    public class Float : Number<Float>
    {
        public Float(float _value)
        {
            Value = _value;
        }

        public readonly float Value;

        public override Float Add(Float _other)
        {
            return new Float(this.Value + _other.Value);
        }

        public override Float Divide(Float _other)
        {
            return new Float(this.Value / _other.Value);
        }

        public override Float Multiply(Float _other)
        {
            return new Float(this.Value * _other.Value);
        }

        public override Float Subtract(Float _other)
        {
            return new Float(this.Value - _other.Value);
        }

        public override float ToFloat()
        {
            return this.Value;
        }
    }

    public class Integer : Number<Integer>
    {
        public Integer(int value)
        {
            Value = value;
        }

        public readonly int Value;

        public override Integer Add(Integer _other)
        {
            return new Integer(this.Value + _other.Value);
        }

        public override Integer Divide(Integer _other)
        {
            return new Integer(this.Value / _other.Value);
        }

        public override Integer Multiply(Integer _other)
        {
            return new Integer(this.Value * _other.Value);
        }

        public override Integer Subtract(Integer _other)
        {
            return new Integer(this.Value - _other.Value);
        }

        public override float ToFloat()
        {
            return this.Value;
        }
    }

    public static class NumberExt
    {
        #region RIGHT_OPERATOR
        public static Float Add<T>(this Float _a, T _b) where T : Number<T>
        {
            return new Float(_a.Value + _b.ToFloat());
        }

        public static Float Subtract<T>(this Float _a, T _b) where T : Number<T>
        {
            return new Float(_a.Value - _b.ToFloat());
        }

        public static Float Multiply<T>(this Float _a, T _b) where T : Number<T>
        {
            return new Float(_a.Value * _b.ToFloat());
        }

        public static Float Divide<T>(this Float _a, T _b) where T : Number<T>
        {
            return new Float(_a.Value / _b.ToFloat());
        }
        #endregion

        #region LEFT_OPERATOR
        public static Float Add<T>(this T _a, Float _b) where T : Number<T>
        {
            return new Float(_b.Value + _a.ToFloat());
        }

        public static Float Subtract<T>(this T _a, Float _b) where T : Number<T>
        {
            return new Float(_b.Value - _a.ToFloat());
        }

        public static Float Multiply<T>(this T _a, Float _b) where T : Number<T>
        {
            return new Float(_b.Value * _a.ToFloat());
        }

        public static Float Divide<T>(this T _a, Float _b) where T : Number<T>
        {
            return new Float(_b.Value / _a.ToFloat());
        }
        #endregion

    }
}

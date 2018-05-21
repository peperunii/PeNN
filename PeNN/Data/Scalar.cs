using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public abstract class Scalar<T>
    {
        public T Value { get; set; }

        public static Scalar<T> operator +(Scalar<T> c1, Scalar<T> c2)
        {
            return c1.Add(c2);
        }

        public static Scalar<T> operator -(Scalar<T> c1, Scalar<T> c2)
        {
            return c1.Subtract(c2);
        }

        public static Scalar<T> operator -(Scalar<T> c1)
        {
            return c1.Negate();
        }


        public static Scalar<T> operator *(Scalar<T> c1, Scalar<T> c2)
        {
            return c1.Multiply(c2);
        }

        public static Scalar<T> operator /(Scalar<T> c1, Scalar<T> c2)
        {
            return c1.Divide(c2);
        }

        protected Scalar(T value)
        {
            Value = value;
        }

        public abstract Scalar<T> Divide(Scalar<T> value);
        public abstract Scalar<T> Multiply(Scalar<T> value);
        public abstract Scalar<T> Add(Scalar<T> value);
        public abstract Scalar<T> Negate();
        public abstract Scalar<T> Subtract(Scalar<T> value);
    }
}

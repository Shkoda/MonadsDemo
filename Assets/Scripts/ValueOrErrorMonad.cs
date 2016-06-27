using System;

namespace Assets.Scripts
{
    public class ValueOrErrorMonad<T>
    {
        public T Value
        {
            get
            {
                if (IsError)
                {
                    throw new InvalidOperationException();
                }
                return _value;
            }
        }

        public bool IsError { get; }

        public string Error { get; }

        private readonly T _value;

        private ValueOrErrorMonad(T value, string error, bool isError)
        {
            _value = value;
            Error = error;
            IsError = isError;
        }

        #region Monad defenition: Unit and Bind
        public static ValueOrErrorMonad<T> FromValue(T value)
        {
            return new ValueOrErrorMonad<T>(value, string.Empty, false);
        }

        public static ValueOrErrorMonad<T> FromError(string error)
        {
            return new ValueOrErrorMonad<T>(default(T), error, true);
        }

        public ValueOrErrorMonad<R> Bind<R>(Func<T, ValueOrErrorMonad<R>> function)
        {
            return !IsError ? function(Value) : ValueOrErrorMonad<R>.FromError(Error);
        }
        #endregion

        #region Bind alias SelectMany and Select
        public ValueOrErrorMonad<R> SelectMany<R>(Func<T, ValueOrErrorMonad<R>> func)
        {
            return Bind(func);
        }

        public ValueOrErrorMonad<R> Select<R>(Func<T, R> func)
        {
            Func<T, ValueOrErrorMonad<R>> tryCatchFunction = x =>
            {
                try
                {
                    return ValueOrErrorMonad<R>.FromValue(func(x));
                }
                catch (Exception e)
                {
                    return ValueOrErrorMonad<R>.FromError(e.Message);
                }
            };

            return Bind(tryCatchFunction);
        }
        #endregion

        #region Bind alias: FlatMap and Flat

        public ValueOrErrorMonad<R> FlatMap<R>(Func<T, ValueOrErrorMonad<R>> func)
        {
            return Bind(func);
        }
        public ValueOrErrorMonad<R> Map<R>(Func<T, R> func)
        {
            return Select(func);
        }
        #endregion
    }
}
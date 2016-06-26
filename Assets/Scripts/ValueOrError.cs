using System;
using UnityEngine;
using System.Collections;


public struct ValueOrError<T>
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

    public static ValueOrError<T> FromValue(T value)
    {
        return new ValueOrError<T>(value, string.Empty, false);
    }

    public static ValueOrError<T> FromError(string error)
    {
        return new ValueOrError<T>(default(T), error, true);
    }

    private ValueOrError(T value, string error, bool isError)
    {
        _value = value;
        Error = error;
        IsError = isError;
    }


    public static ValueOrError<int> AddOne(ValueOrError<int> value)
    {
        if (!value.IsError)
        {
            int unwrapped = value.Value;
            int result = unwrapped + 1;
            return ValueOrError<int>.FromValue(result);
        }

        return ValueOrError<int>.FromError($"AddOne failed: {value.Error}");
    }

    public  ValueOrError<R> ApplyFunction<R>(Func<T, R> function)
    {
        if (!IsError)
        {
            T unwrapped = Value;
            R result = function(unwrapped);
            return ValueOrError<R>.FromValue(result);
        }
        return ValueOrError<R>.FromError(Error);
    }

    public static ValueOrError<int> AddOneFunctional(ValueOrError<int> valueOrError)
    {
        return valueOrError.ApplyFunction(i => i+=1);
    }

    public static ValueOrError<float> SafeLogarithm(float value)
    {
        return value > 0
            ? ValueOrError < float > .FromValue(Mathf.Log(value))
            : ValueOrError < float > .FromError($"{value}<0");
    }

    public  ValueOrError<R> ApplySpecialFunction<R>( Func<T, ValueOrError<R>> function)
    {
        if (!IsError)
        {
            T unwrapped = Value;
            ValueOrError<R> result = function(unwrapped);
            return result;
        }
        return ValueOrError<R>.FromError(Error);
    }

    public ValueOrError<R> ApplyFunctionInCoolWay<R>(Func<T, R> function)
    {
        return ApplySpecialFunction<R>(unwrapped => ValueOrError<R>.FromValue(function(unwrapped)));
    }

 

}
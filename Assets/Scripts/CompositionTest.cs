using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CompositionTest : MonoBehaviour
    {
        private static Func<int, long> cube = x => (long) x*x*x;
        private static Func<long, double> halve = y => y/2.0;
        private static Func<int, double> halvedCube = Compose(cube, halve);
        private static Func<X, Z> Compose<X, Y, Z>(Func<X, Y> f, Func<Y, Z> g) => x => g(f(x));

        //--------------------------

        private Func<int, ValueOrError<double>> logFunction = x => x > 0
            ? ValueOrError<double>.FromValue(Math.Log(x))
            : ValueOrError<double>.FromError("x<0");

        private Func<double, ValueOrError<decimal>> toDecimalFunction = y => Math.Abs(y) < (double) decimal.MaxValue
            ? ValueOrError<decimal>.FromValue((decimal) y)
            : ValueOrError<decimal>.FromError("illegal arg");
    
        private static Func<X, ValueOrError<Z>> ComposeSpecial<X, Y, Z>(Func<X, ValueOrError<Y>> f, Func<Y, ValueOrError<Z>> g) 
            => x => f(x).ApplySpecialFunction(g);

        private void Start()
        {
            /*
            Func<X, M<Y>> f = whatever;
            Func<Y, M<Z>> g = whatever;

            M<X> mx = whatever;
            M<Y> my = ApplySpecialFunction(mx, f);

            M<Z> mz1 = ApplySpecialFunction(my, g);
            Func<X, M<Z>> h = ComposeSpecial(f, g);
            M<Z> mz2 = ApplySpecialFunction(mx, h);
            */


            var argOrError = ValueOrError<int>.FromValue(121);

            var logOrError = argOrError.ApplySpecialFunction(logFunction);
            var decimalLogOrErrorFromFunctionSequence = logOrError.ApplySpecialFunction(toDecimalFunction);

            var composedLogAndToDecimal = ComposeSpecial(logFunction, toDecimalFunction);
            var decimalLogOrErrorFromComposed = argOrError.ApplySpecialFunction(composedLogAndToDecimal);

            Debug.Log($"decimalLogOrErrorFromFunctionSequence {decimalLogOrErrorFromFunctionSequence.Value} = " +
                       $"decimalLogOrErrorFromComposed={decimalLogOrErrorFromComposed.Value}");
        }
    }
}
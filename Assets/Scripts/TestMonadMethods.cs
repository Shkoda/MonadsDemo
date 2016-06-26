using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
  public  class TestMonadMethods : MonoBehaviour
    {
      void Start()
      {
          ValueIdentityTest();
            RawAndWrappedTest();

      }
        void ValueIdentityTest()
        {
            var input = ValueOrError<int>.FromValue(38);
            var output = input.ApplySpecialFunction(ValueOrError<int>.FromValue);

            Debug.Log($"input {input.Value} == output {output.Value}");
        }

      void RawAndWrappedTest()
      {
            int original = 123;
            var resultFromRawValue = ValueOrError<float>.SafeLogarithm(original);

            var wrapped = ValueOrError<float>.FromValue(original);
            var resultFromWrappedValue = wrapped.ApplySpecialFunction(ValueOrError<float>.SafeLogarithm);

            Debug.Log($"resultFromRawValue {resultFromRawValue.Value} == resultFromWrappedValue {resultFromWrappedValue.Value}");
       }

    }
}

using UnityEngine;

namespace Assets.Scripts
{
    public class TestFunctions : MonoBehaviour
    {
        void Test()
        {
            ValueOrError<float> logOrError = ValueOrError<float>.SafeLogarithm(5);
            ValueOrError<ValueOrError<float>> logLogOrError = logOrError.ApplyFunction(ValueOrError<float>.SafeLogarithm);    
            ValueOrError<float> flatternLogLogOrError = logOrError.ApplySpecialFunction(ValueOrError<float>.SafeLogarithm);    
            
                    
        }
    }
}
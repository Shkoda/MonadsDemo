using System;

namespace Assets.Scripts
{
    public class OnDemand<T>
    {
        public T Value => _func();
        private Func<T> _func;

        public OnDemand(Func<T> func)
        {
            _func = func;
        }

        public static OnDemand<T> Create<T>(T item)
        {
            return new OnDemand<T>(() => item);
        }

        public static OnDemand<int> AddOne(OnDemand<int> onDemand)
        {
            var unwrapped = onDemand._func();
            var result = unwrapped + 1;
            return Create(result);
        }

       public static OnDemand<int> AddOneCorrect(OnDemand<int> onDemand)
        {
            return new OnDemand<int>( () =>
            {
                int unwrapped = onDemand.Value;
                int result = unwrapped + 1;
                return result;
            });
        }
    }
}
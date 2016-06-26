using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
   public class WhereTest : MonoBehaviour
   {
        private static Func<int, long> cube = x => (long)x * x * x;
        void Start()
       {
       var list = new List<int> { 1, 2, 3 };
           var bigNumbers = list.Where1(e => e > 100);
            Debug.Log(bigNumbers.Count());

            var enumerable = list.SelectMany(x => bigNumbers);


       }
       
   }
}

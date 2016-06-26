using System;
using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class TestOnDemand : MonoBehaviour
{
    private void Start()
    {
       
        StartCoroutine(AddOneCoroutine(4));
    }

    private IEnumerator AddOneCoroutine(int count)
    {
        var currentTime = new OnDemand<int>(() => DateTime.Now.Second);
        var currentTimePlusOne = OnDemand<int>.AddOne(currentTime);
        var currentTimePlusOneCorrect = OnDemand<int>.AddOneCorrect(currentTime);
        for (var i = 0; i < count; i++)
        {
           Debug.Log($"currentTime: {currentTime.Value}, currentTimePlusOneWrong: {currentTimePlusOne.Value}, " +
                      $"currentTimePlusOneCorrect: {currentTimePlusOneCorrect.Value}");
          yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
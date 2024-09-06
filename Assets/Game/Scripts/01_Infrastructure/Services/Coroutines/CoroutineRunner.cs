using System.Collections;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.Coroutines
{
    public sealed class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator coroutine) => 
            StartCoroutine(coroutine);

        void ICoroutineRunner.StopCoroutine(Coroutine coroutine) => 
            StopCoroutine(coroutine);
    }
}
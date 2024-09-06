using System.Collections;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.Coroutines
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
    }
}
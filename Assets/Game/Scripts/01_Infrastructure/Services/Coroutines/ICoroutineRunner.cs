using System.Collections;
using UnityEngine;

namespace Game.Scripts._01_Infrastructure.Services.Coroutines
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
    }
}
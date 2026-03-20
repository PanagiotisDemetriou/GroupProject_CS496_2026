using System.Collections;
using UnityEngine;

public class ToxicSmokeController : MonoBehaviour
{
    [SerializeField] private ParticleSystem toxicSmoke;
    
    public bool IsBusy { get; private set; }

    public IEnumerator DisableSmokeForSeconds(float disableTime, float cooldownTime)
    {
        if (IsBusy)
            yield break;

        IsBusy = true;

        if (toxicSmoke != null)
            toxicSmoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        yield return new WaitForSeconds(disableTime);

        if (toxicSmoke != null)
            toxicSmoke.Play();

        yield return new WaitForSeconds(cooldownTime);

        IsBusy = false;
    }
}

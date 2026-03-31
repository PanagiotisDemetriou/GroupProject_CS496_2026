using UnityEngine;
public class FootstepController : MonoBehaviour
{
[SerializeField] AudioSource footstepSource;
[SerializeField] AudioClip[] footstepClips;
public void PlayFootstep()
{
    if (footstepClips.Length == 0) return;
    AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
    footstepSource.PlayOneShot(clip);
}
}

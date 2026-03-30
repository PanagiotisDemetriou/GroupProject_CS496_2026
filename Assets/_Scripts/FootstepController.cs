using UnityEngine;
public class FootstepController : MonoBehaviour
{
[SerializeField] AudioSource footstepSource;
[SerializeField] AudioClip[] footstepClips;
public void PlayFootstep()
{
    if (footstepClips.Length == 0) return;
    Debug.Log("Playing footstep sound");
    AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
    footstepSource.PlayOneShot(clip);
}
}

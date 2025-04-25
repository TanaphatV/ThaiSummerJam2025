using UnityEngine;

public class CoinSFX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

            // Find the object named "Coin Sound"
            GameObject coinSoundObject = GameObject.Find("Coin Sound");

            if (coinSoundObject != null)
            {
                // Get the AudioSource component and play the sound
                AudioSource audioSource = coinSoundObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.LogError("No AudioSource found on 'Coin Sound' object!");
                }
            }
            else
            {
                Debug.LogError("Object named 'Coin Sound' not found in the scene!");
            }
        
    }
}
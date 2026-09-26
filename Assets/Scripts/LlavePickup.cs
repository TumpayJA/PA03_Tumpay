using UnityEngine;

// Pon este script sobre un objeto pequeño (una gema, orbe, o algún prop) escondido
// en la zona decorativa. Requiere un Collider con "Is Trigger" activado.
public class LlavePickup : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;       // opcional
    [SerializeField] private ParticleSystem pickupEffect;   // opcional

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameState.tieneLlave = true;
        Debug.Log("Llave recogida.");

        if (audioSource != null) audioSource.Play();
        if (pickupEffect != null) pickupEffect.Play();

        Destroy(gameObject, 0.15f);
    }
}

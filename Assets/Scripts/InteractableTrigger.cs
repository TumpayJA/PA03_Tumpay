using UnityEngine;

// Este script va sobre el objeto interactivo de la escena (un cofre, un orbe, una puerta, etc).
// Requisitos en el Inspector del objeto:
//   1) Un Collider (Box/Sphere) con "Is Trigger" activado.
//   2) El GameObject del jugador debe tener el tag "Player".
public class InteractableTrigger : MonoBehaviour
{
    [Header("Referencias (todas opcionales)")]
    [SerializeField] private Renderer targetRenderer;       // para cambiar de color al activarse
    [SerializeField] private AudioSource audioSource;        // sonido al activarse
    [SerializeField] private ParticleSystem activationEffect; // partículas al activarse
    [SerializeField] private GameObject victoryPanel;         // UI que aparece al llegar a la meta (opcional)

    [Header("Configuración")]
    [SerializeField] private Color colorActivado = Color.yellow;
    [SerializeField] private bool desactivarAlUsar = false; // ej: una moneda que desaparece al recogerla
    [SerializeField] private bool requiereLlave = false;     // si es true, no se activa sin antes recoger la llave

    private Color colorOriginal;
    private bool yaActivado = false;

    private void Start()
    {
        if (targetRenderer != null)
        {
            colorOriginal = targetRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (yaActivado && desactivarAlUsar) return;

        if (requiereLlave && !GameState.tieneLlave)
        {
            Debug.Log("Necesitas encontrar la llave primero.");
            return;
        }

        Activar();
    }

    private void Activar()
    {
        yaActivado = true;
        Debug.Log("Objeto interactivo activado: " + gameObject.name);

        if (targetRenderer != null)
        {
            targetRenderer.material.color = colorActivado;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (activationEffect != null)
        {
            activationEffect.Play();
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (desactivarAlUsar)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (desactivarAlUsar) return;

        // Descomenta si quieres que vuelva a su color original al alejarse:
        // if (targetRenderer != null) targetRenderer.material.color = colorOriginal;
    }
}

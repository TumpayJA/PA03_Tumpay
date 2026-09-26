using UnityEngine;

// IMPORTANTE: este script va en un GameObject VACÍO ubicado en la bisagra de la puerta
// (uno de los postes verticales de la reja), NO en el modelo de la puerta directamente.
// La puerta (PT_Modular_Gate_Wood_01) debe ser HIJO de ese vacío.
// Se abre sola en cuanto el jugador recoge la llave (GameState.tieneLlave).
public class GateOpener : MonoBehaviour
{
    [SerializeField] private float anguloAbierto = 90f; // si abre para el lado equivocado, prueba con -90
    [SerializeField] private float velocidad = 60f;     // grados por segundo

    private bool abriendo = false;
    private bool yaAbierto = false;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;

    private void Start()
    {
        rotacionCerrada = transform.rotation;
        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0f, anguloAbierto, 0f);
    }

    private void Update()
    {
        if (!abriendo && GameState.tieneLlave)
        {
            abriendo = true;
        }

        if (abriendo && !yaAbierto)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionAbierta, velocidad * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotacionAbierta) < 0.5f)
            {
                yaAbierto = true;
            }
        }
    }
}

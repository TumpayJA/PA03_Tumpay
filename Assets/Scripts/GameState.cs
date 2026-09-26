using UnityEngine;

public static class GameState
{
    public static bool tieneLlave = false;

    // Se ejecuta automáticamente antes de cargar la escena,
    // sin importar si Domain Reload está activado o no.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetState()
    {
        tieneLlave = false;
    }
}
using System.Collections;
using UnityEngine;

public class Aviso : MonoBehaviour
{
    public GameObject textoCanvas; // Referencia al canvas que contiene el texto
    public GameObject canvasWin;
    public bool seRequiereLlave = true;

    public AudioClip campanaSound;

    public AudioSource CampanaAudioSource;

    public float minCamInterval = 5f;
    public float maxCamInterval = 15f;

    public void Start()
    {
        CampanaAudioSource.spatialBlend = 1.0f;
        CampanaAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        CampanaAudioSource.minDistance = 1f;
        CampanaAudioSource.maxDistance = 50f;
        CampanaAudioSource.playOnAwake = false;  // Asegurarse de que no se reproduce automáticamente

        StartCoroutine(PlayCampanaSoundRandomly());
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si el jugador tiene una llave en su inventario
            bool tieneLlave = TieneLlave();

            if (tieneLlave)
            {
                // Si el jugador tiene una llave, mostrar el canvas de victoria
                MostrarCanvasWin();
                seRequiereLlave = false; // Ya no se requiere llave
            }
            else if (!tieneLlave && seRequiereLlave)
            {
                // Si el jugador no tiene una llave y se requiere una, mostrar el texto temporal
                MostrarTextoTemporal();
            }
        }
    }


    bool TieneLlave()
    {
        foreach (var item in PlayerInventory.Instance.items)
        {
            if (item.type == ItemType.keys && item.amount > 1)
            {
                return true; // El jugador mas de una llave
            }
        }
        return false; // El jugador no tiene llaves
    }


    void MostrarTextoTemporal()
    {
        // Activar el canvas con el texto
        textoCanvas.SetActive(true);

        // Desactivar el canvas después de 3 segundos
        StartCoroutine(EsperarYDesactivar());
    }

    public void MostrarCanvasWin()
    {
        // Asegurarse de que se activa el canvas de victoria correcto
        canvasWin.SetActive(true);
    }



    IEnumerator EsperarYDesactivar()
    {
        yield return new WaitForSeconds(7f);

        // Desactivar el canvas después de 3 segundos
        textoCanvas.SetActive(false);
    }

    IEnumerator PlayCampanaSoundRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minCamInterval, maxCamInterval);
            yield return new WaitForSeconds(waitTime);

            if (!CampanaAudioSource.isPlaying) // Asegurarse de que no se esté reproduciendo ya el sonido
            {
                CampanaAudioSource.clip = campanaSound;
                CampanaAudioSource.Play();
            }
        }
    }
}

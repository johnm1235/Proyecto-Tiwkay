using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RedTintEffect : MonoBehaviour
{
    public Material redTintMaterial;
    public Transform player;
    public Transform enemy;
    public float maxDistance = 10f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (redTintMaterial != null && player != null && enemy != null)
        {
            // Calcula la distancia entre el jugador y el enemigo
            float distance = Vector3.Distance(player.position, enemy.position);
            
            // Calcula el valor de _RedAmount basado en la distancia
            float redAmount = Mathf.Clamp01(1.0f - (distance / maxDistance));
            
            // Asigna el valor de _RedAmount al material
            redTintMaterial.SetFloat("_RedAmount", redAmount);
            
            // Aplica el efecto de tintado rojo
            Graphics.Blit(source, destination, redTintMaterial);
        }
        else
        {
            // Si falta alguna referencia, renderiza la imagen original
            Graphics.Blit(source, destination);
        }
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public SpriteRenderer hablando;
    public SpriteRenderer sonriendo;
    public SpriteRenderer calmada;
    public SpriteRenderer molesta;
    public SpriteRenderer enojada;

    // Cambia el sprite actual basado en la emoción pasada
    public void CambiarSprite(string emocion)
    {
        ResetearSprites();  // Desactiva todos los sprites antes de habilitar uno nuevo

        switch (emocion)
        {
            case "hablando":
                if (hablando != null) // Verifica si el sprite está asignado
                {
                    hablando.enabled = true;
                }
                break;
            case "sonriendo":
                if (sonriendo != null)
                {
                    sonriendo.enabled = true;
                }
                break;
            case "calmada":
                if (calmada != null)
                {
                    calmada.enabled = true;
                }
                break;
            case "molesta":
                if (molesta != null)
                {
                    molesta.enabled = true;
                }
                break;
            case "enojada":
                if (enojada != null)
                {
                    enojada.enabled = true;
                }
                break;
            default:
                if (calmada != null)
                {
                    calmada.enabled = true;  // Si la emoción no coincide, activar el sprite por defecto
                }
                break;
        }
    }

    // Desactiva todos los sprites
    void ResetearSprites()
    {
        if (hablando != null)
        {
            hablando.enabled = false;
        }
        if (sonriendo != null)
        {
            sonriendo.enabled = false;
        }
        if (calmada != null)
        {
            calmada.enabled = false;
        }
        if (molesta != null)
        {
            molesta.enabled = false;
        }
        if (enojada != null)
        {
            enojada.enabled = false;
        }
    }
}

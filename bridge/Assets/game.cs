using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementador: Interfaz de color
interface IColor
{
    Color ObtenerColor();
}

// Implementación concreta: Rojo
class Rojo : IColor
{
    public Color ObtenerColor()
    {
        return Color.red;
    }
}

// Implementación concreta: Azul
class Azul : IColor
{
    public Color ObtenerColor()
    {
        return Color.blue;
    }
}

// Implementación concreta: Blanco
class Blanco : IColor
{
    public Color ObtenerColor()
    {
        return Color.white;
    }
}

// Abstracción: Forma
abstract class Forma
{
    protected GameObject forma;
    protected IColor color;

    public Forma(IColor color)
    {
        this.color = color;
    }

    public abstract void CrearForma(Vector2 posicion);

    public void CambiarColor(IColor nuevoColor)
    {
        color = nuevoColor;
        if (forma != null)
        {
            forma.GetComponent<SpriteRenderer>().color = color.ObtenerColor();
        }
    }
}

// Implementación concreta: Círculo
class Circulo : Forma
{
    public Circulo(IColor color) : base(color) { }

    public override void CrearForma(Vector2 posicion)
    {
        forma = new GameObject("Circulo");
        var spriteRenderer = forma.AddComponent<SpriteRenderer>();
        forma.AddComponent<CircleCollider2D>();
        forma.transform.position = posicion;

        // Crear un círculo simple con un sprite blanco
        Texture2D textura = new Texture2D(128, 128);
        for (int x = 0; x < 128; x++)
        {
            for (int y = 0; y < 128; y++)
            {
                float dx = x - 64;
                float dy = y - 64;
                if (dx * dx + dy * dy < 64 * 64)
                {
                    textura.SetPixel(x, y, Color.white);
                }
                else
                {
                    textura.SetPixel(x, y, Color.clear);
                }
            }
        }
        textura.Apply();
        spriteRenderer.sprite = Sprite.Create(textura, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
        spriteRenderer.color = color.ObtenerColor();
    }
}

// Implementación concreta: Cuadrado
class Cuadrado : Forma
{
    public Cuadrado(IColor color) : base(color) { }

    public override void CrearForma(Vector2 posicion)
    {
        forma = new GameObject("Cuadrado");
        var spriteRenderer = forma.AddComponent<SpriteRenderer>();
        forma.AddComponent<BoxCollider2D>();
        forma.transform.position = posicion;

        // Crear un sprite simple para el cuadrado
        Texture2D textura = new Texture2D(128, 128);
        for (int x = 0; x < 128; x++)
        {
            for (int y = 0; y < 128; y++)
            {
                textura.SetPixel(x, y, Color.white);
            }
        }
        textura.Apply();
        spriteRenderer.sprite = Sprite.Create(textura, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
        spriteRenderer.color = color.ObtenerColor();
    }
}

public class game : MonoBehaviour
{
    private Forma ultimaForma;

    // Método para obtener una posición aleatoria en el área visible de la cámara
    private Vector2 ObtenerPosicionAleatoria()
    {
        float x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                               Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        float y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                               Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        return new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        // Crear un cuadrado al presionar la tecla '1'
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ultimaForma = new Cuadrado(new Blanco());
            ultimaForma.CrearForma(ObtenerPosicionAleatoria());
        }

        // Crear un círculo al presionar la tecla '2'
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ultimaForma = new Circulo(new Blanco());
            ultimaForma.CrearForma(ObtenerPosicionAleatoria());
        }

        // Cambiar el color al presionar 'R' (Rojo)
        if (Input.GetKeyDown(KeyCode.R) && ultimaForma != null)
        {
            ultimaForma.CambiarColor(new Rojo());
        }

        // Cambiar el color al presionar 'A' (Azul)
        if (Input.GetKeyDown(KeyCode.A) && ultimaForma != null)
        {
            ultimaForma.CambiarColor(new Azul());
        }
    }
}

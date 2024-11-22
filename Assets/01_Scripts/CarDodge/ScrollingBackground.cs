using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Reinicia la posición para un efecto infinito
        if (transform.position.y < -10f)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
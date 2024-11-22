using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float laneWidth = 2f;

    private int currentLane = 1; // Comienza en el segundo carril (index 1)

    void Update()
    {
        // Movimiento entre carriles
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
            MoveToLane();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 3)
        {
            currentLane++;
            MoveToLane();
        }

        // Movimiento hacia adelante y atrás
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * vertical * speed * Time.deltaTime);
    }

    void MoveToLane()
    {
        Vector3 targetPosition = new Vector3(currentLane * laneWidth - laneWidth * 1.5f, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float laneOffset = 20f;
    public float laneChangeSpeed = 10f;

    private Rigidbody rb;
    private bool isGameOver = false;

    private int currentLane = 1; // 0 = sol, 1 = orta, 2 = sağ
    private float targetX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetX = 0f; // orta
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;


        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);

        Vector3 newPos = rb.position;
        newPos.x = Mathf.Lerp(rb.position.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, 2);

        targetX = (currentLane - 1) * laneOffset;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            Destroy(other.gameObject);
        }
    }


}

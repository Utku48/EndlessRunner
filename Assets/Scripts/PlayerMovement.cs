using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float laneOffset = 20f;
    public float laneChangeSpeed = 10f;

    private Rigidbody rb;
    private bool isGameOver = false;

    [Header("Score")]
    public int score = 0;
    public float scoreInterval = 2f;
    public int scorePerInterval = 5;

    private int currentLane = 1; // 0 = sol, 1 = orta, 2 = sağ
    private float targetX;
    private float scoreTimer = 0f;


    [Header("GameOverUi")]
    public GameOverUi gameOverUi;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetX = 0f; // orta
    }

    void Update()
    {
        if (isGameOver) return;

        // Lane input
        if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);
        else if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);

        // Score
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            score += scorePerInterval;
            scoreTimer = 0f;
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
            isGameOver = true;
            rb.velocity = Vector3.zero;

            gameOverUi.Show(score);
        }
    }


}

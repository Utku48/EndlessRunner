using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Boost")]
    public bool isBoostActive = false;
    public float boostMultiplier = 2f;
    public float boostDuration = 3f;

    public float normalSpeed;
    private bool hasBoostCharge = false;
    private int nextBoostScore = 25;

    public TextMeshProUGUI gScoreText;

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

        normalSpeed = forwardSpeed;
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

            if (score > nextBoostScore)
            {
                hasBoostCharge = true;
                nextBoostScore += 25;
            }
        }


        if (hasBoostCharge && !isBoostActive &&
            Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(BoostCoroutine());

        }

        gScoreText.text = "Score: " + score;
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


    IEnumerator BoostCoroutine()
    {
        isBoostActive = true;
        forwardSpeed = normalSpeed * boostMultiplier;

        // (İstersen burada efekt, ses, FOV vs. eklersin)

        yield return new WaitForSeconds(5f);

        forwardSpeed = normalSpeed;
        isBoostActive = false;
    }



}

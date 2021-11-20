using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool jump = false;
    Rigidbody rb;
    Transform cameraHolder;
    public AudioSource sonido;
    private bool isGameOver = false;

    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;

    Vector3 vec;

    [SerializeField] GameObject[] obstacles;
    [SerializeField] float obstacleDistance;
    [SerializeField] float obstaclePosY;
    [SerializeField] int numOfObstacles;

    int lenght;

    void buildObstacles()
    {
        lenght = obstacles.Length;
        vec.z = 56.4f;
        for (int i = 0; i < numOfObstacles; i++)
        {
            vec.z += obstacleDistance;
            vec.y = Random.Range(-obstaclePosY, obstaclePosY);

            Instantiate(obstacles[Random.Range(0, lenght)], vec, Quaternion.identity);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraHolder = Camera.main.transform.parent;
        sonido = GetComponent<AudioSource>();
        buildObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.touchCount == 1)
            if (Input.touches[0].phase == TouchPhase.Began)
        {
            jump = true;
        }

        if (!isGameOver)
        {
            float playerY = transform.position.y;
            
            if (playerY < -32f || playerY > 32f)
            {
                isGameOver = true;
                Invoke("RestartGame", 0.3f);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward*playerSpeed*Time.fixedDeltaTime);
        if (jump)
        {
            rb.AddForce(Vector3.up*jumpForce*1000*Time.fixedDeltaTime);
            jump = false;
        }
    }

    private void LateUpdate()
    {
        vec.x = cameraHolder.transform.position.x;
        vec.y = cameraHolder.transform.position.y;
        vec.z = transform.position.z;

        cameraHolder.transform.position = vec;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Game over");
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        sonido.Play();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        Invoke("RestartGame", 0.7f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

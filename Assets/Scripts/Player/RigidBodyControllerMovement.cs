using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class RigidBodyControllerMovement : MonoBehaviour
{
    [Header("Player physics")]
    private Rigidbody rigidBody;
    [SerializeField]
    private Collider capsuleCollider;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float returnToStartSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpFrequency;
    private float timer;

    [Header("References")]
    [SerializeField]
    private CharacterControllerMovement characterControllerScript;
    [SerializeField]
    private GameObject world;
    [SerializeField]
    private VerticalMovingPlatformController[] platformController;
    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private WorldRotationController[] worldRotation;
    [SerializeField]
    private Animator animator;

    [Header("Health")]
    private HealthController healthController;
    private bool isDead;
    public bool isHealthReduced;

    private int touchedPlatformCounter;
    private int touchedRotationPlatformCounter;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioSourceTeleport;
    [SerializeField]
    private AudioSource audioSourceDeath;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.localPosition = startPosition.localPosition;
        touchedPlatformCounter = -1;
        touchedRotationPlatformCounter = -1;
        healthController = FindObjectOfType<HealthController>();
    }

    private void Update()
    {
        Rotate();
        if (!isDead)
        {
            PlayerDies();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        rigidBody.AddForce(transform.forward * inputVertical * movementSpeed + transform.right * inputHorizontal * movementSpeed);

        if (rigidBody.velocity.magnitude >= maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseHorizontal * rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && timer <= 0)
        {
            animator.SetTrigger("Jump");
            rigidBody.AddForce(Vector3.up * jumpHeight * 100);
            timer = jumpFrequency;
        }
    }

    private void ReduceHealth()
    {
        if (!isHealthReduced)
        {
            healthController.HealthReduce();
            transform.DOMove(startPosition.position, returnToStartSpeed);
            isHealthReduced = true;
            StartCoroutine(WaitPlayerIsReturnedToStart());
        }
    }

    private void PlayerDies()
    {
        if (healthController.health <= 0)
        {
            animator.SetBool("IsDead", true);
            audioSourceDeath.Play();
            StartCoroutine(WaitDeathSound());
            isDead = true;
        }
    }

    public void CountingPlatforms()
    {
        touchedPlatformCounter++;
    }

    public void CountingRotationPlatforms()
    {
        touchedRotationPlatformCounter++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
            platformController[touchedPlatformCounter].enabled = true;
        }
        else if (other.gameObject.CompareTag("Rotate"))
        {
            worldRotation[touchedRotationPlatformCounter].enabled = true;
        }
        else if (other.gameObject.CompareTag("Stairs"))
        {
            characterControllerScript.enabled = true;
            characterControllerScript.characterController.enabled = true;
            capsuleCollider.enabled = false;
            this.enabled = false;
        }
        else if (other.gameObject.CompareTag("Teleport"))
        {
            audioSourceTeleport.Play();
            StartCoroutine(WaitTeleportSound());
        }
        else if (other.gameObject.CompareTag("Save"))
        {
            startPosition.position = transform.position;
        }
        else if (other.gameObject.CompareTag("Death"))
        {
            ReduceHealth();
            touchedPlatformCounter = -1;
            touchedRotationPlatformCounter = -1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = world.transform;
        }
        else if (other.gameObject.CompareTag("Stairs"))
        {
            characterControllerScript.enabled = false;
            characterControllerScript.characterController.enabled = false;
        }
    }

    private IEnumerator WaitPlayerIsReturnedToStart()
    {
        yield return new WaitUntil(() => transform.position == startPosition.position);
        isHealthReduced = false;
    }

    private IEnumerator WaitTeleportSound()
    {
        yield return new WaitWhile(() => audioSourceTeleport.isPlaying);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator WaitDeathSound()
    {
        yield return new WaitForSeconds(4f);
        UIController.singletonUI.ShowGameOverPanel();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using Lean.Pool;

public class CharacterControllerMovement : MonoBehaviour
{
    [Header("Movement config")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    [Header("References")]
    public CharacterController characterController;
    [SerializeField]
    private SoundController soundController;
    [SerializeField]
    private RigidBodyControllerMovement rigidBodyController;
    [SerializeField]
    private Collider capsuleCollider;
    [SerializeField]
    private GameObject particleShot;
    [SerializeField]
    private float fireFrequency;
    [SerializeField]
    private Transform shootPosition;
    [SerializeField]
    private Animator animator;

    private float timer;

    [Header("Health")]
    [SerializeField]
    private HealthController healthController;
    private bool isDead;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioSourceTeleport;
    [SerializeField]
    private AudioSource audioSourceShot;
    [SerializeField]
    private AudioSource audioSourceDeath;

    private void Update()
    {
        Move();
        Rotate();
        Shoot();
        if (!isDead)
        {
            PlayerDies();
        }
    }

    private void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }
        animator.SetFloat("Speed", moveDirection.magnitude);
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseHorizontal * rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (!Cursor.visible)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (Input.GetButton("Fire1") && timer <= 0)
            {
                audioSourceShot.Play();
                LeanPool.Spawn(particleShot, shootPosition.position, transform.rotation);
                timer = fireFrequency;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleport1"))
        {
            audioSourceTeleport.Play();
            StartCoroutine(WaitTeleportSound());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            rigidBodyController.enabled = true;
            capsuleCollider.enabled = true;
            this.enabled = false;
            characterController.enabled = false;
        }
    }

    private IEnumerator WaitTeleportSound()
    {
        yield return new WaitWhile(() => audioSourceTeleport.isPlaying);
        rigidBodyController.enabled = true;
        capsuleCollider.enabled = true;
        this.enabled = false;
        characterController.enabled = false;
        SceneManager.LoadScene("BonusLevel");
    }

    private IEnumerator WaitDeathSound()
    {
        yield return new WaitWhile(() => audioSourceDeath.isPlaying);
        UIController.singletonUI.ShowGameOverPanel();
    }
}

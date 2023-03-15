using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;
    public float staminaDepletionRate = 20f;
    public Slider staminaSlider;
    public float staminaCooldown = 2f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isRunning;
    private float currentStamina;

    private bool isCooldown;
    private float currentCooldownTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentStamina > 0)
            {
                isRunning = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }

    void FixedUpdate()
    {
        float speed = moveSpeed;

        if (isRunning && currentStamina > 0)
        {
            speed = runSpeed;
            currentStamina -= staminaDepletionRate * Time.fixedDeltaTime;
            UpdateStaminaUI();
        }
        else
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            UpdateStaminaUI();
        }

        if (isCooldown)
        {
            currentCooldownTime -= Time.fixedDeltaTime;

            if (currentCooldownTime <= 0f)
            {
                isCooldown = false;
            }
        }

        if (currentStamina <= 0f)
        {
            isCooldown = true;
            currentCooldownTime = staminaCooldown;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * speed * Time.fixedDeltaTime);
    }

    void UpdateStaminaUI()
    {
        staminaSlider.value = currentStamina / maxStamina;
    }
}


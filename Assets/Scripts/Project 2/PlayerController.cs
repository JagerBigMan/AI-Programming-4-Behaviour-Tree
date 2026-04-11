using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 8f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    [Header("Debuff")]
    public float speedCap = 5f;

    private bool isDebuffed = false;
    private float debuffTimer = 0f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (isDebuffed)
        {
            debuffTimer -= Time.deltaTime;
            if (debuffTimer <= 0f)
            {
                isDebuffed = false;
            }
        }

        if (DroneComboSystem.playerFrozen)     // Freeze player completely until patrol drone applies debuff
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * vertical + right * horizontal;

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        float currentMoveSpeed = isDebuffed
            ? Mathf.Min(baseMoveSpeed, speedCap)
            : baseMoveSpeed;

        controller.Move(move * currentMoveSpeed * Time.deltaTime);
    }

    public void ApplyHandcuffDebuff(float duration)
    {
        isDebuffed = true;
        debuffTimer = duration;
    }
}
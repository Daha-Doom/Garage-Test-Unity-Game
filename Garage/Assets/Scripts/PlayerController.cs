using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class PlayersController : MonoBehaviour
{
    private PlayerControls inputActions;

    private CharacterController controller;

    [SerializeField] private Transform interactTarget;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera cam;

    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] public float lookSensitivity = 1.0f;

    private bool takedObj = false;

    private float xRotation = 0f;

    private Vector3 velocity;

    private Transform playerTransform;

    private void Awake()
    {
        inputActions = new PlayerControls();

        playerTransform = transform;
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        OnMovement();
        OnLooking();
        OnInteract();
    }

    private void OnLooking()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * lookSensitivity * Time.deltaTime;
        float lookY = looking.y * lookSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * lookX);
    }

    private void OnMovement()
    {
        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move * movementSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnInteract()
    {
        //if (Physics.Raycast(cam.transform.position, playerTransform.forward, out RaycastHit hitt, interactDistance))
        //    if (hitt.transform.TryGetComponent(out InteractableObject interactable))
        //        Debug.Log("“ÛÚ");

        if (GetPlayerIntaract())
            if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
                return;
            else if (hit.transform.TryGetComponent(out InteractableObject interactable))
            {
                if (!takedObj)
                {
                    interactable.Intaract(interactTarget);
                    takedObj = true;
                }
                else
                {
                    interactable.Drop();
                    takedObj = false;
                }
            }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool GetPlayerIntaract()
    {
        return inputActions.Player.Interact.triggered;
    }
}


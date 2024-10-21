using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    Rigidbody rigidbody;
    private Transform target;
    [SerializeField] private float lerpSpeed = 10f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Intaract(Transform interactTarget)
    {
        target = interactTarget;

        rigidbody.useGravity = false;
    }

    public void Drop()
    {
        target = null;
        rigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
        }
    }
}

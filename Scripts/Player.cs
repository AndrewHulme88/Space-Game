using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    private Vector2 rawInput;
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;

    private void Start()
    {
        InitializeScreenBounds();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 delta = rawInput * playerMoveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minScreenBounds.x + paddingLeft, maxScreenBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minScreenBounds.y + paddingBottom, maxScreenBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }

    void InitializeScreenBounds()
    {
        Camera mainCamera = Camera.main;
        minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}

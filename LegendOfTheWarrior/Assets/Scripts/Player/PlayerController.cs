using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputSystem inputControl;
    public Vector2 inputDirection;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inputControl = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        // 获取到的数值
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

}

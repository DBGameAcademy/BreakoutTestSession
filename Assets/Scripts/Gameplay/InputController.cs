using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public enum InputAction
    {
        AnyKey,
        MenuBack,
        Left,
        Right,
        Fire
    }

    public bool GetInput(InputAction _action)
    {
        switch (_action)
        {
            case InputAction.AnyKey:
                return Input.anyKey;

            case InputAction.MenuBack:
                return Input.GetKey(KeyCode.Escape);

            case InputAction.Left:
                return (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));

            case InputAction.Right:
                return (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
                
            case InputAction.Fire:
                return Input.GetKey(KeyCode.Space);
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction LeftAction;
    // Start is called before the first frame update
    void Start()
    {
        LeftAction.Enabled();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.0f;
       if (LeftAction.IsPressed())
        {
            horizontal = -0.5f;
        }
       else if(Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 0.5f;
        }
        Debug.Log(horizontal);


        float vertical = 0.0f;
       if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 0.5f;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -0.5f;
        }
        Debug.Log(vertical);


        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;

    }
}

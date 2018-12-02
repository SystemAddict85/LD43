using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : SimpleSingleton<CursorManager> {

    public Texture2D handCursor;
    public Texture2D handClickCursor;

    public override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(handClickCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        }
    }

}

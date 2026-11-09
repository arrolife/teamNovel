using UnityEngine.InputSystem;

public static class ClickInput
{
 
    //¶ƒNƒŠƒbƒN
    public static bool WasClicked()
    {
        return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
    }
}
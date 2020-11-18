using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private KnightController knightControl;

    public KnightController KnightControl
    {
        set => knightControl = value;
    }

    public void AttackButtonDown()
    {
        knightControl.AttackButton = true;
    }

    public void AttackButtonUp()
    {
        knightControl.AttackButton = false;
    }

    public void LeftButtonDown()
    {
        knightControl.LeftButton = true;
    }

    public void LeftButtonUp()
    {
        knightControl.LeftButton = false;
    }

    public void RightButtonDown()
    {
        knightControl.RightButton = true;
    }

    public void RightButtonUp()
    {
        knightControl.RightButton = false;
    }

    public void JumpButtonDown()
    {
        knightControl.JumpButton = true;
    }

    public void JumpButtonUp()
    {
        knightControl.JumpButton = false;
    }

    public void TheExclamationMark()
    {
        knightControl.ClickMark = true;
    }
}
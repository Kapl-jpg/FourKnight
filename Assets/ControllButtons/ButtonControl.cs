
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private KnightController _knightControl;

    public KnightController KnightControl
    {
        get => _knightControl;
        set => _knightControl = value;
    }

    public void AttackButtonDown()
    {
        _knightControl.AttackButton = true;
    }

    public void AttackButtonUp()
    {
        _knightControl.AttackButton = false;
    }

    public void LeftButtonDown()
    {
        _knightControl.LeftButton = true;
    }

    public void LeftButtonUp()
    {
        _knightControl.LeftButton = false;
    }

    public void RightButtonDown()
    {
        _knightControl.RightButton = true;
    }

    public void RightButtonUp()
    {
        _knightControl.RightButton = false;
    }

    public void JumpButtonDown()
    {
        _knightControl.JumpButton = true;
    }

    public void JumpButtonUp()
    {
        _knightControl.JumpButton = false;
    }

    public void TheExclamationMark()
    {
        _knightControl.ClickMark = true;
    }
}
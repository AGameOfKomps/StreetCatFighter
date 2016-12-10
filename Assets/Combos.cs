using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public const int MAX_COMBOS = 5;
    public const float COMBO_DELAY = 0.5f;

    public enum ActionType {
        None,
        Up,
        Down,
        Left,
        Right,
        Heavy,
        Light,
        Jump,
        Dodge
    }

    public enum ComboType {
        None,
        One,
        Two,
        Three
    }

    private List<ActionType> attacks;
    private float comboCountdown;
    
    // Use this for initialization
	void Start () {
        attacks = new List<ActionType>();
        comboCountdown = COMBO_DELAY;
	}
	
	// Update is called once per frame
	void Update () {
        comboCountdown -= Time.deltaTime;
        if (comboCountdown <= 0)
        {
            attacks.Clear();
        }
	}

    public ComboType storeAction(float vertical, float horizontal, bool isHeavy, bool isLight, bool isJump, bool isDodge)
    {
        ComboType result = ComboType.None;
        ActionType actionType = mapActionType(vertical, horizontal, isHeavy, isLight, isJump, isDodge);
        if (ActionType.None != actionType)
        {
            comboCountdown = COMBO_DELAY;
            attacks.Add(actionType);
            if (attacks.Count > MAX_COMBOS)
            {
                attacks.RemoveAt(MAX_COMBOS);
            }
            result = checkCombo();
        }
        return result;
    }

    ActionType mapActionType(float vertical, float horizontal, bool isHeavy, bool isLight, bool isJump, bool isDodge)
    {
        ActionType result = ActionType.None;
        if (isLight)
            result = ActionType.Light;
        else if (isHeavy)
            result = ActionType.Heavy;
        else if (isJump)
            result = ActionType.Jump;
        else if (isDodge)
            result = ActionType.Dodge;
        else if (horizontal > 0)
            result = ActionType.Right;
        else if (horizontal < 0)
            result = ActionType.Left;
        else if (vertical > 0)
            result = ActionType.Up;
        else if (vertical < 0)
            result = ActionType.Down;
        return result;
    }

    private ComboType checkCombo()
    {
        ComboType result = ComboType.None;
        if (isComboOne())
            result = ComboType.One;
        else if (isComboTwo())
            result = ComboType.Two;
        else if (isComboThree())
            result = ComboType.Three;
        return result;
    }

    private bool isComboOne()
    {
        return attacks[0] == ActionType.Light 
            && attacks[1] == ActionType.Light 
            && attacks[2] == ActionType.Light;
    }

    private bool isComboTwo()
    {
        return attacks[0] == ActionType.Light 
            && attacks[1] == ActionType.Light 
            && attacks[2] == ActionType.Heavy;
    }

    private bool isComboThree()
    {
        return attacks[0] == ActionType.Light 
            && attacks[1] == ActionType.Light 
            && attacks[2] == ActionType.Heavy 
            && attacks[3] == ActionType.Light;
    }
}

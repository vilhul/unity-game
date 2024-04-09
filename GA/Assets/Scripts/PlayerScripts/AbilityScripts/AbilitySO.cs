using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public abstract class AbilitySO : ScriptableObject
{

    public enum AbilityType {
        Passive,
        Weapon,
        Active
    }

    public new string name;
    public string description;

    public Sprite sprite;

    public int cost;
    public AbilityType abilityType;
    public int AbilityCooldown;
    private float TimeRemaining;

    public virtual void HandleAbility(PlayerManager player) {
        Debug.Log("You are using " +  name);
    }

}

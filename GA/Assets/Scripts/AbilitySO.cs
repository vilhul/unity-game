using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public abstract class AbilitySO : ScriptableObject
{

    public enum AbilityType {
        Passive,
        Weapon,
        Ability
    }

    public new string name;
    public string description;

    public Sprite image;

    public int cost;
    public AbilityType abilityType;

    public virtual void HandleAbility() {
        Debug.Log("You are using " +  name);
    }

}

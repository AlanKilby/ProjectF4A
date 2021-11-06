using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkills
{
    public void ActivateSkill();
    public bool IsActivated();
    public bool IsOnCooldown();

}

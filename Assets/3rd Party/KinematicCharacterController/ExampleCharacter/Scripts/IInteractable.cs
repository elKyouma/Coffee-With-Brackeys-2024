using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Selected();
    public void Interact();
    public void Unselected();
}

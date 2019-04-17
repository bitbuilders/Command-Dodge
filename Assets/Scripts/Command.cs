using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    virtual public void Execute(Rebel rebel)
    {
        Debug.Log("Default command executed on rebel: " + rebel.name);
    }

    virtual public void Undo(Rebel rebel)
    {
        Debug.Log("Default command undone on rebel: " + rebel.name);
    }
}

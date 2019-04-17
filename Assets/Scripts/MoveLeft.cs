using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : Command
{
    public override void Execute(Rebel rebel)
    {
        rebel.Move(Movement.LEFT);
    }

    public override void Undo(Rebel rebel)
    {
        rebel.Move(Movement.RIGHT);
    }
}

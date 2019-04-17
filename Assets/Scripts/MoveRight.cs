using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Command
{
    public override void Execute(Rebel rebel)
    {
        rebel.Move(Movement.RIGHT);
    }

    public override void Undo(Rebel rebel)
    {
        rebel.Move(Movement.LEFT);
    }
}

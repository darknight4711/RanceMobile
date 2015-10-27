using System;
using System.Collections.Generic;
public class BattleStart : BattleState {
    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        manager.Combat = true;
        manager.CurrentState = manager.PlayerTurnPre;
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
public class PlayerTurnPre : BattleState {
    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        foreach (Chara chara in manager.CharaList) {
            if (chara.Dead)
                continue;
            chara.turnPre();
        }
        manager.CurrentState = manager.PlayerInteraction;
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }
}

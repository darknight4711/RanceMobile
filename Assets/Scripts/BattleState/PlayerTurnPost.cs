using System;
using System.Collections.Generic;
public class PlayerTurnPost : BattleState {
    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        foreach (Chara chara in manager.CharaList) {
            if (chara.Dead)
                continue;
            chara.turnPost();
        }
        manager.CurrentState = manager.EnemyAttack;
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }
}

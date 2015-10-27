using System;
using System.Collections.Generic;
public class CheckPlayerDead : BattleState {

    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        bool hasCharaDead = false;
        foreach (Chara c in manager.CharaList) {
            if (!c.Dead && c.checkDead()) {
                manager.removeOrder(c);
                hasCharaDead = true;
            }
        }
        if (hasCharaDead) {
        }
        manager.CurrentState = manager.CheckBattleEnd;
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
class Battle : StageState {
    StageStateManager manager;

    public void StateStart(StageStateManager manager) {
        manager.BattleFlow.setBattleEndCallBack(battleEnd);
        manager.BattleFlow.setBattleState(BattleFlow.BattleState.InitialAttack);
        this.manager = manager;
    }

    public void update(StageStateManager manager) {
        throw new NotImplementedException();
    }

    private void battleEnd(BattleFlow.BattleEndType endType) {
        if (endType == BattleFlow.BattleEndType.AllMonsterDead) {
            manager.CurrentState = manager.AllMonsterDead;
        } else if (endType == BattleFlow.BattleEndType.AllPlayerDead) {
            manager.CurrentState = manager.AllPlayerDead;
        }
    }
}

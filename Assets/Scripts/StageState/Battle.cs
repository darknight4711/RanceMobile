using System;
using System.Collections.Generic;
class Battle : StageState {
    StageStateManager manager;

    public void StateStart(StageStateManager manager, StageViewControl view) {
        view.BattleView.Manager.EndCallBack = battleEnd;
        view.BattleView.Manager.CurrentState = view.BattleView.Manager.BattleStart;
        this.manager = manager;
    }

    public void update(StageStateManager manager, StageViewControl view) {
        
    }

    private void battleEnd(BattleStateManager.BattleEndType endType) {
        if (endType == BattleStateManager.BattleEndType.AllMonsterDead) {
            manager.CurrentState = manager.AllMonsterDead;
        } else if (endType == BattleStateManager.BattleEndType.AllPlayerDead) {
            manager.CurrentState = manager.AllPlayerDead;
        }
    }
}

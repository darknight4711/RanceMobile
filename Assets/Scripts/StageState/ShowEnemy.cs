using System;
using System.Collections.Generic;
class ShowEnemy : StageState {
    public void StateStart(StageStateManager manager) {
        List<Monster> mInfo = manager.Info.MonsterList(manager.StageCount);
        manager.BattleFlow.setMonsters(mInfo);
    }

    public void update(StageStateManager manager) {
        if (manager.BattleFlow.isMonsterShowUpFinish()) {
            manager.CurrentState = manager.Battle;
        }
    }
}

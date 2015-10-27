using System;
using System.Collections.Generic;
public class ShowEnemy : StageState {
    public void StateStart(StageStateManager manager, StageViewControl view) {
        List<Monster> mInfo = manager.Info.MonsterList(manager.StageCount);
        view.BattleView.Manager.setMonsters(mInfo);
    }

    public void update(StageStateManager manager, StageViewControl view) {
        if (view.BattleView.isMonsterShowUpFinish()) {
            manager.CurrentState = manager.Battle;
        }
    }
}

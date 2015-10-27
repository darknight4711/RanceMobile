using System;
using System.Collections.Generic;
public class CheckBattleEnd : BattleState {
    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        bool allMonsterDead = true;
        for (int i = 0; i < manager.MonsterList.Count; i++) {
            if (!manager.MonsterList[i].Dead)
                allMonsterDead = false;
        }
        if (allMonsterDead) {
            BattleEndAction(manager, view);
            manager.EndCallBack(BattleStateManager.BattleEndType.AllMonsterDead);
            manager.Combat = false;
            return;
        }

        bool allCharaDead = true;
        foreach (Chara c in manager.CharaList) {
            if (!c.Dead)
                allCharaDead = false;
        }
        if (allCharaDead) {
            BattleEndAction(manager, view);
            manager.EndCallBack(BattleStateManager.BattleEndType.AllPlayerDead);
            manager.Combat = false;
            return;
        }

        manager.back();
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    private void BattleEndAction(BattleStateManager manager, BattleViewControl view) {
        manager.clearOrder();
        foreach (Chara c in manager.CharaList) {
            c.resetStatus();
        }
        view.StartBattleButton.interactable = false;
    }
}

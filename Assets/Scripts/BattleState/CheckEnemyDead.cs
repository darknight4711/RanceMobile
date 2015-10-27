using System;
using System.Collections.Generic;
using UnityEngine;

class CheckEnemyDead : BattleState {
    public void back(BattleStateManager manager, BattleViewControl view) {
        throw new NotImplementedException();
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        bool hasMonsterDead = false;
        foreach (Monster monster in manager.MonsterList) {
            if (monster.Dead)
                continue;
            if (monster.checkDead()) {
                hasMonsterDead = true;
            }
        }
        if (!hasMonsterDead) {
            manager.CurrentState = manager.CheckPlayerDead;
        }
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        bool allDeadAnimationFinish = isAllMonsterDeadAnimationFinish(view);
        if (allDeadAnimationFinish) {
            view.DestroyDeadEnemy();
            manager.CurrentState = manager.MonsterMoveForward;
        }
    }

    private bool isAllMonsterDeadAnimationFinish(BattleViewControl view) {
        bool allDeadAnimationFinish = true;
        foreach (MonsterPanelControl mpc in view.MonsterPanelList) {
            if (!mpc.isPlayDeadFinish())
                allDeadAnimationFinish = false;
        }
        return allDeadAnimationFinish;
    }
}

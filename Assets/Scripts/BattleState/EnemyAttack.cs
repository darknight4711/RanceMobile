using System;
using System.Collections.Generic;
public class EnemyAttack : BattleState {
    int currentAttackMonsterCount;
    int monsterPointer;

    public void back(BattleStateManager manager, BattleViewControl view) {
        currentAttackMonsterCount++;
        if (!manager.MonsterList[monsterPointer].Dead) {
            manager.MonsterList[monsterPointer].View.DelightIcon();
        }
        monsterPointer++;
        nextMonsterAction(manager, view);
    }

    public void StateStart(BattleStateManager manager, BattleViewControl view) {
        foreach (MonsterPanelControl mpc in view.MonsterPanelList) {
            mpc.DelightIcon();
        }
        currentAttackMonsterCount = 0;
        monsterPointer = 0;
        nextMonsterAction(manager, view);
    }

    public void update(BattleStateManager manager, BattleViewControl view) {
        if (manager.MonsterList[monsterPointer].IsAttackFinish(manager.CharaList, manager.MonsterList, monsterPointer)) {
            manager.BackState = this;
            manager.CurrentState = manager.CheckEnemyDead;
        }
    }

    private void nextMonsterAction(BattleStateManager manager, BattleViewControl view) {
        if (currentAttackMonsterCount < view.MonsterPanelList.Count && currentAttackMonsterCount < 3) {
            while (manager.MonsterList[monsterPointer].Dead) { monsterPointer++; }
            manager.MonsterList[monsterPointer].View.LightIcon();
            manager.MonsterList[monsterPointer].startAttack(manager.CharaList, manager.MonsterList, monsterPointer);
        } else {
            foreach (MonsterPanelControl mpc in view.MonsterPanelList) {
                mpc.LightIcon();
            }
            manager.CurrentState = manager.PlayerTurnPre;
        }
    }
}

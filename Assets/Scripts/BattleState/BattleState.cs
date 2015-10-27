public interface BattleState {
    void StateStart(BattleStateManager manager, BattleViewControl view);
    void back(BattleStateManager manager, BattleViewControl view);
    void update(BattleStateManager manager, BattleViewControl view);
}

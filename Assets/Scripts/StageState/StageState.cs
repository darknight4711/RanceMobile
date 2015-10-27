public interface StageState {
    void StateStart(StageStateManager manager, StageViewControl view);
    void update(StageStateManager manager, StageViewControl view);
}

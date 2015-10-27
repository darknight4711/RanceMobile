public interface OrderManager<T> {
    void addOrder(T obj);
    int getOrder(T obj);
    int getOrderedCount();
    T getOrderedObject(int i);
    void removeOrder(T obj);
    void clearOrder();
}

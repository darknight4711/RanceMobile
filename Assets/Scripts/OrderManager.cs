public interface OrderManager<T> {
    void addOrder(T obj);
    int getOrder(T obj);
    void removeOrder(T obj);
}

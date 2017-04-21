using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SKeyValuePair<TKey, TValue> {
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public SKeyValuePair(TKey key, TValue value) {
        Key = key;
        Value = value;
    }
}

[Serializable]
public class SDictionary<TKey, TValue> : ScriptableObject, IDictionary<TKey, TValue> {
    [SerializeField]
    private List<SKeyValuePair<TKey, TValue>> dict = new List<SKeyValuePair<TKey, TValue>>();

    public TValue this[TKey key] {
        get {
            foreach (SKeyValuePair<TKey, TValue> pair in dict) {
                if (key.Equals(pair.Key))
                    return pair.Value;
            }
            return default(TValue);
        }

        set {
            if (IsReadOnly || key == null)
                return;
            for (int i=0; i < dict.Count; i++) {
                if (key.Equals(dict[i].Key)) {
                    dict.RemoveAt(i);
                    dict.Insert(i, new SKeyValuePair<TKey, TValue>(key, value));
                    return;
                }
            }
        }
    }

    public int Count {
        get {
            return dict.Count;
        }
    }

    public bool IsReadOnly {
        get {
            return false;
        }
    }

    public ICollection<TKey> Keys {
        get {
            List<TKey> keys = new List<TKey>();
            foreach (SKeyValuePair<TKey,TValue> pair in dict) {
                keys.Add(pair.Key);
            }
            return keys;
        }
    }

    public ICollection<TValue> Values {
        get {
            List<TValue> values = new List<TValue>();
            foreach (SKeyValuePair<TKey, TValue> pair in dict) {
                values.Add(pair.Value);
            }
            return values;
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item) {
        if (item.Key == null)
            return;
        dict.Add(new SKeyValuePair<TKey, TValue>(item.Key, item.Value));
    }

    public void Add(TKey key, TValue value) {
        if (key == null)
            return;
        dict.Add(new SKeyValuePair<TKey, TValue>(key, value));
    }

    public void Clear() {
        dict.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) {
        if (item.Key == null)
            return false;
        return dict.Contains(new SKeyValuePair<TKey, TValue>(item.Key, item.Value));
    }

    public bool ContainsKey(TKey key) {
        if (key == null)
            return false;
        foreach (SKeyValuePair<TKey, TValue> pair in dict) {
            if (key.Equals(pair.Key)) {
                return true;
            }
        }
        return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        if (array.Length - arrayIndex < dict.Count)
            return;
        for (int i=0; i < dict.Count; i++) {
            array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(dict[i].Key, dict[i].Value);
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
        foreach (SKeyValuePair<TKey, TValue> pair in dict)
            yield return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) {
        return dict.Remove(new SKeyValuePair<TKey, TValue>(item.Key, item.Value));
    }

    public bool Remove(TKey key) {
        foreach (SKeyValuePair<TKey, TValue> pair in dict) {
            if (key.Equals(pair.Key)) {
                dict.Remove(pair);
                return true;
            }
        }
        return false;
    }

    public bool TryGetValue(TKey key, out TValue value) {
        foreach (SKeyValuePair<TKey, TValue> pair in dict) {
            if (key.Equals(pair.Key)) {
                value = pair.Value;
                return true;
            }
        }
        value = default(TValue);
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return dict.GetEnumerator();
    }
}
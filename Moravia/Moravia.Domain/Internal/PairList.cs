namespace Moravia.Domain.Internal
{
    /// <summary>
    /// Pair list. There is not generic implementation of OrderedDictionary
    /// </summary>
    /// <typeparam name="TKey">Key.</typeparam>
    /// <typeparam name="TValue">Value</typeparam>
    public class PairList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
    {
        readonly Dictionary<TKey, int> _itsIndex = new();

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">Item key.</param>
        /// <returns>Item.</returns>
        public TValue this[TKey key] => this.Get(key);

        /// <summary>
        /// Add item to pair list
        /// </summary>
        /// <param name="key">Key of item.</param>
        /// <param name="value">Value of item.</param>
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
            _itsIndex.Add(key, Count - 1);
        }

        /// <summary>
        /// Get item from pair list.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <returns>Item.</returns>
        public TValue Get(TKey key)
        {
            var idx = _itsIndex[key];
            return this[idx].Value;
        }
    }
}

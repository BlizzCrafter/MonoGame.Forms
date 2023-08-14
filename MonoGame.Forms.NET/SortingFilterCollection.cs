namespace MonoGame.Forms.NET
{
    /// <summary>
    /// The SortingFilteringCollection class provides efficient, reusable
    /// sorting and filtering based on a configurable sort comparer, filter
    /// predicate, and associate change events.
    /// </summary>
    public class SortingFilteringCollection<T> : ICollection<T>
    {
        private readonly List<T> _items;
        private readonly List<AddJournalEntry<T>> _addJournal;
        private readonly Comparison<AddJournalEntry<T>> _addJournalSortComparison;
        private readonly List<int> _removeJournal;
        private readonly List<T> _cachedFilteredItems;
        private bool _shouldRebuildCache;

        private readonly Predicate<T> _filter;
        private readonly Comparison<T> _sort;
        private readonly Action<T, EventHandler<EventArgs>> _filterChangedSubscriber;
        private readonly Action<T, EventHandler<EventArgs>> _filterChangedUnsubscriber;
        private readonly Action<T, EventHandler<EventArgs>> _sortChangedSubscriber;
        private readonly Action<T, EventHandler<EventArgs>> _sortChangedUnsubscriber;

        public SortingFilteringCollection(
            Predicate<T> filter,
            Action<T, EventHandler<EventArgs>> filterChangedSubscriber,
            Action<T, EventHandler<EventArgs>> filterChangedUnsubscriber,
            Comparison<T> sort,
            Action<T, EventHandler<EventArgs>> sortChangedSubscriber,
            Action<T, EventHandler<EventArgs>> sortChangedUnsubscriber)
        {
            _items = new List<T>();
            _addJournal = new List<AddJournalEntry<T>>();
            _removeJournal = new List<int>();
            _cachedFilteredItems = new List<T>();
            _shouldRebuildCache = true;

            _filter = filter;
            _filterChangedSubscriber = filterChangedSubscriber;
            _filterChangedUnsubscriber = filterChangedUnsubscriber;
            _sort = sort;
            _sortChangedSubscriber = sortChangedSubscriber;
            _sortChangedUnsubscriber = sortChangedUnsubscriber;

            _addJournalSortComparison = CompareAddJournalEntry;
        }

        private int CompareAddJournalEntry(AddJournalEntry<T> x, AddJournalEntry<T> y)
        {
            int result = _sort(x.Item, y.Item);
            if (result != 0)
                return result;
            return x.Order - y.Order;
        }

        public void ForEachFilteredItem<TUserData>(Action<T, TUserData> action, TUserData userData)
        {
            if (_shouldRebuildCache)
            {
                ProcessRemoveJournal();
                ProcessAddJournal();

                // Rebuild the cache
                _cachedFilteredItems.Clear();
                for (int i = 0; i < _items.Count; ++i)
                    if (_filter(_items[i]))
                        _cachedFilteredItems.Add(_items[i]);

                _shouldRebuildCache = false;
            }

            for (int i = 0; i < _cachedFilteredItems.Count; ++i)
                action(_cachedFilteredItems[i], userData);

            // If the cache was invalidated as a result of processing items,
            // now is a good time to clear it and give the GC (more of) a
            // chance to do its thing.
            if (_shouldRebuildCache)
                _cachedFilteredItems.Clear();
        }

        public void Add(T item)
        {
            // NOTE: We subscribe to item events after items in _addJournal
            //       have been merged.
            _addJournal.Add(new AddJournalEntry<T>(_addJournal.Count, item));
            InvalidateCache();
        }

        public bool Remove(T item)
        {
            if (_addJournal.Remove(AddJournalEntry<T>.CreateKey(item)))
                return true;

            var index = _items.IndexOf(item);
            if (index >= 0)
            {
                UnsubscribeFromItemEvents(item);
                _removeJournal.Add(index);
                InvalidateCache();
                return true;
            }
            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                _filterChangedUnsubscriber(_items[i], Item_FilterPropertyChanged);
                _sortChangedUnsubscriber(_items[i], Item_SortPropertyChanged);
            }

            _addJournal.Clear();
            _removeJournal.Clear();
            _items.Clear();

            InvalidateCache();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_items).GetEnumerator();
        }

        private static readonly Comparison<int> RemoveJournalSortComparison =
            (x, y) => Comparer<int>.Default.Compare(y, x); // Sort high to low
        private void ProcessRemoveJournal()
        {
            if (_removeJournal.Count == 0)
                return;

            // Remove items in reverse.  (Technically there exist faster
            // ways to bulk-remove from a variable-length array, but List<T>
            // does not provide such a method.)
            _removeJournal.Sort(RemoveJournalSortComparison);
            for (int i = 0; i < _removeJournal.Count; ++i)
                _items.RemoveAt(_removeJournal[i]);
            _removeJournal.Clear();
        }

        private void ProcessAddJournal()
        {
            if (_addJournal.Count == 0)
                return;

            // Prepare the _addJournal to be merge-sorted with _items.
            // _items is already sorted (because it is always sorted).
            _addJournal.Sort(_addJournalSortComparison);

            int iAddJournal = 0;
            int iItems = 0;

            while (iItems < _items.Count && iAddJournal < _addJournal.Count)
            {
                var addJournalItem = _addJournal[iAddJournal].Item;
                // If addJournalItem is less than (belongs before)
                // _items[iItems], insert it.
                if (_sort(addJournalItem, _items[iItems]) < 0)
                {
                    SubscribeToItemEvents(addJournalItem);
                    _items.Insert(iItems, addJournalItem);
                    ++iAddJournal;
                }
                // Always increment iItems, either because we inserted and
                // need to move past the insertion, or because we didn't
                // insert and need to consider the next element.
                ++iItems;
            }

            // If _addJournal had any "tail" items, append them all now.
            for (; iAddJournal < _addJournal.Count; ++iAddJournal)
            {
                var addJournalItem = _addJournal[iAddJournal].Item;
                SubscribeToItemEvents(addJournalItem);
                _items.Add(addJournalItem);
            }

            _addJournal.Clear();
        }

        private void SubscribeToItemEvents(T item)
        {
            _filterChangedSubscriber(item, Item_FilterPropertyChanged);
            _sortChangedSubscriber(item, Item_SortPropertyChanged);
        }

        private void UnsubscribeFromItemEvents(T item)
        {
            _filterChangedUnsubscriber(item, Item_FilterPropertyChanged);
            _sortChangedUnsubscriber(item, Item_SortPropertyChanged);
        }

        private void InvalidateCache()
        {
            _shouldRebuildCache = true;
        }

        private void Item_FilterPropertyChanged(object sender, EventArgs e)
        {
            InvalidateCache();
        }

        private void Item_SortPropertyChanged(object sender, EventArgs e)
        {
            var item = (T)sender;
            var index = _items.IndexOf(item);

            _addJournal.Add(new AddJournalEntry<T>(_addJournal.Count, item));
            _removeJournal.Add(index);

            // Until the item is back in place, we don't care about its
            // events.  We will re-subscribe when _addJournal is processed.
            UnsubscribeFromItemEvents(item);
            InvalidateCache();
        }
    }

    public struct AddJournalEntry<T>
    {
        public readonly int Order;
        public readonly T Item;

        public AddJournalEntry(int order, T item)
        {
            Order = order;
            Item = item;
        }

        public static AddJournalEntry<T> CreateKey(T item)
        {
            return new AddJournalEntry<T>(-1, item);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AddJournalEntry<T>))
                return false;

            return object.Equals(Item, ((AddJournalEntry<T>)obj).Item);
        }
    }
}

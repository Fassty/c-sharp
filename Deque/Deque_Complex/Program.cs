using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DequeTest
{
    public static IList<T> GetReverseView<T>(Deque<T> deque)
    {
        return new ReverseView<T>(deque); 
	}
}

public interface IDeque<T> : IList<T> {
    void PushFront(T item);
    void PushBack(T item);
    T PopFront();
    T PopBack();
    T PeekFront();
    T PeekBack();
}

public sealed class ReverseView<T> : IDeque<T>
{
    Deque<T> source;
    public int Count => source.Count;
    public bool IsReadOnly => source.IsReadOnly;
    public ReverseView(Deque<T> source)
    {
        this.source = source;
    }

    public T this[int index]
    {
        get => source[source.Count - 1 - index];
        set => source[source.Count - 1 - index] = value;
    }

    public void Add(T item) => source.PushFront(item);
    public void Insert(int index, T item) => source.Insert(source.Count - index, item);
    public bool Remove(T item) => source.Remove(item);
    public void RemoveAt(int index) => source.RemoveAt(source.Count - 1 - index);
    public void Clear() => source.Clear();
    public bool Contains(T item) => source.Contains(item);

    public T PeekBack() => source.PeekFront();
    public T PeekFront() => source.PeekBack();
    public T PopBack() => source.PopFront();
    public T PopFront() => source.PopBack();
    public void PushBack(T item) => source.PushFront(item);
    public void PushFront(T item) => source.PushBack(item);

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException("Cannot copy to null destination");

        if (arrayIndex < 0 || Count < 0 || array.Length - arrayIndex < Count) throw new ArgumentException("Invalid range parameters");

        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = this[i];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return source.GetReverseEnumerator();
    }

    public int IndexOf(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        int i = 0;

        if(item != null)
        {
            foreach(T obj in this)
            {
                if (comparer.Equals(item, obj)) return i;
                i++;
            }
        }
        else
        {
            foreach (T obj in this)
            {
                if (obj == null) return i;
                i++;
            }
        }
        return -1;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return source.GetReverseEnumerator();
    }
}

public class Deque<T> : IDeque<T>
{
    private const int INITIAL_SIZE = 3;
    private const int BLOCK_SIZE = 8;

    T[][] data;
    int headBlock;
    int tailBlock;
    int headIndex;
    int tailIndex;
    int avaliableBlocks;
    int activeEnumerators = 0;

    public Deque()
    {
        Init();
    }

    public T this[int index] {
        get {

            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            int block = headBlock + (headIndex + index) / BLOCK_SIZE;
            int innerIndex = (index + headIndex) % BLOCK_SIZE;
            return data[block][innerIndex]; 
            
        }
        set {

            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            LockForEnumeration();

            int block = headBlock + (headIndex + index) / BLOCK_SIZE;
            int innerIndex = (index + headIndex) % BLOCK_SIZE;
            if (data[block] == null)
                data[block] = new T[BLOCK_SIZE];
            data[block][innerIndex] = value;
            
        }
    }

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        PushBack(item);
    }

    public void Clear()
    {
        Init();
    }

    void Init()
    {
        data = new T[INITIAL_SIZE][];
        data[INITIAL_SIZE / 2] = new T[BLOCK_SIZE];

        headBlock = INITIAL_SIZE / 2;
        tailBlock = INITIAL_SIZE / 2;
        headIndex = BLOCK_SIZE / 2 - 1;
        tailIndex = BLOCK_SIZE / 2;
        avaliableBlocks = INITIAL_SIZE;

        Count = 0;
    }

    public bool Contains(T item)
    {

        for (int i = 0; i < Count; i++)
        {
            if (this[i].Equals(item)) return true;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException("Cannot copy to null destination");

        if (arrayIndex < 0 || Count < 0 || array.Length - arrayIndex < Count) throw new ArgumentException("Invalid range parameters");

        for (int i = 0; i < Count; i++)
        {
            array[i + arrayIndex] = this[i];
        }
    }

    public int IndexOf(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        int i = 0;

        if (item != null)
        {
            foreach (T obj in this)
            {
                if (comparer.Equals(item, obj)) return i;
                i++;
            }
        }
        else
        {
            foreach (T obj in this)
            {
                if (obj == null) return i;
                i++;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException();

        if (index == 0)
        {
            PushFront(item);
        }
        else
        {
            LockForEnumeration();

            Count++;
            for (int i = Count - 1; i > index; i--)
            {
                this[i] = this[i - 1];
            }

            LowerTail();
            this[index] = item;
        }
    }

    void Grow()
    {
        var aux = data;
        data = new T[avaliableBlocks * 3][];
        for (int i = avaliableBlocks + 1 ; i <= 2 * avaliableBlocks ; i++)
        {
            data[i] = aux[i - (avaliableBlocks + 1)];
        }
        headBlock += avaliableBlocks + 1;
        tailBlock += avaliableBlocks + 1;
        avaliableBlocks *= 3;
    }

    void LockForEnumeration()
    {
        if (activeEnumerators > 0)
            throw new InvalidOperationException();
    }

    public T PeekFront()
    {
        return this[0];
    }

    public T PeekBack()
    {
        return this[Count - 1];
    }

    #region OuttaMyWay
    void LowerHead()
    {
        headIndex++;
        if (headIndex >= BLOCK_SIZE)
        {
            headBlock++;
            headIndex = 0;
        }


        // Will not happen, but I don't really give a damn at this point
        if(headBlock >= avaliableBlocks)
        {
            Grow();
        }

        if (data[headBlock] == null)
        {
            data[headBlock] = new T[BLOCK_SIZE];
        }
    }

    void ElevateHead()
    {
        headIndex--;
        if (headIndex < 0)
        {
            headBlock--;
            headIndex = BLOCK_SIZE - 1;
        }

        if (headBlock < 0)
        {
            Grow();
        }

        if (data[headBlock] == null)
        {
            data[headBlock] = new T[BLOCK_SIZE];
        }
    }

    void LowerTail()
    {
        tailIndex++;
        if(tailIndex >= BLOCK_SIZE)
        {
            tailBlock++;
            tailIndex = 0;
        }

        if(tailBlock >= avaliableBlocks)
        {
            Grow();
        }

        if(data[tailBlock] == null)
        {
            data[tailBlock] = new T[BLOCK_SIZE];
        }
    }

    void ElevateTail()
    {
        tailIndex--;
        if(tailIndex < 0)
        {
            tailBlock--;
            tailIndex = BLOCK_SIZE - 1;
        }
    }
    #endregion

    #region PushPop
    public void PushFront(T item)
    {
        LockForEnumeration();

        ElevateHead();
        ++Count;
        this[0] = item;
    }
    public T PopFront()
    {
        if (Count == 0) throw new InvalidOperationException("Cannot pop from empty collection");

        LockForEnumeration();

        var aux = this[0];

        LowerHead();
        --Count;

        return aux;
    }

    public void PushBack(T item)
    {
        LockForEnumeration();

        LowerTail();
        this[++Count - 1] = item;
    }
    
    public T PopBack()
    {
        if (Count == 0) throw new InvalidOperationException("Cannot pop from empty collection");
        LockForEnumeration();

        ElevateTail();
        // Why this won't work? :D
        //var a = this[Count-- - 1];
        var toReturn = this[Count - 1];
        --Count;
        return toReturn;
    }
    #endregion

    public bool Remove(T item)
    {
        LockForEnumeration();

        int pos = IndexOf(item);

        if (pos == -1)
            return false;

        RemoveAt(pos);
        return true;
    }

    public void RemoveAt(int index)
    {
        if (index == 0)
            PopFront();
        else if (index == Count - 1)
            PopBack();
        else
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();

            LockForEnumeration();
            for (int i = index; i < Count - 1; i++)
            {
                this[i] = this[i + 1];
            }

            ElevateTail();
            this[Count - 1] = default(T);
            --Count;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator<T>(this, true);
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        return new Enumerator<T>(this, false);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public sealed class Enumerator<ET> : IEnumerator<ET>
    {
        public Deque<ET> deque;
        int currentIndex;
        bool iterateFront;

        public Enumerator(Deque<ET> deque, bool iterateFront)
        {
            this.deque = deque;
            deque.activeEnumerators++;

            this.iterateFront = iterateFront;

            currentIndex = iterateFront ? -1 : deque.Count;
        }
        public ET Current => deque[currentIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            deque.activeEnumerators--;
        }

        public bool MoveNext()
        {
            if (iterateFront)
            {
                currentIndex++;
                return currentIndex < deque.Count;
            }
            else
            {
                currentIndex--;
                return currentIndex >= 0;
            }
        }

        public void Reset()
        {
            currentIndex = iterateFront ? -1 : deque.Count - 1;
        }
    }
}

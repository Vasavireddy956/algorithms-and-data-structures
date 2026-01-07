public class Stack<T>
{
    private T[] data;
    private int top;

    public Stack(int size)
    {
        data = new T[size];
        top = -1;
    }

    public void Push(T item)
    {
        data[++top] = item;
    }

    public T Pop()
    {
        return data[top--];
    }

    public T Peek()
    {
        return data[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }
}


// Node.cs
public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }

    public Node(int value)
    {
        Value = value;
        Next = null;
    }
}

// Stack.cs
public class Stack
{
    private Node top; // Con trỏ tới phần tử đầu (đỉnh stack)
    private int count; // Số lượng phần tử trong stack

    public Stack()
    {
        top = null;
        count = 0;
    }

    // Thêm một phần tử vào stack
    public void Push(int value)
    {
        Node newNode = new Node(value);
        newNode.Next = top; // Trỏ node mới tới node hiện tại ở trên cùng
        top = newNode; // Đặt node mới làm đỉnh stack
        count++;
    }

    // Lấy và xóa phần tử trên cùng
    public int Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty.");

        int value = top.Value; // Lấy giá trị của phần tử trên cùng
        top = top.Next; // Di chuyển con trỏ tới node tiếp theo
        count--;
        return value;
    }

    // Lấy giá trị của phần tử trên cùng mà không xóa
    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty.");

        return top.Value;
    }

    // Kiểm tra stack có rỗng hay không
    public bool IsEmpty()
    {
        return top == null;
    }

    // Lấy số lượng phần tử trong stack
    public int Count => count;

    // Xóa toàn bộ stack
    public void Clear()
    {
        top = null;
        count = 0;
    }
    // Tạo một bản sao của stack
    public Stack Clone()
    {
        Stack clonedStack = new Stack();
        Node current = top;
        while (current != null)
        {
            clonedStack.Push(current.Value);
            current = current.Next;
        }
        return clonedStack;
    }
}

using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Util;

public class LookupTable
{
    private class Node
    {
        public string word;

        public object color;

        private Node[] children;

        public Node this[int index]
        {
            get
            {
                if (children != null)
                {
                    return children[index];
                }
                return null;
            }
            set
            {
                if (children == null)
                {
                    children = new Node[256];
                }
                children[index] = value;
            }
        }

        public Node(object color, string word)
        {
            this.word = word;
            this.color = color;
        }
    }

    private readonly Node root = new(null, null);

    private readonly bool casesensitive;

    private int length;

    public int Count => length;

    public object this[IDocument document, LineSegment line, int offset, int length]
    {
        get
        {
            if (length == 0)
            {
                return null;
            }
            Node node = root;
            int num = line.Offset + offset;
            if (casesensitive)
            {
                for (int i = 0; i < length; i++)
                {
                    int index = document.GetCharAt(num + i) % 256;
                    node = node[index];
                    if (node == null)
                    {
                        return null;
                    }
                    if (node.color != null && TextUtility.RegionMatches(document, num, length, node.word))
                    {
                        return node.color;
                    }
                }
            }
            else
            {
                for (int j = 0; j < length; j++)
                {
                    int index2 = char.ToUpper(document.GetCharAt(num + j)) % 256;
                    node = node[index2];
                    if (node == null)
                    {
                        return null;
                    }
                    if (node.color != null && TextUtility.RegionMatches(document, casesensitive, num, length, node.word))
                    {
                        return node.color;
                    }
                }
            }
            return null;
        }
    }

    public object this[string keyword]
    {
        set
        {
            Node node = root;
            Node node2 = root;
            if (!casesensitive)
            {
                keyword = keyword.ToUpper();
            }
            length++;
            for (int i = 0; i < keyword.Length; i++)
            {
                int index = keyword[i] % 256;
                _ = keyword[i];
                node2 = node2[index];
                if (node2 == null)
                {
                    node[index] = new Node(value, keyword);
                    break;
                }
                if (node2.word != null && node2.word.Length != i)
                {
                    string word = node2.word;
                    object color = node2.color;
                    node2.color = (node2.word = null);
                    this[word] = color;
                }
                if (i == keyword.Length - 1)
                {
                    node2.word = keyword;
                    node2.color = value;
                    break;
                }
                node = node2;
            }
        }
    }

    public LookupTable(bool casesensitive)
    {
        this.casesensitive = casesensitive;
    }
}

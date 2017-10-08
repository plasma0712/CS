using System;

namespace Program
{
    public static class HashCodeExtensionMethod
    {
        public static int GetHashCode2( string input)
        {
            int hashCode = 0;
            for (int i = 0; i < input.Length; i++)
                hashCode += Math.Abs((input[i] - 'Z'));

            hashCode = hashCode % 100;
            return hashCode;
        }

        public static int GetHashCode3(string input)
        {
            int hashCode = 0;
            for (int i = 0; i < input.Length; i++)
                hashCode += input[i];

            hashCode = hashCode % 100;
            return hashCode;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] saveidx = new string[5];
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                Node node = new Node();
                node.value = r.Next(0,200).ToString();
                node.hashNext = null;
                Hash.AddHashData(node.value, node);
                saveidx[0] = node.value;
            }
            Hash.PrintHashData();

            for (int i = 0; i < 50; i++)
                Hash.DeleteHashData(r.Next(0,200).ToString());
             Hash.PrintHashData();

             for (int i = 0; i < 50; i++)
             {
                 Hash.Search(r.Next(0,100));
             }
        }
    }


    public class Node
    {
        public string value;
        public Node hashNext ;
    }

    static public class Hash
    {
        public static Node[] hashTable = new Node[100];

        public static void AddHashData(string value, Node node)
        {
            int hashKey = HashCodeExtensionMethod.GetHashCode2(value);

            if (hashTable[hashKey] == null)
            {
                hashTable[hashKey] = node;
                return;
            }
            else
            {
                if (NodeCount(value, hashKey) >= 5)
                {
                    int hashKey2 = HashCodeExtensionMethod.GetHashCode3(value);
                    if (hashTable[hashKey2] == null)
                    {
                        hashTable[hashKey2] = node;
                        return;
                    }
                    else
                    {
                        if (NodeCount(value, hashKey2) >= 5)
                        {
                            Console.WriteLine("Node is Full(key 1: {0} key2 : {1})",hashKey,hashKey2);
                            return;
                        }
                        node.hashNext = hashTable[hashKey2];
                        hashTable[hashKey2] = node;
                        return;
                    }
                }
                node.hashNext = hashTable[hashKey];
                hashTable[hashKey] = node;
            }
        }

        public static int NodeCount(string value, int key)
        {
            int count = 1;
            Node node = hashTable[key];
            Node next = node.hashNext;

            if (node == null)
                return 0;
            else
                while (next != null)
                {
                    count++;
                    next = next.hashNext;
                }
            return count;
        }
        public static string DeleteHashData(string value)
        {
            int hashKey = HashCodeExtensionMethod.GetHashCode2(value);
            if (hashTable[hashKey] == null)
            {
                hashKey = HashCodeExtensionMethod.GetHashCode3(value);
                if (hashTable[hashKey] == null)
                {
                    Console.WriteLine("{0} 값 삭제 실패", value);
                    return null;
                }
            }

            Node deleteNode = null;
            if (hashTable[hashKey].value == value)
            {
                deleteNode = hashTable[hashKey];
                hashTable[hashKey] = hashTable[hashKey].hashNext;
                Console.WriteLine("{0} 값 삭제 성공", value);
                return value;
            }
            else
            {
                Node node = hashTable[hashKey];
                Node next = node.hashNext;
                while (node != null)
                {
                    if (node.value == value)
                    {
                        if (node.hashNext != null)
                        {
                            node.hashNext = next.hashNext;
                            deleteNode = next;
                        }
                        Console.WriteLine("{0} 값 삭제 성공", value);
                        return value;
 
                    }
                    node = next;
                    if(node!=null)
                        next = node.hashNext;
                }
            }
            Console.WriteLine("{0} 값 삭제 실패", value);

            return null;
        }

        public static void PrintHashData()
        {
            Console.WriteLine("Print Hash Data");
            for (int i = 0; i < 100; i++)
            {
                Console.Write("idx {0} : ", i);
                if (hashTable[i] != null)
                {
                    Node node = hashTable[i];
                    while (node != null)
                    {
                        Console.Write(" {0} ", node.value);
                        node = node.hashNext;
                    }
                    Console.WriteLine(" ");
                }
            }
        }

        public static Node Search(int key)
        {
            Node node = hashTable[key];

            if (node != null)
            {
                Console.Write("{0} 키의 값 :  ", key);
                while (node != null)
                {
                    Console.Write("  {0}  ", node.value);
                    node = node.hashNext;
                }
                Console.WriteLine("");
                return node;
            }
            else
            {
                Console.WriteLine("{0}키에는 값이 없음", key);
                return null;
            }
        }

    }
}

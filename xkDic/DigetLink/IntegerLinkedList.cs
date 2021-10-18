using System;
namespace DigetLink
{
    public class DigetNode
    {
        public uint value;
        public DigetNode next;

        public DigetNode()
        {
            value = 0;
            next = null;
        }

        public static uint operator +(DigetNode Interget1, DigetNode Interget2)
        {
            if (Interget1 != null && Interget2 != null)
            {
                return Interget1.value + Interget2.value;
            }
            else if (Interget1 != null)
            {
                return Interget1.value;
            }
            else if (Interget2 != null)
            {
                return Interget2.value;
            }
            else
            {
                return 0;
            }
        }
    }

    public class IntegerLinkedList
    {
        DigetNode m_Interger;

        public IntegerLinkedList()
        {
            m_Interger = null;
        }

        public IntegerLinkedList(uint value)
        {
            DigetNode parentNode = null;
            do
            {
                uint nDigetValue = value % 10;
                value = value / 10;

                DigetNode tempNode = new DigetNode();
                tempNode.value = nDigetValue;
                if (parentNode == null)
                {
                    m_Interger = tempNode;
                }
                else
                {
                    parentNode.next = tempNode;
                }
                parentNode = tempNode;
            } while (value > 0);
        }

        public static IntegerLinkedList operator +(IntegerLinkedList Interget1LinkedList, IntegerLinkedList Interget2LinkedList)
        {
            DigetNode parentNode = null;
            IntegerLinkedList resultInteget = new IntegerLinkedList();

            DigetNode nTempInterget1 = Interget1LinkedList.m_Interger;
            DigetNode nTempInterget2 = Interget2LinkedList.m_Interger;

            bool bNextJinWei = false;
            while (true)
            {
                uint nDigetValue = nTempInterget1 + nTempInterget2;
                if(bNextJinWei)
                {
                    nDigetValue++;
                }

                bNextJinWei = nDigetValue >= 10;
                if (bNextJinWei)
                {
                    nDigetValue %= 10;
                }

                if (nTempInterget1 != null)
                {
                    nTempInterget1 = nTempInterget1.next;
                }
                if (nTempInterget2 != null)
                {
                    nTempInterget2 = nTempInterget2.next;
                }

                DigetNode tempNode = new DigetNode();
                tempNode.value = nDigetValue;
                if (parentNode == null)
                {
                    resultInteget.m_Interger = tempNode;
                }
                else
                {
                    parentNode.next = tempNode;
                }
                parentNode = tempNode;

                if (nTempInterget1 == null && nTempInterget2 == null)
                {
                    break;
                }
            }

            if (bNextJinWei)
            {
                DigetNode tempNode = new DigetNode();
                tempNode.value = 1;
                parentNode.next = tempNode;
            }

            return resultInteget;
        }

        public static IntegerLinkedList FromInteger(uint value)
        {
            return new IntegerLinkedList(value);
        }

        public uint ToInteger()
        {
            uint resultValue = 0;
            uint n10Multuile = 1;

            DigetNode tempDigetNode = m_Interger;
            while (tempDigetNode != null)
            {
                resultValue += tempDigetNode.value * n10Multuile;

                tempDigetNode = tempDigetNode.next;
                n10Multuile *= 10;
            }

            return resultValue;
        }
    }
}

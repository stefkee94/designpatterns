﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_Tokenizer
{
    public class TokenList<T> : IEnumerable<T>
    {
        public class Node<T> 
        {
            public Node<T> Next;
            public Node<T> Previous;
            public T Data;

            public Node(T d)
            {
                Data = d;
                Next = null;
                Previous = null;
            }
        }

        private int _size;
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get { return _size; } }
        public Node<T> Head { get { return _head; } }
        public Node<T> Tail { get { return _tail; } } 

        public TokenList()
        {
            _size = 0;
            _head  = null;
        }

        public bool isEmpty()
        {
            return (_size == 0);
        }

        public void addFirstItem(T item)
        {
            _head = new Node<T>(item);
            _tail = _head;
        }

        public void AddAfter(T existingData, T newData)
        {
            Node<T> node = Find(existingData);
            if (node == null)
                throw new ArgumentException("existingData doesn't exist in the list");
            AddAfter(node, newData);
        }

        public void AddAfter(Node<T> node, T newData)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            Node<T> newNode = new Node<T>(newData);
            newNode.Next = node.Next;
            newNode.Previous = node;
            node.Next = newNode;

            if (node != _tail)
                node.Next.Previous = newNode;

            // if the node adding is tail node, then repointing tail node
            if (node == _tail)
                _tail = newNode;
            _size++;
        }

        public void AddBefore(T existingData, T newData)
        {
            Node<T> node = Find(existingData);
            if (node == null)
                throw new ArgumentException("existingData doesn't exist in the list");
            AddBefore(node, newData);
        }

        public void AddBefore(Node<T> node, T newData)
        {
            if (node == null)
                throw new ArgumentNullException("Node");

            Node<T> newNode     = new Node<T>(newData);
            if (node != _head)
                node.Previous.Next = newNode;
            newNode.Previous    = node.Previous;
            newNode.Next        = node;
            node.Previous       = newNode;

            if (node == _head)
                _head = newNode;
            _size++;
        }

        public void AddFirst(T data)
        {
            if (_head == null)
                addFirstItem(data);
            else
            {
                Node<T> newNode     = new Node<T>(data);
                _head.Previous       = newNode;
                newNode.Next        = _head;
                _head                = newNode;
            }

            _size++;
        }


        public void AddLast(T data)
        {
            
            if (_head == null)
                addFirstItem(data);
            else
            {
                Node<T> newNode  = new Node<T>(data);
                _tail.Next        = newNode;
                newNode.Previous = _tail;
                _tail             = newNode;
            }
        
            _size++;
        }

        public Node<T> Find(T data)
        {
            Node<T> node = findNode(_head, data);
            return node;
        }

        private Node<T> findNode(Node<T> node, T valueToCompare)
        {
            Node<T> result = null;
            if (Object.Equals(node.Data, valueToCompare))
                result = node;
            else if (node.Next != null)
                result = findNode(node.Next, valueToCompare);
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = _head;
            if (current != null)
            {
                do
                {
                    yield return current.Data;
                    current = current.Next;
                } while (current != null);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

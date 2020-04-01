using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DmTreeNode
{
    /// <summary>
    /// Узел дерева. Узел правильный, если его Address правильный.
    /// </summary>
    public class Node
    {

        #region Поля.

        List<int> address;
        List<Node> сhildren;
        string name;
        Node parent;
        #endregion


        #region Свойства.

        /// <summary>
        /// Адрес узла в дереве. Последовательность (...,n2,n1,n0), где n – Number. n0 принадлежит этому узлу, остальные элементы принадлежат его предкам: n1 – родителю, n2 – прародителю и т.д., первый элемент – первопредку. Если последовательность содержит один элемент, то соответствующий узел не имеет предков. Address должен быть уникален в пределах дерева. Address правильный, если описывает положение узла в дереве. На Address может ссылаться только один узел. "set" нужен для того, чтобы создавать локальные деревья. После обработки локальные деревья вписываются в нужное место других деревьев.
        /// </summary>
        public List<int> Address
        {
            get
            {
                return address.ToList();
            }
            set
            {
                address = value.ToList();
            }
        }

        /// <summary>
        /// Address в виде "(...,n2,n1,n0)".
        /// </summary>
        public string AddressAsString
        {
            get
            {
                return string.Join(",", address
                    .Select(x => x.ToString())
                    .ToArray());
            }
            set
            {
                address = value
                    .Split(',')
                    .Select(x => int.Parse(x))
                    .ToList();
            }
        }

        /// <summary>
        /// Дочерние узлы этого узла.
        /// </summary>
        public List<Node> Children
        {
            get
            {
                return сhildren;
            }
        }

        /// <summary>
        /// Количество потомков этого узла.
        /// </summary>
        public int DescendantCount
        {
            get
            {
                if (сhildren == null || !сhildren.Any())
                {
                    return 0;
                }

                int Count(Node root, ref int count)
                {
                    if (root.сhildren != null && root.сhildren.Any())
                    {
                        count += root.сhildren.Count;

                        foreach (Node child in root.сhildren)
                        {
                            Count(child, ref count);
                        }
                    }

                    return count;
                }

                int descendantCount = 0;
                return Count(this, ref descendantCount);
            }
        }

        /// <summary>
        /// Результат проверки наличия неправильного потомка у этого узла.
        /// </summary>
        public bool HasIncorrectDescendant
        {
            get
            {
                bool Find(Node root, ref bool result_)
                {
                    if (root.сhildren != null && root.сhildren.Any())
                    {
                        foreach (Node child in root.сhildren)
                        {
                            if (!child.IsCorrect)
                            {
                                result_ = true;
                                goto l;
                            }

                            Find(child, ref result_);
                        }
                    }

                l: return result_;
                }

                bool result = false;
                return Find(this, ref result);
            }
        }

        /// <summary>
        /// Количество непустых уровней с потомками этого узла. Потомки этого узла должны быть правильными.
        /// </summary>
        public int InnerLevelCount
        {
            get
            {
                if (сhildren == null || !сhildren.Any())
                {
                    return 0;
                }

                int InnerLevelMax(Node root, ref int max)
                {
                    if (root.сhildren != null && root.сhildren.Any())
                    {
                        foreach (Node child in root.сhildren)
                        {
                            if (child.Level > max)
                            {
                                max = child.Level;
                            }

                            InnerLevelMax(child, ref max);
                        }
                    }

                    return max;
                }

                int innerLevelMax = 0;
                return InnerLevelMax(this, ref innerLevelMax) - Level;
            }
        }

        /// <summary>
        /// Результат проверки правильности этого узла.
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                if (Level == 0 && Parent == null)
                {
                    return true;
                }

                if (Level != 0 &&
                    Parent != null &&
                    Address
                        .Take(Level)
                        .SequenceEqual(Parent.Address) &&
                    Parent.Children
                        .Count(x => x.Number == Number) == 1)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Уровень иерархии дерева, на котором находится этот узел. Количество предков этого узла, служащее общим признаком узлов, объединяющим их в поколение. Узлы на уровне L являются следующим поколением по отношению к узлам на уровне L-1.
        /// </summary>
        public int Level
        {
            get
            {
                return Address.Count - 1;
            }
        }

        /// <summary>
        /// Имя этого узла. Level дерева может содержать только узлы с уникальными именами. Имена, в отличие от Address, нельзя задавать любые, т.к. невозможно создать метод автоматического исправления имен. 
        /// </summary>
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (parent != null && parent.Children.Any(x => x.Name == value))
                {
                    throw new Exception("Это имя уже есть в Parent.Children этого узла.");
                }

                name = value;
            }
        }

        /// <summary>
        /// Номер этого узла на его Level. Номера на уровне дерева должны быть уникальными, чтобы адреса в этом дереве были уникальными.
        /// </summary>
        public int Number
        {
            get
            {
                return Address.Last();
            }
        }

        /// <summary>
        /// Ссылка на родитель этого узла.
        /// </summary>
        public Node Parent
        {
            get
            {
                return parent;
            }
        }
        #endregion


        #region Конструкторы.

        public Node()
        {
            address = new List<int>() { 0 };
        }
        public Node(string name)
        {
            address = new List<int> { 0 };
            Name = name;
        }
        public Node(IEnumerable<int> address)
        {
            this.address = address.ToList();
        }
        public Node(IEnumerable<Node> children)
        {
            address = new List<int>() { 0 };
            SetChildren(children);
        }
        public Node(string name, string address)
        {
            AddressAsString = address;
            Name = name;
        }
        public Node(string name, IEnumerable<int> address)
        {
            this.address = address.ToList();
            Name = name;
        }
        public Node(string name, IEnumerable<Node> children)
        {
            address = new List<int>() { 0 };
            Name = name;
            SetChildren(children);
        }
        public Node(IEnumerable<int> address, IEnumerable<Node> children)
        {
            this.address = address.ToList();
            SetChildren(children);
        }
        public Node(string name, string address, IEnumerable<Node> children)
        {
            AddressAsString = address;
            Name = name;
            SetChildren(children);
        }
        public Node(string name, IEnumerable<int> address, IEnumerable<Node> children)
        {
            this.address = address.ToList();
            Name = name;
            SetChildren(children);
        }
        #endregion


        #region Делегаты.

        /// <summary>
        /// Возвращает подпись узла в псевдографическом изображении дерева. Аргумент ToString().
        /// </summary>
        /// <param name="node">Ресурс подписи. Аргумент лямбда-выражения, передающегося в делегат.</param>
        /// <returns>Подпись узла в псевдографическом изображении дерева.</returns>
        public delegate string Title(Node node);
        #endregion


        #region Методы.

        /// <summary>
        /// Добавляет новый узел в Children этого узла.
        /// </summary>
        /// <param name="newChild">Узел, добавляемый в Children этого узла.</param>
        /// <returns>Этот узел с новым узлом в Children.</returns>
        public Node Add(Node newChild)
        {
            if (сhildren == null)
            {
                сhildren = new List<Node>();
            }

            if (сhildren.Any(x => x.Name == newChild.Name))
            {
                throw new Exception("Узел с таким именем на данном уровне уже существует.");
            }

            newChild.parent = this;
            newChild.address = Address;
            newChild.address.Add(сhildren.Count);
            сhildren.Add(newChild.DescendantsCorrect());

            return this;
        }

        /// <summary>
        /// Добавляет новые узлы в Children этого узла.
        /// </summary>
        /// <param name="newChildren">Узлы, добавляемые в Children этого узла.</param>
        /// <returns>Этот узел с новыми узлами в Children.</returns>
        public Node AddChildren(IEnumerable<Node> newChildren)
        {
            foreach (Node newChild in newChildren)
            {
                Add(newChild);
            }

            return this;
        }

        /// <summary>
        /// Добавляет новые узлы в Children этого узла.
        /// </summary>
        /// <param name="newChildren">Узлы, копии которых добавляются в Children этого узла.</param>
        /// <returns>Этот узел с новыми узлами в Children.</returns>
        public Node AddChildrenCopy(IEnumerable<Node> newChildren)
        {
            foreach (Node newChild in newChildren)
            {
                AddCopy(newChild);
            }

            return this;
        }

        /// <summary>
        /// Добавляет новый узел в Children этого узла.
        /// </summary>
        /// <param name="newChild">Узел, копия которого добавляется в Children этого узла.</param>
        /// <returns>Этот узел с новым узлом в Children.</returns>
        public Node AddCopy(Node newChild)
        {
            if (сhildren == null)
            {
                сhildren = new List<Node>();
            }

            if (сhildren.Any(x => x.Name == newChild.Name))
            {
                throw new Exception("Узел с таким именем на данном уровне уже существует.");
            }

            Node newChildCopy = newChild.Copy();
            newChildCopy.parent = this;
            newChildCopy.address = Address;
            newChildCopy.address.Add(сhildren.Count);
            сhildren.Add(newChildCopy.DescendantsCorrect());

            return this;
        }

        /// <summary>
        /// Возвращает копию этого узла.
        /// </summary>
        /// <returns>Копия этого узла.</returns>
        public Node Copy()
        {
            Node copy = new Node(Name, Address);

            if (сhildren != null)
            {
                foreach (Node child in сhildren)
                {
                    Node newChild = child.Copy();
                    newChild.parent = copy;
                    copy.Add(newChild);
                }
            }

            return copy;
        }

        /// <summary>
        /// Возвращает все потомки этого узла.
        /// </summary>
        /// <returns></returns>
        public List<Node> Descendants()
        {
            if (сhildren == null || !сhildren.Any())
            {
                return null;
            }

            void GetDescendants(Node root, ref List<Node> descendants_)
            {
                foreach (Node child in root.сhildren)
                {
                    descendants_.Add(child);

                    if (child.сhildren != null && child.сhildren.Any())
                    {
                        GetDescendants(child, ref descendants_);
                    }

                }
            }

            List<Node> descendants = new List<Node>();
            GetDescendants(this, ref descendants);

            return descendants.Any() ? descendants : null;
        }

        /// <summary>
        /// Делает потомки этого узла правильными.
        /// </summary>
        /// <returns>Этот узел с правильными потомками.</returns>
        public Node DescendantsCorrect()
        {
            if (сhildren != null && сhildren.Any())
            {
                int i = 0;

                foreach (Node child in сhildren)
                {
                    child.address = Address;
                    child.address.Add(i++);
                    child.DescendantsCorrect();
                }
            }

            return this;
        }

        /// <summary>
        /// Возвращает узел с указанным Address из дерева, корнем которого является этот узел.
        /// </summary>
        /// <param name="address">Address искомого узла.</param>
        /// <returns>Искомый узел.</returns>
        public Node Find(IEnumerable<int> address)
        {
            Node Find(Node root, ref Node foundNode_, IEnumerable<int> address_)
            {
                if (root.address.SequenceEqual(address_))
                {
                    foundNode_ = root;
                    goto l;
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, ref foundNode_, address_);
                    }
                }

            l: return foundNode_;
            }

            Node foundNode = null;
            return Find(this, ref foundNode, address);
        }

        /// <summary>
        /// Возвращает узлы с указанным IsCorrect из дерева, корнем которого является этот узел.
        /// </summary>
        /// <param name="isCorrect">IsCorrect искомых узлов.</param>
        /// <returns>Искомые узлы.</returns>
        public List<Node> Find(bool isCorrect)
        {
            void Find(Node root, List<Node> foundNodeList_, bool isCorrect_)
            {
                if (root.IsCorrect == isCorrect_)
                {
                    if (foundNodeList_ == null)
                    {
                        foundNodeList_ = new List<Node>();
                    }

                    foundNodeList_.Add(root);
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, foundNodeList_, isCorrect_);
                    }
                }
            }

            List<Node> foundNodeList = new List<Node>();
            Find(this, foundNodeList, isCorrect);

            return foundNodeList.Any() ? foundNodeList : null;
        }

        /// <summary>
        /// Возвращает узлы с указанным Name из дерева, корнем которого является этот узел.
        /// </summary>
        /// <param name="name">Name искомых узлов.</param>
        /// <returns>Искомые узлы.</returns>
        public List<Node> Find(string name)
        {
            void Find(Node root, List<Node> foundNodeList_, string name_)
            {
                if (root.Name == name_)
                {
                    if (foundNodeList_ == null)
                    {
                        foundNodeList_ = new List<Node>();
                    }

                    foundNodeList_.Add(root);
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, foundNodeList_, name_);
                    }
                }
            }

            List<Node> foundNodeList = new List<Node>();
            Find(this, foundNodeList, name);

            return foundNodeList.Any() ? foundNodeList : null;
        }

        /// <summary>
        /// Возвращает узлы с указанным Number из дерева, корнем которого является этот узел.
        /// </summary>
        /// <param name="number">Number искомых узлов.</param>
        /// <returns>Искомые узлы.</returns>
        public List<Node> Find(int number)
        {
            void Find(Node root, List<Node> foundNodeList_, int number_)
            {
                if (root.Number == number_)
                {
                    foundNodeList_.Add(root);
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, foundNodeList_, number_);
                    }
                }
            }

            List<Node> foundNodeList = new List<Node>();
            Find(this, foundNodeList, number);

            return foundNodeList.Any() ? foundNodeList : null;
        }

        /// <summary>
        /// Загружает дерево из указанного файла CSV в этот узел, который будет являться его корнем. Предполагается, что дерево в файле имеет правильные потомки.
        /// </summary>
        /// <param name="path">Путь к файлу, из которого будет загружено дерево.</param>
        /// <returns>Этот узел.</returns>
        public Node Load(string path)
        {
            List<Node> nodes = File.ReadAllLines(path)
                    .Skip(1)
                    .Select(x => x.Split(';'))
                    .Select(x => new Node
                    {
                        Name = x[0],
                        AddressAsString = x[1]
                    })
                    .ToList();

            nodes.Sort(new NodeComparerAddress());

            Node root = nodes[0];

            foreach (Node child in nodes.Skip(1))
            {
                Node newParent = root.Find(child.address
                    .Take(child.Level)
                    .ToArray());

                if (newParent == null)
                {
                    continue;
                }

                newParent.AddCopy(child);
            }

            return root;
        }

        /// <summary>
        /// Сохраняет дерево, корнем которого является этот узел, как таблицу со столбцами "Name", "Address", разделенными символом ";", в файл CSV. Этих столбцов достаточно, чтобы описать узел, т.к. Address узла содержит Address его родителя. Потомки этого узла должны быть правильными. 
        /// </summary>
        /// <param name="path">Путь к файлу, в который будет сохранено дерево.</param>
        /// <returns>Этот узел.</returns>
        public Node Save(string path)
        {
            void CreateTable(Node root, List<string> table_)
            {
                table_.Add(string.Concat(root.Name, ";", root.AddressAsString));

                if (root.сhildren != null)
                {
                    foreach (Node node in root.Children)
                    {
                        CreateTable(node, table_);
                    }
                }
            }

            List<string> table = new List<string> { "Name;Address" };
            CreateTable(this, table);

            if (!path.EndsWith(".csv"))
            {
                path += ".csv";
            }

            File.WriteAllLines(path, table.ToArray());

            return this;
        }

        /// <summary>
        /// Возвращает узлы из дерева, корнем которого является этот узел, Name которых содержит указанную подстроку.
        /// </summary>
        /// <param name="nameSubstring">Подстрока в Name искомых узлов.</param>
        /// <returns>Искомые узлы.</returns>
        public List<Node> Search(string nameSubstring)
        {
            void Find(Node root, List<Node> foundNodeList_, string nameSubstring_)
            {
                if (root.Name.Contains(nameSubstring_))
                {
                    if (foundNodeList_ == null)
                    {
                        foundNodeList_ = new List<Node>();
                    }

                    foundNodeList_.Add(root);
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, foundNodeList_, nameSubstring_);
                    }
                }
            }

            List<Node> foundNodeList = new List<Node>();
            Find(this, foundNodeList, nameSubstring);

            return foundNodeList.Any() ? foundNodeList : null;
        }

        /// <summary>
        /// Возвращает узлы из дерева, корнем которого является этот узел, Name которых содержит хотя бы одну из указанных подстрок.
        /// </summary>
        /// <param name="nameSubstrings">Подстроки в Name искомых узлов.</param>
        /// <returns>Искомые узлы.</returns>
        public List<Node> Search(IEnumerable<string> nameSubstrings)
        {
            void Find(Node root, List<Node> foundNodeList_, IEnumerable<string> nameSubstrings_)
            {
                if (nameSubstrings_.Any(x => root.Name.Contains(x)))
                {
                    if (foundNodeList_ == null)
                    {
                        foundNodeList_ = new List<Node>();
                    }

                    foundNodeList_.Add(root);
                }

                if (root.сhildren != null)
                {
                    foreach (Node child in root.сhildren)
                    {
                        Find(child, foundNodeList_, nameSubstrings_);
                    }
                }
            }

            List<Node> foundNodeList = new List<Node>();
            Find(this, foundNodeList, nameSubstrings);

            return foundNodeList.Any() ? foundNodeList : null;
        }

        /// <summary>
        /// Задает новый Children этому узлу.
        /// </summary>
        /// <param name="newChildren">Узлы, которые составят новый Children этого узла.</param>
        /// <returns>Этот узел с новым Children.</returns>
        public Node SetChildren(IEnumerable<Node> newChildren)
        {
            сhildren = new List<Node>();

            foreach (Node child in newChildren)
            {
                Add(child);
            }

            return this;
        }

        /// <summary>
        /// Задает новый Children этому узлу.
        /// </summary>
        /// <param name="newChildren">Узлы, копии которых составят новый Children этого узла.</param>
        /// <returns>Этот узел с новым Children.</returns>
        public Node SetChildrenCopy(IEnumerable<Node> newChildren)
        {
            сhildren = new List<Node>();

            foreach (Node child in newChildren)
            {
                AddCopy(child);
            }

            return this;
        }

        /// <summary>
        /// Сортирует потомки этого узла на их уровнях согласно указанному компаратору.
        /// </summary>
        /// <param name="comparer">Компаратор, описывающий механизм сравнения узлов.</param>
        /// <returns>Этот узел с отсортированными потомками на их уровнях.</returns>
        public Node Sort(IComparer<Node> comparer)
        {
            if (сhildren != null)
            {
                сhildren.Sort(comparer);

                foreach (Node child in сhildren)
                {
                    child.Sort(comparer);
                }
            }

            return this;
        }

        /// <summary>
        /// Сортирует потомки этого узла на их уровнях согласно указанному признаку сравнения узлов.
        /// </summary>
        /// <param name="sign">Признак сравнения узлов.</param>
        /// <returns>Этот узел с отсортированными потомками на их уровнях.</returns>
        public Node Sort(SortingSign sign)
        {
            switch (sign)
            {
                case SortingSign.Name:
                    Sort(new NodeComparerName());
                    break;
                case SortingSign.Address:
                    Sort(new NodeComparerAddress());
                    break;
                case SortingSign.Number:
                    Sort(new NodeComparerNumber());
                    break;
            }

            return this;
        }

        /// <summary>
        /// Возвращает строку с псевдографическим изображением дерева, корнем которого является этот узел. Изображение состоит из соединительных линий от предков к потомкам и подписей узлов. Узлы подписаны соответствующими именами.
        /// </summary>
        /// <returns>Строка с псевдографическим изображением дерева.</returns>
        public override string ToString()
        {
            return ToString(null);
        }

        /// <summary>
        /// Возвращает строку с псевдографическим изображением дерева, корнем которого является этот узел. Изображение состоит из соединительных линий от предков к потомкам и подписей узлов. Подпись узла создается делегатом Title.
        /// </summary>
        /// <param name="title">Подпись узла в псевдографическим изображением дерева..</param>
        /// <returns>Строка с псевдографическим изображением дерева.</returns>
        public string ToString(Title title)
        {
            void CreateView(Node node, Title title_, List<string> prefixList_, ref string view_)
            {
                // Строка псевдографического изображения дерева.
                string line = "";

                while (prefixList_.Count < node.Level + 1)
                {
                    prefixList_.Add("");
                }

                if (string.IsNullOrEmpty(view_))
                {
                    line = "■ ";
                }
                else
                {
                    if (node != node.Parent.Children.Last())
                    {
                        line = "├─■ ";
                        prefixList_[node.Level] = "│ ";
                    }
                    else
                    {
                        line = "└─■ ";
                        prefixList_[node.Level] = "  ";
                    }

                    for (int i = 0; i < node.Level; i++)
                    {
                        line = prefixList_[node.Level - 1 - i] + line;
                    }
                }

                // Добавление подписи к узлу.
                line += title_ == null ? node.Name : title_(node);

                if (string.IsNullOrEmpty(view_))
                {
                    view_ = line;
                }
                else
                {
                    view_ = string.Concat(view_, "\n", line);
                }

                if (node.Children != null)
                {
                    foreach (Node child in node.Children)
                    {
                        CreateView(child, title_, prefixList_, ref view_);
                    }
                }
            }

            List<string> prefixList = new List<string>();
            string view = "";

            CreateView(this, title, prefixList, ref view);

            return view;
        }
        #endregion
    }
}
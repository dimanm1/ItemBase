using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DmTreeNode;

class Program
{
    static void Main()
    {
        // Примеры.

        #region Copy.
        {
            //Node myNode = new Node("my node");
            //Node myNodeRef = myNode;
            //Node myNodeCopy = myNode.Copy();

            //myNode.Name += " *";

            //Console.WriteLine("myNode.Name: " + myNode.Name);
            //Console.WriteLine("myNodeRef.Name: " + myNodeRef.Name);
            //Console.WriteLine("myNodeCopy.Name: " + myNodeCopy.Name);

            ////myNode.Name: my node *
            ////myNodeRef.Name: my node *
            ////myNodeCopy.Name: my node
        }
        #endregion

        #region Создание подписи узла в псевдографическом изображении дерева.
        {
            //Node myNode = new Node("my node", "1,2,3");

            //Console.WriteLine(myNode);
            //Console.WriteLine(myNode.ToString(x => x.Name.ToUpper()));
            //Console.WriteLine(myNode.ToString(x =>
            //    $"{x.Level}:{x.Number}({x.AddressAsString}) {x.Name}"));

            ////■ my node
            ////■ MY NODE
            ////■ 2:3(1,2,3) my node
        }
        #endregion

        #region Address возвращает копию значения и принимает копию value.
        {
            //List<int> a1 = new List<int> { 1, 2, 3 };
            //Node x = new Node(a1);
            //List<int> a2 = x.Address;

            //a1.Add(11);
            //a2.Add(22);

            //Console.WriteLine("a1: " + string.Join(",", a1));
            //Console.WriteLine("a2: " + string.Join(",", a2));
            //Console.WriteLine("x.Address: " + x.AddressAsString);

            ////a1: 1,2,3,11
            ////a2: 1,2,3,22
            ////x.Address: 1,2,3
        }
        #endregion

        #region Add добавляет ссылку на потомка.
        {
            //Node x = new Node("x");
            //Node y = new Node("y");
            //x.Add(y);
            //y.Name += " *";

            //Console.WriteLine(y.Parent);
            ////■ x
            ////└─■ y *
        }
        #endregion

        #region Add задает правильные адреса добавляемым потомкам.
        {
            //Node x = new Node("x", "5,6,7,8");
            //Node y = new Node("y", "22,33");
            //Node z = new Node("z", "1,2,3,4");
            //Node tree = new Node("tree", "11,22")
            //    .Add(x.Add(y.Add(z)));

            //Console.WriteLine(tree.ToString(t => $"({t.AddressAsString}) {t.IsCorrect}"));

            ////■ (11,22) False
            ////└─■ (11,22,0) True
            ////  └─■ (11,22,0,0) True
            ////    └─■ (11,22,0,0,0) True
        }
        #endregion

        #region Цепочка Add. 
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x")
            //        .Add(new Node("y")))
            //    .Add(new Node("z"));

            //Console.WriteLine(tree);

            ////■ tree
            ////├─■ x
            ////│ └─■ y
            ////└─■ z
        }
        #endregion

        #region Add должен добавлять в дерево только уникальный потомок.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x"))
            //    .Add(new Node("y"))
            //    .Add(new Node("z"));

            //Node w = new Node("w");

            //foreach (Node child in tree.Children)
            //{
            //    child.Add(w);
            //}

            //Console.WriteLine(tree);
            //Console.WriteLine("\nw.Parent");
            //Console.WriteLine(w.Parent);

            ////■ tree
            ////├─■ x
            ////│ └─■ w
            ////├─■ y
            ////│ └─■ w
            ////└─■ z
            ////  └─■ w

            ////w.Parent
            ////■ z
            ////└─■ w
        }
        #endregion

        #region AddCopy.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x"))
            //    .Add(new Node("y"))
            //    .Add(new Node("z"));

            //Node a = new Node("a");
            //Node b = new Node("b");
            //a.Add(b);

            //foreach (Node child in tree.Children)
            //{
            //    child.AddCopy(a);
            //}

            //foreach (Node child in tree.Children)
            //{
            //    Console.WriteLine(child
            //        .Children[0]
            //        .Children[0]
            //        .Parent
            //        .Parent
            //        .Name);
            //}

            //Console.WriteLine(b.Parent.Parent == null);

            ////x
            ////y
            ////z
            ////True
        }
        #endregion

        #region AddChildren.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x"));

            //Node[] nodes =
            //{
            //    new Node("y"),
            //    new Node("z")
            //};

            //tree.AddChildren(nodes);

            //foreach (Node node in nodes)
            //{
            //    node.Name += " *";
            //}

            //Console.WriteLine(tree);

            ////■ tree
            ////├─■ x
            ////├─■ y *
            ////└─■ z *
        }
        #endregion

        #region AddChildrenCopy.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x"));

            //Node[] nodes =
            //{
            //    new Node("y"),
            //    new Node("z")
            //};

            //tree.AddChildrenCopy(nodes);

            //foreach (Node node in nodes)
            //{
            //    node.Name += " *";
            //}

            //Console.WriteLine(tree);

            ////■ tree
            ////├─■ x
            ////├─■ y
            ////└─■ z
        }
        #endregion

        #region SetChildren добавляет ссылки на будущие потомки.
        {
            //Node[] nodes =
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildren(nodes);

            //foreach (Node node in nodes)
            //{
            //    node.Name += " *";
            //}

            //Console.WriteLine(tree);

            ////■ tree
            ////├─■ x *
            ////├─■ y *
            ////└─■ z *
        }
        #endregion

        #region SetChildrenCopy добавляет ссылки копии будущих потомков.
        {
            //Node[] nodes =
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //foreach (Node node in nodes)
            //{
            //    node.Name += " *";
            //}

            //Console.WriteLine(tree);

            ////■ tree
            ////├─■ x
            ////├─■ y
            ////└─■ z
        }
        #endregion

        #region Level.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("x")
            //        .Add(new Node("y")))
            //    .Add(new Node("z"));

            //Console.WriteLine(tree.ToString(x => $"{x.Level}: {x.Name}"));

            ////■ 0: tree
            ////├─■ 1: x
            ////│ └─■ 2: y
            ////└─■ 1: z
        }
        #endregion

        #region InnerLevelCount.
        {
            //Node tree = new Node("tree", "1,2,3,4")

            //    .Add(new Node("0")
            //        .Add(new Node("0.0")
            //            .Add(new Node("0.0.0")
            //                .Add(new Node("0.0.0.0")))))

            //    .Add(new Node("1")
            //        .Add(new Node("1.0"))
            //        .Add(new Node("1.1"))
            //        .Add(new Node("1.2")))

            //    .Add(new Node("2"))
            //    .Add(new Node("3"))

            //    .Add(new Node("4")
            //        .Add(new Node("4.0")
            //            .Add(new Node("4.0.0"))));

            //Console.WriteLine(tree.ToString(x => x.Level.ToString()));
            //Console.WriteLine(tree.InnerLevelCount);

            ////■ 3
            ////├─■ 4
            ////│ └─■ 5
            ////│   └─■ 6
            ////│     └─■ 7
            ////├─■ 4
            ////│ ├─■ 5
            ////│ ├─■ 5
            ////│ └─■ 5
            ////├─■ 4
            ////├─■ 4
            ////└─■ 4
            ////  └─■ 5
            ////    └─■ 6
            ////4
        }
        #endregion

        #region DescendantCount.
        {
            //Node tree = new Node("tree", "1,2,3,4")

            //    .Add(new Node("1")
            //        .Add(new Node("2")
            //            .Add(new Node("3")
            //                .Add(new Node("4")))))

            //    .Add(new Node("5")
            //        .Add(new Node("6"))
            //        .Add(new Node("7"))
            //        .Add(new Node("8")))

            //    .Add(new Node("9"))
            //    .Add(new Node("10"))

            //    .Add(new Node("11")
            //        .Add(new Node("12")
            //            .Add(new Node("13"))));

            //Console.WriteLine(tree.DescendantCount);

            ////13
        }
        #endregion

        #region IsCorrect.
        {
            //Node x1 = new Node("x1");
            //Node x2 = new Node("x2", "50");
            //Node x3 = new Node("x3", "1,2,3");

            //Node y1 = new Node("y1");
            //Node y2 = new Node("y2");
            //Node y3 = new Node("y3");
            //Node y4 = new Node("y4");
            //Node y5 = new Node("y5");

            //x3
            //    .Add(y1)
            //    .Add(y2)
            //    .Add(y3)
            //    .Add(y4)
            //    .Add(y5);

            //y1.AddressAsString = "1,2,3,25";
            //y2.AddressAsString = "1,2,3,50";
            //y3.AddressAsString = y2.AddressAsString;
            //y4.AddressAsString = "123";
            //y5.AddressAsString = "1,2,5,1";

            //Console.WriteLine(x1.ToString(x => $"({x.AddressAsString}) {x.Name}"));
            //Console.WriteLine(x2.ToString(x => $"({x.AddressAsString}) {x.Name}"));
            //Console.WriteLine(x3.ToString(x => $"({x.AddressAsString}) {x.Name}"));
            //Console.WriteLine("Псевдографический рисунок правильный только у дерева с правильными потомками.\n");

            //Console.WriteLine($"x1: {x1.IsCorrect}");
            //Console.WriteLine($"x2: {x2.IsCorrect} <= Важна уникальность Number на Level, а не значение Number.");
            //Console.WriteLine($"x3: {x3.IsCorrect} <= У потомка должна быть ссылка на родитель.");

            //Console.WriteLine($"\ny1: {y1.IsCorrect} <= Последовательность из Number предков == Address родителя.");
            //Console.WriteLine($"y2: {y2.IsCorrect} <= Number не уникален на данном Level.");
            //Console.WriteLine($"y3: {y3.IsCorrect}");
            //Console.WriteLine($"y4: {y4.IsCorrect}");
            //Console.WriteLine($"y5: {y5.IsCorrect}");

            ////■ (0) x1
            ////■ (50) x2
            ////■ (1,2,3) x3
            ////├─■ (1,2,3,25) y1
            ////├─■ (1,2,3,50) y2
            ////├─■ (1,2,3,50) y3
            ////├─■ (123) y4
            ////│ └─■ (1,2,5,1) y5
            ////Псевдографический рисунок правильный только у дерева с правильными потомками.

            ////x1: True
            ////x2: True <= Важна уникальность Number на Level, а не значение Number.
            ////x3: False <= У потомка должна быть ссылка на родитель.

            ////y1: True <= Последовательность из Number предков == Address родителя.
            ////y2: False <= Number не уникален на данном Level.
            ////y3: False
            ////y4: False
            ////y5: False
        }
        #endregion

        #region HasIncorrectDescendant.
        {
            //Node x = new Node("x");
            //Node y = new Node("y");
            //Node z = new Node("z");
            //Node tree = new Node("tree", "11,22")
            //    .Add(x.Add(y.Add(z)));

            //Console.WriteLine(tree.HasIncorrectDescendant);

            //z.AddressAsString = "1,2,3,4,5,6";

            //Console.WriteLine(tree.HasIncorrectDescendant);

            ////False
            ////True
        }
        #endregion

        #region DescendantsCorrect.
        {
            //Node x = new Node("x");
            //Node y = new Node("y");
            //Node z = new Node("z");
            //Node tree = new Node("tree", "11,22")
            //    .Add(x.Add(y.Add(z)));

            //x.AddressAsString = y.AddressAsString = z.AddressAsString = "1,2,3,4";

            //Console.WriteLine(tree.ToString(t => $"{t.AddressAsString} {t.IsCorrect}"));
            //Console.WriteLine("Псевдографический рисунок правильный только у дерева с правильными потомками.\n");
            //Console.WriteLine(tree
            //    .DescendantsCorrect()
            //    .ToString(t => $"{t.AddressAsString} {t.IsCorrect}"));

            ////■ 11,22 False
            ////└─■ 1,2,3,4 False
            ////└─■ 1,2,3,4 False
            ////└─■ 1,2,3,4 False
            ////Псевдографический рисунок правильный только у дерева с правильными потомками.

            ////■ 11,22 False
            ////└─■ 11,22,0 True
            ////  └─■ 11,22,0,0 True
            ////    └─■ 11,22,0,0,0 True
        }
        #endregion

        #region Sort.
        {
            //List<Node> nodes = new List<Node>
            //{
            //    new Node("c"),
            //    new Node("b"),
            //    new Node("a")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //tree.Children[0] // c
            //    .SetChildrenCopy(nodes);
            //tree.Children[0].Children[2] // c.a
            //    .SetChildrenCopy(nodes);

            //Console.WriteLine("После сортировки по Name.");
            //Console.WriteLine(tree
            //    .Sort(new NodeComparerName())
            //    .ToString(x => $"({x.AddressAsString}) {x.Name}"));

            //Console.WriteLine("\nПосле сортировки по Address.");
            //Console.WriteLine(tree
            //    .Sort(new NodeComparerAddress())
            //    .ToString(x => $"({x.AddressAsString}) {x.Name}"));

            //Console.WriteLine("\nПосле сортировки по Number.");
            //Console.WriteLine(tree
            //    .Sort(new NodeComparerName())
            //    .Sort(new NodeComparerNumber())
            //    .ToString(x => $"({x.AddressAsString}) {x.Name}"));

            ////После сортировки по Name.
            ////■ (0) tree
            ////├─■ (0,2) a
            ////├─■ (0,1) b
            ////└─■ (0,0) c
            ////  ├─■ (0,0,2) a
            ////  │ ├─■ (0,0,2,2) a
            ////  │ ├─■ (0,0,2,1) b
            ////  │ └─■ (0,0,2,0) c
            ////  ├─■ (0,0,1) b
            ////  └─■ (0,0,0) c

            ////После сортировки по Address.
            ////■ (0) tree
            ////├─■ (0,0) c
            ////│ ├─■ (0,0,0) c
            ////│ ├─■ (0,0,1) b
            ////│ └─■ (0,0,2) a
            ////│   ├─■ (0,0,2,0) c
            ////│   ├─■ (0,0,2,1) b
            ////│   └─■ (0,0,2,2) a
            ////├─■ (0,1) b
            ////└─■ (0,2) a

            ////После сортировки по Number.
            ////■ (0) tree
            ////├─■ (0,0) c
            ////│ ├─■ (0,0,0) c
            ////│ ├─■ (0,0,1) b
            ////│ └─■ (0,0,2) a
            ////│   ├─■ (0,0,2,0) c
            ////│   ├─■ (0,0,2,1) b
            ////│   └─■ (0,0,2,2) a
            ////├─■ (0,1) b
            ////└─■ (0,2) a
        }
        #endregion

        #region Find. Поиск по Address.
        {
            //List<Node> nodes = new List<Node>
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //foreach (Node child in tree.Children)
            //{
            //    child.SetChildrenCopy(nodes);
            //}

            //Console.WriteLine(tree.ToString(t => $"({t.AddressAsString}) {t.Name}"));
            //Console.WriteLine("\nНайденный");
            //Console.WriteLine(tree
            //    .Find(new List<int> { 0, 1, 1 })
            //    .ToString(t => $"({t.AddressAsString}) {t.Name}"));

            ////■ (0) tree
            ////├─■ (0,0) x
            ////│ ├─■ (0,0,0) x
            ////│ ├─■ (0,0,1) y
            ////│ └─■ (0,0,2) z
            ////├─■ (0,1) y
            ////│ ├─■ (0,1,0) x
            ////│ ├─■ (0,1,1) y
            ////│ └─■ (0,1,2) z
            ////└─■ (0,2) z
            ////  ├─■ (0,2,0) x
            ////  ├─■ (0,2,1) y
            ////  └─■ (0,2,2) z

            ////Найденный
            ////■ (0,1,1) y
        }
        #endregion

        #region Find. Поиск по Name.
        {
            //List<Node> nodes = new List<Node>
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //foreach (Node child in tree.Children)
            //{
            //    child.SetChildrenCopy(nodes);
            //}

            //Console.WriteLine(tree.ToString(t => $"({t.AddressAsString}) {t.Name}"));
            //Console.WriteLine("\nНайденные");

            //int i = 1;

            //foreach (Node y in tree.Find("y"))
            //{
            //    Console.WriteLine(i++ + ". ");
            //    Console.WriteLine(y.ToString(t => $"({t.AddressAsString}) {t.Name}"));
            //}

            ////■ (0) tree
            ////├─■ (0,0) x
            ////│ ├─■ (0,0,0) x
            ////│ ├─■ (0,0,1) y
            ////│ └─■ (0,0,2) z
            ////├─■ (0,1) y
            ////│ ├─■ (0,1,0) x
            ////│ ├─■ (0,1,1) y
            ////│ └─■ (0,1,2) z
            ////└─■ (0,2) z
            ////  ├─■ (0,2,0) x
            ////  ├─■ (0,2,1) y
            ////  └─■ (0,2,2) z

            ////Найденные
            ////1.
            ////■ (0,0,1) y
            ////2.
            ////■ (0,1) y
            ////├─■ (0,1,0) x
            ////├─■ (0,1,1) y
            ////└─■ (0,1,2) z
            ////3.
            ////■ (0,1,1) y
            ////4.
            ////■ (0,2,1) y
        }
        #endregion

        #region Find. Поиск по Number.
        {
            //List<Node> nodes = new List<Node>
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //foreach (Node child in tree.Children)
            //{
            //    child.SetChildrenCopy(nodes);
            //}

            //Console.WriteLine(tree.ToString(t => $"({t.AddressAsString}) {t.Name}"));
            //Console.WriteLine("\nНайденные");

            //int i = 1;

            //foreach (Node y in tree.Find(2))
            //{
            //    Console.WriteLine(i++ + ". ");
            //    Console.WriteLine(y.ToString(t => $"({t.AddressAsString}) {t.Name}"));
            //}

            ////■ (0) tree
            ////├─■ (0,0) x
            ////│ ├─■ (0,0,0) x
            ////│ ├─■ (0,0,1) y
            ////│ └─■ (0,0,2) z
            ////├─■ (0,1) y
            ////│ ├─■ (0,1,0) x
            ////│ ├─■ (0,1,1) y
            ////│ └─■ (0,1,2) z
            ////└─■ (0,2) z
            ////  ├─■ (0,2,0) x
            ////  ├─■ (0,2,1) y
            ////  └─■ (0,2,2) z

            ////Найденные
            ////1.
            ////■ (0,0,2) z
            ////2.
            ////■ (0,1,2) z
            ////3.
            ////■ (0,2) z
            ////├─■ (0,2,0) x
            ////├─■ (0,2,1) y
            ////└─■ (0,2,2) z
            ////4.
            ////■ (0,2,2) z
        }
        #endregion

        #region Find. Поиск по IsCorrect.
        {
            //List<Node> nodes = new List<Node>
            //{
            //    new Node("x"),
            //    new Node("y"),
            //    new Node("z")
            //};

            //Node tree = new Node("tree")
            //    .SetChildrenCopy(nodes);

            //foreach (Node child in tree.Children)
            //{
            //    child.SetChildrenCopy(nodes);
            //}

            //tree.Children[1]
            //    .AddressAsString = "1,2,3,4";
            //tree.Children[1].Children[2]
            //    .AddressAsString = "1,2,3,4";

            //Console.WriteLine(tree.ToString(t => $"({t.AddressAsString}) {t.Name} {t.IsCorrect}"));
            //Console.WriteLine("\nНайденные");

            //int i = 1;

            //foreach (Node y in tree.Find(false))
            //{
            //    Console.WriteLine(i++ + ". ");
            //    Console.WriteLine(y.ToString(t => $"({t.AddressAsString}) {t.Name} {t.IsCorrect}"));
            //}

            //Console.WriteLine("Все найденные узлы имеют последовательность из Number предков != Address родителя.");

            ////■ (0) tree True
            ////├─■ (0,0) x True
            ////│ ├─■ (0,0,0) x True
            ////│ ├─■ (0,0,1) y True
            ////│ └─■ (0,0,2) z True
            ////│   ├─■ (1,2,3,4) y False
            ////│ ├─■ (0,1,0) x False
            ////│ ├─■ (0,1,1) y False
            ////│ │ └─■ (1,2,3,4) z False
            ////└─■ (0,2) z True
            ////  ├─■ (0,2,0) x True
            ////  ├─■ (0,2,1) y True
            ////  └─■ (0,2,2) z True

            ////Найденные
            ////1.
            ////■ (1,2,3,4) y False
            ////├─■ (0,1,0) x False
            ////├─■ (0,1,1) y False
            ////│ └─■ (1,2,3,4) z False
            ////2.
            ////■ (0,1,0) x False
            ////3.
            ////■ (0,1,1) y False
            ////4.
            ////■ (1,2,3,4) z False
            ////Все найденные узлы имеют последовательность из Number предков != Address родителя.
        }
        #endregion

        #region Descendants.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("a")
            //        .Add(new Node("b"))
            //        .Add(new Node("c")
            //            .Add(new Node("d")))
            //        .Add(new Node("e")))
            //    .Add(new Node("f")
            //        .Add(new Node("g"))
            //        .Add(new Node("h")))
            //    .Add(new Node("i"));

            //Console.WriteLine(tree);
            //Console.WriteLine("Количество потомков: " + tree.DescendantCount);
            //Console.WriteLine("Список потомков");

            //int i = 1;

            //foreach (Node descendant in tree.Descendants())
            //{
            //    Console.WriteLine($"{i++}. {descendant.Name}");
            //}

            ////■ tree
            ////├─■ a
            ////│ ├─■ b
            ////│ ├─■ c
            ////│ │ └─■ d
            ////│ └─■ e
            ////├─■ f
            ////│ ├─■ g
            ////│ └─■ h
            ////└─■ i
            ////Количество потомков: 9
            ////Список потомков
            ////1.a
            ////2.b
            ////3.c
            ////4.d
            ////5.e
            ////6.f
            ////7.g
            ////8.h
            ////9.i
        }
        #endregion

        #region Save, Load.
        {
            //Node tree = new Node("tree")
            //    .Add(new Node("a")
            //        .Add(new Node("b"))
            //        .Add(new Node("c")
            //            .Add(new Node("d")))
            //        .Add(new Node("e")))
            //    .Add(new Node("f")
            //        .Add(new Node("g"))
            //        .Add(new Node("h")))
            //    .Add(new Node("i"));

            //Console.WriteLine("Перед сохранением в файл");
            //Console.WriteLine(tree.ToString(x => $"({x.AddressAsString}) {x.Name}"));

            //tree
            //    .Save(@"..\..\tree.csv")
            //    .Load(@"..\..\tree.csv");

            //Console.WriteLine("\nПосле загрузки из файла");
            //Console.WriteLine(tree.ToString(x => $"({x.AddressAsString}) {x.Name}"));

            ////Перед сохранением в файл
            ////■ (0) tree
            ////├─■ (0,0) a
            ////│ ├─■ (0,0,0) b
            ////│ ├─■ (0,0,1) c
            ////│ │ └─■ (0,0,1,0) d
            ////│ └─■ (0,0,2) e
            ////├─■ (0,1) f
            ////│ ├─■ (0,1,0) g
            ////│ └─■ (0,1,1) h
            ////└─■ (0,2) i

            ////После загрузки из файла
            ////■ (0) tree
            ////├─■ (0,0) a
            ////│ ├─■ (0,0,0) b
            ////│ ├─■ (0,0,1) c
            ////│ │ └─■ (0,0,1,0) d
            ////│ └─■ (0,0,2) e
            ////├─■ (0,1) f
            ////│ ├─■ (0,1,0) g
            ////│ └─■ (0,1,1) h
            ////└─■ (0,2) i
        }
        #endregion

        #region Search. Поиск по одному слову.
        {
            //Node boxes = new Node("boxes")
            //    .Add(new Node("red box")
            //        .Add(new Node("red pen"))
            //        .Add(new Node("blue pen"))
            //        .Add(new Node("black pen")))
            //    .Add(new Node("blue box")
            //        .Add(new Node("red notebook"))
            //        .Add(new Node("blue notebook"))
            //        .Add(new Node("black notebook")));

            //List<Node> foundNodes = boxes.Search("red");

            //Console.WriteLine(boxes);

            //Console.WriteLine("\nНайденные");

            //foreach (Node foundNode in foundNodes)
            //{
            //    Console.WriteLine(foundNode.Name);
            //}

            ////■ boxes
            ////├─■ red box
            ////│ ├─■ red pen
            ////│ ├─■ blue pen
            ////│ └─■ black pen
            ////└─■ blue box
            ////  ├─■ red notebook
            ////  ├─■ blue notebook
            ////  └─■ black notebook

            ////Найденные
            ////red box
            ////red pen
            ////red notebook
        }
        #endregion

        #region Search. Поиск по нескольким словам.
        {
            //Node boxes = new Node("boxes")
            //    .Add(new Node("red box")
            //        .Add(new Node("red pen"))
            //        .Add(new Node("blue pen"))
            //        .Add(new Node("black pen")))
            //    .Add(new Node("blue box")
            //        .Add(new Node("red notebook"))
            //        .Add(new Node("blue notebook"))
            //        .Add(new Node("black notebook")));

            //List<Node> foundNodes = boxes.Search(new string[] { "bl", "en" });

            //Console.WriteLine(boxes);

            //Console.WriteLine("\nНайденные");

            //foreach (Node foundNode in foundNodes)
            //{
            //    Console.WriteLine(foundNode.Name);
            //}

            ////■ boxes
            ////├─■ red box
            ////│ ├─■ red pen
            ////│ ├─■ blue pen
            ////│ └─■ black pen
            ////└─■ blue box
            ////  ├─■ red notebook
            ////  ├─■ blue notebook
            ////  └─■ black notebook

            ////Найденные
            ////red box
            ////red pen
            ////red notebook
        }
        #endregion


        Console.ReadKey();
    }
}
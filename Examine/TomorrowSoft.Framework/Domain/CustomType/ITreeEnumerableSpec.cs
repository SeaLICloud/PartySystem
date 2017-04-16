using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class TreeNode : ITreeEnumerable<TreeNode>
    {
        public string Name { get; private set; }

        public TreeNode(string name)
        {
            Level = 1;
            Name = name;
            children = new List<TreeNode>();
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            yield return this;
            foreach (var item in Children)
            {
                foreach (var sub in item)
                {
                    yield return sub;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TreeNode> Children
        {
            get { return children; }
        }

        private IList<TreeNode> children;

        public int Level { get; protected set; }

        public void AddChildren(TreeNode treeNode)
        {
            treeNode.Level = this.Level + 1;
            children.Add(treeNode);
        }
    }

    public class ITreeEnumerableSpec
    {
         Establish context =
            () =>
                {
                    var mA = new TreeNode("A");
                    var mAA = new TreeNode("AA");
                    var mAAA = new TreeNode("AAA");
                    var mAB = new TreeNode("AB");
                    var mB = new TreeNode("B");
                    var mBA = new TreeNode("BA");
                    nodes = new List<TreeNode>();
                    nodes.Add(mA);
                    nodes[0].AddChildren(mAA);
                    nodes[0].Children.First().AddChildren(mAAA);
                    nodes[0].AddChildren(mAB);
                    nodes.Add(mB);
                    nodes[1].AddChildren(mBA);
                };
        protected static IList<TreeNode> nodes;
        protected static string result;
    }

    public class 当计算A_AA_AAA_AB_B_BA树形结构的Map时 : ITreeEnumerableSpec
    {
        Because of = () => result = nodes.CalculateMap();

        It 应该等于正确的Map值 = () => result.ShouldEqual("0,1,2,1,0,5");
    }

    public class 当计算A_AA_AAA_AB_ABA_B_BA_BB_BBA树形结构的Map时 : ITreeEnumerableSpec
    {
        Establish context =
            () =>
            {
                var mABA = new TreeNode("ABA");
                var mBB = new TreeNode("BB");
                var mBBA = new TreeNode("BBA");
                nodes[0].Children.Last().AddChildren(mABA);
                nodes[1].AddChildren(mBB);
                nodes[1].Children.Last().AddChildren(mBBA);
            };

        Because of = () => result = nodes.CalculateMap();

        It 应该等于正确的Map值 = () => result.ShouldEqual("0,1,2,1,4,0,6,6,8");
    }

    public class 当计算A_AA_AAA_AB_B_BA树形结构的Level时 : ITreeEnumerableSpec
    {
        Because of =
            () =>
            {
                var sb = new StringBuilder();
                foreach (var root in nodes)
                {
                    foreach (var item in root)
                    {
                        sb.Append(item.Level).Append(",");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                result = sb.ToString();
            };

        It 应该等于正确的Level值 = () => result.ShouldEqual("1,2,3,2,1,2");
    }
}
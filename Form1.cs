using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24points
{
    /// <summary>
    /// 主窗体类
    /// </summary>
    public partial class Form1 : Form
    {
        double EPS = 1e-5;
        int[] numbers = new int[4];
        string curSolution = "";
        string[] operators = { "+", "-", "*", "/" };

        public Form1()
        {
            InitializeComponent();
        }

        private void form1_Load(object sender, EventArgs e)
        {
            button2.PerformClick();     // 启动窗体时自动 refresh
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /**
                Solution text
             */
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            /**
                User input text
             */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
                Find a Solution
             */
            textBox1.Text = curSolution;
        }

        /// <summary>
        /// 数组是否为倒序（轮子）
        /// </summary>
        /// <param name="array"></param>
        private static bool isDesc(int[] array)
        {
            int temp = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 寻找当前数组的下一个排列（轮子）
        /// </summary>
        /// <param name="array"></param>
        private static void FindNextArray(int[] array)
        {
            // 1.找出数组的最大值
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                }
            }
            // 2.从后向前找：找到第一组后数大于前数，以后数位置为signer
            int signer = array.Length - 1;
            for (int i = array.Length - 1; i > 0; i--)
            {
                if (array[i] > array[i - 1])
                {
                    signer = i;
                    break;
                }
            }
            // 3.从signer向后找：找到大于且最接近于array[signer-1]的数array[t]
            int t = signer;
            for (int i = signer; i < array.Length; i++)
            {
                if (array[i] > array[signer - 1] && array[i] < max)
                {
                    t = i;
                    max = array[t];
                }
            }
            // 4.将找到的array[t]和array[signer-1]互换
            int temp = array[t];
            array[t] = array[signer - 1];
            array[signer - 1] = temp;
            // 5.为signer之后的元素升序排序
            for (int i = signer; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 判断当前 numbers[] 中的四个数是否存在解，如果有解则存入 curSolution
        /// </summary>
        bool legal_judge()
        {
            int[] t_numbers = new int[4];
            for (int i = 0; i < 4; i++)
            {
                t_numbers[i] = numbers[i];
            }
            Array.Sort(t_numbers);

            if (t_numbers[0] == t_numbers[3]) // 若生成的四个数相同
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            // 算式 t_numbers[0] <i> t_numbers[1] <j> t_numbers[2] <k> t_numbers = 24
                            string sendStr = "";
                            object result = 0;
                            // 顺序 1 2 3
                            sendStr = "((" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + t_numbers[2] + ")" + operators[k] + t_numbers[3];
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 1 3 2
                            sendStr = "(" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 2 1 3
                            sendStr = "(" + t_numbers[0] + operators[i] + "(" + t_numbers[1] + operators[j] + t_numbers[2] + "))" + operators[k] + t_numbers[3];
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 2 3 1
                            sendStr = t_numbers[0] + operators[i] + "((" + t_numbers[1] + operators[j] + t_numbers[2] + ")" + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 3 1 2
                            sendStr = "(" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 3 2 1
                            sendStr = t_numbers[0] + operators[i] + "(" + t_numbers[1] + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + "))";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            // 遍历t_numbers的全排列
            while (!isDesc(t_numbers))
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    Console.Write(t_numbers[i].ToString());
                //    Console.Write(" ");
                //}
                //Console.Write("\n");
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            // 算式 t_numbers[0] <i> t_numbers[1] <j> t_numbers[2] <k> t_numbers = 24
                            string sendStr = "";
                            object result = 0;
                            // 顺序 1 2 3
                            sendStr = "((" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + t_numbers[2] + ")" + operators[k] + t_numbers[3];
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 1 3 2
                            sendStr = "(" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 2 1 3
                            sendStr = "(" + t_numbers[0] + operators[i] + "(" + t_numbers[1] + operators[j] + t_numbers[2] + "))" + operators[k] + t_numbers[3];
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 2 3 1
                            sendStr = t_numbers[0] + operators[i] + "((" + t_numbers[1] + operators[j] + t_numbers[2] + ")" + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 3 1 2
                            sendStr = "(" + t_numbers[0] + operators[i] + t_numbers[1] + ")" + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + ")";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                            // 顺序 3 2 1
                            sendStr = t_numbers[0] + operators[i] + "(" + t_numbers[1] + operators[j] + "(" + t_numbers[2] + operators[k] + t_numbers[3] + "))";
                            result = Evaluator.Eval(sendStr);
                            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
                            {
                                curSolution = sendStr;
                                return true;
                            }
                        }
                    }
                }
                FindNextArray(t_numbers);
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /**
                Refresh
             */
            textBox1.Text = "";
            textBox2.Text = "";
            curSolution = "";

            Random rd = new Random();
            bool flag = false;  // 标志变量 表示当前是否为合法解
            while (flag == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    numbers[i] = rd.Next(1, 13);
                }
                flag = legal_judge();
            }
            
            // 显示生成的四个数所对应的扑克牌
            string dir = "D:\\My projects\\24points\\24points\\images\\";
            for (int i = 1; i <= 4; i++)
            {
                string curdir = dir + numbers[i-1] + ".jpg";
                PictureBox pb = this.Controls["pictureBox" + i.ToString()] as PictureBox;
                pb.Image = Image.FromFile(curdir);
            }
            // eg. pictureBox1.Image = Image.FromFile("D:\\My projects\\24points\\24points\\images\\1.jpg");
        }

        /// <summary>
        /// 判断一个字符是否为数字
        /// </summary>
        /// <param name="ch"></param>
        bool isDigit(char ch)
        {
            if (ch >= '0' && ch <= '9') return true;
            return false;
        }

        /// <summary>
        /// 检查用户提交的表达式是否恰好用完 numbers[] 所给的数
        /// </summary>
        /// <param name="s"></param>
        bool checkNumbers(string s)
        {
            //Console.WriteLine("in function - " + s);

            int strLen = s.Length;
            if (strLen == 0 || isDigit(s[strLen - 1]))
            {
                s += "$";           // 确保所有数字都被提取出来，用于串尾是数字的情况
            }

            bool numFlag = false;   // 标志变量 前一位是否为数字
            int curNum = 0;         // 记录当前整数值
            bool[] used = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                used[i] = false;
            }

            // 遍历寻找表达式中的整数，并判断是否恰好用完 numbers[] 中的数
            for (int i = 0; i < s.Length; i++)
            {
                //Console.WriteLine("curNum = " + curNum);
                //Console.Write("current char - " + s[i] + " ");
                if (numFlag == false && isDigit(s[i]) == false)
                {
                    //Console.WriteLine("case 1");
                    continue;
                }
                else if (numFlag == false && isDigit(s[i]) == true)
                {
                    //Console.WriteLine("case 2");
                    numFlag = true;
                    curNum = s[i] - '0';
                }
                else if (numFlag == true && isDigit(s[i]) == false)
                {
                    //Console.WriteLine("case 3");
                    bool ok = false;
                    numFlag = false;
                    //Console.WriteLine("get - " + curNum);
                    for (int j = 0; j < 4; j++)
                    {
                        //Console.WriteLine("numbers " + numbers[j]);
                        //Console.WriteLine("used " + used[j]);
                        if (curNum == numbers[j] && used[j] == false)
                        {
                            ok = true;
                            used[j] = true;
                            break;
                        }
                    }
                    if (ok == false)
                    {
                        //Console.WriteLine("hehheda");
                        return false;
                    }
                }
                else if (numFlag == true && isDigit(s[i]) == true)
                {
                    //Console.WriteLine("case 4");
                    curNum *= 10;
                    curNum += s[i] - '0';
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (used[i] == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /**
                Verify
             */
            string inputStr = textBox2.Text;
            if (checkNumbers(inputStr) == false)
            {
                MessageBox.Show("No - Use Ungiven Number!");  // 表达式没有使用 numbers[] 中的数
                return;
            }
            object result = 0;
            try
            {
                result = Evaluator.Eval(inputStr);
            }
            catch(Exception ex)
            {
                MessageBox.Show("No -  Wrong Answer!");  // 捕获并处理计算异常
                return;
            }
            if (Math.Abs(Convert.ToDouble(result) - 24.0) < EPS)
            {
                MessageBox.Show("Yes - Congratulations!");
                button2.PerformClick();
            }
            else
            {
                MessageBox.Show("No - Wrong Answer!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

    }

    /// <summary>
    /// 数学表达式动态求值（轮子）
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// 计算结果,如果表达式出错则抛出异常
        /// </summary>
        /// <param name="statement">表达式</param>
        public static object Eval(string statement)
        {
            if (statement.Trim() != string.Empty)
            {
                Evaluator evaluator = new Evaluator();
                return evaluator.GetFormulaResult(statement);
            }
            else
            {
                return null;
            }
        }

        private object GetFormulaResult(string s)
        {
            if (s == "")
            {
                return null;
            }
            string S = BuildingRPN(s);

            string tmp = "";
            System.Collections.Stack sk = new System.Collections.Stack();

            char c = ' ';
            System.Text.StringBuilder Operand = new System.Text.StringBuilder();
            double x, y;
            for (int i = 0; i < S.Length; i++)
            {
                c = S[i];
                //added c==',' for germany culture
                if (char.IsDigit(c) || c == '.' || c == ',')
                {
                    //数据值收集.
                    Operand.Append(c);
                }
                else if (c == ' ' && Operand.Length > 0)
                {
                    #region 运算数转换
                    try
                    {
                        tmp = Operand.ToString();
                        if (tmp.StartsWith("-"))//负数的转换一定要小心...它不被直接支持.
                        {
                            //现在我的算法里这个分支可能永远不会被执行.
                            sk.Push(-((double)Convert.ToDouble(tmp.Substring(1, tmp.Length - 1))));
                        }
                        else
                        {
                            sk.Push(Convert.ToDouble(tmp));
                        }
                    }
                    catch
                    {
                        return null; //
                    }
                    Operand = new System.Text.StringBuilder();
                    #endregion
                }
                else if (c == '+'//运算符处理.双目运算处理.
                    || c == '-'
                    || c == '*'
                    || c == '/'
                    || c == '%'
                    || c == '^')
                {
                    #region 双目运算
                    if (sk.Count > 0)/*如果输入的表达式根本没有包含运算符.或是根本就是空串.这里的逻辑就有意义了.*/
                    {
                        y = (double)sk.Pop();
                    }
                    else
                    {
                        sk.Push(0);
                        break;
                    }
                    if (sk.Count > 0)
                        x = (double)sk.Pop();
                    else
                    {
                        sk.Push(y);
                        break;
                    }
                    switch (c)
                    {
                        case '+':
                            sk.Push(x + y);
                            break;
                        case '-':
                            sk.Push(x - y);
                            break;
                        case '*':
                            if (y == 0)
                            {
                                sk.Push(x * 1);
                            }
                            else
                            {
                                sk.Push(x * y);
                            }
                            break;
                        case '/':
                            if (y == 0)
                            {
                                sk.Push(x / 1);
                            }
                            else
                            {
                                sk.Push(x / y);
                            }
                            break;
                        case '%':
                            sk.Push(x % y);
                            break;
                        case '^'://
                            if (x > 0)//
                            {
                                //我原本还想,如果被计算的数是负数,又要开真分数次方时如何处理的问题.后来我想还是算了吧.
                                sk.Push(System.Math.Pow(x, y));
                                //
                            }
                            //
                            else//
                            {
                                //
                                double t = y;
                                //
                                string ts = "";
                                //
                                t = 1 / (2 * t);
                                //
                                ts = t.ToString();
                                //
                                if (ts.ToUpper().LastIndexOf('E') > 0)//
                                {
                                    //
                                    ;
                                    //
                                }
                                //
                            }
                            break;
                    }
                    #endregion
                }
                else if (c == '!')//单目取反. )
                {
                    sk.Push(-((double)sk.Pop()));
                }
            }
            if (sk.Count > 1)
            {
                return null;//;
            }
            if (sk.Count == 0)
            {
                return null;//;
            }
            return sk.Pop();
        }

        private string BuildingRPN(string s)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(s);
            System.Collections.Stack sk = new System.Collections.Stack();
            System.Text.StringBuilder re = new System.Text.StringBuilder();

            char c = ' ';
            //sb.Replace( " ","" );
            //一开始,我只去掉了空格.后来我不想不支持函数和常量能滤掉的全OUT掉.
            for (int i = 0;
                i < sb.Length;
                i++)
            {
                c = sb[i];
                //added c==',' for german culture
                if (char.IsDigit(c) || c == ',')//数字当然要了.
                    re.Append(c);
                //if( char.IsWhiteSpace( c )||
                char.IsLetter(c);//如果是空白,那么不要.现在字母也不要.
                //continue;
                switch (c)//如果是其它字符...列出的要,没有列出的不要.
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '%':
                    case '^':
                    case '!':
                    case '(':
                    case ')':
                    case '.':
                        re.Append(c);
                        break;
                    default:
                        continue;
                }
            }
            sb = new System.Text.StringBuilder(re.ToString());
            #region 对负号进行预转义处理.负号变单目运算符求反.
            for (int i = 0; i < sb.Length - 1; i++)
                if (sb[i] == '-' && (i == 0 || sb[i - 1] == '('))
                    sb[i] = '!';
            //字符转义.
            #endregion
            #region 将中缀表达式变为后缀表达式.
            re = new System.Text.StringBuilder();
            for (int i = 0;
                i < sb.Length;
                i++)
            {
                if (char.IsDigit(sb[i]) || sb[i] == '.')//如果是数值.
                {
                    re.Append(sb[i]);
                    //加入后缀式
                }
                else if (sb[i] == '+'
                    || sb[i] == '-'
                    || sb[i] == '*'
                    || sb[i] == '/'
                    || sb[i] == '%'
                    || sb[i] == '^'
                    || sb[i] == '!')//.
                {
                    #region 运算符处理
                    while (sk.Count > 0) //栈不为空时
                    {
                        c = (char)sk.Pop();
                        //将栈中的操作符弹出.
                        if (c == '(') //如果发现左括号.停.
                        {
                            sk.Push(c);
                            //将弹出的左括号压回.因为还有右括号要和它匹配.
                            break;
                            //中断.
                        }
                        else
                        {
                            if (Power(c) < Power(sb[i]))//如果优先级比上次的高,则压栈.
                            {
                                sk.Push(c);
                                break;
                            }
                            else
                            {
                                re.Append(' ');
                                re.Append(c);
                            }
                            //如果不是左括号,那么将操作符加入后缀式中.
                        }
                    }
                    sk.Push(sb[i]);
                    //把新操作符入栈.
                    re.Append(' ');
                    #endregion
                }
                else if (sb[i] == '(')//基本优先级提升
                {
                    sk.Push('(');
                    re.Append(' ');
                }
                else if (sb[i] == ')')//基本优先级下调
                {
                    while (sk.Count > 0) //栈不为空时
                    {
                        c = (char)sk.Pop();
                        //pop Operator
                        if (c != '(')
                        {
                            re.Append(' ');
                            re.Append(c);
                            //加入空格主要是为了防止不相干的数据相临产生解析错误.
                            re.Append(' ');
                        }
                        else
                            break;
                    }
                }
                else
                    re.Append(sb[i]);
            }
            while (sk.Count > 0)//这是最后一个弹栈啦.
            {
                re.Append(' ');
                re.Append(sk.Pop());
            }
            #endregion
            re.Append(' ');
            return FormatSpace(re.ToString());
            //在这里进行一次表达式格式化.这里就是后缀式了.  
        }

        /// <summary>  
        /// 优先级别测试函数.  
        /// </summary>  
        /// <param name="opr"></param>    
        private static int Power(char opr)
        {
            switch (opr)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '%':
                case '^':
                case '!':
                    return 3;
                default:
                    return 0;
            }
        }

        /// <summary>  
        /// 规范化逆波兰表达式.
        /// </summary>  
        /// <param name="s"></param>  
        private static string FormatSpace(string s)
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (!(s.Length > i + 1 && s[i] == ' ' && s[i + 1] == ' '))
                    ret.Append(s[i]);
                else
                    ret.Append(s[i]);
            }
            return ret.ToString();
            //.Replace( '!','-' );
        }
    }
    /// Y29weXJpZ2h0IGJ5IGx5bWluZ2hhbw==
}

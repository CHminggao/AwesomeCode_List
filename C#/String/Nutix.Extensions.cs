using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
 
namespace Nutix.Extensions
{
    public static class StringEx
    {
 
        #region 1.Intercept ：截取字符串
 
        /// <summary>
        /// 获取字符串中指定字符串之后的字符串
        /// </summary>
        /// <param name="str">要截取的原字符串</param>
        /// <param name="afterWhat">截取的依据</param>
        /// <returns>
        /// 返回截取到的字符串。
        /// 如果无任何匹配，则返回 null；
        /// </returns>
        public static string GetAfter(this string str, string afterWhat)
        {
            int index = str.IndexOf(afterWhat);
            if (index == -1) return null;
 
            index += str.Length;
            return str.Substring(index);
        }
 
        /// <summary>
        /// 获取字符串中指定字符串的最后一个匹配之后的字符串
        /// </summary>
        /// <param name="str">要截取的原字符串</param>
        /// <param name="afterWhat">截取的依据</param>
        /// <returns>
        /// 返回截取到的字符串。
        /// 如果无任何匹配，则返回 null；
        /// </returns>
        public static string GetLastAfter(this string str, string afterWhat)
        {
            int index = str.LastIndexOf(afterWhat);
            if (index == -1) return null;
 
            index += str.Length;
            return str.Substring(index);
        }
 
        /// <summary>
        /// 获取字符串中指定字符串之前的字符串
        /// </summary>
        /// <param name="str">要截取的原字符串</param>
        /// <param name="beforeWhat">截取的依据</param>
        /// <returns>
        /// 返回截取到的字符串。
        /// 如果无任何匹配，则返回 null；
        /// </returns>
        public static string GetBefore(this string str, string beforeWhat)
        {
            int index = str.IndexOf(beforeWhat);
            return str.Substring(0, index);
        }
 
        /// <summary>
        /// 获取字符串中指定字符串最后一个匹配之前的字符串
        /// </summary>
        /// <param name="str">要截取的原字符串</param>
        /// <param name="beforeWhat">截取的依据</param>
        /// <returns>
        /// 返回截取到的字符串。
        /// 如果无任何匹配，则返回 null；
        /// </returns>
        public static string GetLastBefore(this string str, string beforeWhat)
        {
            int index = str.LastIndexOf(beforeWhat);
            return str.Substring(0, index);
        }
 
        /// <summary>
        /// 获取字符串中指定的两个字符串之间的字符串内容
        /// </summary>
        /// <param name="str">要截取的原字符串</param>
        /// <param name="from">
        /// 截取时作为依据的起始字符串
        /// 如果 from == ""，从零位置开始截取
        /// </param>
        /// <param name="to">
        /// 截取时作为依据的终止字符串
        /// 如果 to == "", 一直截取到最后一个字符
        /// </param>
        /// <returns>
        /// 返回截取到的字符串
        /// </returns>
        public static string GetBetween(this string str, string from, string to)
        {
            if (from == null || to == null)
            {
                throw new ArgumentException("参数 from 与 to，都不能为 null");
            }
            int iStart, iEnd;
            if (from == string.Empty)
                iStart = 0;
            else
                iStart = str.IndexOf(from) + from.Length;
            if (to == string.Empty)
                iEnd = str.Length;
            else
                iEnd = str.IndexOf(to);
            return str.Substring(iStart, iEnd - iStart);
        }
 
        #endregion
 
        #region 2.Regex ：正则操作
 
        /// <summary>
        /// 判断字符串是否与给定模式相匹配
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="pattern">要匹配的模式</param>
        /// <returns>
        /// 返回是否匹配
        /// </returns>
        public static bool IsMatch(this string str, string pattern)
        {
            if (str == null) return false;
                 
            return System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
        }
 
        /// <summary>
        /// 查找字符串中与指定模式的所有匹配
        /// </summary>
        /// <param name="str">要匹配的字符串</param>
        /// <param name="pattern">进行匹配的正则表达式</param>
        /// <returns>
        /// 返回所有匹配，包括全局匹配和子匹配，匹配到的文本
        /// </returns>
        public static string[] FindAll(this string str, string pattern)
        {
            if (str == null) return null;
          
            Match m = System.Text.RegularExpressions.Regex.Match(str, pattern);
            return m.Groups.OfType<Group>().Select(g => g.Value).ToArray();
        }
 
        #endregion
 
        #region 3.Fill ：填充
 
        #region 3.1.Center ：居中填充
 
        /// <summary>
        /// 使用空格对文本进行居中填充
        /// </summary>
        /// <param name="str">被居中填充的文本</param>
        /// <param name="totalWidth">填充后的总字符数</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string Center(this string str, int totalWidth)
        {
            return Center(str, totalWidth, ' ');
        }
 
        /// <summary>
        /// 使用指定字符对文本进行居中填充
        /// </summary>
        /// <param name="str">被居中填充的文本</param>
        /// <param name="totalWidth">填充后的总字符数</param>
        /// <param name="fillWith">填充时使用的字符</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string Center(this string str, int totalWidth, char fillWith)
        {
            int strlen = str.Length;
            if (strlen >= totalWidth)
            {
                return str;
            }
            else
            {
                int rightLen = (totalWidth - strlen) / 2;
                int leftLen = totalWidth - strlen - rightLen;
                return fillWith.ToString().Repeat(leftLen) +
                    str + fillWith.ToString().Repeat(rightLen);
            }
        }
 
        #endregion
 
        #region 3.2.PadLeftEx ：定宽左填充
 
        /// <summary>
        /// 按系统默认字符编码对文本进行定宽左填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadLeftEx(this string str, int totalByteCount)
        {
            return PadLeftEx(str, totalByteCount, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定字符编码对文本进行定宽左填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadLeftEx(this string str, int totalByteCount, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，就是应该添加的空格数
            int padLen = totalByteCount - width;
            if (padLen <= 0)
                return str;
            else
                return str.PadLeft(padLen);
        }
 
        /// <summary>
        /// 按系统默认字符编码对文本使用指定的填充符进行定宽左填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="fillWith">填充符</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadLeftEx(this string str, int totalByteCount, char fillWith)
        {
            return PadLeftEx(str, totalByteCount, fillWith, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定字符编码对文本使用指定的填充符进行定宽左填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="fillWith">填充符</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadLeftEx(this string str, int totalByteCount,
            char fillWith, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int fillWithWidth = coding.GetByteCount(new char[] { fillWith });
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，再除以填充字符的占的字节数，
            //就是应该添加的空格数【因为有时候是双字节的填充符，比如中文】
            int padLen = (totalByteCount - width) / fillWithWidth;
            if (padLen <= 0)
                return str;
            else
                return str.PadLeft(padLen, fillWith);
        }
 
        #endregion
 
        #region 3.3.CenterEx ：定宽居中填充
 
        /// <summary>
        /// 按系统默认字符编码对文本进行定宽居中填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="totalByteCount"></param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string CenterEx(this string str, int totalByteCount)
        {
            return CenterEx(str, totalByteCount, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定的字符编码对文本进行定宽居中填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要居中填充的字符串</param>
        /// <param name="totalByteCount">填充后的总字节数</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string CenterEx(this string str, int totalByteCount, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，就是应该添加的空格数
            int padLen = totalByteCount - width;
            if (padLen < 0) return str;
            int padRight = padLen / 2;
            int padLeft = padLen - padRight;
            return " ".Repeat(padLeft) + str + " ".Repeat(padRight);
        }
 
        /// <summary>
        /// 按系统默认字符编码对文本使用指定的填充符进行定宽居中填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">填充后得到的结果包含的总字节数</param>
        /// <param name="fillWith">填充符</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string CenterEx(this string str, int totalByteCount, char fillWith)
        {
            return CenterEx(str, totalByteCount, fillWith, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定的字符编码对文本使用指定的填充符进行定宽居中填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">填充后得到的文本需达到的总字节数</param>
        /// <param name="fillWith">填充符</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string CenterEx(this string str, int totalByteCount,
            char fillWith, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int fillWithWidth = coding.GetByteCount(new char[] { fillWith });
            string fillStr = fillWith.ToString();
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，就是应该添加的空格数
            int padLen = (totalByteCount - width) / fillWithWidth;
            if (padLen < 0) return str;
            int padRight = padLen / 2;
            int padLeft = padLen - padRight;
            return fillStr.Repeat(padLeft) + str + fillStr.Repeat(padRight);
        }
 
        #endregion
 
        #region 3.4.PadRight ： 定宽右填充
 
        /// <summary>
        /// 按系统默认字符编码对文本进行定宽右填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadRightEx(this string str, int totalByteCount)
        {
            return PadRightEx(str, totalByteCount, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定字符编码对文本进行定宽右填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadRightEx(this string str, int totalByteCount, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，就是应该添加的空格数
            int padLen = totalByteCount - width;
            if (padLen <= 0)
                return str;
            else
                return str.PadRight(padLen);
        }
 
        /// <summary>
        /// 按系统默认字符编码对文本使用指定的填充符进行定宽右填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="fillWith">填充符</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadRightEx(this string str, int totalByteCount, char fillWith)
        {
            return PadRightEx(str, totalByteCount, fillWith, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 按指定字符编码对文本使用指定的填充符进行定宽右填充（以半角字符宽度为1单位宽度）
        /// </summary>
        /// <param name="str">要填充的文本</param>
        /// <param name="totalByteCount">要填充到的字节长度</param>
        /// <param name="fillWith">填充符</param>
        /// <param name="encodingName">用于在填充过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回填充后的文本
        /// </returns>
        public static string PadRightEx(this string str, int totalByteCount,
            char fillWith, string encodingName)
        {
            Encoding coding = Encoding.GetEncoding(encodingName);
            int fillWithWidth = coding.GetByteCount(new char[] { fillWith });
            int width = coding.GetByteCount(str);
            //总字节数减去原字符串多占的字节数，再除以填充字符的占的字节数，
            //就是应该添加的空格数【因为有时候是双字节的填充符，比如中文】
            int padLen = (totalByteCount - width) / fillWithWidth;
            if (padLen <= 0)
                return str;
            else
                return str.PadRight(padLen, fillWith);
        }
 
        #endregion
 
        #endregion
 
        #region 4.Repeat ：复制字符串
 
        /// <summary>
        /// 取得字符串的指定次重复后的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="times">要重复的次数</param>
        /// <returns>
        /// 返回复制了指定次的字符串
        /// </returns>
        public static string Repeat(this string str, int times)
        {
            if (times < 0)
                throw new ArgumentException("参数 times 不能小于0.");
 
            if (str == null)
                throw new ArgumentException("要复制的字符串不能为 null.");
 
            if (str == string.Empty) return string.Empty;
 
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= times; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }
 
        /// <summary>
        /// 取得字符串的指定次重复后的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="totalByteCount">要重复到的字符宽度</param>
        /// <returns>
        /// 返回复制了指定次的字符串
        /// </returns>
        public static string RepeatEx(this string str, int totalByteCount)
        {
            return StringEx.RepeatEx(str, totalByteCount, Encoding.Default.BodyName);
        }
 
        /// <summary>
        /// 取得字符串的指定次重复后的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="totalByteCount">要重复到的字符宽度</param>
        /// <param name="encodingName">用于在复制过程中进行文本解析的字符编码</param>
        /// <returns>
        /// 返回复制了指定次的字符串
        /// </returns>
        public static string RepeatEx(this string str, int totalByteCount, string encodingName)
        {
            if (totalByteCount < 0)
                throw new ArgumentException("参数 times 不能小于0.");
 
            if (str == null)
                throw new ArgumentException("要复制的字符串不能为 null.");
 
            if (str == string.Empty) return string.Empty;
 
            Encoding coding = Encoding.GetEncoding(encodingName);
            int len = coding.GetByteCount(str);
            int times = totalByteCount / len;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= times; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }
 
        #endregion
 
        #region 5.Parser ：从字符串提取可空对象
 
        public delegate bool TryParse<T>(string s, out T value);
 
        public static readonly Dictionary<Type, object> Parsers =
            new Dictionary<Type, object>();
 
        /// <summary>
        /// 通过反射获取指定类型的 bool TryParse(string s, out T value) 静态方法
        /// </summary>
        /// <typeparam name="T">要获取方法的类型</typeparam>
        /// <returns>
        /// 如果找到这个方法，就返回它；找不到，就报错
        /// </returns>
        private static MethodInfo GetTryParseMethod<T>()
        {
            Type t = typeof(T);
 
            MethodInfo miTryParse = t.GetMethod("TryParse", BindingFlags.Public |
                BindingFlags.Static, null, CallingConventions.Any,
                new Type[] { typeof(string), typeof(T).MakeByRefType() }, null);
            if (miTryParse == null)
                throw new Exception("类型[" + t.FullName +
                    "]没有 TryParse(string s, out TValue value) 静态方法，无法完成解析");
 
            return miTryParse;
        }
 
        /// <summary>
        /// 使用指定的解析方法解析指定的字符串到指定的类型
        /// </summary>
        /// <typeparam name="T">要解析成的目标类型</typeparam>
        /// <param name="proc">给定的解析方法</param>
        /// <param name="valueStr">要解析的字符器</param>
        /// <returns>
        /// 返回的结果表明了解析成功与否，以及解析得到的值（如果失败返回的是目标类型的默认值）
        /// </returns>
        private static (bool Succeed, T Value) InvokeTryParse<T>(
            object proc, string valueStr, bool temp)
        {
            Type t = typeof(T);
            if (!temp &&
                (!StringEx.Parsers.ContainsKey(t) ||
                StringEx.Parsers[t] != proc))
                StringEx.Parsers.Add(t, proc);
 
            (bool Succeed, T Value) r;
            if (proc is MethodInfo miTryParse)
            {
                object[] args = new object[] { valueStr, default(T) };
 
                r.Succeed = (bool)miTryParse.Invoke(null, args);
                r.Value = (T)args[1];
            }
            else
            {
                TryParse<T> parser = proc as TryParse<T>;
                r.Succeed = parser(valueStr, out T value);
                r.Value = value;
            }
 
            return r;
        }
 
        /// <summary>
        /// 将字符串，使用指定的解析器方法（如果有提供），解析成指定的可空值类型对象
        /// </summary>
        /// <typeparam name="TValue">要解析成的目标类型的可空类型所包装的类型</typeparam>
        /// <param name="valueStr">要解析的字符串</param>
        /// <param name="parser">
        /// 字符串解析器方法
        /// <para>当某类型的解析器被传入调用一次，该解析器会被缓存；</para>
        /// <para>解析器是与解析的目标类型绑定的，下次再调用，不需要再传此参数；</para>
        /// <para>如果再次传入，会视情况（是否相等）覆盖上次调用时所用的解析器</para>
        /// <para>当您未指定解析器方法时，将尝试取得目标类型的固有解析器，如果没找到，将会报错</para>
        /// <para>手传解析器，比目标类型原生解析器具有更高的优先级</para>
        /// </param>
        /// <param name="temp">解析器是否是临时解析器，如果是，则不会被保存</param>
        /// <returns>
        /// 解析成功，返回解析出来的值类型对象；失败，则返回 null
        /// </returns>
        /// <remarks>
        /// <para>如果提供了解析器，本方法通过给定的解析器，解析给定的字符器到目标类型</para>
        /// <para>否则，本方法尝试通过目标类型的 bool TryParse(string s, out TRefer value) 静态方法来实现功能</para>
        /// <para>如果指定的目标类型，不包含此种签名的静态方法，则会报错</para>
        /// </remarks>
        public static TValue? VParse<TValue>(
            this string valueStr, TryParse<TValue> parser = null, bool temp = false)
            where TValue : struct
        {
            Type t = typeof(TValue);
 
            object proc = null;
            if (parser == null)
            {
                if (StringEx.Parsers.ContainsKey(t))
                    proc = StringEx.Parsers[t];
                else
                {
                    if (t.IsEnum)
                        proc = new TryParse<TValue>(
                            (string s, out TValue value) =>
                            Enum.TryParse(s, out value));
                    else
                        proc = StringEx.GetTryParseMethod<TValue>();
                }
            }
            else
                proc = parser;
 
            var (Succeed, Value) =
                StringEx.InvokeTryParse<TValue>(proc, valueStr, temp);
            if (Succeed)
                return Value;
            else
                return null;
        }
 
        /// <summary>
        /// 将字符串，使用指定的解析器方法（如果有提供），解析成指定的引用类型的目标类型对象
        /// </summary>
        /// <typeparam name="TRefer">要解析成的目标类型</typeparam>
        /// <param name="valueStr">要解析的字符串</param>
        /// <param name="parser">
        /// 字符串解析器方法
        /// <para>当某类型的解析器被传入调用一次，该解析器会被缓存；</para>
        /// <para>解析器是与解析的目标类型绑定的，下次再调用，不需要再传此参数；</para>
        /// <para>如果再次传入，会视情况（是否相等）覆盖上次调用时所用的解析器</para>
        /// <para>当您未指定解析器方法时，将尝试取得目标类型的固有解析器，如果没找到，将会报错</para>
        /// <para>手传解析器，比目标类型原生解析器具有更高的优先级</para>
        /// </param>
        /// <param name="temp">解析器是否是临时解析器，如果是，则不会被保存</param>
        /// <returns>
        /// 解析成功，则返回解析到的对象；失败，则返回 null
        /// </returns>
        /// <remarks>
        /// <para>如果提供了解析器，本方法通过给定的解析器，解析给定的字符器到目标类型</para>
        /// <para>否则，本方法尝试通过目标类型的 bool TryParse(string s, out TRefer value) 静态方法来实现功能</para>
        /// <para>如果指定的目标类型，不包含此种签名的静态方法，则会报错</para>
        /// </remarks>
        public static TRefer RParse<TRefer>(
            this string valueStr, TryParse<TRefer> parser = null, bool temp = false)
            where TRefer : class
        {
            Type t = typeof(TRefer);
 
            object proc;
            if (parser == null)
            {
                if (StringEx.Parsers.ContainsKey(t))
                    proc = StringEx.Parsers[t];
                else
                    proc = StringEx.GetTryParseMethod<TRefer>();
            }
            else
            {
                proc = parser;
            }
 
            return StringEx.InvokeTryParse<TRefer>(proc, valueStr, temp).Value;
        }
 
        /// <summary>
        /// 将表式文本，使用指定的解析器方法（如果有提供），解析成指定的可空的目标类型对象数组
        /// </summary>
        /// <typeparam name="TValue">要解析成的目标类型</typeparam>
        /// <param name="tableText">要解析的表式文本</param>
        /// <param name="noWhiteLines">是否抛弃空白行</param>
        /// <param name="parser">
        /// 表式文本解析器方法
        /// <para>当某类型的解析器被传入调用一次，该解析器会被缓存，除非指定它是临时的；</para>
        /// <para>解析器是与解析的目标类型绑定的，下次再调用，不需要再传此参数；</para>
        /// <para>如果再次传入，会视情况（是否相等）覆盖上次调用时所用的解析器</para>
        /// <para>当您未指定解析器方法时，将尝试取得目标类型的固有解析器，如果没找到，将会报错</para>
        /// <para>手传解析器，比目标类型原生解析器具有更高的优先级</para>
        /// </param>
        /// <param name="temp">解析器是否是临时解析器，如果是，则不会被保存</param>
        /// <returns>
        /// 解析成功，则返回解析到的对象；失败，则返回 null
        /// </returns>
        /// <remarks>
        /// <para>如果提供了解析器，本方法通过给定的解析器，解析给定的表式文本到目标类型</para>
        /// <para>否则，本方法尝试通过目标类型的 bool TryParse(string s, out TRefer value) 静态方法来实现功能</para>
        /// <para>如果指定的目标类型，不包含此种签名的静态方法，则会报错</para>
        /// </remarks>
        public static TValue?[] VParseTable<TValue>(
            this string tableText, bool noWhiteLines = true,
            TryParse<TValue> parser = null, bool temp = false)
            where TValue : struct
        {
            Type t = typeof(TValue);
 
            object proc = null;
            if (parser == null)
            {
                if (StringEx.Parsers.ContainsKey(t))
                    proc = StringEx.Parsers[t];
                else
                {
                    if (t.IsEnum)
                        proc = new TryParse<TValue>(
                            (string s, out TValue value) =>
                            Enum.TryParse(s, out value));
                    else
                        proc = StringEx.GetTryParseMethod<TValue>();
                }
            }
            else
                proc = parser;
 
            List<TValue?> values = new List<TValue?>();
            foreach(string line in tableText.GetLines(noWhiteLines))
            {
                var (Succeed, Value) =
                    StringEx.InvokeTryParse<TValue>(proc, line, temp);
                if (Succeed)
                    values.Add(Value);
                else
                    values.Add(null);
            }
 
            return values.ToArray();
        }
 
        /// <summary>
        /// 将表式文本，使用指定的解析器方法（如果有提供），解析成指定的引用类型的目标类型对象数组
        /// </summary>
        /// <typeparam name="TRefer">要解析成的目标类型</typeparam>
        /// <param name="tableText">要解析的表式文本</param>
        /// <param name="noWhiteLines">是否抛弃空白行</param>
        /// <param name="parser">
        /// 表式文本解析器方法
        /// <para>当某类型的解析器被传入调用一次，该解析器会被缓存，除非指定它是临时的；</para>
        /// <para>解析器是与解析的目标类型绑定的，下次再调用，不需要再传此参数；</para>
        /// <para>如果再次传入，会视情况（是否相等）覆盖上次调用时所用的解析器</para>
        /// <para>当您未指定解析器方法时，将尝试取得目标类型的固有解析器，如果没找到，将会报错</para>
        /// <para>手传解析器，比目标类型原生解析器具有更高的优先级</para>
        /// </param>
        /// <param name="temp">解析器是否是临时解析器，如果是，则不会被保存</param>
        /// <returns>
        /// 解析成功，则返回解析到的对象；失败，则返回 null
        /// </returns>
        /// <remarks>
        /// <para>如果提供了解析器，本方法通过给定的解析器，解析给定的表式文本到目标类型</para>
        /// <para>否则，本方法尝试通过目标类型的 bool TryParse(string s, out TRefer value) 静态方法来实现功能</para>
        /// <para>如果指定的目标类型，不包含此种签名的静态方法，则会报错</para>
        /// </remarks>
        public static TRefer[] RParseTable<TRefer>(
            this string tableText, bool noWhiteLines = true,
            TryParse<TRefer> parser = null, bool temp = false)
            where TRefer : class
        {
            Type t = typeof(TRefer);
 
            object proc;
            if (parser == null)
            {
                if (StringEx.Parsers.ContainsKey(t))
                    proc = StringEx.Parsers[t];
                else
                    proc = StringEx.GetTryParseMethod<TRefer>();
            }
            else
            {
                proc = parser;
            }
 
            return tableText.GetLines(noWhiteLines)
                .Select(line =>
                    StringEx.InvokeTryParse<TRefer>(proc, line, temp).Value
                ).ToArray();
        }
 
 
        /// <summary>
        /// 从字符串中尝试提取 bool 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 bool 值；否则，返回 null
        /// </returns>
        public static bool? ParseBoolean(this string valueStr)
        {
            if (bool.TryParse(valueStr, out bool value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 byte 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 byte 值；否则，返回 null
        /// </returns>
        public static byte? ParseByte(this string valueStr)
        {
            if (byte.TryParse(valueStr, out byte value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 sbyte 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 sbyte 值；否则，返回 null
        /// </returns>
        public static sbyte? ParseSByte(this string valueStr)
        {
            if (sbyte.TryParse(valueStr, out sbyte value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 short 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 short 值；否则，返回 null
        /// </returns>
        public static short? ParseInt16(this string valueStr)
        {
            if (short.TryParse(valueStr, out short value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 int 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 int 值；否则，返回 null
        /// </returns>
        public static int? ParseInt32(this string valueStr)
        {
            if (int.TryParse(valueStr, out int value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 long 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 long 值；否则，返回 null
        /// </returns>
        public static long? ParseInt64(this string valueStr)
        {
            if (long.TryParse(valueStr, out long value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 ushort 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 ushort 值；否则，返回 null
        /// </returns>
        public static ushort? ParseUInt16(this string valueStr)
        {
            if (ushort.TryParse(valueStr, out ushort value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 uint 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 uint 值；否则，返回 null
        /// </returns>
        public static uint? ParseUInt32(this string valueStr)
        {
            if (uint.TryParse(valueStr, out uint value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 ulong 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 ulong 值；否则，返回 null
        /// </returns>
        public static ulong? ParseUInt64(this string valueStr)
        {
            if (ulong.TryParse(valueStr, out ulong value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 float 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 float 值；否则，返回 null
        /// </returns>
        public static float? ParseSingle(this string valueStr)
        {
            if (float.TryParse(valueStr, out float value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 double 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 double 值；否则，返回 null
        /// </returns>
        public static double? ParseDouble(this string valueStr)
        {
            if (double.TryParse(valueStr, out double value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取 decimal 值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的 decimal 值；否则，返回 null
        /// </returns>
        public static decimal? ParseDecimal(this string valueStr)
        {
            if (decimal.TryParse(valueStr, out decimal value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取日时（DateTime）值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的日时（DateTime）值；否则，返回 null
        /// </returns>
        public static DateTime? ParseDateTime(this string valueStr)
        {
            if (DateTime.TryParse(valueStr, out DateTime value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取时段（TimeSpan）值
        /// </summary>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的时段（TimeSpan）值；否则，返回 null
        /// </returns>
        public static TimeSpan? ParseTimeSpan(this string valueStr)
        {
            if (TimeSpan.TryParse(valueStr, out TimeSpan value))
                return value;
            else
                return null;
        }
 
        /// <summary>
        /// 从字符串中尝试提取枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="valueStr">要提取的字符串</param>
        /// <returns>
        /// 提取成功，则返回提取到的枚举值；否则，返回 null
        /// </returns>
        public static TEnum? ParseEnum<TEnum>(this string valueStr)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(valueStr, out TEnum value))
                return value;
            else
                return null;
        }
 
        #endregion
 
        #region Z.Unclassified ：未分类
 
        /// <summary>
        /// 获取指定字符器的字节宽度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetWidth(this string str)
        {
            Encoding coding = Encoding.Default;
            return coding.GetByteCount(str);
        }
 
        /// <summary>
        /// 使用指定格式化器格式化值表
        /// </summary>
        /// <param name="format">格式化器</param>
        /// <param name="values">值表</param>
        /// <returns>
        /// 返回格式化后的字符串
        /// </returns>
        public static string FormatWith(this string format, params object[] values)
        {
            return string.Format(format, values);
        }
 
        /// <summary>
        /// 从指定字符器获取行
        /// </summary>
        /// <param name="text">指定的字符器</param>
        /// <param name="noWhiteLines">是否抛弃空白行</param>
        /// <returns></returns>
        public static string[] GetLines(this string text, bool noWhiteLines = false)
        {
            if(noWhiteLines)
                return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                    .Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            else
                return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }
 
        #endregion
 
    }
}
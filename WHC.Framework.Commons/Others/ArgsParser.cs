using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 分析应用程序的args变量，即分析参数。
    /// 作为CommandLine辅助类相同功能的补充。
    /// </summary>
    public class ArgsParser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ArgsParser() : this("/")
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OptionStarter">确定其中一个参数可选项开始的文本</param>
        public ArgsParser(string OptionStarter)
        {
            if (string.IsNullOrEmpty(OptionStarter))
                throw new ArgumentNullException("OptionStarter");

            this.OptionStarter = OptionStarter;
            OptionRegex = new Regex(string.Format(@"(?<Command>{0}[^\s]+)[\s|\S|$](?<Parameter>""[^""]*""|[^""{0}]*)", OptionStarter));
        }

        /// <summary>
        /// 解析程序参数为单独的参数可选项列表
        /// </summary>
        /// <param name="Args">待转换的参数</param>
        /// <returns>选项列表</returns>
        public virtual List<Option> Parse(string[] Args)
        {
            if (Args == null)
                return new List<Option>();
            List<Option> Result = new List<Option>();
            string Text = "";
            string Splitter = "";
            foreach (string Arg in Args)
            {
                Text += Splitter + Arg;
                Splitter = " ";
            }

            MatchCollection Matches = OptionRegex.Matches(Text);
            string Option = "";
            foreach (Match OptionMatch in Matches)
            {
                if (OptionMatch.Value.StartsWith(OptionStarter) && !string.IsNullOrEmpty(Option))
                {
                    Result.Add(new Option(Option, OptionStarter));
                    Option = "";
                }
                Option += OptionMatch.Value + " ";
            }
            Result.Add(new Option(Option, OptionStarter));
            return Result;
        }

        /// <summary>
        /// 启动参数可选项的正则表达式
        /// </summary>
        protected virtual Regex OptionRegex { get; set; }

        /// <summary>
        /// 启动参数可选项的字符串
        /// </summary>
        protected virtual string OptionStarter { get; set; }

    }

    /// <summary>
    /// 包含单个的程序启动参数可选项
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Text">参数可选项文本</param>
        /// <param name="OptionStarter">参数可选项的开始字符 ("/", "-", etc.)</param>
        public Option(string Text, string OptionStarter)
        {
            if (string.IsNullOrEmpty(Text))
                throw new ArgumentNullException("Text");
            if (string.IsNullOrEmpty(OptionStarter))
                throw new ArgumentNullException("OptionStarter");
            Regex CommandParser = new Regex(string.Format(@"{0}(?<Command>[^\s]*)\s(?<Parameters>.*)", OptionStarter));
            Regex ParameterParser = new Regex("(?<Parameter>\"[^\"]*\")[\\s]?|(?<Parameter>[^\\s]*)[\\s]?");
            Parameters = new List<string>();
            Match CommandMatch = CommandParser.Match(Text);
            Command = CommandMatch.Groups["Command"].Value;
            Text = CommandMatch.Groups["Parameters"].Value;

            foreach (Match match in ParameterParser.Matches(Text))
            {
                if (!string.IsNullOrEmpty(match.Value))
                {
                    Parameters.Add(match.Groups["Parameter"].Value);
                }
            }
        }

        /// <summary>
        /// ToString描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("Command: ")
                   .Append(Command)
                   .Append("\n")
                   .Append("Parameters: ");
            Parameters.ForEach(x => Builder.Append(x).Append(" "));
            return Builder.Append("\n")
                          .ToString();
        }

        /// <summary>
        /// 命令字符串
        /// </summary>
        public virtual string Command { get; set; }

        /// <summary>
        /// 找到的参数列表
        /// </summary>
        public virtual List<string> Parameters { get; set; }

    }
}
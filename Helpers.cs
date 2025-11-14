using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SluggHelpers
{
    public static class TechnicalHelpers
    {
        public static IEnumerable<Type> GetTypesFromSpecificInterface<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface);
        }
    }
    public static class Consolas
    {
        public const string RESET = "\u001b[0m";
        public const string UNDERLINE = "\u001b[4m";
        public const string BOLD = "\u001b[1m";
        public const string ITALIC = "\u001b[3m";
        public const string BLINK = "\u001b[5m";

        /// <summary>
        /// Writes a line, and then adds a new line. <br/>
        /// You can add colors to it, but its optional.
        /// <br/><br/>
        /// Example: <br/>
        /// <c>ColorWriteLine("{ConsolasColor.yellow }My yellow text!");</c>
        /// </summary>
        public static void ColorWriteLine(Placeholder builder)
        {
            var lastChar = builder.builder.Length - 1;
            Console.WriteLine(builder.GetFormattedText() + Consolas.AnsiToForeground(ConsolasColor.white));
        }
        /// <summary>
        /// Writes a line, but doenst then makes a new line. <br/>
        /// You can add colors to it, but its optional.
        /// <br/><br/>
        /// Example: <br/>
        /// <c>ColorWrite("{ConsolasColor.yellow }My yellow text!");</c>
        /// </summary>
        public static void ColorWrite(Placeholder builder)
        {
            var lastChar = builder.builder.Length - 1;
            Console.Write(builder.GetFormattedText() + Consolas.AnsiToForeground(ConsolasColor.white));
        }
        /// <summary>
        /// Just a little fork of Logutils.Console.AnsiColor thing <br/>
        /// THIS IS WONDERFULL
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string AnsiToForeground(ConsolasColor color)
        {
            int num = RoundToInt(color.r * 255f);
            int num2 = RoundToInt(color.g * 255f);
            int num3 = RoundToInt(color.b * 255f);
            return string.Format("\u001b[38;2;{0};{1};{2}m", num, num2, num3);
        }

        private static int RoundToInt(float f)
        {
            return (int)Math.Round((double)f);
        }
    }
    public static class StringHelper
    {
        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        public static string IDK(string value, char separator)
        {
            // if it ends with "separator", then returns the value without "separator"
            if (value.EndsWith(separator)) return value.Remove(value.Length - 1);

            // return itself
            return value;
        }
        public static string StringUntil(string str, string stopAt)
        {
            if (!string.IsNullOrEmpty(str) )
            {
                var local = str.IndexOf(stopAt);

                if (local > 0)
                    return str.Substring(0, local);
            }

            return "";
        }
        public static string StringUntil(string str, char stopAt)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var local = str.IndexOf(stopAt);

                if (local > 0)
                    return str.Substring(0, local);
            }

            return "";
        }
    }


    [InterpolatedStringHandler]
    public readonly struct Placeholder
    {
        public StringBuilder builder { get; }

        public Placeholder(int literalLength, int formattedCount, char placeholder = '#')
        {
            builder = new StringBuilder();
        }

        public void AppendLiteral(string s)
        {
            builder.Append(s);
        }
        public string GetFormattedText()
        {
            return builder.ToString();
        }
        public void AppendFormatted(ConsolasColor color)
        {
            var formattedColor = Consolas.AnsiToForeground(color);
            builder.Append(formattedColor);
        }
        public void AppendFormatted(object data)
        {
            builder.Append(data);
        }

    }
    public struct ConsolasColor : IEquatable<ConsolasColor>
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public static ConsolasColor red { get { return new ConsolasColor(1f, 0f, 0f, 1f); } }
        public static ConsolasColor green { get { return new ConsolasColor(0f, 1f, 0f, 1f); } }
        public static ConsolasColor blue { get { return new ConsolasColor(0f, 0f, 1f, 1f); } }
        public static ConsolasColor white { get { return new ConsolasColor(1f, 1f, 1f, 1f); } }
        public static ConsolasColor black { get { return new ConsolasColor(0f, 0f, 0f, 1f); } }
        public static ConsolasColor yellow { get { return new ConsolasColor(1f, 0.92156863f, 0.015686275f, 1f); } }
        public static ConsolasColor cyan { get { return new ConsolasColor(0f, 1f, 1f, 1f); } }
        public static ConsolasColor magenta { get { return new ConsolasColor(1f, 0f, 1f, 1f); } }
        public static ConsolasColor gray { get { return new ConsolasColor(0.5f, 0.5f, 0.5f, 1f); } }
        public static ConsolasColor grey { get { return new ConsolasColor(0.5f, 0.5f, 0.5f, 1f); } }
        public static ConsolasColor clear { get { return new ConsolasColor(0f, 0f, 0f, 0f); } }
        public float grayscale { get { return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b; } }

        public static implicit operator Vector4(ConsolasColor c) { return new Vector4(c.r, c.g, c.b, c.a); }
        public float this[int index]
        {
            get
            {
                float result;
                switch (index)
                {
                    case 0:
                        result = this.r;
                        break;
                    case 1:
                        result = this.g;
                        break;
                    case 2:
                        result = this.b;
                        break;
                    case 3:
                        result = this.a;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
                }
                return result;
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.r = value;
                        break;
                    case 1:
                        this.g = value;
                        break;
                    case 2:
                        this.b = value;
                        break;
                    case 3:
                        this.a = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
                }
            }
        }

        public ConsolasColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public ConsolasColor(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1f;
        }

        public static ConsolasColor RGB(float r, float g, float b)
        {
            return new ConsolasColor(r / 255, g / 255, b / 255);
        }
        public override string ToString()
        {
            return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
        public override bool Equals(object other)
        {
            bool flag = !(other is ConsolasColor);
            return !flag && this.Equals((ConsolasColor)other);
        }
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            bool flag = string.IsNullOrEmpty(format);
            if (flag)
            {
                format = "F3";
            }
            return Format("RGBA({0}, {1}, {2}, {3})", new object[]
            {
                this.r.ToString(format, formatProvider),
                this.g.ToString(format, formatProvider),
                this.b.ToString(format, formatProvider),
                this.a.ToString(format, formatProvider)
            });
        }
        public bool Equals(ConsolasColor other)
        {
            return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
        }


        private string Format(string fmt, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);
        }

        public static ConsolasColor operator +(ConsolasColor a, ConsolasColor b)
        {
            return new ConsolasColor(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        }
        public static ConsolasColor operator -(ConsolasColor a, ConsolasColor b)
        {
            return new ConsolasColor(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        }
        public static ConsolasColor operator *(ConsolasColor a, ConsolasColor b)
        {
            return new ConsolasColor(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        }
        public static ConsolasColor operator *(ConsolasColor a, float b)
        {
            return new ConsolasColor(a.r * b, a.g * b, a.b * b, a.a * b);
        }
        public static ConsolasColor operator *(float b, ConsolasColor a)
        {
            return new ConsolasColor(a.r * b, a.g * b, a.b * b, a.a * b);
        }
        public static ConsolasColor operator /(ConsolasColor a, float b)
        {
            return new ConsolasColor(a.r / b, a.g / b, a.b / b, a.a / b);
        }
        public static bool operator ==(ConsolasColor lhs, ConsolasColor rhs)
        {
            return lhs == rhs;
        }
        public static bool operator !=(ConsolasColor lhs, ConsolasColor rhs)
        {
            return !(lhs == rhs);
        }
    }
}
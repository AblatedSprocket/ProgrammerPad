using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProgrammerPad.Utilities
{
    static class TextConverter
    {
        public static string ConvertVBtoCS(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string adj;
                StringBuilder sb = new StringBuilder();
                string[] textArr = text.Split('\n');
                foreach (string line in textArr)
                {

                    if (line.Trim().StartsWith("Dim"))
                    {
                        adj = "Dim";
                    }
                    else if (line.Trim().StartsWith("Public"))
                    {
                        if (line.Trim().StartsWith("Public Property"))
                        {
                            adj = "Public Property";
                        }
                        else
                        {
                            adj = "Public";
                        }
                    }
                    else if (line.Trim().StartsWith("Private"))
                    {
                        adj = "Private";
                    }
                    else if (line.Trim().StartsWith("Property"))
                    {
                        adj = "Property";
                    }
                    else if (line.Trim().EndsWith("_") || line.Trim().StartsWith("&") || line.Trim().EndsWith("&") || (line.Trim().StartsWith("\"") && line.Trim().EndsWith("\"")))
                    {
                        adj = "String";
                    }
                    else if (line.Trim().StartsWith("'"))
                    {
                        adj = "Comment";
                    }
                    else
                    {
                        adj = string.Empty;
                    }
                    if (adj.Equals("String"))
                    {
                        string formatted;
                        formatted = line.Trim();
                        formatted = formatted.Replace("<>", "!=");
                        if (formatted.StartsWith("&"))
                        {
                            formatted = formatted.Substring(2);
                        }
                        if (formatted.EndsWith("_") || formatted.EndsWith("&"))
                        {
                            sb.Append(formatted.Substring(0, formatted.Length - 1));
                            sb.Append("+\n");
                        }
                        else
                        {
                            sb.Append(formatted);
                            sb.Append(";\n");
                        }
                    }
                    else if (adj.Equals("Comment"))
                    {
                        sb.Append($"//{line.Trim().Substring(1)}\n");
                    }
                    else if (!adj.Equals(string.Empty))
                    {
                        string formatted;
                        string val;
                        if (line.Contains('='))
                        {
                            val = line.Substring(line.IndexOf('='));
                            val = val.Replace("String.", "string.");
                            formatted = line.Substring(0, line.IndexOf('='));
                        }
                        else
                        {
                            formatted = line;
                            val = string.Empty;
                        }
                        string type = Regex.Match(line, "As (?<type>[A-Za-z_]*)( )*").Result("${type}");
                        switch (type.Trim())
                        {
                            case "String":
                                type = type.ToLower();
                                break;
                            case "Boolean":
                                type = "bool";
                                break;
                            case "Integer":
                                type = "int";
                                break;
                            case "Date":
                                type = "DateTime";
                                break;
                        }
                        if (adj == "Property")
                        {
                            formatted = Regex.Replace(formatted, adj, $"public {type}");
                            formatted = Regex.Replace(formatted, "As [A-Za-z_]*", "{ get; set; }");
                        }
                        else if (adj == "Public Property")
                        {
                            formatted = Regex.Replace(formatted, adj, $"public {type}");
                            formatted = Regex.Replace(formatted, "As [A-Za-z_]*", "{ get; set; }");
                        }
                        else
                        {
                            formatted = Regex.Replace(formatted, adj, type);
                            formatted = Regex.Replace(formatted, " As [A-Za-z_]*", string.Empty);
                        }
                        formatted = Regex.Replace(formatted, " As [A-Za-z_]*", string.Empty);
                        sb.Append(string.Concat(formatted, val).Trim());
                        if (!sb.ToString().Trim().EndsWith("}"))
                        {
                            sb.Append(";");
                        }
                        sb.Append("\n");
                    }
                    else
                    {
                        sb.Append(line);
                        sb.Append('\n');
                    }

                }
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string ConvertToSqlParams(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.StartsWith("(") && text.EndsWith(")"))
            {
                if (!text.Trim().StartsWith("(@"))
                {
                    string[] textArr = text.Split(',');
                    StringBuilder sqlBuilder = new StringBuilder();
                    foreach (string param in textArr)
                    {
                        if (param.Trim().StartsWith("("))
                        {
                            sqlBuilder.Append($"(@{param.Substring(1).Trim()}, ");
                        }
                        else if (param.Trim().EndsWith(")"))
                        {
                            sqlBuilder.Append($"@{param.Trim()}");
                        }
                        else
                        {
                            sqlBuilder.Append($"@{param.Trim()}, ");
                        }
                    }
                    return sqlBuilder.ToString();
                }
                else
                {
                    return Regex.Replace(text, "@", "");
                }
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string ConvertToSqlCommand(string text)
        {
            StringBuilder cmdBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Trim().StartsWith("(@") && text.Trim().EndsWith(")"))
                {
                    string cmdString = Regex.Replace(text.Trim(), @"\(", "");
                    cmdString = Regex.Replace(cmdString, @"\)", "");
                    string[] cmdArray = cmdString.Split(',');
                    foreach (string param in cmdArray)
                    {
                        cmdBuilder.Append($"cmd.Parameters.Add(\"{param.Trim()}\", SqlDbType.VarChar).Value = {param.Trim().Substring(1)};\n");
                    }
                    return cmdBuilder.ToString();
                }
                else if (text.StartsWith("cmd.Parameters.Add"))
                {
                    string unCmdText = Regex.Replace(text, "cmd.Parameters.Add\\(\"", "");
                    unCmdText = Regex.Replace(unCmdText, "\", SqlDbType.VarChar\\).Value = ;\n", ", ");
                    StringBuilder unCmdBuilder = new StringBuilder();
                    unCmdBuilder.Append("(");
                    unCmdBuilder.Append(unCmdText.Trim());
                    unCmdBuilder.Remove(unCmdBuilder.Length - 1, 1);
                    unCmdBuilder.Append(")");
                    return unCmdBuilder.ToString();
                }
                else
                {
                    return text;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

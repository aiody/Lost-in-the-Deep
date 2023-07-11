namespace PacketGenerator
{
    internal class Program
    {
        static string managerRegister;
        static void Main(string[] args)
        {
            string file = "../../../../../Common/protoc-23.1-win64/bin/Protocol.proto";

            if (args.Length >= 1)
                file = args[0];

            bool startParsing = false;
            foreach (string line in File.ReadAllLines(file))
            {
                if (!startParsing && line.Contains("enum MsgId"))
                {
                    startParsing = true;
                    continue;
                }

                if (!startParsing)
                    continue;

                if (line.Contains("}"))
                    break;

                string[] names = line.Trim().Split(" =");
                if (names.Length == 0)
                    continue;

                string name = names[0];
                if (name.StartsWith("S_"))
                {
                    string[] words = name.Split("_");

                    string msgName = "";
                    foreach (string word in words)
                        msgName += FirstCharToUpper(word);

                    string packetName = $"S_{msgName.Substring(1)}";
                    managerRegister += string.Format(PacketFormat.managerRegisterFormat, msgName, packetName);
                }
                else if (name.StartsWith("C_"))
                {
                }
            }

            string managerText = string.Format(PacketFormat.managerFormat, managerRegister);
            File.WriteAllText("PacketManager.cs", managerText);
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    class Homophonic_Cipher : Cipher
    {
        public Homophonic_Cipher(string message, string key) : base(message, key, "Homophonic Cipher")
        {

        }
        public override string Decrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Homophonic_Substitution(Message, Key, false);
		}

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Homophonic_Substitution(Message, Key, true);
		}

		/*
		 * Key must be in a certain format, ##,##,...,##;...;##,##,...;
		 * So each letter is linked to a set of 2-digit numbers so there
		 * must be 26 ";"s for each set. Every 2-digit number must be unique.
		*/
		public override bool Is_Key_Valid()
        {
			if (Key.Length % 3 != 0)
			{
				return false;
			}
			var hashKeySet = new HashSet<int>();
			int numDelims = 0;
			for (int i = 0; i < Key.Length; i+=3)
			{
				string num = Key.Substring(i, 2);
				int x = 0;
				char delim = Key[i + 2];
				if (!Int32.TryParse(num, out x) || !hashKeySet.Add(x) || delim != ',' && delim != ';')
				{
					return false;
				}
				if (delim == ';')
				{
					numDelims++;
				}
			}
			if (numDelims != 26)
			{
				return false;
			}
			return true;
        }

		// Reverse dictionary key look-up for the Homophonic Cipher
		private char Key_Table_Lookup(Dictionary<int, List<string>> map, string num)
		{
			foreach (KeyValuePair<int, List<string>> entry in map)
			{
				if (entry.Value.Contains(num))
				{
					return (char)(entry.Key + (int)'A');
				}
			}
			return '?';
		}

		private string Homophonic_Substitution(string input, string key, bool encryptMessage)
        {
            string output = "";
			// Create dictionary to hold translations
			var keyMap = new Dictionary<int, List<string>>();
			int count = 0;
			foreach (string str in key.Split(';'))
			{
				keyMap[count] = new List<string>();
				foreach (string num in str.Split(','))
				{
					keyMap[count].Add(num);
				}
				count++;
			}
			if (encryptMessage)
			{
				int index = 0;
				for (int i = 0; i < input.Length; i++)
				{
					Random random = new Random();
					if (Char.IsLetter(input[i]))
					{
						int x = Get_Alphabetic_Value(input[i]);
						index = random.Next(keyMap[x].Count);
						output += keyMap[x][index];
					}
					else
					{
						output += input[i];
					}
				}
			}
			else
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (Char.IsNumber(input[i]) && Char.IsNumber(input[i + 1]))
					{
						string num = input[i].ToString() + input[i + 1].ToString();
						output += Key_Table_Lookup(keyMap, num);
						i++;
					}
					else
					{
						output += input[i];
					}
				}
			}
			return output;
        }
    }
}

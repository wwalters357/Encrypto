using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    public class Homophonic_Cipher : Cipher
    {
        public Homophonic_Cipher(string message, string key) : base(message, key, "Homophonic Cipher")
        {

        }

		// --------------------------------------------------------------------
		// ------------------- Accessor Methods -------------------------------
		// --------------------------------------------------------------------

		public override string Image
		{
			get
			{
				return "homophonic_cipher.png";
			}
		}

        public override string Description => "Replaces each letter with associated 2-digit number.";

        public override string History
		{
            get
            {
				return "An early attempt to increase the difficulty of frequency analysis attacks on substitution" +
					" ciphers was to disguise plaintext letter frequencies by homophony.In these ciphers, plaintext" +
					" letters map to more than one ciphertext symbol.Usually, the highest-frequency plaintext symbols" +
					" are given more equivalents than lower frequency letters. In this way, the frequency distribution" +
					" is flattened, making analysis more difficult.Since more than 26 characters will be required in" +
					" the ciphertext alphabet, various solutions are employed to invent larger alphabets. Perhaps the" +
					" simplest is to use a numeric substitution 'alphabet'.Another method consists of simple variations" +
					" on the existing alphabet; uppercase, lowercase, upside down, etc. More artistically, though not" +
					" necessarily more securely, some homophonic ciphers employed wholly invented alphabets of fanciful" +
					" symbols.The Beale ciphers are another example of a homophonic cipher. This is a story of buried" +
					" treasure that was described in 1819–21 by use of a ciphered text that was keyed to the Declaration" +
					" of Independence. Here each ciphertext character was represented by a number.The number was determined" +
					" by taking the plaintext character and finding a word in the Declaration of Independence that started" +
					" with that character and using the numerical position of that word in the Declaration of" +
					" Independence as the encrypted form of that letter.Since many words in the Declaration of Independence" +
					" start with the same letter, the encryption of that character could be any of the numbers associated" +
					" with the words in the Declaration of Independence that start with that letter.Deciphering the" +
					" encrypted text character X(which is a number) is as simple as looking up the Xth word of the" +
					" Declaration of Independence and using the first letter of that word as the decrypted character." +
					"Another homophonic cipher was described by Stahl[2][3] and was one of the first[citation needed]" +
					" attempts to provide for computer security of data systems in computers through encryption.Stahl" +
					" constructed the cipher in such a way that the number of homophones for a given character was in" +
					" proportion to the frequency of the character, thus making frequency analysis much more difficult" +
					".The book cipher and straddling checkerboard are types of homophonic cipher.Francesco I Gonzaga," +
					" Duke of Mantua, used the earliest known example of a homophonic substitution cipher in 1401 for" +
					" correspondence with one Simone de Crema.";

			}
		}

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

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

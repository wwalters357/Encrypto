﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Encrypto
{
    public class Monoalphabetic_Cipher : Cipher
    {
		public Monoalphabetic_Cipher(string message, string key) : base(message, key, "Monoalphabetic Cipher")
        {

        }

		// --------------------------------------------------------------------
		// ------------------- Accessor Methods -------------------------------
		// --------------------------------------------------------------------

		public override string Image
		{
			get
			{
				return "monoalphabetic_cipher.png";
			}
		}

        public override string Description => "Swaps each letter with another from parallel alphabet.";

        public override string History
		{
            get
            {
				return "One of the earliest recorded substitution ciphers, the Atbash cipher imposed monoalphabetic" +
					" substitutions on the Hebrew alphabet. It was a simple system in which every passage of plaintext" +
					" that was encoded used the same ciphertext alphabet.The Atbash cipher created its ciphertext" +
					" alphabet by simply reversing the plaintext alphabet, mapping the first letter of the standard" +
					" alphabet to the last, the second to the second last, and so on . The system derived its name" +
					" phonetically from its substitution of the Hebrew “aleph” with its cipher form of “tav” and “beth”" +
					" with “shin”.Naturally, the method may be applied to any language alphabet, and is a quick and" +
					" easy tool for cloaking messages of non - critical importance from the eyes of casual observers." +
					"An element of additional security may be applied to the technique by tagging a string of numbers," +
					" symbols or punctuation marks to the end of the ciphertext alphabet before performing the substitutions.";
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
			return Monoalphabetic_Substitution(Message, Key, false);
        }

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Monoalphabetic_Substitution(Message, Key, true);
        }

		// Checks the key to make sure it is 26 characters, all letters and has no duplicates
		public override bool Is_Key_Valid()
        {
			if (Key.Length != 26)
			{
				return false;
			}
			for (int i = 0; i < Key.Length; i++)
			{
				if (!Char.IsLetter(Key[i]))
				{
					return false;
				}
				for (int j = i + 1; j < Key.Length; j++)
				{
					if (Key[i] == Key[j])
					{
						return false;
					}
				}
			}
			Key = Key.ToUpper();
			return true;
		}

		// Store each element of key into a dictionary for fast access
		// Key must be length 26 to have a substitution for each letter
		// Must check for valid key
		private string Monoalphabetic_Substitution(string input, string key, bool encryptMessage)
        {
			string output = "";
			// Create key map
			var keyMap = new Dictionary<int, char>();
			for (int i = 0; i < key.Length; i++)
			{
				keyMap.Add(i, key[i]);
			}

			if (encryptMessage)
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (Char.IsLetter(input[i]))
					{
						char c = keyMap[Get_Alphabetic_Value(input[i])];
						output += (char)((Char.IsUpper(input[i])) ? c : c + 'a' - 'A');
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
					if (Char.IsLetter(input[i]))
					{
						// Find current char in map
						int x = keyMap.FirstOrDefault(y => y.Value == Char.ToUpper(input[i])).Key;
						output += (char)((Char.IsUpper(input[i])) ? x + (int)'A' : x + (int)'a');
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

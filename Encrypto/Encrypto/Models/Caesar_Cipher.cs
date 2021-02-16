using Encrypto.Models;
using System;
using System.Resources;

namespace Encrypto
{
    public class Caesar_Cipher : Cipher
    {
        public Caesar_Cipher(string message, string key, Cipher_Type option) : base(message, key, (option == Cipher_Type.Caesar) ? "Caesar Cipher" : "Double Caesar Cipher")
        {
            Option = option;
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public Cipher_Type Option { get; set; }

        public override string Image
        {
            get
            {
                return (Option == Cipher_Type.Caesar) ? "caesar_cipher.png" : "double_caesar_cipher.png";
            }
        }

        public override string Description => "Translates each letter by shifting its ascii value by the key amount to get a new letter";

        public override string History
        {
            get 
            {
                return "The Caesar cipher is named after Julius Caesar, who, according to Suetonius, used it with a shift of three " +
                    "(A becoming D when encrypting, and D becoming A when decrypting) to protect messages of military significance. " +
                    "While Caesar's was the first recorded use of this scheme, other substitution ciphers are known to have been used " +
                    "earlier.His nephew, Augustus, also used the cipher, but with a right shift of one, and it did not wrap around to " +
                    "the beginning of the alphabet:Evidence exists that Julius Caesar also used more complicated systems,[6] and one writer," +
                    " Aulus Gellius, refers to a (now lost) treatise on his ciphers:It is unknown how effective the Caesar cipher was at the" +
                    " time, but it is likely to have been reasonably secure, not least because most of Caesar's enemies would have been illiterate" +
                    " and others would have assumed that the messages were written in an unknown foreign language.[7] There is no record at that time" +
                    " of any techniques for the solution of simple substitution ciphers. The earliest surviving records date to the 9th-century works" +
                    " of Al-Kindi in the Arab world with the discovery of frequency analysis.[8] A Caesar cipher with a shift of one is used on the back" +
                    " of the mezuzah to encrypt the names of God.This may be a holdover from an earlier time when Jewish people were not allowed to have mezuzot." +
                    " The letters of the cryptogram themselves comprise a religiously significant \"divine name\" which Orthodox belief holds keeps the forces of" +
                    " evil in check.[9]In the 19th century, the personal advertisements section in newspapers would sometimes be used to exchange" +
                    " messages encrypted using simple cipher schemes.Kahn(1967) describes instances of lovers engaging in secret communications" +
                    " enciphered using the Caesar cipher in The Times.[10] Even as late as 1915, the Caesar cipher was in use: the Russian army" +
                    " employed it as a replacement for more complicated ciphers which had proved to be too difficult for their troops to master;" +
                    " German and Austrian cryptanalysts had little difficulty in decrypting their messages.[11] A construction of two rotating" +
                    " disks with a Caesar cipher can be used to encrypt or decrypt the code.Caesar ciphers can be found today in children's" +
                    " toys such as secret decoder rings. A Caesar shift of thirteen is also performed in the ROT13 algorithm, a simple method" +
                    " of obfuscating text widely found on Usenet and used to obscure text (such as joke punchlines and story spoilers), but" +
                    " not seriously used as a method of encryption.[12]The Vigenère cipher uses a Caesar cipher with a different shift at" +
                    " each position in the text; the value of the shift is defined using a repeating keyword.If the keyword is as long as" +
                    " the message, is chosen at random, never becomes known to anyone else, and is never reused, this is the one - time pad cipher," +
                    " proven unbreakable.The conditions are so difficult they are, in practical effect, never achieved.Keywords shorter than" +
                    " the message(e.g., \"Complete Victory\" used by the Confederacy during the American Civil War), introduce a cyclic pattern" +
                    " that might be detected with a statistically advanced version of frequency analysis.[13] In April 2006, fugitive Mafia boss" +
                    " Bernardo Provenzano was captured in Sicily partly because some of his messages, clumsily written in a variation of the Caesar" +
                    " cipher, were broken.Provenzano's cipher used numbers, so that \"A\" would be written as \"4\", \"B\" as \"5\", and so on.[14] In 2011," +
                    " Rajib Karim was convicted in the United Kingdom of \"terrorism offences\" after using the Caesar cipher to communicate with" +
                    " Bangladeshi Islamic activists discussing plots to blow up British Airways planes or disrupt their IT networks. Although the" +
                    " parties had access to far better encryption techniques(Karim himself used PGP for data storage on computer disks)," +
                    " they chose to use their own scheme(implemented in Microsoft Excel), rejecting a more sophisticated code program called" +
                    " Mujahedeen Secrets \"because 'kaffirs', or non-believers, know about it, so it must be less secure\".[15]" +
                    " This constituted an application of security through obscurity.";
            }
        }


        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        // Run error checking and return decoded string.
        public override string Decrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                    return Caesar_Substitution(Message, -1 * Int32.Parse(Key));
                case Cipher_Type.Double_Caesar:
                    return Double_Caesar_Substitiution(Message, Key, false);
                default:
                    throw new Exception("Invalid Cipher Type");
            }
        }

        // Run error checking and return encoded string. 
        public override string Encrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                    return Caesar_Substitution(Message, Int32.Parse(Key));
                case Cipher_Type.Double_Caesar:
                    return Double_Caesar_Substitiution(Message, Key, true);
                default:
                    throw new Exception("Invalid Cipher Type");
            }
        }

        // Check if the key is an integer for Caesar Cipher.
        // Check if the key is a string for Double Caesar Cipher.
        public override bool Is_Key_Valid()
        {
            if (Key.Length < 1)
            {
                return false;
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                {
                    return int.TryParse(Key, out _);
                }
                case Cipher_Type.Double_Caesar:
                {
                    foreach (char c in Key)
                    {
                        if (!Char.IsLetter(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            throw new Exception("Invalid Cipher Type!");
        }

        // Shift each letter by a given key.
        private string Caesar_Substitution(string input, int key)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int x = Mod(Get_Alphabetic_Value(input[i]) + key, 26);
                if (Char.IsUpper(input, i))
                {
                    output += (char)(x + (int)'A');
                }
                else if (Char.IsLower(input, i))
                {
                    output += (char)(x + (int)'a');
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }

        // Repeat key for the length of the plain text and for encryption
        // shift each key by the current key letter and for decryption shift the
        // opposite direction for current key letter.
        private string Double_Caesar_Substitiution(string input, string key, bool encryptMessage)
        {
            int decrypt = (encryptMessage) ? 1 : -1;
            string output = "";
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int x = Mod(Get_Alphabetic_Value(input[i]) + (Get_Alphabetic_Value(key[Mod(j, key.Length)]) * decrypt), 26);
                if (Char.IsUpper(input, i))
                {
                    output += (char)(x + (int)'A');
                    j++;
                }
                else if (Char.IsLower(input, i))
                {
                    output += (char)(x + (int)'a');
                    j++;
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }   
    }
}

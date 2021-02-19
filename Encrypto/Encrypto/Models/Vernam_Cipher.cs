using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    public class Vernam_Cipher : Cipher
    {
        public Vernam_Cipher(string message) : base(message, "", "Vernam Cipher")
        {
            MessageBytes = Encoding.ASCII.GetBytes(message);
            GeneratedKey = Generate_Key(message.Length);
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public byte[] MessageBytes { get; }
        public byte[] GeneratedKey { get; set; }

        public override string Image
        {
            get
            {
                return "vernam_cipher.png";
            }
        }

        public override string Description => "Shifts each byte by a randomly generated one time use number.";

        public override string History
        {
            get
            {
                return "The combining function Vernam specified in U.S. Patent 1,310,719, issued July 22, 1919," +
                    " is the XOR operation, applied to the individual impulses or bits used to encode the characters" +
                    " in the Baudot code. Vernam did not use the term \"XOR\" in the patent, but he implemented that" +
                    " operation in relay logic. In the example Vernam gave, the plaintext is A, encoded as \"++-- - \"" +
                    " in Baudot, and the key character is B, encoded as \" + --++\". The resulting ciphertext will be" +
                    " \" - +-++\", which encodes a G. Combining the G with the key character B at the receiving end produces" +
                    " \"++-- - \", which is the original plaintext A. The NSA has called this patent \"perhaps one of the most" +
                    " important in the history of cryptography.\".[1]";
            }
        }

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        /*
         * Need to generate keyBytes randomly and it must be the same
         * length as the message to be encrypted.
        */
        private byte[] Generate_Key(int length)
        {
            Random rnd = new Random();
            byte[] keyBytes = new byte[length];
            rnd.NextBytes(keyBytes);
            return keyBytes;
        }

        public override string Decrypt()
        {
            return Vernam_Translation(MessageBytes, GeneratedKey);
        }

        public override string Encrypt()
        {
            GeneratedKey = Generate_Key(Message.Length);
            return Vernam_Translation(MessageBytes, GeneratedKey);
        }

        public override bool Is_Key_Valid()
        {
            return true;
        }

        // Needed to override 
        public override bool Is_Initialized()
        {
            return Message.Length > 0;
        }

        /*
         * The message is encrypted using XOR operation bewteen inBytes
         * and keyBytes. 
	    */
        private string Vernam_Translation(byte[] messageBytes, byte[] keyBytes)
        {
            for (int i = 0; i < messageBytes.Length; i++)
            {
                messageBytes[i] = (byte)(messageBytes[i] ^ keyBytes[i]);
            }
            return Encoding.ASCII.GetString(messageBytes);
        }
    }
}

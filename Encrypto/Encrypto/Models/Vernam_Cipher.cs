using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    class Vernam_Cipher : Cipher
    {
        public Vernam_Cipher(string message) : base(message, "", "Vernam Cipher")
        {
            MessageBytes = Encoding.ASCII.GetBytes(message);
            GeneratedKey = Generate_Key(message.Length);
        }

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

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public byte[] MessageBytes { get; }
        public byte[] GeneratedKey { get; set; }
    }
}

using System;
using MathNet.Numerics.LinearAlgebra;

namespace Encrypto.Models
{
    class Hill_Cipher : Cipher
    {
        public Hill_Cipher(string message, string key) : base(message, key)
        {

        }

        public override string Decrypt()
        {
            throw new NotImplementedException();
        }

        public override string Encrypt()
        {
            throw new NotImplementedException();
        }

        public override bool Is_Key_Valid()
        {
            throw new NotImplementedException();
        }
    }
}

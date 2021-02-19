using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Encrypto.ViewModels
{
    public class CipherViewModel : INotifyPropertyChanged
    {
        Cipher _cipher;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CipherViewModel(Cipher_Type type)
        {
            Type = type;

            switch (type)
            {
                case Cipher_Type.Caesar:
                    _cipher = new Caesar_Cipher("", "");
                    break;
                case Cipher_Type.Vigenere:
                    _cipher = new Vigenere_Cipher("", "");
                    break;
                case Cipher_Type.Monoalphabetic:
                    _cipher = new Monoalphabetic_Cipher("", "");
                    break;
                case Cipher_Type.Homophonic:
                    _cipher = new Homophonic_Cipher("", "");
                    break;
                case Cipher_Type.Hill:
                    _cipher = new Hill_Cipher("", "");
                    break;
                case Cipher_Type.Vernam:
                    _cipher = new Vernam_Cipher("");
                    break;
                default:
                    break;
            }
        }

        public Cipher_Type Type { get; }

        public string Message
        {
            get => _cipher.Message;
            set
            {
                _cipher.Message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public string Key
        {
            get => _cipher.Key;
            set
            {
                _cipher.Key = value;
                NotifyPropertyChanged("Key");
            }
        }

        public string Name => _cipher.Name;

        public string Image => _cipher.Image;

        public string Description => _cipher.Description;

        public string History => _cipher.History;

        public string Encrypt => _cipher.Encrypt();

        public string Decrypt => _cipher.Decrypt();

        public bool Is_Initialized => _cipher.Is_Initialized();
    }
}

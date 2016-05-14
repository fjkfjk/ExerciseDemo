using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EncryptAndDecrypt
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[] encrypted;
        private bool _ifEncrypted = false;
        private int AlgIndex = 0;
        private TripleDESCryptoServiceProvider tripleDES = null;
        private Rijndael algRIJ = null;
        private AesManaged algAES = null;
        private RSACryptoServiceProvider rsaCrypt = null;
        private DSACryptoServiceProvider dsaCrypt = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (_ifEncrypted)
                return;
            DateTime dt = DateTime.UtcNow;
            switch(AlgIndex)
            {
                case (int)EnumAlgorithm.AES:
                    algAES = new AesManaged();
                    encrypted = Encrypt.EncryptStringToBytes_Aes(TextPlain.Text, algAES.Key, algAES.IV);
                    TextEncrypted.Text = Encoding.Unicode.GetString(encrypted);
                    break;
                case (int)EnumAlgorithm.DES:
                    tripleDES = new TripleDESCryptoServiceProvider();
                    encrypted = Encrypt.EncryptStringToBytes_Des(TextPlain.Text, tripleDES.Key, tripleDES.IV);
                    TextEncrypted.Text = Encoding.Unicode.GetString(encrypted);
                    break;
                case (int)EnumAlgorithm.RIJ:
                    algRIJ = Rijndael.Create();
                    encrypted = Encrypt.EncryptStringToBytes_Rij(TextPlain.Text, algRIJ.Key, algRIJ.IV);
                    TextEncrypted.Text = Encoding.Unicode.GetString(encrypted);
                    break;
                case (int)EnumAlgorithm.DSA:
                    break;
                case (int)EnumAlgorithm.RSA:
                    rsaCrypt = new RSACryptoServiceProvider();
                    string publickeyRSA = rsaCrypt.ToXmlString(false);//公钥
                    encrypted = Encrypt.EncryptStringToBytes_Rsa(TextPlain.Text, publickeyRSA);
                    TextEncrypted.Text = Encoding.Unicode.GetString(encrypted);
                    break;
                default:
                    break;
            }
            lbTime.Content = DateTime.UtcNow.Subtract(dt).ToString();
            _ifEncrypted = true;
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!_ifEncrypted)
                return;
            switch (AlgIndex)
            {
                case (int)EnumAlgorithm.AES:
                    TextEncrypted.Text = Decrypt.DecryptStringFromBytes_Aes(encrypted, algAES.Key, algAES.IV);
                    break;
                case (int)EnumAlgorithm.DES:
                    TextEncrypted.Text = Decrypt.DecryptStringFromBytes_Des(encrypted, tripleDES.Key, tripleDES.IV);
                    break;
                case (int)EnumAlgorithm.DSA:
                    break;
                case (int)EnumAlgorithm.RIJ:
                    TextEncrypted.Text = Decrypt.DecryptStringFromBytes_Rij(encrypted, algRIJ.Key, algRIJ.IV);
                    break;
                case (int)EnumAlgorithm.RSA:
                    string privateKeyRSA = rsaCrypt.ToXmlString(true);//私钥
                    TextEncrypted.Text = Decrypt.DecryptStringFromBytes_Rsa(encrypted, privateKeyRSA);
                    break;
                default:
                    break;
            }
            _ifEncrypted = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            switch((e.Source as RadioButton).Content.ToString())
            {
                case "AES":
                    AlgIndex = (int)EnumAlgorithm.AES;
                    break;
                case "DES":
                    AlgIndex = (int)EnumAlgorithm.DES;
                    break;
                case "RIJ":
                    AlgIndex = (int)EnumAlgorithm.RIJ;
                    break;
                case "DSA":
                    AlgIndex = (int)EnumAlgorithm.DSA;
                    break;
                case "RSA":
                    AlgIndex = (int)EnumAlgorithm.RSA;
                    break;
                default:
                    break;
            }
        }

        public enum EnumAlgorithm
        {
            AES = 0,
            DES,
            RIJ,
            DSA,
            RSA
        }
    }
}

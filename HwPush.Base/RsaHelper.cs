using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HwPush.Base
{
    public class RsaHelper
    {
        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Md5(string Text)
        {
            byte[] result = Encoding.Default.GetBytes(Text.Trim());    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// generate private key and public key arr[0] for private key arr[1] for public key
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateKeys()
        {
            string[] sKeys = new String[4];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);

            sKeys[2] = Md5(sKeys[0]);
            sKeys[3] = Md5(sKeys[1]);


            return sKeys;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string xmlpublicKey, string Date)
        {
            System.Security.Cryptography.RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider();
            //得到公钥
            myrsa.FromXmlString(xmlpublicKey);
            //把你要加密的内容转换成byte[]
            byte[] PlainTextBArray = (new UnicodeEncoding()).GetBytes(Date);
            //使用.NET中的Encrypt方法加密
            byte[] CypherTextBArray = myrsa.Encrypt(PlainTextBArray, false);
            //最后吧加密后的byte[]转换成Base64String，这里就是加密后的内容了
            string Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string Date)
        {
            System.Security.Cryptography.RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider();
            //得到私钥
            myrsa.FromXmlString(xmlPrivateKey);
            //把原来加密后的String转换成byte[]
            byte[] PlainTextBArray = Convert.FromBase64String(Date);
            //使用.NET中的Decrypt方法解密
            byte[] DypherTextBArray = myrsa.Decrypt(PlainTextBArray, false);
            //转换解密后的byte[]，这就得到了我们原来的加密前的内容了
            string Result = (new UnicodeEncoding()).GetString(DypherTextBArray);

            return Result;
        }
        /// <summary>
        /// 获取MD5的Hash描述表
        /// </summary>
        /// <param name="m_strSource"></param>
        /// <returns></returns>
        public static byte[] GetHashData(string m_strSource)
        {
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            byte[] Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            byte[] HashData = MD5.ComputeHash(Buffer);
            return HashData;
        }


        /// <summary>
        /// RSA 签名
        /// </summary>
        /// <param name="KeyPrivate"></param>
        /// <param name="HashData"></param>
        /// <returns></returns>
        public static byte[] RasSign(string KeyPrivate, byte[] HashData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider MYRSA = new System.Security.Cryptography.RSACryptoServiceProvider();
            //得到私钥
            MYRSA.FromXmlString(KeyPrivate);
            //使用.NET中提供的RSA签名，生成刚才我们的MYRSA私钥的签名对象RSAFormatter
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(MYRSA);
            //设置签名的算法为MD5
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名EncryptedSignatureData就是签名后的数据
            byte[] EncryptedSignatureData = RSAFormatter.CreateSignature(HashData);
            return EncryptedSignatureData;
        }

        /// <summary>
        /// RSA 签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="Data">数据</param>
        /// <param name="HashData">签名</param>
        /// <returns></returns>

        public static bool VerifySign(string strKeyPublic, byte[] Data, byte[] HashData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider MYRSA = new System.Security.Cryptography.RSACryptoServiceProvider();
            //得到公钥
            MYRSA.FromXmlString(strKeyPublic);
            //使用.NET中提供的RSA签名，生成刚才我们的MYRSA公钥的验证签名对象RSADeformatter
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(MYRSA);
            //指定解密的时候HASH算法为MD5
            RSADeformatter.SetHashAlgorithm("MD5");
            //验证签名，
            return RSADeformatter.VerifySignature(HashData, Data);

        }













        public static class RSACrypto
        {
            private static readonly Encoding Encoder = Encoding.UTF8;
            /// <summary>
            /// rsa加密
            /// </summary>
            /// <param name="plaintext"></param>
            /// <returns></returns>
            public static String Encrypt(string xmlpublicKey,String plaintext)
            {
                //X509Certificate2 _X509Certificate2 = RSACrypto.RetrieveX509Certificate();
                 
 


                using (RSACryptoServiceProvider RSACryptography =new RSACryptoServiceProvider())
                {

                    RSACryptography.FromXmlString(xmlpublicKey);
                    Byte[] PlaintextData = RSACrypto.Encoder.GetBytes(plaintext);
                    int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

                    if (PlaintextData.Length <= MaxBlockSize)
                        return Convert.ToBase64String(RSACryptography.Encrypt(PlaintextData, false));

                    using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
                    using (MemoryStream CrypStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToEncrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                            Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                            CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                            BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
                    }
                }
            }
            /// <summary>
            ///  rsa解密
            /// </summary>
            /// <param name="ciphertext"></param>
            /// <returns></returns>
            public static String Decrypt(string xmlPrivateKey,String ciphertext)
            {
                //X509Certificate2 _X509Certificate2 = RSACrypto.RetrieveX509Certificate();
                using (RSACryptoServiceProvider RSACryptography =new RSACryptoServiceProvider())
                {
                    RSACryptography.FromXmlString(xmlPrivateKey);
                    Byte[] CiphertextData = Convert.FromBase64String(ciphertext);
                    int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                    if (CiphertextData.Length <= MaxBlockSize)
                        return RSACrypto.Encoder.GetString(RSACryptography.Decrypt(CiphertextData, false));

                    using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
                    using (MemoryStream PlaiStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToDecrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                            Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                            PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                            BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return RSACrypto.Encoder.GetString(PlaiStream.ToArray());
                    }
                }
            }

            //private static X509Certificate2 RetrieveX509Certificate()
            //{
            //    return null;    //检索用于 RSA 加密的 X509Certificate2 证书
            //}
        }
    }
}

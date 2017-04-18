using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Presentacion
{
    public class Crypto
    {
        public static byte[] GenSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[16];
            rng.GetBytes(buff);
            return buff;
        }

        public static byte[] Hash(byte[] salt, string password)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar = System.Text.UTF8Encoding.UTF8.GetBytes(password);
            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(salt);
            hashmd5.Clear();
            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();
            
            //arreglo de bytes donde se guarda la
            //cadena cifrad
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            
            tdes.Clear();
            
            //se regresa el resultado en forma de una cadena
            return ArrayResultado;
            /*
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(password);
            return new SHA256Managed().ComputeHash(salt.Concat(pass).ToArray());*/
        }

        public static bool ComparePassword(string password, byte[] salt, byte[] stored_hash)
        {
            byte[] attempt = Hash(salt, password);

            return stored_hash.SequenceEqual(attempt);
        }

        public static string DecrytedPassword(byte[] salt, byte[] stored_hash)
        {
            byte[] keyArray;
             //convierte el texto en una secuencia de bytes
             byte[] Array_a_Descifrar = stored_hash;
             //se llama a las clases que tienen los algoritmos
             //de encriptación se le aplica hashing
             //algoritmo MD5
             MD5CryptoServiceProvider hashmd5 =
             new MD5CryptoServiceProvider();
            
             keyArray = hashmd5.ComputeHash(salt);
             hashmd5.Clear();
            
             TripleDESCryptoServiceProvider tdes =
             new TripleDESCryptoServiceProvider();
             tdes.Key = keyArray;
             tdes.Mode = CipherMode.ECB;
             tdes.Padding = PaddingMode.PKCS7;

            
             ICryptoTransform cTransform =
              tdes.CreateDecryptor();

            
             byte[] resultArray =
             cTransform.TransformFinalBlock(Array_a_Descifrar,
             0, Array_a_Descifrar.Length);

            
             tdes.Clear();
             //se regresa en forma de cadena
             return System.Text.UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
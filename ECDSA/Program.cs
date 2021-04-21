using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECDSA
{
    class Program
    {
        static void Main(string[] args)
        {
            EllipticCurve ellipticCurve = new EllipticCurve();
            EllipticCurveDSA ellipticCurveDsa = new EllipticCurveDSA(ellipticCurve);
            var keyPair = ellipticCurveDsa.GenerateKeyPair();
            Console.WriteLine($"Приватный ключ: {keyPair.privateKey}");
            Console.WriteLine($"Публичный: {keyPair.publicKey.X}, {keyPair.publicKey.Y}");

            string message = "The quick brown fox jumps over the lazy dog";
            var signature = ellipticCurveDsa.SignMessage(keyPair.privateKey, message);

            Console.WriteLine();

            Console.WriteLine($"Сообщение: {message}");
            Console.WriteLine($"Подпись: {signature.r}, {signature.s}");
            string verification = ellipticCurveDsa.VerifySignature(keyPair.publicKey, message, signature)
                ? "Подпись верна"
                : "Подпись не верна";
            Console.WriteLine(verification);

            message = "The quick brown fox jumps over the lazy cog";
            Console.WriteLine();
            Console.WriteLine($"Сообщение: {message}");
            verification = ellipticCurveDsa.VerifySignature(keyPair.publicKey, message, signature)
                ? "Подпись верна"
                : "Подпись не верна";
            Console.WriteLine(verification);

            message = "The quick brown fox jumps over the lazy dog";
            Console.WriteLine();
            Console.WriteLine($"Сообщение: {message}");
            Console.WriteLine($"Публичный ключ: {keyPair.publicKey.X}, {keyPair.publicKey.Y}");
            verification = ellipticCurveDsa.VerifySignature(keyPair.publicKey, message, signature)
                ? "Подпись верна"
                : "Подпись не верна";
            Console.WriteLine(verification);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECDSA
{

    public class EllipticCurve
    {
        public BigInteger p { get; } = BigInteger.Parse("340282366762482138434845932244680310783");
        public BigInteger a { get; } = BigInteger.Parse("340282366762482138434845932244680310780");
        public BigInteger b { get; } = BigInteger.Parse("308990863222245658030922601041482374867");
        public BigIntegerPoint G { get; } = new BigIntegerPoint(
           BigInteger.Parse("29408993404948928992877151431649155974"),
           BigInteger.Parse("275621562871047521857442314737465260675"));
        public BigInteger n { get; } = BigInteger.Parse("340282366762482138443322565580356624661");
    }
    public class EllipticCurveDSA
    {
        public EllipticCurve Curve { get; }

        public EllipticCurveDSA(EllipticCurve curve)
        {
            Curve = curve;
        }

        public (BigInteger privateKey, BigIntegerPoint publicKey) GenerateKeyPair()
        {
            BigInteger privateKey = RandomIntegerBelow(Curve.n);
            BigIntegerPoint publicKey = ScalarMult(privateKey, Curve.G);
            return (privateKey, publicKey);
        }

        public BigInteger HashMessage(string message)
        {
            byte[] messageBytes = Encoding.Default.GetBytes(message);
            byte[] messageHash;

            using (SHA512Managed sha512M = new SHA512Managed())
            {
                messageHash = sha512M.ComputeHash(messageBytes);
            }

            BigInteger e = new BigInteger(messageHash);
            BigInteger z = e >> (e.BitLenght() - Curve.n.BitLenght());

            if (!(z.BitLenght() <= Curve.n.BitLenght()))
            {
                throw new Exception("!(z.BitLenght() <= Curve.n.BitLenght())");
            }

            return z;
        }

        public (BigInteger r, BigInteger s) SignMessage(BigInteger privateKey, string message)
        {
            BigInteger z = HashMessage(message);

            BigInteger r = 0;
            BigInteger s = 0;

            while (r == 0 || s == 0)
            {
                BigInteger k = RandomIntegerBelow(Curve.n);
                BigIntegerPoint point = ScalarMult(k, Curve.G);

                r = MathMod(point.X, Curve.n);
                s = MathMod((z + r * privateKey) * InverseMod(k, Curve.n), Curve.n);
            }

            return (r, s);
        }

        public bool VerifySignature(BigIntegerPoint publicKey, string message,
            (BigInteger r, BigInteger s) signature)
        {
            BigInteger z = HashMessage(message);

            BigInteger w = InverseMod(signature.s, Curve.n);
            BigInteger u1 = MathMod((z * w), Curve.n);
            BigInteger u2 = MathMod((signature.r * w), Curve.n);

            BigIntegerPoint point = PointAdd(ScalarMult(u1, Curve.G), ScalarMult(u2, publicKey));
            return MathMod(signature.r, Curve.n) == MathMod(point.X, Curve.n);
        }
        public BigIntegerPoint ScalarMult(BigInteger k, BigIntegerPoint point)
        {
            if (MathMod(k, Curve.n) == 0 || point.IsEmpty())
            {
                return point;
            }

            if (k < 0)
            {
                return ScalarMult(-k, NegatePoint(point));
            }

            BigIntegerPoint result = BigIntegerPoint.GetEmpty();
            BigIntegerPoint addend = point;

            while (k > 0)
            {
                if ((k & 1) > 0)
                {
                    result = PointAdd(result, addend);
                }

                addend = PointAdd(addend, addend);

                k >>= 1;
            }

            return result;
        }
        public BigIntegerPoint PointAdd(BigIntegerPoint point1, BigIntegerPoint point2)
        {
            if (point1.IsEmpty())
            {
                return point2;
            }

            if (point2.IsEmpty())
            {
                return point1;
            }

            BigInteger x1 = point1.X, y1 = point1.Y;
            BigInteger x2 = point2.X, y2 = point2.Y;

            if (x1 == x2 && y1 != y2)
            {
                return BigIntegerPoint.GetEmpty();
            }

            BigInteger m = BigInteger.Zero;
            if (x1 == x2)
            {
                m = (3 * x1 * x1 + Curve.a) * InverseMod(2 * y1, Curve.p);
            }
            else
            {
                m = (y1 - y2) * InverseMod(x1 - x2, Curve.p);
            }

            BigInteger x3 = m * m - x1 - x2;
            BigInteger y3 = y1 + m * (x3 - x1);
            BigIntegerPoint result = new BigIntegerPoint(MathMod(x3, Curve.p), MathMod(-y3, Curve.p));

            return result;
        }

        public BigInteger InverseMod(BigInteger k, BigInteger p)
        {
            if (k == 0)
            {
                throw new DivideByZeroException(nameof(k));
            }

            if (k < 0)
            {
                return p - InverseMod(-k, p);
            }

            (BigInteger gcd, BigInteger x, BigInteger y) result = ExtendedGcd(p, k);

            if (result.gcd != 1)
            {
                throw new Exception("Gcd is not 1");
            }

            if (MathMod(k * result.x, p) != 1)
            {
                throw new Exception("(k * result.x) % p != 1");
            }

            return MathMod(result.x, p);
        }

        public static BigInteger MathMod(BigInteger a, BigInteger b)
        {
            return (BigInteger.Abs(a * b) + a) % b;
        }

        public static (BigInteger gcd, BigInteger x, BigInteger y) ExtendedGcd(BigInteger a, BigInteger b)
        {
            BigInteger s = 0, old_s = 1;
            BigInteger t = 1, old_t = 0;
            BigInteger r = a, old_r = b;

            while (r != 0)
            {
                BigInteger q = old_r / r;
                var temp = r;
                r = old_r - q * r;
                old_r = temp;

                temp = s;
                s = old_s - q * s;
                old_s = temp;

                temp = t;
                t = old_t - q * t;
                old_t = temp;
            }

            return (old_r, old_s, old_t);
        }

        public BigIntegerPoint NegatePoint(BigIntegerPoint point)
        {
            if (point.IsEmpty())
            {
                return point;
            }

            BigInteger x = point.X;
            BigInteger y = point.Y;
            BigIntegerPoint result = new BigIntegerPoint(x, MathMod(-y, Curve.p));

            return result;
        }
        public bool IsOnCurve(BigIntegerPoint point)
        {
            if (point.IsEmpty())
            {
                return true;
            }

            BigInteger x = point.X;
            BigInteger y = point.Y;

            return MathMod(y * y - x * x * x - Curve.a * x - Curve.b, Curve.p) == 0;
        }

        public static BigInteger RandomIntegerBelow(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;
            Random random = new Random();
            do
            {
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F;
                R = new BigInteger(bytes);
            } while (R >= N && R >= 1);

            return R;
        }
    }
    public struct BigIntegerPoint
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }

        public BigIntegerPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }

        public static BigIntegerPoint GetEmpty()
        {
            return new BigIntegerPoint(0, 0);
        }

        public bool IsEmpty() => X == BigInteger.Zero && Y == BigInteger.Zero;
    }
    public static class IntegerExtensions
    {
        public static int BitLenght(this int source)
        {
            int bitLenght = 0;
            while (source / 2 != 0)
            {
                source /= 2;
                bitLenght++;
            }
            bitLenght += 1;
            return bitLenght;
        }

        public static int BitLenght(this BigInteger source)
        {
            int bitLenght = 0;
            while (source / 2 != 0)
            {
                source /= 2;
                bitLenght++;
            }
            bitLenght += 1;
            return bitLenght;
        }
    }
}


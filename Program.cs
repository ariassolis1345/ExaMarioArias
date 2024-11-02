using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExaMarioArias
{
    internal class Program
    {
        
     int valor = 0;

        private static readonly object lockBinario = new object();
        private static readonly object lockOctal = new object();
        private static readonly object lockHexadecimal = new object();

        public void ingresarDatos()
        {
            Console.WriteLine("CONVERSOR DE DECIMAL A BINARIO, OCTAL Y HEXADECIMAL");
            Console.WriteLine("Ingrese un valor: ");

            valor = int.Parse(Console.ReadLine());
        }

        public void convertirBinario()
        {

            lock (lockBinario)
            {
                Console.WriteLine("Hilo binario controla lockBinario.");
                Thread.Sleep(100);
                lock (lockOctal)
                {
                    Console.WriteLine("Hilo binario controla lockOctal.");
                    string binario = Convert.ToString(valor, 2);
                    Console.WriteLine("Binario: " + binario);
                }

            }
        }

        public void convertirOctal()
        {
            lock (lockOctal)
            {
                Console.WriteLine("Hilo Octal controla lockOctal");
                Thread.Sleep(100);
                lock (lockHexadecimal)
                {
                    Console.WriteLine("Hilo Octal controla lockHexadecimal.");
                    string octal = Convert.ToString(valor, 8);
                    Console.WriteLine($"Octal: {octal}");

                }
            }
        }

        public void convertirHexadecimal()
        {
            lock (lockHexadecimal)
            {
                Console.WriteLine("Hilo Hexadecimal controla lockHexadecimal");
                Thread.Sleep(100);
                lock (lockBinario)
                {
                    Console.WriteLine("Hilo Hexadecimal controla lockBinario.");
                    string hexadecimal = Convert.ToString(valor, 16);
                    Console.WriteLine($"Hexadecimal: {hexadecimal}");

                }
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ingresarDatos();

            Thread hiloBinario = new Thread(new ThreadStart(program.convertirBinario));
            Thread hiloOctal = new Thread(new ThreadStart(program.convertirOctal));
            Thread hiloHexadecimal = new Thread(new ThreadStart(program.convertirHexadecimal));

            hiloBinario.Start();
            hiloOctal.Start();
            hiloHexadecimal.Start();

            hiloBinario.Join();
            hiloOctal.Join();
            hiloHexadecimal.Join();

            Console.WriteLine("El programa ha finalizado.");


        }
    }
    }


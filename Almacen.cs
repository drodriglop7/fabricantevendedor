using System;
using System.Threading;

namespace fabricantevendedor
{
    public class Almacen
    {
        private readonly object bloqueo = new object();
        private int _contador;
        private long _initTime;
        public Almacen()
        {
            this._initTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this._contador = 0;
        }

        public void Guardar()
        {
            lock (bloqueo)
            {
                this._contador++;
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Console.WriteLine("+ {0} {1} Guarda.  Almacen {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    (currentTime - this._initTime).ToString().PadLeft(5),
                    this._contador);
                Monitor.Pulse(bloqueo);
            }
        }
        public void Sacar()
        {
            long currentTime;
            lock (bloqueo)
            {
                while (this._contador == 0)
                {
                    currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    Console.WriteLine("| {0} {1} Bloqueo. Almacen {2}",
                        Thread.CurrentThread.ManagedThreadId,
                        (currentTime - this._initTime).ToString().PadLeft(5),
                        this._contador);
                    Monitor.Wait(bloqueo);
                }
                this._contador--;
                currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Console.WriteLine("- {0} {1} Saca.    Almacen {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    (currentTime - this._initTime).ToString().PadLeft(5),
                    this._contador);
                Monitor.Pulse(bloqueo);
            }
        }
    }

}
using System;
using System.Threading;

namespace fabricantevendedor
{
    public class Vendedor
    {
        private Almacen _a;
        private Thread _t;
        private Random _rnd = new Random();
        public Vendedor(Almacen a)
        {
            this._a = a;
        }

        public void Vende()
        {
            this._t = new Thread(() => this._Accion());
            this._t.Start();
        }

        public void Termina()
        {
            _t.Join();
        }

        private void _Accion()
        {
            int ms;
            for (int i = 0; i < 10; i++)
            {
                ms = _rnd.Next(1000, 1500);
                Thread.Sleep(ms);
                _a.Sacar();
            }
        }
    }
}
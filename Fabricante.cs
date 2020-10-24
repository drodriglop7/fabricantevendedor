using System;
using System.Threading;

namespace fabricantevendedor
{
    public class Fabricante
    {
        private Almacen _a;
        private Thread _t;
        private Random _rnd = new Random();
        public Fabricante(Almacen a)
        {
            this._a = a;
        }

        public void Fabrica()
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
                ms = _rnd.Next(1000, 2000);
                Thread.Sleep(ms);
                _a.Guardar();
            }
        }
    }

}
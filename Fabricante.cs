using System;
using System.Threading;

namespace fabricantevendedor
{
    public class Fabricante
    {
        private Almacen _a;
        private Thread _t;
        private Random _rnd = new Random();
        private int _mls;//milisegundos
        private int _art;//milisegundos
        public Fabricante(Almacen a , int mls , int art)
        {
            this._a = a;
            this._mls = mls;
            this._art = art;
        }

        public void Fabrica()
        {
            this._t = new Thread(() => this._Accion(this._mls , this._art));
            this._t.Start();
        }

        public void Termina()
        {
            _t.Join();
        }

        private void _Accion(int milisegundos , int articulos)
        {
            for (int i = 0; i < articulos; i++)
            {
                Thread.Sleep(milisegundos);
                _a.Guardar();
            }
        }
    }

}
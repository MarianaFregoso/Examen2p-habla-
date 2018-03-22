using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Efectos
{
    class Delay : ISampleProvider
    {
        internal float read;
        private ISampleProvider fuente;

        public int offsetTiempoMS;
        private float ate;
        List<float> muestras = new List<float>();
        public Delay(ISampleProvider fuente, float ate)
        {
            this.fuente = fuente;
            this.ate = ate;

            offsetTiempoMS = 600;
            //50-5000
            
        }

        public WaveFormat WaveFormat {
            get {
                return fuente.WaveFormat;
            }
        }
          
        //offset es el numero de muestras leidas
        public int Read(float[] buffer, int offset, int count)
        {

          

            var read = fuente.Read(buffer, offset, count);
            float tiempoTranscurrido = (float) muestras.Count / (float)fuente.WaveFormat.SampleRate;
            int muestrastrasncurridas = muestras.Count;
            float tiempoTranscurridoMS = tiempoTranscurrido * 1000;
            int numMuestrasOffsetTiempo = (int) (((float)offsetTiempoMS / 1000.0f)
                * (float)fuente.WaveFormat.SampleRate);
            
            //añadir muestras a nuestro buffer
            for (int i = 0; i < read; i++)
            {
                muestras.Add(buffer[i]);
            }


            //mofificar muestras
            if (tiempoTranscurridoMS > offsetTiempoMS)
            {
                for (int i = 0; i < read; i++)
                {
                  

                       buffer[i] += muestras[muestrastrasncurridas +
                         i - numMuestrasOffsetTiempo] * ate ;
                }
            }

            return read;
        }
    }
}

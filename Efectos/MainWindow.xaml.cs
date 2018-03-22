﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using NAudio;
using NAudio.Wave;

namespace Efectos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        WaveOutEvent waveout;
        AudioFileReader reader;
        Delay delayProvider;

        public MainWindow()
        {
            InitializeComponent();
            waveout = new WaveOutEvent();
        }

        private void btnexaminar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
             if ((bool)fileDialog.ShowDialog())
            {
                txtruta.Text = fileDialog.FileName;
                reader = new AudioFileReader(fileDialog.FileName);
            }

        }

        private void btnrepro_Click(object sender, RoutedEventArgs e)
        {
            if (waveout !=null)
            {
                if (waveout.PlaybackState == PlaybackState.Playing)
                {
                    
                    waveout.Stop();
                }
                waveout.Init(reader);
                
                waveout.Play();
            }

          

        }

        private void sldretraso_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblnum != null)
            {
                 lblnum.TextInput = sldretraso.Value.ToString();
                delayProvider.offsetTiempoMS = (int)sldretraso.Value;
            }
        }

      
        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            if (waveout != null && reader != null)
            {
                if (waveout.PlaybackState == PlaybackState.Playing)
                {
                    waveout.Stop();
                }
                delayProvider = new Delay(reader, float.Parse(txtate.Text));
                waveout.Init(delayProvider);
                waveout.Play();
            }
        }
    }
}



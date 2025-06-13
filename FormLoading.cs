using System;
using System.Windows.Forms;

namespace ProjetoMIPs
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Atualiza a progressão da barra e o texto.
        /// Valor deve estar entre 0 e 100.
        /// </summary>
        public void AtualizarProgresso(int percentual)
        {
            if (percentual < 0) percentual = 0;
            if (percentual > 100) percentual = 100;

            progressBar.Value = percentual;
            lblLoading.Text = $"Carregando... {percentual}%";
            // Força redraw imediato
            Application.DoEvents();
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ProjetoMIPs
{
    public partial class FormPrincipal : Form
    {
        private readonly CpuMips cpu;
        private string? arquivoAtual;

        // Quando true, a linha apontada pelo PC ficará em azul escuro.
        private bool highlightNextEnabled = true;

        public FormPrincipal()
        {
            InitializeComponent();
            cpu = new CpuMips();
            
            cpu.FrequenciaDecimal = 10M;
            cpu.ParametroI = 1;
            cpu.ParametroJ = 1;
            cpu.ParametroR = 1;
            
            // ======== Bindings das propriedades de configuração (CPU) ========
            nudFrequencia.DataBindings.Add(
                nameof(NumericUpDown.Value),
                cpu,
                nameof(cpu.FrequenciaDecimal),
                true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamI.DataBindings.Add(
                nameof(NumericUpDown.Value),
                cpu,
                nameof(cpu.ParametroI),
                true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamJ.DataBindings.Add(
                nameof(NumericUpDown.Value),
                cpu,
                nameof(cpu.ParametroJ),
                true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamR.DataBindings.Add(
                nameof(NumericUpDown.Value),
                cpu,
                nameof(cpu.ParametroR),
                true, DataSourceUpdateMode.OnPropertyChanged);

            // Ajustes nos NumericUpDown
            nudFrequencia.Value = 10;
            nudFrequencia.Minimum = 0.01M;
            nudFrequencia.Maximum = 50M;
            nudFrequencia.DecimalPlaces = 2;
            nudFrequencia.Increment = 0.01M;
            foreach (var nud in new[] { nudParamI, nudParamJ, nudParamR })
            {
                nud.Minimum = 1;
                nud.Maximum = 1000;
                nud.Increment = 1;
                nud.Value = 1;
            }

            // ======== Configuração dos DataGridView (tema escuro) ========
            ConfigureGrid(dgvRegistradores, new[]
            {
                ("Numero", "Número"),
                ("Registrador", "Registrador"),
                ("Hexadecimal", "Hexadecimal"),
                ("Decimal", "Decimal")
            });
            dgvRegistradores.DataSource = cpu.RegistradoresView;

            ConfigureGrid(dgvMemoriaPrograma, new[]
            {
                ("Endereco", "Endereço"),
                ("Cod32", "Código"),
                ("Instrucao", "Instrução"),
                ("Operacao", "Operação")
            });
            dgvMemoriaPrograma.DataSource = cpu.MemoriaProgramaView;

            ConfigureGrid(dgvMemoriaDados, new[]
            {
                ("Endereco", "Endereço"),
                ("Tipo", "Tipo"),
                ("Hexadecimal", "Hexadecimal"),
                ("Decimal", "Decimal")
            });
            dgvMemoriaDados.DataSource = cpu.MemoriaDadosView;

            // ======== Quando a CPU notificar mudança de propriedade, atualize labels ========
            cpu.PropertyChanged += Cpu_PropertyChanged;

            // No início, não há nada carregado: então só atualizamos a interface sem executar nada
            AtualizarInterface();
        }

        private void ConfigureGrid(DataGridView grid, (string DataProperty, string Header)[] cols)
        {
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            grid.EnableHeadersVisualStyles = false;

            // Fundo geral do grid (fora das células)
            grid.BackgroundColor = Color.FromArgb(37, 37, 38);

            // Cabeçalho: mantém cinza escuro de fundo e texto claro
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.LightGray;

            // Remove bordas de linhas
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ===== Hier estão os estilos gerais das células =====
            // Fundo preto e texto branco por padrão
            grid.DefaultCellStyle.BackColor = Color.Black;
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 50, 50);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            // ======================================================

            foreach (var (prop, header) in cols)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = prop,
                    HeaderText = header,
                    ReadOnly = true
                };
                grid.Columns.Add(col);
            }
        }

        private void Cpu_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Cpu_PropertyChanged(sender, e)));
                return;
            }

            // Estamos na UI thread: atualiza labels se alguma propriedade relevante mudou
            if (e.PropertyName is nameof(cpu.ContadorPrograma)
                          or nameof(cpu.TotalCiclos)
                          or nameof(cpu.InstrucaoBinariaAtual)
                          or nameof(cpu.InstrucaoHexAtual))
            {
                UpdateStatusLabels();
            }
        }

        private void UpdateStatusLabels()
        {
            txtInstrucaoBinaria.Text = cpu.InstrucaoBinariaAtual;
            txtInstrucaoHex.Text = cpu.InstrucaoHexAtual;
            lblPC.Text = $"PC: 0x{cpu.ContadorPrograma * 4:X8}";
            lblTempoTotal.Text = $"Tempo Total: {cpu.TotalCiclos}";

            // 1) calcula o total de segundos
            double periodo = 1.0 / (cpu.FrequenciaGHz * 1e9);       // s por ciclo
            double totalSeg = cpu.TotalCiclos * periodo;            // segundos

            // 2) seleciona a unidade apropriada
            string texto;
            if (totalSeg >= 1.0)
            {
                texto = $"{totalSeg:F3} s";
            }
            else if (totalSeg >= 1e-3)
            {
                texto = $"{totalSeg * 1e3:F3} ms";
            }
            else if (totalSeg >= 1e-6)
            {
                texto = $"{totalSeg * 1e6:F3} µs";
            }
            else if (totalSeg >= 1e-9)
            {
                texto = $"{totalSeg * 1e9:F3} ns";
            }
            else // abaixo de 1 ns
            {
                texto = $"{totalSeg * 1e12:F3} ps";
            }

            lblTempoAtual.Text = $"Tempo atual: {texto}";
        }

        /// <summary>
        /// Atualiza as grades e aplica destaque de cores:
        /// - Linha executada (ContadorPrograma - 1) fica amarela.
        /// - Linha a ser executada (ContadorPrograma) fica azul escuro, se highlightNextEnabled == true.
        /// </summary>
        private void AtualizarInterface()
        {
            cpu.AtualizarTabelas();
            dgvRegistradores.Refresh();
            dgvMemoriaPrograma.Refresh();
            dgvMemoriaDados.Refresh();
            UpdateStatusLabels();

            AplicarDestaqueMemoriaPrograma();

            ScrollToCurrentInstruction();
        }

        private void ScrollToCurrentInstruction()
        {
            int currentIndex = cpu.ContadorPrograma;
            if (currentIndex < 0 || currentIndex >= dgvMemoriaPrograma.Rows.Count)
                return;

            int visibleRows = dgvMemoriaPrograma.DisplayedRowCount(false);
            int firstIndex = currentIndex - visibleRows / 2;

            if (firstIndex < 0) firstIndex = 0;
            if (firstIndex > dgvMemoriaPrograma.Rows.Count - 1)
                firstIndex = dgvMemoriaPrograma.Rows.Count - 1;

            dgvMemoriaPrograma.ClearSelection();
            dgvMemoriaPrograma.FirstDisplayedScrollingRowIndex = firstIndex;
        }


        /// <summary>
        /// Percorre cada linha de dgvMemoriaPrograma e ajusta a cor conforme o índice:
        /// - linha (ContadorPrograma - 1) → backColor = Yellow, texto preto
        /// - linha (ContadorPrograma) → backColor = DarkBlue, texto branco (apenas se highlightNextEnabled = true)
        /// - todo o restante usa o estilo padrão (fundo preto, texto branco)
        /// </summary>
        private void AplicarDestaqueMemoriaPrograma()
        {
            // Primeiro, limpa cores anteriores
            foreach (DataGridViewRow row in dgvMemoriaPrograma.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.Black;
                row.DefaultCellStyle.ForeColor = Color.White;
            }

            int executedIndex = cpu.ContadorPrograma - 1;
            int nextIndex = cpu.ContadorPrograma;

            // Se existe índice de instrução executada, destaque em amarelo
            if (executedIndex >= 0 && executedIndex < dgvMemoriaPrograma.Rows.Count)
            {
                var rowExec = dgvMemoriaPrograma.Rows[executedIndex];
                rowExec.DefaultCellStyle.BackColor = Color.Yellow;
                rowExec.DefaultCellStyle.ForeColor = Color.Black;
            }

            // Se a flag estiver ativada e o índice next estiver dentro do range, destaque em azul escuro
            if (highlightNextEnabled && nextIndex >= 0 && nextIndex < dgvMemoriaPrograma.Rows.Count)
            {
                var rowNext = dgvMemoriaPrograma.Rows[nextIndex];
                rowNext.DefaultCellStyle.BackColor = Color.DarkBlue;
                rowNext.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        // =================================================================================
        // Exemplo de método público que você pode amarrar a um botão “Ativar/Desativar Azul”
        // =================================================================================
        public void ToggleHighlightNext()
        {
            highlightNextEnabled = !highlightNextEnabled;
            AplicarDestaqueMemoriaPrograma();
        }

        // ======================================================
        // Botão “Abrir Código” na ToolStrip
        // ======================================================
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            using var openFile = new OpenFileDialog
            {
                Title = "Abrir Arquivo",
                Filter = "Arquivos de Texto (*.txt;*.asm)|*.txt;*.asm|Todos os Arquivos (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            arquivoAtual = openFile.FileName;
            try
            {
                string fileContent = File.ReadAllText(arquivoAtual);
                txtCodigoFonte.Text = fileContent;

                // Tentar extrair a primeira linha como configuração de CPU, caso exista
                var primeiraLinha = File.ReadLines(arquivoAtual).FirstOrDefault();
                if (!string.IsNullOrEmpty(primeiraLinha)
                    && primeiraLinha.StartsWith("config_CPU"))
                {
                    ProcessarConfiguracaoNaString(primeiraLinha);
                }

                // Agora carregue o programa inteiro na CPU
                cpu.CarregarPrograma(fileContent);
                AtualizarInterface();

                MessageBox.Show("Arquivo carregado e programa inicializado com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessarConfiguracaoNaString(string primeiraLinha)
        {
            // Exemplo de linha esperada: config_CPU = [50GHz, i=2, j=4, r=1]
            try
            {
                var dados = primeiraLinha
                    .Replace("config_CPU", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("=", "")
                    .Replace("GHz", "")
                    .Trim();

                // Ex.: "  50 , i 2 , j 4 , r 1 "
                var itens = dados.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in itens)
                {
                    var parte = item.Trim();
                    // Se contém letra “i”, “j” ou “r” no início
                    if (parte.StartsWith("i", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tok = parte.Substring(1).Trim();
                        if (int.TryParse(tok, out var vi))
                            nudParamI.Value = vi;
                    }
                    else if (parte.StartsWith("j", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tok = parte.Substring(1).Trim();
                        if (int.TryParse(tok, out var vj))
                            nudParamJ.Value = vj;
                    }
                    else if (parte.StartsWith("r", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tok = parte.Substring(1).Trim();
                        if (int.TryParse(tok, out var vr))
                            nudParamR.Value = vr;
                    }
                    else
                    {
                        // Pode ser o valor de frequência puro
                        if (double.TryParse(parte, out var f))
                            nudFrequencia.Value = (decimal)f;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Formato inválido de config_CPU na primeira linha.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ======================================================
        // Botão “Salvar Código” na ToolStrip
        // ======================================================
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(arquivoAtual))
            {
                MessageBox.Show("Não há arquivo aberto para salvar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                File.WriteAllText(arquivoAtual, txtCodigoFonte.Text);
                MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar arquivo: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ======================================================
        // Eventos dos botões de controle de execução
        // ======================================================
        private void btnProximoCiclo_Click(object sender, EventArgs e)
        {
            cpu.ExecutarCiclo();
            AtualizarInterface();
        }

        private async void btnExecutarTudo_Click(object sender, EventArgs e)
        {
            if (cpu.MemoriaProgramaCount == 0)
            {
                MessageBox.Show("Nenhum programa carregado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var loadingForm = new FormLoading())
            {
                loadingForm.Show(this);
                loadingForm.Refresh();

                int totalInstrucoes = cpu.MemoriaProgramaCount;
                var progresso = new Progress<int>(p => loadingForm.AtualizarProgresso(p));

                await Task.Run(() =>
                {
                    int executadas = 0;
                    while (!cpu.Parada)
                    {
                        cpu.ExecutarCiclo();
                        executadas++;
                        int pct = (int)(executadas * 100L / totalInstrucoes);
                        (progresso as IProgress<int>)?.Report(pct);
                        Thread.Sleep(500);
                    }
                });

                loadingForm.Close();
            }

            AtualizarInterface();
        }

        private void AtivaDesativaPC_Click(object sender, EventArgs e)
        {
            ToggleHighlightNext();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Application.Exit();
        }


        private void tsbSave_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Salvar arquivo";
                saveDialog.Filter = "Arquivo de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
                saveDialog.FileName = "CodigoFonte.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveDialog.FileName, txtCodigoFonte.Text);
                        MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnApplyConfig_Click(object sender, EventArgs e)
        {
            string config = string.Format(
                "config_CPU = [{0}GHz, i={1}, j={2}, r={3}]",
                cpu.FrequenciaGHz,
                cpu.ParametroI,
                cpu.ParametroJ,
                cpu.ParametroR
            );

            var lines = txtCodigoFonte.Lines.ToList();

            if (lines.Count > 0 && lines[0].StartsWith("config_CPU", StringComparison.InvariantCultureIgnoreCase))
            {
                lines[0] = config;
            }
            else
            {
                lines.Insert(0, config);
            }

            txtCodigoFonte.Lines = lines.ToArray();

            cpu.CarregarPrograma(string.Join(Environment.NewLine, lines));
            AtualizarInterface();
        }

        private void txtCodigoFonte_TextChanged(object sender, EventArgs e)
        {
            var lines = txtCodigoFonte.Lines;
            if (lines.Length == 0) return;

            var primeira = lines[0].Trim();
            if (!primeira.StartsWith("config_CPU", StringComparison.InvariantCultureIgnoreCase))
                return;

            try
            {
                // Extrai o conteúdo entre [ e ]
                var inner = primeira
                    .Substring(
                        primeira.IndexOf('[') + 1,
                        primeira.LastIndexOf(']') - primeira.IndexOf('[') - 1
                    );

                // Divide em tokens, remove espaços
                var tokens = inner
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim());

                // Variáveis temporárias
                double freq = 0;
                int vi = 0, vj = 0, vr = 0;

                foreach (var tok in tokens)
                {
                    // Se contém "GHz", é frequência
                    if (tok.EndsWith("GHz", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var num = tok[..^3].Trim();      // tudo exceto os últimos 3 chars
                        freq = double.Parse(num);
                    }
                    else
                    {
                        // Deve estar no formato chave=valor
                        var parts = tok.Split('=', 2);
                        if (parts.Length != 2) continue;

                        var key = parts[0].Trim().ToLower();
                        var val = int.Parse(parts[1].Trim());

                        switch (key)
                        {
                            case "i": vi = val; break;
                            case "j": vj = val; break;
                            case "r": vr = val; break;
                        }
                    }
                }

                // Atualiza só se parse deu tudo certo
                nudFrequencia.Value = (decimal)freq;
                nudParamI.Value = vi;
                nudParamJ.Value = vj;
                nudParamR.Value = vr;

                // recarrega o programa
                cpu.CarregarPrograma(txtCodigoFonte.Text);
                AtualizarInterface();
            }
            catch
            {
                // ignora qualquer erro de parse
            }
        }



    }
}

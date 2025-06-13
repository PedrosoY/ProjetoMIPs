namespace ProjetoMIPs
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.TabControl tabControlPages;
        private System.Windows.Forms.TabPage tabRegistradores;
        private System.Windows.Forms.DataGridView dgvRegistradores;
        private System.Windows.Forms.TabPage tabMemoriaPrograma;
        private System.Windows.Forms.DataGridView dgvMemoriaPrograma;
        private System.Windows.Forms.TabPage tabMemoriaDados;
        private System.Windows.Forms.DataGridView dgvMemoriaDados;
        private System.Windows.Forms.TabPage tabConfiguracao;
        private System.Windows.Forms.TableLayoutPanel tblConfiguracao;
        private System.Windows.Forms.Label lblFrequencia;
        private System.Windows.Forms.NumericUpDown nudFrequencia;
        private System.Windows.Forms.Label lblParamI;
        private System.Windows.Forms.NumericUpDown nudParamI;
        private System.Windows.Forms.Label lblParamJ;
        private System.Windows.Forms.NumericUpDown nudParamJ;
        private System.Windows.Forms.Label lblParamR;
        private System.Windows.Forms.NumericUpDown nudParamR;
        private System.Windows.Forms.Button btnApplyConfig;
        private System.Windows.Forms.TextBox txtCodigoFonte;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblTempoTotal;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.GroupBox grpControles;
        private System.Windows.Forms.Button btnProximoCiclo;
        private System.Windows.Forms.Button btnExecutarTudo;
        private System.Windows.Forms.GroupBox grpInstrucaoAtual;
        private System.Windows.Forms.TextBox txtInstrucaoHex;
        private System.Windows.Forms.TextBox txtInstrucaoBinaria;

        private void InitializeComponent()
        {
            toolStripMenu = new ToolStrip();
            tsbOpen = new ToolStripButton();
            tsbSave = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            AtivaDesativaPC = new ToolStripButton();
            splitContainerPrincipal = new SplitContainer();
            tabControlPages = new TabControl();
            tabRegistradores = new TabPage();
            dgvRegistradores = new DataGridView();
            tabMemoriaPrograma = new TabPage();
            dgvMemoriaPrograma = new DataGridView();
            tabMemoriaDados = new TabPage();
            dgvMemoriaDados = new DataGridView();
            tabConfiguracao = new TabPage();
            tblConfiguracao = new TableLayoutPanel();
            lblFrequencia = new Label();
            nudFrequencia = new NumericUpDown();
            lblParamI = new Label();
            nudParamI = new NumericUpDown();
            lblParamJ = new Label();
            nudParamJ = new NumericUpDown();
            lblParamR = new Label();
            nudParamR = new NumericUpDown();
            btnApplyConfig = new Button();
            txtCodigoFonte = new TextBox();
            grpStatus = new GroupBox();
            lblTempoAtual = new Label();
            lblTempoTotal = new Label();
            lblPC = new Label();
            grpControles = new GroupBox();
            btnReset = new Button();
            btnProximoCiclo = new Button();
            btnExecutarTudo = new Button();
            grpInstrucaoAtual = new GroupBox();
            txtInstrucaoHex = new TextBox();
            txtInstrucaoBinaria = new TextBox();
            toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPrincipal).BeginInit();
            splitContainerPrincipal.Panel1.SuspendLayout();
            splitContainerPrincipal.Panel2.SuspendLayout();
            splitContainerPrincipal.SuspendLayout();
            tabControlPages.SuspendLayout();
            tabRegistradores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRegistradores).BeginInit();
            tabMemoriaPrograma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaPrograma).BeginInit();
            tabMemoriaDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaDados).BeginInit();
            tabConfiguracao.SuspendLayout();
            tblConfiguracao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFrequencia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamJ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudParamR).BeginInit();
            grpStatus.SuspendLayout();
            grpControles.SuspendLayout();
            grpInstrucaoAtual.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripMenu
            // 
            toolStripMenu.Items.AddRange(new ToolStripItem[] { tsbOpen, tsbSave, toolStripSeparator1, AtivaDesativaPC });
            toolStripMenu.Location = new Point(0, 0);
            toolStripMenu.Name = "toolStripMenu";
            toolStripMenu.Size = new Size(1200, 25);
            toolStripMenu.TabIndex = 0;
            // 
            // tsbOpen
            // 
            tsbOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbOpen.Name = "tsbOpen";
            tsbOpen.Size = new Size(79, 22);
            tsbOpen.Text = "Abrir Código";
            tsbOpen.Click += tsbOpen_Click;
            // 
            // tsbSave
            // 
            tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(84, 22);
            tsbSave.Text = "Salvar Código";
            tsbSave.Click += tsbSave_Click_1;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // AtivaDesativaPC
            // 
            AtivaDesativaPC.DisplayStyle = ToolStripItemDisplayStyle.Text;
            AtivaDesativaPC.Name = "AtivaDesativaPC";
            AtivaDesativaPC.Size = new Size(128, 22);
            AtivaDesativaPC.Text = "Ativar ou Desativar PC";
            AtivaDesativaPC.Click += AtivaDesativaPC_Click;
            // 
            // splitContainerPrincipal
            // 
            splitContainerPrincipal.Dock = DockStyle.Fill;
            splitContainerPrincipal.Location = new Point(0, 25);
            splitContainerPrincipal.Name = "splitContainerPrincipal";
            // 
            // splitContainerPrincipal.Panel1
            // 
            splitContainerPrincipal.Panel1.BackColor = Color.FromArgb(45, 45, 48);
            splitContainerPrincipal.Panel1.Controls.Add(tabControlPages);
            // 
            // splitContainerPrincipal.Panel2
            // 
            splitContainerPrincipal.Panel2.BackColor = Color.FromArgb(37, 37, 38);
            splitContainerPrincipal.Panel2.Controls.Add(txtCodigoFonte);
            splitContainerPrincipal.Panel2.Controls.Add(grpStatus);
            splitContainerPrincipal.Panel2.Controls.Add(grpControles);
            splitContainerPrincipal.Panel2.Controls.Add(grpInstrucaoAtual);
            splitContainerPrincipal.Size = new Size(1200, 600);
            splitContainerPrincipal.SplitterDistance = 480;
            splitContainerPrincipal.TabIndex = 1;
            // 
            // tabControlPages
            // 
            tabControlPages.Controls.Add(tabRegistradores);
            tabControlPages.Controls.Add(tabMemoriaPrograma);
            tabControlPages.Controls.Add(tabMemoriaDados);
            tabControlPages.Controls.Add(tabConfiguracao);
            tabControlPages.Dock = DockStyle.Fill;
            tabControlPages.Location = new Point(0, 0);
            tabControlPages.Name = "tabControlPages";
            tabControlPages.SelectedIndex = 0;
            tabControlPages.Size = new Size(480, 600);
            tabControlPages.TabIndex = 0;
            // 
            // tabRegistradores
            // 
            tabRegistradores.BackColor = Color.FromArgb(45, 45, 48);
            tabRegistradores.Controls.Add(dgvRegistradores);
            tabRegistradores.Location = new Point(4, 24);
            tabRegistradores.Name = "tabRegistradores";
            tabRegistradores.Padding = new Padding(3);
            tabRegistradores.Size = new Size(472, 572);
            tabRegistradores.TabIndex = 0;
            tabRegistradores.Text = "Registradores";
            // 
            // dgvRegistradores
            // 
            dgvRegistradores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRegistradores.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvRegistradores.Dock = DockStyle.Fill;
            dgvRegistradores.EnableHeadersVisualStyles = false;
            dgvRegistradores.GridColor = Color.Black;
            dgvRegistradores.Location = new Point(3, 3);
            dgvRegistradores.Name = "dgvRegistradores";
            dgvRegistradores.ReadOnly = true;
            dgvRegistradores.RowHeadersVisible = false;
            dgvRegistradores.Size = new Size(466, 566);
            dgvRegistradores.TabIndex = 0;
            // 
            // tabMemoriaPrograma
            // 
            tabMemoriaPrograma.BackColor = Color.FromArgb(45, 45, 48);
            tabMemoriaPrograma.Controls.Add(dgvMemoriaPrograma);
            tabMemoriaPrograma.Location = new Point(4, 24);
            tabMemoriaPrograma.Name = "tabMemoriaPrograma";
            tabMemoriaPrograma.Padding = new Padding(3);
            tabMemoriaPrograma.Size = new Size(472, 572);
            tabMemoriaPrograma.TabIndex = 1;
            tabMemoriaPrograma.Text = "Memória Programa";
            // 
            // dgvMemoriaPrograma
            // 
            dgvMemoriaPrograma.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemoriaPrograma.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvMemoriaPrograma.Dock = DockStyle.Fill;
            dgvMemoriaPrograma.EnableHeadersVisualStyles = false;
            dgvMemoriaPrograma.GridColor = Color.Black;
            dgvMemoriaPrograma.Location = new Point(3, 3);
            dgvMemoriaPrograma.Name = "dgvMemoriaPrograma";
            dgvMemoriaPrograma.ReadOnly = true;
            dgvMemoriaPrograma.RowHeadersVisible = false;
            dgvMemoriaPrograma.Size = new Size(466, 566);
            dgvMemoriaPrograma.TabIndex = 0;
            // 
            // tabMemoriaDados
            // 
            tabMemoriaDados.BackColor = Color.FromArgb(45, 45, 48);
            tabMemoriaDados.Controls.Add(dgvMemoriaDados);
            tabMemoriaDados.Location = new Point(4, 24);
            tabMemoriaDados.Name = "tabMemoriaDados";
            tabMemoriaDados.Padding = new Padding(3);
            tabMemoriaDados.Size = new Size(472, 572);
            tabMemoriaDados.TabIndex = 2;
            tabMemoriaDados.Text = "Memória Dados";
            // 
            // dgvMemoriaDados
            // 
            dgvMemoriaDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemoriaDados.BackgroundColor = Color.FromArgb(37, 37, 38);
            dgvMemoriaDados.Dock = DockStyle.Fill;
            dgvMemoriaDados.EnableHeadersVisualStyles = false;
            dgvMemoriaDados.GridColor = Color.Black;
            dgvMemoriaDados.Location = new Point(3, 3);
            dgvMemoriaDados.Name = "dgvMemoriaDados";
            dgvMemoriaDados.ReadOnly = true;
            dgvMemoriaDados.RowHeadersVisible = false;
            dgvMemoriaDados.Size = new Size(466, 566);
            dgvMemoriaDados.TabIndex = 0;
            // 
            // tabConfiguracao
            // 
            tabConfiguracao.BackColor = Color.FromArgb(45, 45, 48);
            tabConfiguracao.Controls.Add(tblConfiguracao);
            tabConfiguracao.Location = new Point(4, 24);
            tabConfiguracao.Name = "tabConfiguracao";
            tabConfiguracao.Padding = new Padding(3);
            tabConfiguracao.Size = new Size(472, 572);
            tabConfiguracao.TabIndex = 3;
            tabConfiguracao.Text = "Configuração CPU";
            // 
            // tblConfiguracao
            // 
            tblConfiguracao.ColumnCount = 2;
            tblConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblConfiguracao.Controls.Add(lblFrequencia, 0, 0);
            tblConfiguracao.Controls.Add(nudFrequencia, 1, 0);
            tblConfiguracao.Controls.Add(lblParamI, 0, 1);
            tblConfiguracao.Controls.Add(nudParamI, 1, 1);
            tblConfiguracao.Controls.Add(lblParamJ, 0, 2);
            tblConfiguracao.Controls.Add(nudParamJ, 1, 2);
            tblConfiguracao.Controls.Add(lblParamR, 0, 3);
            tblConfiguracao.Controls.Add(nudParamR, 1, 3);
            tblConfiguracao.Controls.Add(btnApplyConfig, 0, 4);
            tblConfiguracao.Dock = DockStyle.Fill;
            tblConfiguracao.Location = new Point(3, 3);
            tblConfiguracao.Name = "tblConfiguracao";
            tblConfiguracao.RowCount = 5;
            tblConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tblConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tblConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tblConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tblConfiguracao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblConfiguracao.Size = new Size(466, 566);
            tblConfiguracao.TabIndex = 0;
            // 
            // lblFrequencia
            // 
            lblFrequencia.AutoSize = true;
            lblFrequencia.Dock = DockStyle.Fill;
            lblFrequencia.ForeColor = Color.LightGray;
            lblFrequencia.Location = new Point(3, 0);
            lblFrequencia.Name = "lblFrequencia";
            lblFrequencia.Size = new Size(180, 30);
            lblFrequencia.TabIndex = 0;
            lblFrequencia.Text = "Frequência (GHz):";
            lblFrequencia.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudFrequencia
            // 
            nudFrequencia.DecimalPlaces = 2;
            nudFrequencia.Dock = DockStyle.Fill;
            nudFrequencia.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFrequencia.Location = new Point(189, 3);
            nudFrequencia.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudFrequencia.Name = "nudFrequencia";
            nudFrequencia.Size = new Size(274, 23);
            nudFrequencia.TabIndex = 1;
            nudFrequencia.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblParamI
            // 
            lblParamI.AutoSize = true;
            lblParamI.Dock = DockStyle.Fill;
            lblParamI.ForeColor = Color.LightGray;
            lblParamI.Location = new Point(3, 30);
            lblParamI.Name = "lblParamI";
            lblParamI.Size = new Size(180, 30);
            lblParamI.TabIndex = 2;
            lblParamI.Text = "Parâmetro I:";
            lblParamI.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudParamI
            // 
            nudParamI.Dock = DockStyle.Fill;
            nudParamI.Location = new Point(189, 33);
            nudParamI.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamI.Name = "nudParamI";
            nudParamI.Size = new Size(274, 23);
            nudParamI.TabIndex = 3;
            nudParamI.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblParamJ
            // 
            lblParamJ.AutoSize = true;
            lblParamJ.Dock = DockStyle.Fill;
            lblParamJ.ForeColor = Color.LightGray;
            lblParamJ.Location = new Point(3, 60);
            lblParamJ.Name = "lblParamJ";
            lblParamJ.Size = new Size(180, 30);
            lblParamJ.TabIndex = 4;
            lblParamJ.Text = "Parâmetro J:";
            lblParamJ.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudParamJ
            // 
            nudParamJ.Dock = DockStyle.Fill;
            nudParamJ.Location = new Point(189, 63);
            nudParamJ.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamJ.Name = "nudParamJ";
            nudParamJ.Size = new Size(274, 23);
            nudParamJ.TabIndex = 5;
            nudParamJ.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblParamR
            // 
            lblParamR.AutoSize = true;
            lblParamR.Dock = DockStyle.Fill;
            lblParamR.ForeColor = Color.LightGray;
            lblParamR.Location = new Point(3, 90);
            lblParamR.Name = "lblParamR";
            lblParamR.Size = new Size(180, 30);
            lblParamR.TabIndex = 6;
            lblParamR.Text = "Parâmetro R:";
            lblParamR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudParamR
            // 
            nudParamR.Dock = DockStyle.Fill;
            nudParamR.Location = new Point(189, 93);
            nudParamR.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParamR.Name = "nudParamR";
            nudParamR.Size = new Size(274, 23);
            nudParamR.TabIndex = 7;
            nudParamR.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnApplyConfig
            // 
            tblConfiguracao.SetColumnSpan(btnApplyConfig, 2);
            btnApplyConfig.Dock = DockStyle.Top;
            btnApplyConfig.Location = new Point(3, 123);
            btnApplyConfig.Name = "btnApplyConfig";
            btnApplyConfig.Size = new Size(460, 25);
            btnApplyConfig.TabIndex = 8;
            btnApplyConfig.Text = "Aplicar";
            btnApplyConfig.UseVisualStyleBackColor = true;
            btnApplyConfig.Click += btnApplyConfig_Click;
            // 
            // txtCodigoFonte
            // 
            txtCodigoFonte.BackColor = Color.FromArgb(37, 37, 38);
            txtCodigoFonte.Dock = DockStyle.Fill;
            txtCodigoFonte.Font = new Font("Consolas", 10F);
            txtCodigoFonte.ForeColor = Color.LightGray;
            txtCodigoFonte.Location = new Point(0, 200);
            txtCodigoFonte.Multiline = true;
            txtCodigoFonte.Name = "txtCodigoFonte";
            txtCodigoFonte.ScrollBars = ScrollBars.Both;
            txtCodigoFonte.Size = new Size(716, 400);
            txtCodigoFonte.TabIndex = 0;
            txtCodigoFonte.WordWrap = false;
            txtCodigoFonte.TextChanged += txtCodigoFonte_TextChanged;
            // 
            // grpStatus
            // 
            grpStatus.Controls.Add(lblTempoAtual);
            grpStatus.Controls.Add(lblTempoTotal);
            grpStatus.Controls.Add(lblPC);
            grpStatus.Dock = DockStyle.Top;
            grpStatus.ForeColor = Color.LightGray;
            grpStatus.Location = new Point(0, 136);
            grpStatus.Name = "grpStatus";
            grpStatus.Size = new Size(716, 64);
            grpStatus.TabIndex = 1;
            grpStatus.TabStop = false;
            grpStatus.Text = "Status da CPU";
            // 
            // lblTempoAtual
            // 
            lblTempoAtual.AutoSize = true;
            lblTempoAtual.Location = new Point(120, 22);
            lblTempoAtual.Name = "lblTempoAtual";
            lblTempoAtual.Size = new Size(110, 15);
            lblTempoAtual.TabIndex = 2;
            lblTempoAtual.Text = "Tempo atual em: 0s";
            // 
            // lblTempoTotal
            // 
            lblTempoTotal.AutoSize = true;
            lblTempoTotal.Location = new Point(10, 22);
            lblTempoTotal.Name = "lblTempoTotal";
            lblTempoTotal.Size = new Size(85, 15);
            lblTempoTotal.TabIndex = 0;
            lblTempoTotal.Text = "Tempo Total: 0";
            // 
            // lblPC
            // 
            lblPC.AutoSize = true;
            lblPC.Location = new Point(10, 40);
            lblPC.Name = "lblPC";
            lblPC.Size = new Size(87, 15);
            lblPC.TabIndex = 1;
            lblPC.Text = "PC: 0x00000000";
            // 
            // grpControles
            // 
            grpControles.Controls.Add(btnReset);
            grpControles.Controls.Add(btnProximoCiclo);
            grpControles.Controls.Add(btnExecutarTudo);
            grpControles.Dock = DockStyle.Top;
            grpControles.ForeColor = Color.LightGray;
            grpControles.Location = new Point(0, 76);
            grpControles.Name = "grpControles";
            grpControles.Size = new Size(716, 60);
            grpControles.TabIndex = 2;
            grpControles.TabStop = false;
            grpControles.Text = "Controles de Execução";
            // 
            // btnReset
            // 
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(260, 26);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(99, 23);
            btnReset.TabIndex = 2;
            btnReset.Text = "Resetar Tudo";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnProximoCiclo
            // 
            btnProximoCiclo.ForeColor = Color.Black;
            btnProximoCiclo.Location = new Point(10, 26);
            btnProximoCiclo.Name = "btnProximoCiclo";
            btnProximoCiclo.Size = new Size(100, 23);
            btnProximoCiclo.TabIndex = 0;
            btnProximoCiclo.Text = "Próximo Ciclo";
            btnProximoCiclo.UseVisualStyleBackColor = true;
            btnProximoCiclo.Click += btnProximoCiclo_Click;
            // 
            // btnExecutarTudo
            // 
            btnExecutarTudo.ForeColor = Color.Black;
            btnExecutarTudo.Location = new Point(120, 26);
            btnExecutarTudo.Name = "btnExecutarTudo";
            btnExecutarTudo.Size = new Size(125, 23);
            btnExecutarTudo.TabIndex = 1;
            btnExecutarTudo.Text = "Executar Até o Fim";
            btnExecutarTudo.UseVisualStyleBackColor = true;
            btnExecutarTudo.Click += btnExecutarTudo_Click;
            // 
            // grpInstrucaoAtual
            // 
            grpInstrucaoAtual.Controls.Add(txtInstrucaoHex);
            grpInstrucaoAtual.Controls.Add(txtInstrucaoBinaria);
            grpInstrucaoAtual.Dock = DockStyle.Top;
            grpInstrucaoAtual.ForeColor = Color.LightGray;
            grpInstrucaoAtual.Location = new Point(0, 0);
            grpInstrucaoAtual.Name = "grpInstrucaoAtual";
            grpInstrucaoAtual.Size = new Size(716, 76);
            grpInstrucaoAtual.TabIndex = 3;
            grpInstrucaoAtual.TabStop = false;
            grpInstrucaoAtual.Text = "Instrução Atual";
            // 
            // txtInstrucaoHex
            // 
            txtInstrucaoHex.Dock = DockStyle.Top;
            txtInstrucaoHex.Location = new Point(3, 42);
            txtInstrucaoHex.Name = "txtInstrucaoHex";
            txtInstrucaoHex.ReadOnly = true;
            txtInstrucaoHex.Size = new Size(710, 23);
            txtInstrucaoHex.TabIndex = 1;
            // 
            // txtInstrucaoBinaria
            // 
            txtInstrucaoBinaria.Dock = DockStyle.Top;
            txtInstrucaoBinaria.Location = new Point(3, 19);
            txtInstrucaoBinaria.Name = "txtInstrucaoBinaria";
            txtInstrucaoBinaria.ReadOnly = true;
            txtInstrucaoBinaria.Size = new Size(710, 23);
            txtInstrucaoBinaria.TabIndex = 0;
            // 
            // FormPrincipal
            // 
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1200, 625);
            Controls.Add(splitContainerPrincipal);
            Controls.Add(toolStripMenu);
            MinimumSize = new Size(800, 450);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulador MIPS";
            toolStripMenu.ResumeLayout(false);
            toolStripMenu.PerformLayout();
            splitContainerPrincipal.Panel1.ResumeLayout(false);
            splitContainerPrincipal.Panel2.ResumeLayout(false);
            splitContainerPrincipal.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPrincipal).EndInit();
            splitContainerPrincipal.ResumeLayout(false);
            tabControlPages.ResumeLayout(false);
            tabRegistradores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRegistradores).EndInit();
            tabMemoriaPrograma.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaPrograma).EndInit();
            tabMemoriaDados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemoriaDados).EndInit();
            tabConfiguracao.ResumeLayout(false);
            tblConfiguracao.ResumeLayout(false);
            tblConfiguracao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFrequencia).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamI).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamJ).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudParamR).EndInit();
            grpStatus.ResumeLayout(false);
            grpStatus.PerformLayout();
            grpControles.ResumeLayout(false);
            grpInstrucaoAtual.ResumeLayout(false);
            grpInstrucaoAtual.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private ToolStripButton AtivaDesativaPC;
        private Label lblTempoAtual;
        private Button btnReset;
    }
}

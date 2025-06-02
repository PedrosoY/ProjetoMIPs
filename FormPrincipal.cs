using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjetoMIPs
{
    public partial class FormPrincipal : Form
    {
        private readonly CpuMips cpu;
        private string? arquivoAtual;

        public FormPrincipal()
        {
            InitializeComponent();
            cpu = new CpuMips();

            // Bindings
            nudFrequencia.DataBindings.Add(nameof(nudFrequencia.Value), cpu, nameof(cpu.FrequenciaDecimal), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamI.DataBindings.Add(nameof(nudParamI.Value), cpu, nameof(cpu.ParametroI), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamJ.DataBindings.Add(nameof(nudParamJ.Value), cpu, nameof(cpu.ParametroJ), true, DataSourceUpdateMode.OnPropertyChanged);
            nudParamR.DataBindings.Add(nameof(nudParamR.Value), cpu, nameof(cpu.ParametroR), true, DataSourceUpdateMode.OnPropertyChanged);

            // NumericUpDown settings
            nudFrequencia.Minimum = 0.01M;
            nudFrequencia.Maximum = 50M;
            nudFrequencia.DecimalPlaces = 2;
            nudFrequencia.Increment = 0.01M;
            foreach (var nud in new[] { nudParamI, nudParamJ, nudParamR })
            {
                nud.Minimum = 0;
                nud.Maximum = 1000;
                nud.Increment = 1;
            }

            // Dark theme grids
            ConfigureGrid(dgvRegistradores, new[] { ("Numero", "Numero"), ("Registrador", "Registrador"), ("Hexadecimal", "Hexadecimal"), ("Decimal", "Decimal") });
            dgvRegistradores.DataSource = cpu.RegistradoresView;
            ConfigureGrid(dgvMemoriaPrograma, new[] { ("Endereco", "Endereco"), ("Cod32", "Cod32"), ("Instrucao", "Instrucao"), ("Operacao", "Operacao") });
            dgvMemoriaPrograma.DataSource = cpu.MemoriaProgramaView;
            ConfigureGrid(dgvMemoriaDados, new[] { ("Endereco", "Endereco"), ("Tipo", "Tipo"), ("Hexadecimal", "Hexadecimal"), ("Decimal", "Decimal") });
            dgvMemoriaDados.DataSource = cpu.MemoriaDadosView;

            cpu.PropertyChanged += Cpu_PropertyChanged;
            AtualizarInterface();
        }

        private void ConfigureGrid(DataGridView grid, (string DataProperty, string Header)[] cols)
        {
            grid.Columns.Clear(); grid.AutoGenerateColumns = false; grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = System.Drawing.Color.FromArgb(37, 37, 38);
            grid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.LightGray;
            grid.ForeColor = System.Drawing.Color.LightGray;
            grid.RowHeadersVisible = false; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var (prop, header) in cols)
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = prop, HeaderText = header, ReadOnly = true });
        }

        private void Cpu_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(cpu.ContadorPrograma) or nameof(cpu.TotalCiclos) or nameof(cpu.InstrucaoBinariaAtual) or nameof(cpu.InstrucaoHexAtual))
                UpdateStatusLabels();
        }
        private void UpdateStatusLabels()
        {
            txtInstrucaoBinaria.Text = cpu.InstrucaoBinariaAtual;
            txtInstrucaoHex.Text = cpu.InstrucaoHexAtual;
            lblPC.Text = $"PC: 0x{cpu.ContadorPrograma * 4:X8}";
            lblTempoTotal.Text = $"Tempo Total: {cpu.TotalCiclos}";
        }
        private void AtualizarInterface()
        {
            cpu.AtualizarTabelas();
            dgvRegistradores.Refresh(); dgvMemoriaPrograma.Refresh(); dgvMemoriaDados.Refresh();
            UpdateStatusLabels();
        }

        // File and control handlers omitted for brevity (unchanged)...
    }

    public class CpuMips : INotifyPropertyChanged
    {
        private readonly List<Instrucao> prog = new();
        private readonly Dictionary<string, uint> regs = new();
        private readonly Dictionary<int, byte> mem = new();
        private readonly Dictionary<string, int> labels = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        public BindingList<RegistroView> RegistradoresView { get; } = new();
        public BindingList<ProgramaView> MemoriaProgramaView { get; } = new();
        public BindingList<MemoriaView> MemoriaDadosView { get; } = new();
        private double freqGHz;
        public double FrequenciaGHz { get => freqGHz; set { freqGHz = value; OnProp(nameof(FrequenciaGHz)); OnProp(nameof(FrequenciaDecimal)); } }
        public decimal FrequenciaDecimal { get => (decimal)freqGHz; set => FrequenciaGHz = (double)value; }
        private int pI, pJ, pR;
        public int ParametroI { get => pI; set { pI = value; OnProp(nameof(ParametroI)); } }
        public int ParametroJ { get => pJ; set { pJ = value; OnProp(nameof(ParametroJ)); } }
        public int ParametroR { get => pR; set { pR = value; OnProp(nameof(ParametroR)); } }
        public int ContadorPrograma { get; private set; }
        public long TotalCiclos { get; private set; }
        public bool Parada { get; private set; }
        public string InstrucaoBinariaAtual { get; private set; } = string.Empty;
        public string InstrucaoHexAtual { get; private set; } = string.Empty;

        public CpuMips()
        {
            foreach (var n in new[] { "$zero", "$at", "$v0", "$v1", "$a0", "$a1", "$a2", "$a3", "$t0", "$t1", "$t2", "$t3", "$t4", "$t5", "$t6", "$t7", "$s0", "$s1", "$s2", "$s3", "$s4", "$s5", "$s6", "$s7", "$t8", "$t9", "$k0", "$k1", "$gp", "$sp", "$fp", "$ra" })
                regs[n] = 0;
            AtualizarTabelas();
        }

        public void CarregarPrograma(string src)
        {
            TotalCiclos = 0; ContadorPrograma = 0; Parada = false;
            prog.Clear(); labels.Clear(); mem.Clear();
            var lines = src.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(l => l.Trim()).Where(l => !l.StartsWith("//")).ToList();
            int idx = 0;
            foreach (var l in lines)
            {
                if (l.StartsWith("config_CPU")) { ParseConfig(l); continue; }
                if (l.EndsWith(":")) { labels[l.TrimEnd(':')] = idx; continue; }
                var ins = new Instrucao { TextoOriginal = l };
                ins.Decode(); prog.Add(ins); idx++;
            }
            AtualizarTabelas();
        }

        private void ParseConfig(string l)
        {
            var parts = l.Replace("config_CPU", "").Trim().Trim('[', ']').Split(',');
            foreach (var p in parts)
            {
                var kv = p.Split('=');
                if (kv.Length != 2) continue;
                var key = kv[0].Trim(); var val = kv[1].Trim().TrimEnd('G', 'H', 'z');
                switch (key)
                {
                    case var _ when key.StartsWith("20"): FrequenciaGHz = double.Parse(key.TrimEnd('G', 'H', 'z')); break;
                    case "i": ParametroI = int.Parse(val); break;
                    case "j": ParametroJ = int.Parse(val); break;
                    case "r": ParametroR = int.Parse(val); break;
                }
            }
        }

        public void ExecutarCiclo()
        {
            if (Parada || ContadorPrograma >= prog.Count) { Parada = true; return; }
            var ins = prog[ContadorPrograma];
            Execute(ins);
            ContadorPrograma++; TotalCiclos++;
            InstrucaoHexAtual = ins.HexCodigo32;
            InstrucaoBinariaAtual = ins.HexCodigo32;
            OnProp(nameof(ContadorPrograma)); OnProp(nameof(TotalCiclos));
            OnProp(nameof(InstrucaoHexAtual)); OnProp(nameof(InstrucaoBinariaAtual));
        }

        private void Execute(Instrucao ins)
        {
            var parts = ins.TextoOriginal.Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts[0])
            {
                case "li":
                    uint imm;
                    var v = parts[2];
                    if (v.StartsWith("'") && v.EndsWith("'"))
                        imm = (uint)v.Trim('\'')[0];
                    else
                        imm = uint.Parse(v);
                    regs[parts[1]] = imm;
                    break;
                case "add":
                    regs[parts[1]] = regs[parts[2]] + regs[parts[3]];
                    break;
                case "addi":
                    regs[parts[1]] = regs[parts[2]] + (uint)int.Parse(parts[3]);
                    break;
                case "sw":
                    var off1 = int.Parse(parts[2].Split('(')[0]);
                    var breg1 = parts[2].Split('(', ')')[1];
                    var addr1 = (int)regs[breg1] + off1;
                    var bytes = BitConverter.GetBytes(regs[parts[1]]);
                    for (int i = 0; i < 4; i++) mem[addr1 + i] = bytes[i];
                    break;
                case "sb":
                    var off2 = int.Parse(parts[2].Split('(')[0]);
                    var breg2 = parts[2].Split('(', ')')[1];
                    mem[(int)regs[breg2] + off2] = (byte)regs[parts[1]];
                    break;
                case "la":
                    regs[parts[1]] = (uint)int.Parse(parts[2]);
                    break;
                case "beq":
                    if (regs[parts[1]] == regs[parts[2]]) ContadorPrograma = labels[parts[3]] - 1;
                    break;
                case "j":
                    ContadorPrograma = labels[parts[1]] - 1;
                    break;
            }
        }

        public string LerStringDaMemoria(int start, int max)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < max; i++)
            {
                if (!mem.TryGetValue(start + i, out var b) || b == 0) break;
                sb.Append((char)b);
            }
            return sb.ToString();
        }

        public uint GetReg(string n) => regs.TryGetValue(n, out var v) ? v : 0;

        public void AtualizarTabelas()
        {
            RegistradoresView.Clear();
            int idx = 0;
            foreach (var kv in regs)
                RegistradoresView.Add(new RegistroView(idx++, kv.Key, $"0x{kv.Value:X8}", kv.Value));

            MemoriaProgramaView.Clear();
            for (int pc = 0; pc < prog.Count; pc++)
            {
                var ins = prog[pc];
                MemoriaProgramaView.Add(new ProgramaView($"0x{pc * 4:X8}", ins.HexCodigo32, ins.TextoOriginal, ins.Operacao));
            }

            MemoriaDadosView.Clear();
            var groups = mem.OrderBy(kv => kv.Key).GroupBy(kv => kv.Key / 4);
            foreach (var g in groups)
            {
                int addr = g.Key * 4;
                var bts = Enumerable.Range(0, 4).Select(j => mem.TryGetValue(addr + j, out var b) ? b : (byte)0).ToArray();
                uint val = BitConverter.ToUInt32(bts, 0);
                MemoriaDadosView.Add(new MemoriaView($"0x{addr:X8}", "Word", $"0x{val:X8}", val));
            }
        }

        protected void OnProp(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }

    public class RegistroView
    {
        public int Numero { get; }
        public string Registrador { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }
        public RegistroView(int n, string r, string h, uint d) => (Numero, Registrador, Hexadecimal, Decimal) = (n, r, h, d);
    }

    public class ProgramaView
    {
        public string Endereco { get; }
        public string Cod32 { get; }
        public string Instrucao { get; }
        public string Operacao { get; }
        public ProgramaView(string e, string c, string i, string o) => (Endereco, Cod32, Instrucao, Operacao) = (e, c, i, o);
    }

    public class MemoriaView
    {
        public string Endereco { get; }
        public string Tipo { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }
        public MemoriaView(string e, string t, string h, uint d) => (Endereco, Tipo, Hexadecimal, Decimal) = (e, t, h, d);
    }

    public class Instrucao
    {
        public string TextoOriginal { get; set; } = string.Empty;
        public string Operacao { get; set; } = string.Empty;
        public string HexCodigo32 { get; set; } = "0x00000000";
        public void Decode()
        {
            // Placeholder: actual encoding logic goes here
            HexCodigo32 = "0x" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}

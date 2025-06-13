using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjetoMIPs
{
    public class CpuMips : INotifyPropertyChanged
    {
        // ===== Estruturas internas =====
        private readonly List<Instrucao> prog = new(); 
        private readonly Dictionary<string, uint> regs = new(); 
        private readonly Dictionary<int, byte> mem = new(); 
        private readonly Dictionary<string, int> labels = new(); 

        public event PropertyChangedEventHandler? PropertyChanged;

        // ===== Views para DataBinding =====
        public BindingList<RegistroView> RegistradoresView { get; } = new();
        public BindingList<ProgramaView> MemoriaProgramaView { get; } = new();
        public BindingList<MemoriaView> MemoriaDadosView { get; } = new();

        // ===== Propriedades de configuração =====
        private double freqGHz;
        public double FrequenciaGHz
        {
            get => freqGHz;
            set
            {
                freqGHz = value;
                OnProp(nameof(FrequenciaGHz));
                OnProp(nameof(FrequenciaDecimal));
            }
        }
        public decimal FrequenciaDecimal
        {
            get => (decimal)freqGHz;
            set => FrequenciaGHz = (double)value;
        }

        private int pI, pJ, pR;
        public int ParametroI
        {
            get => pI;
            set
            {
                pI = value;
                OnProp(nameof(ParametroI));
            }
        }
        public int ParametroJ
        {
            get => pJ;
            set
            {
                pJ = value;
                OnProp(nameof(ParametroJ));
            }
        }
        public int ParametroR
        {
            get => pR;
            set
            {
                pR = value;
                OnProp(nameof(ParametroR));
            }
        }

        // ===== Estado de execução =====
        public int ContadorPrograma { get; private set; }
        public long TotalCiclos { get; private set; }
        public bool Parada { get; private set; }

        // ===== Instrução Atual (binário e hex) =====
        public string InstrucaoBinariaAtual { get; private set; } = string.Empty;
        public string InstrucaoHexAtual { get; private set; } = string.Empty;

        public CpuMips()
        {
            // Inicializa registradores com nomes MIPS comuns
            foreach (var nome in new[]
            {
                "$zero", "$at", "$v0", "$v1",
                "$a0", "$a1", "$a2", "$a3",
                "$t0", "$t1", "$t2", "$t3",
                "$t4", "$t5", "$t6", "$t7",
                "$s0", "$s1", "$s2", "$s3",
                "$s4", "$s5", "$s6", "$s7",
                "$t8", "$t9", "$k0", "$k1",
                "$gp", "$sp", "$fp", "$ra"
            })
            {
                regs[nome] = 0u;
            }

            AtualizarTabelas();
        }

        // ======================================================
        // Carrega o programa completo (texto) na CPU
        // ======================================================
        public void CarregarPrograma(string src)
        {
            // Reinicia estado
            TotalCiclos = 0;
            ContadorPrograma = 0;
            Parada = false;
            prog.Clear();
            labels.Clear();
            mem.Clear();

            // Quebra linhas, ignora comentários (‘//’) e linhas em branco
            var linhas = src
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !l.StartsWith("//") && l.Length > 0)
                .ToList();

            int idx = 0;
            foreach (var linha in linhas)
            {
                // Se for “config_CPU” pula (já processado na UI)
                if (linha.StartsWith("config_CPU", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                // Se terminar em “:”, é label
                if (linha.EndsWith(":"))
                {
                    var nomeLabel = linha.TrimEnd(':');
                    labels[nomeLabel] = idx;
                    continue;
                }

                // Senão, é instrução normal
                var ins = new Instrucao
                {
                    TextoOriginal = linha
                };
                ins.Decode(); // gera HexCodigo32 (semi aleatório)
                prog.Add(ins);
                idx++;
            }

            AtualizarTabelas();
        }

        // ======================================================
        // Executa um ciclo (uma única instrução)
        // ======================================================
        public void ExecutarCiclo()
        {
            // Verifica se a execução deve ser interrompida:
            // - Caso já tenha sido parada anteriormente (flag Parada)
            // - Ou se o contador do programa chegou ao fim das instruções
            if (Parada || ContadorPrograma >= prog.Count)
            {
                Parada = true;
                return;
            }

            var ins = prog[ContadorPrograma];
            Execute(ins);

            ContadorPrograma++;

            // Determina peso (ciclo) conforme tipo de instrução…
            int peso = ObterPeso(ins.Operacao);
            TotalCiclos += peso;

            // ——— NOTIFICAÇÕES PARA A UI ———
            OnProp(nameof(ContadorPrograma));
            OnProp(nameof(TotalCiclos)); 

            InstrucaoHexAtual = ins.HexCodigo32;
            InstrucaoBinariaAtual = HexParaBinario(ins.HexCodigo32);
            OnProp(nameof(InstrucaoHexAtual));
            OnProp(nameof(InstrucaoBinariaAtual));
        }

        private int ObterPeso(string op)
        {
            if (op == "nop") return ParametroR;
            var rTypes = new[] { "add", "sub", "and", "or", "sll", "srl", "slt", "sltu", "nor", "mul" };
            var iTypes = new[] { "addi", "andi", "ori", "lw", "sw", "beq", "bne", "lh", "lb", "sh", "sb", "slti", "sltiu", "li", "la" };
            var jTypes = new[] { "j", "jal", "jr" };

            if (rTypes.Contains(op)) return ParametroR;
            if (iTypes.Contains(op)) return ParametroI;
            if (jTypes.Contains(op)) return ParametroJ;
            return 1;
        }


        // ======================================================
        // Lógica de execução de cada instrução MIPS
        // ======================================================
        private void Execute(Instrucao ins)
        {
            var partes = ins.TextoOriginal
                .Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 0)
                return;

            string op = partes[0];
            switch (op)
            {
                case "li":
                    // li $t0, 5
                    // li $t1, 'A'
                    if (partes.Length >= 3)
                    {
                        uint imm;
                        var v = partes[2];
                        if (v.StartsWith("'") && v.EndsWith("'") && v.Length == 3)
                        {
                            // caractere único entre aspas simples
                            imm = (uint)v[1];
                        }
                        else
                        {
                            imm = uint.Parse(v);
                        }
                        regs[partes[1]] = imm;
                    }
                    break;

                case "add":
                    // add $t0, $t1, $t2
                    if (partes.Length >= 4)
                    {
                        regs[partes[1]] = regs[partes[2]] + regs[partes[3]];
                    }
                    break;

                case "sub":
                    // sub $t0, $t1, $t2
                    if (partes.Length >= 4)
                    {
                        regs[partes[1]] = regs[partes[2]] - regs[partes[3]];
                    }
                    break;

                case "addi":
                    // addi $t0, $t1, 10
                    // addi $t0, $t1, 0x0A
                    if (partes.Length >= 4)
                    {
                        short imm16 = partes[3].StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)
                            ? Convert.ToInt16(partes[3], 16)
                            : short.Parse(partes[3]);
                        int rsVal = (int)regs[partes[2]];
                        int result = rsVal + imm16;
                        regs[partes[1]] = (uint)result;
                    }
                    break;

                case "sw":
                    // sw $t0, 8($t1)  -> armazena 4 bytes little-endian
                    if (partes.Length >= 3)
                    {
                        uint valor = regs[partes[1]];
                        var offsetPart = partes[2];
                        var baseSplit = offsetPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string registradorBase = baseSplit[1];

                        int endereco = (int)regs[registradorBase] + offset;
                        var bytes = BitConverter.GetBytes(valor);
                        for (int i = 0; i < 4; i++)
                        {
                            mem[endereco + i] = bytes[i];
                        }
                    }
                    break;

                case "la":
                    // la $t0, 100  -> apenas atribuo o valor imediato ao registrador
                    if (partes.Length >= 3)
                    {
                        regs[partes[1]] = (uint)int.Parse(partes[2]);
                    }
                    break;

                case "beq":
                    // beq $t0, $t1, LABEL
                    if (partes.Length >= 4)
                    {
                        if (regs[partes[1]] == regs[partes[2]] && labels.ContainsKey(partes[3]))
                        {
                            ContadorPrograma = labels[partes[3]] - 1;
                        }
                    }
                    break;

                case "bne":
                    // bne $t0, $t1, LABEL
                    if (partes.Length >= 4)
                    {
                        if (regs[partes[1]] != regs[partes[2]] && labels.ContainsKey(partes[3]))
                        {
                            ContadorPrograma = labels[partes[3]] - 1;
                        }
                    }
                    break;

                case "j":
                    // j LABEL
                    if (partes.Length >= 2 && labels.ContainsKey(partes[1]))
                    {
                        ContadorPrograma = labels[partes[1]] - 1;
                    }
                    break;

                case "lw":
                    // lw $t0, 8($t1)
                    if (partes.Length >= 3)
                    {
                        var offPart = partes[2];
                        var baseSplit = offPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string regBase = baseSplit[1];

                        int endereco = (int)regs[regBase] + offset;
                        byte b0 = mem.TryGetValue(endereco + 0, out var vb0) ? vb0 : (byte)0;
                        byte b1 = mem.TryGetValue(endereco + 1, out var vb1) ? vb1 : (byte)0;
                        byte b2 = mem.TryGetValue(endereco + 2, out var vb2) ? vb2 : (byte)0;
                        byte b3 = mem.TryGetValue(endereco + 3, out var vb3) ? vb3 : (byte)0;

                        uint word = (uint)(b0 | (b1 << 8) | (b2 << 16) | (b3 << 24));
                        regs[partes[1]] = word;
                    }
                    break;

                case "lh":
                    // lh $t0, 4($t1)  -> carrega meia‐palavra assinada
                    if (partes.Length >= 3)
                    {
                        var offPart = partes[2];
                        var baseSplit = offPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string regBase = baseSplit[1];

                        int endereco = (int)regs[regBase] + offset;
                        byte b0 = mem.TryGetValue(endereco + 0, out var vb0) ? vb0 : (byte)0;
                        byte b1 = mem.TryGetValue(endereco + 1, out var vb1) ? vb1 : (byte)0;

                        short half = (short)(b0 | (b1 << 8));
                        regs[partes[1]] = (uint)half;
                    }
                    break;

                case "sh":
                    // sh $t0, 4($t1)  -> armazena meia‐palavra (2 bytes)
                    if (partes.Length >= 3)
                    {
                        var offPart = partes[2];
                        var baseSplit = offPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string regBase = baseSplit[1];

                        int endereco = (int)regs[regBase] + offset;
                        short valorHalf = (short)regs[partes[1]];
                        var bytes = BitConverter.GetBytes(valorHalf);
                        for (int i = 0; i < 2; i++)
                        {
                            mem[endereco + i] = bytes[i];
                        }
                    }
                    break;

                case "lb":
                    // lb $t0, 2($t1)  -> carrega byte assinado
                    if (partes.Length >= 3)
                    {
                        var offPart = partes[2];
                        var baseSplit = offPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string regBase = baseSplit[1];

                        int endereco = (int)regs[regBase] + offset;
                        byte b = mem.TryGetValue(endereco, out var vb) ? vb : (byte)0;
                        sbyte sb = (sbyte)b;
                        regs[partes[1]] = (uint)sb;
                    }
                    break;

                case "sb":
                    // sb $t0, 2($t1)  -> armazena apenas um byte
                    if (partes.Length >= 3)
                    {
                        var offPart = partes[2];
                        var baseSplit = offPart.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int offset = int.Parse(baseSplit[0]);
                        string regBase = baseSplit[1];

                        int endereco = (int)regs[regBase] + offset;
                        byte valor = (byte)(regs[partes[1]] & 0xFF);
                        mem[endereco] = valor;
                    }
                    break;

                case "and":
                    // and $t0, $t1, $t2
                    if (partes.Length >= 4)
                        regs[partes[1]] = regs[partes[2]] & regs[partes[3]];
                    break;

                case "or":
                    // or $t0, $t1, $t2
                    if (partes.Length >= 4)
                        regs[partes[1]] = regs[partes[2]] | regs[partes[3]];
                    break;

                case "nor":
                    // nor $t0, $t1, $t2
                    if (partes.Length >= 4)
                        regs[partes[1]] = ~(regs[partes[2]] | regs[partes[3]]);
                    break;

                case "andi":
                    // andi $t0, $t1, 0xFFFF
                    if (partes.Length >= 4)
                    {
                        uint imm = partes[3].StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)
                            ? Convert.ToUInt32(partes[3], 16)
                            : uint.Parse(partes[3]);
                        imm &= 0xFFFF;
                        regs[partes[1]] = regs[partes[2]] & imm;
                    }
                    break;

                case "ori":
                    // ori $t0, $t1, 0xFFFF
                    if (partes.Length >= 4)
                    {
                        uint imm = partes[3].StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)
                            ? Convert.ToUInt32(partes[3], 16)
                            : uint.Parse(partes[3]);
                        imm &= 0xFFFF;
                        regs[partes[1]] = regs[partes[2]] | imm;
                    }
                    break;

                case "sll":
                    // sll $t0, $t1, 2  
                    if (partes.Length >= 4)
                    {
                        uint valor = regs[partes[2]];
                        int shamt = int.Parse(partes[3]) & 0x1F;
                        regs[partes[1]] = valor << shamt;
                    }
                    break;

                case "srl":
                    // srl $t0, $t1, 2
                    if (partes.Length >= 4)
                    {
                        uint valor = regs[partes[2]];
                        int shamt = int.Parse(partes[3]) & 0x1F;
                        regs[partes[1]] = valor >> shamt;
                    }
                    break;

                case "slt":
                    // slt $t0, $t1, $t2
                    // Define $t0 como 1 se o valor com sinal de $t1 for menor que o de $t2; senão, 0
                    if (partes.Length >= 4)
                        regs[partes[1]] =
                            (uint)(((int)regs[partes[2]] < (int)regs[partes[3]]) ? 1 : 0);
                    break;

                case "sltu":
                    // sltu $t0, $t1, $t2
                    // Define $t0 como 1 se o valor sem sinal de $t1 for menor que o de $t2; senão, 0
                    if (partes.Length >= 4)
                        regs[partes[1]] =
                            (uint)((regs[partes[2]] < regs[partes[3]]) ? 1 : 0);
                    break;

                case "slti":
                    // slti $t0, $t1, 5
                    // Define $t0 como 1 se o valor com sinal de $t1 for menor que o imediato; senão, 0
                    if (partes.Length >= 4)
                        regs[partes[1]] =
                            (uint)(((int)regs[partes[2]] < int.Parse(partes[3])) ? 1 : 0);
                    break;

                case "sltiu":
                    // sltiu $t0, $t1, 5
                    // Define $t0 como 1 se o valor sem sinal de $t1 for menor que o imediato; senão, 0
                    if (partes.Length >= 4)
                        regs[partes[1]] =
                            (uint)((regs[partes[2]] < uint.Parse(partes[3])) ? 1 : 0);
                    break;

                case "jr":
                    // jr $ra
                    if (partes.Length >= 2)
                        ContadorPrograma = (int)regs[partes[1]];
                    break;

                case "jal":
                    // jal LABEL
                    if (partes.Length >= 2 && labels.ContainsKey(partes[1]))
                    {
                        regs["$ra"] = (uint)(ContadorPrograma + 1);
                        ContadorPrograma = labels[partes[1]];
                    }
                    break;

                case "mul":
                    // mul $t0, $t1, $t2
                    if (partes.Length >= 4)
                    {
                        regs[partes[1]] = regs[partes[2]] * regs[partes[3]];
                    }
                    break;

                case "nop":
                    break;

                default:
                    // Se não for nenhuma das instruções acima, 
                    // Não vai fazer nada alem de exibir :)
                    MessageBox.Show($"Instrução não conhecida {partes[0]}");
                    break;
            }
        }

        // ======================================================
        // Atualiza as BindingList<> para DataGridViews
        // ======================================================
        public void AtualizarTabelas()
        {
            // ==== Registradores ====
            RegistradoresView.Clear();
            int idx = 0;
            foreach (var kv in regs)
            {
                RegistradoresView.Add(new RegistroView(
                    idx++, kv.Key,
                    $"0x{kv.Value:X8}",
                    kv.Value));
            }

            // ==== Memória de Programa ====
            MemoriaProgramaView.Clear();
            for (int pc = 0; pc < prog.Count; pc++)
            {
                var ins = prog[pc];
                MemoriaProgramaView.Add(new ProgramaView(
                    $"0x{pc * 4:X8}",
                    ins.HexCodigo32,
                    ins.TextoOriginal,
                    ins.Operacao));
            }

            // ==== Memória de Dados (agrupa em words de 4 bytes) ====
            MemoriaDadosView.Clear();
            var agrup = mem.OrderBy(kv => kv.Key)
                          .GroupBy(kv => kv.Key / 4);

            foreach (var g in agrup)
            {
                int enderecoWord = g.Key * 4;
                byte[] bts = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    bts[i] = mem.TryGetValue(enderecoWord + i, out var vb) ? vb : (byte)0;
                }
                uint val = BitConverter.ToUInt32(bts, 0);
                MemoriaDadosView.Add(new MemoriaView(
                    $"0x{enderecoWord:X8}",
                    "Word",
                    $"0x{val:X8}",
                    val));
            }
        }

        // ======================================================
        // converte string hex (“0xAABBCCDD”) para os 32 bits em binário
        // ======================================================
        private string HexParaBinario(string hex)
        {
            if (!hex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;

            try
            {
                string sem0x = hex.Substring(2);
                uint value = Convert.ToUInt32(sem0x, 16);
                return Convert.ToString(value, 2).PadLeft(32, '0');
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void OnProp(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        // Retorna quantas instruções (linhas) foram carregadas no programa.
        public int MemoriaProgramaCount => prog.Count;
    }

    // ======================================================
    // Classes de _ViewModel_ para cada DataGridView
    // ======================================================
    public class RegistroView
    {
        public int Numero { get; }
        public string Registrador { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }

        public RegistroView(int n, string r, string h, uint d) =>
            (Numero, Registrador, Hexadecimal, Decimal) = (n, r, h, d);
    }

    public class ProgramaView
    {
        public string Endereco { get; }
        public string Cod32 { get; }
        public string Instrucao { get; }
        public string Operacao { get; }

        public ProgramaView(string e, string c, string i, string o) =>
            (Endereco, Cod32, Instrucao, Operacao) = (e, c, i, o);
    }

    public class MemoriaView
    {
        public string Endereco { get; }
        public string Tipo { get; }
        public string Hexadecimal { get; }
        public uint Decimal { get; }

        public MemoriaView(string e, string t, string h, uint d) =>
            (Endereco, Tipo, Hexadecimal, Decimal) = (e, t, h, d);
    }

    // ======================================================
    // Classe Instrucao: decodifica texto original e gera um “HexCodigo32”
    // ======================================================
    public class Instrucao
    {
        public string TextoOriginal { get; set; } = string.Empty;
        public string Operacao { get; set; } = string.Empty;
        public string HexCodigo32 { get; set; } = "0x00000000";

        public void Decode()
        {
            // Gera um valor pseudo‐aleatório de 32 bits:
            Operacao = TextoOriginal.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .FirstOrDefault() ?? string.Empty;

            // Gera 8 dígitos hex aleatórios
            var randomHex = Guid.NewGuid().ToString("N").Substring(0, 8);
            HexCodigo32 = $"0x{randomHex}";
        }

    }
}

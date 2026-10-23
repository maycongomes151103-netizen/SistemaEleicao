using SistemaEleicao.Console.Models;

namespace SistemaEleicao.Console.Services;

public class ResultadoEleicao
{
    public IReadOnlyList<Candidato> Candidatos { get; set; } = new List<Candidato>();
    public int VotosBrancos { get; set; }
    public int VotosNulos { get; set; }
    public int TotalVotos { get; set; }
    public List<Candidato> Vencedores { get; set; } = new();
}

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly HashSet<string> _eleitoresQueVotaram = new(StringComparer.OrdinalIgnoreCase);

    public int VotosBrancos { get; private set; }
    public int VotosNulos { get; private set; }

    public IReadOnlyList<Candidato> Candidatos => _candidatos.AsReadOnly();

    public bool CadastrarCandidato(int numero, string nome)
    {
        if (numero <= 0 || _candidatos.Any(c => c.Numero == numero))
        {
            return false;
        }

        _candidatos.Add(new Candidato(numero, nome));
        return true;
    }

    public bool EleitorJaVotou(string documentoEleitor)
    {
        if (string.IsNullOrWhiteSpace(documentoEleitor)) return false;
        return _eleitoresQueVotaram.Contains(documentoEleitor.Trim());
    }

    public bool RegistrarVoto(string documentoEleitor, int numeroVoto)
    {
        if (string.IsNullOrWhiteSpace(documentoEleitor))
            throw new ArgumentException("O documento do eleitor é obrigatório.");

        var docLimpo = documentoEleitor.Trim();

        if (_eleitoresQueVotaram.Contains(docLimpo))
        {
            return false;
        }

        if (numeroVoto == 0)
        {
            VotosBrancos++;
        }
        else
        {
            var candidato = _candidatos.FirstOrDefault(c => c.Numero == numeroVoto);
            if (candidato != null)
            {
                candidato.AdicionarVoto();
            }
            else
            {
                VotosNulos++;
            }
        }

        _eleitoresQueVotaram.Add(docLimpo);
        return true;
    }

    public ResultadoEleicao ObterApuracao()
    {
        var totalVotos = _candidatos.Sum(c => c.Votos) + VotosBrancos + VotosNulos;
        var maxVotos = _candidatos.Any() ? _candidatos.Max(c => c.Votos) : 0;

        List<Candidato> vencedores = new();
        if (maxVotos > 0)
        {
            vencedores = _candidatos.Where(c => c.Votos == maxVotos).ToList();
        }

        return new ResultadoEleicao
        {
            Candidatos = Candidatos,
            VotosBrancos = VotosBrancos,
            VotosNulos = VotosNulos,
            TotalVotos = totalVotos,
            Vencedores = vencedores
        };
    }
}
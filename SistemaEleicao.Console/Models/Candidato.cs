namespace SistemaEleicao.Console.Models;

public class Candidato
{
    public int Numero { get; private set; }
    public string Nome { get; private set; }
    public int Votos { get; private set; }

    public Candidato(int numero, string nome)
    {
        if (numero <= 0)
            throw new ArgumentException("O número do candidato deve ser um valor positivo.", nameof(numero));

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do candidato não pode ser vazio.", nameof(nome));

        Numero = numero;
        Nome = nome.Trim();
        Votos = 0;
    }

    public void AdicionarVoto()
    {
        Votos++;
    }
}
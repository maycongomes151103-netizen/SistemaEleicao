# 🗳️ Sistema de Eleição Digital (.NET 8 / xUnit)

Projeto desenvolvido para a disciplina de **Gestão e Qualidade de Software** / **Garantia da Qualidade de Software**.
**Professor:** Daniel Henrique Matos de Paiva

---

## 📌 Sobre o Projeto
Aplicação de console em C# (.NET) projetada para gerenciar um processo eleitoral simplificado, garantindo a integridade e validação dos dados de votação.

### Funcionalidades
1. **Cadastro de Candidatos:** Permite cadastrar candidatos garantindo a unicidade do número (validação contra duplicados).
2. **Controle de Eleitores:** Controle por CPF/Título de eleitor que impede votos duplicados do mesmo eleitor.
3. **Votação:** Permite votar por número do candidato, votar em **Branco** (`0`) ou registrar votos **Nulos** (número inexistente).
4. **Apuração de Votos:** Relatório completo exibindo o total de votos, votos por candidato, nulos, brancos e o candidato vencedor (ou empate).

---

## 🛠️ Tecnologias Utilizadas
- **Linguagem:** C# (.NET 8)
- **Tipo de Projeto:** Console Application & xUnit Test Project
- **Framework de Testes:** xUnit
- **Padrão de Commits:** Conventional Commits (`feat`, `fix`, `test`, `refactor`, `docs`, `chore`)

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.

### 1. Clonar o Repositório
```bash
git clone <URL_DO_SEU_REPOSITORIO_GITHUB>
cd SistemaEleicao
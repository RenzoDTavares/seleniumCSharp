# Selenium Project

Este repositório contém um projeto de testes automatizados usando Selenium e NUnit. A aplicação web testada é um sistema simples de CRUD disponível em [https://renzodtavares.github.io/simpleCRUD/main.html](https://renzodtavares.github.io/simpleCRUD/main.html). O projeto utiliza o Selenium WebDriver para interagir com a aplicação e o NUnit para a estrutura de testes.

## Funcionalidades

- **Inserir Item:** Adiciona um novo item à tabela e valida se foi adicionado com sucesso.
- **Editar Item:** Edita um item existente e valida se as alterações foram aplicadas corretamente.
- **Excluir Item Aleatório:** Remove um item aleatório da tabela e valida se a exclusão foi bem-sucedida.
- **Incluir com Validação de Duplicata:** Adiciona um item com um nome já existente e verifica se a mensagem de erro de duplicata é exibida.

## Configuração

### Dependências

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Selenium WebDriver](https://www.selenium.dev/)
- [NUnit](https://nunit.org/)
- [Xunit](https://xunit.net/)

### Executando os Testes

1. **Clone este repositório:**

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git

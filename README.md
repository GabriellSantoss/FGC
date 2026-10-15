# 🎮 FIAP Cloud Games (FCG)

Projeto acadêmico desenvolvido durante a pós-graduação em **Arquitetura de Software com .NET e Azure — FIAP**, com foco na construção de uma aplicação backend utilizando arquitetura de microsserviços.

O projeto simula uma plataforma de jogos digitais, permitindo o gerenciamento de usuários, catálogo de jogos e processamento de pagamentos.

## 🏗️ Arquitetura

A aplicação foi organizada em três microsserviços independentes, com um API Gateway responsável pelo roteamento das requisições.

### Microsserviços

**UsuarioService**
- Cadastro e gerenciamento de usuários
- Autenticação com JWT
- Gerenciamento de perfis e permissões
- Persistência de dados com Entity Framework Core

**JogoService**
- Cadastro e gerenciamento de jogos
- Organização por categorias
- Informações de preços, fornecedores e avaliações
- Persistência em banco de dados SQL Server

**PagamentoService**
- Registro de pagamentos
- Integração com RabbitMQ
- Processamento assíncrono de mensagens
- Atualização do status dos pagamentos

**API Gateway**
- Roteamento centralizado das requisições
- Utilização do YARP (Yet Another Reverse Proxy)
- Comunicação com os microsserviços

## 🛠️ Tecnologias utilizadas

- C# e .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token)
- RabbitMQ
- YARP API Gateway
- Docker e Docker Compose
- Kubernetes
- Git e GitHub

## 📂 Estrutura do projeto

```text
FGC/
├── ApiGateway/
├── UsuarioService (FGC)/
├── JogoService/
├── PagamentoService/
├── kubernetes/
│   ├── apigateway.yaml
│   ├── configmap.yaml
│   ├── jogoservice.yaml
│   ├── pagamentoservice.yaml
│   ├── rabbitmq.yaml
│   ├── secret.example.yaml
│   ├── sqlserver.yaml
│   └── usuarioservice.yaml
├── docker-compose.yml
└── FGC.sln
```

*Estrutura ilustrativa dos principais componentes. Alguns diretórios e arquivos podem ter nomes diferentes no repositório.*

## 🔄 Fluxo de pagamento

1. O cliente realiza uma requisição para registrar um pagamento.
2. O PagamentoService registra a operação.
3. Uma mensagem é publicada no RabbitMQ.
4. O consumidor processa a mensagem de forma assíncrona.
5. O status do pagamento é atualizado.

Esse fluxo demonstra a utilização de mensageria para desacoplar etapas do processamento.

## 🐳 Docker e Kubernetes

A infraestrutura foi configurada para execução dos serviços em containers.

O projeto inclui:

- Docker Compose para execução local
- Manifests Kubernetes para Deployments e Services
- ConfigMaps para configurações de ambiente
- Secrets para informações sensíveis
- PersistentVolumeClaims para persistência de dados
- Health checks para acompanhamento dos serviços

Os arquivos de exemplo não devem conter credenciais reais.

## 🧪 Validações realizadas

Durante o desenvolvimento, foram realizados testes manuais envolvendo:

- Autenticação e utilização de tokens JWT
- Acesso aos endpoints pelo API Gateway
- Comunicação entre os microsserviços
- Publicação e consumo de mensagens no RabbitMQ
- Atualização do status de pagamentos
- Verificação de endpoints de saúde

## 🎯 Objetivo do projeto

Aplicar conceitos de arquitetura de software em um cenário prático, explorando separação de responsabilidades, comunicação entre serviços, autenticação, mensageria, conteinerização e orquestração.

## 👨‍💻 Autor

Gabriel Aljarila dos Santos

Projeto desenvolvido para fins acadêmicos e de aprimoramento profissional.

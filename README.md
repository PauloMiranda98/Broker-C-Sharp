# Alerta de cotação de ações usando C#

O objetivo deste projeto é desenvolver um programa que deve ficar continuamente monitorando a cotação do ativo enquanto estiver rodando.

Toda vez que o preço for maior que um limite especificado, um e-mail deve ser disparado aconselhando a venda.

Toda vez que o preço for menor que outro limite especificado, um e-mail deve ser disparado aconselhando a compra.

## Ferramentas utilizadas
Para acompanhar o preço das ações, foi utilizado a API do site [HG Brasil](https://hgbrasil.com/).

Para enviar o email usando SMTP, foi utilizado o serviço de email do site [SendGrid](https://sendgrid.com/).

## Configurando o Projeto

Para configurar o projeto, basta criar um arquivo chamada `config.yml` na pasta `/BrokerCS`, como o seguinte conteúdo:
```yaml
SmtpDomain: 
SmtpPort: 
SmtpUsername: 
SmtpPassword: 
FromEmail: 
ToEmail: 
HGbrasilKey: 
```

## Executando
Ao executar o programa, é necessário passar 3 parâmetros:

- O ativo a ser monitorado
- O preço de referência para venda
- O preço de referência para compra

Exemplo: `stock-quote-alert.exe PETR4 22.67 22.59 `
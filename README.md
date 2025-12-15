# 🏦 Pix Ledger 
#### Core Bancário Imutável / Immutable Banking Core

<img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)"></img> ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)

[🇺🇸 English](#-english) | [🇧🇷 Português](#-português)

---

## 🇺🇸 English

### About the Project
**Pix Ledger** is a high-integrity banking core simulation designed to handle financial transactions with cryptographic security. Unlike traditional ledgers, this project implements a **per-account Blockchain structure**.

Every transaction generates a cryptographic hash based on its data (Amount, Timestamp, ID) and the hash of the previous transaction. This ensures:
1.  **Immutability:** History cannot be rewritten.
2.  **Fraud Detection:** Any manual alteration in the database breaks the hash chain, making it detectable by the audit system.
3.  **Double-Entry Consistency:** Transfers are atomic operations creating strictly coupled Debit and Credit records.

### Tech Stack
* **Core:** C# / .NET 10
* **Database:** PostgreSQL
* **Architecture:** Clean Architecture & Domain-Driven Design (DDD)
* **Containerization:** Docker & Docker Compose.

### Key Features
* **Immutable Ledger:** Uses SHA-256 chaining to guarantee data integrity.
* **Double-Entry Bookkeeping:** Ensures mathematical consistency.
* **Optimistic Locking:** Prevents race conditions during simultaneous transactions.
* **Auditability:** Built-in mechanism to traverse and verify the entire transaction chain.

### How to Run

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/LXSCA7/pix-ledger.git](https://github.com/LXSCA7/pix-ledger.git)
    cd pix-ledger
    ```

2.  **Start Infrastructure (Postgres):**
    ```bash
    docker compose up -d
    ```

3.  **Apply Migrations:**
    ```bash
    dotnet ef database update --project src/PixLedger.Infrastructure --startup-project src/PixLedger.Api
    ```

4.  **Run the API:**
    ```bash
    dotnet run --project src/PixLedger.Api
    ```

---

## 🇧🇷 Português

### Sobre o Projeto
**Pix Ledger** é uma simulação de core bancário de alta integridade projetado para processar transações financeiras com segurança criptográfica. Diferente de ledgers tradicionais, este projeto implementa uma estrutura de Blockchain por conta.

Cada transação gera um hash criptográfico baseado em seus dados (Valor, Timestamp, ID) e no hash da transação anterior. Isso garante:
1.  **Imutabilidade:** O histórico não pode ser reescrito.
2.  **Detecção de Fraude:** Qualquer alteração manual no banco de dados quebra a corrente de hash, sendo detectada pelo sistema de auditoria.
3.  **Consistência de Dupla Entrada:** Transferências são operações atômicas que criam registros de Débito e Crédito estritamente acoplados.

### Tecnologias
* **Core:** C# / .NET 10
* **Banco de Dados:** PostgreSQL
* **Arquitetura:** Clean Architecture & Domain-Driven Design (DDD)
* **Containerização:** Docker & Docker Compose.

### Funcionalidades Chave
* **Ledger Imutável:** Uso de encadeamento SHA-256 para garantir integridade.
* **Contabilidade de Dupla Entrada:** Garante consistência matemática nas transações.
* **Travas Otimistas:** Previne condições de corrida em transações simultâneas.
* **Auditabilidade:** Mecanismo nativo para reconstruir e verificar toda a cadeia de transações.

### Como Rodar

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/LXSCA7/pix-ledger.git](https://github.com/LXSCA7/pix-ledger.git)
    cd pix-ledger
    ```

2.  **Suba a Infraestrutura (Postgres):**
    ```bash
    docker compose up -d
    ```

3.  **Aplique as migrations:**
    ```bash
    dotnet ef database update --project src/PixLedger.Infrastructure --startup-project src/PixLedger.Api
    ```

4.  **Rode a API:**
    ```bash
    dotnet run --project src/PixLedger.Api
    ```
// ================ FUNÇÕES CRUD =================


//funcao listar que busca todos os dispositivos cadastrados na api e preenche a tabela no html  da pagina
async function listar() {
     // Seleciona o corpo da tabela onde os dados serão inseridos.
    // querySelector("#tabela tbody") pega o <tbody> dentro de uma <table id="tabela">
    const tabela = document.querySelector("#tabela tbody");
    tabela.innerHTML = "";
    
    // Limpa o conteúdo atual da tabela, garantindo que uma nova listagem
    // não empilhe dados anteriores.


    // Faz uma requisição GET para a rota /dispositivos.
    // Essa rota deve retornar um JSON com a lista completa de dispositivos.
    
    const resp = await fetch("/dispositivos");
    const dados = await resp.json();
     // Converte a resposta para JSON.
    // "dados" será um array de objetos Dispositivo.
    


    // Percorre cada dispositivo retornado
    dados.forEach(d => {

        let tipoTexto;
        if (d.tipo === 0 && d.tipoPersonalizado) {
            tipoTexto = d.tipoPersonalizado;
        } else {
            switch (d.tipo) {
                case 1:
                    tipoTexto = "Máquina de Vendas";
                    break;
                case 2:
                    tipoTexto = "Máquina de Chicletes";
                    break;
                case 3:
                    tipoTexto = "Lavanderia Autônoma";
                    break;
                default:
                    tipoTexto = d.tipo; // fallback
                  // Fallback: exibe o valor numérico caso não reconheça.
                    // Isso evita que o sistema quebre em caso de novos tipos
                    // adicionados no banco antes da atualização do frontend.

                 // IMPORTANTE:
                // Se você adicionar novos tipos na aplicação
                // (ex: tipo 4 = Impressora 3D), adicione aqui:
                //
                // case 4:
                //     tipoTexto = "Impressora 3D";
                //     break;

            }
        }


        // ====================================================
        // Inserção da linha na tabela
        // ----------------------------------------------------
        // Aqui montamos o HTML que representa uma linha completa
        // da tabela com os dados do dispositivo.
        //
        // OBS:
        // Caso você adicione novos campos no model Dispositivo
        // (como "ManutencaoMensal", "DataCadastro", etc.)
        // será necessário:
        // 1) incluir novas colunas na tabela HTML;
        // 2) incluir as células correspondentes aqui.
        // ====================================================
        tabela.innerHTML += `
            <tr>
                <td>${d.id}</td>
                <td>${d.fabricante}</td>
                <td>${d.empresa}</td>
                <td>${d.local}</td>
                <td>${tipoTexto}</td>
                <td>${d.ativo}</td>
                <td>${d.lucro ?? ""}</td>
                <td>${d.custoDeOperacao ?? ""}</td> 
            </tr>
        `;
    });
}

async function listarDespesas() {
    const tabela = document.querySelector("#tabelaDeDespesas tbody");
    tabela.innerHTML = "";
    const resp = await fetch("/despesas");
    const dados = await resp.json();
    dados.forEach(d => {
        tabela.innerHTML += `
            <tr>
                <td>${d.id}</td>
                <td>${d.nome}</td>
                <td>${d.data}</td>
                <td>${d.categoria}</td>
                <td>${d.valor}</td>
                <td>${d.imposto}</td>
            </tr>
        `;
    });
}
async function relatorio() {
    const tabela = document.querySelector("#relatorio tbody");
    tabela.innerHTML = "";

    const resp = await fetch("/relatorio");
    const dados = await resp.json();
    
   
        tabela.innerHTML += `
            <tr>
                <td>${dados.totalDispositivos}</td>
                <td>${dados.totalDespesas}</td>
                <td>${dados.balancoGeral}</td>
            </tr>
        `;
}

//?? é pra não retornar null

//Se amanhã você adicionar no C#:

//public decimal ManutencaoMensal { get; set; }


//Você precisa adicionar no JS:

//<td>${d.manutencaoMensal ?? ""}</td>


//E no HTML:

//<th>Manutenção</th>



function mostrarOutroTipo(select) {
    const inputOutro = document.getElementById("cTipoOutro");

    if (select.value === "outro") {
        inputOutro.style.display = "block";
        inputOutro.required = true;
    } else {
        inputOutro.style.display = "none";
        inputOutro.required = false;
        inputOutro.value = "";
    }
}

/**
 * ================================================================
 * FUNÇÃO: mostrarOutroTipo(select)
 * ------------------------------------------------
 * Objetivo:
 * - Controlar a exibição do campo de texto "Outro Tipo".
 * - Isso é usado quando o usuário escolhe "outro" no <select> de tipos
 *   (em vez dos tipos padrão como máquina de vendas, chicletes etc.).
 *
 * Como funciona:
 * - O parâmetro 'select' representa o próprio elemento <select>
 *   que chamou esta função via onchange.
 * - Se o valor escolhido for "outro", então o input extra é exibido.
 * - Caso contrário, o input é escondido e desobrigado.
 *
 * Importante:
 * - Quando o select NÃO está em "outro", sempre limpamos o input
 *   para evitar dados residuais.
 *
 * Quando modificar:
 * - Se novos tipos forem adicionados e a lógica mudar,
 *   basta alterar o valor comparado no IF.
 * - Se criar mais campos condicionais, usar esse mesmo padrão.
 * ================================================================
 */

async function criar(e) {
    e.preventDefault();

    let tipoSelecionado = cTipo.value;
    let tipoFinal;
    let tipoPersonalizado = null;

    if (tipoSelecionado === "outro") {
        // no enum: Outro = 0
        tipoFinal = 0;
        tipoPersonalizado = cTipoOutro.value; // ex: "Máquina de Sorvete"
    } else {
        tipoFinal = Number(tipoSelecionado); // 1, 2 ou 3
    }

    const novo = {
        fabricante: cFabricante.value,
        empresa: cEmpresa.value,
        local: cLocal.value,
        tipo: tipoFinal,                 // enum no back
        tipoPersonalizado: tipoPersonalizado, // string ou null
        ativo: cAtivo.value === "true",
        lucro: Number(cLucro.value),
        custoDeOperacao: Number(cCusto.value)
    };

    


    await fetch("/dispositivos", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novo)
    });

    alert("Criado com sucesso!");
    listar();
}


async function atualizar(e) {
    e.preventDefault();

    const id = uId.value;

    const atualizado = {
        fabricante: uFabricante.value,
        empresa: uEmpresa.value,
        local: uLocal.value,
        tipo: uTipo.value ? Number(uTipo.value) : 0,
        ativo: uAtivo.value === "true",
        lucro: uLucro.value ? Number(uLucro.value) : 0,
        custoDeOperacao: uCusto.value ? Number(uCusto.value) : 0
    };

    const resp = await fetch(`/dispositivos/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(atualizado)
    });

    if (resp.ok) alert("Atualizado com sucesso!");
    else alert("ID não encontrado!");

    listar();
}

async function deletar(e) {
    e.preventDefault();

    const id = dId.value;

    const resp = await fetch(`/dispositivos/${id}`, {
        method: "DELETE"
    });

    if (resp.status === 204) alert("Deletado!");
    else alert("ID não encontrado!");

    listar();
}

// ================ DROPDOWN DOS CARDS =================

function configurarDropdownCards() {
    const headers = document.querySelectorAll(".card-header");

    headers.forEach(header => {
        header.addEventListener("click", () => {
            const body = header.nextElementSibling;
            const aberto = body.style.display === "block";
            body.style.display = aberto ? "none" : "block";
        });
    });
}

// ================ INICIALIZAÇÃO =================

window.addEventListener("DOMContentLoaded", () => {
    configurarDropdownCards();
    listar(); // carrega a tabela automaticamente
});

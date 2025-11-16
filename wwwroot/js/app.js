// ================ FUNÇÕES CRUD =================

async function listar() {
    const tabela = document.querySelector("#tabela tbody");
    tabela.innerHTML = "";

    const resp = await fetch("/dispositivos");
    const dados = await resp.json();

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
            }
        }

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

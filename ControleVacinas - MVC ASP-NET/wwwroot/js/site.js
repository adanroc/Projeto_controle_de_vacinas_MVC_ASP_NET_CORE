// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Método 1 - fonte site JQuery DataTable - (letras em inglês)
//$(document).ready(function () {
//    $('#TabelaListaDeCadastros').DataTable();
//});

//Método 2 - fonte site JQuery DataTable
$(document).ready(function () {

    // Traduzir Jquery DataTable
    getDatatable('#TabelaListaDeCadastros');
    getDatatable('#TabelaListaDeUsuarios');

    // Exibir modal
    $('.btn-total-cadastros').click(function () {   
        var usuarioId = $(this).attr('usuario-id');
        //console.log(usuarioId);

        //Requisição Ajax
        $.ajax({
            type:'GET',
            url: '/ListaDeUsuarios/ListarCadastrosPorUsuarioId/' + usuarioId,
            success: function (result) {
                $("#listaDeCadastrosDoUsuario").html(result);                
                $('#modalListaDeCadastrosDoUsuario').modal('show');
                getDatatable('#TabelaListaDeCadastrosDoUsuario');
            }
        });        
    });  

    // Fecha o alerta ao clicar no botão "x"
    $('.close-alert').click(function () {
        $('#alert-box').addClass('fade-out').delay(500).queue(function () {
            $(this).remove();
        });
    });

    // Fecha o alerta ao clicar fora da mensagem
    $(document).mouseup(function (e) {
        var container = $('#alert-box');
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            container.addClass('fade-out').delay(500).queue(function () {
                $(this).remove();
            });
        }
    });

    // Exibe o conteúdo da tabela elipsado ao passar o mouse sobre o conteúdo:
    // Restrict a column to 17 characters, don't split words
    $('#TabelaListaDeCadastros').DataTable({
        columnDefs: [{
            targets: 1,
            render: DataTable.render.ellipsis(17, true)
        }]
    });

});

 //Tradução git-hub Acaciano Neves - Programador Tech (função de paginação desativada junto com sua legenda)
function getDatatable(id)
{
    $(id).DataTable({
        "ordering": true, // Ordenação habilitada
        "paging": false, // Paginação habilitada
        "searching": true, //pesquisa habilitada
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "",
            "sInfoEmpty": "",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });  
}

//function getDatatable(id) {
//    $(id).DataTable({
//        "ordering": true, // Ordenação habilitada
//        "paging": false, // Paginação habilitada
//        "searching": true, //pesquisa habilitada
//        "oLanguage": {
//            "sEmptyTable": "Nenhum registro encontrado na tabela",
//            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
//            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
//            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
//            "sInfoPostFix": "",
//            "sInfoThousands": ".",
//            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
//            "sLoadingRecords": "Carregando...",
//            "sProcessing": "Processando...",
//            "sZeroRecords": "Nenhum registro encontrado",
//            "sSearch": "Pesquisar",
//            "oPaginate": {
//                "sNext": "Proximo",
//                "sPrevious": "Anterior",
//                "sFirst": "Primeiro",
//                "sLast": "Ultimo"
//            },
//            "oAria": {
//                "sSortAscending": ": Ordenar colunas de forma ascendente",
//                "sSortDescending": ": Ordenar colunas de forma descendente"
//            }
//        }
//    });
//}

//function getDatatable(id) {
//    $(id).DataTable({
//        "ordering": true, // Ordenação habilitada
//        "paging": true, // Paginação habilitada
//        "searching": true, //pesquisa habilitada
//        "oLanguage": {
//            "sEmptyTable": "Nenhum registro encontrado na tabela",
//            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
//            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
//            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
//            "sInfoPostFix": "",
//            "sInfoThousands": ".",
//            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
//            "sLoadingRecords": "Carregando...",
//            "sProcessing": "Processando...",
//            "sZeroRecords": "Nenhum registro encontrado",
//            "sSearch": "Pesquisar",
//            "oPaginate": {
//                "sNext": "Proximo",
//                "sPrevious": "Anterior",
//                "sFirst": "Primeiro",
//                "sLast": "Ultimo"
//            },
//            "oAria": {
//                "sSortAscending": ": Ordenar colunas de forma ascendente",
//                "sSortDescending": ": Ordenar colunas de forma descendente"
//            }
//        }
//    });
//}


//$('.close-alert').click(function () {
//    $('.alert').hide('hide');
//});



// Exibe legenda ao passar o mouse instaneamente (sem atraso) (em teste)
document.addEventListener("DOMContentLoaded", function () {
    var input = document.getElementById("myInput");

    // Criação do tooltip
    var tooltip = document.createElement("div");
    tooltip.classList.add("custom-tooltip");
    document.body.appendChild(tooltip);

    // Quando o mouse entra no campo
    input.addEventListener("mouseenter", function () {
        tooltip.innerText = input.title; // Coloca o conteúdo do title
        var rect = input.getBoundingClientRect(); // Obtém a posição do input

        // Posiciona o tooltip
        tooltip.style.left = `${rect.left + window.scrollX}px`;
        tooltip.style.top = `${rect.top + window.scrollY - 30}px`; // Ajuste para cima

        // Torna o tooltip visível instantaneamente
        tooltip.style.opacity = 1;
    });

    // Quando o mouse sai do campo
    input.addEventListener("mouseleave", function () {
        tooltip.style.opacity = 0; // Faz o tooltip desaparecer
    });
});

// Estilo para o tooltip
// Exibir a legenda do texto e permitir selecionar e copiar o texto: 
document.addEventListener("DOMContentLoaded", function () {
    const tableCells = document.querySelectorAll("#TabelaListaDeCadastros td");

    tableCells.forEach(cell => {
        cell.addEventListener("mouseenter", function (e) {
            if (cell.scrollWidth > cell.clientWidth) {
                const tooltip = document.createElement("div");
                tooltip.className = "tooltip";
                tooltip.innerText = cell.innerText;

                document.body.appendChild(tooltip);

                const rect = cell.getBoundingClientRect();
                tooltip.style.left = `${rect.left + window.scrollX}px`;
                tooltip.style.top = `${rect.bottom + window.scrollY + 5}px`;
                tooltip.style.visibility = "visible";
                tooltip.style.opacity = "1";

                // Salva referência ao tooltip
                cell._tooltip = tooltip;

                // Adiciona eventos ao tooltip para mantê-lo visível
                tooltip.addEventListener("mouseenter", () => {
                    tooltip.style.visibility = "visible";
                    tooltip.style.opacity = "1";
                });

                tooltip.addEventListener("mouseleave", () => {
                    tooltip.remove();
                    cell._tooltip = null;
                });
            }
        });

        cell.addEventListener("mouseleave", function () {
            if (cell._tooltip) {
                // Adiciona pequeno atraso para permitir que o cursor alcance o tooltip
                setTimeout(() => {
                    if (cell._tooltip && !cell._tooltip.matches(':hover')) {
                        cell._tooltip.remove();
                        cell._tooltip = null;
                    }
                }, 100); // 100ms para suavizar a transição
            }
        });
    });
});

//Exibe o texto completo ao passar o mouse nas células
//document.addEventListener("DOMContentLoaded", function () {
//    const tableCells = document.querySelectorAll("#TabelaListaDeCadastros td");

//    tableCells.forEach(cell => {
//        cell.addEventListener("mouseenter", function (e) {
//            if (cell.scrollWidth > cell.clientWidth) {
//                const tooltip = document.createElement("div");
//                tooltip.className = "tooltip";
//                tooltip.innerText = cell.innerText;

//                document.body.appendChild(tooltip);

//                const rect = cell.getBoundingClientRect();
//                tooltip.style.left = `${rect.left + window.scrollX}px`;
//                tooltip.style.top = `${rect.bottom + window.scrollY + 5}px`;
//                tooltip.style.visibility = "visible";
//                tooltip.style.opacity = "1";

//                cell._tooltip = tooltip;
//            }
//        });

//        cell.addEventListener("mouseleave", function () {
//            if (cell._tooltip) {
//                cell._tooltip.remove();
//                cell._tooltip = null;
//            }
//        });
//    });
//});

/* Comportamento dinâmico do PlaceHolder padrão do tipo Date: cinza antes de ter dados, cor normal de pois de ter dados*/
document.addEventListener("DOMContentLoaded", function () {
    const dateInputs = document.querySelectorAll('input[type="date"]');

    dateInputs.forEach(input => {
        // Atualiza a classe com base no valor inicial
        if (!input.value) {
            input.classList.add("placeholder-active");
        }

        // Remove a classe ao preencher o campo
        input.addEventListener("input", function () {
            if (input.value) {
                input.classList.remove("placeholder-active");
            } else {
                input.classList.add("placeholder-active");
            }
        });

        // Atualiza a classe ao carregar os valores do banco de dados
        input.addEventListener("blur", function () {
            if (input.value) {
                input.classList.remove("placeholder-active");
            } else {
                input.classList.add("placeholder-active");
            }
        });

        // Quando o campo recebe o foco, o texto ficará preto
        input.addEventListener("focus", function () {
            input.style.color = "black"; // Texto preto ao focar
        });

        // Quando o campo perde o foco, verifica se deve voltar à cor original
        input.addEventListener("blur", function () {
            if (!input.value) {
                input.style.color = "lightgray"; // Cor de placeholder se estiver vazio
            }
        });
    });
});





